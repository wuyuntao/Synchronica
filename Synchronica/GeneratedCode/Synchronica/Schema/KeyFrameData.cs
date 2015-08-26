// automatically generated, do not modify

namespace Synchronica.Schema
{

using FlatBuffers;

public sealed class KeyFrameData : Table {
  public static KeyFrameData GetRootAsKeyFrameData(ByteBuffer _bb) { return GetRootAsKeyFrameData(_bb, new KeyFrameData()); }
  public static KeyFrameData GetRootAsKeyFrameData(ByteBuffer _bb, KeyFrameData obj) { return (obj.__init(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public KeyFrameData __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public KeyFrameUnion DataType { get { int o = __offset(4); return o != 0 ? (KeyFrameUnion)bb.Get(o + bb_pos) : (KeyFrameUnion)0; } }
  public TTable GetData<TTable>(TTable obj) where TTable : Table { int o = __offset(6); return o != 0 ? __union(obj, o) : null; }

  public static Offset<KeyFrameData> CreateKeyFrameData(FlatBufferBuilder builder,
      KeyFrameUnion data_type = (KeyFrameUnion)0,
      int data = 0) {
    builder.StartObject(2);
    KeyFrameData.AddData(builder, data);
    KeyFrameData.AddDataType(builder, data_type);
    return KeyFrameData.EndKeyFrameData(builder);
  }

  public static void StartKeyFrameData(FlatBufferBuilder builder) { builder.StartObject(2); }
  public static void AddDataType(FlatBufferBuilder builder, KeyFrameUnion dataType) { builder.AddByte(0, (byte)(dataType), 0); }
  public static void AddData(FlatBufferBuilder builder, int dataOffset) { builder.AddOffset(1, dataOffset, 0); }
  public static Offset<KeyFrameData> EndKeyFrameData(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<KeyFrameData>(o);
  }
};


}
