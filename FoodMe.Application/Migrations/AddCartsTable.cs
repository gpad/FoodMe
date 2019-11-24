using FluentMigrator;

namespace FoodMe.Application.Migrations
{
    [Migration(20190611151800)]
    public class AddCartsTable : Migration
    {
        public override void Up()
        {
            Create.Table("carts")
                .WithColumn("id").AsGuid().PrimaryKey();
        }

        public override void Down()
        {
            Delete.Table("carts");
        }
    }
}

