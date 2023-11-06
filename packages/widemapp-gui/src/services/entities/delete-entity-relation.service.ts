import { UnwrapRef } from "@vue/composition-api";
import BaseService from "../../common/http/base/baseService";

interface IDeleteEntityRelationResponse{}

interface IDeleteEntityRelationRequest{
    entityFromId: string;
    entityToId: string;
    propertyFromId?: string;
    propertyToId: string;
}

class DeleteEntityRelationService extends BaseService<IDeleteEntityRelationRequest, IDeleteEntityRelationResponse>{

    public async RequestAsync(params?: UnwrapRef<IDeleteEntityRelationRequest>): Promise<any>
    {
        this.Message = "Successfully Deleted.";

        return await this.SendParam("DELETE","api/entities/relation", {params});
    }
    
}

export {DeleteEntityRelationService, IDeleteEntityRelationRequest, IDeleteEntityRelationResponse};