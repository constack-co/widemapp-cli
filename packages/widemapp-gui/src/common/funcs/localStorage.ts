import TokenEnum from "../enums/tokenEnum";
import { JwtClaims } from "./jwtClaims";

class LocalStorage
{
    public static hasToken:boolean = false;

    static setToken(token: string):void{
        localStorage.setItem(TokenEnum.Name, token);
        LocalStorage.hasToken = true;
    }

    static replaceToken(token: string): void {
        localStorage.removeItem(TokenEnum.Name);
        localStorage.setItem(TokenEnum.Name, token);
    }

    static getToken():string{
        return localStorage.getItem(TokenEnum.Name) as string;
    }

    static removeToken():void{
        localStorage.removeItem(TokenEnum.Name);
        LocalStorage.hasToken = false;
        JwtClaims.claims = undefined;
        localStorage.removeItem('SELECTED_PROJECT')
    }

    static initializeToken(){
        if(LocalStorage.getToken())
            LocalStorage.hasToken = true;
        else 
            LocalStorage.hasToken = false;
    }
}

export default LocalStorage;