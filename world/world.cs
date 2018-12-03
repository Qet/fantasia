using System;
using System.Collections.Generic;

namespace world
{
    public class world
    {
        public List<User> users {get; private set;}
        
        public world(){
            users = new List<User>();
        }
                
        public User AddUser(){
            User newUser = new User();
            users.Add(newUser);
            return newUser;
        }
    }
}
