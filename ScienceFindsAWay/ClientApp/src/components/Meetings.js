import React, { Component } from 'react';
import MeetingsTable from './MeetingsTable'
import { Spinner } from './Spinner';

class Meetings extends Component {
    constructor(props) {
      super(props);
  
      this.state = {
        Meetings: [],
        isLoading: false,
      };
    }
  
    componentDidMount() {
      this.setState({
        isLoading:true,
      });

      fetch(`api/meeting/getAllMeetings`)
      .then(res => res.json())
      .then(
        (result) => {
          this.setState({
            Meetings: result,
            isLoading: false,
          });
        }
      )
    }
  
  
    render() {
      if(this.state.isLoading)
      {
        return(
          <Spinner />
        )
      }

      return (
        <div>
          <h1>Meeting list</h1>
          <MeetingsTable table={this.state.Meetings} />
        </div>
      );
    }
  }
  
  export default(Meetings);
  
