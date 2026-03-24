# RPX Code Generator Tool

## 📋 Mô tả

Công cụ tự động chuyển đổi file **RPX** (XML format của ActiveReports) sang code C# để khởi tạo sections và controls của report.

### Chức năng chính:
- ✅ Parse file RPX (XML)
- ✅ Trích xuất cấu trúc Section và Control
- ✅ Tự động sinh code C# initialization
- ✅ Type casting tự động cho Controls
- ✅ Xuất ra file code và summary

## 🏗️ Cấu trúc Project

```
RpxCodeGenerator/
├── Models/
│   └── RpxSection.cs          # Data models cho RPX structure
├── Parsers/
│   └── RpxParser.cs           # Parser RPX files
├── Generators/
│   └── CodeGenerator.cs       # Sinh code C# từ RPX
├── Program.cs                 # Main entry point
└── output/                    # Generated code output
    ├── {FileName}_Initialize.cs    # Initialize code
    ├── {FileName}_Controls.cs      # Controls extraction code
    └── {FileName}_Summary.txt      # Report summary
```

## 🛠️ Công nghệ

- **.NET 10.0** (or compatible)
- **System.Xml.Linq** (for XML parsing)
- **C# 12** features

## 📝 Cách sử dụng

### 1. Build project
```bash
cd RpxCodeGenerator
dotnet build
```

### 2. Chạy ứng dụng
```bash
dotnet run
```

### 3. Output
Các file code sẽ được tạo trong folder `output/`:
- `{FileName}_Initialize.cs` - Code khởi tạo sections
- `{FileName}_Controls.cs` - Code lấy controls với type casting
- `{FileName}_Summary.txt` - Thông tin tóm tắt

## 📋 Ví dụ Output

### Input: KP010020.rpx
```xml
<Section Type="PageHeader" Name="Section2">
  <Control Type="AR.Label" Name="Text2" Left="3345" Top="1395" ... />
  <Control Type="AR.Field" Name="Field28" Left="..." Top="..." ... />
  <Control Type="AR.Field" Name="Field29" Left="..." Top="..." ... />
</Section>
```

### Output: Initialize.cs
```csharp
public void InitializeReportSections()
{
    // PageHeader: Section2
    var section2 = _report.Sections["Section2"];
        var text2 = section2.Controls["Text2"] as Label;
            if (text2 != null) text2.Left = 3345;
            if (text2 != null) text2.Top = 1395;
        var field28 = section2.Controls["Field28"] as TextField;
            if (field28 != null) field28.Left = ...;
        var field29 = section2.Controls["Field29"] as TextField;
            if (field29 != null) field29.Left = ...;
}
```

### Output: Controls.cs
```csharp
// Extract controls from Section2
var section2 = _report.Sections["Section2"];
var text2 = section2.Controls["Text2"] as Label;
var field28 = section2.Controls["Field28"] as TextBox;
var field29 = section2.Controls["Field29"] as TextBox;
var field30 = section2.Controls["Field30"] as TextBox;
var field31 = section2.Controls["Field31"] as TextBox;
```

## 🔧 Classes và Interfaces

### Models

#### `RpxDocument`
```csharp
public class RpxDocument
{
    public string DocumentName { get; set; }
    public string Version { get; set; }
    public List<RpxSection> Sections { get; set; }
}
```

#### `RpxSection`
```csharp
public class RpxSection
{
    public string Type { get; set; }           // ReportHeader, PageHeader, Detail, etc.
    public string Name { get; set; }           // Section1, Section2, etc.
    public List<RpxControl> Controls { get; set; }
}
```

#### `RpxControl`
```csharp
public class RpxControl
{
    public string Type { get; set; }           // AR.Label, AR.Field, AR.TextBox, etc.
    public string Name { get; set; }           // Control name
    public Dictionary<string, string> Properties { get; set; }
}
```

### Parsers

#### `RpxParser`
```csharp
public class RpxParser
{
    // Parse từ file
    public RpxDocument ParseFile(string filePath);
    
    // Parse từ XML string
    public RpxDocument ParseContent(string xmlContent);
}
```

