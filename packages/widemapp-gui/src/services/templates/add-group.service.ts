import { UnwrapRef } from "@vue/composition-api";
import BaseService from "../../common/http/base/baseService";

interface IAddGroupResponse{
    id: string;
}

interface IAddGroupRequest{
    name: string;
    type: string;
    groupId?: string;
    generationTypeId?: string;
    templateId: string;
}

class AddGroupService extends BaseService<IAddGroupRequest, IAddGroupResponse>{
    public async SendAsync(bodyData?: UnwrapRef<IAddGroupRequest>): Promise<any>
    {
        this.Message = "Successfully added.";

        return await this.SendBody("POST","api/templates/group", {bodyData});
    }
}

export {AddGroupService, IAddGroupRequest, IAddGroupResponse};