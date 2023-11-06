import { UnwrapRef } from "@vue/composition-api";
import BaseService from "../../common/http/base/baseService";

interface IAddTemplateResponse{}

interface IAddTemplateRequest{
    name:string;
    projectId: string;
}

class AddTemplateService extends BaseService<IAddTemplateRequest, IAddTemplateResponse>{
    public async SendAsync(bodyData?: UnwrapRef<IAddTemplateRequest>): Promise<any>
    {
        this.Message = "Successfully added.";

        return await this.SendBody("POST","api/templates", {bodyData});
    }
}

export {AddTemplateService, IAddTemplateRequest, IAddTemplateResponse};