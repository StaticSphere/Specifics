#region Using Directives
using System;
using System.Linq;
using NUnit.Framework;
#endregion

namespace StaticSphere.Specifics.Tests
{
    [TestFixture]
    public class ValidationResultTests
    {
        [Test]
        public void CanCreateValidationResult()
        {
            var result = new ValidationResult();

            Assert.IsNotNull(result);
        }

        [Test]
        public void NewValidationResultIsValid()
        {
            var result = new ValidationResult();

            Assert.IsTrue(result.Valid);
        }

        [Test]
        public void CanAccessErrorsCollection()
        {
            var result = new ValidationResult();

            Assert.IsNotNull(result.Errors);
        }

        [Test]
        public void CanAddErrorToValidationResult()
        {
            var result = new ValidationResult();
            var error = new ValidationError("Error", "An error has occurred.");

            var count = result.AddError(error);

            Assert.IsTrue(count > 0);
        }

        [Test]
        public void AddingErrorToValidationResultAddsToErrorsCollection()
        {
            var result = new ValidationResult();
            var error = new ValidationError("Error", "An error has occurred.");

            var count = result.Errors.Count();
            result.AddError(error);

            Assert.AreEqual(count + 1, result.Errors.Count());
        }

        [Test]
        public void AddingErrorToValidationResultReturnsCorrectErrorCount()
        {
            var result = new ValidationResult();
            var error = new ValidationError("Error", "An error has occurred.");
            var error2 = new ValidationError("Error2", "Another error has occurred.");

            var count = result.AddError(error);
            count = result.AddError(error2);

            Assert.AreEqual(result.Errors.Count(), count);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddErrorRequiresValidationError()
        {
            var result = new ValidationResult();
            result.AddError(null);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddedValidationErrorMustHaveUniqueErrors()
        {
            var result = new ValidationResult();
            var error = new ValidationError("Error", "An error has occurred.");

            var count = result.AddError(error);
            count = result.AddError(error);
        }
        
        [Test]
        public void ValidationResultWithoutErrorsShowsValid()
        {
            var result = new ValidationResult();

            Assert.IsTrue(result.Valid);
        }

        [Test]
        public void ValidationResultWithErrorsShowsInvalid()
        {
            var result = new ValidationResult();
            var error = new ValidationError("Test", "An error has occurred.");

            result.AddError(error);

            Assert.IsFalse(result.Valid);
        }
    }
}