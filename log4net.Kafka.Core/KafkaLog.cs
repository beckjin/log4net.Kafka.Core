namespace log4net.Kafka.Core
{
    /// <summary>
    /// 日志模板
    /// </summary>
    public class KafkaLog
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        public long Timestamp { get; set; }

        /// <summary>
        /// 应用id
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 主机名
        /// </summary>
        public string HostName { get; set; }

        /// <summary>
        /// 日志级别
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// 日志名
        /// </summary>
        public string LoggerName { get; set; }

        /// <summary>
        /// 经过ObjectRender处理后的日志信息
        /// </summary>
        public string RenderedMessage { get; set; }

        /// <summary>
        /// 异常
        /// </summary>
        public KafkaLogException Exception { get; set; }
    }

    public class KafkaLogException
    {
        /// <summary>
        /// 类名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 堆栈信息
        /// </summary>
        public string StackTrace { get; set; }
    }
}
