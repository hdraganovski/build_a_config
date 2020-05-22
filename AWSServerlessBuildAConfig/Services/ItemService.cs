namespace AWSServerlessBuildAConfig.Services
{
    using AWSServerlessBuildAConfig.Entities;
    using AWSServerlessBuildAConfig.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ItemService
    {
        private ItemRepository ItemRepository { get; set; }

        public ItemService(ItemRepository itemRepository)
        {
            ItemRepository = itemRepository;
        }

        public async Task<List<Item>> GetAll()
        {
            return await ItemRepository.GetAll();
        }

        public async Task<Item> Get(string uid)
        {
            return await ItemRepository.Get(uid);
        }

        public async Task<string> Save(Item item)
        {
            if(string.IsNullOrWhiteSpace(item.Uid))
            {
                item.Uid = Guid.NewGuid().ToString();
            }

            await ItemRepository.Save(item);

            return item.Uid;
        }

        public async Task Delete(string uid)
        {
            await ItemRepository.Remove(uid);
        }
    }
}
