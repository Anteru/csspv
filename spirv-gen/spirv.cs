using System;
using System.Collections.Generic;

namespace SpirV
{
    class Meta
    {
        public static uint MagicNumber
        {
            get
            {
                return 119734787U;
            }

            set
            {
            }
        }

        public static uint Version
        {
            get
            {
                return 66048U;
            }

            set
            {
            }
        }

        public static uint Revision
        {
            get
            {
                return 2U;
            }

            set
            {
            }
        }

        public static uint OpCodeMask
        {
            get
            {
                return 65535U;
            }

            set
            {
            }
        }

        public static uint WordCountShift
        {
            get
            {
                return 16U;
            }

            set
            {
            }
        }

        public class ToolInfo
        {
            public ToolInfo(string vendor)
            {
                Vendor = vendor;
            }

            public ToolInfo(string vendor, string name)
            {
                Vendor = vendor;
                Name = name;
            }

            public String Name
            {
                get;
            }

            public String Vendor
            {
                get;
            }
        }

        private static Dictionary<int, ToolInfo> toolInfos_ = new Dictionary<int, ToolInfo>{{0, new ToolInfo("Khronos")}, {1, new ToolInfo("LunarG")}, {2, new ToolInfo("Valve")}, {3, new ToolInfo("Codeplay")}, {4, new ToolInfo("NVIDIA")}, {5, new ToolInfo("ARM")}, {6, new ToolInfo("Khronos", "LLVM/SPIR-V Translator")}, {7, new ToolInfo("Khronos", "SPIR-V Tools Assembler")}, {8, new ToolInfo("Khronos", "Glslang Reference Front End")}, {9, new ToolInfo("Qualcomm")}, {10, new ToolInfo("AMD")}, {11, new ToolInfo("Intel")}, {12, new ToolInfo("Imagination")}, {13, new ToolInfo("Google", "Shaderc over Glslang")}, {14, new ToolInfo("Google", "spiregg")}, {15, new ToolInfo("Google", "rspirv")}, {16, new ToolInfo("X-LEGEND", "Mesa-IR/SPIR-V Translator")}, {17, new ToolInfo("Khronos", "SPIR-V Tools Linker")}, };
        public static IReadOnlyDictionary<int, ToolInfo> Tools
        {
            get
            {
                return toolInfos_;
            }
        }
    }

    enum SourceLanguage
    {
        ESSL = 1,
        GLSL = 2,
        HLSL = 5,
        OpenCL_C = 3,
        OpenCL_CPP = 4,
        Unknown = 0
    }

    enum ExecutionModel
    {
        Fragment = 4,
        Geometry = 3,
        GLCompute = 5,
        Kernel = 6,
        TessellationControl = 1,
        TessellationEvaluation = 2,
        Vertex = 0
    }

    enum AddressingModel
    {
        Logical = 0,
        Physical32 = 1,
        Physical64 = 2
    }

    enum MemoryModel
    {
        GLSL450 = 1,
        OpenCL = 2,
        Simple = 0
    }

    enum ExecutionMode
    {
        ContractionOff = 31,
        DepthGreater = 14,
        DepthLess = 15,
        DepthReplacing = 12,
        DepthUnchanged = 16,
        EarlyFragmentTests = 9,
        Finalizer = 34,
        Initializer = 33,
        InputLines = 20,
        InputLinesAdjacency = 21,
        InputPoints = 19,
        InputTrianglesAdjacency = 23,
        Invocations = 0,
        Isolines = 25,
        LocalSize = 17,
        LocalSizeHint = 18,
        LocalSizeHintId = 39,
        LocalSizeId = 38,
        OriginLowerLeft = 8,
        OriginUpperLeft = 7,
        OutputLineStrip = 28,
        OutputPoints = 27,
        OutputTriangleStrip = 29,
        OutputVertices = 26,
        PixelCenterInteger = 6,
        PointMode = 10,
        PostDepthCoverage = 4446,
        Quads = 24,
        SpacingEqual = 1,
        SpacingFractionalEven = 2,
        SpacingFractionalOdd = 3,
        StencilRefReplacingEXT = 5027,
        SubgroupSize = 35,
        SubgroupsPerWorkgroup = 36,
        SubgroupsPerWorkgroupId = 37,
        Triangles = 22,
        VecTypeHint = 30,
        VertexOrderCcw = 5,
        VertexOrderCw = 4,
        Xfb = 11
    }

