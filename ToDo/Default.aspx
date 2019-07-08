<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ToDo._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <section class="todoapp">
        <header class="header">
            <h1>todos</h1>
            <form id="newTodo" class="todo-form" method="post">
                <asp:TextBox ID="taskName" runat="server" class="new-todo" ToolTip="What needs to be done?" placeholder="What needs to be done?"></asp:TextBox>
            </form>
        </header>
        <% if (todos.Count > 0)
            { %>
        <section class="main">
            <ul class="todo-list">
                <asp:Repeater ID="todoList" runat="server">
                    <ItemTemplate>
                        <li>
                            <div class="view">
                                <asp:CheckBox  ID="chk" runat="server" AutoPostBack="true" CssClass="toggle" OnCheckedChanged="IsCompleted_CheckedChanged"></asp:CheckBox>
                                <label><%#Eval("Title")%></label>
                                <button class="destroy"></button>
                            </div>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
                <% } %>
            </ul>
            <footer class="footer">
                <span class="todo-count">
                    <strong><%=todos.Count %></strong> items left</span>
                <button class="clear-completed">Clear completed</button>
            </footer>
        </section>
    </section>
</asp:Content>
