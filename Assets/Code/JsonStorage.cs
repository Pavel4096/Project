using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace Project
{
    public sealed class JsonStorage<T> : IDataStorage<T> where T: new()
    {
        public void Save(string path, T data)
        {
            string dataString = JsonUtility.ToJson(data);
            File.WriteAllText(path, dataString, Encoding.UTF8);
        }

        public T Load(string path)
        {
            string dataString = File.ReadAllText(path, Encoding.UTF8);
            return JsonUtility.FromJson<T>(dataString);
        }
    }
}
