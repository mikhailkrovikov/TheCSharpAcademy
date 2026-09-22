namespace Habit.Tracker
{
    public class Record
    {
        public int Id { get; set; }
        public  int Count { get; set; }
        public  DateTime DateTime { get; set; }

        public override string ToString()
        {
            return $"{Id}: {DateTime:dd-MM-yy} {Count}";
        }
    }
}
