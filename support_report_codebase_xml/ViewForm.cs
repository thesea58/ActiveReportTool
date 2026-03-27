using ActiveReports.Viewer.Helper;
using GrapeCity.ActiveReports.Document;
using KKReport.Factories;

namespace KKReport
{
	/// <summary>
	/// レポート表示機能
	/// </summary>
	/// <newpara>担当者： RVSC Nguyen Thanh Ha</newpara>
	/// <newpara>日付： 2025.09.04</newpara>
	public partial class ViewerForm : Form
	{

		private ReportClass rptMain;
		
		/// <summary>
		/// ViewerForm クラスの新しいインスタンスを初期化します。
		/// </summary>
		/// <param name="reportName">レポート名</param>
		public ViewerForm(string reportName)
		{
			InitializeComponent();
			SetReportName(reportName);
		}

		/// <summary>
		/// ドキュメントをファイルから読み込み、ビューアに表示します。
		/// </summary>
		/// <param name="file">読み込むファイル</param>
		public void LoadDocument(FileInfo file)
		{
			try
			{
				bool isRdf = ViewerHelper.IsRdf((file));
				var reportServerUri = !isRdf ? ViewerHelper.GetReportServerUri(file) : string.Empty;

				if (!string.IsNullOrEmpty(reportServerUri))
				{
					throw new NotSupportedException("Server report is not supported");
				}
				else
					viewer.LoadDocument(file.FullName);

				SetReportName(file.Name);
			}
			catch (Exception ex)
			{
				viewer.HandleError(ex);
			}
		}

		/// <summary>
		/// ReportClass オブジェクトからドキュメントを読み込み、ビューアに表示します。
		/// </summary>
		/// <param name="report">ReportClass オブジェクト</param>
		public void LoadDocument(ReportClass report)
		{
			try
			{
				report.Run();
				rptMain = report;
				viewer.LoadDocument(report.Document);
			}
			catch (Exception ex)
			{
				viewer.HandleError(ex);
			}
		}

		/// <summary>
		/// PageReportClass オブジェクトからドキュメントを読み込み、ビューアに表示します。
		/// </summary>
		/// <param name="pageReport">PageReportClass オブジェクト</param>
		public void LoadDocument(PageReportClass pageReport)
		{
			try
			{
				PageDocument pageDocument = pageReport.CreateDocument();
				viewer.LoadDocument(pageDocument);
			}
			catch (Exception ex)
			{
				viewer.HandleError(ex);
			}
		}

		/// <summary>
		/// PageDocument オブジェクトからドキュメントを読み込み、ビューアに表示します。
		/// </summary>
		/// <param name="pageDocument">PageDocument オブジェクト</param>
		public void LoadDocument(PageDocument pageDocument)
		{
			try
			{
				viewer.LoadDocument(pageDocument);
			}
			catch (Exception ex)
			{
				viewer.HandleError(ex);
			}
		}

		/// <summary>
		/// レポート名を設定
		/// </summary>
		/// <param name="reportName">レポート名</param>
		private void SetReportName(string reportName)
		{
			Text = reportName;
		}

		/// <summary>
		/// ハイパーリンククリック時の処理
		/// リンク内容を解析し、対象レポートを読み込みパラメータを渡して別Viewerで表示する
		/// </summary>
		private void Viewer_HyperLink(object sender, GrapeCity.ActiveReports.Viewer.Win.HyperLinkEventArgs e)
		{
			// イベントを処理済みに設定（既定動作を抑止）
			e.Handled = true;

			// ハイパーリンクが無い場合は終了
			if (e.HyperLink == null) return;

			// ハイパーリンク文字列を取得（空/空白なら終了）
			var link = e.HyperLink.ToString();
			if (string.IsNullOrWhiteSpace(link)) return;

			// ハイパーリンク文字列を解析（形式: reportName:parameter）
			string[] parts = link.Split(':');
			if (parts.Length < 2) return;

			// レポート名とパラメータ値を取得
			string reportName = parts[0];
			string valueFilte = parts[1];

			// ReportFactory を使用して report を作成
			// Constructor 内で自動的に LoadLayout + AttachEvents が実行される
			ReportClass subreport = ReportFactory.CreateReport(reportName);
			if (subreport == null)
			{
				MessageBox.Show($"レポートが見つかりません：{reportName}\n\n利用可能なレポート：\n" +
					string.Join("\n", ReportFactory.GetAllReportNames()),
					"ファイル未検出",
					MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
				// メインレポートのデータソースを引き継ぐ
			// ※ ソート処理後は rptMain.DataSource が DataView になっている場合があるため
			//   DataView → DataTable → DataSet の順で元の DataSet を解決する
			object resolvedDataSource = rptMain.DataSource;
			if (resolvedDataSource is System.Data.DataView dvMain)
				resolvedDataSource = dvMain.Table?.DataSet ?? (object)dvMain.Table;

			subreport.DataSource = resolvedDataSource;
			subreport.DataMember = null;

			// パラメータを作成し値を設定
			const string key = "groupValue";
			var existing = subreport.Parameters[key];

			// 既存パラメータがあれば削除して再設定
			if (existing != null)
			{
				subreport.Parameters.Remove(existing);
			}

			GrapeCity.ActiveReports.SectionReportModel.Parameter groupValue = new GrapeCity.ActiveReports.SectionReportModel.Parameter
			{
				Key = key,
				Value = valueFilte,
				DefaultValue = valueFilte,
				PromptUser = false
			};
			subreport.Parameters.Add(groupValue);

			// 新しいViewerでレポートを表示
			using (ViewerForm popupViewer = new ViewerForm($"{groupValue.Value}"))
			{
				popupViewer.LoadDocument(subreport);
				popupViewer.ShowDialog();
			}
		}
	}
}