import { UnwrapRef } from "@vue/composition-api";
import BaseService from "../../common/http/base/baseService";

interface ISaveFileResponse{}

interface ISaveFileRequest{
    id: string;
    contentAdd?: string;
    fileEdits?: FileEditRequest[];
}

type FileEditRequest =
{
    id?: string;
    placeholder: string;
    content: string;
}

class SaveFileService extends BaseService<ISaveFileRequest, ISaveFileResponse>{
    public async SendAsync(bodyData?: UnwrapRef<ISaveFileRequest>): Promise<any>
    {
        this.Message = "Successfully added.";

        return await this.SendBody("POST","api/templates/save-file", {bodyData});
    }
}

export {SaveFileService, ISaveFileRequest, ISaveFileResponse};