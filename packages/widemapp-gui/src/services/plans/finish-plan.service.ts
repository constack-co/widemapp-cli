import { UnwrapRef } from "@vue/composition-api";
import BaseService from "../../common/http/base/baseService";

interface IFinishPlanResponse{
    id: string;
}

interface IFinishPlanRequest{
    planName: string,
    mainEntityId: string,
    templateId: string,
    generateTypeIds: string[],
    planGroupNames?: string[],
    apis: ApiTypeRequest[]
}

type ApiTypeRequest = {
    id: string,
    name: string,
    methodId: string,
    endpoint: string,
    groupName: string,
    entityId: string,
    apiRequests: PropertyType[],
    apiResponses: PropertyType[]
}

type PropertyType = {
    name: string,
    type: string,
    isNullable: boolean
}

class FinishPlanService extends BaseService<IFinishPlanRequest, IFinishPlanResponse>{
    public async SendAsync(bodyData?: UnwrapRef<IFinishPlanRequest>): Promise<any>
    {
        this.Message = "Successfully added.";

        return await this.SendBody("POST","api/plans/finish", {bodyData});
    }
}

export {FinishPlanService, IFinishPlanRequest, IFinishPlanResponse, ApiTypeRequest};