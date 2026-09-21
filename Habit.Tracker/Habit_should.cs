using Microsoft.Data.Sqlite;
using NUnit.Framework;

namespace Habit.Tracker
{
    [TestFixture]
    public static class Habit_should
    {
        private static SQLiteDataProvider database;

        [Test]
        public static void CreateDatabaseTest()
        {
            var record = new Record
            {
                DateTime = DateTime.Now,
                Count = 10
            };
            database.Create(record);
            database.CreateDatabase();
            Assert.That(database.ReadAllData().Count, Is.EqualTo(1));
        }

        [Test]
        public static void CreateRecordTest()
        {
            var record = new Record
            {
                DateTime = new DateTime(2026, 9, 21),
                Count = 20,
            };
            var result = database.Create(record);
            var added = database.ReadAllData().Single();

            Assert.That(result, Is.True);
            Assert.That(added.Id, Is.GreaterThan(0));
            Assert.That(added.DateTime, Is.EqualTo(record.DateTime));
            Assert.That(added.Count, Is.EqualTo(record.Count));
        }

        [Test]
        public static void ReadRecordsTest()
        {
            database.Create(new Record
            {
                DateTime = new DateTime(2026, 9, 21),
                Count = 10
            });
            database.Create(new Record
            {
                DateTime = new DateTime(2026, 9, 22),
                Count = 20
            });

            var records = database.ReadAllData();
            Assert.That(database.ReadAllData().Count, Is.EqualTo(2));
            Assert.That(database.ReadAllData().Select(r => r.Count), Is.EquivalentTo(new[] { 10, 20 }));
        }

        [Test]
        public static void DeleteRecordTest()
        {
            database.Create(new Record
            {
                DateTime = new DateTime(2026, 9, 21),
                Count = 10
            });
            database.Create(new Record
            {
                DateTime = new DateTime(2026, 9, 22),
                Count = 20
            });

            var records = database.ReadAllData();
            var toDelete = records.Single(r => r.Count == 10);
            var toKeep = records.Single(r => r.Count == 20);

            var result = database.Delete(toDelete.Id);
            var remaining = database.ReadAllData().Single();

            Assert.That(result, Is.True);
            Assert.That(remaining.Id, Is.EqualTo(toKeep.Id));
            Assert.That(remaining.Count, Is.EqualTo(20));
        }

        [Test]
        public static void UpdateRecordTest()
        {
            database.Create(new Record
            {
                DateTime = new DateTime(2026, 9, 21),
                Count = 10
            });
            var original = database.ReadAllData().Single();

            var updatedRecord = new Record
            {
                DateTime = new DateTime(2026, 9, 22),
                Count = 25
            };

            var result = database.Update(original.Id, updatedRecord);
            var updated = database.ReadAllData().Single();

            Assert.That(result, Is.True);
            Assert.That(updated.Id, Is.EqualTo(original.Id));
            Assert.That(updated.DateTime, Is.EqualTo(updatedRecord.DateTime));
            Assert.That(updated.Count, Is.EqualTo(updatedRecord.Count));
        }

        [SetUp]
        public static void Setup()
        {
            DeleteDatabaseFile();
            database = new SQLiteDataProvider();
            database.CreateDatabase();
        }

        [TearDown]
        public static void TearDown()
        {
            DeleteDatabaseFile();
        }

        private static void DeleteDatabaseFile()
        {
            SqliteConnection.ClearAllPools();
            File.Delete("fencing_tracker.db");
        }
    }
}
