# Quick Reference Guide

## 🚀 Quick Start

### 1. Build Project
```bash
cd RpxCodeGenerator
dotnet build
```

### 2. Run Demo
```bash
dotnet run
```

### Output Location
```
RpxCodeGenerator/output/
├── {FileName}_Initialize.cs   # Full initialization code
├── {FileName}_Controls.cs     # Controls extraction (type-safe)
└── {FileName}_Summary.txt     # Report structure summary
```

---

## 💡 Basic Usage

### Single File Processing
```csharp
using RpxCodeGenerator.Parsers;
using RpxCodeGenerator.Generators;

var parser = new RpxParser();
var generator = new CodeGenerator();

// Parse RPX file
var rpxDoc = parser.ParseFile("path/to/report.rpx");

// Generate code
var initCode = generator.Generate(rpxDoc);
var controlCode = generator.GenerateTypedControlsExtraction(rpxDoc);

Console.WriteLine(initCode);
Console.WriteLine(controlCode);
```

---

## 🔥 Common Tasks

### Task 1: Process Multiple RPX Files
```csharp
var parser = new RpxParser();
var generator = new CodeGenerator();

var rpxFiles = Directory.GetFiles("./rpx_folder", "*.rpx");

foreach (var rpxFile in rpxFiles)
{
    var rpxDoc = parser.ParseFile(rpxFile);
    var code = generator.Generate(rpxDoc);
    
    var fileName = Path.GetFileNameWithoutExtension(rpxFile);
    File.WriteAllText($"{fileName}_Init.cs", code);
}
```

### Task 2: Filter Controls by Type
```csharp
var rpxDoc = parser.ParseFile("report.rpx");

foreach (var section in rpxDoc.Sections)
{
    var fieldControls = section.Controls
        .Where(c => c.Type == "AR.Field")
        .ToList();

    Console.WriteLine($"Section: {section.Name}");
    foreach (var control in fieldControls)
    {
        Console.WriteLine($"  - {control.Name}");
    }
}
```

### Task 3: Generate Summary Report
```csharp
var rpxDoc = parser.ParseFile("report.rpx");
var summary = generator.GenerateReportSummary(rpxDoc);

Console.WriteLine(summary);

// Or save to file
File.WriteAllText("report_summary.txt", summary);
```

### Task 4: Extract Control Information
```csharp
var rpx Doc = parser.ParseFile("report.rpx");

var totalControls = rpxDoc.Sections
    .Sum(s => s.Controls.Count);

var controlsByType = rpxDoc.Sections
    .SelectMany(s => s.Controls)
    .GroupBy(c => c.Type);

foreach (var group in controlsByType)
{
    Console.WriteLine($"{group.Key}: {group.Count()}");
}
```

---

## 📊 Data Models

### RpxDocument
```csharp
public class RpxDocument
{
    public string DocumentName { get; set; }
    public string Version { get; set; }
    public List<RpxSection> Sections { get; set; }
}
```

### RpxSection
```csharp
public class RpxSection
{
    public string Type { get; set; }              // ReportHeader, PageHeader, Detail, etc.
    public string Name { get; set; }              // Section1, Section2, etc.
    public List<RpxControl> Controls { get; set; }
}
```

### RpxControl
```csharp
public class RpxControl
{
    public string Type { get; set; }              // AR.Label, AR.Field, etc.
    public string Name { get; set; }              // Control name
    public Dictionary<string, string> Properties { get; set; }
}
```

---

## 🎯 Generated Code Examples

### Example 1: Initialize Code
```csharp
// Auto-generated code from RPX file: KP010020
public void InitializeReportSections()
{
    // PageHeader: Section2
    var section2 = _report.Sections["Section2"];
        var field28 = section2.Controls["Field28"] as TextField;
        var field29 = section2.Controls["Field29"] as TextBox;
        var field30 = section2.Controls["Field30"] as TextBox;
        var field31 = section2.Controls["Field31"] as TextBox;
}
```

