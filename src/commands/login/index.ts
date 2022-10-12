import {Command, Flags} from '@oclif/core'
import { LoginService } from '../../services/login.service'

export default class LoginCommand extends Command {
    static description = 'Authenticate => widemapp login'

    static examples = [
        `$ widemapp login`,
    ]

    static usage = 'login'

    static flags = {
        username: Flags.string({
            description: 'pass username or email',
            required: true
        }),
        password: Flags.string({
            description: 'pass password',
            required: true
        })
    }

    async run(): Promise<void> {
        const {flags} = await this.parse(LoginCommand);

        const loginService = new LoginService();
        await loginService.handle({username: flags.username, password: flags.password});
    }
}
