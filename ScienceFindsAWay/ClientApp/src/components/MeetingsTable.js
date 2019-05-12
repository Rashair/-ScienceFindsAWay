import React, { Component } from 'react';

class MeetingsTable extends Component {
    render() {
        this.items = this.props.table.map((Meeting, i) => {
            var date = new Date(Meeting.date);
            return (
                <tr key={i}>
                    <td><a href={`/MeetingInfo/${Meeting.meetingId}`}>{Meeting.name}</a></td>
                    <td>
                        {date.toLocaleDateString()} {date.toLocaleTimeString().slice(0, 5)}
                    </td>
                    <td>{Meeting.place.name}</td>
                </tr> 
            );}        
        );

        return (
            <table id="Meetings" className="table table-responsive">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Date</th>
                        <th>Place</th>
                    </tr>
                </thead>
                <tbody>
                    {this.items}
                </tbody>
            </table>
        )
    }
}

export default MeetingsTable;
