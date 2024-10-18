using Newtonsoft.Json;
using System.Diagnostics;

namespace Otus.ReflectionTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serializer = new CsvSerializer();
            F original = F.Get();
            int iterations = 100000;

            // Замер времени сериализации с использованием рефлексии
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string serialized = string.Empty;

            for (int i = 0; i < iterations; i++)
            {
                serialized = serializer.SerializeToCsv(original);
            }
            stopwatch.Stop();
            long serializationTime = stopwatch.ElapsedMilliseconds;

            Console.WriteLine($"Сериализованная строка (рефлексия): {serialized}");
            Console.WriteLine($"Время сериализации {iterations} раз (рефлексия): {serializationTime} мс");

            // Десериализация с использованием рефлексии
            stopwatch.Restart();
            F deserialized = serializer.DeserializeFromCsv<F>(serialized);
            stopwatch.Stop();
            long deserializationTime = stopwatch.ElapsedMilliseconds;

            Console.WriteLine($"Время на десериализацию (рефлексия): {deserializationTime} мс");

            // Стандартный механизм (Newtonsoft.Json)
            stopwatch.Restart();
            string jsonSerialized = JsonConvert.SerializeObject(original);
            stopwatch.Stop();
            long jsonSerializationTime = stopwatch.ElapsedMilliseconds;

            Console.WriteLine($"JSON Сериализованная строка: {jsonSerialized}");
            Console.WriteLine($"Время JSON-сериализации {iterations} раз: {jsonSerializationTime} мс");

            // JSON десериализация
            stopwatch.Restart();
            F jsonDeserialized = JsonConvert.DeserializeObject<F>(jsonSerialized);
            stopwatch.Stop();
            long jsonDeserializationTime = stopwatch.ElapsedMilliseconds;

            Console.WriteLine($"Время JSON-десериализации: {jsonDeserializationTime} мс");
        }
    }
}
