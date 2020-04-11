import { AspPrerenderData } from './ViewModels'

class Config {
    readonly apiSettings: AspPrerenderData;

    constructor() {
        if (global && global['prerenderedData']) {
            this.apiSettings = global['prerenderedData'];
        }
    };
}

export default new Config();