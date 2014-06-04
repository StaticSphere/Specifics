#region Using Directives
using System;
using System.Linq;
using System.Linq.Expressions;
using StaticSphere.Specifics.Contracts;
#endregion

namespace StaticSphere.Specifics
{
    /// <summary>
    /// Defines a business entity specification.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity that the specification is being applied to.</typeparam>
    public class Specification<TEntity> : ISpecification<TEntity>
        where TEntity : class
    {
        #region Private Member Variables
        private Func<TEntity, bool> _delegate;
        #endregion

        #region Properties
        /// <summary>
        /// The predicate used to determine if the entity under test satisfies this specification or not.
        /// </summary>
        public Expression<Func<TEntity, bool>> Predicate { get; private set; }

        /// <summary>
        /// The compiled delegate used when determining if the entity under test satisfies this specification or not.
        /// </summary>
        public Func<TEntity, bool> Delegate
        {
            get
            {
                if (_delegate == null)
                    _delegate = Predicate.Compile();
                return _delegate;
            }
        }
        #endregion

        #region Construction
        /// <summary>
        /// Initializes a new instance of the <see cref="Specification{TEntity}" /> class.
        /// </summary>
        /// <param name="predicate">The predicate used to determine if the entity under test satisfies this specification or not.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if no predicate is provided.</exception>
        public Specification(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException("predicate");
            Predicate = predicate;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Determines whether the provided entity satisfies this specification.
        /// </summary>
        /// <param name="entity">The entity to test.</param>
        /// <returns><c>true</c> if the provided entity satisfies the specification; <c>false</c> otherwise.</returns>
        public bool IsSatisfiedBy(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            return Delegate.Invoke(entity);
        }

        /// <summary>
        /// Concatenates a specification on to this one, returning a new specification that
        /// will be satisfied when both source specifications are satisfied.
        /// </summary>
        /// <param name="rightSpec">The specification to concatenate with.</param>
        /// <returns>
        /// A new specification that is only satisfied when the source
        /// specifications are satisfied.
        /// </returns>
        public Specification<TEntity> And(Specification<TEntity> rightSpec)
        {
            return this & rightSpec;
        }

        /// <summary>
        /// Concatenates a specification on to this one, returning a new specification that
        /// will be satisfied when either of the source specifications are satisfied.
        /// </summary>
        /// <param name="rightSpec">The specification to concatenate with.</param>
        /// <returns>
        /// A new specification that is only satisfied when either of the source
        /// specifications are satisfied.
        /// </returns>
        public Specification<TEntity> Or(Specification<TEntity> rightSpec)
        {
            return this | rightSpec;
        }

        /// <summary>
        /// Inverts a specification to mean the opposite of the source specification.
        /// </summary>
        /// <returns>
        /// A new specification that is the opposite of the source specification.
        /// </returns>
        public Specification<TEntity> Not()
        {
            return !this;
        }
        #endregion

        #region Operators
        /// <summary>
        /// Concatenates two specifications, returning a new specification that
        /// will be satisfied when both source specifications are satisfied.
        /// </summary>
        /// <param name="rightSpec">The specification to concatenate with.</param>
        /// <returns>
        /// A new specification that is only satisfied when the source
        /// specifications are satisfied.
        /// </returns>
        public static Specification<TEntity> operator &(Specification<TEntity> leftSpec, Specification<TEntity> rightSpec)
        {
            var rightResult = Expression.Invoke(rightSpec.Predicate, leftSpec.Predicate.Parameters.Cast<Expression>());
            var concatenated = Expression.MakeBinary(ExpressionType.AndAlso, leftSpec.Predicate.Body, rightResult);
            return new Specification<TEntity>(Expression.Lambda<Func<TEntity, bool>>(concatenated, leftSpec.Predicate.Parameters));
        }

        /// <summary>
        /// Concatenates two specifications, returning a new specification that
        /// will be satisfied when either of the source specifications are satisfied.
        /// </summary>
        /// <param name="rightSpec">The specification to concatenate with.</param>
        /// <returns>
        /// A new specification that is only satisfied when either of the source
        /// specifications are satisfied.
        /// </returns>
        public static Specification<TEntity> operator |(Specification<TEntity> leftSpec, Specification<TEntity> rightSpec)
        {
            var rightResult = Expression.Invoke(rightSpec.Predicate, leftSpec.Predicate.Parameters.Cast<Expression>());
            var concatenated = Expression.MakeBinary(ExpressionType.OrElse, leftSpec.Predicate.Body, rightResult);
            return new Specification<TEntity>(Expression.Lambda<Func<TEntity, bool>>(concatenated, leftSpec.Predicate.Parameters));
        }

        /// <summary>
        /// Inverts a specification to mean the opposite of the source specification.
        /// </summary>
        /// <returns>
        /// A new specification that is the opposite of the source specification.
        /// </returns>
        public static Specification<TEntity> operator !(Specification<TEntity> spec)
        {
            var result = Expression.Invoke(spec.Predicate, spec.Predicate.Parameters.Cast<Expression>());
            var inverted = Expression.Not(result);
            return new Specification<TEntity>(Expression.Lambda<Func<TEntity, bool>>(inverted, spec.Predicate.Parameters));
        }
        #endregion
    }
}