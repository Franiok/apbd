﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tut11.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    IdDoctor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.IdDoctor);
                });

            migrationBuilder.CreateTable(
                name: "Medicament",
                columns: table => new
                {
                    IdMedicament = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicament", x => x.IdMedicament);
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    IdPatient = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.IdPatient);
                });

            migrationBuilder.CreateTable(
                name: "Prescription",
                columns: table => new
                {
                    IdPrescription = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdPatient = table.Column<int>(type: "int", nullable: false),
                    PatientIdPatient = table.Column<int>(type: "int", nullable: false),
                    IdDoctor = table.Column<int>(type: "int", nullable: false),
                    DoctorIdDoctor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescription", x => x.IdPrescription);
                    table.ForeignKey(
                        name: "FK_Prescription_Doctor_DoctorIdDoctor",
                        column: x => x.DoctorIdDoctor,
                        principalTable: "Doctor",
                        principalColumn: "IdDoctor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prescription_Patient_PatientIdPatient",
                        column: x => x.PatientIdPatient,
                        principalTable: "Patient",
                        principalColumn: "IdPatient",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrescriptionMedicaments",
                columns: table => new
                {
                    IdMedicament = table.Column<int>(type: "int", nullable: false),
                    IdPrescription = table.Column<int>(type: "int", nullable: false),
                    Dose = table.Column<int>(type: "int", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptionMedicaments", x => new { x.IdMedicament, x.IdPrescription });
                    table.ForeignKey(
                        name: "FK_PrescriptionMedicaments_Medicament_IdMedicament",
                        column: x => x.IdMedicament,
                        principalTable: "Medicament",
                        principalColumn: "IdMedicament",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrescriptionMedicaments_Prescription_IdPrescription",
                        column: x => x.IdPrescription,
                        principalTable: "Prescription",
                        principalColumn: "IdPrescription",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_DoctorIdDoctor",
                table: "Prescription",
                column: "DoctorIdDoctor");

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_PatientIdPatient",
                table: "Prescription",
                column: "PatientIdPatient");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionMedicaments_IdPrescription",
                table: "PrescriptionMedicaments",
                column: "IdPrescription");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrescriptionMedicaments");

            migrationBuilder.DropTable(
                name: "Medicament");

            migrationBuilder.DropTable(
                name: "Prescription");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "Patient");
        }
    }
}
