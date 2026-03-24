# 📊 Phân tích RPX Reports - Controls & Script Review

## 1. Cấu trúc tổng quan RPX (ActiveReports XML)

Mỗi file `.rpx` là một XML có root element `<ActiveReportsLayout>` với các thành phần chính:

### Attributes của `<ActiveReportsLayout>`

| Attribute | Ví dụ | Mô tả |
|---|---|---|
| `Version` | `3.2`, `3.5` | Phiên bản format RPX |
| `PrintWidth` | `11333`, `15987` | Chiều rộng in (twips) |
| `DocumentName` | `KP010020` | Tên document |
| `ScriptLang` | `C#` | Ngôn ngữ script nhúng |
| `MasterReport` | `0` | Report chính hay sub-report |

---

## 2. Danh sách Control Types tìm thấy trong RPX files

| RPX Type | C# Mapping | Mô tả | Ví dụ trong file |
|---|---|---|---|
| `AR.Label` | `Label` | Nhãn text tĩnh (Caption) | `Text2`, `Text3`, `部門コード見出し1` |
| `AR.Field` | `TextField` | Text field bind data (DataField) | `Field2`, `Field4`, `部門コード1` |
| `AR.Line` | `Line` | Đường kẻ (X1,Y1 → X2,Y2) | `Line1`, `Line2`, `Line17` |
| `AR.CrossSectionBox` | `CrossSectionBox` | Box kéo dài qua nhiều section | `CrossSectionBox1` |
| `AR.TextBox` | `TextBox` | _(Declared in mapping, chưa thấy trong file)_ | — |
| `AR.Rectangle` | `Rectangle` | _(Declared in mapping, chưa thấy trong file)_ | — |
| `AR.Image` | `Image` | _(Declared in mapping, chưa thấy trong file)_ | — |
| `AR.CheckBox` | `CheckBox` | _(Declared in mapping, chưa thấy trong file)_ | — |
| `AR.ComboBox` | `ComboBox` | _(Declared in mapping, chưa thấy trong file)_ | — |

### Thuộc tính phổ biến của Control

| Attribute | Áp dụng cho | Mô tả |
|---|---|---|
| `Name` | Tất cả | Tên control (unique trong section) |
| `Type` | Tất cả | Loại control (`AR.Label`, `AR.Field`, ...) |
| `Left`, `Top`, `Width`, `Height` | Label, Field | Vị trí và kích thước (twips) |
| `Visible` | Label, Field | `0` = ẩn, mặc định hiển thị |
| `Caption` | Label | Text tĩnh hiển thị |
| `DataField` | Field | Tên cột dữ liệu bind |
| `Text` | Field | Giá trị mặc định / biểu thức |
| `OutputFormat` | Field | Format hiển thị (`#,##0`, `0.0`) |
| `CanGrow` | Field | Có tự mở rộng không |
| `Style` | Label, Field | CSS-like style (font, color, align) |
| `SummaryType` | Field | Loại tổng hợp (`4` = PageCount) |
| `SummaryRunning` | Field | Phạm vi tổng hợp (`2` = Over All) |
| `X1`, `Y1`, `X2`, `Y2` | Line | Tọa độ đầu/cuối |
| `LineStyle` | Line | Kiểu đường (`3` = dashed) |
| `LineWeight` | Line | Độ dày đường kẻ |
| `CloseBorder` | CrossSectionBox | Đóng viền |

---

## 3. Phân tích chi tiết từng RPX file

### 📄 `KP010020.rpx` — Báo cáo dự toán ngân sách (予算)

- **Version**: 3.2
- **PrintWidth**: 11333
- **Orientation**: 1 (Portrait)

| Section | Type | # Controls | Chi tiết |
|---|---|---|---|
| Section1 | ReportHeader | 0 | `Visible="0"`, `Height="0"` — ẩn hoàn toàn |
| Section2 | PageHeader | 15 | 4× `AR.Label`, 10× `AR.Field`, 1× `AR.Line` |
| Section3 | Detail | 6 | 5× `AR.Field`, 1× `AR.Line` |
| Section5 | PageFooter | 0 | — |
| Section4 | ReportFooter | 0 | — |

**Đặc điểm:**
- Controls ẩn (`Visible="0"`): `Text4`, `所属表示1`, `補正予算額1`, `所属表示2`
- Sử dụng **CalculatedFields** (3 fields)
- Có **Script** nhúng C# (xem mục 4)

