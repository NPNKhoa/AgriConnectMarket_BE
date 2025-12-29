using AgriConnectMarket.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgriConnectMarket.Infrastructure.Data
{
    public static class CareEventTypeSeeding
    {
        public static void ExecuteSeeding(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CareEventType>().HasData(
                new CareEventType
                (
                    Guid.NewGuid(),
                    "Chuẩn bị đất",
                    "Hoạt động chuẩn bị đất trước khi gieo trồng.",
                    "[\"Phương pháp\",\"Máy móc thiết bị sử dụng\",\"Độ sâu làm đất\",\"Số lần làm đất\",\"Chất cải tạo đất (loại)\",\"Chất cải tạo đất (lượng)\",\"Nhiên liệu tiêu thụ\",\"Ghi chú\"]"
                ),
                new CareEventType
                (
                    Guid.NewGuid(),
                    "Kiểm tra đất",
                    "Phân tích mẫu đất và ghi nhận kết quả.",
                    "[\"Ngày lấy mẫu\",\"Vị trí lấy mẫu\",\"Phòng thí nghiệm\",\"Chỉ tiêu phân tích\",\"Kết quả\",\"Khuyến nghị\",\"Tệp đính kèm\"]"
                ),
                new CareEventType
                (
                    Guid.NewGuid(),
                    "Gieo trồng / cấy",
                    "Ghi nhận quá trình gieo hạt hoặc trồng cây con.",
                    "[\"Giống / mã lô\",\"Nhà cung cấp\",\"Mật độ / khoảng cách\",\"Hình thức gieo trồng\",\"Tỷ lệ nảy mầm\",\"Ngày hoàn thành\",\"Ghi chú\"]"
                ),
                new CareEventType
                (
                    Guid.NewGuid(),
                    "Tưới nước",
                    "Hoạt động tưới và cung cấp nước cho cây.",
                    "[\"Phương pháp tưới\",\"Thời gian tưới\",\"Lượng nước\",\"Nguồn nước\",\"Xử lý nước\",\"Ghi chú thời tiết\"]"
                ),
                new CareEventType
                (
                    Guid.NewGuid(),
                    "Bón phân",
                    "Cung cấp dinh dưỡng cho cây trồng.",
                    "[\"Tên sản phẩm\",\"Công thức\",\"Loại (hữu cơ/hoá học)\",\"Liều lượng\",\"Phương pháp bón\",\"Thời gian cách ly\",\"Nhà cung cấp\",\"Mã lô\",\"Ghi chú\"]"
                ),
                new CareEventType
                (
                    Guid.NewGuid(),
                    "Phòng trừ sâu bệnh",
                    "Quản lý sâu bệnh bằng biện pháp sinh học hoặc hoá học.",
                    "[\"Đối tượng (sâu/bệnh)\",\"Tên sản phẩm\",\"Hoạt chất\",\"Liều lượng\",\"Nồng độ pha\",\"PHI (thời gian cách ly)\",\"REI (thời gian cách ly lao động)\",\"Thiết bị phun\",\"Thời tiết khi phun\",\"Xác nhận PPE\",\"Ghi chú\"]"
                ),
                new CareEventType
                (
                    Guid.NewGuid(),
                    "Làm cỏ",
                    "Loại bỏ cỏ dại để giảm cạnh tranh dinh dưỡng.",
                    "[\"Phương pháp\",\"Diện tích xử lý\",\"Nhân công\",\"Mức độ cỏ dại\",\"Ghi chú\"]"
                ),
                new CareEventType
                (
                    Guid.NewGuid(),
                    "Tỉa / tạo tán",
                    "Điều chỉnh tán lá, cành hoặc quả để tối ưu sinh trưởng.",
                    "[\"Loại tác động\",\"Mục đích\",\"Khu vực / số cây\",\"Xử lý phế phẩm\",\"Ghi chú\"]"
                ),
                new CareEventType
                (
                    Guid.NewGuid(),
                    "Theo dõi sinh trưởng",
                    "Ghi nhận tình trạng sinh trưởng và phát hiện sớm rủi ro.",
                    "[\"Nhận xét\",\"Chiều cao / sinh trưởng\",\"Dấu hiệu sâu bệnh\",\"Ảnh chụp\",\"Khuyến nghị\",\"Công việc tiếp theo\"]"
                ),
                new CareEventType
                (
                    Guid.NewGuid(),
                    "Thụ phấn",
                    "Hoạt động hỗ trợ hoặc ghi nhận quá trình thụ phấn.",
                    "[\"Hình thức thụ phấn\",\"Vị trí tổ ong\",\"Mật độ ong\",\"Tỷ lệ đậu quả ước tính\",\"Ghi chú\"]"
                ),
                new CareEventType
                (
                    Guid.NewGuid(),
                    "Thu hoạch",
                    "Ghi nhận thời điểm và sản lượng thu hoạch.",
                    "[\"Ngày thu hoạch\",\"Thời gian\",\"Sản lượng\",\"Phân loại\",\"Nhóm lao động\",\"Mã lô sau thu hoạch\",\"Nơi chuyển đến\",\"Ghi chú\"]"
                )
            );
        }
    }
}
