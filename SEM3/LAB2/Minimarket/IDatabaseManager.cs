namespace Minimarket
{
    public interface IDatabaseManager<T>
    {
        void AddItem(T item);
        void DeleteItem(T item);
        T? Get(string field, string value);
    }
}
