import { UnwrapRef } from "@vue/composition-api";
import BaseService from "../../common/http/base/baseService";

interface IEditUserResponse {
}

interface IEditUserRequest {
    id: string,
    userName: string,
    email:string
    firstName: string,
    lastName: string,
}

class EditUserService extends BaseService<IEditUserRequest, IEditUserResponse>{
    public async RequestAsync(bodyData?: UnwrapRef<IEditUserRequest>): Promise<any> {
        this.Message = "Successfully Updated!";

        return await this.SendBody("PUT", "api/users/:update", {bodyData});
    }
}

export { EditUserService, IEditUserRequest, IEditUserResponse };