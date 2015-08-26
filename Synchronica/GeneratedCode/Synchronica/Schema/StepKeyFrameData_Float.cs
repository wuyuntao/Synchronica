// automatically generated, do not modify

namespace Synchronica.Schema
{

using FlatBuffers;

public sealed class StepKeyFrameData_Float : Table {
  public static StepKeyFrameData_Float GetRootAsStepKeyFrameData_Float(ByteBuffer _bb) { return GetRootAsStepKeyFrameData_Float(_bb, new StepKeyFrameData_Float()); }
  public static StepKeyFrameData_Float GetRootAsStepKeyFrameData_Float(ByteBuffer _bb, StepKeyFrameData_Float obj) { return (obj.__init(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public StepKeyFrameData_Float __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public int Milliseconds { get { int o = __offset(4); return o != 0 ? bb.GetInt(o + bb_pos) : (int)0; } }
  public float Value { get { int o = __offset(6); return o != 0 ? bb.GetFloat(o + bb_pos) : (float)0; } }

  public static Offset<StepKeyFrameData_Float> CreateStepKeyFrameData_Float(FlatBufferBuilder builder,
      int milliseconds = 0,
      float value = 0) {
    builder.StartObject(2);
    StepKeyFrameData_Float.AddValue(builder, value);
    StepKeyFrameData_Float.AddMilliseconds(builder, milliseconds);
    return StepKeyFrameData_Float.EndStepKeyFrameData_Float(builder);
  }

  public static void StartStepKeyFrameData_Float(FlatBufferBuilder builder) { builder.StartObject(2); }
  public static void AddMilliseconds(FlatBufferBuilder builder, int milliseconds) { builder.AddInt(0, milliseconds, 0); }
  public static void AddValue(FlatBufferBuilder builder, float value) { builder.AddFloat(1, value, 0); }
  public static Offset<StepKeyFrameData_Float> EndStepKeyFrameData_Float(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<StepKeyFrameData_Float>(o);
  }
};


}
