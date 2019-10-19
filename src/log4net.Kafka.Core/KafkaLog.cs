using Newtonsoft.Json;

namespace log4net.Kafka.Core
{
    /// <summary>
    /// 日志模板
    /// </summary>
    public class KafkaLog
    {
        /// <summary>
        /// 记录日志时的时间
        /// </summary>
        [JsonProperty("log_timestamp")]
        public long LogTimestamp { get; set; }

        /// <summary>
        /// 应用id
        /// </summary>
        [JsonProperty("app_id")]
        public string AppId { get; set; }

        /// <summary>
        /// 主机名
        /// </summary>
        [JsonProperty("host_name")]
        public string HostName { get; set; }

        /// <summary>
        /// 日志级别
        /// </summary>
        [JsonProperty("level")]
        public string Level { get; set; }

        /// <summary>
        /// 日志名
        /// </summary>
        [JsonProperty("logger_name")]
        public string LoggerName { get; set; }

        /// <summary>
        /// 日志信息
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        [JsonProperty("exception")]
        public KafkaLogException Exception { get; set; }
    }

    /// <summary>
    /// 异常信息实体
    /// </summary>
    public class KafkaLogException
    {
        /// <summary>
        /// 类名
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// 堆栈信息
        /// </summary>
        [JsonProperty("stack_trace")]
        public string StackTrace { get; set; }
    }
}