### Example 2: Controls Extraction Code
```csharp
// Type-safe control extraction
var section2 = _report.Sections["Section2"];
    var field28 = section2.Controls["Field28"] as TextBox;
    var field29 = section2.Controls["Field29"] as TextBox;
    var field30 = section2.Controls["Field30"] as TextBox;
    var field31 = section2.Controls["Field31"] as TextBox;
```

### Example 3: Summary Report
```
RPX Report Summary
Document: KP010020
Version: 3.2

Report Structure:
─────────────────────────────────
  Section: Section2 (PageHeader)
    Controls: 15
      - Label: 5
      - TextField: 9
      - Line: 1
```

---

## 🛠️ API Reference

### RpxParser
```csharp
public class RpxParser
{
    // Parse từ file path
    public RpxDocument ParseFile(string filePath)
    
    // Parse từ XML string
    public RpxDocument ParseContent(string xmlContent)
}
```

### CodeGenerator
```csharp
public class CodeGenerator
{
    // Sinh initialization code
    public string Generate(RpxDocument rpxDoc)
    
    // Sinh type-safe controls extraction
    public string GenerateTypedControlsExtraction(RpxDocument rpxDoc)
    
    // Sinh summary report
    public string GenerateReportSummary(RpxDocument rpxDoc)
}
```

---

## 📁 File Organization

```
RpxCodeGenerator/
├── Models/
│   └── RpxSection.cs              # Data models
├── Parsers/
│   └── RpxParser.cs               # RPX parsing logic
├── Generators/
│   └── CodeGenerator.cs           # Code generation logic
├── Examples/
│   └── AdvancedUsageExamples.cs   # 7 usage examples
├── Program.cs                      # Main console app
└── output/                         # Generated files
```

---

## 🔄 Processing Pipeline

```
RPX File (XML)
    ↓
RpxParser.ParseFile()
    ↓
RpxDocument (Objects)
    ↓
CodeGenerator.Generate()
    ↓
C# Code + Summary
```

---

## ⚙️ Configuration

Edit `Program.cs` to customize:

```csharp
// Directory containing RPX files
const string rpxDirectory = "../rpx_folder_only";

// Output directory for generated code
const string outputDirectory = "./output";

// Number of files to process
var filesToProcess = rpxFiles.Take(5).ToList();  // Change 5 as needed
```

---

## 🐛 Error Handling

```csharp
try
{
    var rpxDoc = parser.ParseFile("report.rpx");
    var code = generator.Generate(rpxDoc);
}
catch (FileNotFoundException)
{
    Console.WriteLine("RPX file not found");
}
catch (InvalidOperationException)
{
    Console.WriteLine("Invalid RPX format");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
```

---

## 📈 Performance Tips

1. **Batch Processing**: Process multiple files in parallel
2. **File Caching**: Cache parsed documents if processing multiple times
3. **Streaming**: For large files, use string streaming instead of loading all into memory

```csharp
// Example: Parallel processing
Parallel.ForEach(rpxFiles, rpxFile =>
{
    var rpxDoc = parser.ParseFile(rpxFile);
    var code = generator.Generate(rpxDoc);
    // Save code...
});
```

---

## 🎁 Advanced Examples

See `Examples/AdvancedUsageExamples.cs` for:
1. Single file generation
2. Batch processing
3. Analyze sections
4. Filter controls by type
5. Custom code templates
6. Compare two RPX files
7. Export to CSV

---

## 📞 Support

- Check `README.md` for detailed documentation
- Review `Examples/` folder for usage patterns
- Check `Models/` for data structure definitions

---

## ✨ Next Steps

1. **Integrate into your project**: Copy `Models/`, `Parsers/`, and `Generators/` folders
2. **Add to existing codebase**: Reference the generated code in your report classes
3. **Automate generation**: Add as build-time tool or scheduled task

