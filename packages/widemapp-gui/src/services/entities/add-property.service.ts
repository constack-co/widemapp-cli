import { UnwrapRef } from "@vue/composition-api";
import BaseService from "../../common/http/base/baseService";

interface IAddPropertyResponse{
    id: string
}

interface IAddPropertyRequest{
    name:string;
    isNullable:boolean;
    propertyTypeId:string;
    entityId:string;
}

class AddPropertyService extends BaseService<IAddPropertyRequest, IAddPropertyResponse>{
    public async SendAsync(bodyData?: UnwrapRef<IAddPropertyRequest>): Promise<any>
    {
        this.Message = "Successfully added.";

        return await this.SendBody("POST","api/entities/property", {bodyData});
    }
}

export {AddPropertyService, IAddPropertyRequest, IAddPropertyResponse};