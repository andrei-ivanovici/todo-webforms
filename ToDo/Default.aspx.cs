using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ToDo
{
    public class Todo
    {
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }

    public partial class _Default : Page
    {
        public static List<Todo> todos = new List<Todo>
        {
            new Todo {Title = "Walk the dog"},
            new Todo {Title = "Mail the document"}
        };
     
        protected override void OnInit(EventArgs e)
        {
            todoList.DataSource = todos;
            todoList.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack && !string.IsNullOrEmpty(taskName.Text))
            {
                todos.Add(new Todo {Title = taskName.Text});
                taskName.Text = "";
                todoList.DataBind();
            }
        }

        protected void IsCompleted_CheckedChanged(object sender, EventArgs e)
        {

            var checkbox = (CheckBox) sender;
            var itemId = checkbox.Attributes["itemID"];
            var foundValue = todos.First(t => t.Title == itemId);
            if (foundValue != null)
            {
                foundValue.IsCompleted = checkbox.Checked;
            }
            todoList.DataBind();
        }
    }
}