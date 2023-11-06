import { UnwrapRef } from "@vue/composition-api";
import BaseService from "../../common/http/base/baseService";

interface IGetRolesResponse{
    id:string,
    name:string
}

interface IGetRolesRequest{}

class GetRolesService extends BaseService<IGetRolesRequest, IGetRolesResponse[]>{

    public async RequestAsync(params?: UnwrapRef<IGetRolesRequest>): Promise<IGetRolesResponse[]>
    {
        return await this.SendParam("GET","api/Roles", {params});
    } 
}

export {GetRolesService, IGetRolesRequest, IGetRolesResponse};