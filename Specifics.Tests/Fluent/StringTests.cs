#region Using Directives
using System.Linq;
using NUnit.Framework;
#endregion

namespace StaticSphere.Specifics.Tests.Fluent
{
    [TestFixture]
    public class StringTests
    {
        #region Helper Classes
        public class PersonRulesRequired : EntityRuleSet<Person>
        {
            public PersonRulesRequired()
            {
                Property(p => p.FirstName)
                    .Required(p => "First Name is Required");
                Property(p => p.LastName)
                    .Required("Last Name is required.");
                Property(p => p.MiddleName)
                    .Required();
            }
        }
        #endregion

        [Test]
        public void CanStringRequiredBeEnforcedWithMessageLambda()
        {
            var rules = new PersonRulesRequired();

            var result = rules.Validate(new Person { });

            Assert.IsTrue(result.Errors.Any(e => e.Name == "FirstName_IsRequired"));
        }

        [Test]
        public void CanStringRequiredBeEnforcedWithMessage()
        {
            var rules = new PersonRulesRequired();

            var result = rules.Validate(new Person { });

            Assert.IsTrue(result.Errors.Any(e => e.Name == "LastName_IsRequired"));
        }

        [Test]
        public void CanStringRequiredBeEnforcedWithDefaultMessage()
        {
            var rules = new PersonRulesRequired();

            var result = rules.Validate(new Person { });

            Assert.IsTrue(result.Errors.Any(e => e.Name == "MiddleName_IsRequired" && e.Message == "The 'MiddleName' property is required."));
        }
    }
}