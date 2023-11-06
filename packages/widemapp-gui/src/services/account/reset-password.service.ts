import { UnwrapRef } from "@vue/composition-api";
import BaseService from "../../common/http/base/baseService";

interface IResetPasswordResponse{}

interface IResetPasswordRequest{
    token?:string;
    email?:string;
    password?:string;
}

class ResetPasswordService extends BaseService<IResetPasswordRequest, IResetPasswordResponse>{
    public async SendAsync(bodyData?: UnwrapRef<IResetPasswordRequest>): Promise<any>
    {
        this.Message = "Password is reseted successfully";
        return await this.SendBody("POST","/api/account/reset-password", {bodyData});
    } 
}

export {ResetPasswordService, IResetPasswordRequest, IResetPasswordResponse};