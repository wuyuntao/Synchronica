// automatically generated, do not modify

namespace Synchronica.Schema
{

using FlatBuffers;

public sealed class VariableParameters : Table {
  public static VariableParameters GetRootAsVariableParameters(ByteBuffer _bb) { return GetRootAsVariableParameters(_bb, new VariableParameters()); }
  public static VariableParameters GetRootAsVariableParameters(ByteBuffer _bb, VariableParameters obj) { return (obj.__init(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public VariableParameters __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public VariableType Type { get { int o = __offset(4); return o != 0 ? (VariableType)bb.GetSbyte(o + bb_pos) : (VariableType)0; } }

  public static Offset<VariableParameters> CreateVariableParameters(FlatBufferBuilder builder,
      VariableType type = (VariableType)0) {
    builder.StartObject(1);
    VariableParameters.AddType(builder, type);
    return VariableParameters.EndVariableParameters(builder);
  }

  public static void StartVariableParameters(FlatBufferBuilder builder) { builder.StartObject(1); }
  public static void AddType(FlatBufferBuilder builder, VariableType type) { builder.AddSbyte(0, (sbyte)(type), 0); }
  public static Offset<VariableParameters> EndVariableParameters(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<VariableParameters>(o);
  }
};


}
