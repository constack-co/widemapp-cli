import { UnwrapRef } from "@vue/composition-api";
import BaseService from "../../common/http/base/baseService";
import LocalStorage from '@/common/funcs/localStorage';
import TokenEnum from "@/common/enums/tokenEnum";

interface ILoginResponse{
    accessToken:string|undefined;
    expiresIn:string|undefined;
    schema:string|undefined;
}

interface ILoginRequest{
    emailOrUsername?:string;
    password?:string;
}

class LoginService extends BaseService<ILoginRequest, ILoginResponse>{
    public async SendAsync(bodyData?: UnwrapRef<ILoginRequest>): Promise<any>
    {
        this.Message = "Successfully logged in";
        return new Promise(async (resolve,reject) => {
            await this.SendBody("POST","api/account/login", {bodyData}).then((response) => {
                LocalStorage.setToken(response[TokenEnum.Name]);
                resolve(response);
            }).catch((error) => reject(error));
        });
    } 
}

export {LoginService, ILoginRequest, ILoginResponse};