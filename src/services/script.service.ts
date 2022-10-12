import { exec } from 'child_process';

export class ScriptService {

    async run(command: string) {
        return new Promise(function (resolve, reject) {
            exec(command, {maxBuffer: 1024 * 5000}, (err, stdout, stderr) => {
                if (err) {
                    reject(err);
                } else {
                    resolve(stdout);
                }
                });
            });
    }
}