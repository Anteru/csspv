C# SPV
======

Overview
--------

This project contains a re-implementation of the ``spirv-dis`` tool written in C#. It consists of three projects:

* ``spirv-gen``, parses the SPIR-V JSON descriptions and produces C# code from there
* ``spirv``, the core library providing the code model
* ``spirv-dis``, the disassembler executable

System requirements
-------------------

This application is built for .NET Core 2.0 and should run on any platform supporting that.

Features & limitations
----------------------

* C# code generation from SPIR-V JSON files
* Handles all known instructions
* Pretty-prints names, types, etc.

A couple of features are missing:

* Not all types are implemented (or rather, the type is not instantiated from the corresponding ``OpType`` instruction)
* Access to operands of instructions is not as nice as it should be.