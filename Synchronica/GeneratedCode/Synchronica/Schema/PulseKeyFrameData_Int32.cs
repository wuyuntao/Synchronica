// automatically generated, do not modify

namespace Synchronica.Schema
{

using FlatBuffers;

public sealed class PulseKeyFrameData_Int32 : Table {
  public static PulseKeyFrameData_Int32 GetRootAsPulseKeyFrameData_Int32(ByteBuffer _bb) { return GetRootAsPulseKeyFrameData_Int32(_bb, new PulseKeyFrameData_Int32()); }
  public static PulseKeyFrameData_Int32 GetRootAsPulseKeyFrameData_Int32(ByteBuffer _bb, PulseKeyFrameData_Int32 obj) { return (obj.__init(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public PulseKeyFrameData_Int32 __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public int Time { get { int o = __offset(4); return o != 0 ? bb.GetInt(o + bb_pos) : (int)0; } }
  public int Value { get { int o = __offset(6); return o != 0 ? bb.GetInt(o + bb_pos) : (int)0; } }

  public static Offset<PulseKeyFrameData_Int32> CreatePulseKeyFrameData_Int32(FlatBufferBuilder builder,
      int time = 0,
      int value = 0) {
    builder.StartObject(2);
    PulseKeyFrameData_Int32.AddValue(builder, value);
    PulseKeyFrameData_Int32.AddTime(builder, time);
    return PulseKeyFrameData_Int32.EndPulseKeyFrameData_Int32(builder);
  }

  public static void StartPulseKeyFrameData_Int32(FlatBufferBuilder builder) { builder.StartObject(2); }
  public static void AddTime(FlatBufferBuilder builder, int time) { builder.AddInt(0, time, 0); }
  public static void AddValue(FlatBufferBuilder builder, int value) { builder.AddInt(1, value, 0); }
  public static Offset<PulseKeyFrameData_Int32> EndPulseKeyFrameData_Int32(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<PulseKeyFrameData_Int32>(o);
  }
};


}
