namespace Project
{
    public interface IDataStorage<T> where T: new()
    {
        void Save(string path, T data);
        T Load(string path);
    }
}
