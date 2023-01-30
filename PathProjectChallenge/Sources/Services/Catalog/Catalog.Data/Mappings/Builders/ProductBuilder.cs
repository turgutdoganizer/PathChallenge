using Catalog.Core.Domain;
using Catalog.Core.Domain.Catalog;
using FluentMigrator.Builders.Create.Table;
using PathProjectChallenge.Data.Extensions;
using PathProjectChallenge.Data.Mapping.Builders;

namespace Catalog.Data.Mappings.Builders
{
    public class ProductBuilder : PathEntityBuilder<Product>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
               .WithColumn(nameof(Product.Name)).AsString(400).NotNullable()
               .WithColumn(nameof(Product.Description)).AsString(400).Nullable()
               .WithColumn(nameof(Product.UnitPrice)).AsInt32().Nullable()
               .WithColumn(nameof(Product.UnitsInStock)).AsInt32().Nullable()
               .WithColumn(nameof(Product.CreatedDate)).AsDateTime().Nullable()
               .WithColumn(nameof(Product.UpdatedDate)).AsDateTime().Nullable()
               .WithColumn(nameof(Product.CategoryId)).AsInt32().ForeignKey<Category>();

        }
    }
}
