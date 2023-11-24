using System.Reflection;
using Microsoft.Extensions.Localization;
using Unitagram.Application.Contracts.Common;

namespace Unitagram.Infrastructure.Localization;

public sealed class LocalizationService : ILocalizationService
{
    private readonly IStringLocalizer _localizer;

    public LocalizationService(IStringLocalizerFactory factory)
    {
        var type = typeof(Resources_en);
        var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName!);
        _localizer = factory.Create("Resources", assemblyName.Name!);
    }

    public string this[string key] => _localizer[key];


    public string GetLocalizedString(string key)
    {
        return _localizer[key];
    }
}