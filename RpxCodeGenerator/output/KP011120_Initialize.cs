// Auto-generated code from RPX file: KP011120
// Generated at: 03/24/2026 04:15:02

namespace YourNamespace.Reports;

/// <summary>
/// Auto-generated report initializer
/// </summary>
public partial class KP011120Initializer
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
            var field11 = section2.Controls["Field11"] as TextField;
                if (field11 != null) field11.Left = 14400;
                if (field11 != null) field11.Top = 1020;
                if (field11 != null) field11.Width = "705.165";
                if (field11 != null) field11.Height = 182;
            var text15 = section2.Controls["Text15"] as Label;
                if (text15 != null) text15.Left = 15180;
                if (text15 != null) text15.Top = 1005;
                if (text15 != null) text15.Width = "270.165";
                if (text15 != null) text15.Height = 211;
            var field2 = section2.Controls["Field2"] as TextField;
                if (field2 != null) field2.Left = 165;
                if (field2 != null) field2.Top = 675;
                if (field2 != null) field2.Width = "2519.976";
                if (field2 != null) field2.Height = 211;
            var field4 = section2.Controls["Field4"] as TextField;
                if (field4 != null) field4.Left = 14085;
                if (field4 != null) field4.Top = 195;
                if (field4 != null) field4.Width = "1365.165";
                if (field4 != null) field4.Height = 211;
            var field5 = section2.Controls["Field5"] as TextField;
                if (field5 != null) field5.Left = 13275;
                if (field5 != null) field5.Top = 750;
                if (field5 != null) field5.Width = "2175.165";
                if (field5 != null) field5.Height = 211;
            var text3 = section2.Controls["Text3"] as Label;
                if (text3 != null) text3.Left = 10140;
                if (text3 != null) text3.Top = 1050;
                if (text3 != null) text3.Width = "1605.165";
                if (text3 != null) text3.Height = 211;
            var text4 = section2.Controls["Text4"] as Label;
                if (text4 != null) text4.Left = 15000;
                if (text4 != null) text4.Top = 1275;
                if (text4 != null) text4.Width = "450.165";
                if (text4 != null) text4.Height = 211;
            var text5 = section2.Controls["Text5"] as Label;
                if (text5 != null) text5.Left = 165;
                if (text5 != null) text5.Top = 1050;
                if (text5 != null) text5.Width = "885.6375";
                if (text5 != null) text5.Height = 211;
            var text1 = section2.Controls["Text1"] as Label;
                if (text1 != null) text1.Left = 1125;
                if (text1 != null) text1.Top = 1050;
                if (text1 != null) text1.Width = "1665.165";
                if (text1 != null) text1.Height = 211;
            var text6 = section2.Controls["Text6"] as Label;
                if (text6 != null) text6.Left = 2685;
                if (text6 != null) text6.Top = 1050;
                if (text6 != null) text6.Width = "1305.165";
                if (text6 != null) text6.Height = 211;
            var text7 = section2.Controls["Text7"] as Label;
                if (text7 != null) text7.Left = 3930;
                if (text7 != null) text7.Top = 1050;
                if (text7 != null) text7.Width = "2475.165";
                if (text7 != null) text7.Height = 211;
            var text8 = section2.Controls["Text8"] as Label;
                if (text8 != null) text8.Left = 3930;
                if (text8 != null) text8.Top = 1305;
                if (text8 != null) text8.Width = "2475.165";
                if (text8 != null) text8.Height = 211;
            var text10 = section2.Controls["Text10"] as Label;
                if (text10 != null) text10.Left = 11835;
                if (text10 != null) text10.Top = 1050;
                if (text10 != null) text10.Width = "3075.165";
                if (text10 != null) text10.Height = 211;
            var 和暦11 = section2.Controls["和暦11"] as TextField;
                if (和暦11 != null) 和暦11.Left = 3300;
                if (和暦11 != null) 和暦11.Top = 150;
                if (和暦11 != null) 和暦11.Width = "2385.165";
                if (和暦11 != null) 和暦11.Height = 345;
            var field3 = section2.Controls["Field3"] as TextField;
                if (field3 != null) field3.Left = 30;
                if (field3 != null) field3.Top = 150;
                if (field3 != null) field3.Width = "16200.17";
                if (field3 != null) field3.Height = 450;
            var 帳票1 = section2.Controls["帳票1"] as TextField;
                if (帳票1 != null) 帳票1.Left = 12990;
                if (帳票1 != null) 帳票1.Top = 465;
                if (帳票1 != null) 帳票1.Width = "2460.165";
                if (帳票1 != null) 帳票1.Height = 211;
            var text12 = section2.Controls["Text12"] as Label;
                if (text12 != null) text12.Left = 7650;
                if (text12 != null) text12.Top = 1305;
                if (text12 != null) text12.Width = "2475.165";
                if (text12 != null) text12.Height = 211;
            var text13 = section2.Controls["Text13"] as Label;
                if (text13 != null) text13.Left = 7650;
                if (text13 != null) text13.Top = 1050;
                if (text13 != null) text13.Width = "2475.165";
                if (text13 != null) text13.Height = 211;
            var text14 = section2.Controls["Text14"] as Label;
                if (text14 != null) text14.Left = 6360;
                if (text14 != null) text14.Top = 1050;
                if (text14 != null) text14.Width = "1305.165";
                if (text14 != null) text14.Height = 211;
            var line2 = section2.Controls["Line2"] as Line;
            var line3 = section2.Controls["Line3"] as Line;
        
        // Detail: Section3
        var section3 = _report.Sections["Section3"];
            var field10 = section3.Controls["Field10"] as TextField;
                if (field10 != null) field10.Left = 3930;
                if (field10 != null) field10.Top = 60;
                if (field10 != null) field10.Width = "2474.951";
                if (field10 != null) field10.Height = 211;
            var field6 = section3.Controls["Field6"] as TextField;
                if (field6 != null) field6.Left = 10140;
                if (field6 != null) field6.Top = 105;
                if (field6 != null) field6.Width = "1574.951";
                if (field6 != null) field6.Height = 182;
            var field7 = section3.Controls["Field7"] as TextField;
                if (field7 != null) field7.Left = 11835;
                if (field7 != null) field7.Top = 105;
                if (field7 != null) field7.Width = "3074.951";
                if (field7 != null) field7.Height = 435;
            var field9 = section3.Controls["Field9"] as TextField;
                if (field9 != null) field9.Left = 1125;
                if (field9 != null) field9.Top = 105;
                if (field9 != null) field9.Width = "1499.952";
                if (field9 != null) field9.Height = 211;
            var field13 = section3.Controls["Field13"] as TextField;
                if (field13 != null) field13.Left = 165;
                if (field13 != null) field13.Top = 105;
                if (field13 != null) field13.Width = "779.9515";
                if (field13 != null) field13.Height = 182;
            var field14 = section3.Controls["Field14"] as TextField;
                if (field14 != null) field14.Left = 15000;
                if (field14 != null) field14.Top = 105;
                if (field14 != null) field14.Width = "449.9515";
                if (field14 != null) field14.Height = 211;
            var 予算増1 = section3.Controls["予算増1"] as TextField;
                if (予算増1 != null) 予算増1.Left = 7620;
                if (予算増1 != null) 予算増1.Top = 60;
                if (予算増1 != null) 予算増1.Width = "2474.951";
                if (予算増1 != null) 予算増1.Height = 211;
            var 予算細名称減1 = section3.Controls["予算細名称減1"] as TextField;
                if (予算細名称減1 != null) 予算細名称減1.Left = 3930;
                if (予算細名称減1 != null) 予算細名称減1.Top = 330;
                if (予算細名称減1 != null) 予算細名称減1.Width = "2474.951";
                if (予算細名称減1 != null) 予算細名称減1.Height = 211;
            var 予算細名称増1 = section3.Controls["予算細名称増1"] as TextField;
                if (予算細名称増1 != null) 予算細名称増1.Left = 7620;
                if (予算細名称増1 != null) 予算細名称増1.Top = 330;
                if (予算細名称増1 != null) 予算細名称増1.Width = "2474.951";
                if (予算細名称増1 != null) 予算細名称増1.Height = 211;
            var 所属名称減1 = section3.Controls["所属名称減1"] as TextField;
                if (所属名称減1 != null) 所属名称減1.Left = 2685;
                if (所属名称減1 != null) 所属名称減1.Top = 330;
                if (所属名称減1 != null) 所属名称減1.Width = "1124.952";
                if (所属名称減1 != null) 所属名称減1.Height = 211;
            var 所属減1 = section3.Controls["所属減1"] as TextField;
                if (所属減1 != null) 所属減1.Left = 2685;
                if (所属減1 != null) 所属減1.Top = 60;
                if (所属減1 != null) 所属減1.Width = "1124.952";
                if (所属減1 != null) 所属減1.Height = 211;
            var 所属名称増1 = section3.Controls["所属名称増1"] as TextField;
                if (所属名称増1 != null) 所属名称増1.Left = 6465;
                if (所属名称増1 != null) 所属名称増1.Top = 330;
                if (所属名称増1 != null) 所属名称増1.Width = "1124.952";
                if (所属名称増1 != null) 所属名称増1.Height = 211;
            var 所属増1 = section3.Controls["所属増1"] as TextField;
                if (所属増1 != null) 所属増1.Left = 6450;
                if (所属増1 != null) 所属増1.Top = 60;
                if (所属増1 != null) 所属増1.Width = "1124.952";
                if (所属増1 != null) 所属増1.Height = 211;
            var field1 = section3.Controls["Field1"] as TextField;
                if (field1 != null) field1.Left = 600;
                if (field1 != null) field1.Top = 330;
                if (field1 != null) field1.Width = "1994.952";
                if (field1 != null) field1.Height = 211;
            var line3Section3 = section3.Controls["Line3Section3"] as Line;
            var line1 = section3.Controls["Line1"] as Line;
        
        // PageFooter: Section5
        var section5 = _report.Sections["Section5"];
        
        // ReportFooter: Section4
        var section4 = _report.Sections["Section4"];
        
    }
}
