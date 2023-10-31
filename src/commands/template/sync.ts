import { Command, } from '@oclif/core';
import { TemplateService } from '../../services/template.service';

export default class PlanListCommand extends Command {
    static description = 'Template Sync => widemapp template:sync'

    static examples = [
      '$ widemapp template:sync',
    ]

    static usage = 'template:sync'

    async run(): Promise<void> {
        const templateService = new TemplateService();
        await templateService.handle();
    }
}
