using System;

public interface IKafkaProducer : IDisposable
{
    /// <summary>
    /// 釋出訊息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="data"></param>
    /// <param name="operateType"></param>
    /// <returns></returns>
    bool Produce<T>(string key, T data, int operateType) where T : class;
}