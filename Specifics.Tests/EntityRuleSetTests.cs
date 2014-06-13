#region Using Directives
using System;
using System.Linq.Expressions;
using NUnit.Framework;
#endregion

namespace StaticSphere.Specifics.Tests
{
    [TestFixture]
    public class EntityRuleSetTests
    {
        private class PersonRuleSet : EntityRuleSet<Person>
        {
            public PersonRuleSet()
            {
                AddRule(p => !String.IsNullOrEmpty(p.FirstName), "FirstNameRequired");
                AddRule(p => !String.IsNullOrEmpty(p.LastName), "LastNameRequired");
            }
        }

        [Test]
        public void CanAValidationBeAddedToAnEntityRuleSet()
        {
            var rules = new PersonRuleSet();
            var ruleCount = rules.ValidationRules.Count;
            rules.AddRule(new ValidationRule<Person>(p => !String.IsNullOrEmpty(p.FirstName), "FirstName is required."));

            Assert.AreEqual(ruleCount + 1, rules.ValidationRules.Count);
        }

        [Test]
        public void CanAValidationBeAddedToAnEntityRuleSetViaLambda()
        {
            var rules = new PersonRuleSet();
            var ruleCount = rules.ValidationRules.Count;
            rules.AddRule(p => !String.IsNullOrEmpty(p.FirstName), "FirstName is required.");

            Assert.AreEqual(ruleCount + 1, rules.ValidationRules.Count);
        }

        [Test]
        public void CanAValidationBeAddedToAnEntityRuleSetViaLambdaWithMessageLambda()
        {
            var rules = new PersonRuleSet();
            var ruleCount = rules.ValidationRules.Count;
            rules.AddRule(p => !String.IsNullOrEmpty(p.FirstName), "FirstNameRequired", p => "FirstName is required.");

            Assert.AreEqual(ruleCount + 1, rules.ValidationRules.Count);
        }

        [Test]
        public void CanAValidationBeAddedToAnEntityRuleSetWithNoMessage()
        {
            var rules = new PersonRuleSet();
            var ruleCount = rules.ValidationRules.Count;
            rules.AddRule(p => !String.IsNullOrEmpty(p.FirstName), "FirstNameRequired");

            Assert.AreEqual(ruleCount + 1, rules.ValidationRules.Count);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DoesAddRequireAValidationRule()
        {
            var rules = new PersonRuleSet();
            rules.AddRule((ValidationRule<Person>)null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DoesAddViaLambdaRequireAValidationRule()
        {
            var rules = new PersonRuleSet();
            rules.AddRule((Expression<Func<Person, bool>>)null, "Test");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DoesAddWithExpressionWithMessageRequireAValidationRule()
        {
            var rules = new PersonRuleSet();
            rules.AddRule((Expression<Func<Person, bool>>)null, "Test");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DoesAddWithExpressionWithMessageLambdaRequireAValidationRule()
        {
            var rules = new PersonRuleSet();
            rules.AddRule((Expression<Func<Person, bool>>)null, "Test", p => "Test");
        }

        //[Test]
        //public void CanRunAValidation()
        //{
        //    var person = new Person { FirstName = "Jim", LastName = "Smith" };
        //    var rules = new PersonRuleSet();

        //    var results = rules.Validate(person);

        //    Assert.IsNotNull(results);
        //}

        //[Test]
        //public void SuccessfulValidationSetsValidToTrue()
        //{
        //    var person = new Person { FirstName = "Jim", LastName = "Smith" };
        //    var rules = new PersonRuleSet();

        //    var results = rules.Validate(person);

        //    Assert.IsTrue(results.Valid);
        //}
    }
}