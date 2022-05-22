using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Core.Data
{
    public class DataProvider<TData>
    {
        private string _dataPath;
        
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
            using var reader = new StreamReader(_dataPath);
            var json = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<TData>(json);
        }
    }
}