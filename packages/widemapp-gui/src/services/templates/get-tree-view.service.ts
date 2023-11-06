import { UnwrapRef } from "@vue/composition-api";
import BaseService from "../../common/http/base/baseService";

interface IGetTreeViewResponse {
    id: string,
    name: string,
    type: string,
    groupId?: string,
    templateId: string,
    generationTypeId?: string;
    children: IGetTreeViewResponse[]
}

interface IGetTreeViewRequest {
    templateId: string
}

class GetTreeViewService extends BaseService<IGetTreeViewRequest, IGetTreeViewResponse[]>{
    public async RequestAsync(params?: UnwrapRef<IGetTreeViewRequest>): Promise<any> {
        return await this.SendParam("GET", "api/templates/tree-view", {params});
    }
}

export { GetTreeViewService, IGetTreeViewRequest, IGetTreeViewResponse }; 