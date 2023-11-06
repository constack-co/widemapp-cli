import { UnwrapRef } from "@vue/composition-api";
import BaseService from "../../common/http/base/baseService";

interface IAddRelationResponse{}

interface IAddRelationRequest{
    entityFromId?:string;
    entityToId?:string;
    propertyFromId?:string;
    propertyToId?:string;
}

class AddRelationService extends BaseService<IAddRelationRequest, IAddRelationResponse>{
    public async SendAsync(bodyData?: UnwrapRef<IAddRelationRequest>): Promise<any>
    {
        this.Message = "Successfully added.";

        return await this.SendBody("POST","api/entities/relation", {bodyData});
    }
}

export {AddRelationService, IAddRelationRequest, IAddRelationResponse};