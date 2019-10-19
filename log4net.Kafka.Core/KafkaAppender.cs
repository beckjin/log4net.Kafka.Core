using Confluent.Kafka;
using log4net.Appender;
using log4net.Core;
using System;
using System.IO;

namespace log4net.Kafka.Core
{
    /// <summary>
    /// log4net KafkaAppender
    /// </summary>
    public class KafkaAppender : AppenderSkeleton
    {
        /// <summary>
        /// Producer
        /// </summary>
        private IProducer<Null, string> _producer;

        /// <summary>
        /// Kafka 连接地址和主题配置
        /// </summary>
        public KafkaSettings KafkaSettings { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public override void ActivateOptions()
        {
            base.ActivateOptions();
            Init();
        }

        /// <summary>
        /// 注销
        /// </summary>
        protected override void OnClose()
        {
            base.OnClose();
            Dispose();
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="loggingEvent"></param>
        protected override void Append(LoggingEvent loggingEvent)
        {
            var message = GetMessage(loggingEvent);
            var topic = KafkaSettings.Topic;
            _producer.ProduceAsync(topic, new Message<Null, string> { Value = message });
        }

        // 根据 KafkaLogLayout 模板获取日志信息
        private string GetMessage(LoggingEvent loggingEvent)
        {
            using (var sr = new StringWriter())
            {
                Layout.Format(sr, loggingEvent);

                if (Layout.IgnoresException && loggingEvent.ExceptionObject != null)
                    sr.Write(loggingEvent.GetExceptionString());

                return sr.ToString();
            }
        }

        /// <summary>
        /// Kafka producer 初始化 
        /// </summary>
        private void Init()
        {
            try
            {
                if (KafkaSettings == null)
                    throw new LogException("KafkaSettings is missing");

                if (string.IsNullOrEmpty(KafkaSettings.Broker))
                    throw new Exception("Broker is missing");

                if (string.IsNullOrEmpty(KafkaSettings.Topic))
                    throw new Exception("Topic is missing");

                if (_producer == null)
                {
                    var config = new ProducerConfig
                    {
                        BootstrapServers = KafkaSettings.Broker
                    };
                    _producer = new ProducerBuilder<Null, string>(config).Build();
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Error("could not init producer", ex);
            }
        }

        /// <summary>
        /// 释放 producer
        /// </summary>
        private void Dispose()
        {
            try
            {
                _producer?.Dispose();
            }
            catch (Exception ex)
            {
                ErrorHandler.Error("could not dispose producer", ex);
            }
        }
    }
}
