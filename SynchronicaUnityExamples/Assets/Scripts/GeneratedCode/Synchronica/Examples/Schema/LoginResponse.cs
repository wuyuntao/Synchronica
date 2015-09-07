// automatically generated, do not modify

namespace Synchronica.Examples.Schema
{

using FlatBuffers;

public sealed class LoginResponse : Table {
  public static LoginResponse GetRootAsLoginResponse(ByteBuffer _bb) { return GetRootAsLoginResponse(_bb, new LoginResponse()); }
  public static LoginResponse GetRootAsLoginResponse(ByteBuffer _bb, LoginResponse obj) { return (obj.__init(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public LoginResponse __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public int ObjectId { get { int o = __offset(4); return o != 0 ? bb.GetInt(o + bb_pos) : (int)0; } }

  public static Offset<LoginResponse> CreateLoginResponse(FlatBufferBuilder builder,
      int objectId = 0) {
    builder.StartObject(1);
    LoginResponse.AddObjectId(builder, objectId);
    return LoginResponse.EndLoginResponse(builder);
  }

  public static void StartLoginResponse(FlatBufferBuilder builder) { builder.StartObject(1); }
  public static void AddObjectId(FlatBufferBuilder builder, int objectId) { builder.AddInt(0, objectId, 0); }
  public static Offset<LoginResponse> EndLoginResponse(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<LoginResponse>(o);
  }
  public static void FinishLoginResponseBuffer(FlatBufferBuilder builder, Offset<LoginResponse> offset) { builder.Finish(offset.Value); }
};


}
