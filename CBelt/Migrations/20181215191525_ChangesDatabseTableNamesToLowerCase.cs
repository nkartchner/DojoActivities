using Microsoft.EntityFrameworkCore.Migrations;

namespace CBelt.Migrations
{
    public partial class ChangesDatabseTableNamesToLowerCase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_events_Users_UserId",
                table: "events");

            migrationBuilder.DropForeignKey(
                name: "FK_Participants_events_EventId",
                table: "Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Users_UserId",
                table: "Participants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Participants",
                table: "Participants");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "Participants",
                newName: "participants");

            migrationBuilder.RenameIndex(
                name: "IX_Participants_UserId",
                table: "participants",
                newName: "IX_participants_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Participants_EventId",
                table: "participants",
                newName: "IX_participants_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_participants",
                table: "participants",
                column: "ParticipantId");

            migrationBuilder.AddForeignKey(
                name: "FK_events_users_UserId",
                table: "events",
                column: "UserId",
                principalTable: "users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_participants_events_EventId",
                table: "participants",
                column: "EventId",
                principalTable: "events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_participants_users_UserId",
                table: "participants",
                column: "UserId",
                principalTable: "users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_events_users_UserId",
                table: "events");

            migrationBuilder.DropForeignKey(
                name: "FK_participants_events_EventId",
                table: "participants");

            migrationBuilder.DropForeignKey(
                name: "FK_participants_users_UserId",
                table: "participants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_participants",
                table: "participants");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "participants",
                newName: "Participants");

            migrationBuilder.RenameIndex(
                name: "IX_participants_UserId",
                table: "Participants",
                newName: "IX_Participants_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_participants_EventId",
                table: "Participants",
                newName: "IX_Participants_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Participants",
                table: "Participants",
                column: "ParticipantId");

            migrationBuilder.AddForeignKey(
                name: "FK_events_Users_UserId",
                table: "events",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_events_EventId",
                table: "Participants",
                column: "EventId",
                principalTable: "events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Users_UserId",
                table: "Participants",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
