// Auto-generated type-safe control extraction
// Generated at: 03/24/2026 04:15:02

namespace YourNamespace.Reports;

/// <summary>
/// Auto-generated control accessor
/// </summary>
public partial class KP010020Controls
{
    /// <summary>Extract controls from Section2</summary>
    public void ExtractSection2Controls()
    {
        var section2 = _report.Sections["Section2"];
        
        var field11 = section2.Controls["Field11"] as TextBox;
        var field2 = section2.Controls["Field2"] as TextBox;
        var field4 = section2.Controls["Field4"] as TextBox;
        var field5 = section2.Controls["Field5"] as TextBox;
        var 和暦11 = section2.Controls["和暦11"] as TextBox;
        var field3 = section2.Controls["Field3"] as TextBox;
        var 備考21 = section2.Controls["備考21"] as TextBox;
        var 所属表示1 = section2.Controls["所属表示1"] as TextBox;
        var 帳票1 = section2.Controls["帳票1"] as TextBox;
    }
    
    /// <summary>Extract controls from Section3</summary>
    public void ExtractSection3Controls()
    {
        var section3 = _report.Sections["Section3"];
        
        var field10 = section3.Controls["Field10"] as TextBox;
        var 所属表示2 = section3.Controls["所属表示2"] as TextBox;
        var 当初予算額1 = section3.Controls["当初予算額1"] as TextBox;
        var 補正予算額1 = section3.Controls["補正予算額1"] as TextBox;
        var 正式科目名称1 = section3.Controls["正式科目名称1"] as TextBox;
    }
    
}
