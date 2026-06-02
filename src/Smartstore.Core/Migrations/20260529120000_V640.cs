using FluentMigrator;
using Smartstore.Core.Common.Configuration;
using Smartstore.Core.Configuration;
using Smartstore.Data.Migrations;
using Smartstore.Utilities;

namespace Smartstore.Core.Data.Migrations;

[MigrationVersion("2026-05-29 12:00:00", "V640")]
internal class V640 : Migration, ILocaleResourcesProvider, IDataSeeder<SmartDbContext>
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
        await MigrateSettingsAsync(context, cancelToken);
    }

    public async Task MigrateSettingsAsync(SmartDbContext context, CancellationToken cancelToken = default)
    {
        // ContentSlider: Corrected setting name & adaptions for template 6.
        var contentSliderTemplate = new[]
        {
            // Template1
            @"<div class=""container h-100""><div class=""row h-100""><div class=""col-md-6 py-3 text-md-right text-center""><h2 data-aos=""slide-right"" style=""--aos-delay: 600ms"">Slide-Title</h2><p class=""d-none d-md-block"" data-aos=""slide-right"" style=""--aos-delay: 800ms"">Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.</p></div><div class=""col-md-6 picture-container""><img alt="""" src=""https://picsum.photos/600/600"" class=""img-fluid"" /></div></div></div>",
            // Template2
            @"<div class=""container h-100""><div class=""row h-100""><div class=""col-6 col-md-3 picture-container""><img src=""https://picsum.photos/400/600"" class=""img-fluid"" /></div><div class=""col-6 col-md-9 py-3""><h2 data-aos=""slide-left"" style=""--aos-delay: 600ms"">Slide-Title</h2><p data-aos=""slide-left"" style=""--aos-delay: 800ms"">Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum.</p></div></div></div>",
            // Template3
            @"<div class=""container h-100""><div class=""row h-100""><div class=""col-md-12 col-lg-6 picture-container""><img alt="""" src=""https://picsum.photos/600/600"" class=""img-fluid"" /></div><div class=""col-lg-6 d-none d-lg-block""><h2 data-aos=""slide-left"" style=""--aos-delay: 600ms"">Slide-Title</h2><p data-aos=""slide-left"" style=""--aos-delay: 800ms"">Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.</p></div></div></div>",
            // Template4
            @"<div class=""container h-100""><div class=""row h-100""><div class=""col-md-6 d-flex align-items-center justify-content-end""><figure class=""picture-container vertical-align-middle"" data-aos=""zoom-in"" data-aos-easing=""ease-out-cubic""><img src=""https://picsum.photos/300/300"" class=""img-fluid"" /></figure></div><div class=""col-md-6 d-flex align-items-center""><div><h2 data-aos=""slide-left"" style=""--aos-delay: 600ms"">Slide-Title</h2><p data-aos=""slide-left"" style=""--aos-delay: 900ms"">Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est .</p></div></div></div></div>",
            // Template5
            @"<div class=""container h-100""><div class=""row h-100""><div class=""col col-md-3 col-sm-6"" data-aos=""slide-down"" data-aos-easing=""ease-out-cubic"" style=""--aos-delay: 1000ms""><img src=""https://picsum.photos/300/500/"" class=""img-fluid"" /></div><div class=""col-md-3 col-12 col-sm-6 d-none d-sm-block"" data-aos=""slide-down"" data-aos-easing=""ease-out-cubic"" style=""--aos-delay: 1500ms""><img src=""https://picsum.photos/300/501/"" class=""img-fluid"" /></div><div class=""col-md-3 d-none d-md-block"" data-aos=""slide-down"" data-aos-easing=""ease-out-cubic"" style=""--aos-delay: 2000ms""><img src=""https://picsum.photos/300/502/"" class=""img-fluid"" /></div><div class=""col-md-3 d-none d-md-block"" data-aos=""slide-down"" data-aos-easing=""ease-out-cubic"" style=""--aos-delay: 2500ms""><img src=""https://picsum.photos/300/503/"" class=""img-fluid"" /></div></div></div>",
            // Template6
            @"<div class=""container p-0""><div class=""row h-100 g-0""><div class=""col-md-3 d-none d-md-block"" data-aos=""fade-up"" data-aos-easing=""ease-out-cubic"" style=""--aos-delay: 1500ms""><img src=""https://picsum.photos/330/501/"" class=""img-fluid"" /></div><div class=""col-md-3 col-12 col-sm-6"" data-aos=""fade-right"" data-aos-easing=""ease-out-cubic"" style=""--aos-delay: 500ms""><img class=""img-fluid"" src=""https://picsum.photos/330/500/"" /></div><div class=""col-md-3 col-sm-6"" data-aos=""fade-left"" data-aos-easing=""ease-out-cubic"" style=""--aos-delay: 500ms""><img class=""img-fluid"" src=""https://picsum.photos/330/500/"" /></div><div class=""col-md-3 d-none d-md-block"" data-aos=""fade-up"" data-aos-easing=""ease-out-cubic"" style=""--aos-delay: 1500ms""><img src=""https://picsum.photos/330/501/"" class=""img-fluid"" /></div></div></div>"
        };

        for (var i = 0; i < 6; i++)
        {
            var templateName = $"ContentSliderSettings.Template{i + 1}";
            var template = await context.Settings
                .Where(x => x.Name == templateName)
                .FirstOrDefaultAsync(cancelToken);

            if (template != null)
            {
                context.Remove(template);
                context.Settings.Add(new Setting
                {
                    Name = templateName,
                    Value = contentSliderTemplate[i],
                    StoreId = 0
                });
            }
        }

        await context.MigrateSettingsAsync(builder =>
        {
            builder.Add(TypeHelper.NameOf<CommonSettings>(x => x.MinLogLevelToRetain, true), LogLevel.Error);
        });
    }

    public void MigrateLocaleResources(LocaleResourcesBuilder builder)
    {
        builder.AddOrUpdate("Admin.Orders.Products.AppliedDiscounts",
      "The following discounts were applied to the products: {0}.",
      "Auf die Produkte wurden die folgenden Rabatte gewährt: {0}.",
      "تخفیف‌های زیر روی محصولات اعمال شدند: {0}.");

        builder.AddOrUpdate("Identity.Error.PasswordRequiresDigit",
            "At least one number (0–9)",
            "Mindestens eine Ziffer (0–9)",
            "حداقل یک عدد (۰–۹)");

        builder.AddOrUpdate("Identity.Error.PasswordRequiresLower",
            "At least one lowercase letter (a–z)",
            "Mindestens ein Kleinbuchstabe (a–z)",
            "حداقل یک حرف کوچک (a-z)");

        builder.AddOrUpdate("Identity.Error.PasswordRequiresNonAlphanumeric",
            "At least one special character (e.g. !@#$)",
            "Mindestens ein Sonderzeichen (z.B. !@#$)",
            "حداقل یک نویسه ویژه (مانند !@#$)");

        builder.AddOrUpdate("Identity.Error.PasswordRequiresUniqueChars",
            "At least {0} unique characters",
            "Mindestens {0} eindeutige Zeichen",
            "حداقل {0} نویسه یکتا (غیرتکراری)");

        builder.AddOrUpdate("Identity.Error.PasswordRequiresUpper",
            "At least one uppercase letter (A–Z)",
            "Mindestens ein Großbuchstabe (A–Z)",
            "حداقل یک حرف بزرگ (A-Z)");

        builder.AddOrUpdate("Identity.Error.PasswordTooShort",
            "At least {0} characters",
            "Mindestens {0} Zeichen",
            "حداقل {0} نویسه");

        builder.AddOrUpdate("Account.Register.Result.MeetPasswordRules",
            "Password must meet these rules: {0}",
            "Passwort muss diese Regeln erfüllen: {0}",
            "رمز عبور باید شامل این قوانین باشد: {0}");

        builder.AddOrUpdate("Admin.OrderNotice.OrderPlacedUcp",
            "The order was placed using UCP (Agentic Commerce) \"{0}\". The payment token {1} was processed without a user interface.",
            "Bestellung ist über UCP (Agentic Commerce) \"{0}\" eingegangen. Das Zahlungstoken {1} wurde ohne Benutzeroberfläche verarbeitet.",
            "سفارش با استفاده از UCP (تجارت عاملی) \"{0}\" ثبت شده است. توکن پرداخت {1} بدون رابط کاربری پردازش شد.");

        builder.AddOrUpdate("Order.Product(s).OrderedQuantity",
            "Ordered",
            "Bestellt",
            "سفارش‌داده‌شده");

        builder.AddOrUpdate("Admin.AI.TextCreation.Organize",
            "Organize",
            "Gliedern",
            "سازماندهی");

        builder.AddOrUpdate("Smartstore.AI.Prompts.EnsureLogicalFlow",
            "Add headings (h2-h6) where appropriate, break long paragraphs, use lists for enumerations, ensure logical flow.",
            "Füge passende Überschriften (h2-h6) ein, unterteile lange Absätze, nutze Listen für Aufzählungen und stelle einen logischen Ablauf sicher.",
            "در صورت لزوم از عناوین (h2-h6) استفاده کنید، پاراگراف‌های طولانی را بشکنید، برای شمارش‌ها از لیست‌ها استفاده کنید و جریان منطقی متن را تضمین کنید.");

        builder.AddOrUpdate("Smartstore.AI.Prompts.AssignIdToHeader",
            "Assign each heading a unique, concise id attribute in kebab-case format based on core content keywords. On ID collision: number them (e.g., id=\"benefits-1\", id=\"benefits-2\").",
            "Vergib für jede Überschrift ein eindeutiges, prägnantes id-Attribut im \"kebab-case\"-Format, das auf den Kern-Keywords des Textinhalts basiert. Bei ID-Kollision: nummeriere (z.B. id=\"vorteile-1\", id=\"vorteile-2\").",
            "به هر عنوان یک شناسه (id) یکتا و مختصر با فرمت kebab-case بر اساس کلمات کلیدی محتوا اختصاص دهید. در صورت تداخل شناسه‌ها، آن‌ها را شماره‌گذاری کنید (مانند id=\"benefits-1\" و id=\"benefits-2\").");

        builder.AddOrUpdate("Smartstore.AI.Prompts.CleanupMarkup",
            "Remove inline styles, empty elements, unnecessary span/div/font wrappers, and redundant nesting.",
            "Entferne Inline-Styles, leere Elemente, überflüssige span/div/font-Umhüllungen und unnötige Verschachtelungen.",
            "استایل‌های درون‌خطی، عناصر خالی، پوشش‌های غیرضروری span/div/font و تو در تو بودن‌های اضافی را حذف کنید.");

        builder.AddOrUpdate("Smartstore.AI.Prompts.PreserveSemantic",
            "Keep all semantic HTML intact (including tables, blockquotes, images, code blocks, etc.). Preserve CSS classes, links, and attributes.",
            "Behalte sämtliche semantischen HTML-Elemente bei (inkl. Tabellen, Blockquotes, Bilder, Code-Blöcke etc.). Erhalte CSS-Klassen, Links und Attribute.",
            "تمام عناصر معنایی HTML (از جمله جداول، نقل‌قول‌ها، تصاویر، بلوک‌های کد و غیره) را دست‌نخورده نگه دارید. کلاس‌های CSS، پیوندها و ویژگی‌ها را حفظ کنید.");

        builder.AddOrUpdate("Smartstore.AI.Prompts.OnlyImproveStructure",
            "Only improve structure—don't remove content or meaningful markup.",
            "Verbessere ausschließlich die Struktur – keine Inhalte oder bedeutsames Markup entfernen.",
            "فقط ساختار را بهبود ببخشید - محتوا یا نشانه‌گذاری‌های (Markup) معنادار را حذف نکنید.");

        builder.AddOrUpdate("Admin.AI.TopicGenerate", "Generate {0}", "{0} generieren", "تولید {0}");
        builder.AddOrUpdate("Admin.AI.TopicOptimize", "Optimize {0}", "{0} optimieren", "بهینه‌سازی {0}");
        builder.AddOrUpdate("Admin.AI.Topic.Text", "Text", "Text", "متن");
        builder.AddOrUpdate("Admin.AI.Topic.Image", "Image", "Bild", "تصویر");
        builder.AddOrUpdate("Admin.AI.Topic.ShortDesc", "Short Description", "Kurzbeschreibung", "توضیح کوتاه");
        builder.AddOrUpdate("Admin.AI.Topic.MetaTitle", "Title-Tag", "Title-Tag", "تگ عنوان (Title-Tag)");
        builder.AddOrUpdate("Admin.AI.Topic.MetaDesc", "Meta Description", "Meta Description", "توضیحات متا");
        builder.AddOrUpdate("Admin.AI.Topic.MetaKeywords", "Meta Keywords", "Meta Keywords", "کلمات کلیدی متا");
        builder.AddOrUpdate("Admin.AI.Topic.FullDesc", "Full Description", "Langtext", "توضیح کامل");

        builder.Delete(
            "Admin.AI.EditHtml",
            "Admin.AI.CreateImage",
            "Admin.AI.CreateText",
            "Admin.AI.CreateShortDesc",
            "Admin.AI.CreateFullDesc",
            "Admin.AI.CreateMetaTitle",
            "Admin.AI.CreateMetaDesc",
            "Admin.AI.CreateMetaKeywords");

        // "Order.Product(s).OrderedQuantity" is repeated in the input
        builder.AddOrUpdate("Order.Product(s).OrderedQuantity", "Ordered", "Bestellt", "سفارش‌داده‌شده");

        builder.AddOrUpdate("ReturnCase.EntireOrder", "Entire order", "Gesamte Bestellung", "کل سفارش");
        builder.AddOrUpdate("ReturnCase.CertainItems", "Certain items", "Bestimmte Artikel", "اقلام خاص");

        builder.AddOrUpdate("ReturnCase.WithdrawEntireOrder",
            "I would like to withdraw the entire order:",
            "Ich möchte die gesamte Bestellung stornieren:",
            "مایل به انصراف از کل سفارش هستم:");

        builder.AddOrUpdate("ReturnCase.WithdrawItems",
            "I would like to withdraw the following items:",
            "Ich möchte folgende Artikel stornieren:",
            "مایل به انصراف از اقلام زیر هستم:");

        builder.AddOrUpdate("Account.CustomerOrders.ReturnItems", "Return items", "Artikel zurücksenden", "مرجوع کردن اقلام");

        builder.Delete("Admin.ReturnRequests.Updated",
            "Admin.ReturnRequests.Deleted",
            "Admin.Orders.Products.ReturnRequest");

        builder.AddOrUpdate("ReturnRequests.NoItemsSubmitted",
            "Please select the items you wish to return and specify the quantity.",
            "Wählen Sie bitte die Artikel und die Menge aus, die Sie zurücksenden möchten.",
            "لطفاً اقلامی که می‌خواهید مرجوع کنید را انتخاب کرده و تعداد آن‌ها را مشخص نمایید.");

        builder.AddOrUpdate("ReturnRequests.Submit", "Submit return request", "Retourenantrag absenden", "ثبت درخواست مرجوعی");
        builder.AddOrUpdate("ReturnRequests.Submitted", "A return request has been submitted.", "Der Retourenantrag wurde übermittelt.", "یک درخواست مرجوعی ثبت شده است.");

        builder.AddOrUpdate("Admin.ReturnRequests.Fields.ID",
            "ID",
            "ID",
            "شناسه (ID)",
            "ID of the withdrawal or return",
            "ID des Widerrufs bzw. der Retoure",
            "شناسه انصراف یا مرجوعی");

        builder.AddOrUpdate("Admin.Common.SuccessfullySaved",
            "The changes have been saved successfully.",
            "Die Änderungen wurden erfolgreich gespeichert.",
            "تغییرات با موفقیت ذخیره شدند.");

        builder.AddOrUpdate("Admin.Common.SuccessfullyDeleted",
            "The entries were successfully deleted.",
            "Die Einträge wurden erfolgreich gelöscht.",
            "ورودی‌ها با موفقیت حذف شدند.");

        builder.AddOrUpdate("Account.PasswordRecovery.EmailHasBeenSent",
            "If there is an account associated with this email, we have sent a link to reset your password.",
            "Falls ein Konto mit dieser E-Mail-Adresse verknüpft ist, haben wir Ihnen soeben Anweisungen zum Zurücksetzen Ihres Passworts zugeschickt.",
            "اگر حسابی با این ایمیل مرتبط باشد، پیوندی برای بازنشانی رمز عبور برای شما ارسال کرده‌ایم.");

        builder.AddOrUpdate("Admin.Configuration.Settings.Order.ReturnRequestsEnabled")
            .Value("en", "Return requests enabled")
            .Value("fa", "درخواست‌های مرجوعی فعال است");

        builder.Delete("Admin.Configuration.Settings.Order.ReturnRequestsEnabled.Hint",
            "Admin.Configuration.Settings.Order.OrderSettings",
            "Admin.Configuration.Settings.Order.ReturnRequestsDescription2");

        builder.AddOrUpdate("Admin.Configuration.Settings.Order.NumberOfDaysReturnRequestAvailable",
            "Allowed period for return requests (in days)",
            "Erlaubter Zeitraum für Retourenanträge (in Tagen)",
            "مهلت مجاز برای درخواست‌های مرجوعی (به روز)");

        builder.AddOrUpdate("Admin.Configuration.Settings.Order.NumberOfDaysReturnRequestAvailable.Hint",
            "The number of days after order completion during which customers can submit return requests."
            + " This applies to RMA return requests only and not to legal withdrawal. The value 0 means \"unlimited\".",
            "Die Anzahl der Tage nach Abschluss der Bestellung, während der Kunden Retourenanträge einreichen können."
            + " Dies gilt nur für Retourenanträge im Rahmen des RMA-Verfahrens und nicht für den gesetzlichen Widerruf. Der Wert 0 bedeutet \"unbegrenzt\".",
            "تعداد روزهایی پس از تکمیل سفارش که طی آن مشتریان می‌توانند درخواست‌های مرجوعی خود را ثبت کنند."
            + " این مورد فقط برای درخواست‌های مرجوعی (RMA) اعمال می‌شود و شامل حق انصراف قانونی نیست. مقدار ۰ به معنای \"نامحدود\" است.");

        builder.AddOrUpdate("Admin.Configuration.Settings.Order.ReturnRequestReasons.Hint")
            .Value("de", "Eine kommaseparierte Liste von Retourengründen, die der Benutzer auswählen kann, wenn er einen Retourenantrag übermittelt.")
            .Value("fa", "لیستی از دلایل مرجوعی جداشده با کاما که کاربر می‌تواند هنگام ثبت درخواست مرجوعی انتخاب کند.");

        builder.AddOrUpdate("Admin.Configuration.Settings.Order.ReturnRequestActions")
            .Value("en", "Available return actions")
            .Value("fa", "اقدامات مرجوعی در دسترس");

        builder.AddOrUpdate("Admin.Configuration.Settings.Order.ReturnRequestActions.Hint",
            "A comma-separated list of the actions that a customer will be able to select when submitting a return request. This is not used for legal withdrawal.",
            "Eine kommaseparierte Liste von Aktionen, aus denen der Benutzer wählen kann, wenn er einen Retourenantrag übermittelt. Beispiel: \"Ersatz\", \"Gutschein\" usw."
            + " Dies wird nicht für den gesetzlichen Widerruf verwendet.",
            "لیستی از اقداماتی که مشتری می‌تواند هنگام ثبت درخواست مرجوعی انتخاب کند (جدا شده با کاما). این مورد برای انصراف قانونی استفاده نمی‌شود.");

        builder.AddOrUpdate("Admin.Configuration.Settings.Order.ReturnRequestSettings", "Returns", "Retouren", "مرجوعی‌ها");

        builder.AddOrUpdate("Admin.Configuration.Settings.GeneralCommon.CaptchaShowOnTargets.Option.Withdrawal", "Withdrawal", "Widerruf", "انصراف");

        var prefix = "Admin.Configuration.Settings.Resiliency";

        builder.AddOrUpdate($"{prefix}.QueuedMailSending", "Mail Sending", "E-Mail-Versand", "ارسال ایمیل");

        builder.AddOrUpdate($"{prefix}.QueuedMailSendingNotes",
            "Limits how many queued emails can be sent within a specific time window to prevent overload during email bursts.",
            "Begrenzt die Anzahl der E-Mails, die innerhalb eines bestimmten Zeitfensters versendet werden können, um Überlastung bei E-Mail-Spitzen zu verhindern.",
            "تعداد ایمیل‌های در صف که می‌توانند در یک بازه زمانی مشخص ارسال شوند را محدود می‌کند تا از اضافه‌بار در هنگام ارسال حجم زیاد ایمیل جلوگیری شود.");

        builder.AddOrUpdate($"{prefix}.MailSendRateWindow",
            "Time window (hh:mm:ss)",
            "Zeitfenster (hh:mm:ss)",
            "پنجره زمانی (hh:mm:ss)",
            "The time period for measuring the queued mail send rate (e.g., 1 minute).",
            "Der Zeitraum für die Messung der E-Mail-Versandrate (z.B. 1 Minute).",
            "بازه زمانی برای اندازه‌گیری نرخ ارسال ایمیل‌های در صف (مانند ۱ دقیقه).");

        builder.AddOrUpdate($"{prefix}.MailSendRateLimit",
            "Limit",
            "Grenzwert",
            "محدودیت",
            "The maximum number of queued emails that may be sent during the time window. Empty value means there is no limit.",
            "Die maximale Anzahl von E-Mails, die während des Zeitfensters versendet werden dürfen. Ein leerer Wert bedeutet: keine Begrenzung.",
            "حداکثر تعداد ایمیل‌های در صف که ممکن است در این بازه زمانی ارسال شوند. مقدار خالی به معنای بدون محدودیت است.");

        builder.AddOrUpdate("Admin.Rules.FilterDescriptor.AllProductsWithDeliveryTimeInCart",
            "All products with delivery time in cart",
            "Alle Produkte mit Lieferzeit im Warenkorb",
            "تمام محصولات دارای زمان تحویل در سبد خرید");

        builder.AddOrUpdate("Common.Unlimited", "Unlimited", "Unbegrenzt", "نامحدود");

        builder.AddOrUpdate("RewardPoints.PointsForPurchasesInfo",
            "For every {0} net order value, {1} points are awarded.",
            "Für einen Auftragswert von je {0} netto werden {1} Punkte gewährt.",
            "به ازای هر {0} ارزش خالص سفارش، {1} امتیاز تعلق می‌گیرد.");

        builder.AddOrUpdate("Admin.Configuration.Settings.GeneralCommon.GoogleRecaptcha.Info",
            "Manage keys and domains in the <a class='fwm' href='https://www.google.com/recaptcha/admin' target='_blank'>reCAPTCHA Admin Console</a>. v3 runs invisibly with a risk score; optional step-up challenges can be enabled if needed.",
            "Keys und Domains verwalten Sie in der <a class='fwm' href='https://www.google.com/recaptcha/admin' target='_blank'>reCAPTCHA Admin Console</a>. Bei v3 erfolgt die Bewertung unsichtbar per Score; Step-Up-Prüfungen sind optional möglich.",
            "کلیدها و دامنه‌ها را در <a class='fwm' href='https://www.google.com/recaptcha/admin' target='_blank'>کنسول مدیریت reCAPTCHA</a> مدیریت کنید. نسخه ۳ به‌صورت نامرئی با اختصاص امتیاز ریسک اجرا می‌شود؛ چالش‌های اضافی اختیاری نیز در صورت نیاز قابل فعال‌سازی هستند.");

        builder.AddOrUpdate("Enums.VatNumberStatus.ServiceUnavailable",
            "Online checks are currently unavailable",
            "Onlineprüfung derzeit nicht möglich",
            "بررسی‌های آنلاین در حال حاضر در دسترس نیستند");

        builder.AddOrUpdate("Admin.Customers.Customers.Fields.VatNumberStatus",
            "VAT number status",
            "Status der Steuernummer",
            "وضعیت شماره مالیات بر ارزش افزوده (VAT)");

        builder.AddOrUpdate("Admin.Customers.Customers.Fields.VatNumber.MarkAs",
            "Mark as",
            "Markieren
    }
}