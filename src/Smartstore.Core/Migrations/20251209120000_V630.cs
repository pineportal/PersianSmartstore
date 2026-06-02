using FluentMigrator;
using Smartstore.Core.Common.Configuration;
using Smartstore.Core.Configuration;
using Smartstore.Core.Security;
using Smartstore.Data.Migrations;

namespace Smartstore.Core.Data.Migrations
{
    [MigrationVersion("2025-12-09 12:00:00", "V630")]
    internal class V630 : Migration, ILocaleResourcesProvider, IDataSeeder<SmartDbContext>
    {
        public override void Up()
        {
        }

        public override void Down()
        {
        }

        public DataSeederStage Stage => DataSeederStage.Early;
        public bool AbortOnFailure => false;

        public async Task SeedAsync(SmartDbContext context, CancellationToken cancelToken = default)
        {
            await context.MigrateLocaleResourcesAsync(MigrateLocaleResources);
        }

        public void MigrateLocaleResources(LocaleResourcesBuilder builder)
        {
            builder.AddOrUpdate("Aria.Label.AlphabeticallySortedLinks", "Alphabetically sorted links", "Alphabetisch sortierte Links", "پیوندهای مرتب‌شده بر اساس الفبا");
            builder.AddOrUpdate("Aria.Label.BundleContains", "The product set contains {0}", "Das Produktset enthält {0}", "مجموعه محصول شامل {0} است");
            builder.AddOrUpdate("Aria.Label.ActiveFilters", "Active filters", "Aktive Filter", "فیلترهای فعال");
            builder.AddOrUpdate("Aria.Label.ProductVariants", "Product variants", "Produktvarianten", "انواع محصول");
            // INFO: We use punctuation (commas and periods), so that SR pauses for a moment when reading aloud. Hyphens and colons are not reliable.
            builder.AddOrUpdate("Aria.Label.Choice", "{0}, {1}", "{0}, {1}", "{0}، {1}");

            builder.AddOrUpdate("Aria.Label.CartItemSummary",
                "{0} at {1}.",
                "{0} zu {1}.",
                "{0} با قیمت {1}.");
            builder.AddOrUpdate("Aria.Label.CartItemSummaryWithTotal",
                "{0} at {1} each, quantity {2}, total {3}.",
                "{0} zu je {1}, Menge {2}, Gesamt {3}.",
                "{0} هر کدام با قیمت {1}، تعداد {2}، مجموع {3}.");
            builder.AddOrUpdate("Aria.Label.CartItemSummaryWithAttributes",
                "{0} at {1}, quantity {2}. {3}",
                "{0} zu {1}, Menge {2}. {3}",
                "{0} با قیمت {1}، تعداد {2}. {3}");

            builder.AddOrUpdate("Aria.Label.CartTotalSummary",
                "Your order: {0} {1}, {2} products.",
                "Ihre Bestellung: {0} {1}, {2} Artikel.",
                "سفارش شما: {0} {1}، {2} محصول.");
            builder.AddOrUpdate("Aria.Label.BuyHint",
                "By clicking on \"Buy,\" I accept the terms and conditions.",
                "Mit Klick auf \"Kaufen\" akzeptiere ich die Bedingungen.",
                "با کلیک روی \"خرید\"، شرایط و ضوابط را می‌پذیرم.");

            builder.AddOrUpdate("Reviews.Overview.Review",
                "Rating: {0} out of 5 stars. {1} review.",
                "Bewertung: {0} von 5 Sternen. {1} Bewertung.",
                "امتیاز: {0} از ۵ ستاره. {1} نقد و بررسی.");
            builder.AddOrUpdate("Reviews.Overview.Reviews",
                "Rating: {0} out of 5 stars. {1} reviews.",
                "Bewertung: {0} von 5 Sternen. {1} Bewertungen.",
                "امتیاز: {0} از ۵ ستاره. {1} نقد و بررسی.");

            builder.AddOrUpdate("Aria.Label.PaginatorItemsPerPage", "Results per page", "Ergebnisse pro Seite", "نتایج در هر صفحه");

            builder.AddOrUpdate("Homepage.TopCategories", "Top categories", "Top-Warengruppen", "دسته‌بندی‌های برتر");
            builder.AddOrUpdate("Common.SkipList", "Skip list", "Liste überspringen", "رد کردن لیست");
            builder.AddOrUpdate("Common.PleaseWait", "Please wait…", "Bitte warten…", "لطفاً منتظر بمانید…");
            builder.AddOrUpdate("Common.Helpful", "Helpful", "Hilfreich", "مفید");
            builder.AddOrUpdate("Common.NotHelpful", "Not helpful", "Nicht hilfreich", "مفید نبود");

            builder.AddOrUpdate("Products.ProductsHaveBeenAddedToTheCart",
                "{0} of {1} products have been added to the shopping cart.",
                "{0} von {1} Produkten wurden in den Warenkorb gelegt.",
                "{0} از {1} محصول به سبد خرید اضافه شدند.");

            builder.Delete(
                "Media.Category.ImageAlternateTextFormat",
                "Media.Manufacturer.ImageAlternateTextFormat",
                "Media.Product.ImageAlternateTextFormat",
                "Common.DecreaseValue",
                "Common.IncreaseValue",
                "Admin.Orders.Address.EditAddress");

            builder.AddOrUpdate("ShoppingCart.DiscountCouponCode.Removed", "The discount code has been removed", "Der Rabattcode wurde entfernt", "کد تخفیف حذف شد");
            builder.AddOrUpdate("ShoppingCart.GiftCardCouponCode.Removed", "The gift card code has been removed", "Der Geschenkgutschein wurde entfernt", "کد کارت هدیه حذف شد");
            builder.AddOrUpdate("ShoppingCart.RewardPoints.Applied", "The reward points were applied.", "Die Bonuspunkte wurden angewendet.", "امتیازات پاداش اعمال شدند.");
            builder.AddOrUpdate("ShoppingCart.RewardPoints.Removed", "The reward points have been removed.", "Die Bonuspunkte wurden entfernt.", "امتیازات پاداش حذف شدند.");

            // Resource value was a bit off.
            builder.AddOrUpdate("ShoppingCart.DiscountCouponCode.Tooltip", "Your discount code", "Ihr Rabattcode", "کد تخفیف شما");

            // Replace problematic "&amp;gt;".
            builder.AddOrUpdate("Search.Facet.RemoveFilter", "Remove filter: {0} \"{1}\"", "Filter aufheben: {0} \"{1}\"", "حذف فیلتر: {0} \"{1}\"");

            builder.AddOrUpdate("Products.SavingBadgeLabel", "&minus; {0} %", "&minus; {0} %", "&minus; {0} %");

            builder.AddOrUpdate("Admin.Rules.FilterDescriptor.CartWeightRule", "Weight of all products in cart", "Gewicht aller Produkte im Warenkorb", "وزن تمامی محصولات در سبد خرید");

            builder.AddOrUpdate("Admin.Configuration.Settings.Price.ApplyDiscountsOfLinkedProducts",
                "Apply discounts of linked products",
                "Rabatte von verknüpften Produkten anwenden",
                "اعمال تخفیفِ محصولات مرتبط",
                "Specifies whether discounts (e.g. tier prices) of linked products are taken into account when calculating attribute price surcharges.",
                "Legt fest, ob bei der Berechnung von Attributpreisaufschlägen die Rabatte (z.B. Staffelpreise) von verknüpften Produkten berücksichtigt werden.",
                "مشخص می‌کند که آیا تخفیف‌های محصولات مرتبط (مانند قیمت‌های پلکانی) هنگام محاسبه اضافه‌بهای ویژگی‌ها در نظر گرفته می‌شوند یا خیر.");

            builder.AddOrUpdate("Admin.Rules.FilterDescriptor.AllProductsFromCategoryInCart",
                "All products from category in cart",
                "Alle Produkte aus Kategorie im Warenkorb",
                "تمامی محصولات یک دسته‌بندی در سبد خرید");
            builder.AddOrUpdate("Admin.Rules.FilterDescriptor.AllProductsFromManufacturerInCart",
                "All products from manufacturer in cart",
                "Alle Produkte von Hersteller im Warenkorb",
                "تمامی محصولات یک تولیدکننده در سبد خرید");
            builder.AddOrUpdate("Admin.Rules.FilterDescriptor.ProductInCategoryTreeCartRule",
                "Product from category or subcategories in cart",
                "Produkt aus Kategorie oder Unterkategorien im Warenkorb",
                "محصول از یک دسته‌بندی یا زیردسته‌های آن در سبد خرید");
            builder.AddOrUpdate("Admin.Rules.FilterDescriptor.SubscribedToNewsletter",
                "Subscribed to newsletter",
                "Newsletter abonniert",
                "عضویت در خبرنامه");

            builder.AddOrUpdate("LinkBuilder.LinkTarget",
                "Define the target attribute for the link.",
                "Definieren Sie das Attribut target für den Link.",
                "مشخصه هدف (target) را برای پیوند تعریف کنید.");

            builder.AddOrUpdate("PrivateMessages.Inbox", "Private messages", "Private Nachrichten", "پیام‌های خصوصی");

            builder.AddOrUpdate("Admin.Catalog.Attributes.SpecificationAttributes.EditAttributeDetails",
                "Edit specification attribute",
                "Spezifikationsattribut bearbeiten",
                "ویرایش ویژگی مشخصات");

            builder.AddOrUpdate("Smartstore.AI.Prompts.CreateImagesOnlyOnExplicitRequest",
                "Only add placeholders for images if this is explicitly requested.",
                "Füge nur dann Platzhalter für Bilder hinzu, wenn dies ausdrücklich gewünscht wird.",
                "تنها در صورتی نگهدارنده‌های تصویر (placeholder) اضافه شوند که صراحتاً درخواست شده باشد.");

            builder.AddOrUpdate("Admin.Permissions.AllPermissionsGranted", "All {0} permissions granted.", "Alle {0} Rechte gewährt.", "تمامی {0} مجوز اعطا شدند.");
            builder.AddOrUpdate("Admin.Permissions.NumPermissionsGranted", "{0} of {1} permissions granted.", "{0} von {1} Rechten gewährt.", "{0} از {1} مجوز اعطا شدند.");
            builder.AddOrUpdate("Admin.Permissions.NoPermissionGranted", "No permissions granted.", "Keine Rechte gewährt.", "هیچ مجوزی اعطا نشده است.");

            builder.AddOrUpdate("Admin.System.Warnings.EuVatWebService.Unstable",
                "Due to the server's IPv6 configuration, the EU web service for validating VAT numbers may not work properly.",
                "Aufgrund der IPv6-Konfiguration des Servers funktioniert der Web-Service der EU zur Überprüfung von Steuernummern möglicherweise nicht korrekt.",
                "به دلیل پیکربندی IPv6 سرور، ممکن است وب‌سرویس اتحادیه اروپا برای اعتبارسنجی شماره‌های مالیات بر ارزش افزوده (VAT) به‌درستی کار نکند.");

            builder.AddOrUpdate("Admin.System.SystemInfo.IPAddress", "IP address", "IP-Adresse", "آدرس آی‌پی (IP)");
            builder.AddOrUpdate("Admin.System.SystemInfo.IPAddress.Hint", "The IP address of the machine.", "Die IP-Adresse der Maschine.", "آدرس آی‌پی (IP) سیستم.");

            // This a workaround/fallback for missing string resources in plugins with duplicate permission names:
            builder.AddOrUpdate("Plugins.Permissions.DisplayName.Display", "Display", "Anzeigen", "نمایش");

            // Return requests
            builder.AddOrUpdate("ReturnRequests.NoItemsSubmitted",
                "Your return request has not been submitted. Please select the required quantity to return.",
                "Ihr Rücksendewunsch wurde nicht übermittelt. Bitte wählen Sie die erforderliche Rücksendemenge aus.",
                "درخواست مرجوعی شما ثبت نشد. لطفاً تعداد مورد نیاز برای مرجوع کردن را انتخاب کنید.");

            builder.AddOrUpdate("ReturnRequests.ReturnsNotPossible",
                "Returns are not possible for this order.",
                "Für diesen Auftrag ist eine Rücksendung von Artikeln nicht möglich.",
                "برای این سفارش امکان مرجوع کردن محصولات وجود ندارد.");

            builder.AddOrUpdate("ReturnRequests.Products.Quantity")
                .Value("en", "Quantity to return")
                .Value("fa", "تعداد برای مرجوعی");

            builder.AddOrUpdate("ReturnRequests.WhyReturning")
                .Value("de", "Warum möchten Sie diese Artikel zurücksenden?")
                .Value("fa", "چرا می‌خواهید این موارد را مرجوع کنید؟");

            builder.AddOrUpdate("ReturnRequests.SelectProduct(s)")
                .Value("de", "Welche Produkte möchten Sie zurücksenden?")
                .Value("fa", "کدام محصولات را می‌خواهید مرجوع کنید؟");

            builder.AddOrUpdate("ReturnRequests.Title",
                "Returnable items from order no. {0}",
                "Retournierbare Artikel aus Auftrag Nr. {0}",
                "موارد قابل مرجوعی از سفارش شماره {0}");

            builder.AddOrUpdate("ReturnRequests.Products.RequestAlreadyExists",
                "There are already return requests for this item.",
                "Zu diesem Artikel gibt es bereits Rücksendewünsche.",
                "برای این محصول از قبل درخواست‌های مرجوعی وجود دارد.");

            builder.AddOrUpdate("Common.EnlargeView", "Enlarge view", "Ansicht vergrößern", "بزرگ‌نمایی نما");

            builder.AddOrUpdate("Admin.Catalog.Categories.Products.AddNew", "Assign products", "Produkte zuordnen", "تخصیص محصولات");
            builder.AddOrUpdate("Admin.Catalog.Categories.ProductsHaveBeenAssignedToCategory",
                "{0} of {1} products have been assigned to the category.",
                "{0} von {1} Produkten wurden der Warengruppe zugeordnet.",
                "{0} از {1} محصول به این دسته‌بندی اختصاص داده شدند.");

            builder.AddOrUpdate("Enums.PostIntroVisibility.Hidden", "Don't show", "Nicht anzeigen", "عدم نمایش");
            builder.AddOrUpdate("Enums.PostIntroVisibility.TwoLines", "Two lines maximum", "Maximal zweizeilig", "حداکثر دو خط");
            builder.AddOrUpdate("Enums.PostIntroVisibility.ThreeLines", "Three lines maximum", "Maximal dreizeilig", "حداکثر سه خط");
            builder.AddOrUpdate("Enums.PostIntroVisibility.FullText", "Show all", "Komplett anzeigen", "نمایش کامل");

            builder.AddOrUpdate("Enums.PostListColumns.Two", "Two columns", "2 Spalten", "دو ستون");
            builder.AddOrUpdate("Enums.PostListColumns.Three", "Three columns", "3 Spalten", "سه ستون");


            builder.Delete(
                "Admin.ContentManagement.Blog.BlogPosts.Fields",
                "Admin.Configuration.Settings.Blog.PostsPageSize",
                "Admin.Configuration.Settings.Blog.PostsPageSize.Hint",
                "Admin.ContentManagement.Blog.Heading.Display",
                "Admin.ContentManagement.Blog.Heading.Publish",
                "Admin.ContentManagement.Blog.BlogPosts.Fields.Picture",
                "Admin.ContentManagement.Blog.BlogPosts.Fields.Picture.Hint",
                "Admin.ContentManagement.Blog.BlogPosts.Fields.PreviewDisplayType",
                "Admin.ContentManagement.Blog.BlogPosts.Fields.PreviewDisplayType.Hint",
                "Admin.ContentManagement.Blog.BlogPosts.Fields.PreviewPicture",
                "Admin.ContentManagement.Blog.BlogPosts.Fields.PreviewPicture.Hint",
                "Admin.ContentManagement.Blog.BlogPosts.Fields.SectionBg",
                "Admin.ContentManagement.Blog.BlogPosts.Fields.SectionBg.Hint",
                "Admin.ContentManagement.Blog.BlogPosts.Fields.DisplayTagsInPreview",
                "Admin.ContentManagement.Blog.BlogPosts.Fields.DisplayTagsInPreview.Hint",
                "Admin.ContentManagement.Blog.BlogPosts.Fields.Title",
                "Admin.ContentManagement.Blog.BlogPosts.Fields.Title.Hint",
                "Admin.ContentManagement.Blog.BlogPosts.Fields.Intro",
                "Admin.ContentManagement.Blog.BlogPosts.Fields.Intro.Hint",
                "Admin.ContentManagement.Blog.BlogPosts.Fields.Body",
                "Admin.ContentManagement.Blog.BlogPosts.Fields.Body.Hint",
                "Admin.ContentManagement.Blog.BlogPosts.Fields.Tags",
                "Admin.ContentManagement.Blog.BlogPosts.Fields.Tags.Hint",
                "Admin.ContentManagement.Blog.BlogPosts.Fields.AllowComments",
                "Admin.ContentManagement.Blog.BlogPosts.Fields.AllowComments.Hint",
                "Admin.ContentManagement.Blog.BlogPosts.Fields.StartDate",
                "Admin.ContentManagement.Blog.BlogPosts.Fields.StartDate.Hint",
                "Admin.ContentManagement.Blog.BlogPosts.Fields.EndDate",
                "Admin.ContentManagement.Blog.BlogPosts.Fields.EndDate.Hint",
                "Admin.ContentManagement.Blog.BlogPosts.Fields.Comments");

            builder.AddOrUpdate("Admin.Configuration.Settings.PostsPageSize", "Posts per page", "Beiträge pro Seite", "پست‌ها در هر صفحه");

            // Collection Groups
            builder.AddOrUpdate("Permissions.DisplayName.CollectionGroup", "Display groups", "Anzeigegruppen", "گروه‌های نمایش");
            builder.AddOrUpdate("Admin.Configuration.CollectionGroups", "Display groups", "Anzeigegruppen", "گروه‌های نمایش");
            builder.AddOrUpdate("Admin.Configuration.CollectionGroups.Add", "Add display group", "Anzeigegruppe hinzufügen", "افزودن گروه نمایش");

            builder.AddOrUpdate("Admin.Configuration.CollectionGroups.Info",
                "Display groups can be used to organize lists, such as lists of specification attributes, and present them more clearly.",
                "Mit Hilfe von Anzeigegruppen können Listen, wie etwa eine Liste von Spezifikationsattributen, gruppiert und somit übersichtlicher dargestellt werden.",
                "از گروه‌های نمایش می‌توان برای سازماندهی لیست‌ها (مانند لیست ویژگی‌های مشخصات) و ارائه واضح‌تر آن‌ها استفاده کرد.");

            builder.AddOrUpdate("Admin.Catalog.Attributes.SpecificationAttributes.Fields.CollectionGroup",
                "Display Group",
                "Anzeigegruppe",
                "گروه نمایش",
                "Specifies the name of a display group (optional). This causes the attributes to be displayed in groups in the frontend.",
                "Legt den Namen einer Anzeigegruppe fest (optional). Dadurch werden die Attribute im Frontend gruppiert angezeigt.",
                "نام یک گروه نمایش را مشخص می‌کند (اختیاری). این ویژگی باعث می‌شود که مشخصات در بخش کاربری (فرانت‌اند) به‌صورت گروه‌بندی‌شده نمایش داده شوند.");

            builder.AddOrUpdate("Admin.Configuration.CollectionGroup.Name",
                "Name",
                "Name",
                "نام",
                "Specifies the name of the display group.",
                "Legt den Namen der Anzeigegruppe fest.",
                "نام گروه نمایش را مشخص می‌کند.");

            builder.AddOrUpdate("Admin.Configuration.CollectionGroup.EntityName",
                "Object",
                "Objekt",
                "شیء",
                "The name of the object assigned to the display group.",
                "Der Name des Objekts, das der Anzeigegruppe zugeordnet ist.",
                "نام شیء اختصاص‌یافته به گروه نمایش.");

            builder.AddOrUpdate("Admin.Configuration.CollectionGroup.NumberOfAssignments",
                "Assignments",
                "Zuordnungen",
                "تخصیص‌ها",
                "The number of objects assigned to the display group.",
                "Die Anzahl der Objekte, die der Anzeigegruppe zugeordnet sind.",
                "تعداد اشیاء اختصاص‌یافته به گروه نمایش.");

            builder.AddOrUpdate("Common.DontShowDialogAgain", "Do not show this dialog again", "Diesen Dialog nicht mehr anzeigen", "این پنجره را دوباره نشان نده");

            builder.AddOrUpdate("Admin.Common.Configured", "Configured", "Konfiguriert", "پیکربندی شده");
            builder.AddOrUpdate("Admin.Common.NotConfigured", "Not configured", "Nicht konfiguriert", "پیکربندی نشده");

            builder.AddOrUpdate("Admin.Promotions.Campaigns.Warning",
                "Save the campaign and use the preview button to test it before sending it to many customers."
                + " You can set additional settings, such as the email account to be used, in the"
                + " <a href=\"{0}\" class=\"alert-link\">System.Campaign</a>",
                "Speichern Sie die Kampagne und benutzen Sie den Vorschau-Button, um sie zu testen, bevor Sie sie an viele Kunden versenden."
                + " Weitere Einstellungen, wie beispielsweise das bei der Kampagne zu verwendende E-Mail-Konto, können bei der Nachrichtenvorlage"
                + " <a href=\"{0}\" class=\"alert-link\">System.Campaign</a> vorgenommen werden.",
                "کمپین را ذخیره کنید و پیش از ارسال آن برای مشتریان زیادی، از دکمه پیش‌نمایش برای تست استفاده کنید."
                + " تنظیمات بیشتر مانند حساب ایمیل مورد استفاده در این کمپین را می‌توانید در بخش"
                + " <a href=\"{0}\" class=\"alert-link\">System.Campaign</a> پیکربندی کنید.");

            builder.AddOrUpdate("Admin.Promotions.Discounts.Fields.CouponCode")
                .Value("de", "Rabattcode")
                .Value("fa", "کد تخفیف");
            builder.AddOrUpdate("Admin.Promotions.Discounts.Fields.RequiresCouponCode")
                .Value("de", "Rabattcode erforderlich")
                .Value("fa", "نیازمند کد تخفیف");
            builder.AddOrUpdate("Admin.Promotions.Discounts.Fields.RequiresCouponCode.Hint")
                .Value("de", "Legt fest, dass der Rabatt erst nach Eingabe des Rabattcodes auf der Warenkorbseite angewendet wird.")
                .Value("fa", "مشخص می‌کند که تخفیف تنها پس از وارد کردن کد تخفیف در صفحه سبد خرید اعمال خواهد شد.");
            builder.Delete("Admin.Promotions.Discounts.Fields.CouponCode.Hint");

            builder.AddOrUpdate("Products.ProductCodeNotFound",
                "The product with the code <b>{0}</b> could not be found.",
                "Es wurde kein Produkt mit dem Produktcode <b>{0}</b> gefunden.",
                "محصولی با کد <b>{0}</b> یافت نشد.");


            // MOSAIC
            // M (Motiv) =>  we already got these resources.
            // O (Optics)
            builder.AddOrUpdate("Admin.AI.ImageCreation.Optic",
                "The visual style is {0} with a {1} color palette.",
                "Die Optik ist {0} und die Farbgestaltung ist {1}.",
                "سبک بصری {0} با پالت رنگی {1} است.");
            builder.AddOrUpdate("Admin.AI.ImageCreation.Optic.Fallback",
                "The visual style is focused on {0} and uses a color palette described as {1}.",
                "Die Optik ist auf {0} fokussiert und verwendet eine Farbgestaltung, die als {1} beschrieben wird.",
                "سبک بصری بر روی {0} متمرکز است و از پالت رنگی توصیف‌شده به عنوان {1} استفاده می‌کند.");

            // Optics: Medium only
            builder.AddOrUpdate("Admin.AI.ImageCreation.Optic.MediumOnly",
                "The visual style is {0}.",
                "Die Optik ist {0}.",
                "سبک بصری {0} است.");
            builder.AddOrUpdate("Admin.AI.ImageCreation.Optic.MediumOnly.Fallback",
                "The visual style is focused on {0}.",
                "Die Optik ist auf {0} fokussiert.",
                "سبک بصری بر روی {0} متمرکز است.");

            // Optics: Color only
            builder.AddOrUpdate("Admin.AI.ImageCreation.Optic.ColorOnly",
                "The visual style uses a {0} color palette.",
                "Die Optik verwendet eine Farbgestaltung, die {0} ist.",
                "سبک بصری از پالت رنگی {0} استفاده می‌کند.");
            builder.AddOrUpdate("Admin.AI.ImageCreation.Optic.ColorOnly.Fallback",
                "The visual style uses a color palette focused on {0}.",
                "Die Optik verwendet eine Farbgestaltung mit Fokus auf {0}.",
                "سبک بصری از پالت رنگی متمرکز بر {0} استفاده می‌کند.");

            // S (Scene)
            builder.AddOrUpdate("Admin.AI.ImageCreation.Scene",
                "The scene takes place {0}.",
                "Die Szene spielt {0}.",
                "صحنه در {0} رخ می‌دهد.");
            builder.AddOrUpdate("Admin.AI.ImageCreation.Scene.Fallback",
                "The scene takes place in an environment with a focus on {0}.",
                "Die Szene spielt in einer Umgebung mit Fokus auf {0}.",
                "صحنه در محیطی با تمرکز بر {0} رخ می‌دهد.");

            // A (Atmosphere)
            builder.AddOrUpdate("Admin.AI.ImageCreation.Atmosphere",
                "The atmosphere feels {1} and is shaped by {0}.",
                "Die Atmosphäre wirkt {1} und wird durch {0} geprägt.",
                "فضا احساس {1} را منتقل می‌کند و توسط {0} شکل گرفته است.");
            builder.AddOrUpdate("Admin.AI.ImageCreation.Atmosphere.Fallback",
                "The atmosphere is intended to feel {1} and is shaped by lighting that is {0}.",
                "Die Atmosphäre soll {1} wirken und wird durch eine Beleuchtung geprägt, die {0} ist.",
                "قرار است فضا احساس {1} را منتقل کند و با نورپردازی که {0} است شکل گرفته است.");

            builder.AddOrUpdate("Admin.AI.ImageCreation.Atmosphere.LightingOnly",
                "The atmosphere is shaped by {0}.",
                "Die Atmosphäre wird durch {0} geprägt.",
                "فضا توسط {0} شکل گرفته است.");
            builder.AddOrUpdate("Admin.AI.ImageCreation.Atmosphere.LightingOnly.Fallback",
                "The atmosphere is shaped by lighting that is {0}.",
                "Die Atmosphäre wird durch eine Beleuchtung geprägt, die {0} ist.",
                "فضا توسط نورپردازی که {0} است شکل گرفته است.");

            builder.AddOrUpdate("Admin.AI.ImageCreation.Atmosphere.MoodOnly",
                "The atmosphere feels {0}.",
                "Die Atmosphäre wirkt {0}.",
                "فضا احساس {0} را منتقل می‌کند.");
            builder.AddOrUpdate("Admin.AI.ImageCreation.Atmosphere.MoodOnly.Fallback",
                "The atmosphere is intended to feel {0}.",
                "Die Atmosphäre soll {0} wirken.",
                "قرار است فضا احساس {0} را منتقل کند.");

            // I (Inszenierung)
            builder.AddOrUpdate("Admin.AI.ImageCreation.Staging",
                "The staging follows {0}.",
                "Die Inszenierung folgt {0}.",
                "صحنه‌پردازی از {0} پیروی می‌کند.");
            builder.AddOrUpdate("Admin.AI.ImageCreation.Staging.Fallback",
                "The staging is arranged with a focus on {0}.",
                "Die Inszenierung ist mit Fokus auf {0} angelegt.",
                "صحنه‌پردازی با تمرکز بر {0} تنظیم شده است.");

            // K (Kontext) is handled by PromptGenerators because they know their context.

            // O => Medium params
            builder.AddOrUpdate("Admin.AI.ImageCreation.Param.Photo", "photo", "Foto", "عکس");
            builder.AddOrUpdate("Admin.AI.ImageCreation.PromptFragment.Photo", "that of a photograph", "die einer Fotografie", "مربوط به یک عکس");

            builder.AddOrUpdate("Admin.AI.ImageCreation.Param.Painting", "painting", "Gemälde", "نقاشی");
            builder.AddOrUpdate("Admin.AI.ImageCreation.PromptFragment.Painting", "that of a painting", "die eines Gemäldes", "مربوط به یک نقاشی");

            builder.AddOrUpdate("Admin.AI.ImageCreation.Param.Illustration", "illustration", "Illustration", "تصویرسازی");
            builder.AddOrUpdate("Admin.AI.ImageCreation.PromptFragment.Illustration", "that of an illustration", "die einer Illustration", "مربوط به یک تصویرسازی");

            // O => Colors params
            builder.AddOrUpdate("Admin.AI.ImageCreation.Param.Colorful", "colorful", "bunt", "رنگارنگ");
            builder.AddOrUpdate("Admin.AI.ImageCreation.PromptFragment.Colorful", "colorful", "bunt", "رنگارنگ");

            builder.AddOrUpdate("Admin.AI.ImageCreation.Param.Monochromatic", "monochromatic", "einfarbig", "تک‌رنگ");
            builder.AddOrUpdate("Admin.AI.ImageCreation.PromptFragment.Monochromatic", "monochromatic", "einfarbig", "تک‌رنگ");

            builder.AddOrUpdate("Admin.AI.ImageCreation.Param.Muted", "muted", "gedämpft", "ملایم (خاموش)");
            builder.AddOrUpdate("Admin.AI.ImageCreation.PromptFragment.Muted", "muted", "gedämpft", "ملایم (خاموش)");

            builder.AddOrUpdate("Admin.AI.ImageCreation.Param.Bright", "bright", "hell", "روشن");
            builder.AddOrUpdate("Admin.AI.ImageCreation.PromptFragment.Bright", "bright", "hell", "روشن");

            builder.AddOrUpdate("Admin.AI.ImageCreation.Param.Vibrant", "vibrant", "lebhaft", "پر جنب و جوش (زنده)");
            builder.AddOrUpdate("Admin.AI.ImageCreation.PromptFragment.Vibrant", "vibrant", "lebhaft", "پر جنب و جوش (زنده)");

            builder.AddOrUpdate("Admin.AI.ImageCreation.Param.Pastel", "pastel", "pastellfarben", "پاستلی");
            builder.AddOrUpdate("Admin.AI.ImageCreation.PromptFragment.Pastel", "pastel", "pastellfarben", "پاستلی");

            builder.AddOrUpdate("Admin.AI.ImageCreation.Param.BlackAndWhite", "black and white", "schwarz-weiß", "سیاه و سفید");
            builder.AddOrUpdate("Admin.AI.ImageCreation.PromptFragment.BlackAndWhite", "black and white", "schwarz-weiß", "سیاه و سفید");


            // S => Environment params
            builder.AddOrUpdate("Admin.AI.ImageCreation.Param.Outdoors", "outdoors", "draußen", "فضای باز");
            builder.AddOrUpdate("Admin.AI.ImageCreation.PromptFragment.Outdoors", "outdoors in an open environment", "draußen im Freien", "فضای باز در یک محیط بیرونی");

            builder.AddOrUpdate("Admin.AI.ImageCreation.Param.Inside", "indoors", "drinnen", "فضای بسته");
            builder.AddOrUpdate("Admin.AI.ImageCreation.PromptFragment.Inside", "indoors in an interior space", "in einem Innenraum", "فضای بسته در یک محیط داخلی");

            builder.AddOrUpdate("Admin.AI.ImageCreation.Param.Shop", "shop", "Geschäft", "فروشگاه");
            builder.AddOrUpdate("Admin.AI.ImageCreation.PromptFragment.Shop", "inside a shop interior", "in einem Geschäft / Laden", "داخل فضای یک فروشگاه");

            builder.AddOrUpdate("Admin.AI.ImageCreation.Param.Kitchen", "kitchen", "Küche", "آشپزخانه");
            builder.AddOrUpdate("Admin.AI.ImageCreation.PromptFragment.Kitchen", "in a kitchen interior", "in einer Küche", "در فضای داخلی آشپزخانه");

            builder.AddOrUpdate("Admin.AI.ImageCreation.Param.City", "city", "Stadt", "شهر");
            builder.AddOrUpdate("Admin.AI.ImageCreation.PromptFragment.City", "in an urban city environment", "in einer städtischen Umgebung", "در محیط شهری");

            builder.AddOrUpdate("Admin.AI.ImageCreation.Param.Beach", "beach", "Strand", "ساحل");
            builder.AddOrUpdate("Admin.AI.ImageCreation.PromptFragment.Beach", "at a beach", "an einem Strand", "در ساحل");

            builder.AddOrUpdate("Admin.AI.ImageCreation.Param.Forest", "forest", "Wald", "جنگل");
            builder.AddOrUpdate("Admin.AI.ImageCreation.PromptFragment.Forest", "in a forest", "in einem Wald", "در جنگل");

            builder.AddOrUpdate("Admin.AI.ImageCreation.Param.LivingRoom", "living room", "Wohnzimmer", "اتاق نشیمن");
            builder.AddOrUpdate("Admin.AI.ImageCreation.PromptFragment.LivingRoom", "in a living room interior", "in einem Wohnzimmer", "در فضای داخلی یک اتاق نشیمن");

            // A => Lighting params
            builder.AddOrUpdate("Admin.AI.ImageCreation.Param.Ambient", "ambient", "ambient", "محیطی");
            builder.AddOrUpdate("Admin.AI.ImageCreation.PromptFragment.Ambient", "soft ambient lighting", "weiches Umgebungslicht", "نورپردازی ملایم محیطی");

            builder.AddOrUpdate("Admin.AI.ImageCreation.Param.Overcast", "overcast", "bewölkt", "ابری");
            builder.AddOrUpdate("Admin.AI.ImageCreation.PromptFragment.Overcast", "overcast daylight", "bewölktes Tageslicht", "نور روز ابری");

            builder.AddOrUpdate("Admin.AI.ImageCreation.Param.Neon", "neon", "neon", "نئون");
            builder.AddOrUpdate("Admin.AI.ImageCreation.PromptFragment.Neon", "neon lighting", "Neonlicht", "نورپردازی نئون");

            builder.AddOrUpdate("Admin.AI.ImageCreation.Param.Studio", "studio lights", "Studiobeleuchtung", "نورهای استودیو");
            builder.AddOrUpdate("Admin.AI.ImageCreation.PromptFragment.Studio", "professional studio lighting", "professionelle Studiobeleuchtung", "نورپردازی حرفه‌ای استودیو");

            builder.AddOrUpdate("Admin.AI.ImageCreation.Param.Soft", "soft", "weich", "ملایم");
            builder.AddOrUpdate("Admin.AI.ImageCreation.PromptFragment.Soft", "soft, diffused light", "weiches, diffuses Licht", "نور ملایم و پخش‌شده");

            // A => Mood params
            builder.AddOrUpdate("Admin.AI.ImageCreation.Param.Cosy", "cosy", "gemütlich", "دنج");
            builder.AddOrUpdate("Admin.AI.ImageCreation.PromptFragment.Cosy", "cosy and inviting", "gemütlich und einladend", "دنج و دعوت‌کننده");

            builder.AddOrUpdate("Admin.AI.ImageCreation.Param.Futuristic", "futuristic", "futuristisch", "آینده‌نگرانه");
            builder.AddOrUpdate("Admin.AI.ImageCreation.PromptFragment.Futuristic", "futuristic and high-tech", "futuristisch und technologisch", "آینده‌نگرانه و با فناوری پیشرفته");

            builder.AddOrUpdate("Admin.AI.ImageCreation.Param.Hectic", "hectic", "hektisch", "پر هیاهو");
            builder.AddOrUpdate("Admin.AI.ImageCreation.PromptFragment.Hectic", "busy and hectic", "hektisch und geschäftig", "شلوغ و پر هیاهو");

            builder.AddOrUpdate("Admin.AI.ImageCreation.Param.Nostalgic", "nostalgic", "nostalgisch", "نوستالژیک");
            builder.AddOrUpdate("Admin.AI.ImageCreation.PromptFragment.Nostalgic", "nostalgic and sentimental", "nostalgisch und sentimental", "نوستالژیک و احساساتی");

            builder.AddOrUpdate("Admin.AI.ImageCreation.Param.Mysterious", "mysterious", "geheimnisvoll", "مرموز");
            builder.AddOrUpdate("Admin.AI.ImageCreation.PromptFragment.Mysterious", "mysterious and slightly enigmatic", "geheimnisvoll und rätselhaft", "مرموز و کمی رازآلود");

            builder.AddOrUpdate("Admin.AI.ImageCreation.Param.Relaxing", "relaxing", "entspannend", "آرام‌بخش");
            builder.AddOrUpdate("Admin.AI.ImageCreation.PromptFragment.Relaxing", "calm and relaxing", "ruhig und entspannend", "آرام و آرام‌بخش");

            // I => Staging params
            builder.AddOrUpdate("Admin.AI.ImageCreation.Param.Asymmetrical", "asymmetrical", "asymmetrisch", "نامتقارن");
            builder.AddOrUpdate("Admin.AI.ImageCreation.PromptFragment.Asymmetrical", "an asymmetrical composition", "einer asymmetrischen Komposition", "یک ترکیب‌بندی نامتقارن");

            builder.AddOrUpdate("Admin.AI.ImageCreation.Param.RuleOfThirds", "Rule of thirds", "Drittel-Regel", "قانون یک‌سوم");
            builder.AddOrUpdate("Admin.AI.ImageCreation.PromptFragment.RuleOfThirds", "the rule of thirds", "der Drittel-Regel", "قانون یک‌سوم");

            builder.AddOrUpdate("Admin.AI.ImageCreation.Param.GoldenRatio", "Golden ratio", "Goldener Schnitt", "نسبت طلایی");
            builder.AddOrUpdate("Admin.AI.ImageCreation.PromptFragment.GoldenRatio", "the golden ratio", "dem Goldenen Schnitt", "نسبت طلایی");

            builder.AddOrUpdate("Admin.AI.ImageCreation.Param.Closeup", "Closeup", "Nahaufnahme", "نمای نزدیک (کلوزآپ)");
            builder.AddOrUpdate("Admin.AI.ImageCreation.PromptFragment.Closeup", "a close-up composition", "einer Nahaufnahme-Komposition", "یک ترکیب‌بندی نمای نزدیک");

            builder.AddOrUpdate("Admin.AI.ImageCreation.Param.Portrait", "Portrait", "Porträt", "پرتره");
            builder.AddOrUpdate("Admin.AI.ImageCreation.PromptFragment.Portrait", "a classic portrait composition", "einer klassischen Porträtkomposition", "یک ترکیب‌بندی پرتره کلاسیک");

            builder.AddOrUpdate("Admin.AI.ImageCreation.Param.Headshot", "Headshot", "Kopf-und-Schultern-Porträt", "عکس پرسنلی (هدشات)");
            builder.AddOrUpdate("Admin.AI.ImageCreation.PromptFragment.Headshot", "a headshot composition", "einer Kopf-und-Schultern-Porträtkomposition", "یک ترکیب‌بندی از سر و شانه‌ها");

            builder.AddOrUpdate("Admin.AI.ImageCreation.Param.Symmetrical", "symmetrical", "symmetrisch", "متقارن");
            builder.AddOrUpdate("Admin.AI.ImageCreation.PromptFragment.Symmetrical", "a symmetrical composition", "einer symmetrischen Komposition", "یک ترکیب‌بندی متقارن");

            builder.AddOrUpdate("Admin.AI.ImageCreation.Param.BirdsEye", "Birds-eye view", "Vogelperspektive", "نمای چشم پرنده");
            builder.AddOrUpdate("Admin.AI.ImageCreation.PromptFragment.BirdsEye", "a bird's-eye view composition", "einer Komposition aus der Vogelperspektive", "یک ترکیب‌بندی از نمای چشم پرنده");

            builder.AddOrUpdate("Admin.AI.ImageCreation.Describe", "Add picture description", "Bildbeschreibung hinzufügen", "افزودن توضیحات تصویر");

        }
    }
}
