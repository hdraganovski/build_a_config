namespace AWSServerlessBuildAConfig.Repositories
{
    using Amazon.DynamoDBv2.DataModel;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public abstract class CrudRepository<T>
    {
        protected IDynamoDBContext DDBContext { get; private set; }

        protected string TABLENAME_ENVIRONMENT_VARIABLE_LOOKUP { get; private set; }

        internal CrudRepository(IDynamoDBContext DDBContext, string lookUpString)
        {
            this.DDBContext = DDBContext;
            TABLENAME_ENVIRONMENT_VARIABLE_LOOKUP = lookUpString;
        }

        public async Task<List<T>> GetAll()
        {
            var search = DDBContext.ScanAsync<T>(null);
            return await search.GetRemainingAsync();
        }

        public async Task<T> Get(string uid)
        {
            return await DDBContext.LoadAsync<T>(uid);
        }

        public async Task Save(T entity)
        {
            await DDBContext.SaveAsync(entity);
        }

        public async Task Remove(string uid)
        {
            await DDBContext.DeleteAsync<T>(uid);
        }
    }
}
