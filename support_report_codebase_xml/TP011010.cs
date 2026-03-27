using GrapeCity.ActiveReports.SectionReportModel;
using KKReport;
using System.Data;
using System.Linq;
using Label = GrapeCity.ActiveReports.SectionReportModel.Label;
using Section = GrapeCity.ActiveReports.SectionReportModel.Section;
using TextAlignment = GrapeCity.ActiveReports.Document.Section.TextAlignment;
using TextBox = GrapeCity.ActiveReports.SectionReportModel.TextBox;


namespace KT011010
{

	/// <summary>
	/// KP031110帳票クラス
	/// レイアウトの読み込みおよびスクリプトのイベント紐付けを行う
	/// </summary>
	public class TP011010 : ReportClass
	{
		#region ARコントロールを初期化する
		private Section section2;

		private TextBox field1;
		private TextBox field2;
		private TextBox field3;
		private TextBox field4;
		private TextBox field5;

		private Section section3_DetailSection2;

		private TextBox field14;
		private TextBox field17;
		private TextBox field18;
		private TextBox field19;
		private TextBox field20;
		private TextBox field21;
		private TextBox field22;
		private TextBox field27;
		private TextBox field28;
		private TextBox field7;
		private TextBox field8;
		private TextBox field9;
		private TextBox field10;
		private TextBox field11;
		private TextBox field12;
		private TextBox field13;
		private TextBox field16;
		private TextBox field23;
		private TextBox field24;
		private TextBox field25;
		private TextBox field26;
		private TextBox 処理区分名称1;
		private SubReport Subreport2;
		private SubReport Subreport1;


		private Section section5;

		private TextBox field6;

		/// <summary>元の DataSet への参照（ReportStart でソート後も DataSet を保持するため）</summary>
		private DataSet _ds;
		#endregion

		public TP011010()
		{
			// クラス名をレポート名として取得する
			string className = this.GetType().Name;
			this.LoadLayout(Path.Combine(KT.KTcls.RPTパス, $"{className}.rpx"));

			//イベントを紐付け(self = this)
			this.AttachEvents();
		}


		/// <summary>
		/// IReportScript実装: イベントを設定する
		/// </summary>
		public void AttachEvents()
		{
			// report参照を保存

			#region 各セクションおよびコントロール参照を取得
			section2 = this.Sections["Section2"];

			field1 = section2.Controls["Field1"] as TextBox;
			field2 = section2.Controls["Field2"] as TextBox;
			field3 = section2.Controls["Field3"] as TextBox;
			field4 = section2.Controls["Field4"] as TextBox;
			field5 = section2.Controls["Field5"] as TextBox;

			section3_DetailSection2 = this.Sections["Section3_DetailSection2"];

			field14 = section3_DetailSection2.Controls["Field14"] as TextBox;
			field17 = section3_DetailSection2.Controls["Field17"] as TextBox;
			field18 = section3_DetailSection2.Controls["Field18"] as TextBox;
			field19 = section3_DetailSection2.Controls["Field19"] as TextBox;
			field20 = section3_DetailSection2.Controls["Field20"] as TextBox;
			field21 = section3_DetailSection2.Controls["Field21"] as TextBox;
			field22 = section3_DetailSection2.Controls["Field22"] as TextBox;
			field27 = section3_DetailSection2.Controls["Field27"] as TextBox;
			field28 = section3_DetailSection2.Controls["Field28"] as TextBox;
			field7 = section3_DetailSection2.Controls["Field7"] as TextBox;
			field8 = section3_DetailSection2.Controls["Field8"] as TextBox;
			field9 = section3_DetailSection2.Controls["Field9"] as TextBox;
			field10 = section3_DetailSection2.Controls["Field10"] as TextBox;
			field11 = section3_DetailSection2.Controls["Field11"] as TextBox;
			field12 = section3_DetailSection2.Controls["Field12"] as TextBox;
			field13 = section3_DetailSection2.Controls["Field13"] as TextBox;
			field16 = section3_DetailSection2.Controls["Field16"] as TextBox;
			field23 = section3_DetailSection2.Controls["Field23"] as TextBox;
			field24 = section3_DetailSection2.Controls["Field24"] as TextBox;
			field25 = section3_DetailSection2.Controls["Field25"] as TextBox;
			field26 = section3_DetailSection2.Controls["Field26"] as TextBox;
			処理区分名称1 = section3_DetailSection2.Controls["処理区分名称1"] as TextBox;
			Subreport1 = section3_DetailSection2.Controls["Subreport1"] as SubReport;
			Subreport2 = section3_DetailSection2.Controls["Subreport2"] as SubReport;

			section5 = this.Sections["Section5"];

			field6 = section5.Controls["Field6"] as TextBox;

			#endregion


			// レポートイベントを登録（初期化・データ処理・書式設定）
			this.ReportStart += ActiveReport_ReportStart;
			this.FetchData += ActiveReport_FetchData;
			section3_DetailSection2.Format += Section3_DetailSection2_Format;
		}

