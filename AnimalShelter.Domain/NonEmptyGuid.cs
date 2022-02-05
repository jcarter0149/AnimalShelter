using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace AnimalShelter.Domain
{
    public class NonEmptyGuid : ValueObject
    {

        public Guid Value { get; }
        private NonEmptyGuid(Guid value)
        {
            Value = value;
        }

        public static Result<NonEmptyGuid> Create(Guid value)
        {
            if(value == Guid.Empty)
            {
                return Result.Failure<NonEmptyGuid>("Value for NonEmptyGuid type cannot be empty");
            }

            return Result.Success(new NonEmptyGuid(value));
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
