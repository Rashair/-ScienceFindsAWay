import React, { Component } from 'react';
import { Spinner } from './Spinner';
import NiceDate from './NiceDate.js';

class MessageInfo extends Component {
  constructor(props) {
    super(props);

    this.state = {
      Info: {},
      isLoading: true,
      joined: false,
    };
  }

  componentDidMount() {
    // This method is called when the component is first added to the document
    var user = JSON.parse(localStorage.getItem('user'));

    fetch(`api/meeting/getMeetingsById?id=${this.props.match.params.id}`)
    .then(res => res.json())
    .then(
      (result) => {
        this.setState({
          Info: result,
          isLoading: false,
          joined: result.participants.find((Participant) => {
            return Participant.userID == user.userID;
          }) != null,
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

    var date = new Date(this.state.Info.date);

    var butClass = "btn btn-" + (!this.state.joined ? "primary" : "danger");
      console.log(butClass);
    var butText = !this.state.joined ? "Join" : "Leave";

    return (
      <div>
        <h1>Meeting info</h1>
        <h2>{this.state.Info.name}</h2>
        <h3><NiceDate date={date} /></h3>
        <div className="pb-4">{this.state.Info.description}</div>
        <div className="row">
          <div className="col-sm-4">
            <h2>Participants</h2>
            <ul>
              {this.state.Info.participants.map((Participant, i) => {
                  return (<li key={i}><a href={`/UserInfo/${Participant.userID}`}>{Participant.name} {Participant.surname}</a></li>);
              })}
            </ul>
            <button type="button" className={butClass}>{butText}</button>
          </div>
          <div className="col-sm-4">
            <h2>Categories</h2>
            <ul>
              {this.state.Info.categories.map((Category, i) => {
                  return (<li key={i}>{Category.name}</li>);
              })}
            </ul>
          </div>
          <div className="col-sm-4">
            <h2>Location</h2>
            <div><strong>{this.state.Info.place.name}</strong> - {this.state.Info.place.address}</div>
            <div>{this.state.Info.place.description}</div>
          </div>
        </div>
      </div>
    );
  }
}

export default(MessageInfo);
