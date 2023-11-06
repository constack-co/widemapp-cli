import { UnwrapRef } from "@vue/composition-api";
import BaseService from "../../common/http/base/baseService";

interface IEditUserRoleResponse {
}

interface IEditUserRoleRequest {
    id: string,
    roleId: string
}

class EditUserRoleService extends BaseService<IEditUserRoleRequest, IEditUserRoleResponse>{
    public async RequestAsync(bodyData?: UnwrapRef<IEditUserRoleRequest>): Promise<any> {
        this.Message = "Role is updated successfully!";

        return await this.SendBody("PUT", "api/users/:update-role", {bodyData});
    }
}

export { EditUserRoleService, IEditUserRoleRequest, IEditUserRoleResponse };