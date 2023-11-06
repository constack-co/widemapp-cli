import RouteNameEnum from '@/common/enums/routeNameEnum';
import LocalStorage from '@/common/funcs/localStorage';
import { JwtClaims } from '@/common/funcs/jwtClaims';

export default function auth({ next, router, to }: any) {
    if(JwtClaims.claims == undefined) JwtClaims.initialize();

    else if(JwtClaims.claims.expDate < new Date()) LocalStorage.removeToken();
    
    if(to.meta[0] == "AllowOnlyAnnonymous" && LocalStorage.hasToken)
        return router.push({name: RouteNameEnum.Dashboard});

    if (!LocalStorage.hasToken && to.meta[0] != "AllowOnlyAnnonymous" && to.meta[0] != "AllowAnnonymous") 
        return router.push({ name: RouteNameEnum.Login });

    return next();
}