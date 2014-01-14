ProjectAutomata
===============

Tool for MS Project automation.

## Prerequisites

To use or build the tool, you'll need the Microsoft Project installation. Only
Project 2010 was tested, but other versions should be ok as well. Leave the
issue if you're experiencing difficulties.

## Usage

Run in console:

    ProjectAutomata import textfile.org
    ProjectAutomata export project.prj

`import` option will create new Project file and create tasks from the input
file (see file format below).

`export` option will create text representation of the project file. File
format is the same.

## Text file format

Standard org file format. Task definition should be written as follows:

    ** [1 h] Task 1
    ^  ^     ^
    |  |     ` Task name
    |  ` Work in hours (you may omit "h" here) (this part is optional)
    ` Indentation (will show task outline in resulting file)
    ...
    Any number of description lines
    ...

More formally, task header should match the regular expression
`^(\*+)(?: \[(.*?)\])? (.*)$`.

Outline indentation should be consistent (i.e. no skipped levels).
    
## Build

To build the project, you'll need MS Project 2010 Interop library. It is
distributed as part of the standard Project 2010 installation.