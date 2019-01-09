﻿using System.Collections.Generic;
using System.Text;
using Agebull.Common.DataModel;
using Newtonsoft.Json;

namespace Agebull.ZeroNet.ZeroApi
{

    /// <summary>
    /// Api调用节点
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class ApiCallItem
    {
        /// <summary>
        /// 全局ID
        /// </summary>
        internal List<IApiHandler> Handlers { get; set; }

        /// <summary>
        /// 站点请求ID
        /// </summary>
        public string LocalId { get; set; }

        /// <summary>
        /// 调用方的全局ID（上一个）
        /// </summary>
        public string CallId { get; set; }

        /// <summary>
        /// 本次调用全局ID
        /// </summary>
        public string GlobalId { get; set; }

        /// <summary>
        /// 发生站点
        /// </summary>
        public string StationName { get; set; }

        /// <summary>
        /// 调用方的站点类型
        /// </summary>
        public string StationType { get; set; }

        /// <summary>
        /// 请求者
        /// </summary>
        [JsonIgnore]
        public byte[] First { get; set; }

        /// <summary>
        /// 请求者
        /// </summary>
        [JsonIgnore]
        public byte[] Caller => First;

        /// <summary>
        /// 请求者
        /// </summary>
        [JsonProperty("Caller")]
        public string CallerName => First == null || First.Length == 0 ? "" : Encoding.ASCII.GetString(First).Trim('\0');

        /// <summary>
        /// 广播标题
        /// </summary>
        [JsonProperty("Title")]
        public string Title => First == null || First.Length == 0 ? "" : Encoding.ASCII.GetString(First).Trim('\0');

        /// <summary>
        /// 请求标识
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// 请求者
        /// </summary>
        public string Requester { get; set; }

        /// <summary>
        /// 命令
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// API名称
        /// </summary>
        public string ApiName => Command;

        /// <summary>
        ///  原始上下文的JSO内容
        /// </summary>
        public string ContextJson { get; set; }

        /// <summary>
        /// 请求参数
        /// </summary>
        public string Argument { get; set; }

        /// <summary>
        /// 请求参数字典
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 返回
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// 执行状态
        /// </summary>
        public ZeroOperatorStatus Status { get; set; }

        /// <summary>
        /// 消息消息
        /// </summary>
        public string StatusMessage { get; set; }

        /// <summary>
        /// 原样帧
        /// </summary>
        [JsonIgnore]
        public List<NameValue<byte, byte[]>> Frames { get; } = new List<NameValue<byte, byte[]>>();

        /// <summary>
        /// 原样帧
        /// </summary>
        public Dictionary<byte, byte[]> Originals { get; } = new Dictionary<byte, byte[]>();

    }
}