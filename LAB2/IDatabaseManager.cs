namespace Minimarket
{
    public interface IDatabaseManager<T>
    {
        void CreateNewTable(string tableName);
        void DeleteTable();
        void AddItem(T item);
        void DeleteItem(T item);
        T? Get(string field, string value);
    }
}
