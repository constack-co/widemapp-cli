import jwt_decode from "jwt-decode";
import LocalStorage from '@/common/funcs/localStorage';

interface IJwtClaims {
    FirstName: string,
    LastName: string,
    Email: string,
    Role: string,
    RoleId: string,
    UserName: string,
    ZipCode: string,
    exp: number,
    expDate: Date,
    Id:string
}

class JwtClaims {
    public static claims: IJwtClaims | undefined = undefined;

    public static async initialize(): Promise<void> {
        if(LocalStorage.hasToken){
            JwtClaims.claims = jwt_decode(LocalStorage.getToken());
            if (JwtClaims.claims == undefined) {
                LocalStorage.removeToken();
            } else {
                JwtClaims.claims.expDate = new Date((JwtClaims.claims.exp * 1000));
            }
        }
    }
}
export { IJwtClaims, JwtClaims }