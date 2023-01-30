using Catalog.Core.Domain;
using Catalog.Core.Domain.Catalog;
using FluentMigrator;
using PathProjectChallenge.Data.Extensions;
using PathProjectChallenge.Data.Migrations;

namespace Catalog.Data.Migrations.Installation
{
    [PathMigration("2023/01/29 11:24:16:2551771", "PathProjectChallenge.Data base schema", MigrationProcessType.Installation)]
    public class SchemaMigration : AutoReversingMigration
    {
        /// <summary>
        /// Collect the UP migration expressions
        /// <remarks>
        /// We use an explicit table creation order instead of an automatic one
        /// due to problems creating relationships between tables
        /// </remarks>
        /// </summary>
        public override void Up()
        {
            Create.TableFor<Category>();
            Create.TableFor<Product>();
        }
    }
}
