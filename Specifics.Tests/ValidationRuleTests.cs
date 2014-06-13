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
            var rule = new ValidationRule<Person>(new Specification<Person>(x => true), "Rule", "The rule must be true.");

            Assert.IsNotNull(rule);
        }

        [Test]
        public void CanAValidationRuleBeCreatedViaLambda()
        {
            var rule = new ValidationRule<Person>(p => true, "Rule", "The rule must be true.");

            Assert.IsNotNull(rule);
        }

        [Test]
        public void CanAValidationRuleBeCreatedWithoutAMessage()
        {
            var rule = new ValidationRule<Person>(new Specification<Person>(x => true), "Rule");

            Assert.IsNotNull(rule);
        }

        [Test]
        public void CanAValidationRuleBeCreatedViaLambdaWithoutAMessage()
        {
            var rule = new ValidationRule<Person>(p => true, "Rule");

            Assert.IsNotNull(rule);
        }

        [Test]
        public void CanAValidationRuleBeCreatedWithMessageLambda()
        {
            var person = new Person();
            var rule = new ValidationRule<Person>(new Specification<Person>(x => !String.IsNullOrEmpty(x.FirstName)), "Rule", x => String.Format("Entity of type {0} first name is null.", x.GetType().Name));
            var valid = rule.Validate(person);

            Assert.AreEqual("Entity of type Person first name is null.", rule.ErrorMessage);
        }
        
        [Test]
        public void CanAValidationRuleBeCreatedViaLambdaWithMessageLambda()
        {
            var person = new Person();
            var rule = new ValidationRule<Person>(x => !String.IsNullOrEmpty(x.FirstName), "Rule", x => String.Format("Entity of type {0} first name is null.", x.GetType().Name));
            var valid = rule.Validate(person);

            Assert.AreEqual("Entity of type Person first name is null.", rule.ErrorMessage);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatingValidationRuleOfTypeSpecificationStringRequiresSpecification()
        {
            var rule = new ValidationRule<Person>((Specification<Person>)null, "Rule");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatingValidationRuleOfTypeSpecificationStringRequiresName()
        {
            var rule = new ValidationRule<Person>(new Specification<Person>(x => true), null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatingValidationRuleOfTypeExpressionStringRequiresExpression()
        {
            var rule = new ValidationRule<Person>((Expression<Func<Person, bool>>)null, "Test");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatingValidationRuleOfTypeExpressionStringRequiresName()
        {
            var rule = new ValidationRule<Person>(x => true, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatingValidationRuleOfTypeSpecifcationStringStringRequiresSpecification()
        {
            var rule = new ValidationRule<Person>((Specification<Person>)null, "Rule", "Just a test");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatingValidationRuleOfTypeSpecifcationStringStringRequiresName()
        {
            var rule = new ValidationRule<Person>(new Specification<Person>(x => true), null, "Just a test");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatingValidationRuleOfTypeSpecifcationStringStringRequiresMessage()
        {
            var rule = new ValidationRule<Person>(new Specification<Person>(x => true), "Rule", (string)null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatingValidationRuleOfTypeSpecificationStringFuncRequiresSpecification()
        {
            var rule = new ValidationRule<Person>((Specification<Person>)null, "Rule", x => "Just a test");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatingValidationRuleOfTypeSpecificationStringFuncRequiresName()
        {
            var rule = new ValidationRule<Person>(new Specification<Person>(x => true), null, x => "Just a test");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatingValidationRuleOfTypeSpecificationStringFuncRequireFunc()
        {
            var rule = new ValidationRule<Person>(new Specification<Person>(x => true), "Test", (Func<Person, string>)null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatingValidationRuleOfTypeExpressionStringStringRequiresExpression()
        {
            var rule = new ValidationRule<Person>((Expression<Func<Person, bool>>)null, "Rule", "Just a test");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatingValidationRuleOfTypeExpressionStringStringRequiresName()
        {
            var rule = new ValidationRule<Person>(x => true, null, "Just a test");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatingValidationRuleOfTypeExpressionStringStringRequiresMessage()
        {
            var rule = new ValidationRule<Person>(x => true, "Test", (string)null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatingValidationRuleOfTypeExpressionStringFuncRequiresExpression()
        {
            var rule = new ValidationRule<Person>((Expression<Func<Person, bool>>)null, "Rule", x => "Just a test");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatingValidationRuleOfTypeExpressionStringFuncRequiresName()
        {
            var rule = new ValidationRule<Person>(x => true, null, x => "Just a test");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatingValidationRuleOfTypeExpressionStringFuncRequiresMessage()
        {
            var rule = new ValidationRule<Person>(x => true, "Test", (Func<Person, string>)null);
        }

        [Test]
        public void CanAnEntityBeValidated()
        {
            var person = new Person { FirstName = "Bob", LastName = "Smith" };
            var spec = new Specification<Person>(p => !String.IsNullOrEmpty(p.FirstName));
            spec &= new Specification<Person>(p => !String.IsNullOrEmpty(p.LastName));

            var rule = new ValidationRule<Person>(spec, "FullNameRequired");

            var result = rule.Validate(person);

            Assert.IsTrue(result);
        }

        [Test]
        public void CanAnEntityBeInvalidated()
        {
            var person = new Person { FirstName = "Bob" };
            var spec = new Specification<Person>(p => !String.IsNullOrEmpty(p.FirstName));
            spec &= new Specification<Person>(p => !String.IsNullOrEmpty(p.LastName));

            var rule = new ValidationRule<Person>(spec, "FullNameRequired");

            var result = rule.Validate(person);

            Assert.IsFalse(result);
        }

        [Test]
        public void DoesInvalidationSetErrorMessage()
        {
            var person = new Person { FirstName = "Bob" };
            var spec = new Specification<Person>(p => !String.IsNullOrEmpty(p.FirstName));
            spec &= new Specification<Person>(p => !String.IsNullOrEmpty(p.LastName));

            var rule = new ValidationRule<Person>(spec, "FullNameRequired", "The persons full name is required");

            var result = rule.Validate(person);

            Assert.IsNotNullOrEmpty(rule.ErrorMessage);
        }

        [Test]
        public void DoesInvalidationSetDefaultErrorMessage()
        {
            var person = new Person { FirstName = "Bob" };
            var spec = new Specification<Person>(p => !String.IsNullOrEmpty(p.FirstName));
            spec &= new Specification<Person>(p => !String.IsNullOrEmpty(p.LastName));

            var rule = new ValidationRule<Person>(spec, "FullNameRequired");

            var result = rule.Validate(person);

            Assert.IsNotNullOrEmpty(rule.ErrorMessage);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DoesValidatingANullEntityThrowException()
        {
            var rule = new ValidationRule<Person>(x => true, "Test");
            rule.Validate(null);
        }
    }
}