### 📄 `KP031110.rpx` — Báo cáo bộ phận (部門)

- **Version**: 3.5
- **PrintWidth**: 15987
- **Orientation**: 2 (Landscape)

| Section | Type | # Controls | Chi tiết |
|---|---|---|---|
| Section1 | ReportHeader | 0 | `Visible="0"` |
| Section2 | PageHeader | 1 | 1× `AR.Line` (`Visible="0"`) |
| Section6 | **GroupHeader** | 6 | 1× `AR.CrossSectionBox`, 5× `AR.Label` |
| Section3 | Detail | 6 | 5× `AR.Field`, 1× `AR.Line` |
| Section7 | GroupFooter | 0 | — |
| Section5 | PageFooter | 0 | `Visible="0"` |
| Section4 | ReportFooter | 0 | `Visible="0"` |

**Đặc điểm:**
- Sử dụng **GroupHeader/GroupFooter** (group by `部門名`, `NewPage="1"`)
- Có `AR.CrossSectionBox` — control đặc biệt kéo dài qua sections
- Có **9 CalculatedFields** (4 fields có formula rỗng)
- **Không có Script** nhúng

---

## 4. 🔍 Review Script Code

### `KP010020.rpx` — Script nhúng

```csharp
Dim dbRec
Dim strSQL
Dim intDeptNo
Dim curBudget, curAlloc, curAdjust
Dim curTotal

' Only for 部門上位
If IsNothing(Fields!DeptTop.Value) Then
    txtDeptTop.Visible = False
Else
    txtDeptTop.Visible = True
    txtDeptTop.Text = Fields!DeptTop.Value
End If

' Chỉ cho phép xem tự động
If UserId() = "admin" Or UserId() = "viewer" Then
    Dim objRpt
    Set objRpt = CreateObject("ShDocVw.InternetExplorer.9")
    objRpt.Visible = True
    objRpt.Navigate "about:blank"
    'Delay 1s
    Dim objShell
    Set objShell = CreateObject("WScript.Shell")
    objShell.AppActivate "Internet Explorer"
    WScript.Sleep 1000
    objShell.SendKeys "^p"
    WScript.Sleep 1000
    objShell.SendKeys "{ENTER}"
    WScript.Sleep 1000
    objShell.SendKeys "^w"
    Set objShell = Nothing
    Set objRpt = Nothing
Else
    ' Khóa mọi thao tác
    report.Items("Detail").Enabled = False
    report.Items("PageHeader").Enabled = False
    report.Items("PageFooter").Enabled = False
    report.Items("GroupHeader").Enabled = False
    report.Items("GroupFooter").Enabled = False
    ' Hiện thông báo Người dùng không có quyền
    MsgBox "Bạn không có quyền thực hiện hành động này!", vbCritical, "ERROR"
    End
End If

Sub Report_PageEnd()
    Dim curPage
    curPage = report.CurrentPageIndex + 1
    ' In trang chẵn (tính từ 1)
    If curPage Mod 2 = 0 Then
        ' Dừng báo cáo
        Report.Cancel
    End If
End Sub
```

### Review nhận xét

| # | Vấn đề | Mức độ | Chi tiết |
|---|---|---|---|
| 1 | **Typo tên method** | ⚠️ Low | `SetBgColerTransparent` → nên là `SetBgColorTransparent` (thiếu chữ `o` trong `Color`) |
| 2 | **Event handler gắn vào Section ẩn** | ⚠️ Medium | `Section1_Format` gắn vào `ReportHeader` (Section1) có `Visible="0"` và `Height="0"`. Event `Format` có thể không được trigger → 3 section không được set transparent. |
| 3 | **Explicit cast thay vì pattern matching** | 💡 Style | Dùng `(TextBox) ctrl` kiểu cũ. C# hiện đại nên dùng: `if (ctrl is TextBox tbx) { ... }` |
| 4 | **Không nhất quán fully-qualified name** | ⚠️ Low | `Label` cast dùng `GrapeCity.ActiveReports.SectionReportModel.Label` nhưng `TextBox` thì dùng short name — không nhất quán |
| 5 | **Thiếu xử lý các loại control khác** | 💡 Info | Chỉ handle `TextBox` và `Label`, bỏ qua `Line`, `CrossSectionBox`, v.v. (OK vì Line không có BackColor) |
| 6 | **Nên dùng `else if`** | 💡 Style | Một control không thể vừa là `TextBox` vừa là `Label`, nên dùng `else if` để tránh check thừa |

