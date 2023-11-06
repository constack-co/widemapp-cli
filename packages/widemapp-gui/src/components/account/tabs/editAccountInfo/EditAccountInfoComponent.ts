import { Component, Prop, Ref, ref, mixins } from "@/importBase";
import { IGetUserByIdResponse } from "@/services/users/GetUserByIdService";
import {
    EditUserService,
    IEditUserRequest,
} from "@/services/users/EditUserService";
import AutoMapper from "@Common/funcs/autoMapper";
import Rules from "@Common/funcs/rules";

@Component
export default class EditAccount extends mixins(){
    protected rules = new Rules();
    private editUserService = new EditUserService();
    private autoMapper = new AutoMapper();

    private editModel = ref(<IEditUserRequest>{
        id: "",
        userName: "",
        email: "",
        firstName: "",
        lastName: "",
        projectIds: [],
    });

    @Prop()
    protected UserData: Ref<IGetUserByIdResponse> | undefined;

    protected async mounted() {
        await this.autoMapper.MapDataReverseAsync(
            this.UserData?.value,
            this.editModel.value
        );
    }

    protected async resetForm() {
        await this.autoMapper.MapDataReverseAsync(
            this.UserData?.value,
            this.editModel.value
        );
    }

    get isFormUpdated(): boolean {
        return this.autoMapper.HaveSameData(
            this.editModel.value,
            this.UserData?.value
        );
    }

    protected async submitForm() {
        if ((this.$refs.submitEditForm as any).validate()) {
            await this.editUserService
                .RequestAsync(this.editModel.value)
                .then(async () => await this.autoMapper.MapDataAsync(this.editModel.value, this.UserData?.value));
        }
    }
}