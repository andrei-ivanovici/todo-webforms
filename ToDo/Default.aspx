<%@ Page EnableEventValidation="false" Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" Async="true" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ToDo._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="nav">
        <div class="icon">
            <svg viewBox="0 0 128 100" style="height: 30px; width: 38.4px;">
                <path fill="#E3173E"
                      d="M125.78 50l-42.7-37.5a50 50 0 1 0 0 74.99l42.7-37.48m-110.6 0a34.83 34.83 0 1 1 69.65 0 34.83 34.83 0 0 1-69.65 0">
                </path>
            </svg>
        </div>
        
        <span class="label">Access todos</span>
        <a class="access-btn navigate" href="http://localhost:3000/">Try New</a>
    </div>
    <section class="todoapp">
        <header class="header">
            <form id="newTodo" class="todo-form" method="post">
                <asp:TextBox ID="taskName" runat="server"
                             class="new-todo" ToolTip="What needs to be done?"
                             placeholder="What needs to be done?">
                </asp:TextBox>
            </form>
        </header>
        <% if (isEmpty)
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
                                              itemId='<%#Eval("Id") %>'>
                                </asp:CheckBox>
                                <label><%#Eval("Title") %></label>
                                <%-- <button class="destroy" runat="server" OnClick="Remove_Item"></button> --%>
                                <asp:Button CssClass="access-btn delete" runat="server" Text="Delete" OnClick="Remove_Item" itemId='<%#Eval("Id") %>'/>
                            </div>
                        </li> 
                    </ItemTemplate>
                </asp:Repeater>

            </ul>
            <footer class="footer">
                <span class="todo-count">
                    <strong><%= TodoCount %></strong> items left
                </span>
                <asp:Button CssClass="access-btn clear-complete" runat="server" OnClick="Clear_Completed" Text="Clear Completed"></asp:Button>
            </footer>
            <% } %>
        </section>
        </section>
</asp:Content>