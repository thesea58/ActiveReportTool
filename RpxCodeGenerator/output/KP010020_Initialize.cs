// Auto-generated code from RPX file: KP010020
// Generated at: 03/24/2026 04:15:02

namespace YourNamespace.Reports;

/// <summary>
/// Auto-generated report initializer
/// </summary>
public partial class KP010020Initializer
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
            var text2 = section2.Controls["Text2"] as Label;
                if (text2 != null) text2.Left = 3345;
                if (text2 != null) text2.Top = 1395;
                if (text2 != null) text2.Width = 1200;
                if (text2 != null) text2.Height = 211;
            var text3 = section2.Controls["Text3"] as Label;
                if (text3 != null) text3.Left = 8655;
                if (text3 != null) text3.Top = 1395;
                if (text3 != null) text3.Width = 1425;
                if (text3 != null) text3.Height = 211;
            var text4 = section2.Controls["Text4"] as Label;
                if (text4 != null) text4.Visible = false;
                if (text4 != null) text4.Left = 9585;
                if (text4 != null) text4.Top = 1395;
                if (text4 != null) text4.Width = 1470;
                if (text4 != null) text4.Height = 211;
            var field11 = section2.Controls["Field11"] as TextField;
                if (field11 != null) field11.Left = 10230;
                if (field11 != null) field11.Top = 1170;
                if (field11 != null) field11.Width = 632;
                if (field11 != null) field11.Height = 182;
            var text15 = section2.Controls["Text15"] as Label;
                if (text15 != null) text15.Left = 10875;
                if (text15 != null) text15.Top = 1155;
                if (text15 != null) text15.Width = 240;
                if (text15 != null) text15.Height = 211;
            var field2 = section2.Controls["Field2"] as TextField;
                if (field2 != null) field2.Left = 345;
                if (field2 != null) field2.Top = 945;
                if (field2 != null) field2.Width = "2579.787";
                if (field2 != null) field2.Height = 211;
            var field4 = section2.Controls["Field4"] as TextField;
                if (field4 != null) field4.Left = 9765;
                if (field4 != null) field4.Top = 345;
                if (field4 != null) field4.Width = 1335;
                if (field4 != null) field4.Height = 211;
            var field5 = section2.Controls["Field5"] as TextField;
                if (field5 != null) field5.Left = 8955;
                if (field5 != null) field5.Top = 900;
                if (field5 != null) field5.Width = 2145;
                if (field5 != null) field5.Height = 211;
            var text5 = section2.Controls["Text5"] as Label;
                if (text5 != null) text5.Left = 1005;
                if (text5 != null) text5.Top = 1395;
                if (text5 != null) text5.Width = 1920;
                if (text5 != null) text5.Height = 211;
            var 和暦11 = section2.Controls["和暦11"] as TextField;
                if (和暦11 != null) 和暦11.Left = 1755;
                if (和暦11 != null) 和暦11.Top = 180;
                if (和暦11 != null) 和暦11.Width = 2280;
                if (和暦11 != null) 和暦11.Height = 345;
            var field3 = section2.Controls["Field3"] as TextField;
                if (field3 != null) field3.Left = 60;
                if (field3 != null) field3.Top = 210;
                if (field3 != null) field3.Width = 11235;
                if (field3 != null) field3.Height = 450;
            var 備考21 = section2.Controls["備考21"] as TextField;
                if (備考21 != null) 備考21.Left = 2745;
                if (備考21 != null) 備考21.Top = 945;
                if (備考21 != null) 備考21.Width = 3285;
                if (備考21 != null) 備考21.Height = 211;
            var 所属表示1 = section2.Controls["所属表示1"] as TextField;
                if (所属表示1 != null) 所属表示1.Visible = false;
                if (所属表示1 != null) 所属表示1.Left = 2460;
                if (所属表示1 != null) 所属表示1.Top = 1395;
                if (所属表示1 != null) 所属表示1.Width = 1230;
                if (所属表示1 != null) 所属表示1.Height = 211;
            var 帳票1 = section2.Controls["帳票1"] as TextField;
                if (帳票1 != null) 帳票1.Left = 8670;
                if (帳票1 != null) 帳票1.Top = 600;
                if (帳票1 != null) 帳票1.Width = 2430;
                if (帳票1 != null) 帳票1.Height = 211;
            var line2 = section2.Controls["Line2"] as Line;
        
        // Detail: Section3
        var section3 = _report.Sections["Section3"];
            var field10 = section3.Controls["Field10"] as TextField;
                if (field10 != null) field10.Left = 1005;
                if (field10 != null) field10.Top = 135;
                if (field10 != null) field10.Width = 1935;
                if (field10 != null) field10.Height = 211;
            var 所属表示2 = section3.Controls["所属表示2"] as TextField;
                if (所属表示2 != null) 所属表示2.Visible = false;
                if (所属表示2 != null) 所属表示2.Left = 2445;
                if (所属表示2 != null) 所属表示2.Top = 135;
                if (所属表示2 != null) 所属表示2.Width = 1230;
                if (所属表示2 != null) 所属表示2.Height = 211;
            var 当初予算額1 = section3.Controls["当初予算額1"] as TextField;
                if (当初予算額1 != null) 当初予算額1.Left = 8655;
                if (当初予算額1 != null) 当初予算額1.Top = 135;
                if (当初予算額1 != null) 当初予算額1.Width = 1507;
                if (当初予算額1 != null) 当初予算額1.Height = 221;
            var 補正予算額1 = section3.Controls["補正予算額1"] as TextField;
                if (補正予算額1 != null) 補正予算額1.Visible = false;
                if (補正予算額1 != null) 補正予算額1.Left = 9585;
                if (補正予算額1 != null) 補正予算額1.Top = 135;
                if (補正予算額1 != null) 補正予算額1.Width = 1470;
                if (補正予算額1 != null) 補正予算額1.Height = 211;
            var 正式科目名称1 = section3.Controls["正式科目名称1"] as TextField;
                if (正式科目名称1 != null) 正式科目名称1.Left = 3345;
                if (正式科目名称1 != null) 正式科目名称1.Top = 135;
                if (正式科目名称1 != null) 正式科目名称1.Width = 5190;
                if (正式科目名称1 != null) 正式科目名称1.Height = 211;
            var line1 = section3.Controls["Line1"] as Line;
        
        // PageFooter: Section5
        var section5 = _report.Sections["Section5"];
        
        // ReportFooter: Section4
        var section4 = _report.Sections["Section4"];
        
    }
}
