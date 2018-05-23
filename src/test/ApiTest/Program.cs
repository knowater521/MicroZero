using System.Threading.Tasks;
using Agebull.Common.Logging;
using Agebull.ZeroNet.Core;
using Agebull.ZeroNet.Log;

namespace ApiTest
{
    partial class Program
    {
        static void Main(string[] args)
        {
            StationConsole.WriteLine("Hello ZeroNet");
            ZeroApplication.Initialize();
            LogRecorder.Initialize(new RemoteRecorder());
            ZeroApplication.Discove();
            ZeroApplication.RunAwaite();
            LogRecorder.Shutdown();
        }
    }
}
