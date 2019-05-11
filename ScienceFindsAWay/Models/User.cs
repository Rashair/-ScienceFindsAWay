using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScienceFindsAWay.Models
{
    public class User
    {
        public string Name { get; protected set; }
        public string Surname { get; protected set; }
        public string University { get; protected set; }
        public string Faculty { get; protected set; }
        public string Mail { get; protected set; }
        public string Username { get; protected set; }
        public int UserID { get; }
        public Skill[] Skills { get; protected set; }
        internal string _password;
        public string PasswordSalt { get; protected set; }
        

        public User(string n, string s, string u, string f,string m, int uid, Skill[] sk, string username, string password, string passwordSalt)
        {
            Name = n;
            Surname = s;
            University = u;
            Faculty = f;
            Mail = m;
            Username = username;
            UserID = uid;
            Skills = sk?.ToArray();
            _password = password;
            PasswordSalt = passwordSalt;
        }

        public bool CheckPassword(string pass)
        {
            return pass == _password;
        }
    }
}