    enum StorageClass
    {
        AtomicCounter = 10,
        CrossWorkgroup = 5,
        Function = 7,
        Generic = 8,
        Image = 11,
        Input = 1,
        Output = 3,
        Private = 6,
        PushConstant = 9,
        StorageBuffer = 12,
        Uniform = 2,
        UniformConstant = 0,
        Workgroup = 4
    }

    enum Dim
    {
        Buffer = 5,
        Cube = 3,
        Dim1D = 0,
        Dim2D = 1,
        Dim3D = 2,
        Rect = 4,
        SubpassData = 6
    }

    enum SamplerAddressingMode
    {
        Clamp = 2,
        ClampToEdge = 1,
        None = 0,
        Repeat = 3,
        RepeatMirrored = 4
    }

    enum SamplerFilterMode
    {
        Linear = 1,
        Nearest = 0
    }

    enum ImageFormat
    {
        R11fG11fB10f = 8,
        R16 = 14,
        R16f = 9,
        R16i = 28,
        R16Snorm = 19,
        R16ui = 38,
        R32f = 3,
        R32i = 24,
        R32ui = 33,
        R8 = 15,
        R8i = 29,
        R8Snorm = 20,
        R8ui = 39,
        Rg16 = 12,
        Rg16f = 7,
        Rg16i = 26,
        Rg16Snorm = 17,
        Rg16ui = 36,
        Rg32f = 6,
        Rg32i = 25,
        Rg32ui = 35,
        Rg8 = 13,
        Rg8i = 27,
        Rg8Snorm = 18,
        Rg8ui = 37,
        Rgb10A2 = 11,
        Rgb10a2ui = 34,
        Rgba16 = 10,
        Rgba16f = 2,
        Rgba16i = 22,
        Rgba16Snorm = 16,
        Rgba16ui = 31,
        Rgba32f = 1,
        Rgba32i = 21,
        Rgba32ui = 30,
        Rgba8 = 4,
        Rgba8i = 23,
        Rgba8Snorm = 5,
        Rgba8ui = 32,
        Unknown = 0
    }

    enum ImageChannelOrder
    {
        A = 1,
        ABGR = 19,
        ARGB = 7,
        BGRA = 6,
        Depth = 13,
        DepthStencil = 14,
        Intensity = 8,
        Luminance = 9,
        R = 0,
        RA = 3,
        RG = 2,
        RGB = 4,
        RGBA = 5,
        RGBx = 12,
        RGx = 11,
        Rx = 10,
        sBGRA = 18,
        sRGB = 15,
        sRGBA = 17,
        sRGBx = 16
    }

    enum ImageChannelDataType
    {
        Float = 14,
        HalfFloat = 13,
        SignedInt16 = 8,
        SignedInt32 = 9,
        SignedInt8 = 7,
        SnormInt16 = 1,
        SnormInt8 = 0,
        UnormInt101010 = 6,
        UnormInt101010_2 = 16,
        UnormInt16 = 3,
        UnormInt24 = 15,
        UnormInt8 = 2,
        UnormShort555 = 5,
        UnormShort565 = 4,
        UnsignedInt16 = 11,
        UnsignedInt32 = 12,
        UnsignedInt8 = 10
    }

    [Flags]
    enum ImageOperands
    {
        Bias = 0,
        ConstOffset = 3,
        ConstOffsets = 5,
        Grad = 2,
        Lod = 1,
        MinLod = 7,
        Offset = 4,
        Sample = 6
    }

    [Flags]
    enum FPFastMathMode
    {
        AllowRecip = 3,
        Fast = 4,
        NotInf = 1,
        NotNaN = 0,
        NSZ = 2
    }

    enum FPRoundingMode
    {
        RTE = 0,
        RTN = 3,
        RTP = 2,
        RTZ = 1
    }

    enum LinkageType
    {
        Export = 0,
        Import = 1
    }

    enum AccessQualifier
    {
        ReadOnly = 0,
        ReadWrite = 2,
        WriteOnly = 1
    }

    enum FunctionParameterAttribute
    {
        ByVal = 2,
        NoAlias = 4,
        NoCapture = 5,
        NoReadWrite = 7,
        NoWrite = 6,
        Sext = 1,
        Sret = 3,
        Zext = 0
    }

