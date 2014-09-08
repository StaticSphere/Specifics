#region Using Directives

#endregion

using System.Collections.Generic;
namespace StaticSphere.Specifics.Tests
{
    public class StringTestSubject : IValidatedEntity
    {
        public string TestString1 { get; set; }
        public string TestString2 { get; set; }
        public string TestString3 { get; set; }
        public string TestString4 { get; set; }
        public string TestString5 { get; set; }
        public string TestString6 { get; set; }
        public IList<ValidationError> ValidationErrors { get; private set; }

        public StringTestSubject()
        {
            ValidationErrors = new List<ValidationError>();
        }
    }

    public class StringTestSubjectNotInitialized : IValidatedEntity
    {
        public string TestString1 { get; set; }
        public IList<ValidationError> ValidationErrors { get; private set; }
    }

    public class NumberTestSubject
    {
        public decimal? TestNumber1 { get; set; }
        public decimal? TestNumber2 { get; set; }
        public decimal? TestNumber3 { get; set; }
    }
}