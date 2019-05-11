import React, { Component } from 'react';

class Meetups extends Component {
    constructor(props) {
      super(props);
  
      this.state = {
        meetups: []
      };
    }
  
    componentDidMount() {
      // This method is called when the component is first added to the document
      fetch(`api/meetup/getAllMeetups`)
      .then(res => res.json())
      .then(
        (result) => {
          this.setState({
            meetups: result
          });
        }
      )
    }
  
  
    render() {
      return (
        <div>
          <h1>Meetup list</h1>
          <ul>
            {this.state.meetups.map((meetup) => {
              return (<li>xd</li>);
            })}
          </ul>
        </div>
      );
    }
  }
  
  export default(Meetups);
  
