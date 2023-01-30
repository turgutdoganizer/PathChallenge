using Catalog.Core.Domain;
using Catalog.Core.Domain.Catalog;
using FluentMigrator;
using PathProjectChallenge.Data.Extensions;

namespace PathProjectChallenge.Data.Migrations.UpgradeTo1
{
    [PathMigration("2023-01-29 00:00:00", "1.0.1", UpdateMigrationType.Data, MigrationProcessType.Update)]
    public class SchemaMigration : Migration
    {
        private readonly IPathDataProvider _dataProvider;

        public SchemaMigration(IPathDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public override void Down()
        {

        }

        public override void Up()
        {
           
        }
    }
}
