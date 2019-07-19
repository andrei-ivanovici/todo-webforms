using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business
{
    public class Store
    {
        private const int TIMEOUT = 100;

        private static List<ToDo> todoStore = new List<ToDo>
        {
            new ToDo {Title = "Walk the dog"},
            new ToDo {Title = "Mail the document"}
        };

        public async Task<IEnumerable<ToDo>> GetData()
        {
            await Task.Delay(TIMEOUT);
            return todoStore;
        }

        public async Task SaveData(IEnumerable<ToDo> newStore)
        {
            await Task.Delay(TIMEOUT);
            todoStore = newStore.ToList();
        }
    }
}