import BaseService from '../base/base.service';

interface IGetTreeViewByGroupIdResponse {
    id: string,
    name: string,
    type: string,
    contentAdd?: string,
    fileEdits?: FileEditResponse[],
    action?: string,
    groupId?: string,
    templateId: string,
    generationTypeId?: string,
    children: IGetTreeViewByGroupIdResponse[]
}

type FileEditResponse =
{
    id: string;
    placeholder: string;
    content: string;
    fileId: string;
}

interface IGetTreeViewByGroupIdRequest {
    groupId: string
}

class GetTreeViewByGroupIdService extends BaseService<IGetTreeViewByGroupIdRequest, IGetTreeViewByGroupIdResponse[]>{
    public async requestAsync(params?: IGetTreeViewByGroupIdRequest): Promise<any> {
        return await this.sendParam('GET', 'api/templates/tree-view/group', { params });
    }
}

export { GetTreeViewByGroupIdService, IGetTreeViewByGroupIdRequest, IGetTreeViewByGroupIdResponse, FileEditResponse};
