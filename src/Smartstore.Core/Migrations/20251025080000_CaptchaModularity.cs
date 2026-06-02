using FluentMigrator;
using Smartstore.Core.Data;
using Smartstore.Core.Data.Migrations;
using Smartstore.Core.Security;
using Smartstore.Data.Migrations;

namespace Smartstore.Core.Migrations
{
    [MigrationVersion("2025-10-25 08:00:00", "Core: CAPTCHA modularity")]
    internal class CaptchaModularity : Migration, ILocaleResourcesProvider, IDataSeeder<SmartDbContext>
    {
        public override void Up()
        {
            // No structural changes
        }

        public override void Down()
        {
            // No structural changes to revert
        }

        public DataSeederStage Stage => DataSeederStage.Early;
        public bool AbortOnFailure => false;

        public async Task SeedAsync(SmartDbContext context, CancellationToken cancelToken = default)
        {
            await context.MigrateLocaleResourcesAsync(MigrateLocaleResources);
            await MigrateCaptchaSettings(context, cancelToken);
        }

        public void MigrateLocaleResources(LocaleResourcesBuilder builder)
        {
            var resPrefix = "Admin.Configuration.Settings.GeneralCommon.";
            builder.Delete(
                resPrefix + "CaptchaShowOnLoginPage",
                resPrefix + "CaptchaShowOnLoginPage.Hint",
                resPrefix + "CaptchaShowOnRegistrationPage",
                resPrefix + "CaptchaShowOnRegistrationPage.Hint",
                resPrefix + "ShowOnPasswordRecoveryPage",
                resPrefix + "ShowOnPasswordRecoveryPage.Hint",
                resPrefix + "CaptchaShowOnContactUsPage",
                resPrefix + "CaptchaShowOnContactUsPage.Hint",
                resPrefix + "CaptchaShowOnEmailWishlistToFriendPage",
                resPrefix + "CaptchaShowOnEmailWishlistToFriendPage.Hint",
                resPrefix + "CaptchaShowOnEmailProductToFriendPage",
                resPrefix + "CaptchaShowOnEmailProductToFriendPage.Hint",
                resPrefix + "CaptchaShowOnAskQuestionPage",
                resPrefix + "CaptchaShowOnAskQuestionPage.Hint",
                resPrefix + "CaptchaShowOnBlogCommentPage",
                resPrefix + "CaptchaShowOnBlogCommentPage.Hint",
                resPrefix + "CaptchaShowOnNewsCommentPage",
                resPrefix + "CaptchaShowOnNewsCommentPage.Hint",
                resPrefix + "CaptchaShowOnForumPage",
                resPrefix + "CaptchaShowOnForumPage.Hint",
                resPrefix + "CaptchaShowOnProductReviewPage",
                resPrefix + "CaptchaShowOnProductReviewPage.Hint",
                resPrefix + "reCaptchaPublicKey",
                resPrefix + "reCaptchaPublicKey.Hint",
                resPrefix + "reCaptchaPrivateKey",
                resPrefix + "reCaptchaPrivateKey.Hint",
                resPrefix + "UseInvisibleReCaptcha",
                resPrefix + "UseInvisibleReCaptcha.Hint",
                resPrefix + "CaptchaEnabledNoKeys",
                "Common.ReCaptchaCheckFailed");

            builder.AddOrUpdate("Common.CaptchaCheckFailed",
       "A CAPTCHA check failed with the error {0}.",
       "Eine CAPTCHA-Prüfung ist fehlgeschlagen. Grund: {0}.",
       "بررسی CAPTCHA با خطای {0} مواجه شد.");

            resPrefix = "Admin.Configuration.Settings.GeneralCommon.CaptchaShowOnTargets.Option.";
            builder.AddOrUpdate(resPrefix + CaptchaSettings.Targets.Login, "Login", "Login", "ورود");
            builder.AddOrUpdate(resPrefix + CaptchaSettings.Targets.Registration, "Registration", "Registrierung", "ثبت‌نام");
            builder.AddOrUpdate(resPrefix + CaptchaSettings.Targets.PasswordRecovery, "Password recovery", "Passwort-Wiederherstellung", "بازیابی رمز عبور");
            builder.AddOrUpdate(resPrefix + CaptchaSettings.Targets.ContactUs, "Contact us", "Kontakt", "تماس با ما");
            builder.AddOrUpdate(resPrefix + CaptchaSettings.Targets.ShareWishlist, "Share wishlist", "Wunschliste teilen", "اشتراک‌گذاری لیست علاقه‌مندی‌ها");
            builder.AddOrUpdate(resPrefix + CaptchaSettings.Targets.ShareProduct, "Share product", "Produkt teilen", "اشتراک‌گذاری محصول");
            builder.AddOrUpdate(resPrefix + CaptchaSettings.Targets.ProductInquiry, "Product inquiry", "Produktanfrage", "استعلام محصول");
            builder.AddOrUpdate(resPrefix + CaptchaSettings.Targets.BlogComment, "Blog comment", "Blog-Kommentar", "نظر وبلاگ");
            builder.AddOrUpdate(resPrefix + CaptchaSettings.Targets.NewsComment, "News comment", "News-Kommentar", "نظر اخبار");
            builder.AddOrUpdate(resPrefix + CaptchaSettings.Targets.Forum, "Forum", "Forum", "انجمن");
            builder.AddOrUpdate(resPrefix + CaptchaSettings.Targets.ProductReview, "Product review", "Produkt-Bewertung", "نقد و بررسی محصول");

            // Fix Captcha --> CAPTCHA
            builder.AddOrUpdate("Common.WrongInvisibleCaptcha",
                "CAPTCHA failed. Please try again.",
                "CAPTCHA ist fehlgeschlagen. Bitte versuchen Sie es erneut.",
                "تأیید CAPTCHA انجام نشد. لطفاً دوباره تلاش کنید.");

            builder.AddOrUpdate("Admin.Configuration.Settings.GeneralCommon.CaptchaEnabled",
                "CAPTCHA enabled",
                "CAPTCHA aktivieren",
                "فعال‌سازی CAPTCHA",
                "Enables global bot protection via CAPTCHA.",
                "Aktiviert den globalen Bot-Schutz durch CAPTCHA.",
                "محافظت سراسری در برابر ربات‌ها از طریق CAPTCHA را فعال می‌کند.");

            builder.AddOrUpdate("Admin.Configuration.Settings.GeneralCommon.CaptchaShowOnTargets",
                "Enable CAPTCHA on the following pages",
                "CAPTCHA auf folgenden Seiten aktivieren",
                "فعال‌سازی CAPTCHA در صفحات زیر",
                "Select the pages on which CAPTCHA should be enabled.",
                "Wählen Sie die Seiten aus, auf denen CAPTCHA aktiviert werden soll.",
                "صفحاتی را که CAPTCHA باید در آن‌ها فعال شود، انتخاب کنید.");

            builder.AddOrUpdate("Admin.Configuration.Settings.GeneralCommon.ProviderSystemName", "Provider", "Anbieter", "ارائه‌دهنده");

            builder.AddOrUpdate("Admin.Configuration.Settings.General.Common.Captcha.Hint",
                "A CAPTCHA is an automated test that distinguishes real users from bots, e.g., via a brief challenge or an invisible risk check. It protects forms and logins from spam and abuse.",
                "Ein CAPTCHA ist ein automatisierter Test, der echte Nutzer von Bots unterscheidet, z. B. durch eine kurze Aufgabe oder eine unsichtbare Risikoprüfung. Es schützt Formulare und Logins vor Spam und Missbrauch.",
                "کپچا (CAPTCHA) یک تست خودکار است که کاربران واقعی را از ربات‌ها متمایز می‌کند، مثلاً از طریق یک چالش کوتاه یا یک بررسی خطر نامرئی. این سیستم از فرم‌ها و ورود به سیستم در برابر هرزنامه و سوءاستفاده محافظت می‌کند.");

            resPrefix = "Admin.Configuration.Settings.GeneralCommon.GoogleRecaptcha.";
            builder.AddOrUpdate(resPrefix + "Info",
                "Manage keys and domains in the <a class='fwm' href='https://www.google.com/recaptcha/admin'>reCAPTCHA Admin Console</a>. v3 runs invisibly with a risk score; optional step-up challenges can be enabled if needed.",
                "Keys und Domains verwalten Sie in der <a class='fwm' href='https://www.google.com/recaptcha/admin'>reCAPTCHA Admin Console</a>. Bei v3 erfolgt die Bewertung unsichtbar per Score; Step-Up-Prüfungen sind optional möglich.",
                "مدیریت کلیدها و دامنه‌ها در <a class='fwm' href='https://www.google.com/recaptcha/admin'>کنسول مدیریت reCAPTCHA</a>. نسخه ۳ به صورت نامرئی و با یک امتیاز ریسک کار می‌کند؛ چالش‌های اضافی انتخابی در صورت نیاز قابل فعال‌سازی هستند.");

            builder.AddOrUpdate(resPrefix + "WidgetUrl",
                "Widget URL",
                "Widget-URL",
                "آدرس ویجت",
                "Client script URL",
                "Client-Skript-URL",
                "آدرس (URL) اسکریپت کلاینت");

            builder.AddOrUpdate(resPrefix + "VerifyUrl",
                "Verify URL",
                "Verify-URL",
                "آدرس تأیید",
                "Server endpoint for token verification",
                "Server-Endpoint zur Token-Prüfung",
                "نقطه پایانی (Endpoint) سرور برای تأیید توکن");

            builder.AddOrUpdate(resPrefix + "SiteKey",
                "Public key",
                "Öffentlicher Schlüssel",
                "کلید عمومی",
                "Public key for the frontend.",
                "Öffentlicher Schlüssel für das Frontend.",
                "کلید عمومی برای بخش کاربری (Frontend).");

            builder.AddOrUpdate(resPrefix + "SecretKey",
                "Private key",
                "Privater Schlüssel",
                "کلید خصوصی",
                "Private server key; never on the client.",
                "Privater Server-Schlüssel; niemals clientseitig.",
                "کلید خصوصی سرور؛ هرگز نباید در کلاینت قرار گیرد.");

            builder.AddOrUpdate(resPrefix + "ScoreThreshold",
                "Score Threshold",
                "Score-Schwelle",
                "آستانه امتیاز",
                "Minimum score (0–1) to accept; higher = stricter. Default: 0.5.",
                "Mindest-Score (0–1) für 'Bestanden'; höher = strenger. Standard: 0,5.",
                "حداقل امتیاز (۰ تا ۱) برای تأیید؛ بالاتر = سخت‌گیرانه‌تر. پیش‌فرض: ۰.۵.");

            builder.AddOrUpdate(resPrefix + "Version", "Version", "Version", "نسخه");

            builder.AddOrUpdate(resPrefix + "UseDarkTheme", "Use dark theme", "Dunkles Design verwenden", "استفاده از قالب تاریک");

            builder.AddOrUpdate(resPrefix + "Size", "Size", "Größe", "اندازه");
            builder.AddOrUpdate(resPrefix + "Size.Normal", "Normal", "Normal", "عادی");
            builder.AddOrUpdate(resPrefix + "Size.Compact", "Compact", "Kompakt", "فشرده");
            builder.AddOrUpdate(resPrefix + "Size.Invisible", "Invisible", "Unsichtbar", "نامرئی");

            builder.AddOrUpdate(resPrefix + "BadgePosition",
                "Badge position",
                "Badge Position",
                "موقعیت نشان",
                "When the badge is hidden, a notice with links to Google's privacy policy and terms of service will be displayed in plain sight.",
                "Wenn das Badge verborgen wird, wird in Sichtweite ein Hinweis mit Links zur Google-Datenschutzrichtlinie und zu den Nutzungsbedingungen angezeigt.",
                "هنگامی که نشان پنهان است، اعلانی با پیوند به سیاست حریم خصوصی و شرایط خدمات گوگل به صورت واضح نمایش داده می‌شود.");

            builder.AddOrUpdate(resPrefix + "BadgePosition.BottomLeft", "Floating at bottom left", "Unten links schwebend", "شناور در پایین سمت چپ");
            builder.AddOrUpdate(resPrefix + "BadgePosition.BottomRight", "Floating at bottom right", "Unten rechts schwebend", "شناور در پایین سمت راست");
            builder.AddOrUpdate(resPrefix + "BadgePosition.Inline", "Inline within the form flow", "Inline innerhalb des Formulars", "درون‌خطی در جریان فرم");
            builder.AddOrUpdate(resPrefix + "BadgePosition.Hide", "Hide badge (but show disclaimer)", "Badge verstecken (aber Richtlinien anzeigen)", "پنهان کردن نشان (اما نمایش سلب مسئولیت)");

            builder.AddOrUpdate(resPrefix + "HiddenBadgeLegalNotice",
                "<div class=\"captcha-disclaimer grecaptcha-disclaimer alert alert-info\">This site is protected by <strong>reCAPTCHA</strong> and the Google <a href=\"https://policies.google.com/privacy\" rel=\"noopener\" target=\"_blank\">Privacy Policy</a> and <a href=\"https://policies.google.com/terms\" rel=\"noopener\" target=\"_blank\">Terms of Service</a> apply.</div>",
                "<div class=\"captcha-disclaimer grecaptcha-disclaimer alert alert-info\">Diese Website ist durch  <strong>reCAPTCHA</strong> und die Google <a href=\"https://policies.google.com/privacy\" rel=\"noopener\" target=\"_blank\">Datenschutzrichtlinie</a> und den <a href=\"https://policies.google.com/terms\" rel=\"noopener\" target=\"_blank\">Nutzungsbedingungen</a> von Google geschützt.</div>",
                "<div class=\"captcha-disclaimer grecaptcha-disclaimer alert alert-info\">این سایت توسط <strong>reCAPTCHA</strong> محافظت می‌شود و <a href=\"https://policies.google.com/privacy\" rel=\"noopener\" target=\"_blank\">سیاست حریم خصوصی</a> و <a href=\"https://policies.google.com/terms\" rel=\"noopener\" target=\"_blank\">شرایط خدمات</a> گوگل اعمال می‌شود.</div>");

        }

