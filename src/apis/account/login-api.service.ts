import BaseService from "../base/base.service";

interface ILoginResponse{
    accessToken:string|undefined;
    expiresIn:string|undefined;
    schema:string|undefined;
}

interface ILoginRequest{
    emailOrUsername?:string;
    password?:string;
}

class LoginApiService extends BaseService<ILoginRequest, ILoginResponse>{
    public async SendAsync(bodyData?: ILoginRequest): Promise<any>
    {
        return new Promise(async (resolve,reject) => {
            await this.SendBody("POST","api/account/login", {bodyData}).then((response) => {
                resolve(response);
            }).catch((error) => reject(error));
        });
    } 
}

export {LoginApiService, ILoginRequest, ILoginResponse};