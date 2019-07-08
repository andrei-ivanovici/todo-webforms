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
        public bool idDone { get; set; }
    }
    public partial class _Default : Page
    {

        public static List<Todo> todos = new List<Todo> {
            new Todo { Title = "Walk the dog" },
            new Todo { Title = "Mail the document"}
        };
        public ObjectDataSource dataSource;

        protected override void OnInit(EventArgs e)
        {
            todoList.DataSource = todos;
            todoList.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack && !string.IsNullOrEmpty(taskName.Text))
            {
                todos.Add(new Todo { Title = taskName.Text });
                taskName.Text = "";
                todoList.DataBind();
            }
        }

        protected void IsCompleted_CheckedChanged(object sender, EventArgs e)
        {
            todoList.DataBind();
            var checkbox = (CheckBox)sender;
            var item = checkbox.Parent as RepeaterItem;

            var currentTodo = item.DataItem as Todo;
            var foundValue = todos.First(t => t.Title == currentTodo.Title);
            if (foundValue != null)
            {
                foundValue.idDone = checkbox.Checked;
            }

        }
    }
}