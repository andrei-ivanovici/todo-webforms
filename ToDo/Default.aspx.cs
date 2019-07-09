using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;

namespace ToDo
{
    public partial class _Default : Page
    {
        private ToDoService _todoService;

        public bool isEmpty;
        public int TodoCount;

        public _Default()
        {
            _todoService = new ToDoService();
        }

        protected override async void OnInit(EventArgs e)
        { 
            await RefreshItems();
        }

        private  async Task RefreshItems()
        {
            var todos = (await _todoService.GetToDos()).ToList();
            isEmpty = todos.Any();
            TodoCount = todos.Count;
            todoList.DataSource = todos;
            todoList.DataBind();
        }

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack && !string.IsNullOrEmpty(taskName.Text))
            {
                await _todoService.AddItem(new Business.ToDo {Title = taskName.Text});
                taskName.Text = "";

                await RefreshItems();
            }
        }

        protected async void IsCompleted_CheckedChanged(object sender, EventArgs e)
        {
            var checkbox = (CheckBox) sender;
            var itemId = checkbox.Attributes["itemID"];
            await _todoService.MarkAsCompleted(itemId, checkbox.Checked);

            await RefreshItems();
        }

        protected async void Remove_Item(object sender, EventArgs e)
        {
            var checkbox = (Button) sender;
            var itemId = checkbox.Attributes["itemID"];
            await _todoService.RemoveItem(itemId);
            await RefreshItems();
        }

        protected async void Clear_Completed(object sender, EventArgs e)
        {
            await _todoService.ClearCompleted();
            await RefreshItems();
        }
    }
}