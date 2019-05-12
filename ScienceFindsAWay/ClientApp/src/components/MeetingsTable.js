import React, { Component } from 'react';
import NiceDate from './NiceDate.js'

class MeetingsTable extends Component {
    render() {
        this.items = this.props.table.map((Meeting, i) => {
            var date = new Date(Meeting.date);
            return (
                <tr key={i}>
                    <td><a href={`/MeetingInfo/${Meeting.meetingId}`}>{Meeting.name}</a></td>
                    <td>
                        <NiceDate date={date} />
                    </td>
                    <td>{Meeting.place.name}</td>
                </tr> 
            );}        
        );

        return (
            <div className="table-responsive"><table id="Meetings" className="table">
                <thead>
                    <tr>
                        <th width="33.33%">Name</th>
                        <th width="33.33%">Date</th>
                        <th>Place</th>
                    </tr>
                </thead>
                <tbody>
                    {this.items}
                </tbody>
            </table></div>
        )
    }
}

export default MeetingsTable;