		#region RpxのScript | ActiveReportイベント定義

		/// <summary>
		/// ActiveReport_ReportStart　イベント
		/// rpt 内のすべてのセクションに対してテキスト系コントロールの背景色を透過に設定する
		/// SetDataSource でマージ済みの MAIN テーブルに 区分項目 計算列（事業所+部門区分+項+目+節）を追加し
		/// 区分項目 ASC, 台帳番号 ASC の順でソートする
		/// </summary>
		public void ActiveReport_ReportStart(object? sender, EventArgs e)
		{
			// rpt 内のすべてのセクションに対してテキスト系コントロールの背景色を透過に設定する
			this.SetTransparentBackgrounds();

			// SetDataSource により DataSource=DataSet, DataMember="MAIN" が設定済みの前提
			DataSet ds = this.DataSource as DataSet;
			if (ds == null) return;

			// DataMember で指定されたテーブル（MAIN）を対象とする
			if (string.IsNullOrEmpty(this.DataMember) || !ds.Tables.Contains(this.DataMember)) return;

			DataTable mainTable = ds.Tables[this.DataMember];

			// 区分項目 計算列を追加する（事業所+部門区分+項+目+節）
			// 元となる列がすべて存在する場合のみ追加する
			string[] baseColumns = { "事業所", "部門区分", "項", "目", "節" };
			if (!mainTable.Columns.Contains("区分項目")
				&& baseColumns.All(col => mainTable.Columns.Contains(col)))
			{
				DataColumn 区分項目Col = new DataColumn("区分項目", typeof(string));
				区分項目Col.Expression = "[事業所] + [部門区分] + [項] + [目] + [節]";
				mainTable.Columns.Add(区分項目Col);
			}

			// ソート式の決定: 区分項目 ASC, 台帳番号 ASC
			bool has区分項目 = mainTable.Columns.Contains("区分項目");
			bool has台帳番号 = mainTable.Columns.Contains("台帳番号");

			if (!has区分項目 && !has台帳番号) return;

			string sortExpr = has区分項目 && has台帳番号 ? "区分項目 ASC, 台帳番号 ASC"
							: has区分項目 ? "区分項目 ASC"
							: "台帳番号 ASC";

			// 元の DataSet を保存する（DataSource を DataView に切り替えた後も参照できるようにする）
			_ds = ds;

			// DataView でソートを適用し DataSource を置き換える
			mainTable.DefaultView.Sort = sortExpr;
			this.DataSource = mainTable.DefaultView;
			this.DataMember = null;
		}

