#region Using Directives
using System;
using System.Linq.Expressions;
using System.Reflection;
using StaticSphere.Specifics.Properties;
#endregion

namespace StaticSphere.Specifics.Fluent
{
    /// <summary>
    /// Defines the fluent API method that can be enacted on a numeric property.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity that is being validated.</typeparam>
    public class FluentNumberRuleSet<TEntity> : FluentRuleSet<TEntity>
        where TEntity : class
    {
        #region Construction
        /// <summary>
        /// Initializes a new instance of the <see cref="FluentNumberRuleSet{TEntity}"/> class.
        /// </summary>
        /// <param name="entityRules">The <see cref="EntityRuleSet{TEntity}"/> instance that validation rules will be added to.</param>
        /// <param name="property">An expression that represents the property that this fluent chain applies to.</param>
        /// <exception cref="System.ArgumentNullException">
        /// entityRules
        /// or
        /// property
        /// </exception>
        /// <exception cref="System.ArgumentException">Raised if the property parameter does not represent a property.</exception>
        public FluentNumberRuleSet(EntityRuleSet<TEntity> entityRules, LambdaExpression property)
        {
            if (entityRules == null)
                throw new ArgumentNullException("entityRules");
            if (property == null)
                throw new ArgumentNullException("property");

            EntityRules = entityRules;
            var body = property.Body as MemberExpression;
            if (body == null && property.Body.NodeType == ExpressionType.Convert)
                body = ((UnaryExpression)property.Body).Operand as MemberExpression;
            if (body == null || (Property = body.Member as PropertyInfo) == null)
                throw new ArgumentException(Resources.InvalidPropertyError);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Adds a rule that specifies that the numeric property must be set.
        /// </summary>
        /// <param name="messageLambda">A lambda or function that will determine the error message to use should this validation rule fail.</param>
        /// <returns>A <see cref="FluentNumberRuleSet{TEntity}"/> object that can be used to chain numeric rules together.</returns>
        public FluentNumberRuleSet<TEntity> Required(Func<TEntity, string> messageLambda)
        {
            var name = String.Format(Resources.FluentIsRequiredName, Property.Name);
            EntityRules.AddRule(x =>
                    Property.PropertyType.IsGenericType && Property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ?
                    ((decimal?)Property.GetValue(x, null)).HasValue :
                    true, name, messageLambda);

            return this;
        }

        /// <summary>
        /// Adds a rule that specifies that the numeric property must be set.
        /// </summary>
        /// <param name="message">The error message to use should this validation rule fail.</param>
        /// <returns>A <see cref="FluentNumberRuleSet{TEntity}"/> object that can be used to chain numeric rules together.</returns>
        public FluentNumberRuleSet<TEntity> Required(string message)
        {
            return Required(x => message);
        }

        /// <summary>
        /// Adds a rule that specifies that the numeric property must be set.
        /// </summary>
        /// <returns>A <see cref="FluentNumberRuleSet{TEntity}"/> object that can be used to chain numeric rules together.</returns>
        public FluentNumberRuleSet<TEntity> Required()
        {
            return Required(String.Format(Resources.FluentIsRequiredDefaultMessage, Property.Name));
        }

        public FluentNumberRuleSet<TEntity> Minimum(decimal minimum, Func<TEntity, string> messageLambda)
        {
            var name = String.Format(Resources.FluentMinimumValueName, Property.Name);
            EntityRules.AddRule(x => (Property.PropertyType.IsGenericType && Property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ?
                (Property.GetValue(x, null) != null ? Convert.ToDecimal(Property.GetValue(x, null)) >= minimum : true) :
                Convert.ToDecimal(Property.GetValue(x, null)) >= minimum), name, messageLambda);

            return this;
        }
        #endregion
    }
}