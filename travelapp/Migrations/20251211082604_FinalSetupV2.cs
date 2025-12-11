using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace travelapp.Migrations
{
    /// <inheritdoc />
    public partial class FinalSetupV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityId);
                });

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    PlaceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.PlaceId);
                    table.ForeignKey(
                        name: "FK_Places_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    FeedbackId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlaceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.FeedbackId);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "PlaceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "CityId", "CityUrl", "Name" },
                values: new object[,]
                {
                    { 6, "/Pictures/anitkabir.png", "Ankara" },
                    { 17, "/Pictures/canakkale.png", "Çanakkale" },
                    { 26, "/Pictures/eskisehir.png", "Eskişehir" },
                    { 34, "/Pictures/kizkulesi.png", "İstanbul" },
                    { 35, "/Pictures/saatkulesi.png", "İzmir" }
                });

            migrationBuilder.InsertData(
                table: "Places",
                columns: new[] { "PlaceId", "CityId", "Description", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 1, 34, "Şehrin harika manzarası.", "/Pictures/galata.png", "Galata Kulesi" },
                    { 2, 34, "Boğazın incisi.", "/Pictures/kizkulesi.png", "Kız Kulesi" },
                    { 3, 34, "Tarihi yarımadanın kalbi.", "/Pictures/sultanahmet.png", "Sultan Ahmet Cami" },
                    { 4, 34, "Gizemli sarnıç.", "/Pictures/yerebatan.png", "Yerebatan Sarnıcı" },
                    { 5, 6, "Atamızın istirahatgahı.", "/Pictures/anitkabir.png", "Anıtkabir" },
                    { 6, 6, "Tarihi cezaevi müzesi.", "/Pictures/ulucanlar.png", "Ulucanlar Cezaevi" },
                    { 7, 6, "Huzurlu bir park.", "/Pictures/kugulupark.png", "Kuğulu Park" },
                    { 8, 6, "Meclis tarihi.", "/Pictures/tbmm.png", "TBMM Müzesi" },
                    { 9, 35, "İzmir'in sembolü.", "/Pictures/saatkulesi.png", "Saat Kulesi" },
                    { 10, 35, "Antik tarih.", "/Pictures/efes.png", "Efes Antik Kenti" },
                    { 11, 35, "Manzara noktası.", "/Pictures/tarihiasansor.png", "Tarihi Asansör" },
                    { 12, 26, "Masalsı bir yapı.", "/Pictures/eskisehir.png", "Masal Şatosu" },
                    { 13, 26, "Renkli tarihi evler.", "/Pictures/odunpazari.png", "Odunpazarı Evleri" },
                    { 14, 26, "Ünlülerin heykelleri.", "/Pictures/balmumu.png", "Balmumu Müzesi" },
                    { 15, 26, "Gondol keyfi.", "/Pictures/porsuk.png", "Porsuk Çayı" },
                    { 16, 17, "Saygı duruşu.", "/Pictures/eskisehir.png", "Şehitler Abidesi" },
                    { 17, 17, "Denizcilik tarihi.", "/Pictures/denizmuzesi.png", "Deniz Müzesi" },
                    { 18, 17, "Antik felsefe kenti.", "/Pictures/assos.png", "Assos Antik Kenti" },
                    { 19, 17, "Efsanevi savaş kenti.", "/Pictures/truva.png", "Truva Antik Kenti" }
                });

            migrationBuilder.InsertData(
                table: "Feedbacks",
                columns: new[] { "FeedbackId", "Comment", "Date", "PlaceId", "Rating", "UserName" },
                values: new object[,]
                {
                    { 1, "İdare eder.", new DateTime(2025, 12, 11, 11, 26, 3, 827, DateTimeKind.Local).AddTicks(2691), 1, 3, "Ziyaretçi 1" },
                    { 2, "Çok fazla kalabalık vardı.", new DateTime(2025, 12, 11, 11, 26, 3, 827, DateTimeKind.Local).AddTicks(2694), 1, 2, "Ziyaretçi 2" },
                    { 3, "Etkileyici.", new DateTime(2025, 12, 11, 11, 26, 3, 827, DateTimeKind.Local).AddTicks(2696), 3, 4, "Ziyaretçi 3" },
                    { 4, "Görülmeli.", new DateTime(2025, 12, 11, 11, 26, 3, 827, DateTimeKind.Local).AddTicks(2697), 4, 4, "Ziyaretçi 4" },
                    { 5, "Hissiyatı çok yüksek.", new DateTime(2025, 12, 11, 11, 26, 3, 827, DateTimeKind.Local).AddTicks(2698), 5, 5, "Vatandaş" },
                    { 6, "Herkesin gitmesi gereken bir yer.", new DateTime(2025, 12, 11, 11, 26, 3, 827, DateTimeKind.Local).AddTicks(2699), 5, 5, "Ahmet" },
                    { 7, "Her şey çok gerçekçi.", new DateTime(2025, 12, 11, 11, 26, 3, 827, DateTimeKind.Local).AddTicks(2733), 6, 4, "Mehmet" },
                    { 8, "Güzel.", new DateTime(2025, 12, 11, 11, 26, 3, 827, DateTimeKind.Local).AddTicks(2734), 8, 4, "Ayşe" },
                    { 9, "Muazzam tarih.", new DateTime(2025, 12, 11, 11, 26, 3, 827, DateTimeKind.Local).AddTicks(2735), 10, 5, "Turist" },
                    { 10, "Çok fazla sıra bekleniyor.", new DateTime(2025, 12, 11, 11, 26, 3, 827, DateTimeKind.Local).AddTicks(2736), 11, 2, "Fatma" },
                    { 11, "Çocuklar için iyi.", new DateTime(2025, 12, 11, 11, 26, 3, 827, DateTimeKind.Local).AddTicks(2738), 12, 3, "Veli" },
                    { 12, "Çok güzel!", new DateTime(2025, 12, 11, 11, 26, 3, 827, DateTimeKind.Local).AddTicks(2739), 13, 5, "Gezgin" },
                    { 13, "Tarihi dokusu harika.", new DateTime(2025, 12, 11, 11, 26, 3, 827, DateTimeKind.Local).AddTicks(2740), 13, 4, "Tarihçi" },
                    { 14, "Çok fazla sıra bekledik.", new DateTime(2025, 12, 11, 11, 26, 3, 827, DateTimeKind.Local).AddTicks(2741), 14, 2, "Ziyaretçi" },
                    { 15, "Romantik bir yürüyüş için ideal.", new DateTime(2025, 12, 11, 11, 26, 3, 827, DateTimeKind.Local).AddTicks(2743), 15, 5, "Aşıklar" },
                    { 16, "Duygusal.", new DateTime(2025, 12, 11, 11, 26, 3, 827, DateTimeKind.Local).AddTicks(2744), 16, 5, "Mehmetçik" },
                    { 17, "Bakımlı.", new DateTime(2025, 12, 11, 11, 26, 3, 827, DateTimeKind.Local).AddTicks(2745), 16, 4, "Ziyaretçi" },
                    { 18, "Geliştirilmeli.", new DateTime(2025, 12, 11, 11, 26, 3, 827, DateTimeKind.Local).AddTicks(2746), 17, 2, "Eleştirmen" },
                    { 19, "Beklentinin altında bir yer.", new DateTime(2025, 12, 11, 11, 26, 3, 827, DateTimeKind.Local).AddTicks(2747), 17, 3, "Ziyaretçi" },
                    { 20, "Yol biraz zorlu.", new DateTime(2025, 12, 11, 11, 26, 3, 827, DateTimeKind.Local).AddTicks(2749), 18, 3, "Şoför" },
                    { 21, "Manzara güzel.", new DateTime(2025, 12, 11, 11, 26, 3, 827, DateTimeKind.Local).AddTicks(2750), 18, 4, "Fotoğrafçı" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_PlaceId",
                table: "Feedbacks",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Places_CityId",
                table: "Places",
                column: "CityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
