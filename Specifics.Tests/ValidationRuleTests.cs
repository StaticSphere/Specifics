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
            var rule = new ValidationRule<StringTestSubject>(new Specification<StringTestSubject>(x => true), "Rule", "The rule must be true.");

            Assert.IsNotNull(rule);
        }

        [Test]
        public void CanAValidationRuleBeCreatedViaLambda()
        {
            var rule = new ValidationRule<StringTestSubject>(p => true, "Rule", "The rule must be true.");

            Assert.IsNotNull(rule);
        }

        [Test]
        public void CanAValidationRuleBeCreatedWithoutAMessage()
        {
            var rule = new ValidationRule<StringTestSubject>(new Specification<StringTestSubject>(x => true), "Rule");

            Assert.IsNotNull(rule);
        }

        [Test]
        public void CanAValidationRuleBeCreatedViaLambdaWithoutAMessage()
        {
            var rule = new ValidationRule<StringTestSubject>(p => true, "Rule");

            Assert.IsNotNull(rule);
        }

        [Test]
        public void CanAValidationRuleBeCreatedWithMessageLambda()
        {
            var testSubject = new StringTestSubject();
            var rule = new ValidationRule<StringTestSubject>(new Specification<StringTestSubject>(x => !String.IsNullOrEmpty(x.TestString1)), "Rule", x => String.Format("Entity of type {0} first name is null.", x.GetType().Name));
            var valid = rule.Validate(testSubject);

            Assert.AreEqual("Entity of type StringTestSubject first name is null.", rule.ErrorMessage);
        }
        
        [Test]
        public void CanAValidationRuleBeCreatedViaLambdaWithMessageLambda()
        {
            var testSubject = new StringTestSubject();
            var rule = new ValidationRule<StringTestSubject>(x => !String.IsNullOrEmpty(x.TestString1), "Rule", x => String.Format("Entity of type {0} first name is null.", x.GetType().Name));
            var valid = rule.Validate(testSubject);

            Assert.AreEqual("Entity of type StringTestSubject first name is null.", rule.ErrorMessage);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatingValidationRuleOfTypeSpecificationStringRequiresSpecification()
        {
            var rule = new ValidationRule<StringTestSubject>((Specification<StringTestSubject>)null, "Rule");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatingValidationRuleOfTypeSpecificationStringRequiresName()
        {
            var rule = new ValidationRule<StringTestSubject>(new Specification<StringTestSubject>(x => true), null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatingValidationRuleOfTypeExpressionStringRequiresExpression()
        {
            var rule = new ValidationRule<StringTestSubject>((Expression<Func<StringTestSubject, bool>>)null, "Test");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatingValidationRuleOfTypeExpressionStringRequiresName()
        {
            var rule = new ValidationRule<StringTestSubject>(x => true, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatingValidationRuleOfTypeSpecifcationStringStringRequiresSpecification()
        {
            var rule = new ValidationRule<StringTestSubject>((Specification<StringTestSubject>)null, "Rule", "Just a test");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatingValidationRuleOfTypeSpecifcationStringStringRequiresName()
        {
            var rule = new ValidationRule<StringTestSubject>(new Specification<StringTestSubject>(x => true), null, "Just a test");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatingValidationRuleOfTypeSpecifcationStringStringRequiresMessage()
        {
            var rule = new ValidationRule<StringTestSubject>(new Specification<StringTestSubject>(x => true), "Rule", (string)null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatingValidationRuleOfTypeSpecificationStringFuncRequiresSpecification()
        {
            var rule = new ValidationRule<StringTestSubject>((Specification<StringTestSubject>)null, "Rule", x => "Just a test");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatingValidationRuleOfTypeSpecificationStringFuncRequiresName()
        {
            var rule = new ValidationRule<StringTestSubject>(new Specification<StringTestSubject>(x => true), null, x => "Just a test");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatingValidationRuleOfTypeSpecificationStringFuncRequireFunc()
        {
            var rule = new ValidationRule<StringTestSubject>(new Specification<StringTestSubject>(x => true), "Test", (Func<StringTestSubject, string>)null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatingValidationRuleOfTypeExpressionStringStringRequiresExpression()
        {
            var rule = new ValidationRule<StringTestSubject>((Expression<Func<StringTestSubject, bool>>)null, "Rule", "Just a test");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatingValidationRuleOfTypeExpressionStringStringRequiresName()
        {
            var rule = new ValidationRule<StringTestSubject>(x => true, null, "Just a test");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatingValidationRuleOfTypeExpressionStringStringRequiresMessage()
        {
            var rule = new ValidationRule<StringTestSubject>(x => true, "Test", (string)null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatingValidationRuleOfTypeExpressionStringFuncRequiresExpression()
        {
            var rule = new ValidationRule<StringTestSubject>((Expression<Func<StringTestSubject, bool>>)null, "Rule", x => "Just a test");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatingValidationRuleOfTypeExpressionStringFuncRequiresName()
        {
            var rule = new ValidationRule<StringTestSubject>(x => true, null, x => "Just a test");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatingValidationRuleOfTypeExpressionStringFuncRequiresMessage()
        {
            var rule = new ValidationRule<StringTestSubject>(x => true, "Test", (Func<StringTestSubject, string>)null);
        }

        [Test]
        public void CanAnEntityBeValidated()
        {
            var testSubject = new StringTestSubject { TestString1 = "Bob", TestString2 = "Smith" };
            var spec = new Specification<StringTestSubject>(p => !String.IsNullOrEmpty(p.TestString1));
            spec &= new Specification<StringTestSubject>(p => !String.IsNullOrEmpty(p.TestString2));

            var rule = new ValidationRule<StringTestSubject>(spec, "FullNameRequired");

            var result = rule.Validate(testSubject);

            Assert.IsTrue(result);
        }

        [Test]
        public void CanAnEntityBeInvalidated()
        {
            var testSubject = new StringTestSubject { TestString1 = "Bob" };
            var spec = new Specification<StringTestSubject>(p => !String.IsNullOrEmpty(p.TestString1));
            spec &= new Specification<StringTestSubject>(p => !String.IsNullOrEmpty(p.TestString2));

            var rule = new ValidationRule<StringTestSubject>(spec, "FullNameRequired");

            var result = rule.Validate(testSubject);

            Assert.IsFalse(result);
        }

        [Test]
        public void DoesInvalidationSetErrorMessage()
        {
            var testSubject = new StringTestSubject { TestString1 = "Bob" };
            var spec = new Specification<StringTestSubject>(p => !String.IsNullOrEmpty(p.TestString1));
            spec &= new Specification<StringTestSubject>(p => !String.IsNullOrEmpty(p.TestString2));

            var rule = new ValidationRule<StringTestSubject>(spec, "FullNameRequired", "The testSubjects full name is required");

            var result = rule.Validate(testSubject);

            Assert.IsNotNullOrEmpty(rule.ErrorMessage);
        }

        [Test]
        public void DoesInvalidationSetDefaultErrorMessage()
        {
            var testSubject = new StringTestSubject { TestString1 = "Bob" };
            var spec = new Specification<StringTestSubject>(p => !String.IsNullOrEmpty(p.TestString1));
            spec &= new Specification<StringTestSubject>(p => !String.IsNullOrEmpty(p.TestString2));

            var rule = new ValidationRule<StringTestSubject>(spec, "FullNameRequired");

            var result = rule.Validate(testSubject);

            Assert.IsNotNullOrEmpty(rule.ErrorMessage);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DoesValidatingANullEntityThrowException()
        {
            var rule = new ValidationRule<StringTestSubject>(x => true, "Test");
            rule.Validate(null);
        }
    }
}