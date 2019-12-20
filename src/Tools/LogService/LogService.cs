using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agebull.Common.Logging;
using Agebull.Common.Tson;
using Agebull.MicroZero.PubSub;
using Agebull.MicroZero.ZeroApis;
using MicroZero.LogService;
using Newtonsoft.Json;

namespace Agebull.MicroZero.LogService
{
    /// <inheritdoc />
    /// <summary>
    /// 日志服务
    /// </summary>
    public class RemoteLogStation : SubStation
    {
        /// <summary>
        ///     刷新
        /// </summary>
        public RemoteLogStation()
        {
            Name = "RemoteLog";
            StationName = "RemoteLog";
            //Subscribe = "Logs";
        }

        private ZeroLogRecorder _recorder;

        protected override void OnLoopBegin()
        {
            _recorder = new ZeroLogRecorder();
            _recorder.Initialize();
            base.OnLoopBegin();
        }

        protected override Task OnLoopComplete()
        {
            _recorder.Shutdown();
            _recorder = null;
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public override async Task Handle(PublishItem args)
        {
            try
            {
                if (args.Content != null)
                {
                    var infos = JsonConvert.DeserializeObject<List<RecordInfo>>(args.Content);
                    await _recorder.RecordLog(infos);
                    return;
                }

                if (args.Tson == null || args.Tson[0] == 0 || args.Tson[0] > 2)
                    return;
                ITsonDeserializer serializer = args.Tson[0] == 1
                    ? new TsonDeserializerV1(args.Tson) as ITsonDeserializer
                    : new TsonDeserializer(args.Tson);

                using (serializer)
                {
                    switch (serializer.DataType)
                    {
                        case TsonDataType.Object:
                            RecordInfo info = new RecordInfo();
                            RecordInfoTson.FromTson(serializer, info);
                            await _recorder.RecordLog(info);
                            break;
                        case TsonDataType.Array:
                            serializer.ReadType();
                            int size = serializer.ReadLen();
                            List<RecordInfo> infos = new List<RecordInfo>();
                            for (int idx = 0; !serializer.IsBad && idx < size; idx++)
                            {
                                info = new RecordInfo();
                                serializer.Begin(out _);
                                RecordInfoTson.FromTson(serializer, info);
                                serializer.End();
                                infos.Add(info);
                            }
                            await _recorder.RecordLog(infos);
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                LogRecorderX.Exception(e);
            }
        }

    }
}