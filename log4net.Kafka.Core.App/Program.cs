using Microsoft.Extensions.Logging;
using System;

namespace log4net.Kafka.Core.App
{
    class Program
    {
        static void Main(string[] args)
        {
            ILogger logger = new LoggerFactory()
                            .AddLog4Net()
                            .CreateLogger(nameof(Program));

            logger.LogInformation("这是一条普通日志");

            logger.LogDebug("这是一条 Debug 日志");

            logger.LogWarning("这是一条警告日志");

            logger.LogError("这是一条错误日志");

            try
            {
                throw new DivideByZeroException();
            }
            catch (Exception ex)
            {
                logger.LogTrace(ex, "这是一条异常日志");
            }

            Console.ReadKey();
        }
    }
}
