// Auto-generated code from RPX file: KP031110
// Generated at: 03/24/2026 04:18:50

namespace YourNamespace.Reports;

/// <summary>
/// Auto-generated report initializer
/// </summary>
public partial class KP031110Initializer
{
    /// <summary>
    /// Initialize sections and controls from report
    /// </summary>
    public void InitializeReportSections()
    {
        // ReportHeader: Section1
        var section1 = _report.Sections["Section1"];
        
        // PageHeader: Section2
        var section2 = _report.Sections["Section2"];
            var line17 = section2.Controls["Line17"] as Line;
        
        // GroupHeader: Section6
        var section6 = _report.Sections["Section6"];
            var crossSectionBox1 = section6.Controls["CrossSectionBox1"] as ARControl;
                if (crossSectionBox1 != null) crossSectionBox1.Left = "314.6457";
                if (crossSectionBox1 != null) crossSectionBox1.Top = 1800;
            var 部門コード見出し1 = section6.Controls["部門コード見出し1"] as Label;
                if (部門コード見出し1 != null) 部門コード見出し1.Left = "3792.315";
                if (部門コード見出し1 != null) 部門コード見出し1.Top = "2090.268";
                if (部門コード見出し1 != null) 部門コード見出し1.Width = 900;
                if (部門コード見出し1 != null) 部門コード見出し1.Height = "286.5827";
            var 部門名見出し1 = section6.Controls["部門名見出し1"] as Label;
                if (部門名見出し1 != null) 部門名見出し1.Left = "4712.315";
                if (部門名見出し1 != null) 部門名見出し1.Top = "2090.268";
                if (部門名見出し1 != null) 部門名見出し1.Width = 2000;
                if (部門名見出し1 != null) 部門名見出し1.Height = "286.5827";
            var 予算見出し1 = section6.Controls["予算見出し1"] as Label;
                if (予算見出し1 != null) 予算見出し1.Left = "6762.315";
                if (予算見出し1 != null) 予算見出し1.Top = "2090.268";
                if (予算見出し1 != null) 予算見出し1.Width = 1200;
                if (予算見出し1 != null) 予算見出し1.Height = "286.5827";
            var 実績見出し1 = section6.Controls["実績見出し1"] as Label;
                if (実績見出し1 != null) 実績見出し1.Left = "7992.315";
                if (実績見出し1 != null) 実績見出し1.Top = "2090.268";
                if (実績見出し1 != null) 実績見出し1.Width = 1200;
                if (実績見出し1 != null) 実績見出し1.Height = "286.5827";
            var 達成率見出し1 = section6.Controls["達成率見出し1"] as Label;
                if (達成率見出し1 != null) 達成率見出し1.Left = "9222.315";
                if (達成率見出し1 != null) 達成率見出し1.Top = "2090.268";
                if (達成率見出し1 != null) 達成率見出し1.Width = 900;
                if (達成率見出し1 != null) 達成率見出し1.Height = "286.5827";
        
        // Detail: Section3
        var section3 = _report.Sections["Section3"];
            var 部門コード1 = section3.Controls["部門コード1"] as TextField;
                if (部門コード1 != null) 部門コード1.Left = "3791.055";
                if (部門コード1 != null) 部門コード1.Top = "106.0158";
                if (部門コード1 != null) 部門コード1.Width = 900;
                if (部門コード1 != null) 部門コード1.Height = 180;
            var 部門名1 = section3.Controls["部門名1"] as TextField;
                if (部門名1 != null) 部門名1.Left = "4711.055";
                if (部門名1 != null) 部門名1.Top = "106.0158";
                if (部門名1 != null) 部門名1.Width = 2000;
                if (部門名1 != null) 部門名1.Height = 180;
            var 予算合計1 = section3.Controls["予算合計1"] as TextField;
                if (予算合計1 != null) 予算合計1.Left = "6761.055";
                if (予算合計1 != null) 予算合計1.Top = "106.0158";
                if (予算合計1 != null) 予算合計1.Width = 1200;
                if (予算合計1 != null) 予算合計1.Height = 180;
            var 実績合計1 = section3.Controls["実績合計1"] as TextField;
                if (実績合計1 != null) 実績合計1.Left = "7991.056";
                if (実績合計1 != null) 実績合計1.Top = "106.0158";
                if (実績合計1 != null) 実績合計1.Width = 1200;
                if (実績合計1 != null) 実績合計1.Height = 180;
            var 達成率1 = section3.Controls["達成率1"] as TextField;
                if (達成率1 != null) 達成率1.Left = "9221.056";
                if (達成率1 != null) 達成率1.Top = "106.0158";
                if (達成率1 != null) 達成率1.Width = 900;
                if (達成率1 != null) 達成率1.Height = 180;
            var 明細罫線1 = section3.Controls["明細罫線1"] as Line;
        
        // GroupFooter: Section7
        var section7 = _report.Sections["Section7"];
        
        // PageFooter: Section5
        var section5 = _report.Sections["Section5"];
        
        // ReportFooter: Section4
        var section4 = _report.Sections["Section4"];
        
    }
}
