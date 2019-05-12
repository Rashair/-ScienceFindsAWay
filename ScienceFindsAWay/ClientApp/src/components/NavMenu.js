import React from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './css/NavMenu.css';

export default class NavMenu extends React.Component {
  constructor(props) {
    super(props);

    this.logo = process.env.PUBLIC_URL + '/consulting.png'
    this.toggle = this.toggle.bind(this);
    this.state = {
      isOpen: false,
    };
  }

  toggle() {
    this.setState({
      isOpen: !this.state.isOpen,
    });
  }

  render() {
    return (
      <header>
        <Navbar className="navbar-expand-sm navbar-toggleable-sm border-bottom box-shadow mb-3" light >
          <Container>
            <img alt="" src={this.logo} style={{ width: 50, padding: 5 }} />
            <NavbarBrand tag={Link} to="/">ScienceFindsAWay</NavbarBrand>
            <NavbarToggler onClick={this.toggle} className="mr-2" />
            <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={this.state.isOpen} navbar>
            {this.props.user !== null &&
              <ul className="navbar-nav flex-grow">
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/AddMeeting">Add a Meeting</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/Meetings">All Meetings</NavLink>
                </NavItem>
                  <NavItem>
                    <NavLink tag={Link} className="text-dark" to="/Login">Logout</NavLink>
                  </NavItem>
                </ul>
              }
            </Collapse>
          </Container>
        </Navbar>
      </header>
    );
  }
}
