import { exec } from 'node:child_process';

export class ScriptService {
    // eslint-disable-next-line @typescript-eslint/explicit-module-boundary-types
    async run(command: string) {
        return new Promise(function (resolve, reject) {
            exec(command, { maxBuffer: 1024 * 5000 }, (err, stdout) => {
                if (err) {
                    reject(err);
                } else {
                    resolve(stdout);
                }
            });
        });
    }
}