#region Using Directives
using System;
using System.Linq.Expressions;
using NUnit.Framework;
#endregion

namespace StaticSphere.Specifics.Tests
{
    [TestFixture]
    public class ValidationRuleTests
    {
        [Test]
        public void CanAValidationRuleBeCreated()
        {
            var rule = new ValidationRule<Person>(new Specification<Person>(x => true), "The rule must be true.");

            Assert.IsNotNull(rule);
        }

        [Test]
        public void CanAValidationRuleBeCreatedViaLambda()
        {
            var rule = new ValidationRule<Person>(p => true, "The rule must be true.");

            Assert.IsNotNull(rule);
        }

        [Test]
        public void CanAValidationRuleBeCreatedWithoutAMessage()
        {
            var rule = new ValidationRule<Person>(new Specification<Person>(x => true));

            Assert.IsNotNull(rule);
        }

        [Test]
        public void CanAValidationRuleBeCreatedViaLambdaWithoutAMessage()
        {
            var rule = new ValidationRule<Person>(p => true);

            Assert.IsNotNull(rule);
        }

        [Test]
        public void CanAValidationRuleBeCreatedWithMessageLambda()
        {
            var person = new Person();
            var rule = new ValidationRule<Person>(new Specification<Person>(x => !String.IsNullOrEmpty(x.FirstName)), x => String.Format("Entity of type {0} first name is null.", x.GetType().Name));
            var valid = rule.Validate(person);

            Assert.AreEqual("Entity of type Person first name is null.", rule.Message);
        }
        
        [Test]
        public void CanAValidationRuleBeCreatedViaLambdaWithMessageLambda()
        {
            var person = new Person();
            var rule = new ValidationRule<Person>(x => !String.IsNullOrEmpty(x.FirstName), x => String.Format("Entity of type {0} first name is null.", x.GetType().Name));
            var valid = rule.Validate(person);

            Assert.AreEqual("Entity of type Person first name is null.", rule.Message);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DoesAValidationRuleRequireASpecification()
        {
            var rule = new ValidationRule<Person>((Specification<Person>)null, "Just a test");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DoesAValidationRuleRequireALambda()
        {
            var rule = new ValidationRule<Person>((Expression<Func<Person, bool>>)null, "Just a test");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DoesAValidationRuleCreatedWithMessageLambdaRequireASpecification()
        {
            var rule = new ValidationRule<Person>((Specification<Person>)null, x => "Test");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DoesAValidationRuleCreatedViaLambdaWithMessageLambdaRequireALambda()
        {
            var rule = new ValidationRule<Person>((Expression<Func<Person, bool>>)null, x => "Test");
        }

        [Test]
        public void CanAValidationRuleValidateAValidEntity()
        {
            var person = new Person { FirstName = "Bob" };
            var rule = new ValidationRule<Person>(p => !String.IsNullOrEmpty(p.FirstName), "FirstName is required.");

            Assert.IsTrue(rule.Validate(person));
        }

        [Test]
        public void CanAValidationRuleValidateAnInvalidEntity()
        {
            var person = new Person { FirstName = "Bob" };
            var rule = new ValidationRule<Person>(p => !String.IsNullOrEmpty(p.LastName), "LastName is required.");

            Assert.IsFalse(rule.Validate(person));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DoesAValidationRuleRequireANonNullEntity()
        {
            var rule = new ValidationRule<Person>(p => !String.IsNullOrEmpty(p.LastName), "LastName is required.");

            rule.Validate(null);
        }
    }
}