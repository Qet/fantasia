using System;
using System.Collections.Generic;

namespace world
{
    public class Realm : IRealm
    {
        public List<User> users {get; private set;}
        
        public Realm(){
            users = new List<User>();
        }
                
        public ICommand AddUser(){
            User newUser = new User();
            users.Add(newUser);
            return newUser;
        }
    }
}
