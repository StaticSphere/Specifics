#region Using Directives
using System.Reflection;
#endregion

namespace StaticSphere.Specifics.Fluent
{
    /// <summary>
    /// Provides the base functionality used by all fluent API rule set classes.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity that is being validated.</typeparam>
    public abstract class FluentRuleSet<TEntity>
        where TEntity : class
    {
        #region Protected Properties
        /// <summary>
        /// The <see cref="EntityRuleSet{TEntity}"/> instance that validation rules will be added to.
        /// </summary>
        protected EntityRuleSet<TEntity> EntityRules { get; set; }

        /// <summary>
        /// The <see cref="PropertyInfo"/> instance that defines the property being validated.
        /// </summary>
        protected PropertyInfo Property { get; set; }
        #endregion
    }
}