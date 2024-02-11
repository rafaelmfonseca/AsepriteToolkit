using AsepriteToolkit.Providers;

try
{
    var dropbox = new DropboxProvider();

    var files = dropbox.GetAllFiles([".ase", ".aseprite", ".png"]);

    var aseprite = new AsepriteProvider();

    aseprite.SetRecentFiles(files);
    aseprite.ClearPinnedFiles();

    aseprite.Save();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}