### Generators

#### `CodeGenerator`
```csharp
public class CodeGenerator
{
    // Sinh code initialization
    public string Generate(RpxDocument rpxDoc);
    
    // Sinh code lấy controls
    public string GenerateTypedControlsExtraction(RpxDocument rpxDoc);
    
    // Sinh summary report
    public string GenerateReportSummary(RpxDocument rpxDoc);
}
```

## 🎯 Control Type Mapping

| RPX Type | C# Type |
|----------|---------|
| AR.Label | Label |
| AR.Field | TextField |
| AR.TextBox | TextBox |
| AR.Line | Line |
| AR.Rectangle | Rectangle |
| AR.Image | Image |
| AR.CheckBox | CheckBox |
| AR.ComboBox | ComboBox |

## 💡 Ví dụ sử dụng trong code

```csharp
using RpxCodeGenerator.Parsers;
using RpxCodeGenerator.Generators;

// Tạo parser và generator
var parser = new RpxParser();
var generator = new CodeGenerator();

// Parse file RPX
var rpxDoc = parser.ParseFile("path/to/report.rpx");

// Sinh code C#
var initCode = generator.Generate(rpxDoc);
var controlCode = generator.GenerateTypedControlsExtraction(rpxDoc);
var summary = generator.GenerateReportSummary(rpxDoc);

// Lưu code
File.WriteAllText("InitializeCode.cs", initCode);
File.WriteAllText("ControlsCode.cs", controlCode);
File.WriteAllText("Summary.txt", summary);
```

## 📊 Sample Report Summary

```
RPX Report Summary
Document: KP010020
Version: 3.2

Report Structure:
──────────────────────────────────────────────
  Section: Section1 (ReportHeader)
    Controls: 0
  Section: Section2 (PageHeader)
    Controls: 16
      - Label: 5
      - TextField: 11
  Section: Section3 (Detail)
    Controls: 5
      - TextField: 5
      - Line: 1
  Section: Section5 (PageFooter)
    Controls: 0
  Section: Section4 (ReportFooter)
    Controls: 0
──────────────────────────────────────────────
```

## 🚀 Features

### Core Features
- ✅ Parse RPX XML files
- ✅ Extract sections and controls
- ✅ Type-safe code generation
- ✅ Support for property initialization
- ✅ Batch processing multiple files

### Advanced Features
- ✅ Handle special characters in control names
- ✅ Property filtering for cleaner code
- ✅ Summary report generation
- ✅ Error handling and logging

## 📌 Limitations

- Hiện tại sinh code cho paragraph 2 cấp (Sections -> Controls)
- Nested controls chưa được hỗ trợ
- Một số thuộc tính phức tạp có thể cần điều chỉnh thủ công

## 🔄 Quy trình hoạt động

```
RPX File (XML)
     ↓
  RpxParser
     ↓
 RpxDocument (Objects)
     ↓
CodeGenerator
     ↓
C# Code + Summary
```

## 📂 Configuration

Sửa trong `Program.cs`:

```csharp
// Đường dẫn folder chứa RPX files
const string rpxDirectory = "../rpx_folder_only";

// Đường dẫn folder output
const string outputDirectory = "./output";

// Số lượng files để xử lý
var filesToProcess = rpxFiles.Take(5).ToList(); // Thay 5 bằng số cần thiết
```

## 🐛 Troubleshooting

### Error: "Directory not found"
Kiểm tra đường dẫn `rpxDirectory` trong `Program.cs`

### Error: "Invalid RPX file format"
Đảm bảo file RPX là valid XML format

### Generated code syntax errors
Một số tên control chứa ký tự đặc biệt có thể cần điều chỉnh thủ công

## 📝 License

This tool is created for converting ActiveReports RPX files to C# code.

## 🤝 Support

Cần hỗ trợ hoặc tính năng bổ sung?
- Kiểm tra cấu trúc RPX file
- Mở issue với chi tiết về lỗi
