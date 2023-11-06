import { UnwrapRef } from "@vue/composition-api";
import BaseService from "../../common/http/base/baseService";

interface IUpdateGroupResponse{}

interface IUpdateGroupRequest{
    id:string;
    name:string;
}

class UpdateGroupService extends BaseService<IUpdateGroupRequest, IUpdateGroupResponse>{
    public async SendAsync(bodyData?: UnwrapRef<IUpdateGroupRequest>): Promise<any>
    {
        this.Message = "Successfully added.";

        return await this.SendBody("PUT","api/templates/group", {bodyData});
    }
}

export {UpdateGroupService, IUpdateGroupRequest, IUpdateGroupResponse};