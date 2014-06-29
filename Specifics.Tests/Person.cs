using System.Collections.Generic;
namespace StaticSphere.Specifics.Tests
{
    public class Person: IValidatedEntity
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public IList<ValidationError> ValidationErrors { get; private set; }

        public Person()
        {
            ValidationErrors = new List<ValidationError>();
        }
    }

    public class PersonNoInitializeValidationErrors: IValidatedEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<ValidationError> ValidationErrors { get; private set; }
    }
}