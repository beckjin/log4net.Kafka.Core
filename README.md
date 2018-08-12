
log4net.Kafka.Core

==========

## Quick Start

### Step 1: Install log4net.Kafka.Core package

```
Install-Package log4net.Kafka.Core
```

### Step 2: Configure log4net sections

```xml
<?xml version="1.0" encoding="utf-8" ?>
<log4net>
  <appender name="KafkaAppender" type="log4net.Kafka.Core.KafkaAppender, log4net.Kafka.Core">
    <KafkaSettings>
      <broker value="127.0.0.1:9092" />
      <topic value="topic.appname.log" />
    </KafkaSettings>
    <layout type="log4net.Kafka.Core.KafkaLogLayout,log4net.Kafka.Core" >
      <appid value="appid" />
    </layout>
  </appender>
  <root>
    <level value="ALL"/>
    <appender-ref ref="KafkaAppender" />
  </root>
</log4net>
```
