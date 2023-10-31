import BaseService from '../base/base.service';

interface IAddTemplateResponse{}

interface IAddTemplateRequest{
    name:string;
    projectId: string;
}

class AddTemplateService extends BaseService<IAddTemplateRequest, IAddTemplateResponse> {
    public async sendAsync(bodyData?: IAddTemplateRequest): Promise<any> {

        return new Promise(async (resolve, reject) => {
            await this.sendBody('POST', 'api/templates', { bodyData }).then(response => {
                resolve(response);
            }).catch(error => reject(error));
        });
    }
}

export { AddTemplateService, IAddTemplateRequest, IAddTemplateResponse };
