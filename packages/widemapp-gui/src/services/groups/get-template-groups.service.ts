import { UnwrapRef } from "@vue/composition-api";
import BaseService from "../../common/http/base/baseService";

interface IGetTemplateGroupsResponse {
    id: string,
    name: string,
    templateId: string,
    type: string
}

interface IGetTemplateGroupsRequest {
    templateId: string
}

class GetTemplateGroupsService extends BaseService<IGetTemplateGroupsRequest, IGetTemplateGroupsResponse[]>{
    public async RequestAsync(params?: UnwrapRef<IGetTemplateGroupsRequest>): Promise<any> {
        return await this.SendParam("GET", "api/groups/template", {params});
    }
}

export { GetTemplateGroupsService, IGetTemplateGroupsRequest, IGetTemplateGroupsResponse }; 