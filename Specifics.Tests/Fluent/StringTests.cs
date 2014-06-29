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
        public class PersonRules : EntityRuleSet<Person>
        {
            public PersonRules()
            {
                Property(p => p.FirstName)
                    .Required(p => "First Name is Required")
                    .MinimumLength(3, p => "First Name must be at least 3 letters.")
                    .MaximumLength(8, p => "First Name must be no more than 8 letters.");
                Property(p => p.LastName)
                    .Required("Last Name is required.")
                    .MinimumLength(3, "Last Name must be at least 3 letters.")
                    .MaximumLength(8, "Last name must be no more than 8 letters.");
                Property(p => p.MiddleName)
                    .Required()
                    .MinimumLength(3)
                    .MaximumLength(8);
                Property(p => p.EmployeeNumber)
                    .MatchesPattern(@"^\w{2}\-\w{4}\-\d{2}$", p => "Employee Number must be formatted as 99-9999-AA");
                Property(p => p.SocialSecurityNumber)
                    .MatchesPattern(@"^\d{3}\-\d{2}\-\d{4}$", "Social Security Number must be formatted as 999-99-9999");
                Property(p => p.DriversLicenseNumber)
                    .MatchesPattern(@"^\w{7}$");
            }
        }
        #endregion

        [Test]
        public void CanStringRequiredBeEnforcedWithMessageLambda()
        {
            var rules = new PersonRules();

            var result = rules.Validate(new Person { });

            Assert.IsTrue(result.Errors.Any(e => e.Name == "FirstName_IsRequired"));
        }

        [Test]
        public void CanStringRequiredBeEnforcedWithMessage()
        {
            var rules = new PersonRules();

            var result = rules.Validate(new Person { });

            Assert.IsTrue(result.Errors.Any(e => e.Name == "LastName_IsRequired"));
        }

        [Test]
        public void CanStringRequiredBeEnforcedWithDefaultMessage()
        {
            var rules = new PersonRules();

            var result = rules.Validate(new Person { });

            Assert.IsTrue(result.Errors.Any(e => e.Name == "MiddleName_IsRequired" && e.Message == "The 'MiddleName' property is required."));
        }

        [Test]
        public void CanStringHaveMinimumLengthWithMessageLambda()
        {
            var rules = new PersonRules();
            var person = new Person { FirstName = "A", MiddleName = "Fred", LastName = "Smith" };

            var result = rules.Validate(person);

            Assert.IsTrue(result.Errors.Any(e => e.Name == "FirstName_MinimumLength"));
            Assert.IsFalse(result.Errors.Any(e => e.Name == "LastName_MinimumLength"));
        }

        [Test]
        public void CanStringHaveMinimumLengthWithMessage()
        {
            var rules = new PersonRules();
            var person = new Person { FirstName = "Alvin", MiddleName = "Fred", LastName = "S" };

            var result = rules.Validate(person);

            Assert.IsFalse(result.Errors.Any(e => e.Name == "FirstName_MinimumLength"));
            Assert.IsTrue(result.Errors.Any(e => e.Name == "LastName_MinimumLength"));
        }

        [Test]
        public void CanStringHaveMinimumLengthWithDefaultMessage()
        {
            var rules = new PersonRules();
            var person = new Person { FirstName = "Alvin", MiddleName="F", LastName = "S" };

            var result = rules.Validate(person);

            Assert.IsTrue(result.Errors.Any(e => e.Name == "MiddleName_MinimumLength" && e.Message == "The 'MiddleName' property must be at least 3 characters long."));
        }

        [Test]
        public void CanStringHaveMaximumLengthWithMessageLambda()
        {
            var rules = new PersonRules();
            var person = new Person { FirstName = "Alexandria", MiddleName = "Fred", LastName = "Smith" };

            var result = rules.Validate(person);

            Assert.IsTrue(result.Errors.Any(e => e.Name == "FirstName_MaximumLength"));
            Assert.IsFalse(result.Errors.Any(e => e.Name == "LastName_MaximumLength"));
        }

        [Test]
        public void CanStringHaveMaximumLengthWithMessage()
        {
            var rules = new PersonRules();
            var person = new Person { FirstName = "Alvin", MiddleName = "Fred", LastName = "Smithsonian" };

            var result = rules.Validate(person);

            Assert.IsFalse(result.Errors.Any(e => e.Name == "FirstName_MaximumLength"));
            Assert.IsTrue(result.Errors.Any(e => e.Name == "LastName_MaximumLength"));
        }

        [Test]
        public void CanStringHaveMaximumLengthWithDefaultMessage()
        {
            var rules = new PersonRules();
            var person = new Person { FirstName = "Alvin", MiddleName = "Frederickson", LastName = "Smith" };

            var result = rules.Validate(person);

            Assert.IsTrue(result.Errors.Any(e => e.Name == "MiddleName_MaximumLength" && e.Message == "The 'MiddleName' property must be no more than 8 characters long."));
        }

        [Test]
        public void CanStringHavePatternWithMessageLambda()
        {
            var rules = new PersonRules();
            var person = new Person { FirstName = "Jim", MiddleName = "Klein", LastName = "Smith", EmployeeNumber = "aaa" };

            var result = rules.Validate(person);

            Assert.IsTrue(result.Errors.Any(e => e.Name == "EmployeeNumber_MatchesPattern"));
        }

        [Test]
        public void CanStringHavePatternWithMessage()
        {
            var rules = new PersonRules();
            var person = new Person { FirstName = "Jim", MiddleName = "Klein", LastName = "Smith", SocialSecurityNumber = "aaa" };

            var result = rules.Validate(person);

            Assert.IsTrue(result.Errors.Any(e => e.Name == "SocialSecurityNumber_MatchesPattern"));
        }

        [Test]
        public void CanStringHavePatternWithDefaultMessage()
        {
            var rules = new PersonRules();
            var person = new Person { FirstName = "Jim", MiddleName = "Klein", LastName = "Smith", DriversLicenseNumber = "aaa" };

            var result = rules.Validate(person);

            Assert.IsTrue(result.Errors.Any(e => e.Name == "DriversLicenseNumber_MatchesPattern"));
        }
    }
}