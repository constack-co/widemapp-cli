import {
    camelCase,
    capitalCase,
    constantCase,
    dotCase,
    headerCase,
    noCase,
    paramCase,
    pascalCase,
    pathCase,
    sentenceCase,
    snakeCase
} from "change-case";
import { titleCase } from "title-case";
import { lowerCase } from "lower-case";
import { upperCase } from "upper-case";
import {plural, singular} from 'pluralize';

export const compilerHelpers = (complier: any) => {
    complier.registerHelper('camelCase', function (data: any) {
        return camelCase(data);
    });

    complier.registerHelper('capitalCase', function (data: any) {
        return capitalCase(data);
    });

    complier.registerHelper('constantCase', function (data: any) {
        return constantCase(data);
    });

    complier.registerHelper('dotCase', function (data: any) {
        return dotCase(data);
    });

    complier.registerHelper('headerCase', function (data: any) {
        return headerCase(data);
    });

    complier.registerHelper('noCase', function (data: any) {
        return noCase(data);
    });

    complier.registerHelper('paramCase', function (data: any) {
        return paramCase(data);
    });

    complier.registerHelper('dashCase', function (data: any) {
        return paramCase(data);
    });

    complier.registerHelper('kebabCase', function (data: any) {
        return paramCase(data);
    });

    complier.registerHelper('pascalCase', function (data: any) {
        return pascalCase(data);
    });

    complier.registerHelper('properCase', function (data: any) {
        return pascalCase(data);
    });

    complier.registerHelper('pathCase', function (data: any) {
        return pathCase(data);
    });

    complier.registerHelper('sentenceCase', function (data: any) {
        return sentenceCase(data);
    });

    complier.registerHelper('snakeCase', function (data: any) {
        return snakeCase(data);
    });

    complier.registerHelper('plural', function (data: any) {
        return plural(data);
    });

    complier.registerHelper('singular', function (data: any) {
        return singular(data);
    });

    complier.registerHelper('titleCase', function (data: any) {
        return titleCase(data);
    });

    complier.registerHelper('lowerCase', function (data: any) {
        return lowerCase(data);
    });

    complier.registerHelper('upperCase', function (data: any) {
        return upperCase(data);
    });
    
    return complier;
}