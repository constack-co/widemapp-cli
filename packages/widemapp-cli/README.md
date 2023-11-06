Widemapp CLI
=================

Widemapp is an automation tool to generate the most repeated processes. It helps to develop CRUDs, RESTful APIs, end-to-end features, and more... The tool is designed to be limitless and customized to developers.

[[Widemapp GUI]](https://widemapp.constack.co)

# Getting Started

```bash
$ npm install -g @constack/widemapp-cli

USAGE
$ widemapp COMMAND
...
```

<!-- usagestop -->
# Commands
<!-- commands -->
* [`widemapp help [COMMAND]`](#widemapp-help-command)
* [`widemapp login`](#widemapp-login)
* [`widemapp plan:list`](#widemapp-planlist)
* [`widemapp plan:run`](#widemapp-planrun)

## `widemapp login`

Authenticate => widemapp login

```
USAGE
  $ widemapp login

FLAGS
  --password=<value>  (required) pass password
  --username=<value>  (required) pass username or email

DESCRIPTION
  Authenticate => widemapp login

EXAMPLES
  $ widemapp login
```

## `widemapp plan:list`

List plans => widemapp plan:list

```
USAGE
  $ widemapp plan:list

DESCRIPTION
  List plans => widemapp plan:list

EXAMPLES
  $ widemapp plan:list
```

## `widemapp plan:run`

Run a plan => widemapp plan:run

```
USAGE
  $ widemapp plan:run

FLAGS
  --back=<value>   pass back-end path
  --front=<value>  pass front-end path
  --id=<value>     (required) pass plan id

DESCRIPTION
  Run a plan => widemapp plan:run

EXAMPLES
  $ widemapp run:plan
```
<!-- commandsstop -->
* [`widemapp help [COMMAND]`](#widemapp-help-command)
* [`widemapp login`](#widemapp-login)
* [`widemapp plan:list`](#widemapp-planlist)
* [`widemapp plan:run`](#widemapp-planrun)

## `widemapp help [COMMAND]`

Display help for widemapp.

```
USAGE
  $ widemapp help [COMMAND] [-n]

ARGUMENTS
  COMMAND  Command to show help for.

FLAGS
  -n, --nested-commands  Include all nested commands in the output.

DESCRIPTION
  Display help for widemapp.
```