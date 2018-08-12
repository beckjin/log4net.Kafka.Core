namespace log4net.Kafka.Core
{
    public class KafkaSettings
    {
        /// <summary>
        /// Kafka Broker 地址，多个使用 "," 分隔
        /// </summary>
        public string Broker { get; set; }

        /// <summary>
        /// 日志主题名
        /// </summary>
        public string Topic { get; set; }
    }
}
