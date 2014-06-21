#region Using Directives
using System;
using NUnit.Framework;
#endregion

namespace StaticSphere.Specifics.Tests
{
    [TestFixture]
    public class ValidationErrorTests
    {
        [Test]
        public void CanCreateValidationError()
        {
            var error = new ValidationError("Error", "This is an error.");

            Assert.IsNotNull(error);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatingValidationErrorRequiresName()
        {
            var error = new ValidationError(null, "This is an error.");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatingValidationErrorRequiresMessage()
        {
            var error = new ValidationError("Error", null);
        }
    }
}