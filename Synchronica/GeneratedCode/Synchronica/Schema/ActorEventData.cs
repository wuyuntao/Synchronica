// automatically generated, do not modify

namespace Synchronica.Schema
{

using FlatBuffers;

public sealed class ActorEventData : Table {
  public static ActorEventData GetRootAsActorEventData(ByteBuffer _bb) { return GetRootAsActorEventData(_bb, new ActorEventData()); }
  public static ActorEventData GetRootAsActorEventData(ByteBuffer _bb, ActorEventData obj) { return (obj.__init(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public ActorEventData __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public ActorEventType Type { get { int o = __offset(4); return o != 0 ? (ActorEventType)bb.GetSbyte(o + bb_pos) : (ActorEventType)0; } }
  public int Time { get { int o = __offset(6); return o != 0 ? bb.GetInt(o + bb_pos) : (int)0; } }

  public static Offset<ActorEventData> CreateActorEventData(FlatBufferBuilder builder,
      ActorEventType type = (ActorEventType)0,
      int time = 0) {
    builder.StartObject(2);
    ActorEventData.AddTime(builder, time);
    ActorEventData.AddType(builder, type);
    return ActorEventData.EndActorEventData(builder);
  }

  public static void StartActorEventData(FlatBufferBuilder builder) { builder.StartObject(2); }
  public static void AddType(FlatBufferBuilder builder, ActorEventType type) { builder.AddSbyte(0, (sbyte)(type), 0); }
  public static void AddTime(FlatBufferBuilder builder, int time) { builder.AddInt(1, time, 0); }
  public static Offset<ActorEventData> EndActorEventData(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<ActorEventData>(o);
  }
};


}
