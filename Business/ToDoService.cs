using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business
{
    public class ToDoService
    {
        private Store _storeRef;

        public ToDoService()
        {
            _storeRef = new Store();
        }

        public Task<IEnumerable<ToDo>> GetToDos()
        {
            return _storeRef.GetData();
        }


        public async Task MarkAsCompleted(string id, bool isCompleted)
        {
            var currentSnapshot = await _storeRef.GetData();
            var foundItem = currentSnapshot.First(item => item.Id == id);
            foundItem.IsCompleted = isCompleted;
        }

        public async Task ClearCompleted()
        {
            var items = await _storeRef.GetData();
            var newStore = items.Where(todo => !todo.IsCompleted).ToList();
            await _storeRef.SaveData(newStore);
        }

        public async Task RemoveItem(string id)
        {
            var items = (await _storeRef.GetData()).ToList();
            var foundItem = items.First(item => item.Id == id);
            items.Remove(foundItem);
            await _storeRef.SaveData(items);
        }

        public async Task AddItem(ToDo newItem)
        {
            if (newItem == null)
            {
                throw new ArgumentNullException(nameof(newItem));
            }

            var newData = (await _storeRef.GetData()).ToList();
            newData.Add(newItem);
            await _storeRef.SaveData(newData);
        }
    }
}