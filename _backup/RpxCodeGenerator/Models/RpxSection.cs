namespace RpxCodeGenerator.Models;

/// <summary>
/// Đại diện cho một Section trong file RPX
/// </summary>
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

/// <summary>
/// Đại diện cho một Control trong Section
/// </summary>
public class RpxControl
{
    public string Type { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public Dictionary<string, string> Properties { get; set; } = [];

    public override string ToString()
    {
        return $"Control: {Name} ({Type})";
    }

    /// <summary>
    /// Trích xuất loại control từ Type (e.g., "AR.Label" -> "Label", "AR.Field" -> "Field")
    /// </summary>
    public string GetControlClassName()
    {
        return Type.Split('.').LastOrDefault()?.Replace("AR", "TextBox") ?? "TextBox";
    }
}

/// <summary>
/// Đại diện cho toàn bộ cấu trúc RPX
/// </summary>
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
