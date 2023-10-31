import BaseService from '../base/base.service';

interface IAddFileResponse{}

interface IAddFileRequest{
    name:string;
    type: string;
    groupId?: string | null;
    templateId:string;
    action: string;
}

class AddFileService extends BaseService<IAddFileRequest, IAddFileResponse> {
    public async sendAsync(bodyData?: IAddFileRequest): Promise<any> {
        // return await this.sendBody('POST', 'api/templates/file', { bodyData });

        return new Promise(async (resolve, reject) => {
            await this.sendBody('POST', 'api/templates/file', { bodyData }).then(response => {
                resolve(response);
            }).catch(error => reject(error));
        });
    }
}

export { AddFileService, IAddFileRequest, IAddFileResponse };
