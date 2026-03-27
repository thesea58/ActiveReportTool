using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.SectionReportModel;
using System.Data;
using System.Text;
using static KKReport.ReportConstants;

namespace KKReport
{
	/// <summary>
	/// レポート機能
	/// </summary>
	/// <newpara>担当者： RVSC Nguyen Thanh Ha</newpara>
	/// <newpara>日付： 2025.09.04</newpara>
	public class ReportClass : SectionReport
	{
		#region クラス定数
		/// <summary>
		/// レポートのデータ
		/// </summary>
		protected DataTable _dtData;

		/// <summary>
		/// 詳細テーブル名
		/// </summary>
		private string _DetailTableName = string.Empty;
		#endregion

		/// <summary>
		/// ｒｐｘのファイルパス
		/// </summary>
		public string _FilePath = string.Empty;

		/// <summary>
		/// DataSet をデータソースとして設定します。  
		/// テーブルが1つの場合は直接設定し、2つ以上の場合は「ヘッダデータ」を基に結合します。
		/// </summary>
		/// <param name="dsDataSource">データソース</param>
		public void SetDataSource(DataSet dsDataSource)
		{
			// DataSource が null またはテーブルが存在しない場合は処理を終了
			if (dsDataSource == null || dsDataSource.Tables.Count == 0)
			{
				return;
			}

			// テーブルが1つしかない場合は、そのまま DataSource に設定して終了
			if (dsDataSource.Tables.Count == 1)
			{
				this.DataSource = dsDataSource;
				return;
			}

			// メインテーブル（HEADER_TABLE_NAME）が存在しない場合は例外を投げる
			if (!dsDataSource.Tables.Contains(HEADER_TABLE_NAME))
				throw new ArgumentException($"'{HEADER_TABLE_NAME}'のテーブルが存在しません。");

			DataTable mainTable = dsDataSource.Tables[HEADER_TABLE_NAME];
			DataTable merged = null;

			var possibleDetailNames = new[] { DETAILS_TABLE_NAME, DETAILS_TABLE_NAME2, DETAILS_TABLE_NAME3 };

			// 明細テーブル名が未設定の場合、候補の中から存在するテーブルを探す
			if (string.IsNullOrEmpty(this._DetailTableName))
			{
				foreach (DataTable dt in dsDataSource.Tables)
				{
					if (!possibleDetailNames.Contains(dt.TableName))
						continue;

					this._DetailTableName = dt.TableName;
					break;
				}	
			}

			// 明細テーブルが存在する場合、メインテーブルとマージする
			if (!string.IsNullOrEmpty(this._DetailTableName))
			{
				merged = MergeMainWithDetails(mainTable, dsDataSource.Tables[this._DetailTableName]);
			}

			// マージ結果が null の場合は、メインテーブルのみを使用
			if (merged == null)
				merged = mainTable;

			this._dtData = merged;
			this._dtData.TableName = MAIN_TABLE_NAME;

			// 既に同名テーブルが存在する場合はクリアしてマージ
			if (dsDataSource.Tables.Contains(MAIN_TABLE_NAME))
			{
				dsDataSource.Tables[MAIN_TABLE_NAME].Clear();
				dsDataSource.Tables[MAIN_TABLE_NAME].Merge(this._dtData);
			}
			else
			{
				// 存在しない場合は新規追加
				dsDataSource.Tables.Add(this._dtData);
			}
			// DataSource と DataMember を設定
			(this.DataSource, this.DataMember) = (dsDataSource, MAIN_TABLE_NAME);
			// RVSC123103 : レポートのテストのために_dtDataをCSVとして出力します。作業が完了した後に削除します。
			DateTime currentDateTime = DateTime.Now;
			string timeStamp = currentDateTime.ToString("yyyyMMdd_HHmmss");
			string pathfilecsv = Path.Combine("C:\\Wins.Net\\DataTable2Csv", $"{Path.GetFileNameWithoutExtension(this.ResourceLocator.BaseUri.LocalPath)}_{timeStamp}.csv");
			ExportDataTableToCSV(this._dtData, pathfilecsv);
		}

		/// <summary>
		/// RVSC123103
		/// レポートのテストのために_dtDataをCSVとして出力します。作業が完了した後に削除します。
		/// </summary>
		/// <param name="dt">メインの DataTable（ヘッダデータ）</param>
		/// <param name="filePath">filePath save file csv</param>
		/// <returns>結合された DataTable</returns>
		public static void ExportDataTableToCSV(DataTable dt, string filePath)
		{
			StringBuilder sb = new StringBuilder();

			// Thêm tiêu đề cột vào CSV
			foreach (DataColumn column in dt.Columns)
			{
				sb.Append(column.ColumnName + ";");
			}
			sb.AppendLine();

			// Thêm dữ liệu vào CSV
			foreach (DataRow row in dt.Rows)
			{
				foreach (var item in row.ItemArray)
				{
					sb.Append(item.ToString() + ";");
				}
				sb.AppendLine();
			}

			// Lưu vào file CSV
			File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
		}


