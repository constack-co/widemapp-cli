import BaseService from '../base/base.service';

interface IAddGroupResponse{
    id: string;
}

interface IAddGroupRequest{
    name: string;
    type: string;
    groupId?: string | null;
    generationTypeId?: string | null;
    templateId: string;
}

class AddGroupService extends BaseService<IAddGroupRequest, IAddGroupResponse> {
    public async sendAsync(bodyData?: IAddGroupRequest): Promise<any> {
        // return await this.sendBody('POST','api/templates/group', { bodyData});

        return new Promise(async (resolve, reject) => {
            await this.sendBody('POST', 'api/templates/group', { bodyData }).then(response => {
                resolve(response);
            }).catch(error => reject(error));
        });
    }
}

export { AddGroupService, IAddGroupRequest, IAddGroupResponse };
