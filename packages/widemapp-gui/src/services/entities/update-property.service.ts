import { UnwrapRef } from "@vue/composition-api";
import BaseService from "../../common/http/base/baseService";

interface IUpdatePropertyRequest{
    propertyId: string;
    attributeToUpdate: string;
    name:string;
    isNullable:boolean;
    propertyTypeId:string;
}

interface IUpdatePropertyResponse{}

class UpdatePropertyService extends BaseService<IUpdatePropertyRequest, IUpdatePropertyResponse>{
    public async SendAsync(bodyData?: UnwrapRef<IUpdatePropertyRequest>): Promise<any>
    {
        this.Message = "Successfully added.";

        return await this.SendBody("PUT","api/entities/property", {bodyData});
    }
}

export {UpdatePropertyService, IUpdatePropertyRequest, IUpdatePropertyResponse};