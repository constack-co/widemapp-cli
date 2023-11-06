import { UnwrapRef } from "@vue/composition-api";
import BaseService from "../../common/http/base/baseService";

interface IGetPropertyTypesResponse {
    id: string;
    name: string;
}

interface IGetPropertyTypesRequest {}

class GetPropertyTypesService extends BaseService<IGetPropertyTypesRequest, IGetPropertyTypesResponse[]>{
    public async RequestAsync(params?: UnwrapRef<IGetPropertyTypesRequest>): Promise<any> {
        return await this.SendParam("GET", "api/entities/properties", {params});
    }
}

export { GetPropertyTypesService, IGetPropertyTypesRequest, IGetPropertyTypesResponse }; 