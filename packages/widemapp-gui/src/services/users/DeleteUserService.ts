import { UnwrapRef } from "@vue/composition-api";
import BaseService from "../../common/http/base/baseService";

interface IDeleteUserResponse{}

interface IDeleteUserRequest{
    id:string;
}

class DeleteUserService extends BaseService<IDeleteUserRequest, IDeleteUserResponse>{

    public async RequestAsync(bodyData?: UnwrapRef<IDeleteUserRequest>): Promise<any>
    {
        this.Message = "Successfully Deleted.";

        return await this.SendBody("DELETE","api/users/:delete", {bodyData});
    }
    
}

export {DeleteUserService, IDeleteUserRequest, IDeleteUserResponse};