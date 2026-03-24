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

Console.WriteLine("╔════════════════════════════════════════════╗");
Console.WriteLine("║   RPX to C# Code Generator Tool           ║");
Console.WriteLine("║   Convert RPX files to initialization code║");
Console.WriteLine("╚════════════════════════════════════════════╝");
Console.WriteLine();

try
{
    var parser = new RpxParser();
    var codeGenerator = new CodeGenerator();

    // Get all RPX files in the directory
    if (!Directory.Exists(rpxDirectory))
    {
        Console.WriteLine($"❌ Error: Directory not found: {rpxDirectory}");
        Console.WriteLine($"   Current directory: {Directory.GetCurrentDirectory()}");
        return;
    }

    var rpxFiles = Directory.GetFiles(rpxDirectory, "*.rpx")
        .OrderBy(f => Path.GetFileName(f))
        .ToList();

    if (rpxFiles.Count == 0)
    {
        Console.WriteLine("❌ No RPX files found in: " + rpxDirectory);
        return;
    }

    Console.WriteLine($"📁 Found {rpxFiles.Count} RPX files");
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
            Console.WriteLine($"❌ RPX file not found: {inputArg}");
            Console.WriteLine($"   Checked path: {candidatePath}");
            return;
        }

        filesToProcess = [candidatePath];
        Console.WriteLine($"🎯 Single-file mode: {Path.GetFileName(candidatePath)}");
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
        Console.WriteLine($"📄 Processing: {fileName}");

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

            Console.WriteLine($"  ✓ Sections: {rpxDoc.Sections.Count}");
            Console.WriteLine($"  ✓ Total Controls: {rpxDoc.Sections.Sum(s => s.Controls.Count)}");
            Console.WriteLine($"  ✓ Generated: {baseFileName}_Initialize.cs");
            Console.WriteLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  ✗ Error: {ex.Message}");
            Console.WriteLine();
        }
    }

    // Summary
    Console.WriteLine("═" + new string('═', 42) + "═");
    Console.WriteLine("📊 Processing Summary:");
    Console.WriteLine($"   Files processed: {filesToProcess.Count}");
    Console.WriteLine($"   Total sections: {totalSections}");
    Console.WriteLine($"   Total controls: {totalControls}");
    Console.WriteLine($"   Output location: {Path.GetFullPath(outputDirectory)}");
    Console.WriteLine();
    Console.WriteLine("✅ Code generation completed successfully!");
    Console.WriteLine();

    // Display sample code from first file
    if (filesToProcess.Count > 0)
    {
        var firstFile = filesToProcess[0];
        var rpxDoc = parser.ParseFile(firstFile);
        var sampleCode = codeGenerator.Generate(rpxDoc);

        Console.WriteLine("📝 Sample generated code (first 30 lines):");
        Console.WriteLine("─" + new string('─', 42) + "─");
        var lines = sampleCode.Split('\n').Take(30);
        foreach (var line in lines)
        {
            Console.WriteLine(line);
        }
        Console.WriteLine("─" + new string('─', 42) + "─");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Fatal error: {ex.Message}");
    Console.WriteLine(ex.StackTrace);
}
