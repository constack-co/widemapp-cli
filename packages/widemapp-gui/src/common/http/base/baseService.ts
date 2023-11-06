import axios, { Method } from "axios";
import { ref, Ref, UnwrapRef } from "@vue/composition-api";
import http from '../base/httpConfigs';
import snackBarMessage from '@/common/funcs/snackBarMessage';
import { HeadersType, IRoleData } from "@/services/IModels";

class BaseService<TRequest, TResponse>{
    private cancelRequest: Ref<Function | undefined> = ref(undefined);
    private CancelToken = axios.CancelToken;
    protected Message: string | undefined;
    private Qs = require("qs");
    public Loader:Ref<Boolean> = ref(false);
    public TotalPages:Ref<number|undefined> = ref(undefined);
    public TotalItems:Ref<number|undefined> = ref(undefined);
    public Response:Ref<TResponse|undefined> = ref<TResponse>();

    constructor(public defaultRoleData: UnwrapRef<IRoleData> | undefined = undefined) {
    }

    protected async SendBody(methodType: Method, url: string, options?: {
        bodyData?:UnwrapRef<TRequest>, 
        roleData?:UnwrapRef<IRoleData>,
        customHeaders?: HeadersType[]
    }): Promise<any> {
        if (this.cancelRequest.value != undefined) this.cancelRequest.value();

        let finalParams = {};
        if(options?.roleData) Object.assign(finalParams, options.roleData);
        if(this.defaultRoleData) Object.assign(finalParams, this.defaultRoleData);

        let app = this;

        let headers = {'Content-Type': 'application/json'};

        for (const header of options?.customHeaders ?? []) {
            headers[header.name] = header.value;
        }

        return new Promise(async (resolve,reject) => {
            this.Loader.value = true;
            await http.request({
                url: url,
                method: methodType,
                data: options?.bodyData ?? {},
                params: finalParams,
                headers: headers,
                cancelToken: new this.CancelToken(function executor(c) { app.cancelRequest.value = c })
            }).then((response) => {
                if (this.Message != undefined) snackBarMessage.Success(this.Message);    
                this.Response.value = response.data;
                resolve(response.data as TResponse);
            }).catch((error) => reject(error)).finally(() => this.Loader.value = false);
        });
    }

    protected async SendParam<TRequest, TResponse>(methodType: Method, url: string, options?: {params?:UnwrapRef<TRequest>, roleData?:UnwrapRef<IRoleData>}): Promise<any> {
        
        if (this.cancelRequest.value != undefined) this.cancelRequest.value();

        let finalParams = {};

        if(options?.params) Object.assign(finalParams, options.params);
        if(options?.roleData) Object.assign(finalParams, options.roleData);
        if(this.defaultRoleData) Object.assign(finalParams, this.defaultRoleData); 

        this.Loader.value = true;

        let app = this;

        return new Promise(async (resolve,reject) => {
            await http.request(
            {
                url: url,
                method: methodType,
                params: finalParams,
                paramsSerializer: params => this.Qs.stringify(params, { arrayFormat: 'repeat' }),
                cancelToken: new this.CancelToken(function executor(c) { app.cancelRequest.value = c })
            }).then((response) => {
                if (this.Message != undefined) snackBarMessage.Success(this.Message);
                if('data' in response.data && 'totalPages' in response.data){
                    this.Response.value = response.data.data;
                    this.TotalPages.value = response.data.totalPages;
                    this.TotalItems.value = response.data.totalItems;
                    resolve(response.data.data as TResponse);
                }else{
                    this.Response.value = response.data;
                    resolve(response.data as TResponse);
                }
            }).catch((error) => reject(error)).finally(() => this.Loader.value = false);
        });
    }
}

export default BaseService;