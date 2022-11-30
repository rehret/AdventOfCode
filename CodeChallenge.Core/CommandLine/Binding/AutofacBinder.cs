namespace CodeChallenge.Core.CommandLine.Binding;

using System.CommandLine.Binding;

using Autofac;

public class AutofacBinder<T> : BinderBase<T>, IAutofacBinder<T>
    where T : notnull
{
    private readonly ILifetimeScope _lifetimeScope;

    public AutofacBinder(ILifetimeScope lifetimeScope)
    {
        _lifetimeScope = lifetimeScope;
    }

    protected override T GetBoundValue(BindingContext bindingContext)
    {
        return _lifetimeScope.Resolve<T>();
    }
}