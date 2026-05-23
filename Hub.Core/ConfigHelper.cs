namespace Hub.Core;

/// <summary>
/// Utility helper for configuration-related operations.
/// For new code, prefer using IOptions&lt;T&gt; injection with dependency injection
/// instead of static configuration access.
/// </summary>
public class ConfigHelper
{
    public static string GetErrorMessage(string fieldName)
    {
        return $"Configuration value '{fieldName}' not found or is empty.";
    }
}
