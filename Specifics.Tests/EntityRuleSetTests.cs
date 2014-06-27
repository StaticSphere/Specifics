#region Using Directives
using System;
using NUnit.Framework;
#endregion

namespace StaticSphere.Specifics.Tests
{
    [TestFixture]
    public class EntityRuleSetTests
    {
        #region Helper Classes
        private class PersonRuleSet : EntityRuleSet<Person>
        {
            public PersonRuleSet()
            {
                AddRule(p => !String.IsNullOrEmpty(p.FirstName), "FirstNameRequired");
            }
        }

        private class PersonRuleSet2 : EntityRuleSet<PersonNoInitializeValidationErrors>
        {
            public PersonRuleSet2()
            {
                AddRule(p => !String.IsNullOrEmpty(p.FirstName), "FirstNameRequired");
            }
        }
        #endregion

        [Test]
        public void CanAddANewValidationRule()
        {
            var ruleSet = new PersonRuleSet();
            var rule = new ValidationRule<Person>(p => !String.IsNullOrEmpty(p.FirstName), "Rule");

            ruleSet.AddRule(rule);
        }

        [Test]
        public void DoesAddingANewValidationRuleIncrementListCount()
        {
            var ruleSet = new PersonRuleSet();
            var rule = new ValidationRule<Person>(p => !String.IsNullOrEmpty(p.FirstName), "Rule");
            var count = ruleSet.ValidationRules.Count;

            var newCount = ruleSet.AddRule(rule);

            Assert.AreEqual(count + 1, newCount);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddingValidationRuleRequiresRule()
        {
            var ruleSet = new PersonRuleSet();

            ruleSet.AddRule(null);
        }

        [Test]
        public void CanAddANewValidationRuleWithExpression()
        {
            var ruleSet = new PersonRuleSet();

            ruleSet.AddRule(p => !String.IsNullOrEmpty(p.FirstName), "Rule");
        }

        [Test]
        public void DoesAddingANewValidationRuleWithExpressionIncrementListCount()
        {
            var ruleSet = new PersonRuleSet();
            var count = ruleSet.ValidationRules.Count;

            var newCount = ruleSet.AddRule(p => !String.IsNullOrEmpty(p.FirstName), "Rule");

            Assert.AreEqual(count + 1, newCount);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddingValidationRuleWithExpressionRequiresRule()
        {
            var ruleSet = new PersonRuleSet();

            ruleSet.AddRule(null, "Rule");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddingValidationRuleWithExpressionRequiresName()
        {
            var ruleSet = new PersonRuleSet();

            ruleSet.AddRule(p => !String.IsNullOrEmpty(p.FirstName), null);
        }

        [Test]
        public void CanAddANewValidationRuleWithExpressionAndErrorMessage()
        {
            var ruleSet = new PersonRuleSet();

            ruleSet.AddRule(p => !String.IsNullOrEmpty(p.FirstName), "Rule", "First name is required.");
        }

        [Test]
        public void DoesAddingANewValidationRuleWithExpressionAndErrorMessageIncrementListCount()
        {
            var ruleSet = new PersonRuleSet();
            var count = ruleSet.ValidationRules.Count;

            var newCount = ruleSet.AddRule(p => !String.IsNullOrEmpty(p.FirstName), "Rule", "First name is required.");

            Assert.AreEqual(count + 1, newCount);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddingValidationRuleWithExpressionAndErrorMessageRequiresRule()
        {
            var ruleSet = new PersonRuleSet();

            ruleSet.AddRule(null, "Rule", "First name is required.");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddingValidationRuleWithExpressionAndErrorMessageRequiresName()
        {
            var ruleSet = new PersonRuleSet();

            ruleSet.AddRule(p => !String.IsNullOrEmpty(p.FirstName), null, "First name is required.");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddingValidationRuleWithExpressionAndErrorMessageRequiresErrorMessage()
        {
            var ruleSet = new PersonRuleSet();

            ruleSet.AddRule(p => !String.IsNullOrEmpty(p.FirstName), "Rule", (string)null);
        }

        [Test]
        public void CanAddANewValidationRuleWithExpressionAndErrorMessageFunc()
        {
            var ruleSet = new PersonRuleSet();

            ruleSet.AddRule(p => !String.IsNullOrEmpty(p.FirstName), "Rule", m => "First name is required.");
        }

        [Test]
        public void DoesAddingANewValidationRuleWithExpressionAndErrorMessageFuncIncrementListCount()
        {
            var ruleSet = new PersonRuleSet();
            var count = ruleSet.ValidationRules.Count;

            var newCount = ruleSet.AddRule(p => !String.IsNullOrEmpty(p.FirstName), "Rule", m =>"First name is required.");

            Assert.AreEqual(count + 1, newCount);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddingValidationRuleWithExpressionAndErrorMessageFuncRequiresRule()
        {
            var ruleSet = new PersonRuleSet();

            ruleSet.AddRule(null, "Rule", m => "First name is required.");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddingValidationRuleWithExpressionAndErrorMessageFuncRequiresName()
        {
            var ruleSet = new PersonRuleSet();

            ruleSet.AddRule(p => !String.IsNullOrEmpty(p.FirstName), null, m => "First name is required.");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddingValidationRuleWithExpressionAndErrorMessageFuncRequiresErrorMessage()
        {
            var ruleSet = new PersonRuleSet();

            ruleSet.AddRule(p => !String.IsNullOrEmpty(p.FirstName), "Rule", (Func<Person, string>)null);
        }

        [Test]
        public void CanValidateEntitySpecifications()
        {
            var ruleSet = new PersonRuleSet();
            var person = new Person { FirstName = "Bob" };

            var result = ruleSet.Validate(person);

            Assert.IsNotNull(result);
        }

        [Test]
        public void ValidEntityValidatesBySpecificationAsValid()
        {
            var ruleSet = new PersonRuleSet();
            var person = new Person { FirstName = "Bob" };

            var result = ruleSet.Validate(person);

            Assert.IsTrue(result.Valid);
        }

        [Test]
        public void InvalidEntityValidatesBySpecificationAsInvalid()
        {
            var ruleSet = new PersonRuleSet();
            ruleSet.AddRule(p => !String.IsNullOrEmpty(p.LastName), "LastNameRequired.");
            var person = new Person { FirstName = "Bob" };

            var result = ruleSet.Validate(person);

            Assert.IsFalse(result.Valid);
        }

        [Test]
        public void ValidatedEntityCanGetErrorsReported()
        {
            var ruleSet = new PersonRuleSet();
            ruleSet.AddRule(p => !String.IsNullOrEmpty(p.LastName), "LastNameRequired.");
            var person = new Person { FirstName = "Bob" };

            ruleSet.Validate(person);

            Assert.IsNotNull(person.ValidationErrors);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DoesValidateThrowIfValidationErrorsNotInitialized()
        {
            var ruleSet = new PersonRuleSet2();
            ruleSet.AddRule(p => !String.IsNullOrEmpty(p.LastName), "LastNameRequired.");
            var person = new PersonNoInitializeValidationErrors { FirstName = "Bob" };

            ruleSet.Validate(person);
        }

        [Test]
        public void ValidatedEntityReportsErrors()
        {
            var ruleSet = new PersonRuleSet();
            ruleSet.AddRule(p => !String.IsNullOrEmpty(p.LastName), "LastNameRequired.");
            var person = new Person { FirstName = "Bob" };

            ruleSet.Validate(person);

            Assert.AreEqual(1, person.ValidationErrors.Count);
        }

        [Test]
        public void ValidationResultContainsPointerToEntityThatFailedValidation()
        {
            var ruleSet = new PersonRuleSet();
            ruleSet.AddRule(p => !String.IsNullOrEmpty(p.LastName), "LastNameRequired.");
            var person = new Person { FirstName = "Bob" };

            var result = ruleSet.Validate(person);

            Assert.AreEqual(person, result.Entity);
        }
    }
}