### Gợi ý cải tiến Script (C# hiện đại)


### `KP031110.rpx` — Không có Script

File này không chứa `<Script>` block. Toàn bộ logic nằm trong **CalculatedFields**.

---

## 5. Review CalculatedFields

### `KP010020.rpx` (3 fields)

| Field Name | Formula | Nhận xét |
|---|---|---|
| `科目コードcal` | `=科目コード.Substring(0,2) + '-' + ...Substring(8,2)` | Format mã khoa mục: `XX-XX-XX-XX-XX` |
| `所属表示` | `=(備考1 == "000000") ? "所属コード" : ""` | Hiển thị label có điều kiện |
| `所属コード表示` | `=(備考1 == "000000") ? 所属コード : ""` | Hiển thị giá trị có điều kiện |

✅ Logic đơn giản, rõ ràng.

### `KP031110.rpx` (9 fields)

| Field Name | Formula | Nhận xét |
|---|---|---|
| `郵便番号sub` | `="〒 " + 郵便番号.ToString()` | Format mã bưu điện |
| `支払方法sub` | `=支払方法 + "：" + 支払方法名称` | Ghép phương thức thanh toán |
| `金融機関名称sub` | `=銀行番号 + "-" + 支店番号 + " " + 支店名称.TrimEnd()` | Ghép thông tin ngân hàng |
| `預貯金種目sub` | `=預貯金種目 + ": " + 預貯金種目名称` | Ghép loại tiết kiệm |
| `予算コードsub` | `=予算コード.Substring(0,2)+"-"+...` | Format giống `科目コードcal` |
| `総計件数sub` | _(rỗng)_ | ⚠️ **Formula rỗng** |
| `総計表示sub` | _(rỗng)_ | ⚠️ **Formula rỗng** |
| `総計表示2sub` | _(rỗng)_ | ⚠️ **Formula rỗng** |
| `総計金額sub` | _(rỗng)_ | ⚠️ **Formula rỗng** |
| `業者名称` | `=債権者コード + 債権者漢字名称` | Ghép mã + tên nghiệp vụ |

⚠️ **4 fields có formula rỗng** — có thể là dead fields hoặc được populate từ code-behind bên ngoài. Cần kiểm tra thêm.

---

## 6. ⚠️ Vấn đề trong RpxParser hiện tại

Parser (`RpxParser.cs`) hiện chỉ trích xuất `Sections` → `Controls`. Nhiều thành phần quan trọng **chưa được parse**:

| Thành phần | Đã parse? | Ảnh hưởng |
|---|---|---|
| `<Sections>/<Section>/<Control>` | ✅ Yes | — |
| `<CalculatedFields>` | ❌ **No** | Mất thông tin computed fields |
| `<Script>` | ❌ **No** | Mất toàn bộ code logic nhúng |
| `<PageSettings>` | ❌ **No** | Mất thông tin layout (khổ giấy, margins, orientation) |
| `<StyleSheet>` | ❌ **No** | Mất định nghĩa styles |
| `<Parameters>` | ❌ **No** | Mất report parameters |
| `AR.CrossSectionBox` mapping | ❌ **No** | `GetControlType()` trả về `"ARControl"` (fallback) |
| Section attributes (`KeepTogether`, `NewPage`, `CanShrink`, `DataField`) | ❌ **No** | Mất thông tin behavior của section |

---

## 7. Thống kê tổng hợp

| Metric | Giá trị |
|---|---|
| Tổng số RPX files trong `rpx_folder` | **187** |
| Control types thực tế sử dụng | 4 (`AR.Label`, `AR.Field`, `AR.Line`, `AR.CrossSectionBox`) |
| Control types declared trong mapping | 8 (thêm `TextBox`, `Rectangle`, `Image`, `CheckBox`, `ComboBox`) |
| Files có Script nhúng | Ít nhất 1 (`KP010020.rpx`) |
| Files có GroupHeader/GroupFooter | Ít nhất 1 (`KP031110.rpx`) |
| Files có CalculatedFields rỗng | Ít nhất 1 (`KP031110.rpx` — 4 fields) |