		/// <summary>
		/// ActiveReport_FetchData　イベント
		/// フィールドにデータをセットするイベント
		/// </summary>
		/// <param name="eof">eof</param>
		public void ActiveReport_FetchData(object? sender, FetchEventArgs eArgs)
		{
			try
			{
				// データ終了時は処理しない
				if (eArgs.EOF) return;

				#region グループ
				//{事業所} + ToText({台帳番号})
				//this.CalculatedFields["グループ"].Value = this.Fields["事業所"].Value.ToString() + FormatAmount(this.Fields["台帳番号"].Value);
				#endregion

				#region タイムスタンプ
				// CR Formula: タイムスタンプ = CurrentDateTime
				#region 非表示
				//if InStr({ヘッダデータ.見出区分}, "TIMESTAMPON") = 0 then
				//true
				//else
				//false;
				this.field6.Visible = this.Fields["見出区分"].Value.ToString().Contains("TIMESTAMPON");
				#endregion

				this.CalculatedFields["タイムスタンプ"].Value = getCurrentDayTime();
				#endregion

				eArgs.EOF = false;
			}
			catch
			{
				// エラー時はデータ終了とする
				eArgs.EOF = true;
			}
		}
		/// <summary>
		/// 現在の日付を取得する。
		/// </summary>
		private string getCurrentDayTime()
		{
			// カルチャの「言語-国/地域」を「日本語-日本」に設定します。
			System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ja-JP");
			// 和暦を表すクラスです。
			System.Globalization.JapaneseCalendar jp = new System.Globalization.JapaneseCalendar();

			// 現在のカルチャで使用する暦を、和暦に設定します。
			ci.DateTimeFormat.Calendar = jp;
			// 「書式」「カルチャの書式情報」を使用し、文字列に変換します。
			return DateTime.Now.ToString("gg yy年MM月dd日 HH:mm:ss", ci);
		}

		public void Section3_DetailSection2_Format(object? sender, EventArgs e)
		{
			if (this.Fields["台帳番号"].Value == null)
			{
				return;
			}

			long 台帳番号 = Convert.ToInt64(this.Fields["台帳番号"].Value);

			// _ds は ReportStart で保存した元の DataSet（資産明細・長期前受金内訳を含む）
			DataSet? ds = _ds ?? this.DataSource as DataSet;
			if (ds == null)
				return;

			// Subreport1
			this.Subreport1 = (this.Sections["Section3_DetailSection2"].Controls["Subreport1"] as GrapeCity.ActiveReports.SectionReportModel.SubReport);
			//this.Subreport1.Report = new TP011010_長期前受金内訳();
			//string hyperlinkValue1 = "TP011010_長期前受金内訳:";
			//foreach (Section section in Subreport2.Report.Sections)
			//{
			//	foreach (ARControl control in section.Controls)
			//	{
			//		if (control is GrapeCity.ActiveReports.SectionReportModel.TextBox textBox)
			//		{
			//			textBox.HyperLink = hyperlinkValue1;
			//		}
			//		else if (control is GrapeCity.ActiveReports.SectionReportModel.Label label)
			//		{
			//			label.HyperLink = hyperlinkValue1;
			//		}
			//	}
			//}
			if (Subreport1 != null && Subreport1.Report != null && ds.Tables.Contains("資産明細"))
			{
				DataView dv1 = new DataView(ds.Tables["資産明細"]);
				dv1.RowFilter = "台帳番号 = " + 台帳番号;

				if (dv1.Count > 0)
				{
					Subreport1.Report.DataSource = dv1.ToTable();
					Subreport1.Report.DataMember = null;
				}
			}

			// Subreport2
			//this.Subreport2.Report = new TP011010_長期前受金内訳();
			
			if (Subreport2 != null && Subreport2.Report != null && ds.Tables.Contains("長期前受金内訳"))
			{
				DataView dv2 = new DataView(ds.Tables["長期前受金内訳"]);
				dv2.RowFilter = "台帳番号 = " + 台帳番号;
				Subreport2.Report.DataSource = dv2.ToTable();

				string hyperlinkValue2 = "TP011010_長期前受金内訳:" + 台帳番号.ToString();
				foreach (Section section in Subreport2.Report.Sections)
				{
					foreach (ARControl control in section.Controls)
					{
						if (control is GrapeCity.ActiveReports.SectionReportModel.TextBox textBox)
						{
							textBox.HyperLink = hyperlinkValue2;
						}
						else if (control is GrapeCity.ActiveReports.SectionReportModel.Label label)
						{
							label.HyperLink = hyperlinkValue2;
						}
					}
				}
			}
		}



		#endregion
	}

}
