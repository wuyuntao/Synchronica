// automatically generated, do not modify

namespace Synchronica.Examples.Schema
{

using FlatBuffers;

public sealed class LoginRequest : Table {
  public static LoginRequest GetRootAsLoginRequest(ByteBuffer _bb) { return GetRootAsLoginRequest(_bb, new LoginRequest()); }
  public static LoginRequest GetRootAsLoginRequest(ByteBuffer _bb, LoginRequest obj) { return (obj.__init(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public LoginRequest __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public string Name { get { int o = __offset(4); return o != 0 ? __string(o + bb_pos) : null; } }

  public static Offset<LoginRequest> CreateLoginRequest(FlatBufferBuilder builder,
      StringOffset name = default(StringOffset)) {
    builder.StartObject(1);
    LoginRequest.AddName(builder, name);
    return LoginRequest.EndLoginRequest(builder);
  }

  public static void StartLoginRequest(FlatBufferBuilder builder) { builder.StartObject(1); }
  public static void AddName(FlatBufferBuilder builder, StringOffset nameOffset) { builder.AddOffset(0, nameOffset.Value, 0); }
  public static Offset<LoginRequest> EndLoginRequest(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<LoginRequest>(o);
  }
  public static void FinishLoginRequestBuffer(FlatBufferBuilder builder, Offset<LoginRequest> offset) { builder.Finish(offset.Value); }
};


}
