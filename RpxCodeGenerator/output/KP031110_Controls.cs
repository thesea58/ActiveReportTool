// Auto-generated type-safe control extraction
// Generated at: 03/24/2026 04:18:50

namespace YourNamespace.Reports;

/// <summary>
/// Auto-generated control accessor
/// </summary>
public partial class KP031110Controls
{
    /// <summary>Extract controls from Section2</summary>
    public void ExtractSection2Controls()
    {
        var section2 = _report.Sections["Section2"];
        
    }
    
    /// <summary>Extract controls from Section6</summary>
    public void ExtractSection6Controls()
    {
        var section6 = _report.Sections["Section6"];
        
    }
    
    /// <summary>Extract controls from Section3</summary>
    public void ExtractSection3Controls()
    {
        var section3 = _report.Sections["Section3"];
        
        var 部門コード1 = section3.Controls["部門コード1"] as TextBox;
        var 部門名1 = section3.Controls["部門名1"] as TextBox;
        var 予算合計1 = section3.Controls["予算合計1"] as TextBox;
        var 実績合計1 = section3.Controls["実績合計1"] as TextBox;
        var 達成率1 = section3.Controls["達成率1"] as TextBox;
    }
    
}
