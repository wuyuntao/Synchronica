// automatically generated, do not modify

namespace Synchronica.Schema
{

using FlatBuffers;

public sealed class Synchronization : Table {
  public static Synchronization GetRootAsSynchronization(ByteBuffer _bb) { return GetRootAsSynchronization(_bb, new Synchronization()); }
  public static Synchronization GetRootAsSynchronization(ByteBuffer _bb, Synchronization obj) { return (obj.__init(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public Synchronization __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public SynchronicaData Data { get { return GetData(new SynchronicaData()); } }
  public SynchronicaData GetData(SynchronicaData obj) { int o = __offset(4); return o != 0 ? obj.__init(__indirect(o + bb_pos), bb) : null; }

  public static Offset<Synchronization> CreateSynchronization(FlatBufferBuilder builder,
      Offset<SynchronicaData> data = default(Offset<SynchronicaData>)) {
    builder.StartObject(1);
    Synchronization.AddData(builder, data);
    return Synchronization.EndSynchronization(builder);
  }

  public static void StartSynchronization(FlatBufferBuilder builder) { builder.StartObject(1); }
  public static void AddData(FlatBufferBuilder builder, Offset<SynchronicaData> dataOffset) { builder.AddOffset(0, dataOffset.Value, 0); }
  public static Offset<Synchronization> EndSynchronization(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<Synchronization>(o);
  }
};


}
