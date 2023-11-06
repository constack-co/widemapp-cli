import Handlebars from 'handlebars';
import { compilerHelpers } from '../helpers/compiler.helpers';

export class CompilerService {
    constructor() {
        compilerHelpers(Handlebars);
    }

    compile(input: any, content: any) : any {
        const template = Handlebars.compile(content);
        return template(input);
    }
}