using System;
using System.Collections.Generic;
using System.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tools.DataDumper.Compare.Tests.SimpleHelpers;

namespace Tools.DataDumper.Compare.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Create a couple objects to compare
            Person person1 = new Person
            {
                DateCreated = DateTime.Now,
                Name = "Greg",
                Adresses = new List<string>() { "A" }
            };

            Person person2 = new Person
            {
                Name = "John",
                DateCreated = person1.DateCreated,
                Adresses = new List<string>() { "A", "B" }
            };

            var snapshot = ObjectDiffPatch.Snapshot(person1);

            var diff = ObjectDiffPatch.GenerateDiff(snapshot, person2);

            if (!diff.AreEqual)
            {
                Console.WriteLine(diff.NewValues.ToString());
            }

            //var _diff = ObjectDiffPatch.GenerateDiff(person1, person2);

            var d = diff.NewValues.ToObject<ExpandoObject>();

            // recreate originalObj from updatedObj
            var patched = ObjectDiffPatch.PatchObject(person2, diff.OldValues);
        }
    }

    public class Person
    {
        public DateTime DateCreated { get; set; }
        public string Name { get; set; }
        public List<string> Adresses { get; set; }
    }
}
