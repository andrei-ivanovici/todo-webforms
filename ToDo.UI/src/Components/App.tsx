import React from 'react';
import './../App.css';
import { TagTopNavbar } from "@tag/tag-components-react-v2";
import { TodoComponent } from './Todo';
import { appContainerStyle, appTodoContainerStyle } from './../Styles';

export const App: React.FC = () => {

  return (
    <div className="App" style={appContainerStyle}>

      <TagTopNavbar name='Access todos' />

      <div style={appTodoContainerStyle}>
        <TodoComponent></TodoComponent>
      </div>

    </div>
  )
}
