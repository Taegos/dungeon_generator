import React, { Component } from 'react';
import {FormGroup, ControlLabel, Button , Form, FormControl, InputGroup } from 'react-bootstrap'

const divStyle = {
  color: "white",
  right: "1vw",
  left: "83vw",
  top: "28vh",
  bottom: "28vh",
  position: "absolute",
  backgroundColor: "green"
};

const buttonStyle = {
  height: "25%",
  width: "100%",
  position: "absolute",
  bottom: 0
}

class RightPanel extends Component {
  constructor(props) {
    super(props)
    this.submit = this.submit.bind(this);
  }

  submit(e) {
    this.props.updateMap();
    e.preventDefault()
  }

  render() {
    return (

      <Form style={divStyle} onSubmit={this.submit}>
        <FormGroup controlId="formBasicText">
          <ControlLabel>Width</ControlLabel>
          <FormControl type="text" />
        </FormGroup>
        <FormGroup controlId="formValidationError4">
          <ControlLabel>Height</ControlLabel>
          <FormControl type="text" />
        </FormGroup>
        <Button type="submit" style= {buttonStyle}>Generate</Button>
      </Form>

  
    );
  }
}

export default RightPanel;