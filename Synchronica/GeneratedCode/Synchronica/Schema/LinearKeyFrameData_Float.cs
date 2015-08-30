// automatically generated, do not modify

namespace Synchronica.Schema
{

using FlatBuffers;

public sealed class LinearKeyFrameData_Float : Table {
  public static LinearKeyFrameData_Float GetRootAsLinearKeyFrameData_Float(ByteBuffer _bb) { return GetRootAsLinearKeyFrameData_Float(_bb, new LinearKeyFrameData_Float()); }
  public static LinearKeyFrameData_Float GetRootAsLinearKeyFrameData_Float(ByteBuffer _bb, LinearKeyFrameData_Float obj) { return (obj.__init(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public LinearKeyFrameData_Float __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public int Time { get { int o = __offset(4); return o != 0 ? bb.GetInt(o + bb_pos) : (int)0; } }
  public float Value { get { int o = __offset(6); return o != 0 ? bb.GetFloat(o + bb_pos) : (float)0; } }

  public static Offset<LinearKeyFrameData_Float> CreateLinearKeyFrameData_Float(FlatBufferBuilder builder,
      int time = 0,
      float value = 0) {
    builder.StartObject(2);
    LinearKeyFrameData_Float.AddValue(builder, value);
    LinearKeyFrameData_Float.AddTime(builder, time);
    return LinearKeyFrameData_Float.EndLinearKeyFrameData_Float(builder);
  }

  public static void StartLinearKeyFrameData_Float(FlatBufferBuilder builder) { builder.StartObject(2); }
  public static void AddTime(FlatBufferBuilder builder, int time) { builder.AddInt(0, time, 0); }
  public static void AddValue(FlatBufferBuilder builder, float value) { builder.AddFloat(1, value, 0); }
  public static Offset<LinearKeyFrameData_Float> EndLinearKeyFrameData_Float(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<LinearKeyFrameData_Float>(o);
  }
};


}
