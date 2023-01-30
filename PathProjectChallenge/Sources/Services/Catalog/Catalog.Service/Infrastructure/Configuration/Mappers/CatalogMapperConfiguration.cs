using AutoMapper;
using AutoMapper.Internal;
using Catalog.Core.Domain;
using Catalog.Core.Domain.Catalog;
using Catalog.Core.EventSourcing.Events.Categories;
using Catalog.Core.EventSourcing.Events.Products;
using Catalog.Dto.Categories;
using Catalog.Dto.Products;
using PathProjectChallenge.Core.Configuration;
using PathProjectChallenge.Core.Infrastructure.Mapper;
using PathProjectChallenge.Core.Infrastructure.Mapper.Extensions;
using PathProjectChallenge.Core.Infrastructure.Mapper.Models;
using PathProjectChallenge.Service.Infrastructure.Plugins;

namespace PathProjectChallenge.Service.Infrastructure.Mapper
{
    /// <summary>
    /// AutoMapper configuration for admin area models
    /// </summary>
    public partial class CatalogMapperConfiguration : Profile, IOrderedMapperProfile
    {
        #region Ctor

        public CatalogMapperConfiguration()
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

            CreateMap<CategoryInsertedEvent, CategoryInsertDto>().ReverseMap();
            CreateMap<CategoryInsertDto, Category>().ReverseMap();

            CreateMap<CategoryUpdatedEvent, CategoryUpdateDto>().ReverseMap();
            CreateMap<CategoryUpdateDto, Category>().ReverseMap();

            CreateMap<CategoryDeletedEvent, CategoryDeleteDto>().ReverseMap();
            CreateMap<CategoryDeleteDto, Category>().ReverseMap();

            // Worker Service


            CreateMap<ProductInsertedEvent, ProductInsertDto>().ReverseMap();
            CreateMap<ProductInsertDto, Product>().ReverseMap();
            
            CreateMap<ProductUpdatedEvent,ProductUpdateDto>().ReverseMap();
            CreateMap<ProductUpdateDto, Product>().ReverseMap();

            CreateMap<ProductDeletedEvent,ProductDeleteDto>().ReverseMap();
            CreateMap<ProductDeleteDto, Product>().ReverseMap();





        }







        #endregion

        #region Properties

        /// <summary>
        /// Order of this mapper implementation
        /// </summary>
        public int Order => 0;

        #endregion
    }
}
