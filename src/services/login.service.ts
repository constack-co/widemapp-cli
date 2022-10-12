import { LoginApiService } from "../apis/account/login-api.service";
import { ScriptService } from "./script.service";
import mkdirp from 'mkdirp';
import * as fs from 'fs/promises';
import { join, normalize } from "path";

export class LoginService {
    private loginApiService = new LoginApiService();
    private scriptService = new ScriptService();
    private credentials: {
        token?: string
    } = { token: undefined }; 

    async handle(data: {username: string, password: string}) {
        await this.loginApiService.SendAsync({
            emailOrUsername: data.username, 
            password: data.password
        }).then(async (response) => {
            this.credentials.token = response['token'];
            let rootPath: any = await this.scriptService.run('echo %PATH%');
            // if linux
            if (rootPath == '%PATH%') {
                rootPath = await this.scriptService.run('echo $PATH');
            }
            rootPath = rootPath.split(';')[0].replace('\\bin', '');
            const resolvedPath = normalize(join(rootPath, '.widemapp'));

            await this.fixPathToCredencials(resolvedPath);
            console.log('Login Succeeded');
        })
    }

    async fixPathToCredencials(resolvedPath: string) {
        const completedPath = normalize(join(resolvedPath, '/credentials.json'));
        await mkdirp(resolvedPath).then(async () => {
            await fs.access(completedPath).then(async() => {
                await fs.unlink(completedPath);
                await fs.writeFile(completedPath, JSON.stringify(this.credentials))
                .then(() => {})
                .catch(error => console.log('error', error));
            }).catch(async () => {
                await fs.writeFile(completedPath, JSON.stringify(this.credentials))
                .then(() => {})
                .catch(error => console.log('error', error));
            })
        })
    }
}