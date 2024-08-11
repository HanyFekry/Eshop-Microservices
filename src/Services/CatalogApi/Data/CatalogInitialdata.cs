using Marten.Schema;

namespace CatalogApi.Data
{
    public class InitialData : IInitialData
    {
        private readonly object[] _initialData;

        public InitialData(params object[] initialData)
        {
            _initialData = initialData;
        }

        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            await using var session = store.LightweightSession();
            if (session.Query<Product>().Any())
                return;
            // Marten UPSERT will cater for existing records
            session.Store(_initialData);
            await session.SaveChangesAsync();
        }
    }
    public class CatalogInitialdata : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            await using var session = store.LightweightSession();
            if (session.Query<Product>().Any())
                return;
            // Marten UPSERT will cater for existing records
            session.Store(products);
            await session.SaveChangesAsync();
        }

        static Random random = new Random();
        public static readonly Product[] products = {
            new Product{Id=Guid.NewGuid(),Name=$"Product{random.Next(5,15)}",Category=new(){ $"cat{random.Next(1,4)}"},Description="seeded from Initial data",Price=random.Next(1,4)*100,ImageUrl="localhost"},
            new Product{Id=Guid.NewGuid(),Name=$"Product{random.Next(5,15)}",Category=new(){ $"cat{random.Next(1,4)}"},Description="seeded from Initial data",Price=random.Next(1,4)*100,ImageUrl="localhost"},
            new Product{Id=Guid.NewGuid(),Name=$"Product{random.Next(5,15)}",Category=new(){ $"cat{random.Next(1,4)}"},Description="seeded from Initial data",Price=random.Next(1,4)*100,ImageUrl="localhost"},
            new Product{Id=Guid.NewGuid(),Name=$"Product{random.Next(5,15)}",Category=new(){ $"cat{random.Next(1,4)}"},Description="seeded from Initial data",Price=random.Next(1,4)*100,ImageUrl="localhost"},
            new Product{Id=Guid.NewGuid(),Name=$"Product{random.Next(5,15)}",Category=new(){ $"cat{random.Next(1,4)}"},Description="seeded from Initial data",Price=random.Next(1,4)*100,ImageUrl="localhost"},
            new Product{Id=Guid.NewGuid(),Name=$"Product{random.Next(5,15)}",Category=new(){ $"cat{random.Next(1,4)}"},Description="seeded from Initial data",Price=random.Next(1,4)*100,ImageUrl="localhost"},
            new Product{Id=Guid.NewGuid(),Name=$"Product{random.Next(5,15)}",Category=new(){ $"cat{random.Next(1,4)}"},Description="seeded from Initial data",Price=random.Next(1,4)*100,ImageUrl="localhost"}
        };
    }
}
