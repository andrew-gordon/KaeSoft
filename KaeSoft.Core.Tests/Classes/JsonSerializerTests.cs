using System;
using System.Runtime.Serialization;
using KaeSoft.Core.Classes;
using NUnit.Framework;

namespace KaeSoft.Core.Tests.Classes
{
    [TestFixture]
    public class JsonSerializerTests
    {
        [DataContract]
        internal class Person
        {
            [DataMember]
            public string Name { get; set; }
            [DataMember]
            public int AnnualSalary { get; set; }
            [DataMember]
            public DateTime DateOfBirth { get; set; }
        }

        [Test]
        public void TestSerialisation()
        {
            var person = new Person
            {
                AnnualSalary = 1234,
                DateOfBirth = new DateTime(1980, 12, 25),
                Name = "Fred Smith"
            };

            var json = JsonSerializer.Serialize(person);
            Assert.AreEqual( "{\"AnnualSalary\":1234,\"DateOfBirth\":\"\\/Date(346550400000+0000)\\/\",\"Name\":\"Fred Smith\"}", json);
        }

        [Test]
        public void TestDeserialisation()
        {
            var json = "{\"AnnualSalary\":1234,\"DateOfBirth\":\"\\/Date(346550400000+0000)\\/\",\"Name\":\"Fred Smith\"}";
            var person = JsonSerializer.Deserialize<Person>(json);

            Assert.IsNotNull(person);
            Assert.AreEqual("Fred Smith", person.Name);
            Assert.AreEqual(1234, person.AnnualSalary);
            Assert.AreEqual(new DateTime(1980, 12, 25), person.DateOfBirth);
        }
    }
}
