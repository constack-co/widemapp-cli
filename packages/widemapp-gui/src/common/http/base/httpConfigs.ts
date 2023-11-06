import TokenEnum from "@/common/enums/tokenEnum";
import LocalStorage from "@/common/funcs/localStorage";
import axios from "axios";
import ApplicationEnum from '../../enums/applicationEnum';
import snackBarMessage from "../../funcs/snackBarMessage";
import HttpStatusCodeEnum from '../../enums/httpStatusCodeEnum';
import router from "@/router/base/routerBase";
import RouteNameEnum from '../../enums/routeNameEnum';

let config = {
    baseURL: process.env.VUE_APP_BACKEND_URL,
    timeout: 5000,
    headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
    }
};

let instance = axios.create(config);

instance.interceptors.request.use((config) => {
    let token = LocalStorage.getToken();

    if (token) config.headers.Authorization = TokenEnum.Schema + " " + token;
    
    return config
});

instance.interceptors.response.use(function (response) {
    return response;
}, async function (error) {
    if (axios.isCancel(error)) {
        return new Promise(() => { });
    } else if (error.response === undefined) {
        await snackBarMessage.Error("Server network error");
    } else if (error.response!.status === HttpStatusCodeEnum.UNAUTHORIZED) {
        LocalStorage.removeToken();
        router.push({ name: RouteNameEnum.Login })
    } else {
        await snackBarMessage.ShowErrors(error);
    }
    return Promise.reject(error);
})

export default instance;