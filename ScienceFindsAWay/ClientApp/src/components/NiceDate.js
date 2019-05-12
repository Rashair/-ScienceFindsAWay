import React, { Component } from 'react';

class NiceDate extends Component {
    render() {
        var date = this.props.date;
        return `${date.toLocaleDateString()} ${date.toLocaleTimeString().slice(0, 5)}`;
    }
}

export default NiceDate;
