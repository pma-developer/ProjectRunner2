using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Core.Data
{
    public class DataProvider<TData>
    {
        private readonly string _dataPath;
        
        public DataProvider(string dataPath)
        {
            _dataPath = dataPath;
        }

        public void Save(TData data)
        {
            var json = JsonConvert.SerializeObject(data);

            using var writer = new StreamWriter(_dataPath, false, Encoding.UTF8);
            writer.Write(json);
        }

        public TData Load()
        {
            if (File.Exists(_dataPath) == false) return default;
            
            using var reader = new StreamReader(_dataPath);
            var json = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<TData>(json);

        }
    }
}