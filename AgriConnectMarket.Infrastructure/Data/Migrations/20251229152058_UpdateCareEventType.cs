using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgriConnectMarket.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCareEventType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PayloadFields",
                table: "CareEventTypes",
                type: "nvarchar(max)",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "PayloadFields",
                table: "CareEventTypes");
        }
    }
}
