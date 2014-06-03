#region Using Directives
using System;
using System.Linq.Expressions;
#endregion

namespace StaticSphere.Specifics.Contracts
{
    /// <summary>
    /// Provides a mechanism for defining a business entity specification.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity that the specification is being applied to.</typeparam>
    public interface ISpecification<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// The predicate used to determine if the entity under test satisfies this specification or not.
        /// </summary>
        Expression<Func<TEntity, bool>> Predicate { get; }

        /// <summary>
        /// Determines whether the provided entity satisfies this specification.
        /// </summary>
        /// <param name="entity">The entity to test.</param>
        /// <returns><c>true</c> if the provided entity satisfies the specification; <c>false</c> otherwise.</returns>
        bool IsSatisfiedBy(TEntity entity);
    }
}