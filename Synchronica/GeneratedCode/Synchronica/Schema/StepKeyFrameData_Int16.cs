// automatically generated, do not modify

namespace Synchronica.Schema
{

using FlatBuffers;

public sealed class StepKeyFrameData_Int16 : Table {
  public static StepKeyFrameData_Int16 GetRootAsStepKeyFrameData_Int16(ByteBuffer _bb) { return GetRootAsStepKeyFrameData_Int16(_bb, new StepKeyFrameData_Int16()); }
  public static StepKeyFrameData_Int16 GetRootAsStepKeyFrameData_Int16(ByteBuffer _bb, StepKeyFrameData_Int16 obj) { return (obj.__init(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public StepKeyFrameData_Int16 __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public int Milliseconds { get { int o = __offset(4); return o != 0 ? bb.GetInt(o + bb_pos) : (int)0; } }
  public short Value { get { int o = __offset(6); return o != 0 ? bb.GetShort(o + bb_pos) : (short)0; } }

  public static Offset<StepKeyFrameData_Int16> CreateStepKeyFrameData_Int16(FlatBufferBuilder builder,
      int milliseconds = 0,
      short value = 0) {
    builder.StartObject(2);
    StepKeyFrameData_Int16.AddMilliseconds(builder, milliseconds);
    StepKeyFrameData_Int16.AddValue(builder, value);
    return StepKeyFrameData_Int16.EndStepKeyFrameData_Int16(builder);
  }

  public static void StartStepKeyFrameData_Int16(FlatBufferBuilder builder) { builder.StartObject(2); }
  public static void AddMilliseconds(FlatBufferBuilder builder, int milliseconds) { builder.AddInt(0, milliseconds, 0); }
  public static void AddValue(FlatBufferBuilder builder, short value) { builder.AddShort(1, value, 0); }
  public static Offset<StepKeyFrameData_Int16> EndStepKeyFrameData_Int16(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<StepKeyFrameData_Int16>(o);
  }
};


}
