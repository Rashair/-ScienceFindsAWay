import React, { Component } from 'react';
import { Spinner } from './Spinner';

class UserInfo extends Component {
  constructor(props) {
    super(props);

    this.state = {
      Info: {},
      isLoading: true,
    };
  }

  componentDidMount() {
    // This method is called when the component is first added to the document
    fetch(`api/user/getUsersWithId?id=${this.props.match.params.id}`)
    .then(res => res.json())
    .then(
      (result) => {
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

    return (
      <div>
        <h1>User info</h1>
        <h2>{this.state.Info.name} {this.state.Info.surname}</h2>
        <div>{this.state.Info.university} — {this.state.Info.faculty}</div>
        <div className="pb-4"><a href={`mailto:${this.state.Info.mail}`}>{this.state.Info.mail}</a></div>
        <h3>Skills</h3>
        <ul>
          {this.state.Info.skills.map((Skill, i) => {
            return (<li key={i}>{Skill.categoryGeneral.name} / {Skill.categoryMedium.name} / {Skill.categorySpecific.name}</li>);
          })}
        </ul>
      </div>
    );
  }
}

export default(UserInfo);
