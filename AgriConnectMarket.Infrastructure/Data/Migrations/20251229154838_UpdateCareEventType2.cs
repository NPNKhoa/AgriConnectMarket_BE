using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgriConnectMarket.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCareEventType2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("070dbd9a-cf55-4c32-8988-2dd0b5ee3203"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("1b04e1a2-1cf2-4fd9-af64-b5d6f8771a5e"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("3c3dfe79-3c58-415d-9379-805fa113f186"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("4887f3a4-bfb6-4c48-9184-d66f5361417c"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("52b1a2ac-fadf-46ec-a6cd-61c5666a2534"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("7a781475-b0c5-432a-9a4f-447774844c7d"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("7d17fb84-f4f9-4fe6-accf-7b3a1908715b"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("8f6368e8-2027-4855-b8de-c8463836c817"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("93d19e88-a4e3-4cf3-898e-af8672a4fa66"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("99f8cae6-e2b1-44dd-b6e5-d6c6ede961fa"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("ab6c04bf-29be-44c0-b174-72c3887e60c1"));

            migrationBuilder.InsertData(
                table: "CareEventTypes",
                columns: new[] { "Id", "EventTypeDesc", "EventTypeName", "PayloadFields" },
                values: new object[,]
                {
                    { new Guid("005ddfcf-dde4-4f32-ba7d-794c1b16583f"), "Điều chỉnh tán lá, cành hoặc quả để tối ưu sinh trưởng.", "Tỉa / tạo tán", "[\"Loại tác động\",\"Mục đích\",\"Khu vực / số cây\",\"Xử lý phế phẩm\",\"Ghi chú\"]" },
                    { new Guid("27b22442-2d9c-4e37-9e1a-4b0a0c8bc4c9"), "Ghi nhận thời điểm và sản lượng thu hoạch.", "Thu hoạch", "[\"Ngày thu hoạch\",\"Thời gian\",\"Sản lượng\",\"Phân loại\",\"Nhóm lao động\",\"Mã lô sau thu hoạch\",\"Nơi chuyển đến\",\"Ghi chú\"]" },
                    { new Guid("6db655ee-b1f2-4b82-bf4e-4a8faff20816"), "Ghi nhận quá trình gieo hạt hoặc trồng cây con.", "Gieo trồng / cấy", "[\"Giống / mã lô\",\"Nhà cung cấp\",\"Mật độ / khoảng cách\",\"Hình thức gieo trồng\",\"Tỷ lệ nảy mầm\",\"Ngày hoàn thành\",\"Ghi chú\"]" },
                    { new Guid("6faa6170-6ed3-4755-9f5e-527b88db7f38"), "Quản lý sâu bệnh bằng biện pháp sinh học hoặc hoá học.", "Phòng trừ sâu bệnh", "[\"Đối tượng (sâu/bệnh)\",\"Tên sản phẩm\",\"Hoạt chất\",\"Liều lượng\",\"Nồng độ pha\",\"PHI (thời gian cách ly)\",\"REI (thời gian cách ly lao động)\",\"Thiết bị phun\",\"Thời tiết khi phun\",\"Xác nhận PPE\",\"Ghi chú\"]" },
                    { new Guid("714c73b2-f1f8-4a77-9bc6-852ce318b995"), "Ghi nhận tình trạng sinh trưởng và phát hiện sớm rủi ro.", "Theo dõi sinh trưởng", "[\"Nhận xét\",\"Chiều cao / sinh trưởng\",\"Dấu hiệu sâu bệnh\",\"Ảnh chụp\",\"Khuyến nghị\",\"Công việc tiếp theo\"]" },
                    { new Guid("8fe8eb75-e5b3-4425-9308-c41cd39d38e3"), "Phân tích mẫu đất và ghi nhận kết quả.", "Kiểm tra đất", "[\"Ngày lấy mẫu\",\"Vị trí lấy mẫu\",\"Phòng thí nghiệm\",\"Chỉ tiêu phân tích\",\"Kết quả\",\"Khuyến nghị\",\"Tệp đính kèm\"]" },
                    { new Guid("99638d3a-b2d1-4ed3-a7a8-391d0902fa87"), "Hoạt động hỗ trợ hoặc ghi nhận quá trình thụ phấn.", "Thụ phấn", "[\"Hình thức thụ phấn\",\"Vị trí tổ ong\",\"Mật độ ong\",\"Tỷ lệ đậu quả ước tính\",\"Ghi chú\"]" },
                    { new Guid("a06de8f7-245b-49fb-99b0-ab5611124ce1"), "Cung cấp dinh dưỡng cho cây trồng.", "Bón phân", "[\"Tên sản phẩm\",\"Công thức\",\"Loại (hữu cơ/hoá học)\",\"Liều lượng\",\"Phương pháp bón\",\"Thời gian cách ly\",\"Nhà cung cấp\",\"Mã lô\",\"Ghi chú\"]" },
                    { new Guid("aec0fdb4-f5ff-4bc2-a7ca-c7b3f2516790"), "Hoạt động chuẩn bị đất trước khi gieo trồng.", "Chuẩn bị đất", "[\"Phương pháp\",\"Máy móc thiết bị sử dụng\",\"Độ sâu làm đất\",\"Số lần làm đất\",\"Chất cải tạo đất (loại)\",\"Chất cải tạo đất (lượng)\",\"Nhiên liệu tiêu thụ\",\"Ghi chú\"]" },
                    { new Guid("d39dee82-bd00-4d38-a6f3-ee9b73106953"), "Loại bỏ cỏ dại để giảm cạnh tranh dinh dưỡng.", "Làm cỏ", "[\"Phương pháp\",\"Diện tích xử lý\",\"Nhân công\",\"Mức độ cỏ dại\",\"Ghi chú\"]" },
                    { new Guid("fce636ea-e083-4cc9-9b03-d4f3c1b9d338"), "Hoạt động tưới và cung cấp nước cho cây.", "Tưới nước", "[\"Phương pháp tưới\",\"Thời gian tưới\",\"Lượng nước\",\"Nguồn nước\",\"Xử lý nước\",\"Ghi chú thời tiết\"]" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("005ddfcf-dde4-4f32-ba7d-794c1b16583f"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("27b22442-2d9c-4e37-9e1a-4b0a0c8bc4c9"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("6db655ee-b1f2-4b82-bf4e-4a8faff20816"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("6faa6170-6ed3-4755-9f5e-527b88db7f38"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("714c73b2-f1f8-4a77-9bc6-852ce318b995"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("8fe8eb75-e5b3-4425-9308-c41cd39d38e3"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("99638d3a-b2d1-4ed3-a7a8-391d0902fa87"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("a06de8f7-245b-49fb-99b0-ab5611124ce1"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("aec0fdb4-f5ff-4bc2-a7ca-c7b3f2516790"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("d39dee82-bd00-4d38-a6f3-ee9b73106953"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("fce636ea-e083-4cc9-9b03-d4f3c1b9d338"));

            migrationBuilder.InsertData(
                table: "CareEventTypes",
                columns: new[] { "Id", "EventTypeDesc", "EventTypeName", "PayloadFields" },
                values: new object[,]
                {
                    { new Guid("070dbd9a-cf55-4c32-8988-2dd0b5ee3203"), "Hoạt động chuẩn bị đất trước khi gieo trồng.", "Chuẩn bị đất", "{\"Phương pháp\",\"Máy móc thiết bị sử dụng\",\"Độ sâu làm đất\",\"Số lần làm đất\",\"Chất cải tạo đất (loại)\",\"Chất cải tạo đất (lượng)\",\"Nhiên liệu tiêu thụ\",\"Ghi chú\"}" },
                    { new Guid("1b04e1a2-1cf2-4fd9-af64-b5d6f8771a5e"), "Hoạt động hỗ trợ hoặc ghi nhận quá trình thụ phấn.", "Thụ phấn", "{\"Hình thức thụ phấn\",\"Vị trí tổ ong\",\"Mật độ ong\",\"Tỷ lệ đậu quả ước tính\",\"Ghi chú\"}" },
                    { new Guid("3c3dfe79-3c58-415d-9379-805fa113f186"), "Cung cấp dinh dưỡng cho cây trồng.", "Bón phân", "{\"Tên sản phẩm\",\"Công thức\",\"Loại (hữu cơ/hoá học)\",\"Liều lượng\",\"Phương pháp bón\",\"Thời gian cách ly\",\"Nhà cung cấp\",\"Mã lô\",\"Ghi chú\"}" },
                    { new Guid("4887f3a4-bfb6-4c48-9184-d66f5361417c"), "Quản lý sâu bệnh bằng biện pháp sinh học hoặc hoá học.", "Phòng trừ sâu bệnh", "{\"Đối tượng (sâu/bệnh)\",\"Tên sản phẩm\",\"Hoạt chất\",\"Liều lượng\",\"Nồng độ pha\",\"PHI (thời gian cách ly)\",\"REI (thời gian cách ly lao động)\",\"Thiết bị phun\",\"Thời tiết khi phun\",\"Xác nhận PPE\",\"Ghi chú\"}" },
                    { new Guid("52b1a2ac-fadf-46ec-a6cd-61c5666a2534"), "Ghi nhận thời điểm và sản lượng thu hoạch.", "Thu hoạch", "{\"Ngày thu hoạch\",\"Thời gian\",\"Sản lượng\",\"Phân loại\",\"Nhóm lao động\",\"Mã lô sau thu hoạch\",\"Nơi chuyển đến\",\"Ghi chú\"}" },
                    { new Guid("7a781475-b0c5-432a-9a4f-447774844c7d"), "Phân tích mẫu đất và ghi nhận kết quả.", "Kiểm tra đất", "{\"Ngày lấy mẫu\",\"Vị trí lấy mẫu\",\"Phòng thí nghiệm\",\"Chỉ tiêu phân tích\",\"Kết quả\",\"Khuyến nghị\",\"Tệp đính kèm\"}" },
                    { new Guid("7d17fb84-f4f9-4fe6-accf-7b3a1908715b"), "Loại bỏ cỏ dại để giảm cạnh tranh dinh dưỡng.", "Làm cỏ", "{\"Phương pháp\",\"Diện tích xử lý\",\"Nhân công\",\"Mức độ cỏ dại\",\"Ghi chú\"}" },
                    { new Guid("8f6368e8-2027-4855-b8de-c8463836c817"), "Ghi nhận tình trạng sinh trưởng và phát hiện sớm rủi ro.", "Theo dõi sinh trưởng", "{\"Nhận xét\",\"Chiều cao / sinh trưởng\",\"Dấu hiệu sâu bệnh\",\"Ảnh chụp\",\"Khuyến nghị\",\"Công việc tiếp theo\"}" },
                    { new Guid("93d19e88-a4e3-4cf3-898e-af8672a4fa66"), "Hoạt động tưới và cung cấp nước cho cây.", "Tưới nước", "{\"Phương pháp tưới\",\"Thời gian tưới\",\"Lượng nước\",\"Nguồn nước\",\"Xử lý nước\",\"Ghi chú thời tiết\"}" },
                    { new Guid("99f8cae6-e2b1-44dd-b6e5-d6c6ede961fa"), "Ghi nhận quá trình gieo hạt hoặc trồng cây con.", "Gieo trồng / cấy", "{\"Giống / mã lô\",\"Nhà cung cấp\",\"Mật độ / khoảng cách\",\"Hình thức gieo trồng\",\"Tỷ lệ nảy mầm\",\"Ngày hoàn thành\",\"Ghi chú\"}" },
                    { new Guid("ab6c04bf-29be-44c0-b174-72c3887e60c1"), "Điều chỉnh tán lá, cành hoặc quả để tối ưu sinh trưởng.", "Tỉa / tạo tán", "{\"Loại tác động\",\"Mục đích\",\"Khu vực / số cây\",\"Xử lý phế phẩm\",\"Ghi chú\"}" }
                });
        }
    }
}
