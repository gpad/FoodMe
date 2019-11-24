using FluentMigrator;

namespace FoodMe.Application.Migrations
{
    [Migration(20190611151801)]
    public class AddCartItemsTable : Migration
    {
        public override void Up()
        {
            Create.Table("cart_items")
                .WithColumn("id").AsGuid().PrimaryKey()
                .WithColumn("product_id").AsGuid()
                .WithColumn("description").AsString()
                .WithColumn("qty").AsCurrency();
        }

        public override void Down()
        {
            Delete.Table("carts");
        }
    }
}

