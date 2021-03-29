using System.IO;
using UnityEngine;

namespace Project
{
    public sealed class ItemRepository
    {
        private IDataStorage<ItemSavedData> dataStorage;
        private string directoryName;

        public ItemRepository(string directoryName_)
        {
            dataStorage = new JsonStorage<ItemSavedData>();
            directoryName = Path.Combine(Application.persistentDataPath, directoryName_);
            if(!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
        }

        public void Save(string name, Item item)
        {
            dataStorage.Save(Path.Combine(directoryName, name), item.GetSavedData());
        }

        public void Load(string name, Item item)
        {
            ItemSavedData data = dataStorage.Load(Path.Combine(directoryName, name));
            item.LoadSavedData(data);
        }
    }
}
