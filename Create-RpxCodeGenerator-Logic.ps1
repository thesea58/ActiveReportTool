param(
    [string]$Destination = ".\RpxCodeGenerator",
    [string]$TargetFramework = "net8.0",
    [switch]$Force
)

$ErrorActionPreference = "Stop"

if (Test-Path -LiteralPath $Destination) {
    if (-not $Force) {
        throw "Destination '$Destination' already exists. Use -Force to overwrite."
    }
    Remove-Item -LiteralPath $Destination -Recurse -Force
}

$directories = @(
    $Destination,
    (Join-Path $Destination "Models"),
    (Join-Path $Destination "Parsers"),
    (Join-Path $Destination "Generators")
)

foreach ($dir in $directories) {
    New-Item -ItemType Directory -Path $dir -Force | Out-Null
}

$csprojTemplate = @'
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>__TARGET_FRAMEWORK__</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <!-- Exclude output folder from build -->
  <ItemGroup>
    <Compile Remove="output/**/*.cs" />
  </ItemGroup>

</Project>
'@

$programCs = @'
using RpxCodeGenerator.Generators;
using RpxCodeGenerator.Parsers;

// Configuration
const string rpxDirectory = "../rpx_folder_only";
const string outputDirectory = "./output";

// Optional input argument: specific RPX file name or full path
// Example:
//   dotnet run -- KP031110.rpx
//   dotnet run -- ../rpx_folder_only/KP031110.rpx
string? inputArg = args.Length > 0 ? args[0] : null;

// Create output directory if not exists
Directory.CreateDirectory(outputDirectory);

Console.WriteLine("RPX to C# Code Generator Tool");
Console.WriteLine("Convert RPX files to initialization code");
Console.WriteLine();

try
{
    var parser = new RpxParser();
    var codeGenerator = new CodeGenerator();

    // Get all RPX files in the directory
    if (!Directory.Exists(rpxDirectory))
    {
        Console.WriteLine($"Error: Directory not found: {rpxDirectory}");
        Console.WriteLine($"Current directory: {Directory.GetCurrentDirectory()}");
        return;
    }

    var rpxFiles = Directory.GetFiles(rpxDirectory, "*.rpx")
        .OrderBy(f => Path.GetFileName(f))
        .ToList();

    if (rpxFiles.Count == 0)
    {
        Console.WriteLine("No RPX files found in: " + rpxDirectory);
        return;
    }

    Console.WriteLine($"Found {rpxFiles.Count} RPX files");
    Console.WriteLine();

    List<string> filesToProcess;
    if (!string.IsNullOrWhiteSpace(inputArg))
    {
        var candidatePath = inputArg;
        if (!Path.IsPathRooted(candidatePath))
        {
            candidatePath = Path.Combine(rpxDirectory, candidatePath);
        }

        candidatePath = Path.GetFullPath(candidatePath);
        if (!File.Exists(candidatePath))
        {
            Console.WriteLine($"RPX file not found: {inputArg}");
            Console.WriteLine($"Checked path: {candidatePath}");
            return;
        }

        filesToProcess = [candidatePath];
        Console.WriteLine($"Single-file mode: {Path.GetFileName(candidatePath)}");
        Console.WriteLine();
    }
    else
    {
        // Default demo mode
        filesToProcess = rpxFiles.Take(5).ToList();
    }

    var totalSections = 0;
    var totalControls = 0;

    foreach (var rpxFile in filesToProcess)
    {
        var fileName = Path.GetFileName(rpxFile);
        Console.WriteLine($"Processing: {fileName}");

        try
        {
            // Parse RPX file
            var rpxDoc = parser.ParseFile(rpxFile);
            totalSections += rpxDoc.Sections.Count;
            totalControls += rpxDoc.Sections.Sum(s => s.Controls.Count);

            // Generate C# code
            var initCode = codeGenerator.Generate(rpxDoc);
            var summary = codeGenerator.GenerateReportSummary(rpxDoc);
            var typedCode = codeGenerator.GenerateTypedControlsExtraction(rpxDoc);

            // Save generated code
            var baseFileName = Path.GetFileNameWithoutExtension(rpxFile);
            var initCodePath = Path.Combine(outputDirectory, $"{baseFileName}_Initialize.cs");
            var typedCodePath = Path.Combine(outputDirectory, $"{baseFileName}_Controls.cs");
            var summaryPath = Path.Combine(outputDirectory, $"{baseFileName}_Summary.txt");

            File.WriteAllText(initCodePath, initCode);
            File.WriteAllText(typedCodePath, typedCode);
            File.WriteAllText(summaryPath, summary);

            Console.WriteLine($"  Sections: {rpxDoc.Sections.Count}");
            Console.WriteLine($"  Total Controls: {rpxDoc.Sections.Sum(s => s.Controls.Count)}");
            Console.WriteLine($"  Generated: {baseFileName}_Initialize.cs");
            Console.WriteLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  Error: {ex.Message}");
            Console.WriteLine();
        }
    }

    // Summary
    Console.WriteLine("Processing Summary:");
    Console.WriteLine($"  Files processed: {filesToProcess.Count}");
    Console.WriteLine($"  Total sections: {totalSections}");
    Console.WriteLine($"  Total controls: {totalControls}");
    Console.WriteLine($"  Output location: {Path.GetFullPath(outputDirectory)}");
    Console.WriteLine();
    Console.WriteLine("Code generation completed successfully.");
}
catch (Exception ex)
{
    Console.WriteLine($"Fatal error: {ex.Message}");
    Console.WriteLine(ex.StackTrace);
}
'@

