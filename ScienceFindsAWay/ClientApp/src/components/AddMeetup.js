import React, { Component } from 'react';

class AddMeetup extends Component {
    constructor(props) {
      super(props);
  
      this.state = {
        user: {},
        places: {}
      };
    }
  
    componentDidMount() {
      // This method is called when the component is first added to the document
      this.setState({
        user: JSON.parse(localStorage.getItem('user')),
      });
      fetch(`api/place/getAllPlaces`)
      .then(res => res.json())
      .then(
        (result) => {
          this.setState({
            places: result
          });
        }
      )
    }
  
  
    render() {
      return (
        <div>
          <h1>Create new Meetup</h1>
           <section> {this.state.places[0]}</section>
           <section> {this.state.user.password}</section>
        </div>
      );
    }
  }
  
  export default(AddMeetup);
  