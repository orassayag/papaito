using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LoginUser
/// </summary>
public class LoginUser
{
    private int _loginID;
    private string _userID;
    private string _password;
    private bool _isLoggedIn;
    private DateTime _createTime;
    private DateTime _lastLogin;
    private bool _active;


	public LoginUser(int loginID, string userID, string password)
	{
        if (loginID > 0 && userID != "" && password != "")
        {
            this._loginID = loginID;
            this._userID = userID;
            this._password = password;
            this._isLoggedIn = false;
            this._lastLogin = TimeNow.TheTimeNow;
            this._createTime = TimeNow.TheTimeNow;
        }
	}

    public void Login()
    {
        _isLoggedIn = true;
        this._lastLogin = TimeNow.TheTimeNow;
    }

    public void Logoff()
    {
        this._loginID = 0;
        _isLoggedIn = false;
        this._lastLogin = default(DateTime);
    }

    public int LoginID
    {
        get { return this._loginID; }
    }

    public string UserID
    {
        get { return this._userID; }
        set
        {
            if (value != "" && value != this._userID)
            {
                this._userID = value;
            }
        }
    }

    public string Password
    {
        get { return this._password; }
        set
        {
            if (value != "" && value != this._password)
            {
                this._password = value;
            }
        }
    }

    public bool IsLoggedIn
    {
        get { return this._isLoggedIn; }
        set
        {
            if (value != this._isLoggedIn)
            {
                this._isLoggedIn = value;
            }
        }
    }

    public DateTime LastLogin
    {
        get { return this._lastLogin; }
        set
        {
            if (value != TimeNow.TheTimeNow && value != default(DateTime))
            {
                this._lastLogin = value;
            }
        }
    }

    public DateTime CreateTime
    {
        get { return this._createTime; }
    }

    public bool Active
    {
        get { return this._active; }
        set
        {
            if (value != this._active)
            {
                this._active = value;
            }
        }
    }
}
