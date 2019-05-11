import React, { Component } from 'react';

class UserMeetings extends Component {
    constructor(props) {
      super(props);
  
      this.state = {
        Meetings: []
      };
    }
  
    componentDidMount() {
      // This method is called when the component is first added to the document
      fetch(`api/meeting/getAllMeetings`)
      .then(res => res.json())
      .then(
        (result) => {
          this.setState({
            Meetings: result
          });
        }
      )
    }
  
  
    render() {
      return (
        <div>
          <h1>Meeting list TODO</h1>
          <table id="Meetings" className="table table-responsive">
            <thead>
              <tr>
                <th>Name</th>
                <th>Date</th>
                <th>Place</th>
              </tr>
            </thead>
            <tbody>
              {this.state.Meetings.map((Meeting) => {
                return (<tr>
                  <td>{Meeting.name}</td>
                  <td>{Meeting.date}</td>
                  <td>{Meeting.place.name}</td>
                </tr>);
              })}
            </tbody>
          </table>
        </div>
      );
    }
  }
  
  export default(UserMeetings);
  
