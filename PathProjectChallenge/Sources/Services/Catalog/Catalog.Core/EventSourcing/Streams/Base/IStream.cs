namespace Catalog.Core.EventSourcing.Streams.Base
{
    public interface IStream
    {
        public Task<bool> InsertAsync(Guid id);
    }
}
