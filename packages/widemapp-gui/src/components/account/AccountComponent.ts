import { Vue, Component, reactive } from "@/importBase";
import { JwtClaims } from "@Common/funcs/jwtClaims";
import { GetUserByIdService } from "@/services/users/GetUserByIdService";

@Component
export default class AccountComponentSc extends Vue{
  protected getUserByIdService = new GetUserByIdService({id: this.$route.params.id ?? JwtClaims.claims!.Id});

  private viewModel = reactive({
    tabs: [ {text:"Account Info"}, {text:"Security"}, {text:"Role", roles:["Administrator"]}]
  })

  private filterTabs(){
    this.viewModel.tabs = this.viewModel.tabs.filter(function(tab){
      if(!("roles" in tab)) return true;
      else return tab.roles!.includes(JwtClaims.claims!.Role);
    });
  }

  protected async mounted() {
    this.$nextTick(async () => {
      await this.getUserByIdService.RequestAsync();
      this.filterTabs();
    });
  }
}