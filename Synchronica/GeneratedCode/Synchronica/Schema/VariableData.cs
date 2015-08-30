// automatically generated, do not modify

namespace Synchronica.Schema
{

using FlatBuffers;

public sealed class VariableData : Table {
  public static VariableData GetRootAsVariableData(ByteBuffer _bb) { return GetRootAsVariableData(_bb, new VariableData()); }
  public static VariableData GetRootAsVariableData(ByteBuffer _bb, VariableData obj) { return (obj.__init(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public VariableData __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public int Id { get { int o = __offset(4); return o != 0 ? bb.GetInt(o + bb_pos) : (int)0; } }
  public VariableParameters Parameters { get { return GetParameters(new VariableParameters()); } }
  public VariableParameters GetParameters(VariableParameters obj) { int o = __offset(6); return o != 0 ? obj.__init(__indirect(o + bb_pos), bb) : null; }
  public KeyFrameData GetKeyFrames(int j) { return GetKeyFrames(new KeyFrameData(), j); }
  public KeyFrameData GetKeyFrames(KeyFrameData obj, int j) { int o = __offset(8); return o != 0 ? obj.__init(__indirect(__vector(o) + j * 4), bb) : null; }
  public int KeyFramesLength { get { int o = __offset(8); return o != 0 ? __vector_len(o) : 0; } }

  public static Offset<VariableData> CreateVariableData(FlatBufferBuilder builder,
      int id = 0,
      Offset<VariableParameters> parameters = default(Offset<VariableParameters>),
      VectorOffset keyFrames = default(VectorOffset)) {
    builder.StartObject(3);
    VariableData.AddKeyFrames(builder, keyFrames);
    VariableData.AddParameters(builder, parameters);
    VariableData.AddId(builder, id);
    return VariableData.EndVariableData(builder);
  }

  public static void StartVariableData(FlatBufferBuilder builder) { builder.StartObject(3); }
  public static void AddId(FlatBufferBuilder builder, int id) { builder.AddInt(0, id, 0); }
  public static void AddParameters(FlatBufferBuilder builder, Offset<VariableParameters> parametersOffset) { builder.AddOffset(1, parametersOffset.Value, 0); }
  public static void AddKeyFrames(FlatBufferBuilder builder, VectorOffset keyFramesOffset) { builder.AddOffset(2, keyFramesOffset.Value, 0); }
  public static VectorOffset CreateKeyFramesVector(FlatBufferBuilder builder, Offset<KeyFrameData>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static void StartKeyFramesVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<VariableData> EndVariableData(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<VariableData>(o);
  }
};


}