$modelsCs = @'
namespace RpxCodeGenerator.Models;

public class RpxSection
{
    public string Type { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public List<RpxControl> Controls { get; set; } = [];

    public override string ToString()
    {
        return $"Section: {Name} ({Type}) with {Controls.Count} controls";
    }
}

public class RpxControl
{
    public string Type { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public Dictionary<string, string> Properties { get; set; } = [];

    public override string ToString()
    {
        return $"Control: {Name} ({Type})";
    }
}

public class RpxDocument
{
    public string DocumentName { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public List<RpxSection> Sections { get; set; } = [];

    public override string ToString()
    {
        return $"RPX Document: {DocumentName} (v{Version}) with {Sections.Count} sections";
    }
}
'@

$parserCs = @'
using System.Xml.Linq;
using RpxCodeGenerator.Models;

namespace RpxCodeGenerator.Parsers;

public class RpxParser
{
    public RpxDocument ParseFile(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"File not found: {filePath}");

        var doc = XDocument.Load(filePath);
        var root = doc.Root;

        if (root == null)
            throw new InvalidOperationException("Invalid RPX file format");

        var rpxDoc = new RpxDocument
        {
            DocumentName = root.Attribute("DocumentName")?.Value ?? Path.GetFileNameWithoutExtension(filePath),
            Version = root.Attribute("Version")?.Value ?? "Unknown"
        };

        var sectionsElement = root.Element("Sections");
        if (sectionsElement != null)
        {
            foreach (var sectionElement in sectionsElement.Elements("Section"))
            {
                var section = ParseSection(sectionElement);
                rpxDoc.Sections.Add(section);
            }
        }

        return rpxDoc;
    }

    private RpxSection ParseSection(XElement sectionElement)
    {
        var section = new RpxSection
        {
            Type = sectionElement.Attribute("Type")?.Value ?? string.Empty,
            Name = sectionElement.Attribute("Name")?.Value ?? string.Empty
        };

        foreach (var controlElement in sectionElement.Elements("Control"))
        {
            var control = ParseControl(controlElement);
            section.Controls.Add(control);
        }

        return section;
    }

    private RpxControl ParseControl(XElement controlElement)
    {
        var control = new RpxControl
        {
            Type = controlElement.Attribute("Type")?.Value ?? string.Empty,
            Name = controlElement.Attribute("Name")?.Value ?? string.Empty
        };

        foreach (var attr in controlElement.Attributes())
        {
            if (attr.Name.LocalName != "Type" && attr.Name.LocalName != "Name")
            {
                control.Properties[attr.Name.LocalName] = attr.Value;
            }
        }

        return control;
    }

