<template>
    <v-app id="inspire">
        <v-navigation-drawer
        permanent
        app
        >
        <v-sheet
            color="grey lighten-4"
            class="pa-4"
        >

            <div>Widemapp Documentation</div>
        </v-sheet>

        <v-divider></v-divider>

        <v-list>
            <v-list-item link @click="scrollToElement('get-started')">
                <v-list-item-icon>
                    <v-icon></v-icon>
                </v-list-item-icon>

                <v-list-item-content>
                    <v-list-item-title>Getting Started</v-list-item-title>
                </v-list-item-content>
            </v-list-item>
            <v-list-item link @click="scrollToElement('install')">
                <v-list-item-icon>
                    <v-icon></v-icon>
                </v-list-item-icon>

                <v-list-item-content>
                    <v-list-item-title>Installation</v-list-item-title>
                </v-list-item-content>
            </v-list-item>
            <v-list-item link @click="scrollToElement('auth')">
                <v-list-item-icon>
                    <v-icon></v-icon>
                </v-list-item-icon>

                <v-list-item-content>
                    <v-list-item-title>Authenticate</v-list-item-title>
                </v-list-item-content>
            </v-list-item>
            <v-list-item link @click="scrollToElement('entities')">
                <v-list-item-icon>
                    <v-icon></v-icon>
                </v-list-item-icon>

                <v-list-item-content>
                    <v-list-item-title>Manage Entities</v-list-item-title>
                </v-list-item-content>
            </v-list-item>
            <v-list-item link @click="scrollToElement('templates')">
                <v-list-item-icon>
                    <v-icon></v-icon>
                </v-list-item-icon>

                <v-list-item-content>
                    <v-list-item-title>Manage Templates</v-list-item-title>
                </v-list-item-content>
            </v-list-item>
            <v-list-item link @click="scrollToElement('plans')">
                <v-list-item-icon>
                    <v-icon></v-icon>
                </v-list-item-icon>

                <v-list-item-content>
                    <v-list-item-title>Build & Execute Plan</v-list-item-title>
                </v-list-item-content>
            </v-list-item>
            <v-list-item link @click="scrollToElement('helpers')">
                <v-list-item-icon>
                    <v-icon></v-icon>
                </v-list-item-icon>

                <v-list-item-content>
                    <v-list-item-title>Helpers</v-list-item-title>
                </v-list-item-content>
            </v-list-item>
        </v-list>
        </v-navigation-drawer>

        <v-main>
        <v-container
            class="py-8 px-6"
            fluid
        >
            <v-row>
            <v-col>
                <v-card class="my-2" id="get-started">
                    <v-card-title primary-title>
                        Getting Started
                    </v-card-title>
                    
                    <v-card-text>
                        Widemapp is an automation tool to generate the most repeated processes. It helps to develop CRUDs, RESTful APIs, end-to-end features, and more... The tool is designed to be limitless and customized to developers.
                    </v-card-text>
                </v-card>

                <v-card class="my-2" id="install">
                    <v-card-title primary-title>
                        Installation & Usage
                    </v-card-title>
                    <v-card-text>
                        Widemapp work using GUI and CLI together.
                        
                        <div class="mt-5">
                            <CodeEditor heading="Install CLI" 
                                fileText="$ npm install -g @constack/widemapp-cli"
                            />
                        </div>

                    </v-card-text>
                </v-card>

                <v-card class="my-2" id="auth">
                    <v-card-title primary-title>
                        Authenticate
                    </v-card-title>
                    <v-card-text>                        
                        <p class="text-subtitle-2 mb-0">Authentication is mandatory because you can't fetch or execute plans without credencials.</p>
                    
                        <CodeEditor
                            class="mt-0"
                            lang="markdown"
                            heading="Login command" 
                            fileText="$ widemapp login --username your-username --password your-password"
                            />
                    </v-card-text>
                </v-card>

                <v-card class="my-2" id="entities">
                    <v-card-title primary-title>
                        Manage Entity or UML Diagram
                    </v-card-title>
                    <v-card-text>
                        <p class="text-subtitle-2 mb-0">Create UML (Define you entities) <a href="/entities">https://widemapp.constack.co/entities</a></p>
                        
                        <p class="text-subtitle-2 mb-0">
                            You can click ADD NEW ENTITY or you can find a button to create by right click mouse.
                        </p>
                        <p class="text-subtitle-2 mb-0">
                            A modal asks for the entity name and you can submit the button.
                        </p>
                        <p class="text-subtitle-2 mb-5">
                            You can create properties after you create the entity. So there is the UI to select name, type, isNullable...
                        </p>
                        <p class="text-subtitle-2 mb-0">Note: First you have to select an existing project. If you have no projects you have to create one.</p>
                    </v-card-text>
                </v-card>

                <v-card class="my-2" id="templates">
                    <v-card-title primary-title>
                        Manage Templates
                    </v-card-title>
                    <v-card-text>
                        <p class="text-subtitle-2 mb-0">Page to manage templates <a href="/templates">https://widemapp.constack.co/templates</a></p>
                        
                        <p class="text-subtitle-2 mb-5">
                            In this page you can CRUD templates, and open a template to create groups, files, folders.
                        </p>
                        <p class="text-subtitle-2 mb-0">Note: Importing templates from local machine will be added in the next version.</p>

                    </v-card-text>
                </v-card>

                <v-card class="my-2" id="plans">
                    <v-card-title primary-title>
                        Build & Execute Plan
                    </v-card-title>
                    <v-card-text>

                        <p class="text-subtitle-2 mb-0">Using <a href="/plans">https://widemapp.constack.co/plans</a>
                            you can configure plans and select all data needed for PAYLOAD
                        </p>

                        <p class="text-subtitle-2 mb-0">PAYLOAD is a generated object which is used to pass parameters to files/folders.</p>
                    
                        <p class="text-subtitle-2 mb-4">Once you finish a plan, you can open a terminal to your project path, list plans to get the plan id and run the plan by id.</p>

                        <CodeEditor
                            lang="markdown"
                            heading="Commands" 
                            fileText="$ widemapp plan:list"
                            codeText="
                            USAGE
                            widemapp plan:list

                            DESCRIPTION
                            List plans => widemapp plan:list"
                        />

                        <CodeEditor
                            class="mt-2"
                            lang="markdown"
                            fileText="$ widemapp plan:run"
                            codeText="
                            USAGE
                            widemapp plan:run

                            FLAGS
                            --back=<value>   pass back-end path
                            --front=<value>  pass front-end path
                            --id=<value>     (required) pass plan id

                            DESCRIPTION
                            Run a plan => widemapp plan:run"
                        />

                        <CodeEditor
                            class="mt-0"
                            lang="markdown"
                            heading="Example" 
                            fileText="$ widemapp plan:run --back ./backend/ --front ./frontend --id 00b8b2b1-8b53-4114-b79a-53c4222a31ee"
                        />
                        
                    </v-card-text>
                </v-card>

                <v-card class="my-2" id="helpers">
                    <v-card-title primary-title>
                        Helpers
                    </v-card-title>
                    <v-card-text>
                    <p class="text-subtitle-2">Read more: <a href="https://handlebarsjs.com/" target="_blank"> https://handlebarsjs.com/</a></p>
                    <CodeEditor
                            class="mt-2"
                            lang="hbs"
                            fileText="Helpers"
                            :codeText="helpers" />

                    </v-card-text>
                </v-card>
            </v-col>
            </v-row>
        </v-container>
        </v-main>
    </v-app>
