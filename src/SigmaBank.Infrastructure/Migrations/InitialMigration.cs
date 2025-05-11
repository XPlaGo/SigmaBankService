using FluentMigrator;

namespace SigmaBank.Infrastructure.Migrations;

[Migration(1)]
public class InitialMigration : Migration
{
    public override void Up()
    {
        const string sql = """
                           create table users
                           (
                               id bigint primary key generated always as identity,
                               phone_number text not null,
                               first_name text not null,
                               last_name text not null,
                               age int not null
                           );
                           """;

        Execute.Sql(sql);
    }

    public override void Down()
    {
        const string sql = """
                           drop table if exists users;
                           """;

        Execute.Sql(sql);
    }
}