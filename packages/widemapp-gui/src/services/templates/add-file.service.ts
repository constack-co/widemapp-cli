import { UnwrapRef } from "@vue/composition-api";
import BaseService from "../../common/http/base/baseService";

interface IAddFileResponse{}

interface IAddFileRequest{
    name:string;
    groupId?:string;
    templateId:string;
    action: string;
}

class AddFileService extends BaseService<IAddFileRequest, IAddFileResponse>{
    public async SendAsync(bodyData?: UnwrapRef<IAddFileRequest>): Promise<any>
    {
        this.Message = "Successfully added.";

        return await this.SendBody("POST","api/templates/file", {bodyData});
    }
}

export {AddFileService, IAddFileRequest, IAddFileResponse};