import { UnwrapRef } from "@vue/composition-api";
import BaseService from "../../common/http/base/baseService";

interface IGetUsersResponse {
    id: string,
    firstName: string,
    lastName: string,
    email: string,
    userName: string,
    projects: string[]
}

interface IGetUsersRequest {}

class GetUsersService extends BaseService<IGetUsersRequest, IGetUsersResponse[]>{
    public async RequestAsync(params?: UnwrapRef<IGetUsersRequest>): Promise<any> {
        return await this.SendParam("GET", "api/users", {params});
    }
}

export { GetUsersService, IGetUsersRequest, IGetUsersResponse }; 