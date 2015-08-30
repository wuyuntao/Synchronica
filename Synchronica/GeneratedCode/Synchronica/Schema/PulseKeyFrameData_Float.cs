// automatically generated, do not modify

namespace Synchronica.Schema
{

using FlatBuffers;

public sealed class PulseKeyFrameData_Float : Table {
  public static PulseKeyFrameData_Float GetRootAsPulseKeyFrameData_Float(ByteBuffer _bb) { return GetRootAsPulseKeyFrameData_Float(_bb, new PulseKeyFrameData_Float()); }
  public static PulseKeyFrameData_Float GetRootAsPulseKeyFrameData_Float(ByteBuffer _bb, PulseKeyFrameData_Float obj) { return (obj.__init(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public PulseKeyFrameData_Float __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public int Time { get { int o = __offset(4); return o != 0 ? bb.GetInt(o + bb_pos) : (int)0; } }
  public float Value { get { int o = __offset(6); return o != 0 ? bb.GetFloat(o + bb_pos) : (float)0; } }

  public static Offset<PulseKeyFrameData_Float> CreatePulseKeyFrameData_Float(FlatBufferBuilder builder,
      int time = 0,
      float value = 0) {
    builder.StartObject(2);
    PulseKeyFrameData_Float.AddValue(builder, value);
    PulseKeyFrameData_Float.AddTime(builder, time);
    return PulseKeyFrameData_Float.EndPulseKeyFrameData_Float(builder);
  }

  public static void StartPulseKeyFrameData_Float(FlatBufferBuilder builder) { builder.StartObject(2); }
  public static void AddTime(FlatBufferBuilder builder, int time) { builder.AddInt(0, time, 0); }
  public static void AddValue(FlatBufferBuilder builder, float value) { builder.AddFloat(1, value, 0); }
  public static Offset<PulseKeyFrameData_Float> EndPulseKeyFrameData_Float(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<PulseKeyFrameData_Float>(o);
  }
};


}
