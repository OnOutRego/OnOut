using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnOut.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DietaryChoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietaryChoices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kennels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MeetingTimes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    FoundingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChildFriendly = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PrimaryImage = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    BannerImage = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kennels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hashers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HashName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Auth0Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomeCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomeState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomeCountry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DietaryChoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MotherKennelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    HomeKennelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProfileCompleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PrimaryImage = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    BannerImage = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hashers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hashers_DietaryChoices_DietaryChoiceId",
                        column: x => x.DietaryChoiceId,
                        principalTable: "DietaryChoices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hashers_Kennels_HomeKennelId",
                        column: x => x.HomeKennelId,
                        principalTable: "Kennels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hashers_Kennels_MotherKennelId",
                        column: x => x.MotherKennelId,
                        principalTable: "Kennels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "KennelRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KennelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KennelRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KennelRoles_Kennels_KennelId",
                        column: x => x.KennelId,
                        principalTable: "Kennels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsSubEvent = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrailTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShiggyLevel = table.Column<double>(type: "float", nullable: false),
                    HasKennel = table.Column<bool>(type: "bit", nullable: false),
                    KennelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MaxRegos = table.Column<int>(type: "int", nullable: false),
                    Gimmes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasGimmes = table.Column<bool>(type: "bit", nullable: false),
                    HasEarlyRego = table.Column<bool>(type: "bit", nullable: false),
                    EarlyRegoDeadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EarlyRegoPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RegoPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RegoDeadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PrimaryImage = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    BannerImage = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Addresses_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Events_EventTypes_EventTypeId",
                        column: x => x.EventTypeId,
                        principalTable: "EventTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Events_Hashers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Hashers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Events_Kennels_KennelId",
                        column: x => x.KennelId,
                        principalTable: "Kennels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Mismanagement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HasherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KennelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mismanagement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mismanagement_Hashers_HasherId",
                        column: x => x.HasherId,
                        principalTable: "Hashers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mismanagement_Kennels_KennelId",
                        column: x => x.KennelId,
                        principalTable: "Kennels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KennelMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HasherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KennelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KennelMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KennelMembers_Hashers_HasherId",
                        column: x => x.HasherId,
                        principalTable: "Hashers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KennelMembers_KennelRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "KennelRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KennelMembers_Kennels_KennelId",
                        column: x => x.KennelId,
                        principalTable: "Kennels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Regoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HasherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    IsTransferable = table.Column<bool>(type: "bit", nullable: false),
                    IsRefundable = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Regoes_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Regoes_Hashers_HasherId",
                        column: x => x.HasherId,
                        principalTable: "Hashers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DietaryChoices",
                columns: new[] { "Id", "CreatedDate", "Description", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), null, "No dietary restrictions.", null, "None" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), null, "Excludes meat, but may include dairy and eggs.", null, "Vegetarian" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), null, "Excludes all animal products, including dairy, eggs, and honey.", null, "Vegan" },
                    { new Guid("00000000-0000-0000-0000-000000000004"), null, "Excludes meat but includes fish and seafood.", null, "Pescatarian" },
                    { new Guid("00000000-0000-0000-0000-000000000005"), null, "Excludes foods containing gluten (e.g., wheat, barley, rye).", null, "Gluten-Free" },
                    { new Guid("00000000-0000-0000-0000-000000000006"), null, "Low-carb, high-fat diet.", null, "Keto" },
                    { new Guid("00000000-0000-0000-0000-000000000007"), null, "Focuses on whole foods, excludes processed foods and grains.", null, "Paleo" },
                    { new Guid("00000000-0000-0000-0000-000000000008"), null, "Complies with Islamic dietary laws.", null, "Halal" },
                    { new Guid("00000000-0000-0000-0000-000000000009"), null, "Complies with Jewish dietary laws.", null, "Kosher" },
                    { new Guid("00000000-0000-0000-0000-00000000000a"), null, "Vegetarian diet including dairy but excluding eggs.", null, "Lacto-Vegetarian" },
                    { new Guid("00000000-0000-0000-0000-00000000000b"), null, "Vegetarian diet including eggs but excluding dairy.", null, "Ovo-Vegetarian" },
                    { new Guid("00000000-0000-0000-0000-00000000000c"), null, "Vegetarian diet including both dairy and eggs.", null, "Lacto-Ovo Vegetarian" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_CreatorId",
                table: "Events",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventTypeId",
                table: "Events",
                column: "EventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_KennelId",
                table: "Events",
                column: "KennelId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_LocationId",
                table: "Events",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Hashers_DietaryChoiceId",
                table: "Hashers",
                column: "DietaryChoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Hashers_HomeKennelId",
                table: "Hashers",
                column: "HomeKennelId");

            migrationBuilder.CreateIndex(
                name: "IX_Hashers_MotherKennelId",
                table: "Hashers",
                column: "MotherKennelId");

            migrationBuilder.CreateIndex(
                name: "IX_KennelMembers_HasherId",
                table: "KennelMembers",
                column: "HasherId");

            migrationBuilder.CreateIndex(
                name: "IX_KennelMembers_KennelId",
                table: "KennelMembers",
                column: "KennelId");

            migrationBuilder.CreateIndex(
                name: "IX_KennelMembers_RoleId",
                table: "KennelMembers",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_KennelRoles_KennelId",
                table: "KennelRoles",
                column: "KennelId");

            migrationBuilder.CreateIndex(
                name: "IX_Mismanagement_HasherId",
                table: "Mismanagement",
                column: "HasherId");

            migrationBuilder.CreateIndex(
                name: "IX_Mismanagement_KennelId",
                table: "Mismanagement",
                column: "KennelId");

            migrationBuilder.CreateIndex(
                name: "IX_Regoes_EventId",
                table: "Regoes",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Regoes_HasherId",
                table: "Regoes",
                column: "HasherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KennelMembers");

            migrationBuilder.DropTable(
                name: "Mismanagement");

            migrationBuilder.DropTable(
                name: "Regoes");

            migrationBuilder.DropTable(
                name: "KennelRoles");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "EventTypes");

            migrationBuilder.DropTable(
                name: "Hashers");

            migrationBuilder.DropTable(
                name: "DietaryChoices");

            migrationBuilder.DropTable(
                name: "Kennels");
        }
    }
}
