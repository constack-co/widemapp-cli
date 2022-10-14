import BaseService from "../base/base.service";

type GetPlansByIdApiRequest = {
    id: string;
}

type GetPlansByIdApiResponse = {
    planId: string;
    planName: string;
    template: TemplateModel;
    mainEntity: EntityModel;
    planGroupNames: string[]
    planApis: PlanApisModel[];
    generationTypes: GenerationTypesModel[];
}

type TemplateModel = {
    id: string;
    name: string;
}

type EntityModel = {
    id: string;
    name: string;
    relations: RelationModel[];
    properties: PropertyModel[]
}

type RelationModel = {
    from: string;
    to: string;
    fromPort: string;
    toPort: string;
}

type PropertyModel = {
    id: string;
    name: string;
    isNullable: boolean;
    type: PropertyTypeModel
}

type PropertyTypeModel = {
    name: string;
    value: string;
}

type PlanApisModel = {
    id: string;
    name: string;
    endpoint: string;
    entity: EntityModel;
    method: MethodModel;
    groupName: string;
    apiRequests: ApiRequestsModel[];
    apiResponses: ApiResponsesModel[];
}

type ApiRequestsModel = {
    id: string;
    name: string;
    isNullable?: boolean;
}

type ApiResponsesModel = {
    id: string;
    name: string;
    isNullable?: boolean;
}

type MethodModel = {
    id: string;
    name: string;
}

type GenerationTypesModel = {
    id: string;
    name: string;
    value: string;
}

class GetPlansByIdApiService extends BaseService<GetPlansByIdApiRequest, GetPlansByIdApiResponse>{
    public async RequestAsync(params?: GetPlansByIdApiRequest): Promise<any> {
        return await this.SendParam("GET", "api/plans/id", {params});
    }
}

export { GetPlansByIdApiService, GetPlansByIdApiRequest, GetPlansByIdApiResponse }; 