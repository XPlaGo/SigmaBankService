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
                               user_id bigint primary key generated always as identity,
                               phone_number text not null,
                               first_name text not null,
                               last_name text not null,
                               age int not null
                           );

                           create type CardType as enum ('Mastercard', 'Visa', 'MIR');

                           create table cards
                           (
                               card_id bigint primary key generated always as identity,
                               account_id bigint not null,
                               number text unique not null,
                               type CardType not null
                           );
                           
                           create table cards_private_data
                           (
                               card_private_data_id bigint primary key generated always as identity,
                               card_id bigint not null,
                               expiration_date timestamp with time zone not null,
                               code text not null
                           );

                           create table accounts
                           (
                               account_id bigint primary key generated always as identity,
                               user_id bigint not null,
                               amount decimal not null
                           );
                           """;

        Execute.Sql(sql);
    }

    public override void Down()
    {
        const string sql = """
                           drop table if exists users;
                           drop table if exists cards;
                           drop table if exists cards_private_data;
                           drop table if exists accounts;
                           """;

        Execute.Sql(sql);
    }
}