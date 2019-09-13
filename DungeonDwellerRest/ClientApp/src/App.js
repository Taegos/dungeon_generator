import React, { Component } from 'react';
import './App.css';
import MainPage from './MainPage'

class App extends Component {
  constructor(props) {
    super(props);
    this.state = { 
      currentPage: <MainPage/>   
    };
  }

  render() {
    return (
      <div className="app" >
        { this.state.currentPage }
      </div>
    );
  }
}

export default App;