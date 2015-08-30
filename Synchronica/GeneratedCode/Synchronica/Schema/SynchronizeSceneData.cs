// automatically generated, do not modify

namespace Synchronica.Schema
{

using FlatBuffers;

public sealed class SynchronizeSceneData : Table {
  public static SynchronizeSceneData GetRootAsSynchronizeSceneData(ByteBuffer _bb) { return GetRootAsSynchronizeSceneData(_bb, new SynchronizeSceneData()); }
  public static SynchronizeSceneData GetRootAsSynchronizeSceneData(ByteBuffer _bb, SynchronizeSceneData obj) { return (obj.__init(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public SynchronizeSceneData __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public int StartTime { get { int o = __offset(4); return o != 0 ? bb.GetInt(o + bb_pos) : (int)0; } }
  public int EndTime { get { int o = __offset(6); return o != 0 ? bb.GetInt(o + bb_pos) : (int)0; } }
  public GameObjectData GetObjects(int j) { return GetObjects(new GameObjectData(), j); }
  public GameObjectData GetObjects(GameObjectData obj, int j) { int o = __offset(8); return o != 0 ? obj.__init(__indirect(__vector(o) + j * 4), bb) : null; }
  public int ObjectsLength { get { int o = __offset(8); return o != 0 ? __vector_len(o) : 0; } }

  public static Offset<SynchronizeSceneData> CreateSynchronizeSceneData(FlatBufferBuilder builder,
      int startTime = 0,
      int endTime = 0,
      VectorOffset objects = default(VectorOffset)) {
    builder.StartObject(3);
    SynchronizeSceneData.AddObjects(builder, objects);
    SynchronizeSceneData.AddEndTime(builder, endTime);
    SynchronizeSceneData.AddStartTime(builder, startTime);
    return SynchronizeSceneData.EndSynchronizeSceneData(builder);
  }

  public static void StartSynchronizeSceneData(FlatBufferBuilder builder) { builder.StartObject(3); }
  public static void AddStartTime(FlatBufferBuilder builder, int startTime) { builder.AddInt(0, startTime, 0); }
  public static void AddEndTime(FlatBufferBuilder builder, int endTime) { builder.AddInt(1, endTime, 0); }
  public static void AddObjects(FlatBufferBuilder builder, VectorOffset objectsOffset) { builder.AddOffset(2, objectsOffset.Value, 0); }
  public static VectorOffset CreateObjectsVector(FlatBufferBuilder builder, Offset<GameObjectData>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static void StartObjectsVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<SynchronizeSceneData> EndSynchronizeSceneData(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<SynchronizeSceneData>(o);
  }
  public static void FinishSynchronizeSceneDataBuffer(FlatBufferBuilder builder, Offset<SynchronizeSceneData> offset) { builder.Finish(offset.Value); }
};


}
