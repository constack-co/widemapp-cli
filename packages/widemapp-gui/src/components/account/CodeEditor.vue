<template>
    <div>
        <div v-if="getHeading" class="text-subtitle-1">
        {{getHeading}}
        </div>
        <div class="code-editor overflow-auto text-wrap text-subtitle-2">
            <div v-if="getFileText" class="file-text">
                {{getFileText}}
            </div>
            <div v-if="getCodeText" class="code-text-parent">
                <div v-for="(text, index) in getCodeText?.split(/\r\n|\r|\n/)" :key="index" class="code-text">
                    <span class="index-value">{{index}}</span>
                    <span class="text-value" v-html="hightLight(text).value"></span>
                </div>
            </div>
            
        </div>
    </div>
    
</template>
    
<script lang="ts">

import hljs from 'highlight.js';
import 'highlight.js/styles/github-dark.css';

import {Vue, Component, Prop} from "@/importBase";

@Component
export default class CodeEditor extends Vue {
    @Prop()
    heading: any;

    get getHeading() {
        return this.heading;
    }

    @Prop()
    fileText: any;

    get getFileText() {
        return this.fileText;
    }

    @Prop()
    codeText: any;

    get getCodeText() {
        return this.codeText;
    }

    @Prop()
    lang: any;

    get getLang() {
        return this.lang;
    }
    
    hightLight(value: any) {
        return hljs.highlight(value , {language: this.getLang ?? 'javascript'})
    }
}

</script>

<style scoped>
.code-editor {
    background-color: #24292f; 
    border-radius: 10px;
    border: 1px solid #30363d;
}

.code-text-parent {
    padding-top: 10px;
    padding-bottom: 10px;
}
.code-text {
    padding-left: 25px;
}
.index-value {
    color: #6e7681;
}
.text-value {
    margin-left: 5px;
}
.file-text {
    background-color: #161b22;
    padding: 10px;
    border-radius: 10px;;
    color: white;
}
</style>
    