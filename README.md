C# SPIR-V disassembler
======================

Overview
--------

This project contains a re-implementation of the ``spirv-dis`` tool written in C#. It consists of three projects:

* ``CSPVGen``, parses the SPIR-V JSON descriptions and produces C# code from there
* ``SpirV``, the core library providing the code model
* ``CSPVDis``, the disassembler executable

System requirements
-------------------

This application is built for .NET Core 2.0 and should run on any platform supporting that.

It requires C# 7.3 due to `System.Enum` constraints. Please make sure you're using the latest .NET Core SDK to have access to this feature.

Features & limitations
----------------------

* Generates C# code from SPIR-V JSON files
* Pretty-prints names, types, etc.

Build
-----

Build `CSPVDis`, this will in turn build `SpirV` and the project will be ready to use.

If you want to re-generate the generate files, build and run `CSPVGen`.