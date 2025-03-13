using Smartstore.Core.Common.Configuration;
using Smartstore.Core.Configuration;
using Smartstore.Core.Security;
using Smartstore.Data.Migrations;

namespace Smartstore.Core.Data.Migrations
{
    public class SmartDbContextDataSeeder : IDataSeeder<SmartDbContext>
    {
        public DataSeederStage Stage => DataSeederStage.Early;
        public bool AbortOnFailure => false;

        public async Task SeedAsync(SmartDbContext context, CancellationToken cancelToken = default)
        {
            await context.MigrateLocaleResourcesAsync(MigrateLocaleResources);
            await MigrateSettingsAsync(context, cancelToken);
        }

        public async Task MigrateSettingsAsync(SmartDbContext context, CancellationToken cancelToken = default)
        {
            await SettingFactory.SaveSettingsAsync(context, new PerformanceSettings(), false);
            await SettingFactory.SaveSettingsAsync(context, new ResiliencySettings(), false);
        }

        public void MigrateLocaleResources(LocaleResourcesBuilder builder)
        {
            builder.AddOrUpdate("Admin.Configuration.Settings.Search.CommonFacet.Sorting",
                "Sorting",
                "Sortierung",
                "Specifies the sorting of the search filters.",
                "Legt die Sortierung der Suchfilter fest.");

            builder.AddOrUpdate("Enums.FacetSorting.ValueAsc", "Value/ID: lowest first", "Wert/ID: Niedrigste zuerst");

            builder.AddOrUpdate("Admin.Common.ExportToPdf.TooManyItems",
                "Too many objects! A maximum of {0} objects can be converted. Please reduce the number of selected data records ({1}) or increase the limit in the PDF settings.",
                "Zu viele Objekte! Es k�nnen maximal {0} Objekte konvertiert werden. Bitte reduzieren Sie die Anzahl der ausgew�hlten Datens�tze ({1}) oder erh�hen Sie das Limit in den PDF-Einstellungen.");

            builder.AddOrUpdate("Admin.Configuration.Settings.GeneralCommon.MaxItemsToPrint",
                "Maximum number of objects to print",
                "Maximale Anzahl zu druckender Objekte",
                "Specifies the maximum number of objects to be printed, above which an error message is issued. The default value is 500 and should not be set too high so that the process does not take too long.",
                "Legt die maximale Anzahl der zu druckenden Objekte fest, bei deren �berschreitung eine Fehlermeldung ausgegeben wird. Der Standardwert ist 500 und sollte nicht zu hoch gew�hlt werden, damit der Vorgang nicht zu lange dauert.");

            builder.AddOrUpdate("Admin.Configuration.Settings.Shipping.CalculateShippingAtCheckout",
                "Calculate shipping costs during checkout",
                "Versandkosten w�hrend des Checkouts berechnen",
                "Specifies whether shipping costs are displayed on the shopping cart page as long as the customer has not yet entered a shipping address. If activated, a note appears instead that the calculation will only take place at checkout.",
                "Legt fest, ob Versandkosten auf der Warenkorbseite angezeigt werden, solange der Kunde noch keine Lieferanschrift eingegeben hat. Wenn aktiviert, erscheint stattdessen ein Hinweis, dass die Berechnung erst beim Checkout erfolgt.");

            builder.AddOrUpdate("Common.CartRules", "Cart rules", "Warenkorbregeln");
            builder.AddOrUpdate("Common.CustomerRules", "Customer rules", "Kundenregeln");
            builder.AddOrUpdate("Common.ProductRules", "Product rules", "Produktregeln");

            builder.AddOrUpdate("Admin.Common.RestartHint",
                "Changes to the following settings only take effect after the application has been restarted.",
                "�nderungen an den folgenden Einstellungen werden erst nach einem Neustart der Anwendung wirksam.");

            builder.AddOrUpdate("Admin.Configuration.Settings.RewardPoints.RoundDownPointsForPurchasedAmount",
                "Round down the amount of points for a purchase",
                "Betrag bei Punkten f�r einen Einkauf abrunden",
                "Specifies whether to round down the amount when calculating the reward points awarded for a product purchase.",
                "Legt fest, ob der Betrag bei der Berechnung der Bonuspunkte, die f�r den Kauf eines Produkts gew�hrt werden, abgerundet werden soll.");

            builder.AddOrUpdate("Admin.Configuration.Settings.CustomerUser.HideMyAccountOrders",
                "Hide orders in the \"My account\" area",
                "Bestellungen im Bereich \"Mein Konto\" ausblenden");

            builder.AddOrUpdate("Admin.Rules.FilterDescriptor.VariantInCart", "Product with SKU in cart", "Produkt mit SKU im Warenkorb");

            builder.AddOrUpdate("Admin.RecurringPayments.History")
                .Value("de", "Historie");
            builder.AddOrUpdate("Admin.RecurringPayments.Fields.CyclesRemaining")
                .Value("de", "Verbleibende Zahlungen");
            builder.AddOrUpdate("Admin.RecurringPayments.Fields.CyclesRemaining.Hint")
                .Value("de", "Die Anzahl der verbleibenden Zahlungen");

            builder.AddOrUpdate("Admin.RecurringPayments.List.RemainingCycles",
                "Remaining payments",
                "Verbleibende Zahlungen",
                "Filter list by remaining payments.",
                "Liste nach verbleibenden Zahlungen filtern.");

            // Frontend renaming: "Wiederkehrende Zahlung" -> "Regelm��ige Lieferung".
            builder.AddOrUpdate("Account.CustomerOrders.RecurringOrders.Cancel", "Cancel repeat delivery", "Regelm��ige Lieferung abbrechen");
            builder.AddOrUpdate("Account.CustomerOrders.RecurringOrders", "Repeat deliveries", "Regelm��ige Lieferungen");
            builder.AddOrUpdate("Account.CustomerOrders.RecurringOrders.TotalCycles", "Total deliveries", "Lieferungen insgesamt");
            builder.AddOrUpdate("ShoppingCart.RecurringPeriod", "[Repeat deliveries every {0} {1}]", "[Regelm��ige Lieferung alle {0} {1}]");

            builder.AddOrUpdate("Account.CustomerOrders.RecurringOrders.CancelDelivery",
                "Would you like to cancel the repeat delivery?",
                "M�chten Sie die regelm��ige Lieferung abbrechen?");

            builder.AddOrUpdate("Account.CustomerOrders.RecurringOrders.SuccessfullyCanceled",
                "The repeat delivery was successfully canceled.",
                "Die regelm��ige Lieferung wurde erfolgreich abgebrochen.");

            builder.Delete(
                "Admin.RecurringPayments.History.OrderStatus",
                "Admin.RecurringPayments.History.PaymentStatus",
                "Admin.RecurringPayments.History.ShippingStatus",
                "Admin.Orders.Products.RecurringPeriod",
                "Account.CustomerOrders.RecurringOrders.ViewInitialOrder");

            builder.AddOrUpdate("Admin.Catalog.Products.RecycleBin.DeleteProductsResult",
                "{0} of {1} products have been permanently deleted.",
                "Es wurden {0} von {1} Produkten endg�ltig gel�scht.");

            builder.AddOrUpdate("Admin.Catalog.Products.RecycleBin.DeletedAndSkippedProductsResult",
                "{0} of {1} products have been permanently deleted. {2} Products were skipped as they are assigned to orders and cannot be permanently deleted.",
                "{0} von {1} Produkten wurden endg�ltig gel�scht. {2} Produkte wurden �bersprungen, da sie Auftr�gen zugeordnet sind und nicht permanent gel�scht werden k�nnen.");

            builder.AddOrUpdate("Order.CannotCompleteUnpaidOrder", 
                "An unpaid order cannot be completed.",
                "Ein unbezahlter Auftrag kann nicht abgeschlossen werden.");

            builder.Delete(
                "Admin.Orders.List.StartDate",
                "Admin.Orders.List.StartDate.Hint",
                "Admin.Orders.List.EndDate",
                "Admin.Orders.List.EndDate.Hint",
                "Admin.Customers.Reports.BestBy.StartDate",
                "Admin.Customers.Reports.BestBy.StartDate.Hint",
                "Admin.Customers.Reports.BestBy.EndDate",
                "Admin.Customers.Reports.BestBy.EndDate.Hint",
                "Admin.SalesReport.Bestsellers.StartDate",
                "Admin.SalesReport.Bestsellers.StartDate.Hint",
                "Admin.SalesReport.Bestsellers.EndDate",
                "Admin.SalesReport.Bestsellers.EndDate.Hint",
                "Admin.SalesReport.NeverSold.StartDate",
                "Admin.SalesReport.NeverSold.StartDate.Hint",
                "Admin.SalesReport.NeverSold.EndDate",
                "Admin.SalesReport.NeverSold.EndDate.Hint",
                "Admin.Orders.Shipments.List.StartDate",
                "Admin.Orders.Shipments.List.StartDate.Hint",
                "Admin.Orders.Shipments.List.EndDate",
                "Admin.Orders.Shipments.List.EndDate.Hint",
                "Admin.Common.Search.StartDate",
                "Admin.Common.Search.StartDate.Hint",
                "Admin.Common.Search.EndDate",
                "Admin.Common.Search.EndDate.Hint",
                "Admin.System.QueuedEmails.List.StartDate",
                "Admin.System.QueuedEmails.List.StartDate.Hint",
                "Admin.System.QueuedEmails.List.EndDate",
                "Admin.System.QueuedEmails.List.EndDate.Hint");

            builder.AddOrUpdate("Admin.Media.Editing.Align", "Align", "Ausrichten");
            
            builder.AddOrUpdate("Admin.Media.Editing.AlignTop", "Top", "Oben");
            builder.AddOrUpdate("Admin.Media.Editing.AlignMiddle", "Center", "Mitte");
            builder.AddOrUpdate("Admin.Media.Editing.AlignBottom", "Bottom", "Unten");


            #region Performance settings

            var prefix = "Admin.Configuration.Settings.Performance";

            builder.AddOrUpdate($"{prefix}", "Performance", "Leistung");
            builder.AddOrUpdate($"{prefix}.Resiliency", "Overload protection", "�berlastungsschutz");
            builder.AddOrUpdate($"{prefix}.Cache", "Cache", "Cache");

            builder.AddOrUpdate($"{prefix}.Hint",
                "For technically experienced users only. Pay attention to the CPU and memory usage when changing these settings.",
                "Nur f�r technisch erfahrene Benutzer. Achten Sie auf die CPU- und Speicherauslastung, wenn Sie diese Einstellungen �ndern.");

            builder.AddOrUpdate($"{prefix}.CacheSegmentSize",
                "Cache segment size", 
                "Cache Segment Gr��e",
                "The number of entries in a single cache segment when greedy loading is disabled. The larger the catalog, the smaller this value should be. We recommend segment size of 500 for catalogs with less than 100.000 items.",
                "Die Anzahl der Eintr�ge in einem einzelnen Cache-Segment, wenn Greedy Loading deaktiviert ist. Je gr��er der Katalog ist, desto kleiner sollte dieser Wert sein. Wir empfehlen eine Segmentgr��e von 500 f�r Kataloge mit weniger als 100.000 Eintr�gen.");

            builder.AddOrUpdate($"{prefix}.AlwaysPrefetchTranslations",
                "Always prefetch translations",
                "�bersetzungen immer vorladen (Prefetch)",
                "By default, only Instant Search prefetches translations. All other product listings work against the segmented cache. For very large multilingual catalogs (> 500,000), enabling this can improve query performance and reduce resource usage.",
                "Standardm��ig werden nur bei der Sofortsuche �bersetzungen vorgeladen. Alle anderen Produktauflistungen arbeiten mit dem segmentierten Cache. Bei sehr gro�en mehrsprachigen Katalogen (> 500.000) kann die Aktivierung dieser Option die Abfrageleistung verbessern und die Ressourcennutzung verringern.");

            builder.AddOrUpdate($"{prefix}.AlwaysPrefetchUrlSlugs",
                "Always prefetch URL slugs",
                "URL Slugs immer vorladen  (Prefetch)",
                "By default, only Instant Search prefetches URL slugs. All other product listings work against the segmented cache. For very large multilingual catalogs (> 500,000), enabling this can improve query performance and reduce resource usage.",
                "Standardm��ig werden nur bei der Sofortsuche URL slugs vorgeladen. Alle anderen Produktauflistungen arbeiten mit dem segmentierten Cache. Bei sehr gro�en mehrsprachigen Katalogen (> 500.000) kann die Aktivierung dieser Option die Abfrageleistung verbessern und die Ressourcennutzung verringern.");

            builder.AddOrUpdate($"{prefix}.MaxUnavailableAttributeCombinations",
                "Max. unavailable attribute combinations",
                "Max. nicht verf�gbare Attributkombinationen",
                "Maximum number of attribute combinations that will be loaded and parsed to make them unavailable for selection on the product detail page.",
                "Maximale Anzahl von Attributkombinationen, die geladen und analysiert werden, um nicht verf�gbare Kombinationen zu ermitteln.");

            builder.AddOrUpdate($"{prefix}.MediaDupeDetectorMaxCacheSize",
                "Media Duplicate Detector max. cache size",
                "Max. Cache-Gr��e f�r Medien-Duplikat-Detektor",
                "Maximum number of MediaFile entities to cache for duplicate file detection. If a media folder contains more files, no caching is done for scalability reasons and the MediaFile entities are loaded directly from the database.",
                "Maximale Anzahl der MediaFile-Entit�ten, die f�r die Duplikat-Erkennung zwischengespeichert werden. Enth�lt ein Medienordner mehr Dateien, erfolgt aus Gr�nden der Skalierbarkeit keine Zwischenspeicherung und die MediaFile-Entit�ten werden direkt aus der Datenbank geladen.");

            prefix = "Admin.Configuration.Settings.Resiliency";

            builder.AddOrUpdate($"{prefix}.Description",
                @"Overload protection is used to keep server resources under control, prevent latencies from getting out of hand and keep the system performant and available 
in the event of increased traffic (e.g. due to unidentifiable ""Bad Bots"", peaks, sales events, sudden high visitor numbers).
Limits only apply to guest accounts and bots, not to registered users.",
                @"�berlastungsschutz dient dazu, die Server-Ressourcen unter Kontrolle zu halten, Latenzen nicht ausufern zu lassen und im Falle von erh�htem Ansturm 
(z.B. durch nicht identifizierbare ""Bad-Bots"", Peaks, Sales Events, pl�tzlich hohe Nutzerzahlen) das System performant und verf�gbar zu halten.
Limits gelten nur f�r Gastkonten und Bots, nicht f�r registrierte User.");

            builder.AddOrUpdate($"{prefix}.LongTraffic", "Traffic limit", "Besucherlimit");
            builder.AddOrUpdate($"{prefix}.LongTrafficNotes",
                "Configuration of the long traffic window. Use these settings to monitor traffic restrictions over a longer period of time, such as a minute or longer.",
                "Konfiguration des langen Zeitfensters. Verwenden Sie diese Einstellungen, um Limits �ber einen l�ngeren Zeitraum zu �berwachen, z.B. eine Minute oder l�nger.");

            builder.AddOrUpdate($"{prefix}.PeakTraffic", "Peak", "Lastspitzen");
            builder.AddOrUpdate($"{prefix}.PeakTrafficNotes",
                "The peak traffic window defines the shorter period used for detecting sudden traffic spikes. These settings are useful for reacting to bursts of traffic that occur in a matter of seconds.",
                "Der k�rzere Zeitraum, der f�r die Erkennung pl�tzlicher Lastspitzen verwendet wird. Diese Einstellungen sind n�tzlich, um auf Lastspitzen zu reagieren, die innerhalb weniger Sekunden auftreten.");

            builder.AddOrUpdate($"{prefix}.TrafficTimeWindow",
                "Time window (hh:mm:ss)",
                "Zeitfenster (hh:mm:ss)",
                "The duration of the traffic window, which defines the period of time during which sustained traffic is measured.",
                "Die Dauer des Zeitfensters, das den Zeitraum definiert, in dem der anhaltende Traffic gemessen wird.");

            builder.AddOrUpdate($"{prefix}.TrafficLimitGuest",
                "Guest limit",
                "G�ste-Grenzwert",
                "The maximum number of requests allowed from guest users within the duration of the defined time window. Empty value means there is no limit applied for guest users.",
                "Die maximale Anzahl von Gastbenutzern innerhalb des festgelegten Zeitfensters. Ein leerer Wert bedeutet: keine Begrenzung.");

            builder.AddOrUpdate($"{prefix}.TrafficLimitBot",
                "Bot limit",
                "Bot-Grenzwert",
                "The maximum number of requests allowed from bots within the duration of the defined time window. Empty value means there is no limit applied for bots.",
                "Die maximale Anzahl von Bots innerhalb des festgelegten Zeitfensters. Ein leerer Wert bedeutet: keine Begrenzung.");

            builder.AddOrUpdate($"{prefix}.TrafficLimitGlobal",
                "Global limit",
                "Globaler Grenzwert",
                @"The global traffic limit for both guests and bots together. This limit applies to the combined traffic from guests and bots. 
It ensures that the overall system load remains within acceptable thresholds, regardless of the distribution of requests among specific user types.  
Unlike guest or bot limiters, this global limit acts as a safeguard for the entire system. If the cumulative requests from both types exceed this limit 
within the observation window, additional requests may be denied, even if type-specific limits have not been reached. An empty value means that there is no global limit.",
                @"Das globale Limit f�r G�ste und Bots zusammen. Dieses Limit gilt f�r den kombinierten Traffic von G�sten und Bots. 
Es stellt sicher, dass die Gesamtsystemlast innerhalb akzeptabler Schwellenwerte bleibt, unabh�ngig von der Verteilung der Anfragen auf bestimmte Benutzertypen. 
Im Gegensatz zu Gast- oder Bot-Limitern dient dieses globale Limit als Schutz f�r das gesamte System. 
Wenn die kumulierten Anfragen beider Typen dieses Limit innerhalb des Beobachtungsfensters �berschreiten, werden weitere Anfragen abgelehnt, 
auch wenn die typspezifischen Limits nicht erreicht wurden. Ein leerer Wert bedeutet: keine Begrenzung.");

            builder.AddOrUpdate($"{prefix}.EnableOverloadProtection",
                "Enable overload protection",
                "�berlastungsschutz aktivieren",
                "When enabled, the system applies the defined traffic limits and overload protection policies. If disabled, no traffic limits are enforced.",
                "Wendet die festgelegten Richtlinien an. Wenn diese Option deaktiviert ist, werden keine Begrenzungen erzwungen.");

            builder.AddOrUpdate($"{prefix}.ForbidNewGuestsIfSubRequest",
                "If sub request, forbid \"new\" guests",
                "Wenn Sub-Request, \"neue\" G�ste blockieren",
                @"Forbids ""new"" guest users if the request is a sub/secondary request, e.g., an AJAX request, POST, script, media file, etc. This setting can be used to restrict 
the creation of new guest sessions on successive (secondary) resource requests. A ""bad bot"" that does not accept cookies is difficult to identify 
as a bot and may create a new guest session with each (sub)-request, especially if it varies its client IP address and user agent string with each request. 
If enabled, new guests will be blocked under these circumstances.",
                @"Blockiert ""neue"" Gastbenutzer, wenn es sich bei der Anforderung um eine untergeordnete/sekund�re Anforderung handelt, z. B. AJAX, POST, Skript, Mediendatei usw. 
Diese Einstellung kann verwendet werden, um die Erstellung neuer Gastsitzungen bei aufeinander folgenden (sekund�ren) Ressourcenanfragen einzuschr�nken. 
Ein ""Bad Bot"", der keine Cookies akzeptiert, ist schwer als Bot zu erkennen und kann bei jeder (Unter-)Anfrage eine neue Gastsitzung erzeugen, 
insbesondere wenn er seine Client-IP-Adresse und den User-Agent-String bei jeder Anfrage �ndert. 
Wenn diese Option aktiviert ist, werden neue G�ste unter diesen Umst�nden blockiert.");

            #endregion

            builder.AddOrUpdate("Tax.LegalInfoShort3", "Prices {0}, {1}", "Preise {0}, {1}");

            builder.AddOrUpdate("Smartstore.AI.Prompts.Role.ImageAnalyzerAssistant",
                "Be an image analyzer assistant.",
                "Sei ein Assistent f�r Bildanalyse.");

            builder.AddOrUpdate("Smartstore.AI.Prompts.PleaseContinue",
                "Please continue exactly at the marked point without repeating the previous text.",
                "Bitte fahre genau an der markierten Stelle fort, ohne den bisherigen Text zu wiederholen.");

            builder.AddOrUpdate("Smartstore.AI.Prompts.ContinueHere", "[Continue here]", "[Fortsetzung hier]");

            builder.AddOrUpdate("Smartstore.AI.Prompts.Suggestions.GeneralPrompt",
                "Separate the suggestions with the � character (paragraph mark). Do not use numbering. Do not repeat suggestions.",
                "Trenne die Vorschl�ge durch das � Zeichen (Absatzmarke). Verwende keine Nummerierungen. Wiederhole keine Vorschl�ge.");
            
            builder.AddOrUpdate("Smartstore.AI.Prompts.Suggestions.CharLimit",
                "Each suggestion should have a maximum of {0} characters.",
                "Jeder Vorschlag soll maximal {0} Zeichen haben.");

            builder.AddOrUpdate("Smartstore.AI.Prompts.CharLimit",
                "The text can contain a maximum of {0} characters.",
                "Der Text darf maximal {0} Zeichen enthalten.");

            builder.AddOrUpdate("Smartstore.AI.Prompts.CharWordLimit",
                "The text may contain no more than {0} characters and no more than {1} words.",
                "Der Text darf nicht mehr als {0} Zeichen und nicht mehr als {1} W�rter enthalten.");

            builder.AddOrUpdate("Admin.AI.Suggestions.DefaultPrompt",
                "Make {0} suggestions about the topic '{1}'.", 
                "Mache {0} Vorschl�ge zum Thema '{1}'.");

            builder.AddOrUpdate("Smartstore.AI.Prompts.DontUseQuotes",
                "Do not enclose the text in quotation marks or other characters.",
                "Der Text darf nicht in Anf�hrungszeichen oder andere Zeichen eingeschlossen werden.");

            builder.AddOrUpdate("Admin.Orders.CompleteUnpaidOrder",
                "The order has a payment status of <strong>{0}</strong>. Do you still want to set it to complete?",
                "Der Auftrag hat den Zahlungsstatus <strong>{0}</strong>. M�chten Sie ihn trotzdem auf komplett setzen?");

            builder.AddOrUpdate("Products.Sorting.Featured")
                .Value("en", "Recommendation");

            builder.AddOrUpdate("Admin.Configuration.Settings.Search.UseFeaturedSorting",
                "Sort by recommendation",
                "Nach Empfehlung sortieren",
                "Specifies whether sorting by recommendations is offered instead of sorting by best results. If activated, the products are sorted in the order specified for them.",
                "Legt fest, ob die Sortierung nach Empfehlungen anstelle der Sortierung nach besten Ergebnissen angeboten wird. Wenn aktiviert, werden die Produkte in der f�r sie angegebenen Reihenfolge sortiert.");

            builder.AddOrUpdate("Admin.Catalog.Products.Fields.DisplayOrder",
                "Display order",
                "Reihenfolge",
                "Specifies the order in which associated products of a grouped product are displayed. In addition, this setting determines the order of hits in the search, if sort by recommendation is enabled in the search settings.",
                "Legt die Reihenfolge fest, in der verkn�pfte Produkte eines Gruppenproduktes angezeigt werden. Zus�tzlich legt diese Einstellung die Reihenfolge der Treffer bei der Suche fest, sofern in den Sucheinstellungen die Sortierung nach Empfehlung aktiviert ist.");

            builder.AddOrUpdate("Admin.Configuration.Settings.Shipping.FreeShippingCountryIds",
                "Countries with free shipping",
                "L�nder mit kostenlosem Versand",
                "Specifies the shipping countries for which free shipping is enabled. Free shipping is enabled for all countries if none are specified here (default).",
                "Legt die Lieferl�nder fest, f�r die der kostenlose Versand aktiviert ist. Wird hier kein Land angegeben, ist der kostenlose Versand f�r alle L�nder aktiv (Standardeinstellung).");

            builder.AddOrUpdate("Admin.Plugins.KnownGroup.AI", "AI", "KI");
        }
    }
}