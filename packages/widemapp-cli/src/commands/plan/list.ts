import {Command, Flags} from '@oclif/core'
import { FetchPlansService } from '../../services/fetch-plans.service'

export default class PlanListCommand extends Command {
    static description = 'List plans => widemapp plan:list'

    static examples = [
        `$ widemapp plan:list`,
    ]

    static usage = 'plan:list'

    async run(): Promise<void> {
        const fetchPlansService = new FetchPlansService();
        await fetchPlansService.handle();
        for(const plan of fetchPlansService.plans) {
            this.log(`id: ${plan.id} | name: ${plan.name}`)
        }
    }
}
