import { RouteConfig } from "vue-router";
import RouteNameEnum from '@/common/enums/routeNameEnum';

const importBase = (path:string):any => {
    return import("@/views/guest/"+path);
}

const UnrestrictedRoutes:Array<RouteConfig> = [
    {
        path: "/",
        name:RouteNameEnum.Login,
        component: () => importBase("account/Login.vue"),
        meta: ['AllowOnlyAnnonymous']
    },
    {
        path: "/forget-password",
        name:RouteNameEnum.ForgotPassword,
        component: () => importBase("account/ForgotPassword.vue"),
        meta: ['AllowOnlyAnnonymous']
    },
    {
        path:"/sign-up",
        name:RouteNameEnum.Register,
        component: () => importBase("account/SignUp.vue"),
        meta: ['AllowOnlyAnnonymous']
    },
    {
        path:"/reset-password",
        name:RouteNameEnum.ResetPassword,
        component: () => importBase("account/ResetPassword.vue"),
        meta: ['AllowOnlyAnnonymous']
    },
    {
        path:"/documentation",
        name: RouteNameEnum.Documentation,
        component: () => importBase("documentation/Documentation.vue"),
        meta: ['AllowAnnonymous']
    }
];

export default UnrestrictedRoutes;