namespace Catalog.Core.EventSourcing.Streams.Base
{
    public static class ScreamConfigurations
    {
        #region Category
        public static string CategoryStreamName => "CategoryStream";
        public static string CategoryGroupName => "category-stream-group";
        #endregion

        #region Product
        public static string ProductStreamName => "ProductStream";
        public static string ProductGroupName => "product-stream-group";
        #endregion
    }
}
