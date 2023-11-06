import { UnwrapRef } from "@vue/composition-api";
import BaseService from "../../common/http/base/baseService";

interface IDeleteFileOrGroupResponse{}

interface IDeleteFileOrGroupRequest{
    id:string;
    type:string;
}

class DeleteFileOrGroupService extends BaseService<IDeleteFileOrGroupRequest, IDeleteFileOrGroupResponse>{
    public async RequestAsync(params?: UnwrapRef<IDeleteFileOrGroupRequest>): Promise<any>
    {
        this.Message = "Successfully Deleted.";

        return await this.SendParam("DELETE","api/templates/file-group", {params});
    }
}

export {DeleteFileOrGroupService, IDeleteFileOrGroupRequest, IDeleteFileOrGroupResponse};