    enum Decoration
    {
        Aliased = 20,
        Alignment = 44,
        AlignmentId = 46,
        ArrayStride = 6,
        Binding = 33,
        Block = 2,
        BufferBlock = 3,
        BuiltIn = 11,
        Centroid = 16,
        Coherent = 23,
        ColMajor = 5,
        Component = 31,
        Constant = 22,
        CPacked = 10,
        DescriptorSet = 34,
        ExplicitInterpAMD = 4999,
        Flat = 14,
        FPFastMathMode = 40,
        FPRoundingMode = 39,
        FuncParamAttr = 38,
        GLSLPacked = 9,
        GLSLShared = 8,
        Index = 32,
        InputAttachmentIndex = 43,
        Invariant = 18,
        LinkageAttributes = 41,
        Location = 30,
        MatrixStride = 7,
        MaxByteOffset = 45,
        MaxByteOffsetId = 47,
        NoContraction = 42,
        NonReadable = 25,
        NonWritable = 24,
        NoPerspective = 13,
        Offset = 35,
        OverrideCoverageNV = 5248,
        PassthroughNV = 5250,
        Patch = 15,
        RelaxedPrecision = 0,
        Restrict = 19,
        RowMajor = 4,
        Sample = 17,
        SaturatedConversion = 28,
        SecondaryViewportRelativeNV = 5256,
        SpecId = 1,
        Stream = 29,
        Uniform = 26,
        ViewportRelativeNV = 5252,
        Volatile = 21,
        XfbBuffer = 36,
        XfbStride = 37
    }

    enum BuiltIn
    {
        BaryCoordNoPerspAMD = 4992,
        BaryCoordNoPerspCentroidAMD = 4993,
        BaryCoordNoPerspSampleAMD = 4994,
        BaryCoordPullModelAMD = 4998,
        BaryCoordSmoothAMD = 4995,
        BaryCoordSmoothCentroidAMD = 4996,
        BaryCoordSmoothSampleAMD = 4997,
        BaseInstance = 4425,
        BaseVertex = 4424,
        ClipDistance = 3,
        CullDistance = 4,
        DeviceIndex = 4438,
        DrawIndex = 4426,
        EnqueuedWorkgroupSize = 32,
        FragCoord = 15,
        FragDepth = 22,
        FragStencilRefEXT = 5014,
        FrontFacing = 17,
        GlobalInvocationId = 28,
        GlobalLinearId = 34,
        GlobalOffset = 33,
        GlobalSize = 31,
        HelperInvocation = 23,
        InstanceId = 6,
        InstanceIndex = 43,
        InvocationId = 8,
        Layer = 9,
        LocalInvocationId = 27,
        LocalInvocationIndex = 29,
        NumEnqueuedSubgroups = 39,
        NumSubgroups = 38,
        NumWorkgroups = 24,
        PatchVertices = 14,
        PointCoord = 16,
        PointSize = 1,
        Position = 0,
        PositionPerViewNV = 5261,
        PrimitiveId = 7,
        SampleId = 18,
        SampleMask = 20,
        SamplePosition = 19,
        SecondaryPositionNV = 5257,
        SecondaryViewportMaskNV = 5258,
        SubgroupEqMaskKHR = 4416,
        SubgroupGeMaskKHR = 4417,
        SubgroupGtMaskKHR = 4418,
        SubgroupId = 40,
        SubgroupLeMaskKHR = 4419,
        SubgroupLocalInvocationId = 41,
        SubgroupLtMaskKHR = 4420,
        SubgroupMaxSize = 37,
        SubgroupSize = 36,
        TessCoord = 13,
        TessLevelInner = 12,
        TessLevelOuter = 11,
        VertexId = 5,
        VertexIndex = 42,
        ViewIndex = 4440,
        ViewportIndex = 10,
        ViewportMaskNV = 5253,
        ViewportMaskPerViewNV = 5262,
        WorkDim = 30,
        WorkgroupId = 26,
        WorkgroupSize = 25
    }

    [Flags]
    enum SelectionControl
    {
        DontFlatten = 1,
        Flatten = 0
    }

    [Flags]
    enum LoopControl
    {
        DependencyInfinite = 2,
        DependencyLength = 3,
        DontUnroll = 1,
        Unroll = 0
    }

    [Flags]
    enum FunctionControl
    {
        Const = 3,
        DontInline = 1,
        Inline = 0,
        Pure = 2
    }

