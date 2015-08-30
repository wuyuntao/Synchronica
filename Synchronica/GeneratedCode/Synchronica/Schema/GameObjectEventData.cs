// automatically generated, do not modify

namespace Synchronica.Schema
{

using FlatBuffers;

public sealed class GameObjectEventData : Table {
  public static GameObjectEventData GetRootAsGameObjectEventData(ByteBuffer _bb) { return GetRootAsGameObjectEventData(_bb, new GameObjectEventData()); }
  public static GameObjectEventData GetRootAsGameObjectEventData(ByteBuffer _bb, GameObjectEventData obj) { return (obj.__init(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public GameObjectEventData __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public GameObjectEventType Type { get { int o = __offset(4); return o != 0 ? (GameObjectEventType)bb.GetSbyte(o + bb_pos) : (GameObjectEventType)0; } }
  public int Time { get { int o = __offset(6); return o != 0 ? bb.GetInt(o + bb_pos) : (int)0; } }

  public static Offset<GameObjectEventData> CreateGameObjectEventData(FlatBufferBuilder builder,
      GameObjectEventType type = (GameObjectEventType)0,
      int time = 0) {
    builder.StartObject(2);
    GameObjectEventData.AddTime(builder, time);
    GameObjectEventData.AddType(builder, type);
    return GameObjectEventData.EndGameObjectEventData(builder);
  }

  public static void StartGameObjectEventData(FlatBufferBuilder builder) { builder.StartObject(2); }
  public static void AddType(FlatBufferBuilder builder, GameObjectEventType type) { builder.AddSbyte(0, (sbyte)(type), 0); }
  public static void AddTime(FlatBufferBuilder builder, int time) { builder.AddInt(1, time, 0); }
  public static Offset<GameObjectEventData> EndGameObjectEventData(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<GameObjectEventData>(o);
  }
};


}
