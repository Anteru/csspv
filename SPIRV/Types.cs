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
		public Type SampledType { get; }
		public Dim Dim { get; }
		public int Depth { get; }
		public bool IsArray { get; }
		public bool IsMultisampled { get; }
		public int Sampled { get; }
		public ImageFormat Format { get; }
		public AccessQualifier AccessQualifier { get; }
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
		}

		public IReadOnlyList<Type> MemberTypes { get; }

		public override string ToString ()
		{
			var sb = new StringBuilder ();

			sb.Append ("struct {");
			foreach (var m in MemberTypes) {
				sb.Append (m.ToString ());
				sb.Append ("; ");
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
				return $"* [{StorageClass}]";
			} else {
				return $"{Type}* [{StorageClass}]";
			}
		}
	}

	class FunctionType : Type
	{
		public Type ReturnType { get; }
		public IReadOnlyList<Type> ParameterTypes { get; }
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