    [Flags]
    enum MemorySemantics
    {
        Acquire = 1,
        AcquireRelease = 3,
        AtomicCounterMemory = 10,
        CrossWorkgroupMemory = 9,
        ImageMemory = 11,
        Release = 2,
        SequentiallyConsistent = 4,
        SubgroupMemory = 7,
        UniformMemory = 6,
        WorkgroupMemory = 8
    }

    [Flags]
    enum MemoryAccess
    {
        Aligned = 1,
        Nontemporal = 2,
        Volatile = 0
    }

    enum Scope
    {
        CrossDevice = 0,
        Device = 1,
        Invocation = 4,
        Subgroup = 3,
        Workgroup = 2
    }

    enum GroupOperation
    {
        ExclusiveScan = 2,
        InclusiveScan = 1,
        Reduce = 0
    }

    enum KernelEnqueueFlags
    {
        NoWait = 0,
        WaitKernel = 1,
        WaitWorkGroup = 2
    }

    [Flags]
    enum KernelProfilingInfo
    {
        CmdExecTime = 0
    }

    enum Capability
    {
        Addresses = 4,
        AtomicStorage = 21,
        AtomicStorageOps = 4445,
        ClipDistance = 32,
        CullDistance = 33,
        DerivativeControl = 51,
        DeviceEnqueue = 19,
        DeviceGroup = 4437,
        DrawParameters = 4427,
        Float16 = 9,
        Float16Buffer = 8,
        Float64 = 10,
        FragmentMaskAMD = 5010,
        GenericPointer = 38,
        Geometry = 2,
        GeometryPointSize = 24,
        GeometryShaderPassthroughNV = 5251,
        GeometryStreams = 54,
        Groups = 18,
        Image1D = 44,
        ImageBasic = 13,
        ImageBuffer = 47,
        ImageCubeArray = 34,
        ImageGatherBiasLodAMD = 5009,
        ImageGatherExtended = 25,
        ImageMipmap = 15,
        ImageMSArray = 48,
        ImageQuery = 50,
        ImageReadWrite = 14,
        ImageReadWriteLodAMD = 5015,
        ImageRect = 36,
        InputAttachment = 40,
        Int16 = 22,
        Int64 = 11,
        Int64Atomics = 12,
        Int8 = 39,
        InterpolationFunction = 52,
        Kernel = 6,
        Linkage = 5,
        LiteralSampler = 20,
        Matrix = 0,
        MinLod = 42,
        MultiView = 4439,
        MultiViewport = 57,
        NamedBarrier = 59,
        PerViewAttributesNV = 5260,
        Pipes = 17,
        PipeStorage = 60,
        Sampled1D = 43,
        SampledBuffer = 46,
        SampledCubeArray = 45,
        SampledImageArrayDynamicIndexing = 29,
        SampledRect = 37,
        SampleMaskOverrideCoverageNV = 5249,
        SampleMaskPostDepthCoverage = 4447,
        SampleRateShading = 35,
        Shader = 1,
        ShaderStereoViewNV = 5259,
        ShaderViewportIndexLayerEXT = 5254,
        ShaderViewportIndexLayerNV = 5254,
        ShaderViewportMaskNV = 5255,
        SparseResidency = 41,
        StencilExportEXT = 5013,
        StorageBuffer16BitAccess = 4433,
        StorageBufferArrayDynamicIndexing = 30,
        StorageImageArrayDynamicIndexing = 31,
        StorageImageExtendedFormats = 49,
        StorageImageMultisample = 27,
        StorageImageReadWithoutFormat = 55,
        StorageImageWriteWithoutFormat = 56,
        StorageInputOutput16 = 4436,
        StoragePushConstant16 = 4435,
        StorageUniform16 = 4434,
        StorageUniformBufferBlock16 = 4433,
        SubgroupBallotKHR = 4423,
        SubgroupBufferBlockIOINTEL = 5569,
        SubgroupDispatch = 58,
        SubgroupImageBlockIOINTEL = 5570,
        SubgroupShuffleINTEL = 5568,
        SubgroupVoteKHR = 4431,
        Tessellation = 3,
        TessellationPointSize = 23,
        TransformFeedback = 53,
        UniformAndStorageBuffer16BitAccess = 4434,
        UniformBufferArrayDynamicIndexing = 28,
        VariablePointers = 4442,
        VariablePointersStorageBuffer = 4441,
        Vector16 = 7
    }

