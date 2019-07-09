using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ToDo
{
    public class Todo
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }

        public Todo()
        {
            Id = Guid.NewGuid().ToString();
        }
    }

    public partial class _Default : Page
    {
        public static List<Todo> todoStore = new List<Todo>
        {
//            new Todo {Title = "Walk the dog"},
//            new Todo {Title = "Mail the document"}
        };

        protected override void OnInit(EventArgs e)
        {
//            Page.EnableEventValidation = false;
            todoList.DataSource = todoStore;
            todoList.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack && !string.IsNullOrEmpty(taskName.Text))
            {
                todoStore.Add(new Todo {Title = taskName.Text});
                taskName.Text = "";
                todoList.DataBind();
            }
        }

        protected void IsCompleted_CheckedChanged(object sender, EventArgs e)
        {
            var checkbox = (CheckBox) sender;
            var itemId = checkbox.Attributes["itemID"];
            var foundValue = todoStore.First(t => t.Title == itemId);
            if (foundValue != null)
            {
                foundValue.IsCompleted = checkbox.Checked;
            }

            todoList.DataBind();
        }

        protected void Remove_Item(object sender, EventArgs e)
        {
            var checkbox = (Button) sender;
            var itemId = checkbox.Attributes["itemID"];
            var foundValue = todoStore.First(t => t.Title == itemId);
            todoStore.Remove(foundValue);
            todoList.DataBind();
        }

        protected void Clear_Completed(object sender, EventArgs e)
        {
            todoStore = todoStore.Where(todo => !todo.IsCompleted).ToList();
            todoList.DataSource = todoStore;
            todoList.DataBind();
        }
    }
}