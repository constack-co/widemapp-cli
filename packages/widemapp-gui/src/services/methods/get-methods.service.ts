import { UnwrapRef } from "@vue/composition-api";
import BaseService from "../../common/http/base/baseService";

interface IGetMethodsResponse {}

interface IGetMethodsRequest {}

class GetMethodsService extends BaseService<IGetMethodsRequest, IGetMethodsResponse[]>{
    public async RequestAsync(params?: UnwrapRef<IGetMethodsRequest>): Promise<any> {
        return await this.SendParam("GET", "api/methods", {params});
    }
}

export { GetMethodsService, IGetMethodsRequest, IGetMethodsResponse }; 