		/// <summary>
		/// メインテーブルと詳細テーブルを結合し、新しい DataTable を生成します。
		/// 詳細テーブルの列名が重複する場合は「_Detail」を付加します。
		/// </summary>
		/// <param name="mainTable">メインの DataTable（ヘッダデータ）</param>
		/// <param name="detailTable">詳細の DataTable</param>
		/// <returns>結合された DataTable</returns>
		public static DataTable MergeMainWithDetails(DataTable mainTable, DataTable detailTable)
		{
			DataTable merged = new DataTable();

			foreach (DataColumn col in mainTable.Columns)
			{
				merged.Columns.Add(col.ColumnName, col.DataType);
			}

			foreach (DataColumn col in detailTable.Columns)
			{
				string newColName = col.ColumnName;
				while (merged.Columns.Contains(newColName))
					newColName += "_Detail";

				merged.Columns.Add(newColName, col.DataType);
			}

			DataRow mainRow = mainTable.Rows[0];
			for (int i = 0; i < mainTable.Columns.Count; i++)
			{
				merged.Columns[i].DefaultValue = mainRow[i];
			}

			if (detailTable.Rows.Count == 0)
			{
				DataRow newRow = merged.NewRow();
				merged.Rows.Add(newRow);
				return merged;
			}

			foreach (DataRow detailRow in detailTable.Rows)
			{
				DataRow newRow = merged.NewRow();

				for (int j = 0; j < detailTable.Columns.Count; j++)
				{
					newRow[mainTable.Columns.Count + j] = detailRow[j];
				}

				merged.Rows.Add(newRow);
			}

			return merged;
		}

		/// <summary>
		/// レポートをプリンターに印刷します。
		/// </summary>
		/// <param name="nCopies">印刷部数</param>
		/// <param name="collated">部単位で印刷するかどうか（コラテート）</param>
		/// <param name="startPageN">印刷開始ページ番号</param>
		/// <param name="endPageN">印刷終了ページ番号</param>

		public void PrintToPrinter(int nCopies, bool collated, int startPageN, int endPageN)
		{
			this.Document.Printer.PrinterSettings.Copies = (short)nCopies;
			this.Document.Printer.PrinterSettings.Collate = collated;
			this.Document.Printer.PrinterSettings.FromPage = startPageN;
			this.Document.Printer.PrinterSettings.ToPage = endPageN;
			this.Run();
			this.Document.Print();
		}

		/// <summary>
		/// 指定したパスからレイアウトXMLを読み込み、レポートのレイアウトを設定します。
		/// </summary>
		/// <param name="path">レイアウトXMLファイルのパス</param>
		public new void LoadLayout(string path)
		{
			using (System.Xml.XmlTextReader xtr = new System.Xml.XmlTextReader(path))
			{
				base.LoadLayout(xtr);
				xtr.Close();
			}

			// レポートのパスを保存します。
			this._FilePath = path;

			// 詳細テーブル名を設定する
			if (base.Parameters.Contains(base.Parameters["詳細テーブル名"]))
			{
				var value = base.Parameters["詳細テーブル名"].Value;
				if (value != null)
				{
					this._DetailTableName = value.ToString();
				}
			}

		}

		#region 共通利用メソッド
		/// <summary>
		/// テキスト系コントロール背景を透過に揃えます。
		/// </summary>
		protected void SetTransparentBackgrounds()
		{
			// report 参照がなければ section 走査を行えないため、そのまま抜けます。
			if (this == null)
			{
				return;
			}

			// 全 section を対象にし、group header/footer を含めた配色を同じ方針へ揃えます。
			foreach (Section section in this.Sections)
			{
				// control ごとに種類を判定し、テキスト表示系だけ背景透過を適用します。
				foreach (ARControl control in section.Controls)
				{
					// TextBox は集計値や明細値を載せるため、背景を消して帯色や罫線を見やすくします。
					if (control is GrapeCity.ActiveReports.SectionReportModel.TextBox textBox)
					{
						textBox.BackColor = Color.Transparent;
					}
					// Label も同様に透過へ寄せ、デザイン上の背景差異を最小化します。
					else if (control is GrapeCity.ActiveReports.SectionReportModel.Label label)
					{
						label.BackColor = Color.Transparent;
					}
				}
			}
		}

		/// <summary>
		/// DataTable に指定した列が存在しない場合のみ、新しい列を追加する。
		/// 既存列がある場合は何もしない（重複追加による例外回避）。
		/// </summary>
		/// <param name="dt">対象の DataTable。</param>
		/// <param name="colName">追加する列名。</param>
		/// <param name="type">列のデータ型。</param>
		protected void AddCol(System.Data.DataTable dt, string colName, System.Type type)
		{
			if (!dt.Columns.Contains(colName))
				dt.Columns.Add(new System.Data.DataColumn(colName, type));
		}

		/// <summary>
		/// DataSourceからDataTableを取得する
		/// DataSetの場合は「MAIN」テーブルを返却する
		/// </summary>
		protected System.Data.DataTable GetMainTable(object dataSource)
		{
			// DataTableそのまま
			if (dataSource is System.Data.DataTable dt)
			{
				return dt;
			}

			// DataSetの場合はMAINテーブル
			if (dataSource is System.Data.DataSet ds)
			{
				return ds.Tables.Contains("MAIN") ? ds.Tables["MAIN"] : null;
			}

			return null;
		}
		#endregion

	}
}
