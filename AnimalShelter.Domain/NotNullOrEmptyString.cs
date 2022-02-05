using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalShelter.Domain
{
    public class NotNullOrEmptyString : ValueObject
    {
        public string Value { get; }

        private NotNullOrEmptyString(string value)
        {
            Value = value;
        }

        public static Result<NotNullOrEmptyString> Create(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return Result.Failure<NotNullOrEmptyString>("Value for NotNullOrEmptyString type cannot be null or empty");
            }

            return Result.Success(new NotNullOrEmptyString(value));
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
