import BaseService from "../base/base.service";

type GetGroupsByNameApiRequest = {
    groupName: string;
    templateId?: string;
}

type GetGroupsByNameApiResponse = {
    id: string;
    generationType: GenerationTypeModel;
}

type GenerationTypeModel = {
    id: string;
    value: string;
}

class GetGroupsByNameApiService extends BaseService<GetGroupsByNameApiRequest, GetGroupsByNameApiResponse[]>{
    public async RequestAsync(params?: GetGroupsByNameApiRequest): Promise<any> {
        return await this.SendParam("GET", "api/groups/by-name", {params});
    }
}

export { GetGroupsByNameApiService, GetGroupsByNameApiRequest, GetGroupsByNameApiResponse }; 