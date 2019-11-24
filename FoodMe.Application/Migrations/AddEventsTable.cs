using FluentMigrator;

namespace FoodMe.Application.Migrations
{
    [Migration(20190611151802)]
    public class AddEventsTable : Migration
    {
        public override void Up()
        {
            Create.Table("events")
                .WithColumn("id").AsGuid().PrimaryKey()
                .WithColumn("aggregate_id").AsGuid()
                .WithColumn("version").AsInt64()
                .WithColumn("event_type").AsString()
                .WithColumn("payload").AsString(1024);
        }

        public override void Down()
        {
            Delete.Table("carts");
        }
    }
}

