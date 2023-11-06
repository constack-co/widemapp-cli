import {Command, Flags} from '@oclif/core'
import { RunPlanService } from '../../services/run-plan.service'

export default class PlanRunCommand extends Command {
  static description = 'Run a plan => widemapp plan:run'

  static examples = [
    `$ widemapp run:plan`,
  ]

  static usage = 'plan:run'

  static flags = {
    id: Flags.string({
      description: 'pass plan id',
      required: true
    }),
    front: Flags.string({
      description: 'pass front-end path',
      required: false
    }),
    back: Flags.string({
      description: 'pass back-end path',
      required: false
    })
  }

  async run(): Promise<void> {
    const {flags} = await this.parse(PlanRunCommand);
    const runPlanService: RunPlanService = new RunPlanService({
      frontPath: flags.front,
      backPath: flags.back
    });
    await runPlanService.handle({params: {
      id: flags.id
    }});
  }
}
