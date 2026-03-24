using RpxCodeGenerator.Generators;
using RpxCodeGenerator.Models;
using RpxCodeGenerator.Parsers;

namespace RpxCodeGenerator.Examples;

/// <summary>
/// Advanced usage examples của RPX Code Generator
/// </summary>
public class AdvancedUsageExamples
{
    private readonly RpxParser _parser = new();
    private readonly CodeGenerator _generator = new();

    /// <summary>
    /// Example 1: Parse single file và sinh code
    /// </summary>
    public void Example1_SingleFileGeneration(string rpxFilePath)
    {
        Console.WriteLine("=== Example 1: Single File Generation ===\n");

        try
        {
            // Parse RPX file
            var rpxDoc = _parser.ParseFile(rpxFilePath);

            // Generate initialization code
            var initCode = _generator.Generate(rpxDoc);

            // Generate controls extraction code
            var controlCode = _generator.GenerateTypedControlsExtraction(rpxDoc);

            // Display results
            Console.WriteLine(initCode);
            Console.WriteLine("\n--- Controls Extraction ---\n");
            Console.WriteLine(controlCode);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Example 2: Batch processing multiple files
    /// </summary>
    public void Example2_BatchProcessing(string rpxDirectory, string outputDirectory)
    {
        Console.WriteLine("=== Example 2: Batch Processing ===\n");

        Directory.CreateDirectory(outputDirectory);

        var rpxFiles = Directory.GetFiles(rpxDirectory, "*.rpx");
        var statistics = new Dictionary<string, int>
        {
            { "TotalFiles", rpxFiles.Length },
            { "TotalSections", 0 },
            { "TotalControls", 0 },
            { "SuccessCount", 0 },
            { "ErrorCount", 0 }
        };

        foreach (var rpxFile in rpxFiles)
        {
            try
            {
                var rpxDoc = _parser.ParseFile(rpxFile);
                var fileName = Path.GetFileNameWithoutExtension(rpxFile);

                // Generate and save code
                var initCode = _generator.Generate(rpxDoc);
                File.WriteAllText(Path.Combine(outputDirectory, $"{fileName}_Init.cs"), initCode);

                // Update statistics
                statistics["TotalSections"] += rpxDoc.Sections.Count;
                statistics["TotalControls"] += rpxDoc.Sections.Sum(s => s.Controls.Count);
                statistics["SuccessCount"]++;

                Console.WriteLine($"✓ {fileName}");
            }
            catch (Exception ex)
            {
                statistics["ErrorCount"]++;
                Console.WriteLine($"✗ {Path.GetFileName(rpxFile)} - {ex.Message}");
            }
        }

        // Display summary
        Console.WriteLine("\n--- Summary ---");
        foreach (var stat in statistics)
        {
            Console.WriteLine($"{stat.Key}: {stat.Value}");
        }
    }

    /// <summary>
    /// Example 3: Analyze specific sections trong RPX
    /// </summary>
    public void Example3_AnalyzeSections(string rpxFilePath)
    {
        Console.WriteLine("=== Example 3: Analyze Sections ===\n");

        var rpxDoc = _parser.ParseFile(rpxFilePath);

        foreach (var section in rpxDoc.Sections)
        {
            Console.WriteLine($"\n📍 Section: {section.Name}");
            Console.WriteLine($"   Type: {section.Type}");
            Console.WriteLine($"   Controls: {section.Controls.Count}");

            var controlsByType = section.Controls.GroupBy(c => c.Type);
            foreach (var group in controlsByType)
            {
                Console.WriteLine($"     - {group.Key}: {group.Count()} controls");

                // Show control names
                foreach (var control in group.Take(3))
                {
                    Console.WriteLine($"       • {control.Name}");
                }

                if (group.Count() > 3)
                {
                    Console.WriteLine($"       ... and {group.Count() - 3} more");
                }
            }
        }
    }

    /// <summary>
    /// Example 4: Filter và export specific controls
    /// </summary>
    public void Example4_FilterControls(string rpxFilePath, string controlType = "AR.Field")
    {
        Console.WriteLine($"=== Example 4: Filter Controls (Type: {controlType}) ===\n");

        var rpxDoc = _parser.ParseFile(rpxFilePath);

        Console.WriteLine($"Searching for controls of type: {controlType}\n");

        foreach (var section in rpxDoc.Sections)
        {
            var filteredControls = section.Controls
                .Where(c => c.Type == controlType)
                .ToList();

            if (filteredControls.Count == 0)
                continue;

            Console.WriteLine($"📍 {section.Name}:");
            foreach (var control in filteredControls)
            {
                Console.WriteLine($"  - {control.Name}");
                if (control.Properties.ContainsKey("DataField"))
                {
                    Console.WriteLine($"    DataField: {control.Properties["DataField"]}");
                }
            }
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Example 5: Generate custom code template
    /// </summary>
    public void Example5_CustomCodeTemplate(string rpxFilePath)
    {
        Console.WriteLine("=== Example 5: Custom Code Template ===\n");

        var rpxDoc = _parser.ParseFile(rpxFilePath);

        // Generate custom template
        var customCode = GenerateCustomTemplate(rpxDoc);
        Console.WriteLine(customCode);
    }

    /// <summary>
    /// Generate custom code template cho Unit Testing
    /// </summary>
    private string GenerateCustomTemplate(RpxDocument rpxDoc)
    {
        var builder = new System.Text.StringBuilder();

        builder.AppendLine("// Unit Test Template for Report Initialization");
        builder.AppendLine($"// Report: {rpxDoc.DocumentName}");
        builder.AppendLine();
        builder.AppendLine("[TestClass]");
        builder.AppendLine($"public class {rpxDoc.DocumentName}ReportTests");
        builder.AppendLine("{");
        builder.AppendLine("    private Report _report;");
        builder.AppendLine();
        builder.AppendLine("    [TestInitialize]");
        builder.AppendLine("    public void Setup()");
        builder.AppendLine("    {");
        builder.AppendLine("        _report = new Report();");
        builder.AppendLine("    }");
        builder.AppendLine();

        // Generate test methods for each section
        foreach (var section in rpxDoc.Sections)
        {
            builder.AppendLine("    [TestMethod]");
            builder.AppendLine($"    public void Test_{section.Name}_HasControls()");
            builder.AppendLine("    {");
            builder.AppendLine($"        var section = _report.Sections[\"{section.Name}\"];");
            builder.AppendLine($"        Assert.IsNotNull(section);");
            builder.AppendLine($"        Assert.AreEqual({section.Controls.Count}, section.Controls.Count);");
            builder.AppendLine("    }");
            builder.AppendLine();
        }

        builder.AppendLine("}");

        return builder.ToString();
    }

    /// <summary>
    /// Example 6: Compare two RPX files
    /// </summary>
    public void Example6_CompareFiles(string rpxFile1, string rpxFile2)
    {
        Console.WriteLine("=== Example 6: Compare RPX Files ===\n");

        var doc1 = _parser.ParseFile(rpxFile1);
        var doc2 = _parser.ParseFile(rpxFile2);

        Console.WriteLine($"File 1: {doc1.DocumentName} (Sections: {doc1.Sections.Count})");
        Console.WriteLine($"File 2: {doc2.DocumentName} (Sections: {doc2.Sections.Count})");
        Console.WriteLine();

        // Find differences
        var sections1 = doc1.Sections.Select(s => s.Name).ToHashSet();
        var sections2 = doc2.Sections.Select(s => s.Name).ToHashSet();

        var sectionsOnlyIn1 = sections1.Except(sections2);
        var sectionsOnlyIn2 = sections2.Except(sections1);

        if (sectionsOnlyIn1.Any())
        {
            Console.WriteLine("Sections only in File 1:");
            foreach (var section in sectionsOnlyIn1)
            {
                Console.WriteLine($"  - {section}");
            }
        }

        if (sectionsOnlyIn2.Any())
        {
            Console.WriteLine("Sections only in File 2:");
            foreach (var section in sectionsOnlyIn2)
            {
                Console.WriteLine($"  - {section}");
            }
        }

        if (!sectionsOnlyIn1.Any() && !sectionsOnlyIn2.Any())
        {
            Console.WriteLine("✓ Both files have the same sections");
        }
    }

    /// <summary>
    /// Example 7: Export control information to CSV
    /// </summary>
    public void Example7_ExportToCsv(string rpxFilePath, string csvOutputPath)
    {
        Console.WriteLine("=== Example 7: Export to CSV ===\n");

        var rpxDoc = _parser.ParseFile(rpxFilePath);
        var lines = new List<string> { "Section,Control,Type,Name,DataField" };

        foreach (var section in rpxDoc.Sections)
        {
            foreach (var control in section.Controls)
            {
                var dataField = control.Properties.ContainsKey("DataField")
                    ? control.Properties["DataField"]
                    : "";

                var line = $"{section.Name},{control.Name},{control.Type},,{dataField}";
                lines.Add(line);
            }
        }

        File.WriteAllLines(csvOutputPath, lines);

        Console.WriteLine($"✓ Exported to: {csvOutputPath}");
        Console.WriteLine($"  Total rows: {lines.Count - 1}");
    }
}
