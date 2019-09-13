import React, { Component } from 'react';
import './App.css';
import DungeonMap from './DungeonMap'
import RightPanel from './RightPanel'

const endpoint = "https://localhost:44383/api/generate";

class MainPage extends Component {
  constructor(props) {
    super(props)
    this.state = {
      map: undefined
    }
    this.getMap = this.getMap.bind(this);
    this.updateMap = this.updateMap.bind(this);
  }
  
  componentDidMount() {    
    this.updateMap();
  }

  getMap() {
    return this.state.map;
  }

  updateMap() {
    this.setState({map: undefined});
    fetch(endpoint, {
      method: 'GET',
      headers: {
      "Accept": "application/json",
      'Content-Type': 'application/json'
      }
    })
    .then(response => {
      return response.json();
    })
    .then(responseData => {
        return responseData;
    })
    .then(data => {
        this.setState({
          map: data
        })
      }
    )
  }

  render() {

    const map = this.getMap();

    return (
      <div className="app">
          { map == undefined ? "Fetching map ..." : <DungeonMap getMap={this.getMap}/>}
          <RightPanel updateMap={this.updateMap}/>
      </div>
    )
  }
}

export default MainPage;