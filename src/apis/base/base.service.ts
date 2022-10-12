import axios, { Method } from "axios";
import http from "./http.configs";
import { stringify } from 'qs';
import { join, normalize } from "path";
import { ScriptService } from "../../services/script.service";
import * as fs from 'fs/promises';

export default class BaseService<TRequest, TResponse> {
    private cancelRequest: Function | undefined = undefined;
    private CancelToken = axios.CancelToken;
    public Response:TResponse|undefined;
    public TotalPages:number|undefined;
    public TotalItems:number|undefined;
    private scriptService = new ScriptService();
    private accessToken: string = '';

    async getCredentials() {
        let rootPath: any = await this.scriptService.run('echo %PATH%');
        // if linux
        if (rootPath == '%PATH%') {
            rootPath = await this.scriptService.run('echo $PATH');
        }
        rootPath = rootPath.split(';')[0].replace('\\bin', '');
        const resolvedPath = normalize(join(rootPath, '.widemapp/credentials.json'));
        await fs.readFile(resolvedPath, 'utf-8').then(async (filedata) => {
            const token = JSON.parse(filedata).token;
            this.accessToken = token;
        }).catch(() => {});
    }

    protected async SendBody(methodType: Method, url: string, options?: {bodyData?:TRequest}): Promise<any> {
        if (this.cancelRequest != undefined) this.cancelRequest;

        let finalParams = {};

        let app = this;

        await this.getCredentials();

        return new Promise(async (resolve,reject) => {
            await http.request({
                url: url,
                method: methodType,
                data: options?.bodyData ?? {},
                params: finalParams,
                headers: {'Content-Type': 'application/json', 'Authorization': `Bearer ${this.accessToken}`},
                cancelToken: new this.CancelToken(function executor(c) { app.cancelRequest = c })
            }).then((response) => {
                this.Response = response.data;
                resolve(response.data as TResponse);
            }).catch((error) => {
                console.log(error.response.data.message)
                reject(error)
            });
        });
    }

    protected async SendParam<TRequest, TResponse>(methodType: Method, url: string, options?: {params?:TRequest}): Promise<any> {
        
        if (this.cancelRequest != undefined) this.cancelRequest;

        let finalParams = {};

        if(options?.params) Object.assign(finalParams, options.params);

        let app = this;

        await this.getCredentials();

        return new Promise(async (resolve,reject) => {
            await http.request(
            {
                url: url,
                method: methodType,
                params: finalParams,
                headers: {'Authorization': `Bearer ${this.accessToken}`},
                paramsSerializer: params => stringify(params, { arrayFormat: 'repeat' }),
                cancelToken: new this.CancelToken(function executor(c) { app.cancelRequest = c })
            }).then((response) => {
                if('data' in response.data && 'totalPages' in response.data){
                    this.Response = response.data.data;
                    this.TotalPages = response.data.totalPages;
                    this.TotalItems = response.data.totalItems;
                    resolve(response.data.data as TResponse);
                }else{
                    this.Response = response.data;
                    resolve(response.data as TResponse);
                }
            }).catch((error) => {
                console.log(error.response.data.message)
                reject(error)
            });
        });
    }
}