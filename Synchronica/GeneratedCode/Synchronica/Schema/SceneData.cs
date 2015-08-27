// automatically generated, do not modify

namespace Synchronica.Schema
{

using FlatBuffers;

public sealed class SceneData : Table {
  public static SceneData GetRootAsSceneData(ByteBuffer _bb) { return GetRootAsSceneData(_bb, new SceneData()); }
  public static SceneData GetRootAsSceneData(ByteBuffer _bb, SceneData obj) { return (obj.__init(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public SceneData __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public int StartMilliseconds { get { int o = __offset(4); return o != 0 ? bb.GetInt(o + bb_pos) : (int)0; } }
  public int EndMilliseconds { get { int o = __offset(6); return o != 0 ? bb.GetInt(o + bb_pos) : (int)0; } }
  public GameObjectData GetObjects(int j) { return GetObjects(new GameObjectData(), j); }
  public GameObjectData GetObjects(GameObjectData obj, int j) { int o = __offset(8); return o != 0 ? obj.__init(__indirect(__vector(o) + j * 4), bb) : null; }
  public int ObjectsLength { get { int o = __offset(8); return o != 0 ? __vector_len(o) : 0; } }

  public static Offset<SceneData> CreateSceneData(FlatBufferBuilder builder,
      int startMilliseconds = 0,
      int endMilliseconds = 0,
      VectorOffset objects = default(VectorOffset)) {
    builder.StartObject(3);
    SceneData.AddObjects(builder, objects);
    SceneData.AddEndMilliseconds(builder, endMilliseconds);
    SceneData.AddStartMilliseconds(builder, startMilliseconds);
    return SceneData.EndSceneData(builder);
  }

  public static void StartSceneData(FlatBufferBuilder builder) { builder.StartObject(3); }
  public static void AddStartMilliseconds(FlatBufferBuilder builder, int startMilliseconds) { builder.AddInt(0, startMilliseconds, 0); }
  public static void AddEndMilliseconds(FlatBufferBuilder builder, int endMilliseconds) { builder.AddInt(1, endMilliseconds, 0); }
  public static void AddObjects(FlatBufferBuilder builder, VectorOffset objectsOffset) { builder.AddOffset(2, objectsOffset.Value, 0); }
  public static VectorOffset CreateObjectsVector(FlatBufferBuilder builder, Offset<GameObjectData>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static void StartObjectsVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<SceneData> EndSceneData(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<SceneData>(o);
  }
};


}
