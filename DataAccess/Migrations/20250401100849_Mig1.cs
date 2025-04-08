using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Roles__8AFACE3A23BED8A5", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__1788CCACE5825CE3", x => x.UserID);
                    table.ForeignKey(
                        name: "FK__Users__RoleID__3D5E1FD2",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID");
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    Message = table.Column<string>(type: "text", nullable: true),
                    IsRead = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Notifica__20CF2E32C47DD7A4", x => x.NotificationID);
                    table.ForeignKey(
                        name: "FK__Notificat__UserI__5812160E",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "ProviderServices",
                columns: table => new
                {
                    ServiceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderID = table.Column<int>(type: "int", nullable: true),
                    ServiceName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Provider__C51BB0EA937992DF", x => x.ServiceID);
                    table.ForeignKey(
                        name: "FK__ProviderS__Provi__74AE54BC",
                        column: x => x.ProviderID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    SubscriptionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Subscrip__9A2B24BD0E2D18BF", x => x.SubscriptionID);
                    table.ForeignKey(
                        name: "FK__Subscript__UserI__6D0D32F4",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Tours",
                columns: table => new
                {
                    TourID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ProviderID = table.Column<int>(type: "int", nullable: true),
                    GuideID = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tours__604CEA10A2F3859E", x => x.TourID);
                    table.ForeignKey(
                        name: "FK__Tours__GuideID__4222D4EF",
                        column: x => x.GuideID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK__Tours__ProviderI__412EB0B6",
                        column: x => x.ProviderID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Complaints",
                columns: table => new
                {
                    ComplaintID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    TourID = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, defaultValueSql: "('Open')"),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Complain__740D89AFF6AEADED", x => x.ComplaintID);
                    table.ForeignKey(
                        name: "FK__Complaint__TourI__534D60F1",
                        column: x => x.TourID,
                        principalTable: "Tours",
                        principalColumn: "TourID");
                    table.ForeignKey(
                        name: "FK__Complaint__UserI__52593CB8",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    TourID = table.Column<int>(type: "int", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, defaultValueSql: "('Pending')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Orders__C3905BAF24D78EA3", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK__Orders__TourID__47DBAE45",
                        column: x => x.TourID,
                        principalTable: "Tours",
                        principalColumn: "TourID");
                    table.ForeignKey(
                        name: "FK__Orders__UserID__46E78A0C",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    TourID = table.Column<int>(type: "int", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Reviews__74BC79AECA20445D", x => x.ReviewID);
                    table.ForeignKey(
                        name: "FK__Reviews__TourID__4D94879B",
                        column: x => x.TourID,
                        principalTable: "Tours",
                        principalColumn: "TourID");
                    table.ForeignKey(
                        name: "FK__Reviews__UserID__4CA06362",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Statistic",
                columns: table => new
                {
                    StatisticID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourID = table.Column<int>(type: "int", nullable: true),
                    TotalBookings = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    TotalRevenue = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Statisti__367DEB37FE5B723A", x => x.StatisticID);
                    table.ForeignKey(
                        name: "FK__Statistic__TourI__5CD6CB2B",
                        column: x => x.TourID,
                        principalTable: "Tours",
                        principalColumn: "TourID");
                });

            migrationBuilder.CreateTable(
                name: "TourLoadStatistics",
                columns: table => new
                {
                    StatisticID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourID = table.Column<int>(type: "int", nullable: true),
                    AvailableSeats = table.Column<int>(type: "int", nullable: false),
                    BookedSeats = table.Column<int>(type: "int", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TourLoad__367DEB37D8C7331B", x => x.StatisticID);
                    table.ForeignKey(
                        name: "FK__TourLoadS__TourI__70DDC3D8",
                        column: x => x.TourID,
                        principalTable: "Tours",
                        principalColumn: "TourID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_TourID",
                table: "Complaints",
                column: "TourID");

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_UserID",
                table: "Complaints",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserID",
                table: "Notifications",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TourID",
                table: "Orders",
                column: "TourID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserID",
                table: "Orders",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderServices_ProviderID",
                table: "ProviderServices",
                column: "ProviderID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_TourID",
                table: "Reviews",
                column: "TourID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserID",
                table: "Reviews",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "UQ__Roles__8A2B61607B249946",
                table: "Roles",
                column: "RoleName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Statistic_TourID",
                table: "Statistic",
                column: "TourID");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_UserID",
                table: "Subscriptions",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_TourLoadStatistics_TourID",
                table: "TourLoadStatistics",
                column: "TourID");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_GuideID",
                table: "Tours",
                column: "GuideID");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_ProviderID",
                table: "Tours",
                column: "ProviderID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleID",
                table: "Users",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__536C85E45B6333EB",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Users__A9D10534C5DA5B05",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Complaints");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ProviderServices");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Statistic");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "TourLoadStatistics");

            migrationBuilder.DropTable(
                name: "Tours");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
