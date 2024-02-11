using System.Diagnostics;
using System.Text;
using AsepriteToolkit.Common;
using IniParser;
using IniParser.Model;

namespace AsepriteToolkit.Providers;

public sealed class AsepriteProvider
{
    private readonly FileIniDataParser _parser = new();
    private readonly IniData _data;
    private readonly string _path;

    public AsepriteProvider()
    {
        _path = ToolkitEnvironment.GetFilePath(ToolkitEnvironment.SpecialFile.AsepriteConfig);

        if (string.IsNullOrEmpty(_path) || !File.Exists(_path))
        {
            throw new FileNotFoundException("Aseprite config file not found.");
        }

        _data = _parser.ReadFile(_path);
    }

    public void SetRecentFiles(string[] files)
    {
        const string SectionName = "RecentFiles";

        _data.Sections.RemoveSection(SectionName);
        _data.Sections.AddSection(SectionName);

        var section = _data.Sections[SectionName];

        for (int i = 0; i < files.Length; i++)
        {
            var key = Convert.ToString(i).PadLeft(4, '0');

            section.AddKey(key, files[i]);
        }
    }

    public void ClearPinnedFiles() => _data.Sections.RemoveSection("PinnedFiles");

    public void Run()
    {
        var path = ToolkitEnvironment.GetFilePath(ToolkitEnvironment.SpecialFile.AsepriteExecutable);

        Process.Start(path);
    }

    public void Save() => _parser.WriteFile(_path, _data, Encoding.UTF8);
}