    enum Op
    {
        OpAccessChain = 65,
        OpAll = 155,
        OpAny = 154,
        OpArrayLength = 68,
        OpAtomicAnd = 240,
        OpAtomicCompareExchange = 230,
        OpAtomicCompareExchangeWeak = 231,
        OpAtomicExchange = 229,
        OpAtomicFlagClear = 319,
        OpAtomicFlagTestAndSet = 318,
        OpAtomicIAdd = 234,
        OpAtomicIDecrement = 233,
        OpAtomicIIncrement = 232,
        OpAtomicISub = 235,
        OpAtomicLoad = 227,
        OpAtomicOr = 241,
        OpAtomicSMax = 238,
        OpAtomicSMin = 236,
        OpAtomicStore = 228,
        OpAtomicUMax = 239,
        OpAtomicUMin = 237,
        OpAtomicXor = 242,
        OpBitcast = 124,
        OpBitCount = 205,
        OpBitFieldInsert = 201,
        OpBitFieldSExtract = 202,
        OpBitFieldUExtract = 203,
        OpBitReverse = 204,
        OpBitwiseAnd = 199,
        OpBitwiseOr = 197,
        OpBitwiseXor = 198,
        OpBranch = 249,
        OpBranchConditional = 250,
        OpBuildNDRange = 304,
        OpCapability = 17,
        OpCaptureEventProfilingInfo = 302,
        OpCommitReadPipe = 280,
        OpCommitWritePipe = 281,
        OpCompositeConstruct = 80,
        OpCompositeExtract = 81,
        OpCompositeInsert = 82,
        OpConstant = 43,
        OpConstantComposite = 44,
        OpConstantFalse = 42,
        OpConstantNull = 46,
        OpConstantPipeStorage = 323,
        OpConstantSampler = 45,
        OpConstantTrue = 41,
        OpControlBarrier = 224,
        OpConvertFToS = 110,
        OpConvertFToU = 109,
        OpConvertPtrToU = 117,
        OpConvertSToF = 111,
        OpConvertUToF = 112,
        OpConvertUToPtr = 120,
        OpCopyMemory = 63,
        OpCopyMemorySized = 64,
        OpCopyObject = 83,
        OpCreatePipeFromPipeStorage = 324,
        OpCreateUserEvent = 299,
        OpDecorate = 71,
        OpDecorateId = 332,
        OpDecorationGroup = 73,
        OpDot = 148,
        OpDPdx = 207,
        OpDPdxCoarse = 213,
        OpDPdxFine = 210,
        OpDPdy = 208,
        OpDPdyCoarse = 214,
        OpDPdyFine = 211,
        OpEmitStreamVertex = 220,
        OpEmitVertex = 218,
        OpEndPrimitive = 219,
        OpEndStreamPrimitive = 221,
        OpEnqueueKernel = 292,
        OpEnqueueMarker = 291,
        OpEntryPoint = 15,
        OpExecutionMode = 16,
        OpExecutionModeId = 331,
        OpExtension = 10,
        OpExtInst = 12,
        OpExtInstImport = 11,
        OpFAdd = 129,
        OpFConvert = 115,
        OpFDiv = 136,
        OpFMod = 141,
        OpFMul = 133,
        OpFNegate = 127,
        OpFOrdEqual = 180,
        OpFOrdGreaterThan = 186,
        OpFOrdGreaterThanEqual = 190,
        OpFOrdLessThan = 184,
        OpFOrdLessThanEqual = 188,
        OpFOrdNotEqual = 182,
        OpFragmentFetchAMD = 5012,
        OpFragmentMaskFetchAMD = 5011,
        OpFRem = 140,
        OpFSub = 131,
        OpFunction = 54,
        OpFunctionCall = 57,
        OpFunctionEnd = 56,
        OpFunctionParameter = 55,
        OpFUnordEqual = 181,
        OpFUnordGreaterThan = 187,
        OpFUnordGreaterThanEqual = 191,
        OpFUnordLessThan = 185,
        OpFUnordLessThanEqual = 189,
        OpFUnordNotEqual = 183,
        OpFwidth = 209,
        OpFwidthCoarse = 215,
        OpFwidthFine = 212,
        OpGenericCastToPtr = 122,
        OpGenericCastToPtrExplicit = 123,
        OpGenericPtrMemSemantics = 69,
        OpGetDefaultQueue = 303,
        OpGetKernelLocalSizeForSubgroupCount = 325,
        OpGetKernelMaxNumSubgroups = 326,
        OpGetKernelNDrangeMaxSubGroupSize = 294,
        OpGetKernelNDrangeSubGroupCount = 293,
        OpGetKernelPreferredWorkGroupSizeMultiple = 296,
        OpGetKernelWorkGroupSize = 295,
        OpGetMaxPipePackets = 284,
        OpGetNumPipePackets = 283,
        OpGroupAll = 261,
        OpGroupAny = 262,
        OpGroupAsyncCopy = 259,
        OpGroupBroadcast = 263,
        OpGroupCommitReadPipe = 287,
        OpGroupCommitWritePipe = 288,
        OpGroupDecorate = 74,
        OpGroupFAdd = 265,
        OpGroupFAddNonUniformAMD = 5001,
        OpGroupFMax = 269,
        OpGroupFMaxNonUniformAMD = 5005,
        OpGroupFMin = 266,
        OpGroupFMinNonUniformAMD = 5002,
        OpGroupIAdd = 264,
        OpGroupIAddNonUniformAMD = 5000,
        OpGroupMemberDecorate = 75,
        OpGroupReserveReadPipePackets = 285,
        OpGroupReserveWritePipePackets = 286,
        OpGroupSMax = 271,
        OpGroupSMaxNonUniformAMD = 5007,
        OpGroupSMin = 268,
        OpGroupSMinNonUniformAMD = 5004,
        OpGroupUMax = 270,
        OpGroupUMaxNonUniformAMD = 5006,
        OpGroupUMin = 267,
        OpGroupUMinNonUniformAMD = 5003,
        OpGroupWaitEvents = 260,
        OpIAdd = 128,
        OpIAddCarry = 149,
        OpIEqual = 170,
        OpImage = 100,
        OpImageDrefGather = 97,
        OpImageFetch = 95,
        OpImageGather = 96,
        OpImageQueryFormat = 101,
        OpImageQueryLevels = 106,
        OpImageQueryLod = 105,
        OpImageQueryOrder = 102,
        OpImageQuerySamples = 107,
        OpImageQuerySize = 104,
        OpImageQuerySizeLod = 103,
        OpImageRead = 98,
        OpImageSampleDrefExplicitLod = 90,
        OpImageSampleDrefImplicitLod = 89,
        OpImageSampleExplicitLod = 88,
        OpImageSampleImplicitLod = 87,
        OpImageSampleProjDrefExplicitLod = 94,
        OpImageSampleProjDrefImplicitLod = 93,
        OpImageSampleProjExplicitLod = 92,
        OpImageSampleProjImplicitLod = 91,
        OpImageSparseDrefGather = 315,
        OpImageSparseFetch = 313,
        OpImageSparseGather = 314,
        OpImageSparseRead = 320,
        OpImageSparseSampleDrefExplicitLod = 308,
        OpImageSparseSampleDrefImplicitLod = 307,
        OpImageSparseSampleExplicitLod = 306,
        OpImageSparseSampleImplicitLod = 305,
        OpImageSparseSampleProjDrefExplicitLod = 312,
        OpImageSparseSampleProjDrefImplicitLod = 311,
        OpImageSparseSampleProjExplicitLod = 310,
        OpImageSparseSampleProjImplicitLod = 309,
        OpImageSparseTexelsResident = 316,
        OpImageTexelPointer = 60,
        OpImageWrite = 99,
        OpIMul = 132,
        OpInBoundsAccessChain = 66,
        OpInBoundsPtrAccessChain = 70,
        OpINotEqual = 171,
        OpIsFinite = 158,
        OpIsInf = 157,
        OpIsNan = 156,
        OpIsNormal = 159,
        OpISub = 130,
        OpISubBorrow = 150,
        OpIsValidEvent = 300,
        OpIsValidReserveId = 282,
        OpKill = 252,
        OpLabel = 248,
        OpLessOrGreater = 161,
        OpLifetimeStart = 256,
        OpLifetimeStop = 257,
        OpLine = 8,
        OpLoad = 61,
        OpLogicalAnd = 167,
        OpLogicalEqual = 164,
        OpLogicalNot = 168,
        OpLogicalNotEqual = 165,
        OpLogicalOr = 166,
        OpLoopMerge = 246,
        OpMatrixTimesMatrix = 146,
        OpMatrixTimesScalar = 143,
        OpMatrixTimesVector = 145,
        OpMemberDecorate = 72,
        OpMemberName = 6,
        OpMemoryBarrier = 225,
        OpMemoryModel = 14,
        OpMemoryNamedBarrier = 329,
        OpModuleProcessed = 330,
        OpName = 5,
        OpNamedBarrierInitialize = 328,
        OpNoLine = 317,
        OpNop = 0,
        OpNot = 200,
        OpOrdered = 162,
        OpOuterProduct = 147,
        OpPhi = 245,
        OpPtrAccessChain = 67,
        OpPtrCastToGeneric = 121,
        OpQuantizeToF16 = 116,
        OpReadPipe = 274,
        OpReleaseEvent = 298,
        OpReservedReadPipe = 276,
        OpReservedWritePipe = 277,
        OpReserveReadPipePackets = 278,
        OpReserveWritePipePackets = 279,
        OpRetainEvent = 297,
        OpReturn = 253,
        OpReturnValue = 254,
        OpSampledImage = 86,
        OpSatConvertSToU = 118,
        OpSatConvertUToS = 119,
        OpSConvert = 114,
        OpSDiv = 135,
        OpSelect = 169,
        OpSelectionMerge = 247,
        OpSetUserEventStatus = 301,
        OpSGreaterThan = 173,
        OpSGreaterThanEqual = 175,
        OpShiftLeftLogical = 196,
        OpShiftRightArithmetic = 195,
        OpShiftRightLogical = 194,
        OpSignBitSet = 160,
        OpSizeOf = 321,
        OpSLessThan = 177,
        OpSLessThanEqual = 179,
        OpSMod = 139,
        OpSMulExtended = 152,
        OpSNegate = 126,
        OpSource = 3,
        OpSourceContinued = 2,
        OpSourceExtension = 4,
        OpSpecConstant = 50,
        OpSpecConstantComposite = 51,
        OpSpecConstantFalse = 49,
        OpSpecConstantOp = 52,
        OpSpecConstantTrue = 48,
        OpSRem = 138,
        OpStore = 62,
        OpString = 7,
        OpSubgroupAllEqualKHR = 4430,
        OpSubgroupAllKHR = 4428,
        OpSubgroupAnyKHR = 4429,
        OpSubgroupBallotKHR = 4421,
        OpSubgroupBlockReadINTEL = 5575,
        OpSubgroupBlockWriteINTEL = 5576,
        OpSubgroupFirstInvocationKHR = 4422,
        OpSubgroupImageBlockReadINTEL = 5577,
        OpSubgroupImageBlockWriteINTEL = 5578,
        OpSubgroupReadInvocationKHR = 4432,
        OpSubgroupShuffleDownINTEL = 5572,
        OpSubgroupShuffleINTEL = 5571,
        OpSubgroupShuffleUpINTEL = 5573,
        OpSubgroupShuffleXorINTEL = 5574,
        OpSwitch = 251,
        OpTranspose = 84,
        OpTypeArray = 28,
        OpTypeBool = 20,
        OpTypeDeviceEvent = 35,
        OpTypeEvent = 34,
        OpTypeFloat = 22,
        OpTypeForwardPointer = 39,
        OpTypeFunction = 33,
        OpTypeImage = 25,
        OpTypeInt = 21,
        OpTypeMatrix = 24,
        OpTypeNamedBarrier = 327,
        OpTypeOpaque = 31,
        OpTypePipe = 38,
        OpTypePipeStorage = 322,
        OpTypePointer = 32,
        OpTypeQueue = 37,
        OpTypeReserveId = 36,
        OpTypeRuntimeArray = 29,
        OpTypeSampledImage = 27,
        OpTypeSampler = 26,
        OpTypeStruct = 30,
        OpTypeVector = 23,
        OpTypeVoid = 19,
        OpUConvert = 113,
        OpUDiv = 134,
        OpUGreaterThan = 172,
        OpUGreaterThanEqual = 174,
        OpULessThan = 176,
        OpULessThanEqual = 178,
        OpUMod = 137,
        OpUMulExtended = 151,
        OpUndef = 1,
        OpUnordered = 163,
        OpUnreachable = 255,
        OpVariable = 59,
        OpVectorExtractDynamic = 77,
        OpVectorInsertDynamic = 78,
        OpVectorShuffle = 79,
        OpVectorTimesMatrix = 144,
        OpVectorTimesScalar = 142,
        OpWritePipe = 275
    }
}