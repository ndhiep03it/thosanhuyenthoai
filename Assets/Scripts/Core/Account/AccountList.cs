using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Account
{
    public string username;
    public string password;
    public string timerLogin;
}

[System.Serializable]
public class AccountList 
{
    public List<Account> accounts;
}
