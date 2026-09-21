namespace Habit.Tracker
{
    public interface IDataProvider<TElement>
    {
        bool CreateDatabase();
        bool Create(TElement record);
        List<TElement> ReadAllData();
        bool Update(int id, TElement record);
        bool Delete(int id);
    }
}
