using Microsoft.EntityFrameworkCore.Migrations;

namespace GPS.Repository.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cnpj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    abertura = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fantasia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    natureza_juridica = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    logradouro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    numero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    complemento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    bairro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    municipio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    uf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    telefone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    efr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    situacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    data_situacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    motivo_situacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    situacao_especial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    data_situacao_especial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    capital_social = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "atividade_principais",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaId = table.Column<int>(type: "int", nullable: true),
                    code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    text = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_atividade_principais", x => x.id);
                    table.ForeignKey(
                        name: "FK_atividade_principais_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "atividades_secundarias",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaId = table.Column<int>(type: "int", nullable: true),
                    code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    text = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_atividades_secundarias", x => x.id);
                    table.ForeignKey(
                        name: "FK_atividades_secundarias_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "billing",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaId = table.Column<int>(type: "int", nullable: true),
                    free = table.Column<bool>(type: "bit", nullable: false),
                    database = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_billing", x => x.id);
                    table.ForeignKey(
                        name: "FK_billing_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "qsas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaId = table.Column<int>(type: "int", nullable: true),
                    nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    qual = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pais_origem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nome_rep_legal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    qual_rep_legal = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qsas", x => x.id);
                    table.ForeignKey(
                        name: "FK_qsas_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_atividade_principais_EmpresaId",
                table: "atividade_principais",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_atividades_secundarias_EmpresaId",
                table: "atividades_secundarias",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_billing_EmpresaId",
                table: "billing",
                column: "EmpresaId",
                unique: true,
                filter: "[EmpresaId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_qsas_EmpresaId",
                table: "qsas",
                column: "EmpresaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "atividade_principais");

            migrationBuilder.DropTable(
                name: "atividades_secundarias");

            migrationBuilder.DropTable(
                name: "billing");

            migrationBuilder.DropTable(
                name: "qsas");

            migrationBuilder.DropTable(
                name: "Empresas");
        }
    }
}
