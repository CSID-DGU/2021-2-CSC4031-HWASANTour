using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Converters;


public class LoginRequest
{
    public string username { get; set; }
    public string password { get; set; }

    public LoginRequest(string _username, string _password)
    {
        username = _username;
        password = _password;
    }

}