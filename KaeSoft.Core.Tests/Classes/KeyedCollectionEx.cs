using System;
using System.Runtime.Serialization;
using Andy.Lib.Classes;
using NUnit.Framework;

namespace Andy.Lib.Tests.Classes
{
    [TestFixture]
    public class KeyedCollectionExTests
    {
        private KeyedCollectionEx<string, Employee> _employeeList;
        private Employee _fredSmith;
        private Employee _joeBloggs;

        [DataContract]
        internal class Employee
        {
            [DataMember]
            public string Id { get; set; }
            [DataMember]
            public string Name { get; set; }
            [DataMember]
            public int AnnualSalary { get; set; }
            [DataMember]
            public DateTime DateOfBirth { get; set; }
        }

        [SetUp]
        public void SetUp()
        {
            _fredSmith = new Employee
            {
                Id = "1357",
                AnnualSalary = 12345,
                DateOfBirth = new DateTime(1980, 12, 25),
                Name = "Fred Smith"
            };

            _joeBloggs = new Employee
            {
                Id = "2468",
                AnnualSalary = 2468,
                DateOfBirth = new DateTime(1985, 4, 13),
                Name = "Joe Bloggs"
            };

            _employeeList = new KeyedCollectionEx<string, Employee>(e => e.Id)
            {
                _fredSmith,
                _joeBloggs
            };
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Null_Contructor_Parameter_Test()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new KeyedCollectionEx<string, int>(null);
        }

        [Test]
        public void Test_Get_By_Key()
        {
            Assert.AreEqual("Fred Smith", _employeeList["1357"].Name);
            Assert.AreEqual("Joe Bloggs", _employeeList["2468"].Name);
        }

        [Test]
        // This test shows that the internal keys do not get updated in the KeyedCollection when an
        // an item's key property is changed after being added to the KeyedCollection.
        public void Test_Change_Key_Of_An_Item_And_Lookup_Will_Fail()
        {
            var oldId = _fredSmith.Id;
            Assert.IsTrue(_employeeList.Contains(oldId));

            const string newId = "999";
            _fredSmith.Id = newId;

            // This proves that the KeyedCollection still uses the old key
            var e = _employeeList[oldId];
            Assert.AreEqual(_fredSmith, e);

            Assert.IsTrue(_employeeList.Contains(oldId));
            Assert.IsFalse(_employeeList.Contains(newId));
        }
    }
}
