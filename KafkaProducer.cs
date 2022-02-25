using Confluent.Kafka;
using Newtonsoft.Json;
using System;

public class KafkaProducer : IKafkaProducer
{
    private bool disposeHasBeenCalled = false;
    private readonly object disposeHasBeenCalledLockObj = new object();

    private readonly IProducer<string, string> _producer;
    private readonly string _topic;

    /// <summary>
    /// 建構函式，初始化配置
    /// </summary>
    /// <param name="config">配置引數</param>
    /// <param name="topic">主題名稱</param>
    public KafkaProducer(ProducerConfig config, string topic)
    {
        _producer = new ProducerBuilder<string, string>(config).Build();
        _topic = topic;
    }

    /// <summary>
    /// 釋出訊息
    /// </summary>
    /// <typeparam name="T">資料實體</typeparam>
    /// <param name="key">資料key,partition分割槽會根據key</param>
    /// <param name="data">資料</param>
    /// <param name="operateType">操作型別[增、刪、改等不同型別]</param>
    /// <returns></returns>
    public bool Produce<T>(string key, T data, int operateType) where T : class
    {
        var obj = JsonConvert.SerializeObject(new
        {
            Type = operateType,
            Data = data
        });

        try
        {
            var result = _producer.ProduceAsync(_topic, new Message<string, string>
            {
                Key = key,
                Value = obj
            }).ConfigureAwait(false).GetAwaiter().GetResult();

#if DEBUG

            Console.WriteLine($"Topic: {result.Topic} Partition: {result.Partition} Offset: {result.Offset}");
#endif
            return true;

        }
        catch (ProduceException<string, string> e)
        {
            Console.WriteLine($"Delivery failed: {e.Error.Reason}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Delivery failed: {e.Message}");
        }

        return false;
    }

    /// <summary>
    /// 釋放
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Dispose
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        lock (disposeHasBeenCalledLockObj)
        {
            if (disposeHasBeenCalled) { return; }
            disposeHasBeenCalled = true;
        }

        if (disposing)
        {
            _producer?.Dispose();
        }
    }
}