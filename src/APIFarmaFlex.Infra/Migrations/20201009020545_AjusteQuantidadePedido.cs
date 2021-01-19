using Microsoft.EntityFrameworkCore.Migrations;

namespace APIFarmaFlex.Infra.Migrations
{
    public partial class AjusteQuantidadePedido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "QuantidadeProduto",
                table: "ItensPedidos",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "QuantidadeProduto",
                table: "ItensPedidos",
                type: "float",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
