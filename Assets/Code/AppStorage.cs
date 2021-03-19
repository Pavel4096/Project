using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;

namespace Project
{
    public sealed class AppStorage
    {
        private static Dictionary<string, Entry> dict;

        static AppStorage()
        {
            Application.quitting += Save;
            dict = new Dictionary<string, Entry>();
            Load();
        }

        public static bool KeyExists(string name)
        {
            return dict.ContainsKey(name);
        }

        public static void AddEntry(string name, object data)
        {
            EntryType type = EntryType.Unknown;

            switch(data)
            {
                case string data2:
                    type = EntryType.String;
                    break;
                case float data2:
                    type = EntryType.Float;
                    break;
            }
            dict[name] = new Entry(name, type, data);
        }

        public static object GetEntry(string name)
        {
            Entry entry;

            if(dict.TryGetValue(name, out entry))
            {
                return entry.data;
            }
            else
                throw new KeyNotFoundException($"There is no element with name: \"{name}\".");
        }

        public static void AddFloat(string name, float data)
        {
            AddEntry(name, data);
        }

        public static float GetFloat(string name)
        {
            Entry entry;

            if(dict.TryGetValue(name, out entry))
            {
                if(entry.type == EntryType.Float)
                    return (float)entry.data;
                else
                    throw new InvalidOperationException($"There is no element with name \"{name}\" and type \"float\".");
            }
            else
                throw new KeyNotFoundException($"There is no element with name: \"{name}\".");
        }

        public static void Save()
        {
            using(var fs = new FileStream(Path.Combine(Application.persistentDataPath, "data.xml"), FileMode.Create, FileAccess.Write))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Entry>));
                List<Entry> list = new List<Entry>();

                foreach(var pair in dict)
                {
                    list.Add(pair.Value);
                }
                serializer.Serialize(fs, list);
            }
        }

        public static void Load()
        {
            try
            {
                using(var fs = new FileStream(Path.Combine(Application.persistentDataPath, "data.xml"), FileMode.Open, FileAccess.Read))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Entry>));
                    List<Entry> list = serializer.Deserialize(fs) as List<Entry>;

                    if(list != null)
                        foreach(var item in list)
                        {
                            dict.Add(item.name, item);
                        }
                }
            }
            catch(FileNotFoundException) {}
        }

        public class Entry
        {
            public string name;
            public EntryType type;
            public object data;

            public Entry()
            {

            }
            
            public Entry(string name_, EntryType type_, object data_)
            {
                name = name_;
                type = type_;
                data = data_;
            }
        }

        public enum EntryType
        {
            Unknown = 0,
            String = 1,
            Float = 2
        }
    }
}
