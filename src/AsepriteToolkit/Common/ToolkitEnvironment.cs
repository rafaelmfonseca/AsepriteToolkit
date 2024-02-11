using Microsoft.Win32;

namespace AsepriteToolkit.Common;

public class ToolkitEnvironment
{
    public enum SpecialFile : ulong
    {
        AsepriteConfig = 0x01,
        AsepriteExecutable = 0x02,
        DropboxInfo = 0x03,
    }

    public static string GetFilePath(SpecialFile file) => file switch
        {
            SpecialFile.AsepriteConfig => Path.Join(AppDataPath, "Aseprite", "aseprite.ini"),
            SpecialFile.AsepriteExecutable => Path.Join(SteamPath, "steamapps", "common", "Aseprite", "Aseprite.exe"),
            SpecialFile.DropboxInfo => Path.Join(LocalAppDataPath, "Dropbox", "info.json"),
            _ => throw new ArgumentOutOfRangeException(nameof(file), file, null)
        };

    public static string AppDataPath
        => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

    public static string LocalAppDataPath
        => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

#pragma warning disable CS8602, CA1416, CS8600, CS8603
    public static string SteamPath
        => (string) Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Valve").OpenSubKey("Steam").GetValue("SteamPath");
#pragma warning restore CS8602, CA1416, CS8600, CS8603

}
