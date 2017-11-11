using System;
using System.Collections.Generic;

namespace SpirV
{
    public static class Instructions
    {
        private static Dictionary<int, Instruction> instructions_ = new Dictionary<int, Instruction>{{0, new Instruction("OpNop")}, {1, new Instruction("OpUndef", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), })}, {2, new Instruction("OpSourceContinued", new List<Operand>()
    {new Operand(new LiteralString(), "Continued Source", OperandQuantifier.Default), })}, {3, new Instruction("OpSource", new List<Operand>()
    {new Operand(new SourceLanguage(), null, OperandQuantifier.Default), new Operand(new LiteralInteger(), "Version", OperandQuantifier.Default), new Operand(new IdRef(), "File", OperandQuantifier.Optional), new Operand(new LiteralString(), "Source", OperandQuantifier.Optional), })}, {4, new Instruction("OpSourceExtension", new List<Operand>()
    {new Operand(new LiteralString(), "Extension", OperandQuantifier.Default), })}, {5, new Instruction("OpName", new List<Operand>()
    {new Operand(new IdRef(), "Target", OperandQuantifier.Default), new Operand(new LiteralString(), "Name", OperandQuantifier.Default), })}, {6, new Instruction("OpMemberName", new List<Operand>()
    {new Operand(new IdRef(), "Type", OperandQuantifier.Default), new Operand(new LiteralInteger(), "Member", OperandQuantifier.Default), new Operand(new LiteralString(), "Name", OperandQuantifier.Default), })}, {7, new Instruction("OpString", new List<Operand>()
    {new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new LiteralString(), "String", OperandQuantifier.Default), })}, {8, new Instruction("OpLine", new List<Operand>()
    {new Operand(new IdRef(), "File", OperandQuantifier.Default), new Operand(new LiteralInteger(), "Line", OperandQuantifier.Default), new Operand(new LiteralInteger(), "Column", OperandQuantifier.Default), })}, {10, new Instruction("OpExtension", new List<Operand>()
    {new Operand(new LiteralString(), "Name", OperandQuantifier.Default), })}, {11, new Instruction("OpExtInstImport", new List<Operand>()
    {new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new LiteralString(), "Name", OperandQuantifier.Default), })}, {12, new Instruction("OpExtInst", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Set", OperandQuantifier.Default), new Operand(new LiteralExtInstInteger(), "Instruction", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1, +Operand 2, +...", OperandQuantifier.Varying), })}, {14, new Instruction("OpMemoryModel", new List<Operand>()
    {new Operand(new AddressingModel(), null, OperandQuantifier.Default), new Operand(new MemoryModel(), null, OperandQuantifier.Default), })}, {15, new Instruction("OpEntryPoint", new List<Operand>()
    {new Operand(new ExecutionModel(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Entry Point", OperandQuantifier.Default), new Operand(new LiteralString(), "Name", OperandQuantifier.Default), new Operand(new IdRef(), "Interface", OperandQuantifier.Varying), })}, {16, new Instruction("OpExecutionMode", new List<Operand>()
    {new Operand(new IdRef(), "Entry Point", OperandQuantifier.Default), new Operand(new ExecutionMode(), "Mode", OperandQuantifier.Default), })}, {17, new Instruction("OpCapability", new List<Operand>()
    {new Operand(new Capability(), "Capability", OperandQuantifier.Default), })}, {19, new Instruction("OpTypeVoid", new List<Operand>()
    {new Operand(new IdResult(), null, OperandQuantifier.Default), })}, {20, new Instruction("OpTypeBool", new List<Operand>()
    {new Operand(new IdResult(), null, OperandQuantifier.Default), })}, {21, new Instruction("OpTypeInt", new List<Operand>()
    {new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new LiteralInteger(), "Width", OperandQuantifier.Default), new Operand(new LiteralInteger(), "Signedness", OperandQuantifier.Default), })}, {22, new Instruction("OpTypeFloat", new List<Operand>()
    {new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new LiteralInteger(), "Width", OperandQuantifier.Default), })}, {23, new Instruction("OpTypeVector", new List<Operand>()
    {new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Component Type", OperandQuantifier.Default), new Operand(new LiteralInteger(), "Component Count", OperandQuantifier.Default), })}, {24, new Instruction("OpTypeMatrix", new List<Operand>()
    {new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Column Type", OperandQuantifier.Default), new Operand(new LiteralInteger(), "Column Count", OperandQuantifier.Default), })}, {25, new Instruction("OpTypeImage", new List<Operand>()
    {new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Sampled Type", OperandQuantifier.Default), new Operand(new Dim(), null, OperandQuantifier.Default), new Operand(new LiteralInteger(), "Depth", OperandQuantifier.Default), new Operand(new LiteralInteger(), "Arrayed", OperandQuantifier.Default), new Operand(new LiteralInteger(), "MS", OperandQuantifier.Default), new Operand(new LiteralInteger(), "Sampled", OperandQuantifier.Default), new Operand(new ImageFormat(), null, OperandQuantifier.Default), new Operand(new AccessQualifier(), null, OperandQuantifier.Optional), })}, {26, new Instruction("OpTypeSampler", new List<Operand>()
    {new Operand(new IdResult(), null, OperandQuantifier.Default), })}, {27, new Instruction("OpTypeSampledImage", new List<Operand>()
    {new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Image Type", OperandQuantifier.Default), })}, {28, new Instruction("OpTypeArray", new List<Operand>()
    {new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Element Type", OperandQuantifier.Default), new Operand(new IdRef(), "Length", OperandQuantifier.Default), })}, {29, new Instruction("OpTypeRuntimeArray", new List<Operand>()
    {new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Element Type", OperandQuantifier.Default), })}, {30, new Instruction("OpTypeStruct", new List<Operand>()
    {new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Member 0 type, +member 1 type, +...", OperandQuantifier.Varying), })}, {31, new Instruction("OpTypeOpaque", new List<Operand>()
    {new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new LiteralString(), "The name of the opaque type.", OperandQuantifier.Default), })}, {32, new Instruction("OpTypePointer", new List<Operand>()
    {new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new StorageClass(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Type", OperandQuantifier.Default), })}, {33, new Instruction("OpTypeFunction", new List<Operand>()
    {new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Return Type", OperandQuantifier.Default), new Operand(new IdRef(), "Parameter 0 Type, +Parameter 1 Type, +...", OperandQuantifier.Varying), })}, {34, new Instruction("OpTypeEvent", new List<Operand>()
    {new Operand(new IdResult(), null, OperandQuantifier.Default), })}, {35, new Instruction("OpTypeDeviceEvent", new List<Operand>()
    {new Operand(new IdResult(), null, OperandQuantifier.Default), })}, {36, new Instruction("OpTypeReserveId", new List<Operand>()
    {new Operand(new IdResult(), null, OperandQuantifier.Default), })}, {37, new Instruction("OpTypeQueue", new List<Operand>()
    {new Operand(new IdResult(), null, OperandQuantifier.Default), })}, {38, new Instruction("OpTypePipe", new List<Operand>()
    {new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new AccessQualifier(), "Qualifier", OperandQuantifier.Default), })}, {39, new Instruction("OpTypeForwardPointer", new List<Operand>()
    {new Operand(new IdRef(), "Pointer Type", OperandQuantifier.Default), new Operand(new StorageClass(), null, OperandQuantifier.Default), })}, {41, new Instruction("OpConstantTrue", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), })}, {42, new Instruction("OpConstantFalse", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), })}, {43, new Instruction("OpConstant", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new LiteralContextDependentNumber(), "Value", OperandQuantifier.Default), })}, {44, new Instruction("OpConstantComposite", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Constituents", OperandQuantifier.Varying), })}, {45, new Instruction("OpConstantSampler", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new SamplerAddressingMode(), null, OperandQuantifier.Default), new Operand(new LiteralInteger(), "Param", OperandQuantifier.Default), new Operand(new SamplerFilterMode(), null, OperandQuantifier.Default), })}, {46, new Instruction("OpConstantNull", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), })}, {48, new Instruction("OpSpecConstantTrue", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), })}, {49, new Instruction("OpSpecConstantFalse", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), })}, {50, new Instruction("OpSpecConstant", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new LiteralContextDependentNumber(), "Value", OperandQuantifier.Default), })}, {51, new Instruction("OpSpecConstantComposite", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Constituents", OperandQuantifier.Varying), })}, {52, new Instruction("OpSpecConstantOp", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new LiteralSpecConstantOpInteger(), "Opcode", OperandQuantifier.Default), })}, {54, new Instruction("OpFunction", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new FunctionControl(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Function Type", OperandQuantifier.Default), })}, {55, new Instruction("OpFunctionParameter", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), })}, {56, new Instruction("OpFunctionEnd")}, {57, new Instruction("OpFunctionCall", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Function", OperandQuantifier.Default), new Operand(new IdRef(), "Argument 0, +Argument 1, +...", OperandQuantifier.Varying), })}, {59, new Instruction("OpVariable", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new StorageClass(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Initializer", OperandQuantifier.Optional), })}, {60, new Instruction("OpImageTexelPointer", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Image", OperandQuantifier.Default), new Operand(new IdRef(), "Coordinate", OperandQuantifier.Default), new Operand(new IdRef(), "Sample", OperandQuantifier.Default), })}, {61, new Instruction("OpLoad", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Pointer", OperandQuantifier.Default), new Operand(new MemoryAccess(), null, OperandQuantifier.Optional), })}, {62, new Instruction("OpStore", new List<Operand>()
    {new Operand(new IdRef(), "Pointer", OperandQuantifier.Default), new Operand(new IdRef(), "Object", OperandQuantifier.Default), new Operand(new MemoryAccess(), null, OperandQuantifier.Optional), })}, {63, new Instruction("OpCopyMemory", new List<Operand>()
    {new Operand(new IdRef(), "Target", OperandQuantifier.Default), new Operand(new IdRef(), "Source", OperandQuantifier.Default), new Operand(new MemoryAccess(), null, OperandQuantifier.Optional), })}, {64, new Instruction("OpCopyMemorySized", new List<Operand>()
    {new Operand(new IdRef(), "Target", OperandQuantifier.Default), new Operand(new IdRef(), "Source", OperandQuantifier.Default), new Operand(new IdRef(), "Size", OperandQuantifier.Default), new Operand(new MemoryAccess(), null, OperandQuantifier.Optional), })}, {65, new Instruction("OpAccessChain", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Base", OperandQuantifier.Default), new Operand(new IdRef(), "Indexes", OperandQuantifier.Varying), })}, {66, new Instruction("OpInBoundsAccessChain", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Base", OperandQuantifier.Default), new Operand(new IdRef(), "Indexes", OperandQuantifier.Varying), })}, {67, new Instruction("OpPtrAccessChain", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Base", OperandQuantifier.Default), new Operand(new IdRef(), "Element", OperandQuantifier.Default), new Operand(new IdRef(), "Indexes", OperandQuantifier.Varying), })}, {68, new Instruction("OpArrayLength", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Structure", OperandQuantifier.Default), new Operand(new LiteralInteger(), "Array member", OperandQuantifier.Default), })}, {69, new Instruction("OpGenericPtrMemSemantics", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Pointer", OperandQuantifier.Default), })}, {70, new Instruction("OpInBoundsPtrAccessChain", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Base", OperandQuantifier.Default), new Operand(new IdRef(), "Element", OperandQuantifier.Default), new Operand(new IdRef(), "Indexes", OperandQuantifier.Varying), })}, {71, new Instruction("OpDecorate", new List<Operand>()
    {new Operand(new IdRef(), "Target", OperandQuantifier.Default), new Operand(new Decoration(), null, OperandQuantifier.Default), })}, {72, new Instruction("OpMemberDecorate", new List<Operand>()
    {new Operand(new IdRef(), "Structure Type", OperandQuantifier.Default), new Operand(new LiteralInteger(), "Member", OperandQuantifier.Default), new Operand(new Decoration(), null, OperandQuantifier.Default), })}, {73, new Instruction("OpDecorationGroup", new List<Operand>()
    {new Operand(new IdResult(), null, OperandQuantifier.Default), })}, {74, new Instruction("OpGroupDecorate", new List<Operand>()
    {new Operand(new IdRef(), "Decoration Group", OperandQuantifier.Default), new Operand(new IdRef(), "Targets", OperandQuantifier.Varying), })}, {75, new Instruction("OpGroupMemberDecorate", new List<Operand>()
    {new Operand(new IdRef(), "Decoration Group", OperandQuantifier.Default), new Operand(new PairIdRefLiteralInteger(), "Targets", OperandQuantifier.Varying), })}, {77, new Instruction("OpVectorExtractDynamic", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Vector", OperandQuantifier.Default), new Operand(new IdRef(), "Index", OperandQuantifier.Default), })}, {78, new Instruction("OpVectorInsertDynamic", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Vector", OperandQuantifier.Default), new Operand(new IdRef(), "Component", OperandQuantifier.Default), new Operand(new IdRef(), "Index", OperandQuantifier.Default), })}, {79, new Instruction("OpVectorShuffle", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Vector 1", OperandQuantifier.Default), new Operand(new IdRef(), "Vector 2", OperandQuantifier.Default), new Operand(new LiteralInteger(), "Components", OperandQuantifier.Varying), })}, {80, new Instruction("OpCompositeConstruct", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Constituents", OperandQuantifier.Varying), })}, {81, new Instruction("OpCompositeExtract", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Composite", OperandQuantifier.Default), new Operand(new LiteralInteger(), "Indexes", OperandQuantifier.Varying), })}, {82, new Instruction("OpCompositeInsert", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Object", OperandQuantifier.Default), new Operand(new IdRef(), "Composite", OperandQuantifier.Default), new Operand(new LiteralInteger(), "Indexes", OperandQuantifier.Varying), })}, {83, new Instruction("OpCopyObject", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand", OperandQuantifier.Default), })}, {84, new Instruction("OpTranspose", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Matrix", OperandQuantifier.Default), })}, {86, new Instruction("OpSampledImage", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Image", OperandQuantifier.Default), new Operand(new IdRef(), "Sampler", OperandQuantifier.Default), })}, {87, new Instruction("OpImageSampleImplicitLod", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Sampled Image", OperandQuantifier.Default), new Operand(new IdRef(), "Coordinate", OperandQuantifier.Default), new Operand(new ImageOperands(), null, OperandQuantifier.Optional), })}, {88, new Instruction("OpImageSampleExplicitLod", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Sampled Image", OperandQuantifier.Default), new Operand(new IdRef(), "Coordinate", OperandQuantifier.Default), new Operand(new ImageOperands(), null, OperandQuantifier.Default), })}, {89, new Instruction("OpImageSampleDrefImplicitLod", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Sampled Image", OperandQuantifier.Default), new Operand(new IdRef(), "Coordinate", OperandQuantifier.Default), new Operand(new IdRef(), "D~ref~", OperandQuantifier.Default), new Operand(new ImageOperands(), null, OperandQuantifier.Optional), })}, {90, new Instruction("OpImageSampleDrefExplicitLod", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Sampled Image", OperandQuantifier.Default), new Operand(new IdRef(), "Coordinate", OperandQuantifier.Default), new Operand(new IdRef(), "D~ref~", OperandQuantifier.Default), new Operand(new ImageOperands(), null, OperandQuantifier.Default), })}, {91, new Instruction("OpImageSampleProjImplicitLod", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Sampled Image", OperandQuantifier.Default), new Operand(new IdRef(), "Coordinate", OperandQuantifier.Default), new Operand(new ImageOperands(), null, OperandQuantifier.Optional), })}, {92, new Instruction("OpImageSampleProjExplicitLod", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Sampled Image", OperandQuantifier.Default), new Operand(new IdRef(), "Coordinate", OperandQuantifier.Default), new Operand(new ImageOperands(), null, OperandQuantifier.Default), })}, {93, new Instruction("OpImageSampleProjDrefImplicitLod", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Sampled Image", OperandQuantifier.Default), new Operand(new IdRef(), "Coordinate", OperandQuantifier.Default), new Operand(new IdRef(), "D~ref~", OperandQuantifier.Default), new Operand(new ImageOperands(), null, OperandQuantifier.Optional), })}, {94, new Instruction("OpImageSampleProjDrefExplicitLod", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Sampled Image", OperandQuantifier.Default), new Operand(new IdRef(), "Coordinate", OperandQuantifier.Default), new Operand(new IdRef(), "D~ref~", OperandQuantifier.Default), new Operand(new ImageOperands(), null, OperandQuantifier.Default), })}, {95, new Instruction("OpImageFetch", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Image", OperandQuantifier.Default), new Operand(new IdRef(), "Coordinate", OperandQuantifier.Default), new Operand(new ImageOperands(), null, OperandQuantifier.Optional), })}, {96, new Instruction("OpImageGather", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Sampled Image", OperandQuantifier.Default), new Operand(new IdRef(), "Coordinate", OperandQuantifier.Default), new Operand(new IdRef(), "Component", OperandQuantifier.Default), new Operand(new ImageOperands(), null, OperandQuantifier.Optional), })}, {97, new Instruction("OpImageDrefGather", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Sampled Image", OperandQuantifier.Default), new Operand(new IdRef(), "Coordinate", OperandQuantifier.Default), new Operand(new IdRef(), "D~ref~", OperandQuantifier.Default), new Operand(new ImageOperands(), null, OperandQuantifier.Optional), })}, {98, new Instruction("OpImageRead", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Image", OperandQuantifier.Default), new Operand(new IdRef(), "Coordinate", OperandQuantifier.Default), new Operand(new ImageOperands(), null, OperandQuantifier.Optional), })}, {99, new Instruction("OpImageWrite", new List<Operand>()
    {new Operand(new IdRef(), "Image", OperandQuantifier.Default), new Operand(new IdRef(), "Coordinate", OperandQuantifier.Default), new Operand(new IdRef(), "Texel", OperandQuantifier.Default), new Operand(new ImageOperands(), null, OperandQuantifier.Optional), })}, {100, new Instruction("OpImage", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Sampled Image", OperandQuantifier.Default), })}, {101, new Instruction("OpImageQueryFormat", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Image", OperandQuantifier.Default), })}, {102, new Instruction("OpImageQueryOrder", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Image", OperandQuantifier.Default), })}, {103, new Instruction("OpImageQuerySizeLod", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Image", OperandQuantifier.Default), new Operand(new IdRef(), "Level of Detail", OperandQuantifier.Default), })}, {104, new Instruction("OpImageQuerySize", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Image", OperandQuantifier.Default), })}, {105, new Instruction("OpImageQueryLod", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Sampled Image", OperandQuantifier.Default), new Operand(new IdRef(), "Coordinate", OperandQuantifier.Default), })}, {106, new Instruction("OpImageQueryLevels", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Image", OperandQuantifier.Default), })}, {107, new Instruction("OpImageQuerySamples", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Image", OperandQuantifier.Default), })}, {109, new Instruction("OpConvertFToU", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Float Value", OperandQuantifier.Default), })}, {110, new Instruction("OpConvertFToS", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Float Value", OperandQuantifier.Default), })}, {111, new Instruction("OpConvertSToF", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Signed Value", OperandQuantifier.Default), })}, {112, new Instruction("OpConvertUToF", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Unsigned Value", OperandQuantifier.Default), })}, {113, new Instruction("OpUConvert", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Unsigned Value", OperandQuantifier.Default), })}, {114, new Instruction("OpSConvert", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Signed Value", OperandQuantifier.Default), })}, {115, new Instruction("OpFConvert", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Float Value", OperandQuantifier.Default), })}, {116, new Instruction("OpQuantizeToF16", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Value", OperandQuantifier.Default), })}, {117, new Instruction("OpConvertPtrToU", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Pointer", OperandQuantifier.Default), })}, {118, new Instruction("OpSatConvertSToU", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Signed Value", OperandQuantifier.Default), })}, {119, new Instruction("OpSatConvertUToS", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Unsigned Value", OperandQuantifier.Default), })}, {120, new Instruction("OpConvertUToPtr", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Integer Value", OperandQuantifier.Default), })}, {121, new Instruction("OpPtrCastToGeneric", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Pointer", OperandQuantifier.Default), })}, {122, new Instruction("OpGenericCastToPtr", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Pointer", OperandQuantifier.Default), })}, {123, new Instruction("OpGenericCastToPtrExplicit", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Pointer", OperandQuantifier.Default), new Operand(new StorageClass(), "Storage", OperandQuantifier.Default), })}, {124, new Instruction("OpBitcast", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand", OperandQuantifier.Default), })}, {126, new Instruction("OpSNegate", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand", OperandQuantifier.Default), })}, {127, new Instruction("OpFNegate", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand", OperandQuantifier.Default), })}, {128, new Instruction("OpIAdd", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {129, new Instruction("OpFAdd", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {130, new Instruction("OpISub", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {131, new Instruction("OpFSub", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {132, new Instruction("OpIMul", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {133, new Instruction("OpFMul", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {134, new Instruction("OpUDiv", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {135, new Instruction("OpSDiv", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {136, new Instruction("OpFDiv", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {137, new Instruction("OpUMod", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {138, new Instruction("OpSRem", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {139, new Instruction("OpSMod", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {140, new Instruction("OpFRem", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {141, new Instruction("OpFMod", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {142, new Instruction("OpVectorTimesScalar", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Vector", OperandQuantifier.Default), new Operand(new IdRef(), "Scalar", OperandQuantifier.Default), })}, {143, new Instruction("OpMatrixTimesScalar", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Matrix", OperandQuantifier.Default), new Operand(new IdRef(), "Scalar", OperandQuantifier.Default), })}, {144, new Instruction("OpVectorTimesMatrix", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Vector", OperandQuantifier.Default), new Operand(new IdRef(), "Matrix", OperandQuantifier.Default), })}, {145, new Instruction("OpMatrixTimesVector", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Matrix", OperandQuantifier.Default), new Operand(new IdRef(), "Vector", OperandQuantifier.Default), })}, {146, new Instruction("OpMatrixTimesMatrix", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "LeftMatrix", OperandQuantifier.Default), new Operand(new IdRef(), "RightMatrix", OperandQuantifier.Default), })}, {147, new Instruction("OpOuterProduct", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Vector 1", OperandQuantifier.Default), new Operand(new IdRef(), "Vector 2", OperandQuantifier.Default), })}, {148, new Instruction("OpDot", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Vector 1", OperandQuantifier.Default), new Operand(new IdRef(), "Vector 2", OperandQuantifier.Default), })}, {149, new Instruction("OpIAddCarry", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {150, new Instruction("OpISubBorrow", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {151, new Instruction("OpUMulExtended", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {152, new Instruction("OpSMulExtended", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {154, new Instruction("OpAny", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Vector", OperandQuantifier.Default), })}, {155, new Instruction("OpAll", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Vector", OperandQuantifier.Default), })}, {156, new Instruction("OpIsNan", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "x", OperandQuantifier.Default), })}, {157, new Instruction("OpIsInf", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "x", OperandQuantifier.Default), })}, {158, new Instruction("OpIsFinite", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "x", OperandQuantifier.Default), })}, {159, new Instruction("OpIsNormal", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "x", OperandQuantifier.Default), })}, {160, new Instruction("OpSignBitSet", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "x", OperandQuantifier.Default), })}, {161, new Instruction("OpLessOrGreater", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "x", OperandQuantifier.Default), new Operand(new IdRef(), "y", OperandQuantifier.Default), })}, {162, new Instruction("OpOrdered", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "x", OperandQuantifier.Default), new Operand(new IdRef(), "y", OperandQuantifier.Default), })}, {163, new Instruction("OpUnordered", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "x", OperandQuantifier.Default), new Operand(new IdRef(), "y", OperandQuantifier.Default), })}, {164, new Instruction("OpLogicalEqual", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {165, new Instruction("OpLogicalNotEqual", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {166, new Instruction("OpLogicalOr", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {167, new Instruction("OpLogicalAnd", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {168, new Instruction("OpLogicalNot", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand", OperandQuantifier.Default), })}, {169, new Instruction("OpSelect", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Condition", OperandQuantifier.Default), new Operand(new IdRef(), "Object 1", OperandQuantifier.Default), new Operand(new IdRef(), "Object 2", OperandQuantifier.Default), })}, {170, new Instruction("OpIEqual", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {171, new Instruction("OpINotEqual", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {172, new Instruction("OpUGreaterThan", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {173, new Instruction("OpSGreaterThan", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {174, new Instruction("OpUGreaterThanEqual", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {175, new Instruction("OpSGreaterThanEqual", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {176, new Instruction("OpULessThan", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {177, new Instruction("OpSLessThan", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {178, new Instruction("OpULessThanEqual", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {179, new Instruction("OpSLessThanEqual", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {180, new Instruction("OpFOrdEqual", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {181, new Instruction("OpFUnordEqual", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {182, new Instruction("OpFOrdNotEqual", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {183, new Instruction("OpFUnordNotEqual", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {184, new Instruction("OpFOrdLessThan", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {185, new Instruction("OpFUnordLessThan", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {186, new Instruction("OpFOrdGreaterThan", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {187, new Instruction("OpFUnordGreaterThan", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {188, new Instruction("OpFOrdLessThanEqual", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {189, new Instruction("OpFUnordLessThanEqual", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {190, new Instruction("OpFOrdGreaterThanEqual", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {191, new Instruction("OpFUnordGreaterThanEqual", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {194, new Instruction("OpShiftRightLogical", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Base", OperandQuantifier.Default), new Operand(new IdRef(), "Shift", OperandQuantifier.Default), })}, {195, new Instruction("OpShiftRightArithmetic", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Base", OperandQuantifier.Default), new Operand(new IdRef(), "Shift", OperandQuantifier.Default), })}, {196, new Instruction("OpShiftLeftLogical", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Base", OperandQuantifier.Default), new Operand(new IdRef(), "Shift", OperandQuantifier.Default), })}, {197, new Instruction("OpBitwiseOr", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {198, new Instruction("OpBitwiseXor", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {199, new Instruction("OpBitwiseAnd", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand 1", OperandQuantifier.Default), new Operand(new IdRef(), "Operand 2", OperandQuantifier.Default), })}, {200, new Instruction("OpNot", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Operand", OperandQuantifier.Default), })}, {201, new Instruction("OpBitFieldInsert", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Base", OperandQuantifier.Default), new Operand(new IdRef(), "Insert", OperandQuantifier.Default), new Operand(new IdRef(), "Offset", OperandQuantifier.Default), new Operand(new IdRef(), "Count", OperandQuantifier.Default), })}, {202, new Instruction("OpBitFieldSExtract", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Base", OperandQuantifier.Default), new Operand(new IdRef(), "Offset", OperandQuantifier.Default), new Operand(new IdRef(), "Count", OperandQuantifier.Default), })}, {203, new Instruction("OpBitFieldUExtract", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Base", OperandQuantifier.Default), new Operand(new IdRef(), "Offset", OperandQuantifier.Default), new Operand(new IdRef(), "Count", OperandQuantifier.Default), })}, {204, new Instruction("OpBitReverse", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Base", OperandQuantifier.Default), })}, {205, new Instruction("OpBitCount", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Base", OperandQuantifier.Default), })}, {207, new Instruction("OpDPdx", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "P", OperandQuantifier.Default), })}, {208, new Instruction("OpDPdy", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "P", OperandQuantifier.Default), })}, {209, new Instruction("OpFwidth", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "P", OperandQuantifier.Default), })}, {210, new Instruction("OpDPdxFine", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "P", OperandQuantifier.Default), })}, {211, new Instruction("OpDPdyFine", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "P", OperandQuantifier.Default), })}, {212, new Instruction("OpFwidthFine", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "P", OperandQuantifier.Default), })}, {213, new Instruction("OpDPdxCoarse", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "P", OperandQuantifier.Default), })}, {214, new Instruction("OpDPdyCoarse", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "P", OperandQuantifier.Default), })}, {215, new Instruction("OpFwidthCoarse", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "P", OperandQuantifier.Default), })}, {218, new Instruction("OpEmitVertex")}, {219, new Instruction("OpEndPrimitive")}, {220, new Instruction("OpEmitStreamVertex", new List<Operand>()
    {new Operand(new IdRef(), "Stream", OperandQuantifier.Default), })}, {221, new Instruction("OpEndStreamPrimitive", new List<Operand>()
    {new Operand(new IdRef(), "Stream", OperandQuantifier.Default), })}, {224, new Instruction("OpControlBarrier", new List<Operand>()
    {new Operand(new IdScope(), "Execution", OperandQuantifier.Default), new Operand(new IdScope(), "Memory", OperandQuantifier.Default), new Operand(new IdMemorySemantics(), "Semantics", OperandQuantifier.Default), })}, {225, new Instruction("OpMemoryBarrier", new List<Operand>()
    {new Operand(new IdScope(), "Memory", OperandQuantifier.Default), new Operand(new IdMemorySemantics(), "Semantics", OperandQuantifier.Default), })}, {227, new Instruction("OpAtomicLoad", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Pointer", OperandQuantifier.Default), new Operand(new IdScope(), "Scope", OperandQuantifier.Default), new Operand(new IdMemorySemantics(), "Semantics", OperandQuantifier.Default), })}, {228, new Instruction("OpAtomicStore", new List<Operand>()
    {new Operand(new IdRef(), "Pointer", OperandQuantifier.Default), new Operand(new IdScope(), "Scope", OperandQuantifier.Default), new Operand(new IdMemorySemantics(), "Semantics", OperandQuantifier.Default), new Operand(new IdRef(), "Value", OperandQuantifier.Default), })}, {229, new Instruction("OpAtomicExchange", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Pointer", OperandQuantifier.Default), new Operand(new IdScope(), "Scope", OperandQuantifier.Default), new Operand(new IdMemorySemantics(), "Semantics", OperandQuantifier.Default), new Operand(new IdRef(), "Value", OperandQuantifier.Default), })}, {230, new Instruction("OpAtomicCompareExchange", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Pointer", OperandQuantifier.Default), new Operand(new IdScope(), "Scope", OperandQuantifier.Default), new Operand(new IdMemorySemantics(), "Equal", OperandQuantifier.Default), new Operand(new IdMemorySemantics(), "Unequal", OperandQuantifier.Default), new Operand(new IdRef(), "Value", OperandQuantifier.Default), new Operand(new IdRef(), "Comparator", OperandQuantifier.Default), })}, {231, new Instruction("OpAtomicCompareExchangeWeak", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Pointer", OperandQuantifier.Default), new Operand(new IdScope(), "Scope", OperandQuantifier.Default), new Operand(new IdMemorySemantics(), "Equal", OperandQuantifier.Default), new Operand(new IdMemorySemantics(), "Unequal", OperandQuantifier.Default), new Operand(new IdRef(), "Value", OperandQuantifier.Default), new Operand(new IdRef(), "Comparator", OperandQuantifier.Default), })}, {232, new Instruction("OpAtomicIIncrement", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Pointer", OperandQuantifier.Default), new Operand(new IdScope(), "Scope", OperandQuantifier.Default), new Operand(new IdMemorySemantics(), "Semantics", OperandQuantifier.Default), })}, {233, new Instruction("OpAtomicIDecrement", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Pointer", OperandQuantifier.Default), new Operand(new IdScope(), "Scope", OperandQuantifier.Default), new Operand(new IdMemorySemantics(), "Semantics", OperandQuantifier.Default), })}, {234, new Instruction("OpAtomicIAdd", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Pointer", OperandQuantifier.Default), new Operand(new IdScope(), "Scope", OperandQuantifier.Default), new Operand(new IdMemorySemantics(), "Semantics", OperandQuantifier.Default), new Operand(new IdRef(), "Value", OperandQuantifier.Default), })}, {235, new Instruction("OpAtomicISub", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Pointer", OperandQuantifier.Default), new Operand(new IdScope(), "Scope", OperandQuantifier.Default), new Operand(new IdMemorySemantics(), "Semantics", OperandQuantifier.Default), new Operand(new IdRef(), "Value", OperandQuantifier.Default), })}, {236, new Instruction("OpAtomicSMin", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Pointer", OperandQuantifier.Default), new Operand(new IdScope(), "Scope", OperandQuantifier.Default), new Operand(new IdMemorySemantics(), "Semantics", OperandQuantifier.Default), new Operand(new IdRef(), "Value", OperandQuantifier.Default), })}, {237, new Instruction("OpAtomicUMin", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Pointer", OperandQuantifier.Default), new Operand(new IdScope(), "Scope", OperandQuantifier.Default), new Operand(new IdMemorySemantics(), "Semantics", OperandQuantifier.Default), new Operand(new IdRef(), "Value", OperandQuantifier.Default), })}, {238, new Instruction("OpAtomicSMax", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Pointer", OperandQuantifier.Default), new Operand(new IdScope(), "Scope", OperandQuantifier.Default), new Operand(new IdMemorySemantics(), "Semantics", OperandQuantifier.Default), new Operand(new IdRef(), "Value", OperandQuantifier.Default), })}, {239, new Instruction("OpAtomicUMax", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Pointer", OperandQuantifier.Default), new Operand(new IdScope(), "Scope", OperandQuantifier.Default), new Operand(new IdMemorySemantics(), "Semantics", OperandQuantifier.Default), new Operand(new IdRef(), "Value", OperandQuantifier.Default), })}, {240, new Instruction("OpAtomicAnd", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Pointer", OperandQuantifier.Default), new Operand(new IdScope(), "Scope", OperandQuantifier.Default), new Operand(new IdMemorySemantics(), "Semantics", OperandQuantifier.Default), new Operand(new IdRef(), "Value", OperandQuantifier.Default), })}, {241, new Instruction("OpAtomicOr", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Pointer", OperandQuantifier.Default), new Operand(new IdScope(), "Scope", OperandQuantifier.Default), new Operand(new IdMemorySemantics(), "Semantics", OperandQuantifier.Default), new Operand(new IdRef(), "Value", OperandQuantifier.Default), })}, {242, new Instruction("OpAtomicXor", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Pointer", OperandQuantifier.Default), new Operand(new IdScope(), "Scope", OperandQuantifier.Default), new Operand(new IdMemorySemantics(), "Semantics", OperandQuantifier.Default), new Operand(new IdRef(), "Value", OperandQuantifier.Default), })}, {245, new Instruction("OpPhi", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new PairIdRefIdRef(), "Variable, Parent, ...", OperandQuantifier.Varying), })}, {246, new Instruction("OpLoopMerge", new List<Operand>()
    {new Operand(new IdRef(), "Merge Block", OperandQuantifier.Default), new Operand(new IdRef(), "Continue Target", OperandQuantifier.Default), new Operand(new LoopControl(), null, OperandQuantifier.Default), })}, {247, new Instruction("OpSelectionMerge", new List<Operand>()
    {new Operand(new IdRef(), "Merge Block", OperandQuantifier.Default), new Operand(new SelectionControl(), null, OperandQuantifier.Default), })}, {248, new Instruction("OpLabel", new List<Operand>()
    {new Operand(new IdResult(), null, OperandQuantifier.Default), })}, {249, new Instruction("OpBranch", new List<Operand>()
    {new Operand(new IdRef(), "Target Label", OperandQuantifier.Default), })}, {250, new Instruction("OpBranchConditional", new List<Operand>()
    {new Operand(new IdRef(), "Condition", OperandQuantifier.Default), new Operand(new IdRef(), "True Label", OperandQuantifier.Default), new Operand(new IdRef(), "False Label", OperandQuantifier.Default), new Operand(new LiteralInteger(), "Branch weights", OperandQuantifier.Varying), })}, {251, new Instruction("OpSwitch", new List<Operand>()
    {new Operand(new IdRef(), "Selector", OperandQuantifier.Default), new Operand(new IdRef(), "Default", OperandQuantifier.Default), new Operand(new PairLiteralIntegerIdRef(), "Target", OperandQuantifier.Varying), })}, {252, new Instruction("OpKill")}, {253, new Instruction("OpReturn")}, {254, new Instruction("OpReturnValue", new List<Operand>()
    {new Operand(new IdRef(), "Value", OperandQuantifier.Default), })}, {255, new Instruction("OpUnreachable")}, {256, new Instruction("OpLifetimeStart", new List<Operand>()
    {new Operand(new IdRef(), "Pointer", OperandQuantifier.Default), new Operand(new LiteralInteger(), "Size", OperandQuantifier.Default), })}, {257, new Instruction("OpLifetimeStop", new List<Operand>()
    {new Operand(new IdRef(), "Pointer", OperandQuantifier.Default), new Operand(new LiteralInteger(), "Size", OperandQuantifier.Default), })}, {259, new Instruction("OpGroupAsyncCopy", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdScope(), "Execution", OperandQuantifier.Default), new Operand(new IdRef(), "Destination", OperandQuantifier.Default), new Operand(new IdRef(), "Source", OperandQuantifier.Default), new Operand(new IdRef(), "Num Elements", OperandQuantifier.Default), new Operand(new IdRef(), "Stride", OperandQuantifier.Default), new Operand(new IdRef(), "Event", OperandQuantifier.Default), })}, {260, new Instruction("OpGroupWaitEvents", new List<Operand>()
    {new Operand(new IdScope(), "Execution", OperandQuantifier.Default), new Operand(new IdRef(), "Num Events", OperandQuantifier.Default), new Operand(new IdRef(), "Events List", OperandQuantifier.Default), })}, {261, new Instruction("OpGroupAll", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdScope(), "Execution", OperandQuantifier.Default), new Operand(new IdRef(), "Predicate", OperandQuantifier.Default), })}, {262, new Instruction("OpGroupAny", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdScope(), "Execution", OperandQuantifier.Default), new Operand(new IdRef(), "Predicate", OperandQuantifier.Default), })}, {263, new Instruction("OpGroupBroadcast", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdScope(), "Execution", OperandQuantifier.Default), new Operand(new IdRef(), "Value", OperandQuantifier.Default), new Operand(new IdRef(), "LocalId", OperandQuantifier.Default), })}, {264, new Instruction("OpGroupIAdd", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdScope(), "Execution", OperandQuantifier.Default), new Operand(new GroupOperation(), "Operation", OperandQuantifier.Default), new Operand(new IdRef(), "X", OperandQuantifier.Default), })}, {265, new Instruction("OpGroupFAdd", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdScope(), "Execution", OperandQuantifier.Default), new Operand(new GroupOperation(), "Operation", OperandQuantifier.Default), new Operand(new IdRef(), "X", OperandQuantifier.Default), })}, {266, new Instruction("OpGroupFMin", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdScope(), "Execution", OperandQuantifier.Default), new Operand(new GroupOperation(), "Operation", OperandQuantifier.Default), new Operand(new IdRef(), "X", OperandQuantifier.Default), })}, {267, new Instruction("OpGroupUMin", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdScope(), "Execution", OperandQuantifier.Default), new Operand(new GroupOperation(), "Operation", OperandQuantifier.Default), new Operand(new IdRef(), "X", OperandQuantifier.Default), })}, {268, new Instruction("OpGroupSMin", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdScope(), "Execution", OperandQuantifier.Default), new Operand(new GroupOperation(), "Operation", OperandQuantifier.Default), new Operand(new IdRef(), "X", OperandQuantifier.Default), })}, {269, new Instruction("OpGroupFMax", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdScope(), "Execution", OperandQuantifier.Default), new Operand(new GroupOperation(), "Operation", OperandQuantifier.Default), new Operand(new IdRef(), "X", OperandQuantifier.Default), })}, {270, new Instruction("OpGroupUMax", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdScope(), "Execution", OperandQuantifier.Default), new Operand(new GroupOperation(), "Operation", OperandQuantifier.Default), new Operand(new IdRef(), "X", OperandQuantifier.Default), })}, {271, new Instruction("OpGroupSMax", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdScope(), "Execution", OperandQuantifier.Default), new Operand(new GroupOperation(), "Operation", OperandQuantifier.Default), new Operand(new IdRef(), "X", OperandQuantifier.Default), })}, {274, new Instruction("OpReadPipe", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Pipe", OperandQuantifier.Default), new Operand(new IdRef(), "Pointer", OperandQuantifier.Default), new Operand(new IdRef(), "Packet Size", OperandQuantifier.Default), new Operand(new IdRef(), "Packet Alignment", OperandQuantifier.Default), })}, {275, new Instruction("OpWritePipe", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Pipe", OperandQuantifier.Default), new Operand(new IdRef(), "Pointer", OperandQuantifier.Default), new Operand(new IdRef(), "Packet Size", OperandQuantifier.Default), new Operand(new IdRef(), "Packet Alignment", OperandQuantifier.Default), })}, {276, new Instruction("OpReservedReadPipe", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Pipe", OperandQuantifier.Default), new Operand(new IdRef(), "Reserve Id", OperandQuantifier.Default), new Operand(new IdRef(), "Index", OperandQuantifier.Default), new Operand(new IdRef(), "Pointer", OperandQuantifier.Default), new Operand(new IdRef(), "Packet Size", OperandQuantifier.Default), new Operand(new IdRef(), "Packet Alignment", OperandQuantifier.Default), })}, {277, new Instruction("OpReservedWritePipe", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Pipe", OperandQuantifier.Default), new Operand(new IdRef(), "Reserve Id", OperandQuantifier.Default), new Operand(new IdRef(), "Index", OperandQuantifier.Default), new Operand(new IdRef(), "Pointer", OperandQuantifier.Default), new Operand(new IdRef(), "Packet Size", OperandQuantifier.Default), new Operand(new IdRef(), "Packet Alignment", OperandQuantifier.Default), })}, {278, new Instruction("OpReserveReadPipePackets", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Pipe", OperandQuantifier.Default), new Operand(new IdRef(), "Num Packets", OperandQuantifier.Default), new Operand(new IdRef(), "Packet Size", OperandQuantifier.Default), new Operand(new IdRef(), "Packet Alignment", OperandQuantifier.Default), })}, {279, new Instruction("OpReserveWritePipePackets", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Pipe", OperandQuantifier.Default), new Operand(new IdRef(), "Num Packets", OperandQuantifier.Default), new Operand(new IdRef(), "Packet Size", OperandQuantifier.Default), new Operand(new IdRef(), "Packet Alignment", OperandQuantifier.Default), })}, {280, new Instruction("OpCommitReadPipe", new List<Operand>()
    {new Operand(new IdRef(), "Pipe", OperandQuantifier.Default), new Operand(new IdRef(), "Reserve Id", OperandQuantifier.Default), new Operand(new IdRef(), "Packet Size", OperandQuantifier.Default), new Operand(new IdRef(), "Packet Alignment", OperandQuantifier.Default), })}, {281, new Instruction("OpCommitWritePipe", new List<Operand>()
    {new Operand(new IdRef(), "Pipe", OperandQuantifier.Default), new Operand(new IdRef(), "Reserve Id", OperandQuantifier.Default), new Operand(new IdRef(), "Packet Size", OperandQuantifier.Default), new Operand(new IdRef(), "Packet Alignment", OperandQuantifier.Default), })}, {282, new Instruction("OpIsValidReserveId", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Reserve Id", OperandQuantifier.Default), })}, {283, new Instruction("OpGetNumPipePackets", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Pipe", OperandQuantifier.Default), new Operand(new IdRef(), "Packet Size", OperandQuantifier.Default), new Operand(new IdRef(), "Packet Alignment", OperandQuantifier.Default), })}, {284, new Instruction("OpGetMaxPipePackets", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Pipe", OperandQuantifier.Default), new Operand(new IdRef(), "Packet Size", OperandQuantifier.Default), new Operand(new IdRef(), "Packet Alignment", OperandQuantifier.Default), })}, {285, new Instruction("OpGroupReserveReadPipePackets", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdScope(), "Execution", OperandQuantifier.Default), new Operand(new IdRef(), "Pipe", OperandQuantifier.Default), new Operand(new IdRef(), "Num Packets", OperandQuantifier.Default), new Operand(new IdRef(), "Packet Size", OperandQuantifier.Default), new Operand(new IdRef(), "Packet Alignment", OperandQuantifier.Default), })}, {286, new Instruction("OpGroupReserveWritePipePackets", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdScope(), "Execution", OperandQuantifier.Default), new Operand(new IdRef(), "Pipe", OperandQuantifier.Default), new Operand(new IdRef(), "Num Packets", OperandQuantifier.Default), new Operand(new IdRef(), "Packet Size", OperandQuantifier.Default), new Operand(new IdRef(), "Packet Alignment", OperandQuantifier.Default), })}, {287, new Instruction("OpGroupCommitReadPipe", new List<Operand>()
    {new Operand(new IdScope(), "Execution", OperandQuantifier.Default), new Operand(new IdRef(), "Pipe", OperandQuantifier.Default), new Operand(new IdRef(), "Reserve Id", OperandQuantifier.Default), new Operand(new IdRef(), "Packet Size", OperandQuantifier.Default), new Operand(new IdRef(), "Packet Alignment", OperandQuantifier.Default), })}, {288, new Instruction("OpGroupCommitWritePipe", new List<Operand>()
    {new Operand(new IdScope(), "Execution", OperandQuantifier.Default), new Operand(new IdRef(), "Pipe", OperandQuantifier.Default), new Operand(new IdRef(), "Reserve Id", OperandQuantifier.Default), new Operand(new IdRef(), "Packet Size", OperandQuantifier.Default), new Operand(new IdRef(), "Packet Alignment", OperandQuantifier.Default), })}, {291, new Instruction("OpEnqueueMarker", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Queue", OperandQuantifier.Default), new Operand(new IdRef(), "Num Events", OperandQuantifier.Default), new Operand(new IdRef(), "Wait Events", OperandQuantifier.Default), new Operand(new IdRef(), "Ret Event", OperandQuantifier.Default), })}, {292, new Instruction("OpEnqueueKernel", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Queue", OperandQuantifier.Default), new Operand(new IdRef(), "Flags", OperandQuantifier.Default), new Operand(new IdRef(), "ND Range", OperandQuantifier.Default), new Operand(new IdRef(), "Num Events", OperandQuantifier.Default), new Operand(new IdRef(), "Wait Events", OperandQuantifier.Default), new Operand(new IdRef(), "Ret Event", OperandQuantifier.Default), new Operand(new IdRef(), "Invoke", OperandQuantifier.Default), new Operand(new IdRef(), "Param", OperandQuantifier.Default), new Operand(new IdRef(), "Param Size", OperandQuantifier.Default), new Operand(new IdRef(), "Param Align", OperandQuantifier.Default), new Operand(new IdRef(), "Local Size", OperandQuantifier.Varying), })}, {293, new Instruction("OpGetKernelNDrangeSubGroupCount", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "ND Range", OperandQuantifier.Default), new Operand(new IdRef(), "Invoke", OperandQuantifier.Default), new Operand(new IdRef(), "Param", OperandQuantifier.Default), new Operand(new IdRef(), "Param Size", OperandQuantifier.Default), new Operand(new IdRef(), "Param Align", OperandQuantifier.Default), })}, {294, new Instruction("OpGetKernelNDrangeMaxSubGroupSize", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "ND Range", OperandQuantifier.Default), new Operand(new IdRef(), "Invoke", OperandQuantifier.Default), new Operand(new IdRef(), "Param", OperandQuantifier.Default), new Operand(new IdRef(), "Param Size", OperandQuantifier.Default), new Operand(new IdRef(), "Param Align", OperandQuantifier.Default), })}, {295, new Instruction("OpGetKernelWorkGroupSize", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Invoke", OperandQuantifier.Default), new Operand(new IdRef(), "Param", OperandQuantifier.Default), new Operand(new IdRef(), "Param Size", OperandQuantifier.Default), new Operand(new IdRef(), "Param Align", OperandQuantifier.Default), })}, {296, new Instruction("OpGetKernelPreferredWorkGroupSizeMultiple", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Invoke", OperandQuantifier.Default), new Operand(new IdRef(), "Param", OperandQuantifier.Default), new Operand(new IdRef(), "Param Size", OperandQuantifier.Default), new Operand(new IdRef(), "Param Align", OperandQuantifier.Default), })}, {297, new Instruction("OpRetainEvent", new List<Operand>()
    {new Operand(new IdRef(), "Event", OperandQuantifier.Default), })}, {298, new Instruction("OpReleaseEvent", new List<Operand>()
    {new Operand(new IdRef(), "Event", OperandQuantifier.Default), })}, {299, new Instruction("OpCreateUserEvent", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), })}, {300, new Instruction("OpIsValidEvent", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Event", OperandQuantifier.Default), })}, {301, new Instruction("OpSetUserEventStatus", new List<Operand>()
    {new Operand(new IdRef(), "Event", OperandQuantifier.Default), new Operand(new IdRef(), "Status", OperandQuantifier.Default), })}, {302, new Instruction("OpCaptureEventProfilingInfo", new List<Operand>()
    {new Operand(new IdRef(), "Event", OperandQuantifier.Default), new Operand(new IdRef(), "Profiling Info", OperandQuantifier.Default), new Operand(new IdRef(), "Value", OperandQuantifier.Default), })}, {303, new Instruction("OpGetDefaultQueue", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), })}, {304, new Instruction("OpBuildNDRange", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "GlobalWorkSize", OperandQuantifier.Default), new Operand(new IdRef(), "LocalWorkSize", OperandQuantifier.Default), new Operand(new IdRef(), "GlobalWorkOffset", OperandQuantifier.Default), })}, {305, new Instruction("OpImageSparseSampleImplicitLod", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Sampled Image", OperandQuantifier.Default), new Operand(new IdRef(), "Coordinate", OperandQuantifier.Default), new Operand(new ImageOperands(), null, OperandQuantifier.Optional), })}, {306, new Instruction("OpImageSparseSampleExplicitLod", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Sampled Image", OperandQuantifier.Default), new Operand(new IdRef(), "Coordinate", OperandQuantifier.Default), new Operand(new ImageOperands(), null, OperandQuantifier.Default), })}, {307, new Instruction("OpImageSparseSampleDrefImplicitLod", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Sampled Image", OperandQuantifier.Default), new Operand(new IdRef(), "Coordinate", OperandQuantifier.Default), new Operand(new IdRef(), "D~ref~", OperandQuantifier.Default), new Operand(new ImageOperands(), null, OperandQuantifier.Optional), })}, {308, new Instruction("OpImageSparseSampleDrefExplicitLod", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Sampled Image", OperandQuantifier.Default), new Operand(new IdRef(), "Coordinate", OperandQuantifier.Default), new Operand(new IdRef(), "D~ref~", OperandQuantifier.Default), new Operand(new ImageOperands(), null, OperandQuantifier.Default), })}, {309, new Instruction("OpImageSparseSampleProjImplicitLod", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Sampled Image", OperandQuantifier.Default), new Operand(new IdRef(), "Coordinate", OperandQuantifier.Default), new Operand(new ImageOperands(), null, OperandQuantifier.Optional), })}, {310, new Instruction("OpImageSparseSampleProjExplicitLod", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Sampled Image", OperandQuantifier.Default), new Operand(new IdRef(), "Coordinate", OperandQuantifier.Default), new Operand(new ImageOperands(), null, OperandQuantifier.Default), })}, {311, new Instruction("OpImageSparseSampleProjDrefImplicitLod", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Sampled Image", OperandQuantifier.Default), new Operand(new IdRef(), "Coordinate", OperandQuantifier.Default), new Operand(new IdRef(), "D~ref~", OperandQuantifier.Default), new Operand(new ImageOperands(), null, OperandQuantifier.Optional), })}, {312, new Instruction("OpImageSparseSampleProjDrefExplicitLod", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Sampled Image", OperandQuantifier.Default), new Operand(new IdRef(), "Coordinate", OperandQuantifier.Default), new Operand(new IdRef(), "D~ref~", OperandQuantifier.Default), new Operand(new ImageOperands(), null, OperandQuantifier.Default), })}, {313, new Instruction("OpImageSparseFetch", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Image", OperandQuantifier.Default), new Operand(new IdRef(), "Coordinate", OperandQuantifier.Default), new Operand(new ImageOperands(), null, OperandQuantifier.Optional), })}, {314, new Instruction("OpImageSparseGather", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Sampled Image", OperandQuantifier.Default), new Operand(new IdRef(), "Coordinate", OperandQuantifier.Default), new Operand(new IdRef(), "Component", OperandQuantifier.Default), new Operand(new ImageOperands(), null, OperandQuantifier.Optional), })}, {315, new Instruction("OpImageSparseDrefGather", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Sampled Image", OperandQuantifier.Default), new Operand(new IdRef(), "Coordinate", OperandQuantifier.Default), new Operand(new IdRef(), "D~ref~", OperandQuantifier.Default), new Operand(new ImageOperands(), null, OperandQuantifier.Optional), })}, {316, new Instruction("OpImageSparseTexelsResident", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Resident Code", OperandQuantifier.Default), })}, {317, new Instruction("OpNoLine")}, {318, new Instruction("OpAtomicFlagTestAndSet", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Pointer", OperandQuantifier.Default), new Operand(new IdScope(), "Scope", OperandQuantifier.Default), new Operand(new IdMemorySemantics(), "Semantics", OperandQuantifier.Default), })}, {319, new Instruction("OpAtomicFlagClear", new List<Operand>()
    {new Operand(new IdRef(), "Pointer", OperandQuantifier.Default), new Operand(new IdScope(), "Scope", OperandQuantifier.Default), new Operand(new IdMemorySemantics(), "Semantics", OperandQuantifier.Default), })}, {320, new Instruction("OpImageSparseRead", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Image", OperandQuantifier.Default), new Operand(new IdRef(), "Coordinate", OperandQuantifier.Default), new Operand(new ImageOperands(), null, OperandQuantifier.Optional), })}, {321, new Instruction("OpSizeOf", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Pointer", OperandQuantifier.Default), })}, {322, new Instruction("OpTypePipeStorage", new List<Operand>()
    {new Operand(new IdResult(), null, OperandQuantifier.Default), })}, {323, new Instruction("OpConstantPipeStorage", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new LiteralInteger(), "Packet Size", OperandQuantifier.Default), new Operand(new LiteralInteger(), "Packet Alignment", OperandQuantifier.Default), new Operand(new LiteralInteger(), "Capacity", OperandQuantifier.Default), })}, {324, new Instruction("OpCreatePipeFromPipeStorage", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Pipe Storage", OperandQuantifier.Default), })}, {325, new Instruction("OpGetKernelLocalSizeForSubgroupCount", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Subgroup Count", OperandQuantifier.Default), new Operand(new IdRef(), "Invoke", OperandQuantifier.Default), new Operand(new IdRef(), "Param", OperandQuantifier.Default), new Operand(new IdRef(), "Param Size", OperandQuantifier.Default), new Operand(new IdRef(), "Param Align", OperandQuantifier.Default), })}, {326, new Instruction("OpGetKernelMaxNumSubgroups", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Invoke", OperandQuantifier.Default), new Operand(new IdRef(), "Param", OperandQuantifier.Default), new Operand(new IdRef(), "Param Size", OperandQuantifier.Default), new Operand(new IdRef(), "Param Align", OperandQuantifier.Default), })}, {327, new Instruction("OpTypeNamedBarrier", new List<Operand>()
    {new Operand(new IdResult(), null, OperandQuantifier.Default), })}, {328, new Instruction("OpNamedBarrierInitialize", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Subgroup Count", OperandQuantifier.Default), })}, {329, new Instruction("OpMemoryNamedBarrier", new List<Operand>()
    {new Operand(new IdRef(), "Named Barrier", OperandQuantifier.Default), new Operand(new IdScope(), "Memory", OperandQuantifier.Default), new Operand(new IdMemorySemantics(), "Semantics", OperandQuantifier.Default), })}, {330, new Instruction("OpModuleProcessed", new List<Operand>()
    {new Operand(new LiteralString(), "Process", OperandQuantifier.Default), })}, {331, new Instruction("OpExecutionModeId", new List<Operand>()
    {new Operand(new IdRef(), "Entry Point", OperandQuantifier.Default), new Operand(new ExecutionMode(), "Mode", OperandQuantifier.Default), })}, {332, new Instruction("OpDecorateId", new List<Operand>()
    {new Operand(new IdRef(), "Target", OperandQuantifier.Default), new Operand(new Decoration(), null, OperandQuantifier.Default), })}, {4421, new Instruction("OpSubgroupBallotKHR", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Predicate", OperandQuantifier.Default), })}, {4422, new Instruction("OpSubgroupFirstInvocationKHR", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Value", OperandQuantifier.Default), })}, {4428, new Instruction("OpSubgroupAllKHR", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Predicate", OperandQuantifier.Default), })}, {4429, new Instruction("OpSubgroupAnyKHR", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Predicate", OperandQuantifier.Default), })}, {4430, new Instruction("OpSubgroupAllEqualKHR", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Predicate", OperandQuantifier.Default), })}, {4432, new Instruction("OpSubgroupReadInvocationKHR", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Value", OperandQuantifier.Default), new Operand(new IdRef(), "Index", OperandQuantifier.Default), })}, {5000, new Instruction("OpGroupIAddNonUniformAMD", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdScope(), "Execution", OperandQuantifier.Default), new Operand(new GroupOperation(), "Operation", OperandQuantifier.Default), new Operand(new IdRef(), "X", OperandQuantifier.Default), })}, {5001, new Instruction("OpGroupFAddNonUniformAMD", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdScope(), "Execution", OperandQuantifier.Default), new Operand(new GroupOperation(), "Operation", OperandQuantifier.Default), new Operand(new IdRef(), "X", OperandQuantifier.Default), })}, {5002, new Instruction("OpGroupFMinNonUniformAMD", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdScope(), "Execution", OperandQuantifier.Default), new Operand(new GroupOperation(), "Operation", OperandQuantifier.Default), new Operand(new IdRef(), "X", OperandQuantifier.Default), })}, {5003, new Instruction("OpGroupUMinNonUniformAMD", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdScope(), "Execution", OperandQuantifier.Default), new Operand(new GroupOperation(), "Operation", OperandQuantifier.Default), new Operand(new IdRef(), "X", OperandQuantifier.Default), })}, {5004, new Instruction("OpGroupSMinNonUniformAMD", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdScope(), "Execution", OperandQuantifier.Default), new Operand(new GroupOperation(), "Operation", OperandQuantifier.Default), new Operand(new IdRef(), "X", OperandQuantifier.Default), })}, {5005, new Instruction("OpGroupFMaxNonUniformAMD", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdScope(), "Execution", OperandQuantifier.Default), new Operand(new GroupOperation(), "Operation", OperandQuantifier.Default), new Operand(new IdRef(), "X", OperandQuantifier.Default), })}, {5006, new Instruction("OpGroupUMaxNonUniformAMD", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdScope(), "Execution", OperandQuantifier.Default), new Operand(new GroupOperation(), "Operation", OperandQuantifier.Default), new Operand(new IdRef(), "X", OperandQuantifier.Default), })}, {5007, new Instruction("OpGroupSMaxNonUniformAMD", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdScope(), "Execution", OperandQuantifier.Default), new Operand(new GroupOperation(), "Operation", OperandQuantifier.Default), new Operand(new IdRef(), "X", OperandQuantifier.Default), })}, {5011, new Instruction("OpFragmentMaskFetchAMD", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Image", OperandQuantifier.Default), new Operand(new IdRef(), "Coordinate", OperandQuantifier.Default), })}, {5012, new Instruction("OpFragmentFetchAMD", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Image", OperandQuantifier.Default), new Operand(new IdRef(), "Coordinate", OperandQuantifier.Default), new Operand(new IdRef(), "Fragment Index", OperandQuantifier.Default), })}, {5571, new Instruction("OpSubgroupShuffleINTEL", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Data", OperandQuantifier.Default), new Operand(new IdRef(), "InvocationId", OperandQuantifier.Default), })}, {5572, new Instruction("OpSubgroupShuffleDownINTEL", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Current", OperandQuantifier.Default), new Operand(new IdRef(), "Next", OperandQuantifier.Default), new Operand(new IdRef(), "Delta", OperandQuantifier.Default), })}, {5573, new Instruction("OpSubgroupShuffleUpINTEL", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Previous", OperandQuantifier.Default), new Operand(new IdRef(), "Current", OperandQuantifier.Default), new Operand(new IdRef(), "Delta", OperandQuantifier.Default), })}, {5574, new Instruction("OpSubgroupShuffleXorINTEL", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Data", OperandQuantifier.Default), new Operand(new IdRef(), "Value", OperandQuantifier.Default), })}, {5575, new Instruction("OpSubgroupBlockReadINTEL", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Ptr", OperandQuantifier.Default), })}, {5576, new Instruction("OpSubgroupBlockWriteINTEL", new List<Operand>()
    {new Operand(new IdRef(), "Ptr", OperandQuantifier.Default), new Operand(new IdRef(), "Data", OperandQuantifier.Default), })}, {5577, new Instruction("OpSubgroupImageBlockReadINTEL", new List<Operand>()
    {new Operand(new IdResultType(), null, OperandQuantifier.Default), new Operand(new IdResult(), null, OperandQuantifier.Default), new Operand(new IdRef(), "Image", OperandQuantifier.Default), new Operand(new IdRef(), "Coordinate", OperandQuantifier.Default), })}, {5578, new Instruction("OpSubgroupImageBlockWriteINTEL", new List<Operand>()
    {new Operand(new IdRef(), "Image", OperandQuantifier.Default), new Operand(new IdRef(), "Coordinate", OperandQuantifier.Default), new Operand(new IdRef(), "Data", OperandQuantifier.Default), })}, };
        public static IReadOnlyDictionary<int, Instruction> OpcodeToInstruction
        {
            get
            {
                return instructions_;
            }
        }
    }
    public class ImageOperands : EnumOperandKind
    {
        [Flags]
        public enum Values
        {
            None = 0,
            Bias = 1,
            Lod = 2,
            Grad = 4,
            ConstOffset = 8,
            Offset = 16,
            ConstOffsets = 32,
            Sample = 64,
            MinLod = 128,
        }

        public class BiasParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new IdRef(), };
        }

        public class LodParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new IdRef(), };
        }

        public class GradParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new IdRef(), new IdRef(), };
        }

        public class ConstOffsetParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new IdRef(), };
        }

        public class OffsetParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new IdRef(), };
        }

        public class ConstOffsetsParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new IdRef(), };
        }

        public class SampleParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new IdRef(), };
        }

        public class MinLodParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new IdRef(), };
        }

        public override Parameter CreateParameter(uint value)
        {
            switch (value)
            {
                case (uint)Values.Bias:
                    return new BiasParameter();
                case (uint)Values.Lod:
                    return new LodParameter();
                case (uint)Values.Grad:
                    return new GradParameter();
                case (uint)Values.ConstOffset:
                    return new ConstOffsetParameter();
                case (uint)Values.Offset:
                    return new OffsetParameter();
                case (uint)Values.ConstOffsets:
                    return new ConstOffsetsParameter();
                case (uint)Values.Sample:
                    return new SampleParameter();
                case (uint)Values.MinLod:
                    return new MinLodParameter();
            }

            return null;
        }
    }
    public class FPFastMathMode : EnumOperandKind
    {
        [Flags]
        public enum Values
        {
            None = 0,
            NotNaN = 1,
            NotInf = 2,
            NSZ = 4,
            AllowRecip = 8,
            Fast = 16,
        }
    }
    public class SelectionControl : EnumOperandKind
    {
        [Flags]
        public enum Values
        {
            None = 0,
            Flatten = 1,
            DontFlatten = 2,
        }
    }
    public class LoopControl : EnumOperandKind
    {
        [Flags]
        public enum Values
        {
            None = 0,
            Unroll = 1,
            DontUnroll = 2,
            DependencyInfinite = 4,
            DependencyLength = 8,
        }

        public class DependencyLengthParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new LiteralInteger(), };
        }

        public override Parameter CreateParameter(uint value)
        {
            switch (value)
            {
                case (uint)Values.DependencyLength:
                    return new DependencyLengthParameter();
            }

            return null;
        }
    }
    public class FunctionControl : EnumOperandKind
    {
        [Flags]
        public enum Values
        {
            None = 0,
            Inline = 1,
            DontInline = 2,
            Pure = 4,
            Const = 8,
        }
    }
    public class MemorySemantics : EnumOperandKind
    {
        [Flags]
        public enum Values
        {
            Relaxed = 0,
            None = 0,
            Acquire = 2,
            Release = 4,
            AcquireRelease = 8,
            SequentiallyConsistent = 16,
            UniformMemory = 64,
            SubgroupMemory = 128,
            WorkgroupMemory = 256,
            CrossWorkgroupMemory = 512,
            AtomicCounterMemory = 1024,
            ImageMemory = 2048,
        }
    }
    public class MemoryAccess : EnumOperandKind
    {
        [Flags]
        public enum Values
        {
            None = 0,
            Volatile = 1,
            Aligned = 2,
            Nontemporal = 4,
        }

        public class AlignedParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new LiteralInteger(), };
        }

        public override Parameter CreateParameter(uint value)
        {
            switch (value)
            {
                case (uint)Values.Aligned:
                    return new AlignedParameter();
            }

            return null;
        }
    }
    public class KernelProfilingInfo : EnumOperandKind
    {
        [Flags]
        public enum Values
        {
            None = 0,
            CmdExecTime = 1,
        }
    }
    public class SourceLanguage : EnumOperandKind
    {
        public enum Values
        {
            Unknown = 0,
            ESSL = 1,
            GLSL = 2,
            OpenCL_C = 3,
            OpenCL_CPP = 4,
            HLSL = 5,
        }
    }
    public class ExecutionModel : EnumOperandKind
    {
        public enum Values
        {
            Vertex = 0,
            TessellationControl = 1,
            TessellationEvaluation = 2,
            Geometry = 3,
            Fragment = 4,
            GLCompute = 5,
            Kernel = 6,
        }
    }
    public class AddressingModel : EnumOperandKind
    {
        public enum Values
        {
            Logical = 0,
            Physical32 = 1,
            Physical64 = 2,
        }
    }
    public class MemoryModel : EnumOperandKind
    {
        public enum Values
        {
            Simple = 0,
            GLSL450 = 1,
            OpenCL = 2,
        }
    }
    public class ExecutionMode : EnumOperandKind
    {
        public enum Values
        {
            Invocations = 0,
            SpacingEqual = 1,
            SpacingFractionalEven = 2,
            SpacingFractionalOdd = 3,
            VertexOrderCw = 4,
            VertexOrderCcw = 5,
            PixelCenterInteger = 6,
            OriginUpperLeft = 7,
            OriginLowerLeft = 8,
            EarlyFragmentTests = 9,
            PointMode = 10,
            Xfb = 11,
            DepthReplacing = 12,
            DepthGreater = 14,
            DepthLess = 15,
            DepthUnchanged = 16,
            LocalSize = 17,
            LocalSizeHint = 18,
            InputPoints = 19,
            InputLines = 20,
            InputLinesAdjacency = 21,
            Triangles = 22,
            InputTrianglesAdjacency = 23,
            Quads = 24,
            Isolines = 25,
            OutputVertices = 26,
            OutputPoints = 27,
            OutputLineStrip = 28,
            OutputTriangleStrip = 29,
            VecTypeHint = 30,
            ContractionOff = 31,
            Initializer = 33,
            Finalizer = 34,
            SubgroupSize = 35,
            SubgroupsPerWorkgroup = 36,
            SubgroupsPerWorkgroupId = 37,
            LocalSizeId = 38,
            LocalSizeHintId = 39,
            PostDepthCoverage = 4446,
            StencilRefReplacingEXT = 5027,
        }

        public class InvocationsParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new LiteralInteger(), };
        }

        public class LocalSizeParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new LiteralInteger(), new LiteralInteger(), new LiteralInteger(), };
        }

        public class LocalSizeHintParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new LiteralInteger(), new LiteralInteger(), new LiteralInteger(), };
        }

        public class OutputVerticesParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new LiteralInteger(), };
        }

        public class VecTypeHintParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new LiteralInteger(), };
        }

        public class SubgroupSizeParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new LiteralInteger(), };
        }

        public class SubgroupsPerWorkgroupParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new LiteralInteger(), };
        }

        public class SubgroupsPerWorkgroupIdParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new IdRef(), };
        }

        public class LocalSizeIdParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new IdRef(), new IdRef(), new IdRef(), };
        }

        public class LocalSizeHintIdParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new IdRef(), };
        }

        public override Parameter CreateParameter(uint value)
        {
            switch (value)
            {
                case (uint)Values.Invocations:
                    return new InvocationsParameter();
                case (uint)Values.LocalSize:
                    return new LocalSizeParameter();
                case (uint)Values.LocalSizeHint:
                    return new LocalSizeHintParameter();
                case (uint)Values.OutputVertices:
                    return new OutputVerticesParameter();
                case (uint)Values.VecTypeHint:
                    return new VecTypeHintParameter();
                case (uint)Values.SubgroupSize:
                    return new SubgroupSizeParameter();
                case (uint)Values.SubgroupsPerWorkgroup:
                    return new SubgroupsPerWorkgroupParameter();
                case (uint)Values.SubgroupsPerWorkgroupId:
                    return new SubgroupsPerWorkgroupIdParameter();
                case (uint)Values.LocalSizeId:
                    return new LocalSizeIdParameter();
                case (uint)Values.LocalSizeHintId:
                    return new LocalSizeHintIdParameter();
            }

            return null;
        }
    }
    public class StorageClass : EnumOperandKind
    {
        public enum Values
        {
            UniformConstant = 0,
            Input = 1,
            Uniform = 2,
            Output = 3,
            Workgroup = 4,
            CrossWorkgroup = 5,
            Private = 6,
            Function = 7,
            Generic = 8,
            PushConstant = 9,
            AtomicCounter = 10,
            Image = 11,
            StorageBuffer = 12,
        }
    }
    public class Dim : EnumOperandKind
    {
        public enum Values
        {
            Dim1D = 0,
            Dim2D = 1,
            Dim3D = 2,
            Cube = 3,
            Rect = 4,
            Buffer = 5,
            SubpassData = 6,
        }
    }
    public class SamplerAddressingMode : EnumOperandKind
    {
        public enum Values
        {
            None = 0,
            ClampToEdge = 1,
            Clamp = 2,
            Repeat = 3,
            RepeatMirrored = 4,
        }
    }
    public class SamplerFilterMode : EnumOperandKind
    {
        public enum Values
        {
            Nearest = 0,
            Linear = 1,
        }
    }
    public class ImageFormat : EnumOperandKind
    {
        public enum Values
        {
            Unknown = 0,
            Rgba32f = 1,
            Rgba16f = 2,
            R32f = 3,
            Rgba8 = 4,
            Rgba8Snorm = 5,
            Rg32f = 6,
            Rg16f = 7,
            R11fG11fB10f = 8,
            R16f = 9,
            Rgba16 = 10,
            Rgb10A2 = 11,
            Rg16 = 12,
            Rg8 = 13,
            R16 = 14,
            R8 = 15,
            Rgba16Snorm = 16,
            Rg16Snorm = 17,
            Rg8Snorm = 18,
            R16Snorm = 19,
            R8Snorm = 20,
            Rgba32i = 21,
            Rgba16i = 22,
            Rgba8i = 23,
            R32i = 24,
            Rg32i = 25,
            Rg16i = 26,
            Rg8i = 27,
            R16i = 28,
            R8i = 29,
            Rgba32ui = 30,
            Rgba16ui = 31,
            Rgba8ui = 32,
            R32ui = 33,
            Rgb10a2ui = 34,
            Rg32ui = 35,
            Rg16ui = 36,
            Rg8ui = 37,
            R16ui = 38,
            R8ui = 39,
        }
    }
    public class ImageChannelOrder : EnumOperandKind
    {
        public enum Values
        {
            R = 0,
            A = 1,
            RG = 2,
            RA = 3,
            RGB = 4,
            RGBA = 5,
            BGRA = 6,
            ARGB = 7,
            Intensity = 8,
            Luminance = 9,
            Rx = 10,
            RGx = 11,
            RGBx = 12,
            Depth = 13,
            DepthStencil = 14,
            sRGB = 15,
            sRGBx = 16,
            sRGBA = 17,
            sBGRA = 18,
            ABGR = 19,
        }
    }
    public class ImageChannelDataType : EnumOperandKind
    {
        public enum Values
        {
            SnormInt8 = 0,
            SnormInt16 = 1,
            UnormInt8 = 2,
            UnormInt16 = 3,
            UnormShort565 = 4,
            UnormShort555 = 5,
            UnormInt101010 = 6,
            SignedInt8 = 7,
            SignedInt16 = 8,
            SignedInt32 = 9,
            UnsignedInt8 = 10,
            UnsignedInt16 = 11,
            UnsignedInt32 = 12,
            HalfFloat = 13,
            Float = 14,
            UnormInt24 = 15,
            UnormInt101010_2 = 16,
        }
    }
    public class FPRoundingMode : EnumOperandKind
    {
        public enum Values
        {
            RTE = 0,
            RTZ = 1,
            RTP = 2,
            RTN = 3,
        }
    }
    public class LinkageType : EnumOperandKind
    {
        public enum Values
        {
            Export = 0,
            Import = 1,
        }
    }
    public class AccessQualifier : EnumOperandKind
    {
        public enum Values
        {
            ReadOnly = 0,
            WriteOnly = 1,
            ReadWrite = 2,
        }
    }
    public class FunctionParameterAttribute : EnumOperandKind
    {
        public enum Values
        {
            Zext = 0,
            Sext = 1,
            ByVal = 2,
            Sret = 3,
            NoAlias = 4,
            NoCapture = 5,
            NoWrite = 6,
            NoReadWrite = 7,
        }
    }
    public class Decoration : EnumOperandKind
    {
        public enum Values
        {
            RelaxedPrecision = 0,
            SpecId = 1,
            Block = 2,
            BufferBlock = 3,
            RowMajor = 4,
            ColMajor = 5,
            ArrayStride = 6,
            MatrixStride = 7,
            GLSLShared = 8,
            GLSLPacked = 9,
            CPacked = 10,
            BuiltIn = 11,
            NoPerspective = 13,
            Flat = 14,
            Patch = 15,
            Centroid = 16,
            Sample = 17,
            Invariant = 18,
            Restrict = 19,
            Aliased = 20,
            Volatile = 21,
            Constant = 22,
            Coherent = 23,
            NonWritable = 24,
            NonReadable = 25,
            Uniform = 26,
            SaturatedConversion = 28,
            Stream = 29,
            Location = 30,
            Component = 31,
            Index = 32,
            Binding = 33,
            DescriptorSet = 34,
            Offset = 35,
            XfbBuffer = 36,
            XfbStride = 37,
            FuncParamAttr = 38,
            FPRoundingMode = 39,
            FPFastMathMode = 40,
            LinkageAttributes = 41,
            NoContraction = 42,
            InputAttachmentIndex = 43,
            Alignment = 44,
            MaxByteOffset = 45,
            AlignmentId = 46,
            MaxByteOffsetId = 47,
            ExplicitInterpAMD = 4999,
            OverrideCoverageNV = 5248,
            PassthroughNV = 5250,
            ViewportRelativeNV = 5252,
            SecondaryViewportRelativeNV = 5256,
        }

        public class SpecIdParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new LiteralInteger(), };
        }

        public class ArrayStrideParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new LiteralInteger(), };
        }

        public class MatrixStrideParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new LiteralInteger(), };
        }

        public class BuiltInParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new BuiltIn(), };
        }

        public class StreamParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new LiteralInteger(), };
        }

        public class LocationParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new LiteralInteger(), };
        }

        public class ComponentParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new LiteralInteger(), };
        }

        public class IndexParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new LiteralInteger(), };
        }

        public class BindingParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new LiteralInteger(), };
        }

        public class DescriptorSetParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new LiteralInteger(), };
        }

        public class OffsetParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new LiteralInteger(), };
        }

        public class XfbBufferParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new LiteralInteger(), };
        }

        public class XfbStrideParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new LiteralInteger(), };
        }

        public class FuncParamAttrParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new FunctionParameterAttribute(), };
        }

        public class FPRoundingModeParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new FPRoundingMode(), };
        }

        public class FPFastMathModeParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new FPFastMathMode(), };
        }

        public class LinkageAttributesParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new LiteralString(), new LinkageType(), };
        }

        public class InputAttachmentIndexParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new LiteralInteger(), };
        }

        public class AlignmentParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new LiteralInteger(), };
        }

        public class MaxByteOffsetParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new LiteralInteger(), };
        }

        public class AlignmentIdParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new IdRef(), };
        }

        public class MaxByteOffsetIdParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new IdRef(), };
        }

        public class SecondaryViewportRelativeNVParameter : Parameter
        {
            public override IList<OperandKind> OperandKinds
            {
                get
                {
                    return operandKinds_;
                }
            }

            private static IList<OperandKind> operandKinds_ = new List<OperandKind>()
        {new LiteralInteger(), };
        }

        public override Parameter CreateParameter(uint value)
        {
            switch (value)
            {
                case (uint)Values.SpecId:
                    return new SpecIdParameter();
                case (uint)Values.ArrayStride:
                    return new ArrayStrideParameter();
                case (uint)Values.MatrixStride:
                    return new MatrixStrideParameter();
                case (uint)Values.BuiltIn:
                    return new BuiltInParameter();
                case (uint)Values.Stream:
                    return new StreamParameter();
                case (uint)Values.Location:
                    return new LocationParameter();
                case (uint)Values.Component:
                    return new ComponentParameter();
                case (uint)Values.Index:
                    return new IndexParameter();
                case (uint)Values.Binding:
                    return new BindingParameter();
                case (uint)Values.DescriptorSet:
                    return new DescriptorSetParameter();
                case (uint)Values.Offset:
                    return new OffsetParameter();
                case (uint)Values.XfbBuffer:
                    return new XfbBufferParameter();
                case (uint)Values.XfbStride:
                    return new XfbStrideParameter();
                case (uint)Values.FuncParamAttr:
                    return new FuncParamAttrParameter();
                case (uint)Values.FPRoundingMode:
                    return new FPRoundingModeParameter();
                case (uint)Values.FPFastMathMode:
                    return new FPFastMathModeParameter();
                case (uint)Values.LinkageAttributes:
                    return new LinkageAttributesParameter();
                case (uint)Values.InputAttachmentIndex:
                    return new InputAttachmentIndexParameter();
                case (uint)Values.Alignment:
                    return new AlignmentParameter();
                case (uint)Values.MaxByteOffset:
                    return new MaxByteOffsetParameter();
                case (uint)Values.AlignmentId:
                    return new AlignmentIdParameter();
                case (uint)Values.MaxByteOffsetId:
                    return new MaxByteOffsetIdParameter();
                case (uint)Values.SecondaryViewportRelativeNV:
                    return new SecondaryViewportRelativeNVParameter();
            }

            return null;
        }
    }
    public class BuiltIn : EnumOperandKind
    {
        public enum Values
        {
            Position = 0,
            PointSize = 1,
            ClipDistance = 3,
            CullDistance = 4,
            VertexId = 5,
            InstanceId = 6,
            PrimitiveId = 7,
            InvocationId = 8,
            Layer = 9,
            ViewportIndex = 10,
            TessLevelOuter = 11,
            TessLevelInner = 12,
            TessCoord = 13,
            PatchVertices = 14,
            FragCoord = 15,
            PointCoord = 16,
            FrontFacing = 17,
            SampleId = 18,
            SamplePosition = 19,
            SampleMask = 20,
            FragDepth = 22,
            HelperInvocation = 23,
            NumWorkgroups = 24,
            WorkgroupSize = 25,
            WorkgroupId = 26,
            LocalInvocationId = 27,
            GlobalInvocationId = 28,
            LocalInvocationIndex = 29,
            WorkDim = 30,
            GlobalSize = 31,
            EnqueuedWorkgroupSize = 32,
            GlobalOffset = 33,
            GlobalLinearId = 34,
            SubgroupSize = 36,
            SubgroupMaxSize = 37,
            NumSubgroups = 38,
            NumEnqueuedSubgroups = 39,
            SubgroupId = 40,
            SubgroupLocalInvocationId = 41,
            VertexIndex = 42,
            InstanceIndex = 43,
            SubgroupEqMaskKHR = 4416,
            SubgroupGeMaskKHR = 4417,
            SubgroupGtMaskKHR = 4418,
            SubgroupLeMaskKHR = 4419,
            SubgroupLtMaskKHR = 4420,
            BaseVertex = 4424,
            BaseInstance = 4425,
            DrawIndex = 4426,
            DeviceIndex = 4438,
            ViewIndex = 4440,
            BaryCoordNoPerspAMD = 4992,
            BaryCoordNoPerspCentroidAMD = 4993,
            BaryCoordNoPerspSampleAMD = 4994,
            BaryCoordSmoothAMD = 4995,
            BaryCoordSmoothCentroidAMD = 4996,
            BaryCoordSmoothSampleAMD = 4997,
            BaryCoordPullModelAMD = 4998,
            FragStencilRefEXT = 5014,
            ViewportMaskNV = 5253,
            SecondaryPositionNV = 5257,
            SecondaryViewportMaskNV = 5258,
            PositionPerViewNV = 5261,
            ViewportMaskPerViewNV = 5262,
        }
    }
    public class Scope : EnumOperandKind
    {
        public enum Values
        {
            CrossDevice = 0,
            Device = 1,
            Workgroup = 2,
            Subgroup = 3,
            Invocation = 4,
        }
    }
    public class GroupOperation : EnumOperandKind
    {
        public enum Values
        {
            Reduce = 0,
            InclusiveScan = 1,
            ExclusiveScan = 2,
        }
    }
    public class KernelEnqueueFlags : EnumOperandKind
    {
        public enum Values
        {
            NoWait = 0,
            WaitKernel = 1,
            WaitWorkGroup = 2,
        }
    }
    public class Capability : EnumOperandKind
    {
        public enum Values
        {
            Matrix = 0,
            Shader = 1,
            Geometry = 2,
            Tessellation = 3,
            Addresses = 4,
            Linkage = 5,
            Kernel = 6,
            Vector16 = 7,
            Float16Buffer = 8,
            Float16 = 9,
            Float64 = 10,
            Int64 = 11,
            Int64Atomics = 12,
            ImageBasic = 13,
            ImageReadWrite = 14,
            ImageMipmap = 15,
            Pipes = 17,
            Groups = 18,
            DeviceEnqueue = 19,
            LiteralSampler = 20,
            AtomicStorage = 21,
            Int16 = 22,
            TessellationPointSize = 23,
            GeometryPointSize = 24,
            ImageGatherExtended = 25,
            StorageImageMultisample = 27,
            UniformBufferArrayDynamicIndexing = 28,
            SampledImageArrayDynamicIndexing = 29,
            StorageBufferArrayDynamicIndexing = 30,
            StorageImageArrayDynamicIndexing = 31,
            ClipDistance = 32,
            CullDistance = 33,
            ImageCubeArray = 34,
            SampleRateShading = 35,
            ImageRect = 36,
            SampledRect = 37,
            GenericPointer = 38,
            Int8 = 39,
            InputAttachment = 40,
            SparseResidency = 41,
            MinLod = 42,
            Sampled1D = 43,
            Image1D = 44,
            SampledCubeArray = 45,
            SampledBuffer = 46,
            ImageBuffer = 47,
            ImageMSArray = 48,
            StorageImageExtendedFormats = 49,
            ImageQuery = 50,
            DerivativeControl = 51,
            InterpolationFunction = 52,
            TransformFeedback = 53,
            GeometryStreams = 54,
            StorageImageReadWithoutFormat = 55,
            StorageImageWriteWithoutFormat = 56,
            MultiViewport = 57,
            SubgroupDispatch = 58,
            NamedBarrier = 59,
            PipeStorage = 60,
            SubgroupBallotKHR = 4423,
            DrawParameters = 4427,
            SubgroupVoteKHR = 4431,
            StorageBuffer16BitAccess = 4433,
            StorageUniformBufferBlock16 = 4433,
            UniformAndStorageBuffer16BitAccess = 4434,
            StorageUniform16 = 4434,
            StoragePushConstant16 = 4435,
            StorageInputOutput16 = 4436,
            DeviceGroup = 4437,
            MultiView = 4439,
            VariablePointersStorageBuffer = 4441,
            VariablePointers = 4442,
            AtomicStorageOps = 4445,
            SampleMaskPostDepthCoverage = 4447,
            ImageGatherBiasLodAMD = 5009,
            FragmentMaskAMD = 5010,
            StencilExportEXT = 5013,
            ImageReadWriteLodAMD = 5015,
            SampleMaskOverrideCoverageNV = 5249,
            GeometryShaderPassthroughNV = 5251,
            ShaderViewportIndexLayerEXT = 5254,
            ShaderViewportIndexLayerNV = 5254,
            ShaderViewportMaskNV = 5255,
            ShaderStereoViewNV = 5259,
            PerViewAttributesNV = 5260,
            SubgroupShuffleINTEL = 5568,
            SubgroupBufferBlockIOINTEL = 5569,
            SubgroupImageBlockIOINTEL = 5570,
        }
    }
}