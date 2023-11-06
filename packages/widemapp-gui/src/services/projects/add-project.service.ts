import { UnwrapRef } from "@vue/composition-api";
import BaseService from "../../common/http/base/baseService";

interface IAddProjectResponse{}

interface IAddProjectRequest{
    name:string;
}

class AddProjectService extends BaseService<IAddProjectRequest, IAddProjectResponse>{
    public async SendAsync(bodyData?: UnwrapRef<IAddProjectRequest>): Promise<any>
    {
        this.Message = "Successfully added.";

        return await this.SendBody("POST","api/projects", {bodyData});
    }
}

export {AddProjectService, IAddProjectRequest, IAddProjectResponse};