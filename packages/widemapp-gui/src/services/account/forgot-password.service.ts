import { HeadersType } from "@Services/IModels";
import { UnwrapRef } from "@vue/composition-api";
import BaseService from "../../common/http/base/baseService";

interface IForgotPasswordResponse{}

interface IForgotPasswordRequest{
    email?:string;
    token: string;
}

class ForgotPasswordService extends BaseService<IForgotPasswordRequest, IForgotPasswordResponse>{
    public async SendAsync(bodyData?: UnwrapRef<IForgotPasswordRequest>, customHeaders?: HeadersType[]): Promise<any>
    {
        this.Message = "Link to reset password is sent to your email.";
        return await this.SendBody("POST","api/account/forget-password", {bodyData, customHeaders: customHeaders});
    } 
}

export {ForgotPasswordService, IForgotPasswordRequest, IForgotPasswordResponse};