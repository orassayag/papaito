using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dal;
using System.Reflection;
using System.Drawing;

public partial class Admin_Admin : System.Web.UI.MasterPage
{
    private GlobalFunctions globalFunctions;
    private PapaDal papaDal;
    private Logger logger;
    private Notifier notifier;
    private LoginUser loginUser;

    public GlobalFunctions _GlobalFunctions
    {
        get { return this.globalFunctions; }
        set
        {
            if (value != null)
            {
                this.globalFunctions = value;
            }
        }
    }

    public PapaDal _PapaDal
    {
        get { return this.papaDal; }
        set
        {
            if (value != null)
            {
                this.papaDal = value;
            }
        }
    }

    public Logger _Logger
    {
        get { return this.logger; }
        set
        {
            if (value != null)
            {
                this.logger = value;
            }
        }
    }

    public LoginUser _LoginUser
    {
        get { return this.loginUser; }
        set
        {
            if (value != null)
            {
                this.loginUser = value;
            }
        }
    }

    public Notifier _Notifier
    {
        get { return this.notifier; }
        set
        {
            if (value != null)
            {
                this.notifier = value;
            }
        }
    }

    public bool _MainMenu
    {
        get { return this.mainMenu.Visible; }
        set
        {
            if (value != this.mainMenu.Visible)
            {
                this.mainMenu.Visible = value;
            }
        }
    }

