// automatically generated, do not modify

namespace Synchronica.Schema
{

using FlatBuffers;

public sealed class PulseKeyFrameData_Int16 : Table {
  public static PulseKeyFrameData_Int16 GetRootAsPulseKeyFrameData_Int16(ByteBuffer _bb) { return GetRootAsPulseKeyFrameData_Int16(_bb, new PulseKeyFrameData_Int16()); }
  public static PulseKeyFrameData_Int16 GetRootAsPulseKeyFrameData_Int16(ByteBuffer _bb, PulseKeyFrameData_Int16 obj) { return (obj.__init(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public PulseKeyFrameData_Int16 __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public int Time { get { int o = __offset(4); return o != 0 ? bb.GetInt(o + bb_pos) : (int)0; } }
  public short Value { get { int o = __offset(6); return o != 0 ? bb.GetShort(o + bb_pos) : (short)0; } }

  public static Offset<PulseKeyFrameData_Int16> CreatePulseKeyFrameData_Int16(FlatBufferBuilder builder,
      int time = 0,
      short value = 0) {
    builder.StartObject(2);
    PulseKeyFrameData_Int16.AddTime(builder, time);
    PulseKeyFrameData_Int16.AddValue(builder, value);
    return PulseKeyFrameData_Int16.EndPulseKeyFrameData_Int16(builder);
  }

  public static void StartPulseKeyFrameData_Int16(FlatBufferBuilder builder) { builder.StartObject(2); }
  public static void AddTime(FlatBufferBuilder builder, int time) { builder.AddInt(0, time, 0); }
  public static void AddValue(FlatBufferBuilder builder, short value) { builder.AddShort(1, value, 0); }
  public static Offset<PulseKeyFrameData_Int16> EndPulseKeyFrameData_Int16(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<PulseKeyFrameData_Int16>(o);
  }
};


}
