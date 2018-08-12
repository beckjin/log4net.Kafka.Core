using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using log4net.Appender;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace log4net.Kafka.Core
{
    public class KafkaAppender : AppenderSkeleton
    {
        /// <summary>
        /// Producer
        /// </summary>
        private Producer<Null, string> _producer;

        /// <summary>
        /// Kafka 连接地址和主题配置
        /// </summary>
        public KafkaSettings KafkaSettings { get; set; }

        public override void ActivateOptions()
        {
            base.ActivateOptions();
            Init();
        }

        protected override void OnClose()
        {
            base.OnClose();
            Dispose();
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            var message = GetMessage(loggingEvent);
            var topic = KafkaSettings.Topic;
            _producer.ProduceAsync(topic, null, message);
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
                    var config = new Dictionary<string, object>
                    {
                        { "bootstrap.servers", KafkaSettings.Broker }
                    };
                    _producer = new Producer<Null, string>(config, null, new StringSerializer(Encoding.UTF8));
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
