using System.Data;
using FluentMigrator;
using Smartstore.Core.Catalog.Attributes;
using Smartstore.Core.Common;
using Smartstore.Core.Data;
using Smartstore.Core.Data.Migrations;
using Smartstore.Data.Migrations;

namespace Smartstore.Core.Migrations
{
    [MigrationVersion("2025-10-02 20:00:00", "Core: Collection groups")]
    internal class CollectionGroups : Migration, ILocaleResourcesProvider, IDataSeeder<SmartDbContext>
    {
        public override void Up()
        {
            const string groupTableName = nameof(CollectionGroup);
            const string mappingTableName = nameof(CollectionGroupMapping);
            const string specAttributeTableName = nameof(SpecificationAttribute);
            const string specAttributeColumnName = nameof(SpecificationAttribute.CollectionGroupMappingId);

            if (!Schema.Table(groupTableName).Exists())
            {
                Create.Table(groupTableName)
                    .WithIdColumn()
                    .WithColumn(nameof(CollectionGroup.Name)).AsString(400).NotNullable()
                        .Indexed()
                    .WithColumn(nameof(CollectionGroup.EntityName)).AsString(100).NotNullable()
                        .Indexed()
                    .WithColumn(nameof(CollectionGroup.DisplayOrder)).AsInt32().NotNullable()
                        .Indexed()
                    .WithColumn(nameof(CollectionGroup.Published)).AsBoolean().NotNullable();
            }

            if (!Schema.Table(mappingTableName).Exists())
            {
                Create.Table(mappingTableName)
                    .WithIdColumn()
                    .WithColumn(nameof(CollectionGroupMapping.CollectionGroupId)).AsInt32().NotNullable()
                        .Indexed()
                        .ForeignKey(groupTableName, nameof(BaseEntity.Id))
                        .OnDelete(Rule.Cascade)
                    .WithColumn(nameof(CollectionGroupMapping.EntityId)).AsInt32().NotNullable();
            }

            if (!Schema.Table(specAttributeTableName).Column(specAttributeColumnName).Exists())
            {
                Create.Column(specAttributeColumnName).OnTable(specAttributeTableName)
                    .AsInt32()
                    .Nullable()
                    .Indexed()
                    .ForeignKey(mappingTableName, nameof(BaseEntity.Id))
                    .OnDelete(Rule.SetNull);
            }
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
            builder.AddOrUpdate("Permissions.DisplayName.CollectionGroup", "Collection Groups", "Gruppierungen", "گروه‌های مجموعه");
            builder.AddOrUpdate("Admin.Configuration.CollectionGroups", "Collection Groups", "Gruppierungen", "گروه‌های مجموعه");

            builder.AddOrUpdate("Common.Entity.SpecificationAttribute", "Specification attribute", "Spezifikationsattribut", "ویژگی مشخصه");

            // Typo.
            builder.AddOrUpdate("Admin.Catalog.Attributes.SpecificationAttributes.Added")
                .Value("de", "Das Attribut wurde erfolgreich hinzugefügt.")
                .Value("fa", "ویژگی با موفقیت اضافه شد.");

            builder.AddOrUpdate("Admin.Catalog.Attributes.SpecificationAttributes.Fields.CollectionGroup",
                "Collection Group",
                "Gruppierung",
                "گروه مجموعه",
                "Specifies the name of a collection group (optional). Grouped attributes are indented in the frontend.",
                "Legt den Namen einer Gruppierung fest (optional). Gruppierte Attribute werden im Frontend eingerückt dargestellt.",
                "نام یک گروه مجموعه را مشخص می‌کند (اختیاری). ویژگی‌های گروه‌بندی شده در بخش کاربری با تورفتگی نمایش داده می‌شوند.");

            builder.AddOrUpdate("Admin.Configuration.CollectionGroup.Name",
                "Name",
                "Name",
                "نام",
                "Specifies the name of the collection group.",
                "Legt den Namen der Gruppierung fest.",
                "نام گروه مجموعه را مشخص می‌کند.");

            builder.AddOrUpdate("Admin.Configuration.CollectionGroup.EntityName",
                "Object",
                "Objekt",
                "شیء",
                "The name of the object assigned to the group.",
                "Der Name des Objekts, das der Gruppierung zugeordnet ist.",
                "نام شیء اختصاص‌یافته به گروه.");

            builder.AddOrUpdate("Admin.Configuration.CollectionGroup.NumberOfAssignments",
                "Assignments",
                "Zuordnungen",
                "تخصیص‌ها",
                "The number of objects assigned to the group.",
                "Die Anzahl der Objekte, die der Gruppierung zugeordnet sind.",
                "تعداد اشیاء اختصاص‌یافته به گروه.");

            builder.AddOrUpdate("Products.Specs.AdditionalFeatures", "Additional features", "Weitere Merkmale", "ویژگی‌های اضافی");
        }
    }
}
