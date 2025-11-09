using AntdUI;

public class Localizer : ILocalization
{
    public string GetLocalizedString(string key)
    {
        switch (key)
        {
            // ========== BASIC ==========
            case "ID":
                return "en-VN";

            case "Cancel":
                return "Hủy";

            case "OK":
                return "Đồng ý";

            case "Now":
                return "Bây giờ";

            case "ToDay":
                return "Hôm nay";

            case "NoData":
                return "Không có dữ liệu";

            // ========== CALENDAR / DATEPICKER ==========

            // DatePicker placeholders
            case "DatePicker.PlaceholderText":
                return "Chọn ngày";

            case "DatePicker.PlaceholderS":
                return "Ngày bắt đầu";

            case "DatePicker.PlaceholderE":
                return "Ngày kết thúc";

            // ========== NGÀY TRONG TUẦN (Weekdays) ==========
            // Các key này KHÔNG được AntdUI gọi trực tiếp
            // Nhưng giữ lại cho tương lai nếu library cập nhật
            case "Calendar.Monday":
            case "MondayButton":
            case "Mon":
                return "Th2";

            case "Calendar.Tuesday":
            case "TuesdayButton":
            case "Tue":
                return "Th3";

            case "Calendar.Wednesday":
            case "WednesdayButton":
            case "Wed":
                return "Th4";

            case "Calendar.Thursday":
            case "ThursdayButton":
            case "Thu":
                return "Th5";

            case "Calendar.Friday":
            case "FridayButton":
            case "Fri":
                return "Th6";

            case "Calendar.Saturday":
            case "SaturdayButton":
            case "Sat":
                return "Th7";

            case "Calendar.Sunday":
            case "SundayButton":
            case "Sun":
                return "CN";

            // ========== THÁNG (Months) ==========

            case "Calendar.January":
            case "Month.1":
            case "Jan":
                return "Th1";

            case "Calendar.February":
            case "Month.2":
            case "Feb":
                return "Th2";

            case "Calendar.March":
            case "Month.3":
            case "Mar":
                return "Th3";

            case "Calendar.April":
            case "Month.4":
            case "Apr":
                return "Th4";

            case "Calendar.May":
            case "Month.5":
            case "May":
                return "Th5";

            case "Calendar.June":
            case "Month.6":
            case "Jun":
                return "Th6";

            case "Calendar.July":
            case "Month.7":
            case "Jul":
                return "Th7";

            case "Calendar.August":
            case "Month.8":
            case "Aug":
                return "Th8";

            case "Calendar.September":
            case "Month.9":
            case "Sep":
                return "Th9";

            case "Calendar.October":
            case "Month.10":
            case "Oct":
                return "Th10";

            case "Calendar.November":
            case "Month.11":
            case "Nov":
                return "Th11";

            case "Calendar.December":
            case "Month.12":
            case "Dec":
                return "Th12";

            // Tháng viết tắt (3 chữ)
            case "Month.Short.1":
                return "T1";

            case "Month.Short.2":
                return "T2";

            case "Month.Short.3":
                return "T3";

            case "Month.Short.4":
                return "T4";

            case "Month.Short.5":
                return "T5";

            case "Month.Short.6":
                return "T6";

            case "Month.Short.7":
                return "T7";

            case "Month.Short.8":
                return "T8";

            case "Month.Short.9":
                return "T9";

            case "Month.Short.10":
                return "T10";

            case "Month.Short.11":
                return "T11";

            case "Month.Short.12":
                return "T12";

            // ========== PAGINATION ==========
            case "ItemsPerPage":
                return "Số dòng/trang";

            // ========== FILTER ==========
            case "Filter.Clean":
                return "Xóa lọc";

            case "Filter.SelectAll":
                return "(Chọn tất cả)";

            case "Filter.Search":
                return "Tìm kiếm";

            case "Filter.Equal":
                return "Bằng";

            case "Filter.NotEqual":
                return "Khác";

            case "Filter.Contains":
                return "Chứa";

            case "Filter.StartsWith":
                return "Bắt đầu với";

            case "Filter.EndsWith":
                return "Kết thúc với";

            // ========== TABLE ==========
            case "Table.Column.name":
                return "Tên";

            case "Table.Column.address":
                return "Địa chỉ";

            case "Table.Column.btns":
                return "Thao tác";

            default:
                return key; // Fallback to key
        }
    }
}