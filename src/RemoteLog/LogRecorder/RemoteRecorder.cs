﻿
#region

using Agebull.Common.Logging;
using Agebull.ZeroNet.Core;
using Newtonsoft.Json;

#endregion

namespace Agebull.ZeroNet.LogRecorder
{
    /// <summary>
    ///   远程记录器
    /// </summary>
    public sealed class RemoteRecorder : ILogRecorder
    {
        /// <summary>
        ///   初始化
        /// </summary>
        public void Initialize()
        {
        }

        /// <summary>
        ///   停止
        /// </summary>
        public void Shutdown()
        {
        }
        
        /// <summary>
        ///   记录日志
        /// </summary>
        /// <param name="info"> 日志消息 </param>
        public void RecordLog(RecordInfo info)
        {
            using (LogRecordingScope.CreateScope())
            {
                Publisher.Publish("RemoteLog",info.TypeName,JsonConvert.SerializeObject( info ));
            }
        }
    }
}
