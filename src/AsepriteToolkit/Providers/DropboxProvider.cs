using AsepriteToolkit.Common;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AsepriteToolkit.Providers;

public sealed class DropboxProvider
{
    private readonly DropboxInfo _info;

    public DropboxProvider()
    {
        var path = ToolkitEnvironment.GetFilePath(ToolkitEnvironment.SpecialFile.DropboxInfo);

        if (string.IsNullOrEmpty(path) || !File.Exists(path))
        {
            throw new FileNotFoundException("Dropbox info file not found.");
        }

        var source = JsonSerializer.Deserialize<DropboxInfo>(File.ReadAllText(path));

        if (source is null)
        {
            throw new InvalidOperationException("Failed to deserialize Dropbox info.");
        }

        _info = source;
    }

    public string[] GetAllFiles(string[] extensions)
    {
        return Directory.GetFiles(_info.Personal.Path, "*.*", SearchOption.AllDirectories)
            .Where(file => extensions.Contains(Path.GetExtension(file)))
            .OrderBy(file => file)
            .ToArray();
    }

    public record DropboxInfo(
        [property: JsonPropertyName("personal")] DropboxProfile Personal);

    public record DropboxProfile(
        [property: JsonPropertyName("path")] string Path,
        [property: JsonPropertyName("host")] long Host,
        [property: JsonPropertyName("is_team")] bool IsTeam,
        [property: JsonPropertyName("subscription_type")] string SubscriptionType);
}
