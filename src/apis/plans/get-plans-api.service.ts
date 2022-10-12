import BaseService from "../base/base.service";

type GetPlansApiRequest = {}

type GetPlansApiResponse = {
    id: string;
    name: string;
}

class GetPlansApiService extends BaseService<GetPlansApiRequest, GetPlansApiResponse[]>{
    public async RequestAsync(params?: GetPlansApiRequest): Promise<any> {
        return await this.SendParam("GET", "api/plans", {params});
    }
}

export { GetPlansApiService, GetPlansApiRequest, GetPlansApiResponse }; 