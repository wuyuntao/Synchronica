// automatically generated, do not modify

namespace Synchronica.Schema
{

using FlatBuffers;

public sealed class StepKeyFrameData_Boolean : Table {
  public static StepKeyFrameData_Boolean GetRootAsStepKeyFrameData_Boolean(ByteBuffer _bb) { return GetRootAsStepKeyFrameData_Boolean(_bb, new StepKeyFrameData_Boolean()); }
  public static StepKeyFrameData_Boolean GetRootAsStepKeyFrameData_Boolean(ByteBuffer _bb, StepKeyFrameData_Boolean obj) { return (obj.__init(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public StepKeyFrameData_Boolean __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public int Milliseconds { get { int o = __offset(4); return o != 0 ? bb.GetInt(o + bb_pos) : (int)0; } }
  public bool Value { get { int o = __offset(6); return o != 0 ? 0!=bb.Get(o + bb_pos) : (bool)false; } }

  public static Offset<StepKeyFrameData_Boolean> CreateStepKeyFrameData_Boolean(FlatBufferBuilder builder,
      int milliseconds = 0,
      bool value = false) {
    builder.StartObject(2);
    StepKeyFrameData_Boolean.AddMilliseconds(builder, milliseconds);
    StepKeyFrameData_Boolean.AddValue(builder, value);
    return StepKeyFrameData_Boolean.EndStepKeyFrameData_Boolean(builder);
  }

  public static void StartStepKeyFrameData_Boolean(FlatBufferBuilder builder) { builder.StartObject(2); }
  public static void AddMilliseconds(FlatBufferBuilder builder, int milliseconds) { builder.AddInt(0, milliseconds, 0); }
  public static void AddValue(FlatBufferBuilder builder, bool value) { builder.AddBool(1, value, false); }
  public static Offset<StepKeyFrameData_Boolean> EndStepKeyFrameData_Boolean(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<StepKeyFrameData_Boolean>(o);
  }
};


}
