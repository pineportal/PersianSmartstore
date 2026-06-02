using FluentMigrator;
using Smartstore.Core.Catalog.Products;
using Smartstore.Core.Data;
using Smartstore.Core.Data.Migrations;
using Smartstore.Data.Migrations;

namespace Smartstore.Core.Migrations;

[MigrationVersion("2026-04-29 10:00:00", "Core: Quantity per parent unit of required products")]
internal class QuantityPerParentUnit : Migration, ILocaleResourcesProvider, IDataSeeder<SmartDbContext>
{
    const string ProductTable = nameof(Product);
    const string QuantityPerParentUnitColumn = nameof(Product.QuantityPerParentUnit);

    public override void Up()
    {
        if (!Schema.Table(ProductTable).Column(QuantityPerParentUnitColumn).Exists())
        {
            Create.Column(QuantityPerParentUnitColumn).OnTable(ProductTable)
                .AsInt32()
                .NotNullable()
                .WithDefaultValue(0);
        }
    }

    public override void Down()
    {
        if (Schema.Table(ProductTable).Column(QuantityPerParentUnitColumn).Exists())
        {
            Delete.Column(QuantityPerParentUnitColumn).FromTable(ProductTable);
        }
    }

    public DataSeederStage Stage => DataSeederStage.Early;
    public bool AbortOnFailure => false;

    public async Task SeedAsync(SmartDbContext context, CancellationToken cancelToken = default)
    {
        await context.MigrateLocaleResourcesAsync(MigrateLocaleResources);
    }

    public void MigrateLocaleResources(LocaleResourcesBuilder builder)
    {
        builder.AddOrUpdate("Admin.Catalog.Products.Fields.QuantityPerParentUnit",
      "Quantity per main product",
      "Menge pro Hauptprodukt",
      "تعداد به ازای محصول اصلی",
      "Specifies how many units of this required product are automatically kept in the cart for each unit of the main product."
      + " If 0 (default), the quantity will not be adjusted automatically, and the customer will be able to change it themselves. If greater than 0,"
      + " the quantity is automatically calculated and cannot be changed manually in the shopping cart. Example: If a shelf requires four screw sets, enter 4.",
      "Legt fest, wie viele Einheiten dieses erforderlichen Produkts pro Einheit des Hauptprodukts automatisch im Warenkorb gehalten werden."
      + " Bei 0 (Standard) wird die Menge nicht automatisch angepasst und der Kunde kann sie selbst ändern. Bei einem Wert größer 0 wird die Menge"
      + " automatisch berechnet und kann im Warenkorb nicht manuell geändert werden. Beispiel: Wenn ein Regal vier Schraubensets benötigt, tragen Sie hier den Wert 4 ein.",
      "مشخص می‌کند که به ازای هر واحد از محصول اصلی، چه تعداد از این محصولِ پیش‌نیاز به‌طور خودکار در سبد خرید نگه داشته شود."
      + " اگر ۰ باشد (پیش‌فرض)، تعداد به‌طور خودکار تنظیم نمی‌شود و مشتری می‌تواند خودش آن را تغییر دهد. اگر بزرگتر از ۰ باشد، تعداد"
      + " به‌طور خودکار محاسبه شده و نمی‌توان آن را به‌صورت دستی در سبد خرید تغییر داد. مثال: اگر یک قفسه به چهار مجموعه پیچ نیاز دارد، عدد ۴ را وارد کنید.");

        builder.AddOrUpdate("ShoppingCart.SyncedRequiredProductQuantityWarning",
            "The quantity {0} is not allowed. The required quantity is {1}.",
            "Die Menge {0} ist nicht zulässig. Erforderlich ist die Menge {1}.",
            "تعداد {0} مجاز نیست. تعداد مورد نیاز {1} است.");

        builder.AddOrUpdate("ShoppingCart.SyncedRequiredProductQuantityInfo",
            "The quantity is automatically calculated based on the main product.",
            "Die Menge wird automatisch anhand des Hauptprodukts ermittelt.",
            "تعداد به‌طور خودکار بر اساس محصول اصلی محاسبه می‌شود.");

    }
}