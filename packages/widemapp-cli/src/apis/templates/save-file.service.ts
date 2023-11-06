import BaseService from '../base/base.service';

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

interface ISaveFileResponse{}

class SaveFileService extends BaseService<ISaveFileRequest, ISaveFileResponse> {
    public async sendAsync(bodyData?: ISaveFileRequest): Promise<any> {
        return new Promise(async (resolve, reject) => {
            await this.sendBody('POST', 'api/templates/save-file', { bodyData }).then(response => {
                resolve(response);
            }).catch(error => reject(error));
        });
    }
}

export { SaveFileService, ISaveFileRequest, ISaveFileResponse };
