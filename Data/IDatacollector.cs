using Models;

namespace Data
{
    public interface IDatacollector
    {
        IEnumerable<Day> getData();
        void Add (Day day);
        int Commit();
        void delete_withCondition(string condition);
    }
}