import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import Login from './components/Login';
import Meetup from './components/Meetup';
import Meetups from './components/Meetups';
import PrivateRoute from './authentication/PrivateRoute';

export default () => (
  <Layout>
        <Route path="/login" component={Login} />
        <PrivateRoute exact path="/" component={Home} />
        <PrivateRoute path='/meetup' component={Meetup} />
        <PrivateRoute path='/meetups' component={Meetups} />
  </Layout>
);
