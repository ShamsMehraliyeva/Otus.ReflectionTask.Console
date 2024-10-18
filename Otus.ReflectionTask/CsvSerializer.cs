using System.Reflection;
using System.Text;

namespace Otus.ReflectionTask
{
    public class CsvSerializer
    {
        public string SerializeToCsv<T>(T obj)
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Instance);

            StringBuilder csvBuilder = new StringBuilder();
            foreach (var prop in properties)
            {
                csvBuilder.Append(prop.GetValue(obj)?.ToString() + ",");
            }
            foreach (var field in fields)
            {
                csvBuilder.Append(field.GetValue(obj)?.ToString() + ",");
            }
            if (csvBuilder.Length > 0)
                csvBuilder.Length--;

            return csvBuilder.ToString();
        }

        public T DeserializeFromCsv<T>(string csv) where T : new()
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Instance);
            var parts = csv.Split(',');

            T obj = new T();
            int index = 0;

            foreach (var prop in properties)
            {
                if (index < parts.Length)
                {
                    prop.SetValue(obj, Convert.ChangeType(parts[index], prop.PropertyType));
                    index++;
                }
            }

            foreach (var field in fields)
            {
                if (index < parts.Length)
                {
                    field.SetValue(obj, Convert.ChangeType(parts[index], field.FieldType));
                    index++;
                }
            }

            return obj;
        }
    }
}
