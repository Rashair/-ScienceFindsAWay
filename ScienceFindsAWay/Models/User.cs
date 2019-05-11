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
        private string _password;
        public byte[] _passwordSalt = new byte[128/8];
        

        public User(string n, string s, string u, string f,string m, int uid, Skill[] sk, string username, string password, byte[] passwordSalt)
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
            _passwordSalt = passwordSalt?.ToArray();
        }

        public bool CheckPassword(string pass)
        {
            return pass == _password;
        }
    }
}
