import React, { Component } from 'react';
import './Meetups.css'

class Meetups extends Component {
    constructor(props) {
      super(props);
  
      this.state = {
        meetups: []
      };
    }
  
    componentDidMount() {
      // This method is called when the component is first added to the document
      fetch(`api/meeting/getAllMeetings`)
      .then(res => res.json())
      .then(
        (result) => {
          this.setState({
            meetups: result
          });
        }
      )

            /*
        this.setState({
            meetups: [{name: "Lol", date: "never", place: "311", categories: [], participants: []},
            {name: "C", date: "d", place: "aisodjdijadoiasijd", categories: [], participants: []}]
        })
        */
    }
  
  
    render() {
      return (
        <div>
          <h1>Meetup list</h1>
          <table id="meetups">
            <thead><tr>
              <td>Name</td>
              <td>Date</td>
              <td>Place</td>
            </tr></thead>
            <tbody>
              {this.state.meetups.map((meetup) => {
                return (<tr>
                  <td>{meetup.name}</td>
                  <td>{meetup.date}</td>
                  <td>{meetup.place}</td>
                </tr>);
              })}
            </tbody>
          </table>
        </div>
      );
    }
  }
  
  export default(Meetups);
  
