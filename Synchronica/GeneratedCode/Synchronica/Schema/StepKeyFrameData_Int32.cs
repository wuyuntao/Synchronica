// automatically generated, do not modify

namespace Synchronica.Schema
{

using FlatBuffers;

public sealed class StepKeyFrameData_Int32 : Table {
  public static StepKeyFrameData_Int32 GetRootAsStepKeyFrameData_Int32(ByteBuffer _bb) { return GetRootAsStepKeyFrameData_Int32(_bb, new StepKeyFrameData_Int32()); }
  public static StepKeyFrameData_Int32 GetRootAsStepKeyFrameData_Int32(ByteBuffer _bb, StepKeyFrameData_Int32 obj) { return (obj.__init(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public StepKeyFrameData_Int32 __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public int Milliseconds { get { int o = __offset(4); return o != 0 ? bb.GetInt(o + bb_pos) : (int)0; } }
  public int Value { get { int o = __offset(6); return o != 0 ? bb.GetInt(o + bb_pos) : (int)0; } }

  public static Offset<StepKeyFrameData_Int32> CreateStepKeyFrameData_Int32(FlatBufferBuilder builder,
      int milliseconds = 0,
      int value = 0) {
    builder.StartObject(2);
    StepKeyFrameData_Int32.AddValue(builder, value);
    StepKeyFrameData_Int32.AddMilliseconds(builder, milliseconds);
    return StepKeyFrameData_Int32.EndStepKeyFrameData_Int32(builder);
  }

  public static void StartStepKeyFrameData_Int32(FlatBufferBuilder builder) { builder.StartObject(2); }
  public static void AddMilliseconds(FlatBufferBuilder builder, int milliseconds) { builder.AddInt(0, milliseconds, 0); }
  public static void AddValue(FlatBufferBuilder builder, int value) { builder.AddInt(1, value, 0); }
  public static Offset<StepKeyFrameData_Int32> EndStepKeyFrameData_Int32(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<StepKeyFrameData_Int32>(o);
  }
};


}
