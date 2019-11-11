# JumpList

## Summary
Simple dotnet tool to create and manage a folder jump list.

## Installation
To install as a dotnet tool, run the following from a command prompt: `dotnet tool install -g CliTools.JumpList`

## Usage
To run the tool, the short name _jumplist_ is used. The saves paths for a given key and will output a path for the specified key. This tool is intended to be used in conjunction with the given a bash script to alias the dotnet command and to use the output path to change into the specified directory.

The command supports the following parameters

| Argument        | Description |
| ------------- |:-------------|
| --list or -l        | lists all saved jump list paths |
| --add or -a        | adds the current folder path to the jumplist keyed by the name specified after the argument |
| --rm or -r        | removes the path from the jump list with the name specified after the argument |
| none specified        | outputs the path for the key specified|

### Example: Adding a path
The following command adds the current path (c:\Users) to the jump list with the name 'users'
`c:\Users> jumplist -a users`

### Example: Listing saved paths
The following command lists the paths saved to the jump list
```
c:\> jumplist -l
 user -> c:\Users
```
### Example: Removing saved path
The following command removes a path from the jump list by name
`c:\> jumplist -r users`

### Example: Retriving a path from the jump list by name
The following command outputs the path for the specified name
```
c:\> jumplist users
 c:\Users
```

## Batch Files
The _.\Scripts_ folder in the repository contains .bat files which service both alias the `jumplist` tool command to a shorter _j_ command, and to use the path provided by the command to change the current working directory to the path specified by the jump command.

Once these scripts are placed in the *PATH*, the may be executed in the following way:

### Adding a path
The following command adds the current path (c:\Users) to the jump list with the name 'users'
`c:\Users> ja users`

### Listing saved paths
The following command lists the paths saved to the jump list
```
c:\> jls
 user -> c:\Users
```
### Removing saved path
The following command removes a path from the jump list by name
`c:\> jrm users`

### Retriving a path from the jump list by name
The following command outputs the path for the specified name
```
c:\> j users
c:\Users> 
```
