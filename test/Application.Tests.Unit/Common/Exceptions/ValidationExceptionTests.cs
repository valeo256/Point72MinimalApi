using FluentAssertions;
using FluentValidation.Results;
using Xunit;

namespace Application.Tests.Unit.Common.Exceptions;

public class ValidationExceptionTests
{
    [Fact]
    public void DefaultConstructorCreatesAnEmptyErrorDictionary()
    {
        var actual = new Application.Common.Exceptions.ValidationException().Errors;

        actual.Keys.Should().BeEquivalentTo(Array.Empty<string>());
    }

    [Fact]
    public void SingleValidationFailureCreatesASingleElementErrorDictionary()
    {
        var failures = new List<ValidationFailure>
            {
                new ValidationFailure("Length", "Must be greater 2 characters"),
            };

        var actual = new Application.Common.Exceptions.ValidationException(failures).Errors;

        actual.Keys.Should().BeEquivalentTo(new string[] { "Length" });
        actual["Length"].Should().BeEquivalentTo(new string[] { "Must be greater 2 characters" });
    }

    [Fact]
    public void MulitpleValidationFailureForMultiplePropertiesCreatesAMultipleElementErrorDictionaryEachWithMultipleValues()
    {
        var failures = new List<ValidationFailure>
            {
                new ValidationFailure("Length", "Must be greater 2 characters"),
                new ValidationFailure("Length", "Must be greater 5 characters"),
                new ValidationFailure("Password", "must contain at least 8 characters"),
                new ValidationFailure("Password", "must contain a digit"),
                new ValidationFailure("Password", "must contain upper case letter"),
                new ValidationFailure("Password", "must contain lower case letter"),
            };

        var actual = new Application.Common.Exceptions.ValidationException(failures).Errors;

        actual.Keys.Should().BeEquivalentTo(new string[] { "Password", "Length" });

        actual["Length"].Should().BeEquivalentTo(new string[]
        {
                "Must be greater 5 characters",
                "Must be greater 2 characters",
        });

        actual["Password"].Should().BeEquivalentTo(new string[]
        {
                "must contain lower case letter",
                "must contain upper case letter",
                "must contain at least 8 characters",
                "must contain a digit",
        });
    }
}
