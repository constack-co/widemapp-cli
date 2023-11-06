import { UnwrapRef } from "@vue/composition-api";
import BaseService from "../../common/http/base/baseService";

interface IGetEntitiesResponse {
    id: string,
    name: string,
    properties: PropertiesResponse,
    inputs: string[],
    outputs: string[]
}

type PropertiesResponse = {
    id: string;
    name: string;
    type: PropertyTypeTypeResponse;
    isNullable: boolean;
}

type PropertyTypeTypeResponse = {
    name: string;
}

interface IGetEntitiesRequest {
    projectId: string
}

class GetEntitiesService extends BaseService<IGetEntitiesRequest, IGetEntitiesResponse[]>{
    public async RequestAsync(params?: UnwrapRef<IGetEntitiesRequest>): Promise<any> {
        return await this.SendParam("GET", "api/entities", {params});
    }
}

export { GetEntitiesService, IGetEntitiesRequest, IGetEntitiesResponse }; 