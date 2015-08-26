// automatically generated, do not modify

namespace Synchronica.Schema
{

using FlatBuffers;

public sealed class SynchronicaData : Table {
  public static SynchronicaData GetRootAsSynchronicaData(ByteBuffer _bb) { return GetRootAsSynchronicaData(_bb, new SynchronicaData()); }
  public static SynchronicaData GetRootAsSynchronicaData(ByteBuffer _bb, SynchronicaData obj) { return (obj.__init(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public SynchronicaData __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public SceneData GetScenes(int j) { return GetScenes(new SceneData(), j); }
  public SceneData GetScenes(SceneData obj, int j) { int o = __offset(4); return o != 0 ? obj.__init(__indirect(__vector(o) + j * 4), bb) : null; }
  public int ScenesLength { get { int o = __offset(4); return o != 0 ? __vector_len(o) : 0; } }

  public static Offset<SynchronicaData> CreateSynchronicaData(FlatBufferBuilder builder,
      VectorOffset scenes = default(VectorOffset)) {
    builder.StartObject(1);
    SynchronicaData.AddScenes(builder, scenes);
    return SynchronicaData.EndSynchronicaData(builder);
  }

  public static void StartSynchronicaData(FlatBufferBuilder builder) { builder.StartObject(1); }
  public static void AddScenes(FlatBufferBuilder builder, VectorOffset scenesOffset) { builder.AddOffset(0, scenesOffset.Value, 0); }
  public static VectorOffset CreateScenesVector(FlatBufferBuilder builder, Offset<SceneData>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static void StartScenesVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<SynchronicaData> EndSynchronicaData(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<SynchronicaData>(o);
  }
  public static void FinishSynchronicaDataBuffer(FlatBufferBuilder builder, Offset<SynchronicaData> offset) { builder.Finish(offset.Value); }
};


}