    public RpxDocument ParseContent(string xmlContent)
    {
        var doc = XDocument.Parse(xmlContent);
        var root = doc.Root;

        if (root == null)
            throw new InvalidOperationException("Invalid RPX content format");

        var rpxDoc = new RpxDocument
        {
            DocumentName = root.Attribute("DocumentName")?.Value ?? "Unknown",
            Version = root.Attribute("Version")?.Value ?? "Unknown"
        };

        var sectionsElement = root.Element("Sections");
        if (sectionsElement != null)
        {
            foreach (var sectionElement in sectionsElement.Elements("Section"))
            {
                var section = ParseSection(sectionElement);
                rpxDoc.Sections.Add(section);
            }
        }

        return rpxDoc;
    }
}
'@

$generatorCs = @'
using System.Text;
using RpxCodeGenerator.Models;

namespace RpxCodeGenerator.Generators;

public class CodeGenerator
{
    private readonly StringBuilder _builder = new();
    private int _indentLevel = 0;
    private const string INDENT = "    ";

    public string Generate(RpxDocument rpxDoc)
    {
        _builder.Clear();
        _indentLevel = 0;

        WriteLine("// Auto-generated code from RPX file: " + rpxDoc.DocumentName);
        WriteLine("// Generated at: " + DateTime.Now);
        WriteLine();
        WriteLine("namespace YourNamespace.Reports;");
        WriteLine();
        WriteLine("/// <summary>");
        WriteLine("/// Auto-generated report initializer");
        WriteLine("/// </summary>");
        WriteLine($"public partial class {SanitizeClassName(rpxDoc.DocumentName)}Initializer");
        WriteLine("{");
        _indentLevel++;

        GenerateInitializationCode(rpxDoc);

        _indentLevel--;
        WriteLine("}");

        return _builder.ToString();
    }

    private void GenerateInitializationCode(RpxDocument rpxDoc)
    {
        WriteLine("/// <summary>");
        WriteLine("/// Initialize sections and controls from report");
        WriteLine("/// </summary>");
        WriteLine("public void InitializeReportSections()");
        WriteLine("{");
        _indentLevel++;

        foreach (var section in rpxDoc.Sections)
        {
            GenerateSection(section);
            WriteLine();
        }

        _indentLevel--;
        WriteLine("}");
    }

    private void GenerateSection(RpxSection section)
    {
        var varName = char.ToLower(section.Name[0]) + section.Name.Substring(1);

        WriteLine($"// {section.Type}: {section.Name}");
        WriteLine($"var {varName} = _report.Sections[\"{section.Name}\"];");

        if (section.Controls.Count > 0)
        {
            _indentLevel++;
            foreach (var control in section.Controls)
            {
                GenerateControl(varName, control);
            }
            _indentLevel--;
        }
    }

    private void GenerateControl(string sectionVarName, RpxControl control)
    {
        var controlVarName = char.ToLower(control.Name[0]) + control.Name.Substring(1);
        var controlClass = GetControlType(control);

        WriteLine($"var {controlVarName} = {sectionVarName}.Controls[\"{control.Name}\"] as {controlClass};");

        if (control.Properties.Count > 0)
        {
            WriteControlProperties(controlVarName, control);
        }
    }

    private void WriteControlProperties(string varName, RpxControl control)
    {
        var importantProps = new[] { "Visible", "Left", "Top", "Width", "Height" };
        var relevantProps = control.Properties
            .Where(p => importantProps.Contains(p.Key))
            .ToList();

        if (relevantProps.Count == 0)
            return;

        _indentLevel++;
        foreach (var prop in relevantProps)
        {
            var value = FormatPropertyValue(prop.Key, prop.Value);
            WriteLine($"if ({varName} != null) {varName}.{prop.Key} = {value};");
        }
        _indentLevel--;
    }

    private string FormatPropertyValue(string propertyName, string value)
    {
        if (propertyName.Equals("Visible", StringComparison.OrdinalIgnoreCase))
        {
            return value == "0" ? "false" : "true";
        }

        if (int.TryParse(value, out _))
        {
            return value;
        }

        return $"\"{value}\"";
    }

    private string GetControlType(RpxControl control)
    {
        return control.Type switch
        {
            "AR.Label" => "Label",
            "AR.Field" => "TextField",
            "AR.TextBox" => "TextBox",
            "AR.Line" => "Line",
            "AR.Rectangle" => "Rectangle",
            "AR.Image" => "Image",
            "AR.CheckBox" => "CheckBox",
            "AR.ComboBox" => "ComboBox",
            _ => "ARControl"
        };
    }

