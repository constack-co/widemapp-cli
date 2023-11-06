import { UnwrapRef } from "@vue/composition-api";
import BaseService from "../../common/http/base/baseService";

interface IGetGenerationTypesResponse {}

interface IGetGenerationTypesRequest {}

class GetGenerationTypesService extends BaseService<IGetGenerationTypesRequest, IGetGenerationTypesResponse[]>{
    public async RequestAsync(params?: UnwrapRef<IGetGenerationTypesRequest>): Promise<any> {
        return await this.SendParam("GET", "api/plans/generation-types", {params});
    }
}

export { GetGenerationTypesService, IGetGenerationTypesRequest, IGetGenerationTypesResponse }; 