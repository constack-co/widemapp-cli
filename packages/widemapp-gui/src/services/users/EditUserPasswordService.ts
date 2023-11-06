import { UnwrapRef } from "@vue/composition-api";
import BaseService from "../../common/http/base/baseService";

interface IEditUserPasswordResponse {
}

interface IEditUserPasswordRequest {
    id: string,
    currentPassword: string,
    newPassword:string
}

class EditUserPasswordService extends BaseService<IEditUserPasswordRequest, IEditUserPasswordResponse>{
    public async RequestAsync(bodyData?: UnwrapRef<IEditUserPasswordRequest>): Promise<any> {
        this.Message = "Successfully Password Updated!";

        return await this.SendBody("PUT", "api/users/:password", {bodyData});
    }
}

export { EditUserPasswordService, IEditUserPasswordRequest, IEditUserPasswordResponse };