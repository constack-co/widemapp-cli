import { HeadersType } from "@Services/IModels";
import { UnwrapRef } from "@vue/composition-api";
import BaseService from "../../common/http/base/baseService";

interface IAddUserResponse{}

interface IAddUserRequest{
    firstName?:string;
    lastName?:string;
    username?:string;
    email?:string;
    password?:string;
    token: string;
}

class AddUserService extends BaseService<IAddUserRequest, IAddUserResponse>{
    public async SendAsync(bodyData?: UnwrapRef<IAddUserRequest>, customHeaders?: HeadersType[]): Promise<any>
    {
        this.Message = "Successfully Signed Up.";

        return await this.SendBody("POST","api/users", {bodyData, customHeaders: customHeaders});
    }
}

export {AddUserService, IAddUserRequest, IAddUserResponse};