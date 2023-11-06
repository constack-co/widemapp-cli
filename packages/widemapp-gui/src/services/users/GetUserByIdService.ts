import { UnwrapRef } from "@vue/composition-api";
import BaseService from "../../common/http/base/baseService";

interface IGetUserByIdResponse {
    id: string,
    firstName: string,
    lastName: string,
    email: string,
    userName: string,
    roleId: string,
    projects: string[]
}

interface IGetUserByIdRequest {
    id: string
}

class GetUserByIdService extends BaseService<IGetUserByIdRequest, IGetUserByIdResponse>{
    public async RequestAsync(params?: UnwrapRef<IGetUserByIdRequest>): Promise<any> {
        return await this.SendParam("GET", "api/users/:id", {params});
    }
}

export { GetUserByIdService, IGetUserByIdRequest, IGetUserByIdResponse }; 