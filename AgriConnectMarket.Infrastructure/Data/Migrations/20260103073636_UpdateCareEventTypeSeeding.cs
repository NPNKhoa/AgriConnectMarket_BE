using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgriConnectMarket.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCareEventTypeSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { new Guid("1dc3dc2c-e1b2-433b-9802-9c50944650b4"), "Remove weeds to reduce competition.", "Weeding", "[\"Method\",\"Area treated\",\"Labor\",\"Weed pressure\",\"Notes\"]" },
                    { new Guid("4fac5486-aaf5-484b-a61d-e54b0a5a7630"), "Prepare the soil before planting.", "Soil preparation", "[\"Method\",\"Equipment used\",\"Tillage depth\",\"Number of passes\",\"Soil amendment (type)\",\"Soil amendment (amount)\",\"Fuel consumed\",\"Notes\"]" },
                    { new Guid("50fd455b-6dd1-472f-ac0b-d5e695af638b"), "Record harvest timing and quantities.", "Harvest", "[\"Harvest date\",\"Time\",\"Quantity\",\"Grade\",\"Worker team\",\"Post-harvest lot\",\"Destination\",\"Notes\"]" },
                    { new Guid("57600985-3561-474e-8eff-1945adba105f"), "Record plant growth and identify risks early.", "Growth monitoring", "[\"Observations\",\"Growth/height\",\"Pest or disease signs\",\"Photos\",\"Recommendations\",\"Follow-up tasks\"]" },
                    { new Guid("73f77891-29f6-40f8-8dcc-b4d3f4cca9d0"), "Provide nutrients to the crop.", "Fertilization", "[\"Product name\",\"Formula\",\"Type (organic/synthetic)\",\"Rate\",\"Application method\",\"Withholding period\",\"Supplier\",\"Batch number\",\"Notes\"]" },
                    { new Guid("7e5f74bd-d097-4f1e-ae91-a6f6c8e1efac"), "Provide water to crops.", "Irrigation", "[\"Irrigation method\",\"Duration\",\"Water volume\",\"Water source\",\"Water treatment\",\"Weather notes\"]" },
                    { new Guid("95542943-d256-47cc-9b7d-f1924bfd2691"), "Analyze soil samples and record results.", "Soil testing", "[\"Sampling date\",\"Sampling location\",\"Laboratory\",\"Parameters tested\",\"Results\",\"Recommendations\",\"Attachments\"]" },
                    { new Guid("98e7d68d-48f6-47b2-9277-b494f0825bc8"), "Manage pests or diseases using biological or chemical methods.", "Pest and disease control", "[\"Target pest/disease\",\"Product name\",\"Active ingredient\",\"Rate\",\"Dilution\",\"PHI (pre-harvest interval)\",\"REI (re-entry interval)\",\"Application equipment\",\"Weather during application\",\"PPE confirmation\",\"Notes\"]" },
                    { new Guid("c78cdd5a-6958-43d8-a57a-08dec93cba12"), "Support or record pollination activities.", "Pollination", "[\"Pollination method\",\"Hive placement\",\"Bee density\",\"Estimated fruit set\",\"Notes\"]" },
                    { new Guid("da39f35a-8617-4681-bafe-15037a03e752"), "Record the planting or transplanting process.", "Planting / transplanting", "[\"Variety / seed lot\",\"Supplier\",\"Spacing / density\",\"Planting method\",\"Germination rate\",\"Completion date\",\"Notes\"]" },
                    { new Guid("f95320ca-0041-43e7-bfd0-c9e725d683a3"), "Adjust canopy, branches, or fruits to optimize growth.", "Pruning / training", "[\"Operation type\",\"Purpose\",\"Area / number of plants\",\"Waste handling\",\"Notes\"]" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("1dc3dc2c-e1b2-433b-9802-9c50944650b4"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("4fac5486-aaf5-484b-a61d-e54b0a5a7630"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("50fd455b-6dd1-472f-ac0b-d5e695af638b"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("57600985-3561-474e-8eff-1945adba105f"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("73f77891-29f6-40f8-8dcc-b4d3f4cca9d0"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("7e5f74bd-d097-4f1e-ae91-a6f6c8e1efac"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("95542943-d256-47cc-9b7d-f1924bfd2691"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("98e7d68d-48f6-47b2-9277-b494f0825bc8"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("c78cdd5a-6958-43d8-a57a-08dec93cba12"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("da39f35a-8617-4681-bafe-15037a03e752"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("f95320ca-0041-43e7-bfd0-c9e725d683a3"));

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
    }
}
