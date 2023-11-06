import { Vue, Component, ref, Prop, Ref, reactive } from "@/importBase";
import Rules from "@Common/funcs/rules";
import {
  EditUserPasswordService,
  IEditUserPasswordRequest,
} from "@/services/users/EditUserPasswordService";
import { IGetUserByIdResponse } from "@/services/users/GetUserByIdService";

@Component
export default class EditSecurity extends Vue {
  private editUserPasswordService = new EditUserPasswordService();

  protected rules = new Rules();

  protected viewModel = reactive({
    confirmPassword: "",
    showNewPassword: false,
    showConfirmPassword: false,
    showCurrentPassword: false,
  });

  @Prop()
  protected UserData: Ref<IGetUserByIdResponse> | undefined;

  protected async mounted() {
    this.passwordModel.value.id = this.UserData!.value.id;
  }

  private passwordModel = ref(<IEditUserPasswordRequest>{});

  protected async submitForm() {
    if ((this.$refs.submitSecurityForm as any).validate()) {
      this.editUserPasswordService
        .RequestAsync(this.passwordModel.value)
        .then(() => {

          this.passwordModel.value = <IEditUserPasswordRequest>{
            id: this.UserData!.value.id
          };

          this.viewModel.confirmPassword = "";
          (this.$refs.submitSecurityForm as any).resetValidation();
        });
    }
  }
}