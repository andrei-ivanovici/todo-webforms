<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ToDo._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <section class="todoapp">
        <header class="header">
            <h1>todos</h1>
            <form id="newTodo" class="todo-form" method="post">
                <asp:TextBox ID="taskName" runat="server"
                             class="new-todo" ToolTip="What needs to be done?"
                             placeholder="What needs to be done?">
                </asp:TextBox>
            </form>
        </header>
        <% if (todoStore.Count > 0)
           { %>
        <section class="main">
            <ul class="todo-list">
                <asp:Repeater ID="todoList" runat="server">
                    <ItemTemplate>
                        <li class="<%#(bool) Eval("IsCompleted") ? "completed" : "" %>">
                            <div class="view">
                                <asp:CheckBox ID="chk" runat="server" AutoPostBack="true"
                                              CssClass="toggle"
                                              Text="  "
                                              Checked='<%#Eval("IsCompleted") %>'
                                              OnCheckedChanged="IsCompleted_CheckedChanged"
                                              itemId='<%#Eval("Title") %>'>
                                </asp:CheckBox>
                                <label><%#Eval("Title") %></label>
                                <%-- <button class="destroy" runat="server" OnClick="Remove_Item"></button> --%>
                                 <asp:Button CssClass="destroy" runat="server"  OnClick="Remove_Item" itemId='<%#Eval("Title") %>' />
                            </div>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>

            </ul>
            <footer class="footer">
                <span class="todo-count">
                    <strong><%= todoStore.Count %></strong> items left
                </span>
                <asp:Button CssClass="clear-completed" runat="server" OnClick="Clear_Completed" Text="Clear Completed"></asp:Button>
            </footer>
            <% } %>
        </section>
        </section>
</asp:Content>