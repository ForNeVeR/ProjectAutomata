ProjectAutomata
===============

Tool for MS Project automation.

## Usage

    ExportMsProjectFile.exe import textfile.org
    ExportMsProjectFile.exe export project.prj

`import` option will create new Project file and create tasks from the input file (see file format below).

`export` option will create text representation of the project file.

## Input file format

Standard org file format. Task definition should be written as follows:

    ** [1 h] Task 1
    ^  ^     ^
    |  |     ` Task name
    |  ` Work in hours (you may omit "h" here)
    ` Indentation (will show task outline in resulting file)
    ...
    Any number of description lines
    ...
    
