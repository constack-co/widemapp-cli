<template>
    <div>
        <v-combobox
            v-model="responsePropsModelProp"
            :filter="filter"
            :hide-no-data="!search"
            :items="responsePropsItemsProp"
            :search-input.sync="search"
            hide-selected
            label="Search for an option"
            multiple
            small-chips
            solo
            >
            <template v-slot:no-data>
                <v-list-item>
                <span class="subheading">Create</span>
                <v-chip
                    :color="`${colors[nonce - 1]} lighten-3`"
                    label
                    small
                >
                    {{ search }}
                </v-chip>
                </v-list-item>
            </template>
            <template v-slot:selection="{ attrs, item, parent, selected }">
                <v-chip
                v-if="item === Object(item)"
                v-bind="attrs"
                :color="`${item.color} lighten-3`"
                :input-value="selected"
                label
                small
                >
                <span class="pr-2">
                    {{ item.text }}
                </span>
                <v-icon
                    small
                    @click="parent.selectItem(item);removeFromModel(item)"
                >
                    $delete
                </v-icon>
                </v-chip>
            </template>
            <template v-slot:item="{ index, item }">
                <v-text-field
                v-if="editing === item"
                v-model="editing.text"
                autofocus
                flat
                background-color="transparent"
                hide-details
                solo
                @keyup.enter="edit(index, item)"
                ></v-text-field>
                <v-chip
                v-else
                :color="`${item.color} lighten-3`"
                dark
                label
                small
                >
                {{ item.text }}
                </v-chip>
                <v-spacer></v-spacer>
                <v-list-item-action @click.stop>
                <v-btn
                    icon
                    @click.stop.prevent="edit(index, item)"
                >
                    <v-icon>{{ editing !== item ? 'mdi-pencil' : 'mdi-check' }}</v-icon>
                </v-btn>
                </v-list-item-action>
            </template>
        </v-combobox>
    </div>
</template>


<script lang="ts">
import {Vue, Component, reactive, Watch} from "@/importBase";
import { Prop } from "vue-property-decorator";

@Component
export default class ComboboxResponses extends Vue{
    public colors = ['green', 'purple', 'indigo', 'cyan', 'teal', 'orange'];
    public editing: any = null;
    public editingIndex = -1;

    @Prop()
    responsePropsItems: any;

    @Prop()
    responsePropsModel: any;

    @Prop()
    api: any;

    get responsePropsItemsProp() {
        return this.responsePropsItems;
    }

    get responsePropsModelProp() {
        return this.responsePropsModel;
    }

    set responsePropsModelProp(value) {
        if (value.length === 1) {
            this.onModelChanged(value, undefined)
        }
        else {
            const prev: any = [];
            for(let i = 0; i < this.responsePropsModel.length; i++) {
                if (i != this.responsePropsModel.length) {
                    prev.push(this.responsePropsModel[i])
                }
            }
            this.onModelChanged(value, prev)
        }
    }

    get getApiProp() {
        return this.api;
    }

    public nonce = 1;

    public search = null;

    @Watch('responsePropsModel', {
        deep: true,
        immediate: false
    })
    onModelChanged(val: any, prev: any) {
        if (prev && val.length === prev.length) {
            const cases: any = []
            for (let i = 0; i < prev.length; i++) {
                if (val[i] == prev[i]) {
                    cases.push(true)
                }
                else {
                    cases.push(false)
                }
            }
            if (cases.every(x => x == true)) return;
        }
        val.map(v => {
            if (typeof v === 'string') {
                v = {
                    text: v,
                    color: this.colors[this.nonce - 1],
                }

                this.$emit('updateResponsePropsItem', {item: this.getApiProp, prop: v});

                this.nonce++
            }
            this.$emit('updateResponsePropsModel', {item: this.getApiProp, prop: v, action: 'ADD' } )
        })
    }

    removeFromModel(item) {
        this.$emit('updateResponsePropsModel', {item: this.getApiProp, prop: item, action: 'REMOVE' } )
    }

    edit (index, item) {
        if (!this.editing) {
            this.editing = item
            this.editingIndex = index
        } else {
            this.editing = null
            this.editingIndex = -1
        }
    }

    filter (item, queryText, itemText) {
        if (item.header) return false

        const hasValue = val => val != null ? val : ''

        const text = hasValue(itemText)
        const query = hasValue(queryText)

        return text.toString()
            .toLowerCase()
            .indexOf(query.toString().toLowerCase()) > -1
    }
}
</script>