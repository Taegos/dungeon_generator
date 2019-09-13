import React, { Component } from 'react';
import './App.css';
import Draggable from 'react-draggable';
import Vec2D from 'vec2';

const divStyle = {
  height: '100vh',
  backgroundColor:'#110D16',
};
 
const handleStyle = {
  overflow: 'hidden',
  position: 'fixed',
};

const canvasStyle = {
  backgroundColor: 'red'
};

const tileColor = "#3B3740";

class DungeonMap extends Component {

  constructor(props) {
    super(props);
    const map = this.props.getMap();
    const tileSize = 1;

    this.state = {
      tileSize: tileSize,
      defaultPosition: {
        x: window.innerWidth / 2 - (map.length * tileSize) / 2,
        y: window.innerHeight / 2 - (map[0].length * tileSize) / 2
      },  
      canvasSize: {
        x: map.length * tileSize,
        y: map[0].length * tileSize
      }
    }
    this.redrawCanvas = this.redrawCanvas.bind(this);
    this.incrZoomLevel = this.incrZoomLevel.bind(this);
  }
  
  componentDidMount() { 
    this.redrawCanvas();   
  }

  componentDidUpdate() {
    this.redrawCanvas();
  }

  incrZoomLevel(e) {
    var scale = .5
    if (e.nativeEvent.wheelDelta > 0) {
      scale = 2
    } 

    var canvasSize = this.state.canvasSize;
    var tileSize = this.state.tileSize;

    // canvasSize.x *= scale;
    // canvasSize.y *= scale;
    // tileSize *= scale;

    // this.setState({
    //   canvasSize: canvasSize,
    //   tileSize: tileSize
    // })
    const canvas = this.refs.canvas;
    const ctx = canvas.getContext('2d');
    ctx.scale(scale, scale);
    e.preventDefault();
  }

  redrawCanvas() {
    const map = this.props.getMap();
    const canvas = this.refs.canvas;
    const ctx = canvas.getContext('2d');
    const tileSize = this.state.tileSize;
    const defaultPosition = this.state.defaultPosition;
    console.log(defaultPosition)

    ctx.fillStyle = tileColor;

    for (var x = 0; x < map.length; x++) {
      for (var y = 0; y < map[0].length; y++) {
        if (!map[x][y]) {
          ctx.fillRect(
            x * tileSize , 
            y * tileSize , 
            tileSize, 
            tileSize);
        }
      }
    }
  }

  render() {

    const defaultPosition = this.state.defaultPosition;
    const canvasSize = this.state.canvasSize;
    // const canvas = this.refs.canvas;
    // if (canvas != undefined) {

    //   const ctx = canvas.getContext('2d');
    //   ctx.translate(defaultPosition.x, defaultPosition)
    //   console.log("ASD")
    // }

    return (   
      <div className="DungeonMap" style={divStyle} onWheel={(e) => {this.incrZoomLevel(e)}} >
        {/* <Draggable axis="both" handle=".handle" defaultPosition={this.state.defaultPosition}> */}
     
        <canvas ref="canvas" style={canvasStyle} width={canvasSize.x} height={canvasSize.y}/>

        {/* </Draggable>              */}
      </div>
    );
  }
}

export default DungeonMap;
