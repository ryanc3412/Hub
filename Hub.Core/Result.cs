using System;
using Hub.Core.Enums;
using System.Net.NetworkInformation;

namespace Hub.Core;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string Error { get; }
    public ErrorType ErrorType { get; }

    // Protected constructor to enforce use of factory methods
    protected Result(bool isSuccess, string error)
    {
        if (isSuccess && !string.IsNullOrEmpty(error))
            throw new InvalidOperationException("A successful result cannot have an error message.");
        if (!isSuccess && string.IsNullOrEmpty(error))
            throw new ArgumentException("A failure result must contain an error message.", nameof(error));

        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new (true, string.Empty);
    public static Result Failure(string error) => new (false, error);
}


public class Result<T> : Result
{
    private readonly T? _value;

    public T value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("Cannot access the value of a failed result.");

    protected internal Result(T? value, bool isSuccess, string error) : base(isSuccess, error)
    {
        _value = value;
    }

    public static Result<T> Success(T value) => new(value, true, string.Empty);
    public static new Result<T> Failure(string error) => new(default, false, error);

    public static implicit operator Result<T>(T value) => Success(value);
    public static implicit operator Result<T>(string error) => Failure(error);
}