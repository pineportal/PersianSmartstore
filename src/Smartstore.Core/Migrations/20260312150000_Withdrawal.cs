using FluentMigrator;
using Smartstore.Core.Catalog.Categories;
using Smartstore.Core.Catalog.Products;
using Smartstore.Core.Checkout.Orders;
using Smartstore.Core.Data;
using Smartstore.Core.Data.Migrations;
using Smartstore.Data.Migrations;
using RcEntity = Smartstore.Core.Checkout.Orders.ReturnCase;

namespace Smartstore.Core.Migrations;

[MigrationVersion("2026-03-12 15:00:00", "Core: Withdrawal")]
internal class Withdrawal : Migration, ILocaleResourcesProvider, IDataSeeder<SmartDbContext>
{
    const string ReturnCaseTable = nameof(Checkout.Orders.ReturnCase);
    const string WithdrawalIdColumn = nameof(RcEntity.WithdrawalId);

    public override void Up()
    {
        if (!Schema.Table(ReturnCaseTable).Column(WithdrawalIdColumn).Exists())
        {
            Create.Column(WithdrawalIdColumn).OnTable(ReturnCaseTable)
                .AsInt32()
                .Nullable()
                .Indexed();
        }

        if (!Schema.Table(nameof(Order)).Column(nameof(Order.CompletedOn)).Exists())
        {
            Create.Column(nameof(Order.CompletedOn)).OnTable(nameof(Order))
                .AsDateTime2()
                .Nullable();
        }

        if (!Schema.Table(nameof(Product)).Column(nameof(Product.WithdrawalPeriodDays)).Exists())
        {
            Create.Column(nameof(Product.WithdrawalPeriodDays)).OnTable(nameof(Product))
                .AsInt32()
                .Nullable();
        }

        if (!Schema.Table(nameof(Category)).Column(nameof(Category.WithdrawalPeriodDays)).Exists())
        {
            Create.Column(nameof(Category.WithdrawalPeriodDays)).OnTable(nameof(Category))
                .AsInt32()
                .Nullable();
        }
    }

    public override void Down()
    {
        // Columns
        if (Schema.Table(ReturnCaseTable).Column(WithdrawalIdColumn).Exists())
            Delete.Column(WithdrawalIdColumn).FromTable(ReturnCaseTable);

        if (Schema.Table(nameof(Order)).Column(nameof(Order.CompletedOn)).Exists())
            Delete.Column(nameof(Order.CompletedOn)).FromTable(nameof(Order));

        if (Schema.Table(nameof(Product)).Column(nameof(Product.WithdrawalPeriodDays)).Exists())
            Delete.Column(nameof(Product.WithdrawalPeriodDays)).FromTable(nameof(Product));

        if (Schema.Table(nameof(Category)).Column(nameof(Category.WithdrawalPeriodDays)).Exists())
            Delete.Column(nameof(Category.WithdrawalPeriodDays)).FromTable(nameof(Category));
    }

    public DataSeederStage Stage => DataSeederStage.Early;
    public bool AbortOnFailure => false;

    public async Task SeedAsync(SmartDbContext context, CancellationToken cancelToken = default)
    {
        await context.MigrateLocaleResourcesAsync(MigrateLocaleResources);

        // Fix for cases where "RequestedActionUpdatedOnUtc" is set, but "RequestedAction" is empty.
        await context.ReturnCases
            .Where(x => x.RequestedActionUpdatedOnUtc != null && string.IsNullOrEmpty(x.RequestedAction))
            .ExecuteUpdateAsync(x => x.SetProperty(rc => rc.RequestedActionUpdatedOnUtc, rc => null), cancelToken);
    }

