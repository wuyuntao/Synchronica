// automatically generated, do not modify

namespace Synchronica.Examples.Schema
{

using FlatBuffers;

public sealed class InputRequest : Table {
  public static InputRequest GetRootAsInputRequest(ByteBuffer _bb) { return GetRootAsInputRequest(_bb, new InputRequest()); }
  public static InputRequest GetRootAsInputRequest(ByteBuffer _bb, InputRequest obj) { return (obj.__init(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public InputRequest __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public int Milliseconds { get { int o = __offset(4); return o != 0 ? bb.GetInt(o + bb_pos) : (int)0; } }
  public Command Command { get { int o = __offset(6); return o != 0 ? (Command)bb.GetInt(o + bb_pos) : (Command)0; } }

  public static Offset<InputRequest> CreateInputRequest(FlatBufferBuilder builder,
      int milliseconds = 0,
      Command command = (Command)0) {
    builder.StartObject(2);
    InputRequest.AddCommand(builder, command);
    InputRequest.AddMilliseconds(builder, milliseconds);
    return InputRequest.EndInputRequest(builder);
  }

  public static void StartInputRequest(FlatBufferBuilder builder) { builder.StartObject(2); }
  public static void AddMilliseconds(FlatBufferBuilder builder, int milliseconds) { builder.AddInt(0, milliseconds, 0); }
  public static void AddCommand(FlatBufferBuilder builder, Command command) { builder.AddInt(1, (int)(command), 0); }
  public static Offset<InputRequest> EndInputRequest(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<InputRequest>(o);
  }
};


}
