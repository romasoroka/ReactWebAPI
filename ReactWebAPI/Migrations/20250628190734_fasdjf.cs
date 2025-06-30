using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReactWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class fasdjf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkSessions_Employees_EmployeeId",
                table: "WorkSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkSessions_Projects_ProjectId",
                table: "WorkSessions");

            migrationBuilder.DropTable(
                name: "EmployeeStats");

            migrationBuilder.DropTable(
                name: "ProjectAnalytics");

            migrationBuilder.DropTable(
                name: "ProjectCredentials");

            migrationBuilder.DropTable(
                name: "ProjectEmployees");

            migrationBuilder.DropTable(
                name: "ProjectTechnologies");

            migrationBuilder.AddColumn<int>(
                name: "ActiveEmployees",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReportCount",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalHoursLogged",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProjectsInvolved",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReportsSubmitted",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalHoursWorked",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CredentialProject",
                columns: table => new
                {
                    CredentialsId = table.Column<int>(type: "int", nullable: false),
                    ProjectsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CredentialProject", x => new { x.CredentialsId, x.ProjectsId });
                    table.ForeignKey(
                        name: "FK_CredentialProject_Credentials_CredentialsId",
                        column: x => x.CredentialsId,
                        principalTable: "Credentials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CredentialProject_Projects_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeProject",
                columns: table => new
                {
                    EmployeesId = table.Column<int>(type: "int", nullable: false),
                    ProjectsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeProject", x => new { x.EmployeesId, x.ProjectsId });
                    table.ForeignKey(
                        name: "FK_EmployeeProject_Employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeProject_Projects_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTechnology",
                columns: table => new
                {
                    ProjectsId = table.Column<int>(type: "int", nullable: false),
                    TechnologiesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTechnology", x => new { x.ProjectsId, x.TechnologiesId });
                    table.ForeignKey(
                        name: "FK_ProjectTechnology_Projects_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectTechnology_Technologies_TechnologiesId",
                        column: x => x.TechnologiesId,
                        principalTable: "Technologies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CredentialProject_ProjectsId",
                table: "CredentialProject",
                column: "ProjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProject_ProjectsId",
                table: "EmployeeProject",
                column: "ProjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTechnology_TechnologiesId",
                table: "ProjectTechnology",
                column: "TechnologiesId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSessions_Employees_EmployeeId",
                table: "WorkSessions",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSessions_Projects_ProjectId",
                table: "WorkSessions",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkSessions_Employees_EmployeeId",
                table: "WorkSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkSessions_Projects_ProjectId",
                table: "WorkSessions");

            migrationBuilder.DropTable(
                name: "CredentialProject");

            migrationBuilder.DropTable(
                name: "EmployeeProject");

            migrationBuilder.DropTable(
                name: "ProjectTechnology");

            migrationBuilder.DropColumn(
                name: "ActiveEmployees",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ReportCount",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "TotalHoursLogged",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectsInvolved",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ReportsSubmitted",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "TotalHoursWorked",
                table: "Employees");

            migrationBuilder.CreateTable(
                name: "EmployeeStats",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ProjectsInvolved = table.Column<int>(type: "int", nullable: false),
                    ReportsSubmitted = table.Column<int>(type: "int", nullable: false),
                    TotalHoursWorked = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeStats", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_EmployeeStats_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectAnalytics",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    ActiveEmployees = table.Column<int>(type: "int", nullable: false),
                    ReportCount = table.Column<int>(type: "int", nullable: false),
                    TotalHoursLogged = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectAnalytics", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_ProjectAnalytics_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectCredentials",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    CredentialId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectCredentials", x => new { x.ProjectId, x.CredentialId });
                    table.ForeignKey(
                        name: "FK_ProjectCredentials_Credentials_CredentialId",
                        column: x => x.CredentialId,
                        principalTable: "Credentials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectCredentials_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectEmployees",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectEmployees", x => new { x.ProjectId, x.EmployeeId });
                    table.ForeignKey(
                        name: "FK_ProjectEmployees_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectEmployees_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTechnologies",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    TechnologyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTechnologies", x => new { x.ProjectId, x.TechnologyId });
                    table.ForeignKey(
                        name: "FK_ProjectTechnologies_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectTechnologies_Technologies_TechnologyId",
                        column: x => x.TechnologyId,
                        principalTable: "Technologies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCredentials_CredentialId",
                table: "ProjectCredentials",
                column: "CredentialId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEmployees_EmployeeId",
                table: "ProjectEmployees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTechnologies_TechnologyId",
                table: "ProjectTechnologies",
                column: "TechnologyId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSessions_Employees_EmployeeId",
                table: "WorkSessions",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSessions_Projects_ProjectId",
                table: "WorkSessions",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
