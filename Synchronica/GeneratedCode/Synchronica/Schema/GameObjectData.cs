// automatically generated, do not modify

namespace Synchronica.Schema
{

using FlatBuffers;

public sealed class GameObjectData : Table {
  public static GameObjectData GetRootAsGameObjectData(ByteBuffer _bb) { return GetRootAsGameObjectData(_bb, new GameObjectData()); }
  public static GameObjectData GetRootAsGameObjectData(ByteBuffer _bb, GameObjectData obj) { return (obj.__init(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public GameObjectData __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public int Id { get { int o = __offset(4); return o != 0 ? bb.GetInt(o + bb_pos) : (int)0; } }
  public PropertyData GetProperties(int j) { return GetProperties(new PropertyData(), j); }
  public PropertyData GetProperties(PropertyData obj, int j) { int o = __offset(6); return o != 0 ? obj.__init(__indirect(__vector(o) + j * 4), bb) : null; }
  public int PropertiesLength { get { int o = __offset(6); return o != 0 ? __vector_len(o) : 0; } }

  public static Offset<GameObjectData> CreateGameObjectData(FlatBufferBuilder builder,
      int id = 0,
      VectorOffset properties = default(VectorOffset)) {
    builder.StartObject(2);
    GameObjectData.AddProperties(builder, properties);
    GameObjectData.AddId(builder, id);
    return GameObjectData.EndGameObjectData(builder);
  }

  public static void StartGameObjectData(FlatBufferBuilder builder) { builder.StartObject(2); }
  public static void AddId(FlatBufferBuilder builder, int id) { builder.AddInt(0, id, 0); }
  public static void AddProperties(FlatBufferBuilder builder, VectorOffset propertiesOffset) { builder.AddOffset(1, propertiesOffset.Value, 0); }
  public static VectorOffset CreatePropertiesVector(FlatBufferBuilder builder, Offset<PropertyData>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static void StartPropertiesVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<GameObjectData> EndGameObjectData(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<GameObjectData>(o);
  }
};


}
