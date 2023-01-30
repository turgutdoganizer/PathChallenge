using AutoMapper;
using AutoMapper.Internal;
using PathProjectChallenge.Core.Configuration;
using PathProjectChallenge.Core.Infrastructure.Mapper.Extensions;
using PathProjectChallenge.Core.Infrastructure.Mapper.Models;
using PathProjectChallenge.Service.Infrastructure.Plugins;

namespace PathProjectChallenge.Core.Infrastructure.Mapper
{
    /// <summary>
    /// AutoMapper configuration for admin area models
    /// </summary>
    public partial class PathMapperConfiguration : Profile, IOrderedMapperProfile
    {
        #region Ctor

        public PathMapperConfiguration()
        {
            //create specific maps
            CreateConfigMaps();
            CreateCatalogMaps();


            //add some generic mapping rules
            this.Internal().ForAllMaps((mapConfiguration, map) =>
            {
                //exclude Form and CustomProperties from mapping BaseNopModel
                if (typeof(BasePathModel).IsAssignableFrom(mapConfiguration.DestinationType))
                {
                    //map.ForMember(nameof(BaseNopModel.Form), options => options.Ignore());
                    map.ForMember(nameof(BasePathModel.CustomProperties), options => options.Ignore());
                }

                //exclude ActiveStoreScopeConfiguration from mapping ISettingsModel
                if (typeof(ISettingsModel).IsAssignableFrom(mapConfiguration.DestinationType))
                    map.ForMember(nameof(ISettingsModel.ActiveStoreScopeConfiguration), options => options.Ignore());

                //exclude some properties from mapping configuration and models
                if (typeof(IConfig).IsAssignableFrom(mapConfiguration.DestinationType))
                    map.ForMember(nameof(IConfig.Name), options => options.Ignore());

                //exclude Locales from mapping ILocalizedModel
                //if (typeof(ILocalizedModel).IsAssignableFrom(mapConfiguration.DestinationType))
                //    map.ForMember(nameof(ILocalizedModel<ILocalizedModel>.Locales), options => options.Ignore());

                //exclude some properties from mapping store mapping supported entities and models
                //if (typeof(IStoreMappingSupported).IsAssignableFrom(mapConfiguration.DestinationType))
                //    map.ForMember(nameof(IStoreMappingSupported.LimitedToStores), options => options.Ignore());
                //if (typeof(IStoreMappingSupportedModel).IsAssignableFrom(mapConfiguration.DestinationType))
                //{
                //    map.ForMember(nameof(IStoreMappingSupportedModel.AvailableStores), options => options.Ignore());
                //    map.ForMember(nameof(IStoreMappingSupportedModel.SelectedStoreIds), options => options.Ignore());
                //}

                //exclude some properties from mapping ACL supported entities and models
                //if (typeof(IAclSupported).IsAssignableFrom(mapConfiguration.DestinationType))
                //    map.ForMember(nameof(IAclSupported.SubjectToAcl), options => options.Ignore());
                //if (typeof(IAclSupportedModel).IsAssignableFrom(mapConfiguration.DestinationType))
                //{
                //    map.ForMember(nameof(IAclSupportedModel.AvailableCustomerRoles), options => options.Ignore());
                //    map.ForMember(nameof(IAclSupportedModel.SelectedCustomerRoleIds), options => options.Ignore());
                //}

                //exclude some properties from mapping discount supported entities and models
                //if (typeof(IDiscountSupportedModel).IsAssignableFrom(mapConfiguration.DestinationType))
                //{
                //    map.ForMember(nameof(IDiscountSupportedModel.AvailableDiscounts), options => options.Ignore());
                //    map.ForMember(nameof(IDiscountSupportedModel.SelectedDiscountIds), options => options.Ignore());
                //}

                if (typeof(IPluginModel).IsAssignableFrom(mapConfiguration.DestinationType))
                {
                    //exclude some properties from mapping plugin models
                    map.ForMember(nameof(IPluginModel.ConfigurationUrl), options => options.Ignore());
                    map.ForMember(nameof(IPluginModel.IsActive), options => options.Ignore());
                    map.ForMember(nameof(IPluginModel.LogoUrl), options => options.Ignore());

                    //define specific rules for mapping plugin models
                    if (typeof(IPlugin).IsAssignableFrom(mapConfiguration.SourceType))
                    {
                        map.ForMember(nameof(IPluginModel.DisplayOrder), options => options.MapFrom(plugin => ((IPlugin)plugin).PluginDescriptor.DisplayOrder));
                        map.ForMember(nameof(IPluginModel.FriendlyName), options => options.MapFrom(plugin => ((IPlugin)plugin).PluginDescriptor.FriendlyName));
                        map.ForMember(nameof(IPluginModel.SystemName), options => options.MapFrom(plugin => ((IPlugin)plugin).PluginDescriptor.SystemName));
                    }
                }
            });
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Create configuration maps 
        /// </summary>
        protected virtual void CreateConfigMaps()
        {
            //CreateMap<CacheConfig, CacheConfigModel>();
            //CreateMap<CacheConfigModel, CacheConfig>();

            //CreateMap<HostingConfig, HostingConfigModel>();
            //CreateMap<HostingConfigModel, HostingConfig>();

            //CreateMap<DistributedCacheConfig, DistributedCacheConfigModel>()
            //    .ForMember(model => model.DistributedCacheTypeValues, options => options.Ignore());
            //CreateMap<DistributedCacheConfigModel, DistributedCacheConfig>();

            //CreateMap<AzureBlobConfig, AzureBlobConfigModel>();
            //CreateMap<AzureBlobConfigModel, AzureBlobConfig>()
            //    .ForMember(entity => entity.Enabled, options => options.Ignore())
            //    .ForMember(entity => entity.DataProtectionKeysEncryptWithVault, options => options.Ignore());

            //CreateMap<InstallationConfig, InstallationConfigModel>();
            //CreateMap<InstallationConfigModel, InstallationConfig>();

            //CreateMap<PluginConfig, PluginConfigModel>();
            //CreateMap<PluginConfigModel, PluginConfig>();

            //CreateMap<CommonConfig, CommonConfigModel>();
            //CreateMap<CommonConfigModel, CommonConfig>();

            //CreateMap<DataConfig, DataConfigModel>()
            //    .ForMember(model => model.DataProviderTypeValues, options => options.Ignore());
            //CreateMap<DataConfigModel, DataConfig>();

            //CreateMap<WebOptimizerConfig, WebOptimizerConfigModel>();
            //CreateMap<WebOptimizerConfigModel, WebOptimizerConfig>()
            //    .ForMember(entity => entity.CdnUrl, options => options.Ignore())
            //    .ForMember(entity => entity.AllowEmptyBundle, options => options.Ignore())
            //    .ForMember(entity => entity.HttpsCompression, options => options.Ignore())
            //    .ForMember(entity => entity.EnableTagHelperBundling, options => options.Ignore())
            //    .ForMember(entity => entity.EnableCaching, options => options.Ignore())
            //    .ForMember(entity => entity.EnableMemoryCache, options => options.Ignore());
        }



        /// <summary>
        /// Create catalog maps 
        /// </summary>
        protected virtual void CreateCatalogMaps()
        {



            //CreateMap<ProductCategory, CategoryProductModel>()
            //    .ForMember(model => model.ProductName, options => options.Ignore());
            //CreateMap<CategoryProductModel, ProductCategory>()
            //    .ForMember(entity => entity.CategoryId, options => options.Ignore())
            //    .ForMember(entity => entity.ProductId, options => options.Ignore());












            //CreateMap<CategoryInsertDto, CategoryInsertedEvent>().ReverseMap();
            //CreateMap<CategoryUpdateDto, CategoryUpdatedEvent>().ReverseMap();
            //CreateMap<CategoryDeleteDto, CategoryDeletedEvent>().ReverseMap();


            ////  For Writing to Sql Server
            ////CreateMap<CategoryInsertedEvent, Category>().ReverseMap();
            //CreateMap<CategoryUpdatedEvent, Category>().ReverseMap();
            //CreateMap<CategoryDeletedEvent, Category>().ReverseMap();

            //CreateMap<CategoryInsertedEvent, Category>()
            //    .ForMember(model => model.Name, options => options.Ignore())
            //    .ForMember(model => model.Description, options => options.Ignore())
            //    .ForMember(model => model.Id, options => options.Ignore())
            //    .IgnoreNoMap()
            //    .ReverseMap();

            //CreateMap<Category, CategoryInsertedEvent>()
            //   .ForMember(model => model.Name, options => options.Ignore())
            //   .ForMember(model => model.Description, options => options.Ignore())
            //   .ForMember(model => model.Id, options => options.Ignore())
            //   .IgnoreNoMap()
            //   .ReverseMap();

            //CreateMap<CategoryInsertedEvent, CategoryInsertDto>().ReverseMap();
            //CreateMap<CategoryInsertDto, Category>().ReverseMap();

            //CreateMap<Category, CategoryInsertedEvent>()
            //    .ForMember(model => model.AvailableCategories, options => options.Ignore())
            //    .ForMember(model => model.AvailableCategoryTemplates, options => options.Ignore())
            //    .ForMember(model => model.Breadcrumb, options => options.Ignore())
            //    .ForMember(model => model.CategoryProductSearchModel, options => options.Ignore())
            //    .ForMember(model => model.SeName, options => options.Ignore())
            //    .ForMember(model => model.PrimaryStoreCurrencyCode, options => options.Ignore());











            //CreateMap<Category, CategoryModel>()
            //    .ForMember(model => model.AvailableCategories, options => options.Ignore())
            //    .ForMember(model => model.AvailableCategoryTemplates, options => options.Ignore())
            //    .ForMember(model => model.Breadcrumb, options => options.Ignore())
            //    .ForMember(model => model.CategoryProductSearchModel, options => options.Ignore())
            //    .ForMember(model => model.SeName, options => options.Ignore())
            //    .ForMember(model => model.PrimaryStoreCurrencyCode, options => options.Ignore());
            //CreateMap<CategoryModel, Category>()
            //    .ForMember(entity => entity.CreatedOnUtc, options => options.Ignore())
            //    .ForMember(entity => entity.Deleted, options => options.Ignore())
            //    .ForMember(entity => entity.UpdatedOnUtc, options => options.Ignore());
















            //CreateMap<CategoryTemplate, CategoryTemplateModel>();
            //CreateMap<CategoryTemplateModel, CategoryTemplate>();

            //CreateMap<ProductManufacturer, ManufacturerProductModel>()
            //    .ForMember(model => model.ProductName, options => options.Ignore());
            //CreateMap<ManufacturerProductModel, ProductManufacturer>()
            //    .ForMember(entity => entity.ManufacturerId, options => options.Ignore())
            //    .ForMember(entity => entity.ProductId, options => options.Ignore());

            //CreateMap<Manufacturer, ManufacturerModel>()
            //    .ForMember(model => model.AvailableManufacturerTemplates, options => options.Ignore())
            //    .ForMember(model => model.ManufacturerProductSearchModel, options => options.Ignore())
            //    .ForMember(model => model.SeName, options => options.Ignore())
            //    .ForMember(model => model.PrimaryStoreCurrencyCode, options => options.Ignore());
            //CreateMap<ManufacturerModel, Manufacturer>()
            //    .ForMember(entity => entity.CreatedOnUtc, options => options.Ignore())
            //    .ForMember(entity => entity.Deleted, options => options.Ignore())
            //    .ForMember(entity => entity.UpdatedOnUtc, options => options.Ignore());








            //products
            //CreateMap<Product, ProductModel>()
            //    .ForMember(model => model.AddPictureModel, options => options.Ignore())
            //    .ForMember(model => model.AssociatedProductSearchModel, options => options.Ignore())
            //    .ForMember(model => model.AssociatedToProductId, options => options.Ignore())
            //    .ForMember(model => model.AssociatedToProductName, options => options.Ignore())
            //    .ForMember(model => model.AvailableBasepriceBaseUnits, options => options.Ignore())
            //    .ForMember(model => model.AvailableBasepriceUnits, options => options.Ignore())
            //    .ForMember(model => model.AvailableCategories, options => options.Ignore())
            //    .ForMember(model => model.AvailableDeliveryDates, options => options.Ignore())
            //    .ForMember(model => model.AvailableManufacturers, options => options.Ignore())
            //    .ForMember(model => model.AvailableProductAvailabilityRanges, options => options.Ignore())
            //    .ForMember(model => model.AvailableProductTemplates, options => options.Ignore())
            //    .ForMember(model => model.AvailableTaxCategories, options => options.Ignore())
            //    .ForMember(model => model.AvailableVendors, options => options.Ignore())
            //    .ForMember(model => model.AvailableWarehouses, options => options.Ignore())
            //    .ForMember(model => model.BaseDimensionIn, options => options.Ignore())
            //    .ForMember(model => model.BaseWeightIn, options => options.Ignore())
            //    .ForMember(model => model.CopyProductModel, options => options.Ignore())
            //    .ForMember(model => model.CrossSellProductSearchModel, options => options.Ignore())
            //    .ForMember(model => model.HasAvailableSpecificationAttributes, options => options.Ignore())
            //    .ForMember(model => model.IsLoggedInAsVendor, options => options.Ignore())
            //    .ForMember(model => model.LastStockQuantity, options => options.Ignore())
            //    .ForMember(model => model.PictureThumbnailUrl, options => options.Ignore())
            //    .ForMember(model => model.PrimaryStoreCurrencyCode, options => options.Ignore())
            //    .ForMember(model => model.ProductAttributeCombinationSearchModel, options => options.Ignore())
            //    .ForMember(model => model.ProductAttributeMappingSearchModel, options => options.Ignore())
            //    .ForMember(model => model.ProductAttributesExist, options => options.Ignore())
            //    .ForMember(model => model.CanCreateCombinations, options => options.Ignore())
            //    .ForMember(model => model.ProductEditorSettingsModel, options => options.Ignore())
            //    .ForMember(model => model.ProductOrderSearchModel, options => options.Ignore())
            //    .ForMember(model => model.ProductPictureModels, options => options.Ignore())
            //    .ForMember(model => model.ProductPictureSearchModel, options => options.Ignore())
            //    .ForMember(model => model.ProductVideoModels, options => options.Ignore())
            //    .ForMember(model => model.ProductVideoSearchModel, options => options.Ignore())
            //    .ForMember(model => model.AddVideoModel, options => options.Ignore())
            //    .ForMember(model => model.ProductSpecificationAttributeSearchModel, options => options.Ignore())
            //    .ForMember(model => model.ProductsTypesSupportedByProductTemplates, options => options.Ignore())
            //    .ForMember(model => model.ProductTags, options => options.Ignore())
            //    .ForMember(model => model.ProductTypeName, options => options.Ignore())
            //    .ForMember(model => model.ProductWarehouseInventoryModels, options => options.Ignore())
            //    .ForMember(model => model.RelatedProductSearchModel, options => options.Ignore())
            //    .ForMember(model => model.SelectedCategoryIds, options => options.Ignore())
            //    .ForMember(model => model.SelectedManufacturerIds, options => options.Ignore())
            //    .ForMember(model => model.SeName, options => options.Ignore())
            //    .ForMember(model => model.StockQuantityHistory, options => options.Ignore())
            //    .ForMember(model => model.StockQuantityHistorySearchModel, options => options.Ignore())
            //    .ForMember(model => model.StockQuantityStr, options => options.Ignore())
            //    .ForMember(model => model.TierPriceSearchModel, options => options.Ignore())
            //    .ForMember(model => model.InitialProductTags, options => options.Ignore());
            //CreateMap<ProductModel, Product>()
            //    .ForMember(entity => entity.ApprovedRatingSum, options => options.Ignore())
            //    .ForMember(entity => entity.ApprovedTotalReviews, options => options.Ignore())
            //    .ForMember(entity => entity.BackorderMode, options => options.Ignore())
            //    .ForMember(entity => entity.CreatedOnUtc, options => options.Ignore())
            //    .ForMember(entity => entity.Deleted, options => options.Ignore())
            //    .ForMember(entity => entity.DownloadActivationType, options => options.Ignore())
            //    .ForMember(entity => entity.GiftCardType, options => options.Ignore())
            //    .ForMember(entity => entity.HasDiscountsApplied, options => options.Ignore())
            //    .ForMember(entity => entity.HasTierPrices, options => options.Ignore())
            //    .ForMember(entity => entity.LowStockActivity, options => options.Ignore())
            //    .ForMember(entity => entity.ManageInventoryMethod, options => options.Ignore())
            //    .ForMember(entity => entity.NotApprovedRatingSum, options => options.Ignore())
            //    .ForMember(entity => entity.NotApprovedTotalReviews, options => options.Ignore())
            //    .ForMember(entity => entity.ParentGroupedProductId, options => options.Ignore())
            //    .ForMember(entity => entity.ProductType, options => options.Ignore())
            //    .ForMember(entity => entity.RecurringCyclePeriod, options => options.Ignore())
            //    .ForMember(entity => entity.RentalPricePeriod, options => options.Ignore())
            //    .ForMember(entity => entity.UpdatedOnUtc, options => options.Ignore());

            //CreateMap<Product, DiscountProductModel>()
            //    .ForMember(model => model.ProductId, options => options.Ignore())
            //    .ForMember(model => model.ProductName, options => options.Ignore());

            //CreateMap<Product, AssociatedProductModel>()
            //    .ForMember(model => model.ProductName, options => options.Ignore());

            //CreateMap<ProductAttributeCombination, ProductAttributeCombinationModel>()
            //   .ForMember(model => model.AttributesXml, options => options.Ignore())
            //   .ForMember(model => model.ProductAttributes, options => options.Ignore())
            //   .ForMember(model => model.ProductPictureModels, options => options.Ignore())
            //   .ForMember(model => model.PictureThumbnailUrl, options => options.Ignore())
            //   .ForMember(model => model.Warnings, options => options.Ignore());
            //CreateMap<ProductAttributeCombinationModel, ProductAttributeCombination>()
            //   .ForMember(entity => entity.AttributesXml, options => options.Ignore());

            //CreateMap<ProductAttribute, ProductAttributeModel>()
            //    .ForMember(model => model.PredefinedProductAttributeValueSearchModel, options => options.Ignore())
            //    .ForMember(model => model.ProductAttributeProductSearchModel, options => options.Ignore());
            //CreateMap<ProductAttributeModel, ProductAttribute>();

            //CreateMap<Product, ProductAttributeProductModel>()
            //    .ForMember(model => model.ProductName, options => options.Ignore());

            //CreateMap<PredefinedProductAttributeValue, PredefinedProductAttributeValueModel>()
            //    .ForMember(model => model.WeightAdjustmentStr, options => options.Ignore())
            //    .ForMember(model => model.PriceAdjustmentStr, options => options.Ignore());
            //CreateMap<PredefinedProductAttributeValueModel, PredefinedProductAttributeValue>();

            //CreateMap<ProductAttributeMapping, ProductAttributeMappingModel>()
            //    .ForMember(model => model.ValidationRulesString, options => options.Ignore())
            //    .ForMember(model => model.AttributeControlType, options => options.Ignore())
            //    .ForMember(model => model.ConditionString, options => options.Ignore())
            //    .ForMember(model => model.ProductAttribute, options => options.Ignore())
            //    .ForMember(model => model.AvailableProductAttributes, options => options.Ignore())
            //    .ForMember(model => model.ConditionAllowed, options => options.Ignore())
            //    .ForMember(model => model.ConditionModel, options => options.Ignore())
            //    .ForMember(model => model.ProductAttributeValueSearchModel, options => options.Ignore());
            //CreateMap<ProductAttributeMappingModel, ProductAttributeMapping>()
            //    .ForMember(entity => entity.ConditionAttributeXml, options => options.Ignore())
            //    .ForMember(entity => entity.AttributeControlType, options => options.Ignore());

            //CreateMap<ProductAttributeValue, ProductAttributeValueModel>()
            //    .ForMember(model => model.AttributeValueTypeName, options => options.Ignore())
            //    .ForMember(model => model.Name, options => options.Ignore())
            //    .ForMember(model => model.PriceAdjustmentStr, options => options.Ignore())
            //    .ForMember(model => model.AssociatedProductName, options => options.Ignore())
            //    .ForMember(model => model.PictureThumbnailUrl, options => options.Ignore())
            //    .ForMember(model => model.WeightAdjustmentStr, options => options.Ignore())
            //    .ForMember(model => model.DisplayColorSquaresRgb, options => options.Ignore())
            //    .ForMember(model => model.DisplayImageSquaresPicture, options => options.Ignore())
            //    .ForMember(model => model.ProductPictureModels, options => options.Ignore());
            //CreateMap<ProductAttributeValueModel, ProductAttributeValue>()
            //   .ForMember(entity => entity.AttributeValueType, options => options.Ignore())
            //   .ForMember(entity => entity.Quantity, options => options.Ignore());

            //CreateMap<ProductEditorSettings, ProductEditorSettingsModel>();
            //CreateMap<ProductEditorSettingsModel, ProductEditorSettings>();

            //CreateMap<ProductPicture, ProductPictureModel>()
            //    .ForMember(model => model.OverrideAltAttribute, options => options.Ignore())
            //    .ForMember(model => model.OverrideTitleAttribute, options => options.Ignore())
            //    .ForMember(model => model.PictureUrl, options => options.Ignore());

            //CreateMap<ProductVideo, ProductVideoModel>()
            //   .ForMember(model => model.VideoUrl, options => options.Ignore());

            //CreateMap<Product, SpecificationAttributeProductModel>()
            //    .ForMember(model => model.SpecificationAttributeId, options => options.Ignore())
            //    .ForMember(model => model.ProductId, options => options.Ignore())
            //    .ForMember(model => model.ProductName, options => options.Ignore());

            //CreateMap<ProductSpecificationAttribute, ProductSpecificationAttributeModel>()
            //    .ForMember(model => model.AttributeTypeName, options => options.Ignore())
            //    .ForMember(model => model.ValueRaw, options => options.Ignore())
            //    .ForMember(model => model.AttributeId, options => options.Ignore())
            //    .ForMember(model => model.AttributeName, options => options.Ignore())
            //    .ForMember(model => model.SpecificationAttributeOptionId, options => options.Ignore());

            //CreateMap<ProductSpecificationAttribute, AddSpecificationAttributeModel>()
            //    .ForMember(entity => entity.SpecificationId, options => options.Ignore())
            //    .ForMember(entity => entity.AttributeTypeName, options => options.Ignore())
            //    .ForMember(entity => entity.AttributeId, options => options.Ignore())
            //    .ForMember(entity => entity.AttributeName, options => options.Ignore())
            //    .ForMember(entity => entity.ValueRaw, options => options.Ignore())
            //    .ForMember(entity => entity.Value, options => options.Ignore())
            //    .ForMember(entity => entity.AvailableOptions, options => options.Ignore())
            //    .ForMember(entity => entity.AvailableAttributes, options => options.Ignore());

            //CreateMap<AddSpecificationAttributeModel, ProductSpecificationAttribute>()
            //    .ForMember(model => model.CustomValue, options => options.Ignore())
            //    .ForMember(model => model.AttributeType, options => options.Ignore());

            //CreateMap<ProductTag, ProductTagModel>()
            //   .ForMember(model => model.ProductCount, options => options.Ignore());

            //CreateMap<ProductTemplate, ProductTemplateModel>();
            //CreateMap<ProductTemplateModel, ProductTemplate>();

            //CreateMap<RelatedProduct, RelatedProductModel>()
            //   .ForMember(model => model.Product2Name, options => options.Ignore());

            //CreateMap<SpecificationAttribute, SpecificationAttributeModel>()
            //    .ForMember(model => model.SpecificationAttributeOptionSearchModel, options => options.Ignore())
            //    .ForMember(model => model.SpecificationAttributeProductSearchModel, options => options.Ignore())
            //    .ForMember(model => model.AvailableGroups, options => options.Ignore());
            //CreateMap<SpecificationAttributeModel, SpecificationAttribute>();

            //CreateMap<SpecificationAttributeOption, SpecificationAttributeOptionModel>()
            //    .ForMember(model => model.EnableColorSquaresRgb, options => options.Ignore())
            //    .ForMember(model => model.NumberOfAssociatedProducts, options => options.Ignore());
            //CreateMap<SpecificationAttributeOptionModel, SpecificationAttributeOption>();

            //CreateMap<SpecificationAttributeGroup, SpecificationAttributeGroupModel>();
            //CreateMap<SpecificationAttributeGroupModel, SpecificationAttributeGroup>();

            //CreateMap<StockQuantityHistory, StockQuantityHistoryModel>()
            //    .ForMember(model => model.WarehouseName, options => options.Ignore())
            //    .ForMember(model => model.CreatedOn, options => options.Ignore())
            //    .ForMember(model => model.AttributeCombination, options => options.Ignore());

            //CreateMap<TierPrice, TierPriceModel>()
            //    .ForMember(model => model.Store, options => options.Ignore())
            //    .ForMember(model => model.AvailableCustomerRoles, options => options.Ignore())
            //    .ForMember(model => model.AvailableStores, options => options.Ignore())
            //    .ForMember(model => model.CustomerRole, options => options.Ignore());
            //CreateMap<TierPriceModel, TierPrice>()
            //    .ForMember(entity => entity.CustomerRoleId, options => options.Ignore())
            //    .ForMember(entity => entity.ProductId, options => options.Ignore());
        }







        ///// <summary>
        ///// Create logging maps 
        ///// </summary>
        //protected virtual void CreateLoggingMaps()
        //{
        //    CreateMap<ActivityLog, ActivityLogModel>()
        //        .ForMember(model => model.ActivityLogTypeName, options => options.Ignore())
        //        .ForMember(model => model.CreatedOn, options => options.Ignore())
        //        .ForMember(model => model.CustomerEmail, options => options.Ignore());
        //    CreateMap<ActivityLogModel, ActivityLog>()
        //        .ForMember(entity => entity.ActivityLogTypeId, options => options.Ignore())
        //        .ForMember(entity => entity.CreatedOnUtc, options => options.Ignore())
        //        .ForMember(entity => entity.EntityId, options => options.Ignore())
        //        .ForMember(entity => entity.EntityName, options => options.Ignore());

        //    CreateMap<ActivityLogType, ActivityLogTypeModel>();
        //    CreateMap<ActivityLogTypeModel, ActivityLogType>()
        //        .ForMember(entity => entity.SystemKeyword, options => options.Ignore());

        //    CreateMap<Log, LogModel>()
        //        .ForMember(model => model.CreatedOn, options => options.Ignore())
        //        .ForMember(model => model.FullMessage, options => options.Ignore())
        //        .ForMember(model => model.CustomerEmail, options => options.Ignore());
        //    CreateMap<LogModel, Log>()
        //        .ForMember(entity => entity.CreatedOnUtc, options => options.Ignore())
        //        .ForMember(entity => entity.LogLevelId, options => options.Ignore());
        //}





        #endregion

        #region Properties

        /// <summary>
        /// Order of this mapper implementation
        /// </summary>
        public int Order => 0;

        #endregion
    }
}
