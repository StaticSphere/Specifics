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
        private class StringTestSubjectRules : EntityRuleSet<StringTestSubject>
        {
            public StringTestSubjectRules()
            {
                AddRule(x => !String.IsNullOrEmpty(x.TestString1), "TestString1Required");
            }
        }

        private class StringTestSubjectRules2 : EntityRuleSet<StringTestSubjectNotInitialized>
        {
            public StringTestSubjectRules2()
            {
                AddRule(x => !String.IsNullOrEmpty(x.TestString1), "TestString1Required");
            }
        }
        #endregion

        [Test]
        public void CanAddANewValidationRule()
        {
            var ruleSet = new StringTestSubjectRules();
            var rule = new ValidationRule<StringTestSubject>(x => !String.IsNullOrEmpty(x.TestString1), "Rule");

            ruleSet.AddRule(rule);
        }

        [Test]
        public void DoesAddingANewValidationRuleIncrementListCount()
        {
            var ruleSet = new StringTestSubjectRules();
            var rule = new ValidationRule<StringTestSubject>(x => !String.IsNullOrEmpty(x.TestString1), "Rule");
            var count = ruleSet.ValidationRules.Count;

            var newCount = ruleSet.AddRule(rule);

            Assert.AreEqual(count + 1, newCount);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddingValidationRuleRequiresRule()
        {
            var ruleSet = new StringTestSubjectRules();

            ruleSet.AddRule(null);
        }

        [Test]
        public void CanAddANewValidationRuleWithExpression()
        {
            var ruleSet = new StringTestSubjectRules();

            ruleSet.AddRule(x => !String.IsNullOrEmpty(x.TestString1), "Rule");
        }

        [Test]
        public void DoesAddingANewValidationRuleWithExpressionIncrementListCount()
        {
            var ruleSet = new StringTestSubjectRules();
            var count = ruleSet.ValidationRules.Count;

            var newCount = ruleSet.AddRule(x => !String.IsNullOrEmpty(x.TestString1), "Rule");

            Assert.AreEqual(count + 1, newCount);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddingValidationRuleWithExpressionRequiresRule()
        {
            var ruleSet = new StringTestSubjectRules();

            ruleSet.AddRule(null, "Rule");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddingValidationRuleWithExpressionRequiresName()
        {
            var ruleSet = new StringTestSubjectRules();

            ruleSet.AddRule(x => !String.IsNullOrEmpty(x.TestString1), null);
        }

        [Test]
        public void CanAddANewValidationRuleWithExpressionAndErrorMessage()
        {
            var ruleSet = new StringTestSubjectRules();

            ruleSet.AddRule(x => !String.IsNullOrEmpty(x.TestString1), "Rule", "First name is required.");
        }

        [Test]
        public void DoesAddingANewValidationRuleWithExpressionAndErrorMessageIncrementListCount()
        {
            var ruleSet = new StringTestSubjectRules();
            var count = ruleSet.ValidationRules.Count;

            var newCount = ruleSet.AddRule(x => !String.IsNullOrEmpty(x.TestString1), "Rule", "First name is required.");

            Assert.AreEqual(count + 1, newCount);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddingValidationRuleWithExpressionAndErrorMessageRequiresRule()
        {
            var ruleSet = new StringTestSubjectRules();

            ruleSet.AddRule(null, "Rule", "First name is required.");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddingValidationRuleWithExpressionAndErrorMessageRequiresName()
        {
            var ruleSet = new StringTestSubjectRules();

            ruleSet.AddRule(x => !String.IsNullOrEmpty(x.TestString1), null, "First name is required.");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddingValidationRuleWithExpressionAndErrorMessageRequiresErrorMessage()
        {
            var ruleSet = new StringTestSubjectRules();

            ruleSet.AddRule(x => !String.IsNullOrEmpty(x.TestString1), "Rule", (string)null);
        }

        [Test]
        public void CanAddANewValidationRuleWithExpressionAndErrorMessageFunc()
        {
            var ruleSet = new StringTestSubjectRules();

            ruleSet.AddRule(x => !String.IsNullOrEmpty(x.TestString1), "Rule", m => "First name is required.");
        }

        [Test]
        public void DoesAddingANewValidationRuleWithExpressionAndErrorMessageFuncIncrementListCount()
        {
            var ruleSet = new StringTestSubjectRules();
            var count = ruleSet.ValidationRules.Count;

            var newCount = ruleSet.AddRule(x => !String.IsNullOrEmpty(x.TestString1), "Rule", m =>"First name is required.");

            Assert.AreEqual(count + 1, newCount);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddingValidationRuleWithExpressionAndErrorMessageFuncRequiresRule()
        {
            var ruleSet = new StringTestSubjectRules();

            ruleSet.AddRule(null, "Rule", m => "First name is required.");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddingValidationRuleWithExpressionAndErrorMessageFuncRequiresName()
        {
            var ruleSet = new StringTestSubjectRules();

            ruleSet.AddRule(x => !String.IsNullOrEmpty(x.TestString1), null, m => "First name is required.");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddingValidationRuleWithExpressionAndErrorMessageFuncRequiresErrorMessage()
        {
            var ruleSet = new StringTestSubjectRules();

            ruleSet.AddRule(x => !String.IsNullOrEmpty(x.TestString1), "Rule", (Func<StringTestSubject, string>)null);
        }

        [Test]
        public void CanValidateEntitySpecifications()
        {
            var ruleSet = new StringTestSubjectRules();
            var testSubject = new StringTestSubject { TestString1 = "Bob" };

            var result = ruleSet.Validate(testSubject);

            Assert.IsNotNull(result);
        }

        [Test]
        public void ValidEntityValidatesBySpecificationAsValid()
        {
            var ruleSet = new StringTestSubjectRules();
            var testSubject = new StringTestSubject { TestString1 = "Bob" };

            var result = ruleSet.Validate(testSubject);

            Assert.IsTrue(result.Valid);
        }

        [Test]
        public void InvalidEntityValidatesBySpecificationAsInvalid()
        {
            var ruleSet = new StringTestSubjectRules();
            ruleSet.AddRule(x => !String.IsNullOrEmpty(x.TestString1), "TestString1Required.");
            var testSubject = new StringTestSubject();

            var result = ruleSet.Validate(testSubject);

            Assert.IsFalse(result.Valid);
        }

        [Test]
        public void ValidatedEntityCanGetErrorsReported()
        {
            var ruleSet = new StringTestSubjectRules();
            ruleSet.AddRule(x => !String.IsNullOrEmpty(x.TestString1), "TestString1Required.");
            var testSubject = new StringTestSubject { TestString1 = "Bob" };

            ruleSet.Validate(testSubject);

            Assert.IsNotNull(testSubject.ValidationErrors);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DoesValidateThrowIfValidationErrorsNotInitialized()
        {
            var ruleSet = new StringTestSubjectRules2();
            ruleSet.AddRule(x => !String.IsNullOrEmpty(x.TestString1), "TestString1Required.");
            var testSubject = new StringTestSubjectNotInitialized();

            ruleSet.Validate(testSubject);
        }

        [Test]
        public void ValidatedEntityReportsErrors()
        {
            var ruleSet = new StringTestSubjectRules();
            ruleSet.AddRule(x => !String.IsNullOrEmpty(x.TestString2), "TestString2Required.");
            var testSubject = new StringTestSubject { TestString1 = "Bob" };

            ruleSet.Validate(testSubject);

            Assert.AreEqual(1, testSubject.ValidationErrors.Count);
        }

        [Test]
        public void ValidationResultContainsPointerToEntityThatFailedValidation()
        {
            var ruleSet = new StringTestSubjectRules();
            ruleSet.AddRule(x => !String.IsNullOrEmpty(x.TestString1), "TestString1Required.");
            var testSubject = new StringTestSubject { TestString1 = "Bob" };

            var result = ruleSet.Validate(testSubject);

            Assert.AreEqual(testSubject, result.Entity);
        }
    }
}