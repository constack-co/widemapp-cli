import { UnwrapRef } from "@vue/composition-api";
import BaseService from "../../common/http/base/baseService";

interface IUpdateFileResponse{}

interface IUpdateFileRequest{
    id:string;
    name:string;
}

class UpdateFileService extends BaseService<IUpdateFileRequest, IUpdateFileResponse>{
    public async SendAsync(bodyData?: UnwrapRef<IUpdateFileRequest>): Promise<any>
    {
        this.Message = "Successfully added.";

        return await this.SendBody("PUT","api/templates/file", {bodyData});
    }
}

export {UpdateFileService, IUpdateFileRequest, IUpdateFileResponse};