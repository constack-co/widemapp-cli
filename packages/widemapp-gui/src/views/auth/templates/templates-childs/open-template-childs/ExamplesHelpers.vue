<template>
    <div>
        Discover more:
        <a href="https://handlebarsjs.com/guide/">https://handlebarsjs.com/guide/</a>
        <v-divider></v-divider>
        <CodeEditor v-model="examplesHelpers.value" 
        :language_selector="false"
        :display_language="false"
        spellcheck="false"
        :wrap_code="true"
        width="auto"
        :read_only="true"
    ></CodeEditor>   
    </div>
</template>

<script lang="ts">
import {Vue, Component} from "@/importBase";
import { ref } from "@vue/composition-api";
import CodeEditor from 'simple-code-editor';


@Component({
    components: {
        CodeEditor
    }
})
export default class ExamplesHelpers extends Vue{
    async mounted() {
    }

    public examplesHelpers = ref(`
// example input => change format to this
{{camelCase mainEntity.name}} // changeFormatToThis
{{snakeCase mainEntity.name}} // change_format_to_this

{{capitalCase mainEntity.name}} // Change Format To This
{{titleCase mainEntity.name}} // Change Format To This
{{headerCase mainEntity.name}} // Change-Format-To-This

{{kebabCase mainEntity.name}} // change-format-to-this
{{dashCase mainEntity.name}} // change-format-to-this
{{paramCase mainEntity.name}} // change-format-to-this

{{dotCase mainEntity.name}} // change.format.to.this
{{pathCase mainEntity.name}} // change/format/to/this

{{pascalCase mainEntity.name}} // ChangeFormatToThis
{{properCase mainEntity.name}} // ChangeFormatToThis

{{lowerCase mainEntity.name}} // change format to this
{{upperCase mainEntity.name}} // CHANGE FORMAT TO THIS

{{sentenceCase mainEntity.name}} // Change format to this
{{constantCase mainEntity.name}} // CHANGE_FORMAT_TO_THIS

{{noCase mainEntity.name}} // change format to this

// ===================================================

// example input => change
{{plural mainEntity.name}} // from "change" to "changes"
{{singular mainEntity.name}} // from "changes" to "change"

// ===================================================
// nested helpers, input => changes
{{constantCase  (singular planName)}} // from "changes" to "CHANGE"
`)
}
</script>
