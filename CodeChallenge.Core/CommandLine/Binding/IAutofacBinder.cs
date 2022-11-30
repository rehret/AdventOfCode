namespace CodeChallenge.Core.CommandLine.Binding;

using System.CommandLine.Binding;

public interface IAutofacBinder<out T> : IValueDescriptor<T> { }
