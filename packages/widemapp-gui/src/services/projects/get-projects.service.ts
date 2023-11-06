import { UnwrapRef } from "@vue/composition-api";
import BaseService from "../../common/http/base/baseService";

interface IGetProjectsResponse {
    id: string,
    name: string,
}

interface IGetProjectsRequest {}

class GetProjectsService extends BaseService<IGetProjectsRequest, IGetProjectsResponse[]>{
    public async RequestAsync(params?: UnwrapRef<IGetProjectsRequest>): Promise<any> {
        return await this.SendParam("GET", "api/projects", {params});
    }
}

export { GetProjectsService, IGetProjectsRequest, IGetProjectsResponse }; 