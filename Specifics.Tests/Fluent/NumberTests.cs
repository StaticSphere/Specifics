#region Using Directives
using System.Linq;
using NUnit.Framework;
#endregion

namespace StaticSphere.Specifics.Tests.Fluent
{
    [TestFixture]
    public class NumberTests
    {
        #region Helper Classes
        public class NumberTestSubjectRules : EntityRuleSet<NumberTestSubject>
        {
            public NumberTestSubjectRules()
            {
                Property(x => x.TestNumber1)
                    .Required(x => "TestNumber1 is required.")
                    .Minimum(3, x => "TestNumber1 must be at least 3.");
                Property(x => x.TestNumber2)
                    .Required("TestNumber2 is required.");
                Property(x => x.TestNumber3)
                    .Required();
            }
        }
        #endregion

        [Test]
        public void CanNumberRequiredBeEnforcedWithMessageLambda()
        {
            var rules = new NumberTestSubjectRules();

            var result = rules.Validate(new NumberTestSubject { });

            Assert.IsTrue(result.Errors.Any(e => e.Name == "TestNumber1_IsRequired"));
        }

        [Test]
        public void CanNumberRequiredBeEnforcedWithMessage()
        {
            var rules = new NumberTestSubjectRules();

            var result = rules.Validate(new NumberTestSubject { });

            Assert.IsTrue(result.Errors.Any(e => e.Name == "TestNumber2_IsRequired"));
        }

        [Test]
        public void CanNumberRequiredBeEnforcedWithDefaultMessage()
        {
            var rules = new NumberTestSubjectRules();

            var result = rules.Validate(new NumberTestSubject { });

            Assert.IsTrue(result.Errors.Any(e => e.Name == "TestNumber3_IsRequired" && e.Message == "The 'TestNumber3' property is required."));
        }

        [Test]
        public void CanNumberMinimumBeEnforcedWithMessageLambda()
        {
            var rules = new NumberTestSubjectRules();
            var testSubject = new NumberTestSubject { TestNumber1 = 1 };

            var result = rules.Validate(testSubject);
            Assert.IsTrue(result.Errors.Any(e => e.Name == "TestNumber1_MinimumValue"));
            testSubject.TestNumber1 = 10;
            result = rules.Validate(testSubject);
            Assert.IsFalse(result.Errors.Any(e => e.Name == "TestNumber1_MinimumValue"));
        }
    }
}