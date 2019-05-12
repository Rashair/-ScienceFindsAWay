import React, { Component } from 'react';
import { Spinner } from './Spinner';

class Home extends Component {
  constructor(props) {
    super(props);

    this.state = {
      Info: {},
      isLoading: true,
    };
  }

  componentDidMount() {
    // This method is called when the component is first added to the document
    fetch(`api/meeting/getMeetingsById?id=${this.props.match.params.id}`)
    .then(res => res.json())
    .then(
      (result) => {
        console.log(result);
        this.setState({
          Info: result,
          isLoading: false,
        });
      }
    )
  }


  render() {
    if (this.state.isLoading) {
      return (
        <Spinner/>
      )
    }

    console.log(this.state);
    return (
      <div>
        <h1>Meeting info</h1>
        <h2>{this.state.Info.name}</h2>
        <h3>{this.state.Info.date}</h3>
        <div class="pb-4">{this.state.Info.description}</div>
        <div class="row">
          <div class="col-sm-4">
            <h2>Participants</h2>
            <ul>
              {this.state.Info.participants.map((Participant, i) => {
                  return (<li>{Participant.name} {Participant.surname}</li>);
              })}
            </ul>
          </div>
          <div class="col-sm-4">
            <h2>Categories</h2>
            <ul>
              {this.state.Info.categories.map((Category, i) => {
                  return (<li>{Category.name}</li>);
              })}
            </ul>
          </div>
          <div class="col-sm-4">
            <h2>Location</h2>
            <div><strong>{this.state.Info.place.name}</strong> @ {this.state.Info.place.address}</div>
            <div>{this.state.Info.place.description}</div>
          </div>
        </div>
      </div>
    );
  }
}

export default(Home);
