import { UnwrapRef } from "@vue/composition-api";
import BaseService from "../../common/http/base/baseService";

interface IAddEntityResponse{
    id: string;
}

interface IAddEntityRequest{
    name:string;
    projectId: string;
}

class AddEntityService extends BaseService<IAddEntityRequest, IAddEntityResponse>{
    public async SendAsync(bodyData?: UnwrapRef<IAddEntityRequest>): Promise<any>
    {
        this.Message = "Successfully added.";

        return await this.SendBody("POST","api/entities", {bodyData});
    }
}

export {AddEntityService, IAddEntityRequest, IAddEntityResponse};