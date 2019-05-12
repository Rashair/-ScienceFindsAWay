import React, { Component } from 'react';

class AddMeeting extends Component {
  constructor(props) {
    super(props);

    this.handleChange = this.handleChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);

    this.state = {
      places: [],
      categories1: [],
      categories2: [],
      categories3: [],
      meeting: {
        name: "",
        desc: "",
        date: {},
        place: {},
        categories: [],
        participants: [],
      },
      error: "",
    };
  }

  handleChange(e) {
    this.setState({ meeting: { ...this.state.meeting, [e.target.name]: e.target.value } });
  }

  handleCat1Choice(e)  {
    const s = e.target;
    const elementId = s[s.selectedIndex].id;
    fetch(`api/category/GetSlaveCategories?id=${elementId}`)
    .then(res => res.json())
    .then((result) => {
      this.setState({ meeting: { ...this.state.meeting, categories2: result } });
    });
  }

  handleSubmit(e) {
    e.preventDefault();
    console.log(this.state.meeting)
    const requestOptions = {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(this.state.meeting)
    };

    fetch(`api/meeting/addMeeting`, requestOptions)
    .then(res => res.json())
    .then(
        (result) => {
          console.log(result);
        },
        (err) => {
          this.setState({ error: "Adding category failed"});
        }
    );
  }

  componentDidMount() {
    let currUser = JSON.parse(localStorage.getItem('user'));
    delete currUser.authdata;
    Promise.all([
      fetch(`api/place/getAllPlaces`),
      fetch(`api/category/GetCategoriesByLevel?level=1`),
    ])
    .then(([res1, res2]) => Promise.all([res1.json(), res2.json()]))
    .then(([data1, data2]) => {
      this.setState({
        places: data1,
        categories1: data2,
      });
    });
  }

  render() {
    const places = this.state.places.map((place, i) => <option key={i}>{place.name}</option>);
    const categoriesLev1 = this.state.categories1.map((category, i) => 
      <option key={i} id={category.categoryID}>{category.name}</option>
    );
    const categoriesLev2 = this.state.categories2.map((category, i) => 
      <option key={i}>{category.name}</option>
    );
    const categoriesLev3 = this.state.categories3.map((category, i) => 
      <option key={i}>{category.name}</option>
    );

    return (
      <div>
        <h2>Create a new meeting</h2>
        <form onSubmit={this.handleSubmit}>
          <div className="form-group">
            <label>
              Name:
              <input className="form-control" name="name" type="text" onChange={this.handleChange} />
            </label>
          </div>
          <div className="form-group">
            <label>
              Description:
              <textarea className="form-control" name="desc" type="text" onChange={this.handleChange} />
            </label>
          </div>
          <div className="form-group">
            <label>
              Date:
              <input className="form-control" name="date" type="date" onChange={this.handleChange} />
            </label>
          </div>
          <div className="form-group">
            <label>
              Place:
              <select className="form-control" name="place" onChange={this.handleChange}>
                <option>Choose a place...</option>
                {places}
              </select>
            </label>
          </div>
          <div className="row">
            <div className="form-group col-sm-3">
              <label>
                General category:
                <select className="form-control" name="category1" onChange={this.handleCat1Choice}>
                  <option>Choose a category...</option>
                  {categoriesLev1}
                </select>
              </label>
            </div>
            <div className="form-group col-sm-3">
              <label>
                Detailed category (optional):
                <select className="form-control" name="category2" onChange={this.handleChange}>
                  <option>Choose a category...</option>
                  {categoriesLev2}
                </select>
              </label>
            </div>
            <div className="form-group col-sm-3">
              <label>
                Area (optional):
                <select className="form-control" name="category3" onChange={this.handleChange}>
                  <option>Choose an area...</option>
                  {categoriesLev3}
                </select>
              </label>
            </div>
          </div>
          <button className="btn btn-primary" type="submit">Submit</button>
        </form>
        {this.state.error &&
          <div className={'alert alert-danger'}>{this.state.error}</div>
        }
      </div>
    );
  }
}

export default (AddMeeting);

