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
        public class StringTestSubjectRules : EntityRuleSet<StringTestSubject>
        {
            public StringTestSubjectRules()
            {
                Property(x => x.TestString1)
                    .Required(x => "TestString1 is required.")
                    .MinimumLength(3, x => "TestString1 must be at least 3 characters.")
                    .MaximumLength(8, x => "TestString1 must be no more than 8 letters.");
                Property(x => x.TestString2)
                    .Required("TestString2 is required.")
                    .MinimumLength(3, "TestString2 must be at least 3 characters.")
                    .MaximumLength(8, "TestString2 must be no more than 8 letters.");
                Property(x => x.TestString3)
                    .Required()
                    .MinimumLength(3)
                    .MaximumLength(8);
                Property(x => x.TestString4)
                    .MatchesPattern(@"^\w{2}\-\w{4}\-\d{2}$", x => "TestString4 must be formatted as AA-AAAA-99");
                Property(x => x.TestString5)
                    .MatchesPattern(@"^\d{3}\-\d{2}\-\d{4}$", "TestString5 Number must be formatted as 999-99-9999");
                Property(x => x.TestString6)
                    .MatchesPattern(@"^\w{7}$");
            }
        }
        #endregion

        [Test]
        public void CanStringRequiredBeEnforcedWithMessageLambda()
        {
            var rules = new StringTestSubjectRules();

            var result = rules.Validate(new StringTestSubject { });

            Assert.IsTrue(result.Errors.Any(e => e.Name == "TestString1_IsRequired"));
        }

        [Test]
        public void CanStringRequiredBeEnforcedWithMessage()
        {
            var rules = new StringTestSubjectRules();

            var result = rules.Validate(new StringTestSubject { });

            Assert.IsTrue(result.Errors.Any(e => e.Name == "TestString2_IsRequired"));
        }

        [Test]
        public void CanStringRequiredBeEnforcedWithDefaultMessage()
        {
            var rules = new StringTestSubjectRules();

            var result = rules.Validate(new StringTestSubject { });

            Assert.IsTrue(result.Errors.Any(e => e.Name == "TestString3_IsRequired" && e.Message == "The 'TestString3' property is required."));
        }

        [Test]
        public void CanStringHaveMinimumLengthWithMessageLambda()
        {
            var rules = new StringTestSubjectRules();
            var testSubject = new StringTestSubject { TestString1 = "A", TestString2 = "Fred", TestString3 = "Smith" };

            var result = rules.Validate(testSubject);

            Assert.IsTrue(result.Errors.Any(e => e.Name == "TestString1_MinimumLength"));
            Assert.IsFalse(result.Errors.Any(e => e.Name == "TestString2_MinimumLength"));
        }

        [Test]
        public void CanStringHaveMinimumLengthWithMessage()
        {
            var rules = new StringTestSubjectRules();
            var testSubject = new StringTestSubject { TestString1 = "Alvin", TestString2 = "F", TestString3 = "S" };

            var result = rules.Validate(testSubject);

            Assert.IsFalse(result.Errors.Any(e => e.Name == "TestString1_MinimumLength"));
            Assert.IsTrue(result.Errors.Any(e => e.Name == "TestString2_MinimumLength"));
        }

        [Test]
        public void CanStringHaveMinimumLengthWithDefaultMessage()
        {
            var rules = new StringTestSubjectRules();
            var testSubject = new StringTestSubject { TestString1 = "Alvin", TestString2 = "F", TestString3 = "S" };

            var result = rules.Validate(testSubject);

            Assert.IsTrue(result.Errors.Any(e => e.Name == "TestString3_MinimumLength" && e.Message == "The 'TestString3' property must be at least 3 characters long."));
        }

        [Test]
        public void CanStringHaveMaximumLengthWithMessageLambda()
        {
            var rules = new StringTestSubjectRules();
            var testSubject = new StringTestSubject { TestString1 = "Alexandria", TestString2 = "Fred", TestString3 = "Smith" };

            var result = rules.Validate(testSubject);

            Assert.IsTrue(result.Errors.Any(e => e.Name == "TestString1_MaximumLength"));
            Assert.IsFalse(result.Errors.Any(e => e.Name == "TestString2_MaximumLength"));
        }

        [Test]
        public void CanStringHaveMaximumLengthWithMessage()
        {
            var rules = new StringTestSubjectRules();
            var testSubject = new StringTestSubject { TestString1 = "Alvin", TestString2 = "Smithsonian", TestString3 = "Fred" };

            var result = rules.Validate(testSubject);

            Assert.IsFalse(result.Errors.Any(e => e.Name == "TestString1_MaximumLength"));
            Assert.IsTrue(result.Errors.Any(e => e.Name == "TestString2_MaximumLength"));
        }

        [Test]
        public void CanStringHaveMaximumLengthWithDefaultMessage()
        {
            var rules = new StringTestSubjectRules();
            var testSubject = new StringTestSubject { TestString1 = "Alvin", TestString2 = "Smith", TestString3 = "Frederickson" };

            var result = rules.Validate(testSubject);

            Assert.IsTrue(result.Errors.Any(e => e.Name == "TestString3_MaximumLength" && e.Message == "The 'TestString3' property must be no more than 8 characters long."));
        }

        [Test]
        public void CanStringHavePatternWithMessageLambda()
        {
            var rules = new StringTestSubjectRules();
            var testSubject = new StringTestSubject { TestString1 = "Jim", TestString2 = "Klein", TestString3 = "Smith", TestString4 = "aaa" };

            var result = rules.Validate(testSubject);

            Assert.IsTrue(result.Errors.Any(e => e.Name == "TestString4_MatchesPattern"));
            testSubject.TestString4 = "AR-QUAD-23";
            result = rules.Validate(testSubject);
            Assert.IsFalse(result.Errors.Any(e => e.Name == "TestString4_MatchesPattern"));
        }

        [Test]
        public void CanStringHavePatternWithMessage()
        {
            var rules = new StringTestSubjectRules();
            var testSubject = new StringTestSubject { TestString1 = "Jim", TestString2 = "Klein", TestString3 = "Smith", TestString5 = "aaa" };

            var result = rules.Validate(testSubject);

            Assert.IsTrue(result.Errors.Any(e => e.Name == "TestString5_MatchesPattern"));
            testSubject.TestString5 = "123-45-6789";
            result = rules.Validate(testSubject);
            Assert.IsFalse(result.Errors.Any(e => e.Name == "TestString5_MatchesPattern"));
        }

        [Test]
        public void CanStringHavePatternWithDefaultMessage()
        {
            var rules = new StringTestSubjectRules();
            var testSubject = new StringTestSubject { TestString1 = "Jim", TestString2 = "Klein", TestString3 = "Smith", TestString6 = "aaa" };

            var result = rules.Validate(testSubject);

            Assert.IsTrue(result.Errors.Any(e => e.Name == "TestString6_MatchesPattern" && e.Message == @"The 'TestString6' property must match the pattern '^\w{7}$'."));
            testSubject.TestString6 = "44976AZ";
            result = rules.Validate(testSubject);
            Assert.IsFalse(result.Errors.Any(e => e.Name == "TestString6_MatchesPattern"));
        }
    }
}