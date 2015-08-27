// automatically generated, do not modify

namespace Synchronica.Schema
{

using FlatBuffers;

public sealed class PulseKeyFrameData_Int64 : Table {
  public static PulseKeyFrameData_Int64 GetRootAsPulseKeyFrameData_Int64(ByteBuffer _bb) { return GetRootAsPulseKeyFrameData_Int64(_bb, new PulseKeyFrameData_Int64()); }
  public static PulseKeyFrameData_Int64 GetRootAsPulseKeyFrameData_Int64(ByteBuffer _bb, PulseKeyFrameData_Int64 obj) { return (obj.__init(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public PulseKeyFrameData_Int64 __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public int Milliseconds { get { int o = __offset(4); return o != 0 ? bb.GetInt(o + bb_pos) : (int)0; } }
  public long Value { get { int o = __offset(6); return o != 0 ? bb.GetLong(o + bb_pos) : (long)0; } }

  public static Offset<PulseKeyFrameData_Int64> CreatePulseKeyFrameData_Int64(FlatBufferBuilder builder,
      int milliseconds = 0,
      long value = 0) {
    builder.StartObject(2);
    PulseKeyFrameData_Int64.AddValue(builder, value);
    PulseKeyFrameData_Int64.AddMilliseconds(builder, milliseconds);
    return PulseKeyFrameData_Int64.EndPulseKeyFrameData_Int64(builder);
  }

  public static void StartPulseKeyFrameData_Int64(FlatBufferBuilder builder) { builder.StartObject(2); }
  public static void AddMilliseconds(FlatBufferBuilder builder, int milliseconds) { builder.AddInt(0, milliseconds, 0); }
  public static void AddValue(FlatBufferBuilder builder, long value) { builder.AddLong(1, value, 0); }
  public static Offset<PulseKeyFrameData_Int64> EndPulseKeyFrameData_Int64(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<PulseKeyFrameData_Int64>(o);
  }
};


}
