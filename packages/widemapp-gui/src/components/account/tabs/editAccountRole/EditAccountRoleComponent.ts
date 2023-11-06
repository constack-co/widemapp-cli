import { Component, Ref, Prop, reactive, mixins } from "@/importBase";
import { GetRolesService } from "@/services/roles/GetRolesService";
import { IGetUserByIdResponse } from "@/services/users/GetUserByIdService";
import {EditUserRoleService, IEditUserRoleRequest} from "@/services/users/EditUserRoleService";

@Component
export default class EditAccountRole extends mixins(){
  private getRolesService = new GetRolesService();
  private editUserRoleService = new EditUserRoleService();

  @Prop()
  protected UserData: Ref<IGetUserByIdResponse> | undefined;

  private model = reactive(<IEditUserRoleRequest>{
    id: "",
    roleId: ""
  });

  async mounted() {
    this.getRolesService.RequestAsync().then(() => {
        this.model.id = this.UserData!.value.id;
        this.model.roleId = this.UserData!.value.roleId;
    });
  }

  protected async submitForm(){
      await this.editUserRoleService.RequestAsync(this.model).then(() => {
        this.UserData!.value.roleId = this.model.roleId;
      });
  }

  get isDisabled(){
    return this.model.roleId == this.UserData!.value.roleId;
  }
}