</template>

<script lang="ts">
import CodeEditor from "@/components/account/CodeEditor.vue";
import {Vue, Component} from "@/importBase";

@Component({
    components: {
        CodeEditor
    }
})
export default class Documentation extends Vue{
    public scrollToElement(id: string) {
        const element = document.querySelector(`#${id}`);
        element?.scrollIntoView({ behavior: "smooth" });
    }

    public helpers = `{{!-> example input => change format to this
{{camelCase mainEntity.name}} {{!-> changeFormatToThis
{{snakeCase mainEntity.name}} {{!-> change_format_to_this

{{capitalCase mainEntity.name}} {{!-> Change Format To This
{{titleCase mainEntity.name}} {{!-> Change Format To This
{{headerCase mainEntity.name}} {{!-> Change-Format-To-This

{{kebabCase mainEntity.name}} {{!-> change-format-to-this
{{dashCase mainEntity.name}} {{!-> change-format-to-this
{{paramCase mainEntity.name}} {{!-> change-format-to-this

{{dotCase mainEntity.name}} {{!-> change.format.to.this
{{pathCase mainEntity.name}} {{!-> change/format/to/this

{{pascalCase mainEntity.name}} {{!-> ChangeFormatToThis
{{properCase mainEntity.name}} {{!-> ChangeFormatToThis

{{lowerCase mainEntity.name}} {{!-> change format to this
{{upperCase mainEntity.name}} {{!-> CHANGE FORMAT TO THIS

{{sentenceCase mainEntity.name}} {{!-> Change format to this
{{constantCase mainEntity.name}} {{!-> CHANGE_FORMAT_TO_THIS

{{noCase mainEntity.name}} {{!-> change format to this

{{!-> ===================================================

{{!-> example input => change
{{plural mainEntity.name}} {{!-> from "change" to "changes"
{{singular mainEntity.name}} {{!-> from "changes" to "change"

{{!-> ===================================================
{{!-> nested helpers, input => changes
{{constantCase  (singular planName)}} {{!-> from "changes" to "CHANGE"`;
}
</script>