    public string GenerateTypedControlsExtraction(RpxDocument rpxDoc)
    {
        _builder.Clear();
        _indentLevel = 0;

        WriteLine("// Auto-generated type-safe control extraction");
        WriteLine("// Generated at: " + DateTime.Now);
        WriteLine();
        WriteLine("namespace YourNamespace.Reports;");
        WriteLine();
        WriteLine("/// <summary>");
        WriteLine("/// Auto-generated control accessor");
        WriteLine("/// </summary>");
        WriteLine($"public partial class {SanitizeClassName(rpxDoc.DocumentName)}Controls");
        WriteLine("{");
        _indentLevel++;

        foreach (var section in rpxDoc.Sections)
        {
            if (section.Controls.Count == 0)
                continue;

            var sectionVarName = char.ToLower(section.Name[0]) + section.Name.Substring(1);
            WriteLine($"/// <summary>Extract controls from {section.Name}</summary>");
            WriteLine($"public void Extract{section.Name}Controls()");
            WriteLine("{");
            _indentLevel++;

            WriteLine($"var {sectionVarName} = _report.Sections[\"{section.Name}\"];");
            WriteLine();

            var textBoxControls = section.Controls.Where(c => c.Type == "AR.Field" || c.Type == "AR.TextBox");
            foreach (var control in textBoxControls)
            {
                var varName = char.ToLower(control.Name[0]) + control.Name.Substring(1);
                WriteLine($"var {varName} = {sectionVarName}.Controls[\"{control.Name}\"] as TextBox;");
            }

            _indentLevel--;
            WriteLine("}");
            WriteLine();
        }

        _indentLevel--;
        WriteLine("}");

        return _builder.ToString();
    }

    public string GenerateReportSummary(RpxDocument rpxDoc)
    {
        _builder.Clear();
        _indentLevel = 0;

        WriteLine("// RPX Report Summary");
        WriteLine($"// Document: {rpxDoc.DocumentName}");
        WriteLine($"// Version: {rpxDoc.Version}");
        WriteLine();
        WriteLine("Report Structure:");
        WriteLine("-" + new string('-', 50));

        foreach (var section in rpxDoc.Sections)
        {
            WriteLine($"  Section: {section.Name} ({section.Type})");
            WriteLine($"    Controls: {section.Controls.Count}");

            var controlsByType = section.Controls.GroupBy(c => c.Type);
            foreach (var group in controlsByType)
            {
                WriteLine($"      - {GetControlType(group.First())}: {group.Count()}");
            }
        }

        WriteLine("-" + new string('-', 50));

        return _builder.ToString();
    }

    private void WriteLine(string text = "")
    {
        _builder.AppendLine(GetIndent() + text);
    }

    private string GetIndent()
    {
        return string.Concat(Enumerable.Repeat(INDENT, _indentLevel));
    }

    private string SanitizeClassName(string name)
    {
        var sanitized = System.Text.RegularExpressions.Regex.Replace(name, @"[^a-zA-Z0-9_]", "");

        if (string.IsNullOrEmpty(sanitized) || char.IsDigit(sanitized[0]))
        {
            sanitized = "_" + sanitized;
        }

        return sanitized;
    }
}
'@

$files = @{
    (Join-Path $Destination "RpxCodeGenerator.csproj") = ($csprojTemplate -replace "__TARGET_FRAMEWORK__", $TargetFramework)
    (Join-Path $Destination "Program.cs") = $programCs
    (Join-Path $Destination "Models/RpxSection.cs") = $modelsCs
    (Join-Path $Destination "Parsers/RpxParser.cs") = $parserCs
    (Join-Path $Destination "Generators/CodeGenerator.cs") = $generatorCs
}

$utf8NoBom = New-Object System.Text.UTF8Encoding($false)
foreach ($path in $files.Keys) {
    $parent = Split-Path -Parent $path
    if (-not (Test-Path -LiteralPath $parent)) {
        New-Item -ItemType Directory -Path $parent -Force | Out-Null
    }

    [System.IO.File]::WriteAllText($path, $files[$path], $utf8NoBom)
    Write-Host "Created: $path"
}

Write-Host ""
Write-Host "Done. Project scaffold created at: $Destination"
Write-Host "Next steps:"
Write-Host "  1) cd $Destination"
Write-Host "  2) dotnet build"
Write-Host "  3) dotnet run -- KP031110.rpx"