        private async Task MigrateCaptchaSettings(SmartDbContext db, CancellationToken cancelToken = default)
        {
            var startIndex = nameof(CaptchaSettings).Length + 1;
            var showOnSettingName = $"{nameof(CaptchaSettings)}.{nameof(CaptchaSettings.ShowOn)}".ToLower();

            // Load all existing CaptchaSettings
            var existingCaptchaSettings = await db.Settings
                .Where(x => x.Name.StartsWith(nameof(CaptchaSettings) + "."))
                .OrderBy(x => x.StoreId)
                .ToListAsync(cancelToken);

            var legacySettingNames = CaptchaSettings.Targets.GetLegacySettingNames();
            var globalShow = CaptchaSettings.Targets.All.ToDictionary(k => $"0:{k}", v => false, StringComparer.OrdinalIgnoreCase);

            await db.MigrateSettingsAsync(builder =>
            {
                foreach (var group in existingCaptchaSettings.GroupBy(x => x.StoreId))
                {
                    var storeId = group.Key;

                    foreach (var setting in group)
                    {
                        var newSettingName = "GoogleRecaptchaSettings.";
                        var newValue = setting.Value;

                        if (setting.Name.EndsWithNoCase("ReCaptchaPublicKey"))
                        {
                            // CaptchaSettings.ReCaptchaPublicKey --> GoogleRecaptchaSettings.SiteKey
                            newSettingName += nameof(GoogleRecaptchaSettings.SiteKey);
                            db.Settings.Remove(setting);
                        }
                        else if (setting.Name.EndsWithNoCase("ReCaptchaPrivateKey"))
                        {
                            // CaptchaSettings.ReCaptchaPrivateKey --> GoogleRecaptchaSettings.SecretKey
                            newSettingName += nameof(GoogleRecaptchaSettings.SecretKey);
                            db.Settings.Remove(setting);
                        }
                        else if (setting.Name.EndsWithNoCase("UseInvisibleReCaptcha"))
                        {
                            // CaptchaSettings.UseInvisibleReCaptcha --> GoogleRecaptchaSettings.Size ("invisible")
                            newSettingName += nameof(GoogleRecaptchaSettings.Size);
                            newValue = setting.Value.ToBool() ? "invisible" : "normal";
                            db.Settings.Remove(setting);
                        }
                        else if (setting.Name.ContainsNoCase(".ShowOn"))
                        {
                            var target = legacySettingNames.Get(setting.Name[startIndex..]);
                            if (target.HasValue())
                            {
                                var show = setting.Value.ToBool();
                                globalShow[$"{storeId}:{target}"] = show;
                                db.Settings.Remove(setting);
                            }

                            continue;
                        }
                        else
                        {
                            continue;
                        }

                        builder.Add(newSettingName.ToLower(), newValue, storeId);
                    }

                    // Multi bool CaptchaSettings.ShowOnXyz --> Single CaptchaSettings.ShowOn array
                    var showOn = new List<string>(11);
                    foreach (var target in CaptchaSettings.Targets.All)
                    {
                        var key = $"{storeId}:{target}";
                        if (globalShow.TryGetValue(key, out var showTarget) || (storeId > 0 && globalShow.TryGetValue($"0:{target}", out showTarget)))
                        {
                            if (showTarget) showOn.Add(target);
                        }
                    }

                    if (showOn.Count > 0)
                    {
                        builder.Add(showOnSettingName, showOn.Convert<string>(), storeId);
                    }
                }
            });
        }
    }
}
