#region Using Directives

#endregion

namespace StaticSphere.Specifics
{
    /// <summary>
    /// Provides information about how well an entity satisfies a specification.
    /// </summary>
    public class ValidationResult
    {
        #region Properties
        /// <summary>
        /// Specifies whether a validation was successful or not.
        /// </summary>
        public bool Valid { get; private set; }
        #endregion
    }
}