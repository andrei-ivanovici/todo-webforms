import React from 'react';
import { BaseUrl } from '../Constants'
import axios from 'axios';
import { TagEditField, TagList, TagButton, IListButtonClickArgs, IListItemsCheckedArgs } from "@tag/tag-components-react-v2";
import { TodoComponentState, TodoComponentProps } from '../Models';
import { LeftItemsLabel } from './LeftItemsLabel';
import { todoContainerStyle, todoFooterContainerStyle, leftItemsLabelStyle } from '../Styles';

export class TodoComponent extends React.Component<TodoComponentProps, TodoComponentState> {

    constructor(props: TodoComponentProps) {
        super(props);
        this.state = {
            newTodoTitle: '',
            todos: []
        }
    }

    componentDidMount() {
        axios
            .get(`${BaseUrl}/todos`)
            .then(res => {
                const todos = res.data;
                this.setState({ todos });
            })
    }

    addNewTodoItem = (e: any) => {
        if (e.key === 'Enter') {
            let todoTitle = e.target.value;
            let todoRequestBody = { title: todoTitle, isCompleted: false }

            axios
                .post(`${BaseUrl}/todos`, todoRequestBody)
                .then(res => {
                    const receivedTodo = res.data;
                    this.setState({
                        newTodoTitle: '',
                        todos: [...this.state.todos, receivedTodo]
                    })
                })
        }
    }

    todoStateChanged = (e: CustomEvent<IListItemsCheckedArgs>) => {
        //Only one item can change, the select all option is disabled
        let changedItem = e.detail.changes[0];

        axios
            .patch(`${BaseUrl}/todos/${changedItem.item.id}`, { isCompleted: changedItem.checked })
            .then(_ => {
                let oldItem = this.state.todos.find(t => t.id === changedItem.item.id);
                if (oldItem !== undefined) {
                    oldItem.isCompleted = changedItem.checked;
                    this.setState({
                        todos: [...this.state.todos]
                    })
                }
            })
    }

    deleteTodo = (e: CustomEvent<IListButtonClickArgs>) => {
        axios
            .delete(`${BaseUrl}/todos/${e.detail.item.id}`)
            .then(_ => {
                this.setState({
                    todos: [...this.state.todos.filter(t => t.id != e.detail.item.id)]
                })
            })
    }

    clearCompleted = () => {
        axios
            .delete(`${BaseUrl}/todos/completed`)
            .then(res => {
                this.setState({
                    todos: [...this.state.todos.filter(t => !t.isCompleted)]
                })
            })
    }

    public render = () => {
        let noCompletedItems = this.state.todos.filter(t => t.isCompleted).length;
        let noTodos = this.state.todos.length;
        let displayFooter = noTodos > 0;

        return (
            <div style={todoContainerStyle}>
                <TagEditField
                    name='todo'
                    value={this.state.newTodoTitle}
                    placeholder='What needs to be done?'
                    autocomplete='off' onKeyDown={this.addNewTodoItem} >
                </TagEditField>

                <TagList
                    keyField="id"
                    checkedItems={[...this.state.todos.filter(t => t.isCompleted)]}
                    data={this.state.todos}
                    multi-check
                    primary-field='title'
                    multi-check-suppress-select-all='true'
                    onButtonClick={this.deleteTodo}
                    onListItemsChecked={this.todoStateChanged}
                    button-1='Delete'>
                </TagList>

                {displayFooter && (<div style={todoFooterContainerStyle}>
                    <LeftItemsLabel style={leftItemsLabelStyle} noItems={noTodos}> </LeftItemsLabel>
                    <TagButton
                        text='Clear completed'
                        onClick={this.clearCompleted}
                        badge={noCompletedItems == 0 ? undefined : noCompletedItems.toString()}
                        badge-accent='primary' badge-inline='false' />
                </div>)}
            </div>
        )
    }
}
