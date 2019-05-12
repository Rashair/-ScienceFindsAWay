import React, { Component } from 'react';
import MeetingsTable from './MeetingsTable'

class Home extends Component {
  constructor(props) {
    super(props);

    this.state = {
      Meetings: [],
      isLoading: false,
    };
  }

  componentDidMount() {
    // This method is called when the component is first added to the document
    var user = JSON.parse(localStorage.getItem('user'));

    this.setState({
      isLoading: true,
    });

    fetch(`api/meeting/getAllUserMeetings?id=${user.userID}`)
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
    var t;
    if (this.state.isLoading) {
      t = "Here Przemek will add a spinner"
    }
    else {
      t = <MeetingsTable table={this.state.Meetings} />
    }
    return (
      <div class="row">
        <div class="col-sm-6">
          <h1>Your Meetings</h1>
          {t}
        </div>
        <div class="col-sm-6">
          Something will maybe be here…
        </div>

      </div>
    );
  }
}

export default(Home);
