import { GetPlansApiService, GetPlansApiResponse } from "../apis/plans/get-plans-api.service";

export class FetchPlansService {
    private getPlansApiService = new GetPlansApiService();
    public plans: GetPlansApiResponse[] = [];

    async handle() {
        await this.getPlansApiService.requestAsync().then((response: GetPlansApiResponse[]) => {
            this.plans = response;
        })
        return this.plans;
    }
}