    public bool _Login
    {
        get { return this.login.Visible; }
        set
        {
            if (value != this.login.Visible)
            {
                this.login.Visible = value;
            }
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        this.papaDal = new PapaDal();
        this.notifier = new Notifier();
        this.logger = new Logger();
        this.globalFunctions = new GlobalFunctions();

        if (!Page.IsPostBack)
        {
            if (this.papaDal.GetCount("admin") == 0)
            {
                AdminUser f = new AdminUser
                {
                    LoginID = this.papaDal.GetNextAvailableID("admin"),
                    CreateTime = TimeNow.TheTimeNow,
                    spCreateTime = TimeNow.TheTimeNow.ToString(),
                    LastLogin = TimeNow.TheTimeNow,
                    LastUpdate = TimeNow.TheTimeNow,
                    spLastUpdate = TimeNow.TheTimeNow.ToShortDateString(),
                    spLastLogin = TimeNow.TheTimeNow.ToShortDateString(),
                    Active = 1,
                    spActive = "Enable",
                    Password = "papaito",
                    UserID = "itay"
                };

                try
                {
                    if (this.papaDal.GetAdminUser(f.UserID, f.Password) == null)
                    {
                        this.papaDal.Add("admin", f);
                        this.notifier.Notify(9, "White", f.UserID);
                        this.logger.Log(new AdminException(string.Format(". {0} Was Added Successfully On {1}", f.UserID, TimeNow.TheTimeNow)),
                        MethodBase.GetCurrentMethod().Name);
                    }
                }
                catch (Exception g)
                {
                    this.logger.Error(g, MethodBase.GetCurrentMethod().Name);
                    this.notifier.Notify(10, "Red", f.UserID);
                    return;
                }
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.papaDal.GetCount("log") >= 100)
        {
            try
            {
                this.globalFunctions.WriteLogToFile(TimeNow.TheTimeNow);
                this.papaDal.DeleteAllByType("log");
            }
            catch (Exception p)
            {
                this.logger.Error(p, MethodBase.GetCurrentMethod().Name);
                this.notifier.Notify(23, "Red", "");
            }
        }

        if (this.Session["login"] != null)
        {
            this.loginUser = (LoginUser)this.Session["login"];
            if (this.loginUser.IsLoggedIn)
            {
                this.mainMenu.Visible = true;
                this.login.Visible = false;
                this.login.Attributes["class"] = "mailNo";
                this.mainMenu.Attributes["class"] = "mailYes";
            }
        }
        else
        {
            this.login.Visible = true;
            this.login.Attributes["class"] = "mailYes";
            this.mainMenu.Attributes["class"] = "mailNo";
            this.mainMenu.Visible = false;
        }

        if (this.Session["tab"] != null)
        {
            this.SelectTab((string)this.Session["tab"]);
        }
    }

    protected void loginBut_Click(object sender, EventArgs e)
    {
        if (this.getUserID.Text == "")
        {
            this.errorLoginLabel.Text = "Please Enter User ID";
            return;
        }

        if (this.getPassword.Text == "")
        {
            this.errorLoginLabel.Text = "Please Enter Password";
            return;
        }

        AdminUser m = this.papaDal.GetAdminUser(this.getUserID.Text, this.getPassword.Text);
        if (m != null)
        {
            if (m.Active == 2)
            {
                this.logger.Error(new AdminException(". (m.Active == 2)"), MethodBase.GetCurrentMethod().Name);
                this.errorLoginLabel.Text = this.notifier.Notify(70, "Red", m.UserID)[0];
                return;
            }

            this.loginUser = new LoginUser(int.Parse(m.LoginID), m.UserID, m.Password);
            this.loginUser.Login();
            this.Session["login"] = this.loginUser;
        }
        else
        {
            this.errorLoginLabel.Text = "Incorrect User ID Or Password";
            return;
        }

        this.mainMenu.Visible = true;
        this.login.Visible = false;

        this.Session["tab"] = "tabs1";
        Response.Redirect("~/Admin/Home.aspx");

    }

    public void forgot_Click(object sender, EventArgs e)
    {
        if (this.recoverMail.Text == "" || this.recoverMail.Text == null)
        {
            this.logger.Error(new AdminException(". (this.recoverMail.Text == \"\")"), MethodBase.GetCurrentMethod().Name);
            return;
        }

        if (!this.globalFunctions.ValidateMail(this.recoverMail.Text))
        {
            this.logger.Error(new AdminException(". (!this.globalFunctions.ValidateMail(this.recoverMail.Text))"), MethodBase.GetCurrentMethod().Name);
            return;
        }

        try
        {
            this.globalFunctions.SendMailAdmins(this.recoverMail.Text);
            this.errorLoginLabel.ForeColor = Color.Blue;
            this.errorLoginLabel.Text = "Mail Was Sent Successfully";
        }
        catch (Exception p)
        {
            this.logger.Error(p, MethodBase.GetCurrentMethod().Name);
            this.errorLoginLabel.ForeColor = Color.Red;
            this.errorLoginLabel.Text = "Failed To Send Mail";
        }
    }

    protected void tabs1_Click(object sender, EventArgs e)
    {
        this.SelectTab("tabs1");
        Response.Redirect("~/Admin/Home.aspx");
    }
    protected void tabs2_Click(object sender, EventArgs e)
    {
        this.SelectTab("tabs2");
        Response.Redirect("~/Admin/Complex.aspx");
    }
    protected void tabs3_Click(object sender, EventArgs e)
    {
        this.SelectTab("tabs3");
        Response.Redirect("~/Admin/Studio.aspx");
    }
    protected void tabs4_Click(object sender, EventArgs e)
    {
        this.SelectTab("tabs4");
        Response.Redirect("~/Admin/Design.aspx");
    }
    protected void tabs5_Click(object sender, EventArgs e)
    {
        this.SelectTab("tabs5");
        Response.Redirect("~/Admin/Production.aspx");
    }
    protected void tabs6_Click(object sender, EventArgs e)
    {
        this.SelectTab("tabs6");
        Response.Redirect("~/Admin/AllArtists.aspx");
    }

    protected void tabs7_Click(object sender, EventArgs e)
    {
        this.SelectTab("tabs7");
        Response.Redirect("~/Admin/ContactAndAbout.aspx");
    }

    protected void tabs8_Click(object sender, EventArgs e)
    {
        this.SelectTab("tabs8");
        Response.Redirect("~/Admin/Staff.aspx");
    }

    protected void tabs9_Click(object sender, EventArgs e)
    {
        this.SelectTab("tabs9");
        Response.Redirect("~/Admin/PR.aspx");
    }

    protected void tabs10_Click(object sender, EventArgs e)
    {
        this.SelectTab("tabs10");
        Response.Redirect("~/Admin/Publish.aspx");
    }

    protected void tabs11_Click(object sender, EventArgs e)
    {
        this.SelectTab("tabs11");
        Response.Redirect("~/Admin/Admins.aspx");
    }

    private void SelectTab(string tabID)
    {
        if (tabID == "" || tabID == null)
        {
            this.logger.Error(new AdminException
            (". tabID == \"\" || tabID == null"), MethodBase.GetCurrentMethod().Name);
            this.notifier.Notify(23, "Red", "");
            return;
        }

        this.Session["tab"] = tabID;
        this.ClearTabs();

        switch (tabID)
        {
            case "tabs1":
                this.tabs1.CssClass = "defaulttab2";
                break;
            case "tabs2":
                this.tabs2.CssClass = "defaulttab2";
                break;
            case "tabs3":
                this.tabs3.CssClass = "defaulttab2";
                break;
            case "tabs4":
                this.tabs4.CssClass = "defaulttab2";
                break;
            case "tabs5":
                this.tabs5.CssClass = "defaulttab2";
                break;
            case "tabs6":
                this.tabs6.CssClass = "defaulttab2";
                break;
            case "tabs7":
                this.tabs7.CssClass = "defaulttab2";
                break;
            case "tabs8":
                this.tabs8.CssClass = "defaulttab2";
                break;
            case "tabs9":
                this.tabs9.CssClass = "defaulttab2";
                break;
            case "tabs10":
                this.tabs10.CssClass = "defaulttab2";
                break;
            case "tabs11":
                this.tabs11.CssClass = "defaulttab2";
                break;
            default:
                break;
        }
    }

    private void ClearTabs()
    {
        this.tabs1.CssClass = "";
        this.tabs2.CssClass = "";
        this.tabs3.CssClass = "";
        this.tabs4.CssClass = "";
        this.tabs5.CssClass = "";
        this.tabs6.CssClass = "";
        this.tabs7.CssClass = "";
        this.tabs8.CssClass = "";
        this.tabs9.CssClass = "";
        this.tabs10.CssClass = "";
        this.tabs11.CssClass = "";
    }

    public void Exit()
    {
        this.loginUser.Logoff();
        this.Session["login"] = null;
        this.mainMenu.Visible = false;
        this.login.Visible = true;
        this.login.Attributes["class"] = "mailYes";
        this.mainMenu.Attributes["class"] = "mailNo";
    }

    public void exit_Click(object sender, EventArgs e)
    {
        this.Exit();
    }
}
