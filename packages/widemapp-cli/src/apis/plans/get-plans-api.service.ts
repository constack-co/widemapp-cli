import BaseService from "../base/base.service";

type GetPlansApiRequest = {}

type GetPlansApiResponse = {
    id: string;
    name: string;
}

class GetPlansApiService extends BaseService<GetPlansApiRequest, GetPlansApiResponse[]>{
    public async requestAsync(params?: GetPlansApiRequest): Promise<any> {
        return await this.sendParam("GET", "api/plans", {params});
    }
}

export { GetPlansApiService, GetPlansApiRequest, GetPlansApiResponse };
