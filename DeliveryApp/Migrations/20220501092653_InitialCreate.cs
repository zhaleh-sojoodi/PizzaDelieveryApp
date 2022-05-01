using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliveryApp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    customer_email = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    customer_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    customer_phone_number = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    customer_address = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Customer__CE486A0B3B778F1C", x => x.customer_email);
                });

            migrationBuilder.CreateTable(
                name: "Pizza",
                columns: table => new
                {
                    pizza_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pizza_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    pizza_amount = table.Column<decimal>(type: "decimal(4,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pizza", x => x.pizza_id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    order_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customer_email = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    order_total = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    order_date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.order_id);
                    table.ForeignKey(
                        name: "FK__Orders__customer__25869641",
                        column: x => x.customer_email,
                        principalTable: "Customers",
                        principalColumn: "customer_email",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    order_item_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_id = table.Column<int>(type: "int", nullable: false),
                    pizza_id = table.Column<int>(type: "int", nullable: false),
                    item_count = table.Column<int>(type: "int", nullable: false),
                    Order_item_amount = table.Column<decimal>(type: "decimal(8,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.order_item_id);
                    table.ForeignKey(
                        name: "FK__OrderItem__order__2A4B4B5E",
                        column: x => x.order_id,
                        principalTable: "Orders",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__OrderItem__pizza__2B3F6F97",
                        column: x => x.pizza_id,
                        principalTable: "Pizza",
                        principalColumn: "pizza_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_order_id",
                table: "OrderItem",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_pizza_id",
                table: "OrderItem",
                column: "pizza_id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_customer_email",
                table: "Orders",
                column: "customer_email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Pizza");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
