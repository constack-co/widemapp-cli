import { UnwrapRef } from "@vue/composition-api";
import BaseService from "../../common/http/base/baseService";

interface IGetAllTemplatesResponse {
    id: string,
    name: string,
}

interface IGetAllTemplatesRequest {
    projectId: string;
}

class GetAllTemplatesService extends BaseService<IGetAllTemplatesRequest, IGetAllTemplatesResponse[]>{
    public async RequestAsync(params?: UnwrapRef<IGetAllTemplatesRequest>): Promise<any> {
        return await this.SendParam("GET", "api/templates", {params});
    }
}

export { GetAllTemplatesService, IGetAllTemplatesRequest, IGetAllTemplatesResponse }; 