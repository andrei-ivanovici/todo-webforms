import React, { Fragment } from 'react';
import logo from './logo.svg';
import './App.css';
import {
  TagEditField, TagList, TagTopNavbar,
  TagButton, IListButtonClickArgs,
  IListItemsCheckedArgs
} from "@tag/tag-components-react-v2";

interface AppState {
  todo: string,
  todos: Todo[]
}

interface Todo {
  id: string,
  title: string,
  isCompleted: boolean,
}

class App extends React.Component<any, AppState> {

  constructor(props: any) {
    super(props);
    this.state = {
      todo: '',
      todos: this._todos
    }
  }

  private _todos: Todo[] = [
    { id: "1", title: 'Walk the dog', isCompleted: true },
    { id: "2", title: 'Mail the document', isCompleted: true },
  ];

  deleteTodo = (e: CustomEvent<IListButtonClickArgs>) => {
    console.log(e.detail.item);
    //TODO: call api
    this.setState({
      todos: [...this.state.todos.filter(t => t.id != e.detail.item.id)]
    })
  }

  todoStateChanged = (e: CustomEvent<IListItemsCheckedArgs>) => {
    //Only one item can change, the select all option is disabled
    let changedItem = e.detail.changes[0];
    console.log(changedItem);
    //TODO: call api
    let oldItem = this.state.todos.find(t => t.id === changedItem.item.id);
    if (oldItem !== undefined) {
      oldItem.isCompleted = changedItem.checked;
      this.setState({
        todos: [...this.state.todos]
      })
    }
  }

  addNewTodoItem = (e: any) => {
    if (e.key === 'Enter') {
      console.log(e.target.value);
      let todoTitle = e.target.value;
      //TODO: call api
      setTimeout(() => {
        this.setState({
          todo: '',
          todos: [...this.state.todos, {
            id: Date.now().toString(),
            title: todoTitle,
            isCompleted: false
          }]
        })
      }, 0);
    }
  }

  clearCompleted = () => {
    console.log("Clear completed")
    //TODO: call api
    this.setState({
      todos: [...this.state.todos.filter(t => !t.isCompleted)]
    })
  }

  public render = () => {
    let noCompletedItems = this.state.todos.filter(t => t.isCompleted).length;
    let noTodos = this.state.todos.length;
    return (
      <div className="App" style={{ backgroundColor: "#f7f8fa", height: "100%" }}>
        <div style={{ backgroundColor: "#f7f8fa" }}>
          <TagTopNavbar name='Access todos' />

          <div style={{
            paddingTop: "50px",
            display: "flex",
            flexDirection: "column",
            alignItems: "center",
            minWidth: "500px"
          }}>
            <TagEditField
              name='todo'
              value={this.state.todo}
              placeholder='What needs to be done?'
              autocomplete='off' onKeyDown={this.addNewTodoItem} >
            </TagEditField>

            <TagList
              keyField="id"
              checkedItems={[...this.state.todos.filter(t => t.isCompleted)]}
              data={this.state.todos}
              multi-check
              item-container-style-field='is'
              primary-field-style-field='is'
              primary-field='title'
              multi-check-suppress-select-all='true'
              onButtonClick={this.deleteTodo}
              onListItemsChecked={this.todoStateChanged}
              button-1='Delete'>
            </TagList>

            {noTodos > 0 && (<Fragment>
              <div>{noTodos} {noTodos == 1 ? 'item' : 'items'} left</div>
              <TagButton text='Clear completed'
                onClick={this.clearCompleted}
                badge={noCompletedItems == 0 ? undefined : noCompletedItems.toString()}
                badge-accent='primary' badge-inline='false' />
            </Fragment>)}

          </div>
        </div>
      </div>
    )
  }
}

export default App;
