namespace AsepriteToolkit.Common;

public class ToolkitEnvironment
{
    public enum SpecialFile : ulong
    {
        AsepriteConfig = 0x01,
        DropboxInfo = 0x02,
    }

    public static string GetFilePath(SpecialFile file) => file switch
        {
            SpecialFile.AsepriteConfig => Path.Join(AppDataPath, "Aseprite", "aseprite.ini"),
            SpecialFile.DropboxInfo => Path.Join(LocalAppDataPath, "Dropbox", "info.json"),
            _ => throw new ArgumentOutOfRangeException(nameof(file), file, null)
        };

    public static string AppDataPath
        => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

    public static string LocalAppDataPath
        => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

}
