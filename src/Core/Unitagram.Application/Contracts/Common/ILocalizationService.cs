namespace Unitagram.Application.Contracts.Common;

public interface ILocalizationService
{
    string GetLocalizedString(string key);
    string this[string key] { get; }
}