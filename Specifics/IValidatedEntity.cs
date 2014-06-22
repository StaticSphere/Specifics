#region Using Directives
using System.Collections.Generic;
#endregion

namespace StaticSphere.Specifics
{
    /// <summary>
    /// Provides a mechanism for attaching the validation errors for an entity to that entity.
    /// </summary>
    public interface IValidatedEntity
    {
        /// <summary>
        /// The list of <see cref="ValidationError"/> objects that describe why the entity failed validation.
        /// </summary>
        /// <remarks>
        /// The class that implements <see cref="IValidatedEntity"/> must initialize the list.
        /// </remarks>
        IList<ValidationError> ValidationErrors { get; }
    }
}