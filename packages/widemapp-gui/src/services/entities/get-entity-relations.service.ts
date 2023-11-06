import { UnwrapRef } from "@vue/composition-api";
import BaseService from "../../common/http/base/baseService";

interface IGetEntityRelationsResponse {
    from: string,
    to: string,
    fromPort: string,
    toPort: string
}

interface IGetEntityRelationsRequest {}

class GetEntityRelationsService extends BaseService<IGetEntityRelationsRequest, IGetEntityRelationsResponse[]>{
    public async RequestAsync(params?: UnwrapRef<IGetEntityRelationsRequest>): Promise<any> {
        return await this.SendParam("GET", "api/entities/relations", {params});
    }
}

export { GetEntityRelationsService, IGetEntityRelationsRequest, IGetEntityRelationsResponse }; 