using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScienceFindsAWay.Models
{
    public class Meeting
    {

        public int MeetingId { get; private set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public Place Place { get; set; }
        public List<Category> Categories { get; set; }
        public List<User> Participants { get; set; }

        public Meeting(int _meetingId, string name, DateTime date, Place placeName, List<Category> categories, List<User> participants)
        {
            MeetingId = _meetingId;
            Name = name;
            Date = date;
            Place = placeName;
            Categories = categories;
            Participants = participants;
        }
    }
}
