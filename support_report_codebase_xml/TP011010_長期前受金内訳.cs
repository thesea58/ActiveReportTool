using KKReport;
using System.Data;
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
	public class TP011010_長期前受金内訳 : ReportClass
	{
		#region ARコントロールを初期化する
		private Section reportHeaderSection1;


		private Section detailSection1;

		private TextBox 内訳名称表示1;
		private TextBox 長期前受金;
		private TextBox 金額1;
		private TextBox 期首収益化累計額1;
		private TextBox 期首長期前受金1;
		private TextBox 当期収益化額1;
		private TextBox 期末収益化累計額1;
		private TextBox 期末長期前受金1;

		private Section reportFooter1;

		private TextBox 金額合計1;
		private TextBox 当初長期前受金合計1;
		private TextBox 期首収益化累計額合計1;
		private TextBox 期首長期前受金合計1;
		private TextBox 当期収益化額合計1;
		private TextBox 期末収益化累計額合計1;
		private TextBox 期末長期前受金合計1;

		#endregion

		public TP011010_長期前受金内訳()
		{
			// クラス名をレポート名として取得する
			string className = this.GetType().Name;
			this.LoadLayout(Path.Combine(KT.KTcls.RPTパス, $"{className}.rpx"));

			// イベントを紐付け (self = this)
			this.AttachEvents();
		}


		/// <summary>
		/// IReportScript実装: イベントを設定する
		/// </summary>
		public void AttachEvents()
		{
			// report参照を保存


			#region 各セクションおよびコントロール参照を取得
			reportHeaderSection1 = this.Sections["ReportHeaderSection1"];
			detailSection1 = this.Sections["DetailSection1"];

			内訳名称表示1 = detailSection1.Controls["内訳名称表示1"] as TextBox;
			長期前受金 = detailSection1.Controls["長期前受金"] as TextBox;
			金額1 = detailSection1.Controls["金額1"] as TextBox;
			期首収益化累計額1 = detailSection1.Controls["期首収益化累計額1"] as TextBox;
			期首長期前受金1 = detailSection1.Controls["期首長期前受金1"] as TextBox;
			当期収益化額1 = detailSection1.Controls["当期収益化額1"] as TextBox;
			期末収益化累計額1 = detailSection1.Controls["期末収益化累計額1"] as TextBox;
			期末長期前受金1 = detailSection1.Controls["期末長期前受金1"] as TextBox;

			reportFooter1 = this.Sections["ReportFooter1"];

			金額合計1 = reportFooter1.Controls["金額合計1"] as TextBox;
			当初長期前受金合計1 = reportFooter1.Controls["当初長期前受金合計1"] as TextBox;
			期首収益化累計額合計1 = reportFooter1.Controls["期首収益化累計額合計1"] as TextBox;
			期首長期前受金合計1 = reportFooter1.Controls["期首長期前受金合計1"] as TextBox;
			当期収益化額合計1 = reportFooter1.Controls["当期収益化額合計1"] as TextBox;
			期末収益化累計額合計1 = reportFooter1.Controls["期末収益化累計額合計1"] as TextBox;
			期末長期前受金合計1 = reportFooter1.Controls["期末長期前受金合計1"] as TextBox;
			#endregion


			// レポートイベントを登録（初期化・データ処理・書式設定）
			this.ReportStart += ActiveReport_ReportStart;
			//this.FetchData += ActiveReport_FetchData;
			//section3_DetailSection2.Format += Section3_DetailSection2_Format;
			this.NoData += ActiveReport_NoData;
		}



		#region RpxのScript | ActiveReportイベント定義
		private void ActiveReport_ReportStart(object? sender, EventArgs e)
		{
			// テキスト系コントロール背景を透過に揃えます。
			this.SetTransparentBackgrounds();

			if (this.DataSource == null) return;

			// ① 親レポートから DataView（フィルタ済み）で渡された場合 → DataTable に変換してそのまま使用
			if (this.DataSource is DataView dvSource)
			{
				this.DataSource = dvSource.ToTable();
				this.DataMember = null;
				return;
			}

			// ② DataTable で渡された場合 → そのまま使用
			if (this.DataSource is DataTable)
			{
				return;
			}

			// ③ DataSet で渡された場合 → 長期前受金内訳テーブルを取得してフィルタする
			DataSet dsDataSource = this.DataSource as DataSet;
			this.DataSource = dsDataSource != null && dsDataSource.Tables.Contains("長期前受金内訳")
				? dsDataSource.Tables["長期前受金内訳"]
				: null;

			DataTable dt = this.DataSource as DataTable;
			if (dt == null || !dt.Columns.Contains("台帳番号")) return;

			// パラメータ "groupValue" を取得してフィルタ
			string filterValue = Convert.ToString(this.Parameters["groupValue"].Value);
			DataView dv2 = new DataView(dt);
			dv2.RowFilter = string.Format("台帳番号 = '{0}'", filterValue);
			// フィルタ後の結果を新しい DataTable として反映させる
			this.DataSource = dv2.ToTable();
			this.DataMember = null;
		}

		private void ActiveReport_NoData(object? sender, EventArgs e)
		{
			detailSection1.Visible = false;
		}


		#endregion
	}

}
