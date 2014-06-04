#region Using Directives
using System;
using NUnit.Framework;
#endregion

namespace StaticSphere.Specifics.Tests
{
    [TestFixture]
    public class SpecificationTests
    {
        [Test]
        public void CanASpecificationBeCreated()
        {
            var spec = new Specification<Person>(x => true);

            Assert.IsNotNull(spec);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DoesASpecificationRequireAPredicate()
        {
            var spec = new Specification<Person>(null);
        }

        [Test]
        public void CanASpecificationReturnItsPredicate()
        {
            var spec = new Specification<Person>(x => true);
            var predicate = spec.Predicate;

            Assert.IsNotNull(predicate);
        }


        [Test]
        public void CanASpecificationReturnItsDelegate()
        {
            var spec = new Specification<Person>(x => true);
            var @delegate = spec.Delegate;

            Assert.IsNotNull(@delegate);
        }

        [Test]
        public void CanASpecificationDetermineSatisfaction()
        {
            var person = new Person { FirstName = "Bob" };
            var spec = new Specification<Person>(p => !String.IsNullOrEmpty(p.FirstName));


            Assert.IsTrue(spec.IsSatisfiedBy(person));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DoesIsSatisifedByThrowOnNullEntity()
        {
            var spec = new Specification<Person>(p => !String.IsNullOrEmpty(p.FirstName));
            var satisfied = spec.IsSatisfiedBy(null);
        }

        [Test]
        public void CanASpecificationDetermineNotSatisfied()
        {
            var person = new Person();
            var spec = new Specification<Person>(p => !String.IsNullOrEmpty(p.FirstName));

            Assert.IsFalse(spec.IsSatisfiedBy(person));
        }

        [Test]
        public void CanSpecificationsBeANDedTogether()
        {
            var person = new Person { FirstName = "Bob" };
            var spec = new Specification<Person>(p => !String.IsNullOrEmpty(p.FirstName)).And(
                new Specification<Person>(p => !String.IsNullOrEmpty(p.LastName)));

            Assert.IsFalse(spec.IsSatisfiedBy(person));

            person.LastName = "Smith";
            Assert.IsTrue(spec.IsSatisfiedBy(person));
        }

        [Test]
        public void CanSpecificationsBeORedTogether()
        {
            var person = new Person();
            var spec = new Specification<Person>(p => !String.IsNullOrEmpty(p.FirstName)).Or(
                new Specification<Person>(p => !String.IsNullOrEmpty(p.LastName)));

            Assert.IsFalse(spec.IsSatisfiedBy(person));

            person.LastName = "Smith";
            Assert.IsTrue(spec.IsSatisfiedBy(person));

            person.FirstName = "Bob";
            person.LastName = null;
            Assert.IsTrue(spec.IsSatisfiedBy(person));
        }

        [Test]
        public void CanSpecificationBeNOTed()
        {
            var person = new Person { FirstName = "Bob" };
            var spec = new Specification<Person>(p => !String.IsNullOrEmpty(p.FirstName)).Not();

            Assert.IsFalse(spec.IsSatisfiedBy(person));
        }

        [Test]
        public void CanSpecificationBeANDedByOperator()
        {
            var person = new Person { FirstName = "Bob" };
            var spec = new Specification<Person>(p => !String.IsNullOrEmpty(p.FirstName)) &
                new Specification<Person>(p => !String.IsNullOrEmpty(p.LastName));

            Assert.IsFalse(spec.IsSatisfiedBy(person));

            person.LastName = "Smith";
            Assert.IsTrue(spec.IsSatisfiedBy(person));
        }

        [Test]
        public void CanSpecificationBeORedByOperator()
        {
            var person = new Person();
            var spec = new Specification<Person>(p => !String.IsNullOrEmpty(p.FirstName)) |
                new Specification<Person>(p => !String.IsNullOrEmpty(p.LastName));

            Assert.IsFalse(spec.IsSatisfiedBy(person));

            person.LastName = "Smith";
            Assert.IsTrue(spec.IsSatisfiedBy(person));

            person.FirstName = "Bob";
            person.LastName = null;
            Assert.IsTrue(spec.IsSatisfiedBy(person));
        }

        [Test]
        public void CanSpecificationBeNOTedByOperator()
        {
            var person = new Person { FirstName = "Bob" };
            var spec = !(new Specification<Person>(p => !String.IsNullOrEmpty(p.FirstName)));

            Assert.IsFalse(spec.IsSatisfiedBy(person));
        }
    }
}