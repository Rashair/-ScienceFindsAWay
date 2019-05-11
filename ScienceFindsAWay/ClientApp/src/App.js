import React from 'react';
import { Route } from 'react-router';
import { Container } from 'reactstrap';
import NavMenu from './components/NavMenu';
import Home from './components/Home';
import Login from './components/Login';
import AddMeeting from './components/AddMeeting';
import Meetings from './components/Meetings';
import UserMeetings from './components/UserMeetings';
import PrivateRoute from './authentication/PrivateRoute';

class App extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      user: JSON.parse(localStorage.getItem('user')),
    };
  }

  loginHandler = (u) => {
    this.setState({
      user: u,
    })
  }

  render() {
    return (
      <div>
        <NavMenu user={this.state.user} />
        <Container>
          <Route path="/login" render={(props) => <Login {...props} loginHandler={this.loginHandler}  />} />
          <PrivateRoute exact path="/" component={Home} />
          <PrivateRoute path='/addMeeting' component={AddMeeting} />
          <PrivateRoute path='/meetings' component={Meetings} />
          <PrivateRoute path='/userMeetings' component={UserMeetings} />
        </Container>
      </div>
    );
  }
}

export default App;
