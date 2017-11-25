using System;
using System.Collections.Generic;
using System.Text;

namespace SpirV
{
	public class Type
	{

	}

	class VoidType : Type
	{
		public override string ToString ()
		{
			return "void";
		}
	}

	public class ScalarType : Type
	{

	}

	class BoolType : ScalarType
	{
		public override string ToString ()
		{
			return "bool";
		}
	}

	public class IntegerType : ScalarType
	{
		public IntegerType (int width, bool signed)
		{
			Width = width;
			Signed = signed;
		}

		public int Width { get; }
		public bool Signed { get; }

		public override string ToString ()
		{
			if (Signed) {
				return $"i{Width}";
			} else {
				return $"u{Width}";
			}
		}
	}

	public class FloatingPointType : ScalarType
	{
		public FloatingPointType (int width)
		{
			Width = width;
		}

		public int Width { get; }

		public override string ToString ()
		{
			return $"f{Width}";
		}
	}

	class VectorType : Type
	{
		public VectorType (ScalarType scalarType, int componentCount)
		{
			ComponentType = scalarType;
			ComponentCount = componentCount;
		}

		public ScalarType ComponentType { get; }
		public int ComponentCount { get; }

		public override string ToString ()
		{
			return $"{ComponentType}_{ComponentCount}";
		}
	}

	class MatrixType : Type
	{
		public MatrixType (VectorType vectorType, int columnCount)
		{
			ColumnType = vectorType;
			ColumnCount = columnCount;
		}

		public VectorType ColumnType { get; }
		public int ColumnCount { get; }
		public int RowCount { get { return ColumnType.ComponentCount; } }

		public override string ToString ()
		{
			return $"{ColumnType}x{ColumnCount}";
		}
	}

	class ImageType : Type
	{
		public ImageType (Type sampledType, Dim.Values dim, int depth,
			bool isArray, bool isMultisampled, int sampleCount,
			ImageFormat.Values imageFormat, AccessQualifier.Values accessQualifier)
		{
			SampledType = sampledType;
			Dim = dim;
			Depth = depth;
			IsArray = isArray;
			IsMultisampled = isMultisampled;
			SampleCount = sampleCount;
			Format = imageFormat;
			AccessQualifier = accessQualifier;
		}

		public Type SampledType { get; }
		public Dim.Values Dim { get; }
		public int Depth { get; }
		public bool IsArray { get; }
		public bool IsMultisampled { get; }
		public int SampleCount { get; }
		public ImageFormat.Values Format { get; }
		public AccessQualifier.Values AccessQualifier { get; }
	}

	class SamplerType : Type
	{

	}

	class SampledImage : Type
	{
		public ImageType ImageType { get; }
	}

	class ArrayType : Type
	{
		public ArrayType (Type elementType, int elementCount)
		{
			ElementType = elementType;
			elementCount = ElementCount;
		}

		public int ElementCount { get; }
		public Type ElementType { get; }
	}

	class RuntimeArrayType : Type
	{
		public RuntimeArrayType (Type elementType)
		{
			ElementType = elementType;
		}

		public Type ElementType { get; }
	}

	class StructType : Type
	{
		public StructType (IReadOnlyList<Type> memberTypes)
		{
			MemberTypes = memberTypes;
			memberNames_ = new List<string> ();

			for (int i = 0; i < memberTypes.Count; ++i) {
				memberNames_.Add (String.Empty);
			}
		}

		private List<string> memberNames_;

		public IReadOnlyList<Type> MemberTypes { get; }
		public IReadOnlyList<string> MemberNames { get { return memberNames_; } }

		public void SetMemberName (uint member, string name)
		{
			memberNames_ [(int)member] = name;
		}

		public override string ToString ()
		{
			var sb = new StringBuilder ();

			sb.Append ("struct {");
			for(int i = 0; i <  MemberTypes.Count; ++i) {
				var memberType = MemberTypes [i];
				sb.Append (memberType.ToString ());

				if (! string.IsNullOrEmpty (memberNames_ [i])) {
					sb.Append (" ");
					sb.Append (MemberNames [i]);
				}

				sb.Append (";");
				if (i < (MemberTypes.Count - 1)) {
					sb.Append (" ");
				}
			}
			sb.Append ("}");

			return sb.ToString ();
		}
	}

	class OpaqueType : Type
	{
		public string Name { get; }
	}

	class PointerType : Type
	{
		public StorageClass.Values StorageClass { get; }
		public Type Type { get; private set; }

		public PointerType (StorageClass.Values storageClass, Type type)
		{
			StorageClass = storageClass;
			Type = type;
		}

		public PointerType (StorageClass.Values storageClass)
		{
			StorageClass = storageClass;
		}

		public void ResolveForwardReference (Type t)
		{
			Type = t;
		}

		public override string ToString ()
		{
			if (Type == null) {
				return $"{StorageClass} *";
			} else {
				return $"{StorageClass} {Type}*";
			}
		}
	}

	class FunctionType : Type
	{
		public Type ReturnType { get; }
		public IReadOnlyList<Type> ParameterTypes { get { return parameterTypes_; } }

		private List<Type> parameterTypes_ = new List<Type> ();

		public FunctionType (Type returnType, List<Type> parameterTypes)
		{
			ReturnType = returnType;
			parameterTypes_ = parameterTypes;
		}
	}

	class EventType : Type
	{
	}

	class DeviceEventType : Type
	{
	}

	class ReserveIdType : Type
	{
	}

	class QueueType : Type
	{
	}

	class PipeType : Type
	{
	}

	class PipeStorage : Type
	{
	}

	class NamedBarrier : Type
	{
	}
}
