import { UnwrapRef } from "@vue/composition-api";
import BaseService from "../../common/http/base/baseService";

interface IGetFileRequest {
    id: string,
}

interface IGetFileResponse {
    id: string,
    name: string,
    action: string,
    contentAdd: string,
    fileEdits: FileEditResponse[],
    groupId: string,
    templateId: string
}

type FileEditResponse =
{
    id: string;
    placeholder: string;
    content: string;
    fileId: string;
}

class GetFileService extends BaseService<IGetFileRequest, IGetFileResponse[]>{
    public async RequestAsync(params?: UnwrapRef<IGetFileRequest>): Promise<any> {
        return await this.SendParam("GET", "api/templates/file", {params});
    }
}

export { GetFileService, IGetFileRequest, IGetFileResponse }; 