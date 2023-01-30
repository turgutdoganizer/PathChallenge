namespace Ordering.Core.EventSourcing.Streams.Base
{
    public static class ScreamConfigurations
    {
        #region Order
        public static string OrderStreamName => "OrderStream";
        public static string OrderGroupName => "order-stream-group";
        #endregion
    }
}
