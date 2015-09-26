using System;
using System.Runtime.Serialization;
using Andy.Lib.Classes;
using NUnit.Framework;

namespace Andy.Lib.Tests.Classes
{
    [TestFixture]
    public class DataContractClonerTests
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
        public void CloneDataContract_AllMembersDecoratedWithDataMemberAttribute()
        {
            var person = new Person { Name = "John Smith", AnnualSalary = 50000, DateOfBirth = new DateTime(1980, 6, 12) };
            var clonedPerson = DataContractCloner.Clone(person);
            Assert.AreEqual(person.Name, clonedPerson.Name);
            Assert.AreEqual(person.DateOfBirth, clonedPerson.DateOfBirth);
            Assert.AreEqual(person.AnnualSalary, clonedPerson.AnnualSalary);
        }


        [DataContract]
        internal class Company
        {
            [DataMember]
            public int VatNumber { get; set; }

            // Deliberately not marked with [DataMember]
            public string Name { get; set; }
        }

        [Test]
        public void CloneDataContract_NotAllMembersDecoratedWithDataMemberAttribute()
        {
            var company = new Company { Name = "Marks & Spencer", VatNumber = 1234 };
            var clonedCompany = DataContractCloner.Clone(company);
            Assert.AreEqual(company.VatNumber, clonedCompany.VatNumber);
            Assert.AreNotEqual(company.Name, clonedCompany.Name); // because Name is not a [DataMember]
            Assert.IsNull(clonedCompany.Name);
        }


        private class Car
        {
// ReSharper disable UnusedAutoPropertyAccessor.Local
            public string Make { get; set; }
            public string Model { get; set; }
            public string RegistrationNumber { get; set; }
            public DateTime ManufactureDate { get; set; }
// ReSharper restore UnusedAutoPropertyAccessor.Local
        }
        
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void CloneClassWithoutDataContractAtribute()
        {
            var car = new Car { Make = "Toyota", Model = "Prius", ManufactureDate = new DateTime(2008,4,3), RegistrationNumber = "TN08 PPY"};
            DataContractCloner.Clone(car);
        }
    }
}
