using Synchronica.Tests.Simulation;

namespace Synchronica.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new VInt32Test();
            //test.TestAppendFrames();
            //test.TestRemoveFramesBefore();
            test.TestRemoveFramesAfter();
        }
    }
}