    public void MigrateLocaleResources(LocaleResourcesBuilder builder)
    {
        builder.AddOrUpdate("Common.Type", "Type", "Typ", "نوع");

        builder.Delete(
            "Admin.ReturnRequests.Fields.CreatedOn.Hint",
            "Admin.ReturnRequests.Fields.Status.Hint",
            "Account.CustomerReturnRequests.Title",
            "ReturnRequests.Products.RequestAlreadyExists",
            "Admin.ReturnRequests.Accept.Caption",
            "Account.CustomerOrders.ReturnItems");

        builder.AddOrUpdate("Admin.Catalog.Products.Fields.WithdrawalPeriodDays",
            "Withdrawal period",
            "Widerrufsfrist",
            "مهلت انصراف",
            "Specifies the number of days within which the product can be withdrawn. A value of 0 means that the product is not eligible for withdrawal (e.g., hygiene products).",
            "Legt die Frist in Tagen fest, bis zu der das Produkt widerrufen werden kann. Der Wert 0 bedeutet, dass das Produkt nicht widerrufbar ist (z.B. Hygieneartikel).",
            "تعداد روزهایی را تعیین می‌کند که طی آن می‌توان از خرید محصول انصراف داد. مقدار 0 به این معنی است که محصول قابل انصراف نیست (مانند محصولات بهداشتی).");

        builder.AddOrUpdate("Admin.Catalog.Categories.Fields.WithdrawalPeriodDays",
            "Withdrawal period",
            "Widerrufsfrist",
            "مهلت انصراف",
            "Specifies the number of days within which products in this category can be withdrawn. A value of 0 means that the product is not eligible for withdrawal (e.g., hygiene products)."
            + " If a product is assigned to multiple categories, the withdrawal period of each category must be met in order for the product to be eligible for withdrawal.",
            "Legt die Frist in Tagen fest, innerhalb derer Produkte dieser Warengruppe widerrufen werden können. Der Wert 0 bedeutet, dass die Produkte nicht widerrufbar sind"
            + " (z.B. Hygieneartikel). Wenn ein Produkt mehreren Warengruppen zugeordnet ist, müssen die Widerrufsfristen aller Warengruppen eingehalten sein, damit der Artikel",
            "تعداد روزهایی را تعیین می‌کند که طی آن می‌توان از خرید محصولات این دسته انصراف داد. مقدار 0 به این معنی است که محصول قابل انصراف نیست (مانند محصولات بهداشتی)."
            + " اگر محصولی به چند دسته اختصاص داده شده باشد، مهلت انصراف تمامی دسته‌ها باید رعایت شود تا محصول قابل انصراف باشد.");

        builder.AddOrUpdate("Enums.ReturnCaseKind.Return", "Return", "Retoure", "مرجوعی");
        builder.AddOrUpdate("Enums.ReturnCaseKind.Withdrawal", "Withdrawal", "Widerruf", "انصراف");

        builder.AddOrUpdate("ReturnCase.Case", "Case {0}", "Fall {0}", "مورد {0}");
        builder.AddOrUpdate("ReturnCase.CaseNo", "Case no.", "Fall Nr.", "شماره مورد");
        builder.AddOrUpdate("ReturnCase.NextStep", "Next step", "Nächster Schritt", "مرحله بعدی");
        builder.AddOrUpdate("ReturnCase.WithdrawalQuantity", "Withdrawal quantity", "Widerrufsmenge", "تعداد انصرافی");
        builder.AddOrUpdate("ReturnCase.ReceivedWithdrawal", "Received withdrawal", "Widerruf eingegangen", "انصراف دریافت شد");
        builder.AddOrUpdate("ReturnCase.Open", "Open", "Offen", "باز");
        builder.AddOrUpdate("ReturnCase.Complete", "Completed", "Abgeschlossen", "تکمیل شده");

        builder.AddOrUpdate("ReturnCase.NextStep.Pending", "Please return the items.", "Bitte senden Sie die Artikel zurück.", "لطفاً اقلام را برگردانید.");
        builder.AddOrUpdate("ReturnCase.NextStep.Received", "We are processing your return.", "Wir bearbeiten Ihre Rücksendung.", "در حال پردازش مرجوعی شما هستیم.");
        builder.AddOrUpdate("ReturnCase.NextStep.ReturnAuthorized", "We will continue processing your request.", "Wir veranlassen die weitere Bearbeitung.", "پردازش درخواست شما را ادامه خواهیم داد.");
        builder.AddOrUpdate("ReturnCase.NextStep.ItemsRepaired", "We will send the repaired items back to you.", "Wir senden Ihnen die reparierten Artikel zurück.", "اقلام تعمیر شده را برای شما پس خواهیم فرستاد.");
        builder.AddOrUpdate("ReturnCase.NextStep.ItemsRefunded", "The refund has been processed.", "Die Erstattung wurde veranlasst.", "بازپرداخت انجام شد.");
        builder.AddOrUpdate("ReturnCase.NextStep.RequestRejected", "No further action is required.", "Es ist keine weitere Aktion erforderlich.", "اقدام دیگری نیاز نیست.");
        builder.AddOrUpdate("ReturnCase.NextStep.Cancelled", "No further action is required.", "Es ist keine weitere Aktion erforderlich.", "اقدام دیگری نیاز نیست.");

        builder.AddOrUpdate("ReturnCase.OrderWithdrawn",
            "The order has been withdrawn.",
            "Die Bestellung wurde widerrufen.",
            "از سفارش انصراف داده شده است.");

        builder.AddOrUpdate("ReturnCase.WithdrawalItemExists",
            "A withdrawal request has been submitted for this item.",
            "Für diesen Artikel wurde ein Widerruf eingereicht.",
            "درخواست انصراف برای این قلم کالا ثبت شده است.");

        builder.AddOrUpdate("ReturnCase.ReturnItemExists",
            "There are returns for this item.",
            "Für diesen Artikel liegen Retouren vor.",
            "برای این قلم کالا مرجوعی وجود دارد.");

        builder.AddOrUpdate("ReturnCase.StartProcessing",
            "Start processing",
            "Verarbeitung starten",
            "شروع پردازش",
            "Converts the withdrawal into a return for further processing.",
            "Wandelt den Widerruf in eine Retoure zur weiteren Bearbeitung um.",
            "انصراف را برای پردازش بیشتر به مرجوعی تبدیل می‌کند.");

        builder.AddOrUpdate("ReturnCase.ConvertedWithdrawal",
            "The withdrawal has been converted to a return. Status: <b>{0}</b>.",
            "Der Widerruf wurde in eine Retoure umgewandelt. Status: <b>{0}</b>.",
            "انصراف به مرجوعی تبدیل شده است. وضعیت: <b>{0}</b>.");

        // Renaming, typos, fixes.
        builder.AddOrUpdate("Products.Details", "Product details", "Produktdetails", "جزئیات محصول");
        builder.AddOrUpdate("Account.CustomerReturnRequests", "Withdrawals and Returns", "Widerrufe und Retouren", "انصراف‌ها و مرجوعی‌ها");
        builder.AddOrUpdate("Admin.ReturnRequests", "Withdrawals and Returns", "Widerrufe und Retouren", "انصراف‌ها و مرجوعی‌ها");

        builder.AddOrUpdate("Enums.ReturnRequestStatus.RequestRejected")
            .Value("de", "Antrag abgewiesen")
            .Value("fa", "درخواست رد شد");

        builder.AddOrUpdate("Enums.ReturnRequestStatus.Received", "Received items", "Ware erhalten", "اقلام دریافت شد");

        builder.AddOrUpdate("PageTitle.OrderDetails")
            .Value("de", "Bestelldetails")
            .Value("fa", "جزئیات سفارش");

        builder.AddOrUpdate("Admin.ReturnRequests.EditReturnRequestDetails", "Edit return request", "Retoure bearbeiten", "ویرایش درخواست مرجوعی");
        builder.AddOrUpdate("Admin.Withdrawal.EditWithdrawal", "Edit withdrawal", "Widerruf bearbeiten", "ویرایش انصراف");

        builder.AddOrUpdate("Account.CustomerReturnRequests.Date", "Requested on", "Angefragt am", "تاریخ درخواست");

        builder.AddOrUpdate("Admin.ReturnRequests.Fields.Quantity.Hint",
            "Number of items to be returned",
            "Anzahl der zurückzusendenden Artikel",
            "تعداد اقلامی که باید مرجوع شوند");

        builder.AddOrUpdate("Admin.ReturnRequests.MaxRefundAmount.Hint",
            "The maximum amount that can be refunded for this item.",
            "Der maximale Betrag, der für diesen Retourenartikel erstattet werden kann.",
            "حداکثر مبلغی که می‌تواند برای این قلم کالا بازپرداخت شود.");

        builder.AddOrUpdate("Admin.ReturnRequests.Accept.Caption", "Accept the return", "Retoure genehmigen", "تأیید مرجوعی");

    }
}