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
            var spec = new Specification<StringTestSubject>(x => true);

            Assert.IsNotNull(spec);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DoesASpecificationRequireAPredicate()
        {
            var spec = new Specification<StringTestSubject>(null);
        }

        [Test]
        public void CanASpecificationReturnItsPredicate()
        {
            var spec = new Specification<StringTestSubject>(x => true);
            var predicate = spec.Predicate;

            Assert.IsNotNull(predicate);
        }


        [Test]
        public void CanASpecificationReturnItsDelegate()
        {
            var spec = new Specification<StringTestSubject>(x => true);
            var @delegate = spec.Delegate;

            Assert.IsNotNull(@delegate);
        }

        [Test]
        public void CanASpecificationDetermineSatisfaction()
        {
            var testSubject = new StringTestSubject { TestString1 = "Bob" };
            var spec = new Specification<StringTestSubject>(p => !String.IsNullOrEmpty(p.TestString1));


            Assert.IsTrue(spec.IsSatisfiedBy(testSubject));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DoesIsSatisifedByThrowOnNullEntity()
        {
            var spec = new Specification<StringTestSubject>(p => !String.IsNullOrEmpty(p.TestString1));
            var satisfied = spec.IsSatisfiedBy(null);
        }

        [Test]
        public void CanASpecificationDetermineNotSatisfied()
        {
            var testSubject = new StringTestSubject();
            var spec = new Specification<StringTestSubject>(p => !String.IsNullOrEmpty(p.TestString1));

            Assert.IsFalse(spec.IsSatisfiedBy(testSubject));
        }

        [Test]
        public void CanSpecificationsBeANDedTogether()
        {
            var testSubject = new StringTestSubject { TestString1 = "Bob" };
            var spec = new Specification<StringTestSubject>(p => !String.IsNullOrEmpty(p.TestString1)).And(
                new Specification<StringTestSubject>(p => !String.IsNullOrEmpty(p.TestString2)));

            Assert.IsFalse(spec.IsSatisfiedBy(testSubject));

            testSubject.TestString2 = "Smith";
            Assert.IsTrue(spec.IsSatisfiedBy(testSubject));
        }

        [Test]
        public void CanSpecificationsBeORedTogether()
        {
            var testSubject = new StringTestSubject();
            var spec = new Specification<StringTestSubject>(p => !String.IsNullOrEmpty(p.TestString1)).Or(
                new Specification<StringTestSubject>(p => !String.IsNullOrEmpty(p.TestString2)));

            Assert.IsFalse(spec.IsSatisfiedBy(testSubject));

            testSubject.TestString2 = "Smith";
            Assert.IsTrue(spec.IsSatisfiedBy(testSubject));

            testSubject.TestString1 = "Bob";
            testSubject.TestString2 = null;
            Assert.IsTrue(spec.IsSatisfiedBy(testSubject));
        }

        [Test]
        public void CanSpecificationBeNOTed()
        {
            var testSubject = new StringTestSubject { TestString1 = "Bob" };
            var spec = new Specification<StringTestSubject>(p => !String.IsNullOrEmpty(p.TestString1)).Not();

            Assert.IsFalse(spec.IsSatisfiedBy(testSubject));
        }

        [Test]
        public void CanSpecificationBeANDedByOperator()
        {
            var testSubject = new StringTestSubject { TestString1 = "Bob" };
            var spec = new Specification<StringTestSubject>(p => !String.IsNullOrEmpty(p.TestString1)) &
                new Specification<StringTestSubject>(p => !String.IsNullOrEmpty(p.TestString2));

            Assert.IsFalse(spec.IsSatisfiedBy(testSubject));

            testSubject.TestString2 = "Smith";
            Assert.IsTrue(spec.IsSatisfiedBy(testSubject));
        }

        [Test]
        public void CanSpecificationBeORedByOperator()
        {
            var testSubject = new StringTestSubject();
            var spec = new Specification<StringTestSubject>(p => !String.IsNullOrEmpty(p.TestString1)) |
                new Specification<StringTestSubject>(p => !String.IsNullOrEmpty(p.TestString2));

            Assert.IsFalse(spec.IsSatisfiedBy(testSubject));

            testSubject.TestString2 = "Smith";
            Assert.IsTrue(spec.IsSatisfiedBy(testSubject));

            testSubject.TestString1 = "Bob";
            testSubject.TestString2 = null;
            Assert.IsTrue(spec.IsSatisfiedBy(testSubject));
        }

        [Test]
        public void CanSpecificationBeNOTedByOperator()
        {
            var testSubject = new StringTestSubject { TestString1 = "Bob" };
            var spec = !(new Specification<StringTestSubject>(p => !String.IsNullOrEmpty(p.TestString1)));

            Assert.IsFalse(spec.IsSatisfiedBy(testSubject));
        }
    }
}