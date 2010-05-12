using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dal;
using System.Reflection;
using System.Drawing;

public partial class Admin_Admins : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.adminsSelector.Items.Add(new ListItem("--Select Action--", "1"));
            this.adminsSelector.Items.Add(new ListItem("Add New Admin", "2"));
            this.adminsSelector.Items.Add(new ListItem("Remove/Update Admin", "3"));

            foreach (AdminUser p in (IEnumerable<AdminUser>)this.Master._PapaDal.GetAll("admin"))
            {
                this.removeUpdateAdminsSelector.Items.Add(new ListItem(p.UserID, "s" + p.LoginID));
            }

            this.DivSwitcher(1);
        }
    }

    protected void adminsSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!this.ValidateFields(1))
        {
            return;
        }

        this.DivSwitcher(int.Parse(this.adminsSelector.SelectedValue));
    }

    protected void addAdminsButton_Click(object sender, EventArgs e)
    {
        if (this.adminsHiddenUp.Value == "")
        {
            this.AddAdmins();
        }
        else
        {
            this.UpdateAdmins();
            this.adminsHiddenUp.Value = "";
        }
    }

    protected void disableAdminsButton_Click(object sender, EventArgs e)
    {
        this.DisableAdmins();
    }

    protected void enableAdminsButton_Click(object sender, EventArgs e)
    {
        this.EnableAdmins();
    }

    protected void cancelAdminsButton_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    protected void removeAdminsButton_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(4))
        {
            this.ClearAdmins();
            return;
        }

        AdminUser p = (AdminUser)this.Master._PapaDal.Get("admin",
        this.removeUpdateAdminsSelector.SelectedValue.Remove(0, 1));
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearAdmins();
            return;
        }

        this.adminsHiddenRe.Value = p.LoginID;
        this.Notify(this.Master._Notifier.Notify(5, "Red", p.UserID));
    }

    protected void updateAdminsButton_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(4))
        {
            this.ClearAdmins();
            return;
        }

        AdminUser p = (AdminUser)this.Master._PapaDal.Get("admin",
        this.removeUpdateAdminsSelector.SelectedValue.Remove(0, 1));
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearAdmins();
            return;
        }

        this.adminsHiddenUp.Value = p.LoginID;
        this.UpdateAdminsInit();
        this.DivSwitcher(2);
    }

    public bool ValidateFields(int action)
    {
        if (action <= 0)
        {
            this.Master._Logger.Error(new AdminException(". action <= 0"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            return false;
        }

        switch (action)
        {
            case 1:
                if (this.adminsSelector.SelectedIndex == 0)
                {
                    this.Master._Logger.Error(new AdminException(". this.adminsSelector.SelectedIndex == 0"), 
                    MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(1, "Red", ""));
                    return false;
                }
                break;
            case 2:
                if (this.adminsUserID.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                            (". this.adminsUserID.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(44, "Red", ""));
                    return false;
                }

                if (this.adminsPassword1.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                            (". this.adminsPassword1.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(45, "Red", ""));
                    return false;
                }

                if (this.adminsPassword2.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                            (". this.adminsPassword2.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(46, "Red", ""));
                    return false;
                }

                if (this.adminsPassword1.Text != this.adminsPassword2.Text)
                {
                    this.Master._Logger.Error(new AdminException
                            (". this.adminsPassword1.Text != this.adminsPassword2.Text"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(49, "Red", ""));
                    return false;
                }
                break;
            case 3:
                if (this.removeUpdateAdminsSelector.Items.Count == 0)
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.removeUpdateAdminsSelector.Items.Count == 0"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(50, "Red", ""));
                    return false;
                }
                break;
            case 4:
                if (this.removeUpdateAdminsSelector.SelectedValue == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.removeUpdateAdminsSelector.SelectedValue == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(57, "Red", "Admin"));
                    return false;
                }
                break;
            case 5:
                if (this.adminsHidden.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                     (". this.adminsHidden.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
                }
                break;
            case 6:
                if (this.adminsHiddenRe.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                      (". this.adminsHiddenRe.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
                }
                break;
            case 7:
                if (this.adminsHiddenUp.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                      (". this.adminsHiddenUp.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
                }
                break;
            default:
                break;
        }

        return true;
    }

    public void ClearFields(int action)
    {
        if (action <= 0)
        {
            this.Master._Logger.Error(new AdminException(". action <= 0"),
            MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            return;
        }

        switch (action)
        {
            case 1:
                foreach (ListItem l in this.adminsSelector.Items)
                {
                    l.Selected = false;
                }
                break;
            case 2:
                this.adminsLastUpdateLabel.Text = "";
                this.adminsCreationLabel.Text = "";
                this.adminsLastLoginLabel.Text = "";
                this.adminsStatusLabel.Text = "";
                this.adminsUserID.Text = "";
                this.adminsPassword1.Text = "";
                this.adminsPassword2.Text = "";
                break;
            case 3:
                foreach (ListItem l in this.removeUpdateAdminsSelector.Items)
                {
                    l.Selected = false;
                }
                break;
            case 4:
                this.adminsHiddenRe.Value = "";
                this.adminsHiddenUp.Value = "";
                break;
            default:
                break;
        }
    }

    private void DivSwitcher(int action)
    {
        if (action <= 0)
        {
            this.Master._Logger.Error(new AdminException
                 (". action <= 0"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            return;
        }

        this.ShowAdminsUpdateInfo(false);

        this.mainAdmins.Visible = false;
        this.addAdmins.Visible = false;
        this.removeUpdateAdmins.Visible = false;
        this.adminsNotify.Visible = false;

        this.mainAdmins.Attributes["class"] = "mailNo";
        this.addAdmins.Attributes["class"] = "mailNo";
        this.removeUpdateAdmins.Attributes["class"] = "mailNo";
        this.adminsNotify.Attributes["class"] = "mailNo";

        switch (action)
        {
            case 1:
                this.mainAdmins.Visible = true;
                this.mainAdmins.Attributes["class"] = "mailYes";
                break;
            case 2:
                this.addAdmins.Visible = true;
                this.addAdmins.Attributes["class"] = "mailYes";

                if (this.adminsHiddenUp.Value == "")
                {
                    this.ShowAdminsEnableDisable(false);
                    this.ShowAdminsUpdateInfo(false);
                }
                else
                {
                    this.ShowAdminsEnableDisable(true);
                    this.ShowAdminsUpdateInfo(true);
                }
                break;
            case 3:
                this.removeUpdateAdmins.Visible = true;
                this.removeUpdateAdmins.Attributes["class"] = "mailYes";
                break;
            case 4:
                this.adminsNotify.Visible = true;
                this.adminsNotify.Attributes["class"] = "mailYes";
                break;
            default:
                break;
        }
    }

    private void AddAdmins()
    {
        if (!this.ValidateFields(2))
        {
            this.ClearAdmins();
            return;
        }

        if (this.Master._PapaDal.GetAdminUserByUserID(this.adminsUserID.Text) != null)
        {
            this.Master._Logger.Error(new AdminException
             (". (this.Master._PapaDal.GetAdminUserByUserID(this.adminsUserID.Text) != null)"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(47, "Red", this.adminsUserID.Text));
            this.ClearAdmins();
            return;
        }

        if (this.Master._PapaDal.GetAdminUserByPassword(this.adminsPassword2.Text) != null)
        {
            this.Master._Logger.Error(new AdminException
             (". (this.Master._PapaDal.GetAdminUserByPassword(this.adminsPassword2.Text) != null)"),
             MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(47, "Red", this.adminsPassword2.Text));
            this.ClearAdmins();
            return;
        }

        try
        {
            AdminUser g = new AdminUser
            {
                Active = 2,
                CreateTime = TimeNow.TheTimeNow,
                LastLogin = TimeNow.TheTimeNow,
                LastUpdate = TimeNow.TheTimeNow,
                LoginID = this.Master._PapaDal.GetNextAvailableID("admin"),
                Password = this.adminsPassword2.Text,
                UserID = this.adminsUserID.Text,
                spActive = "Disable",
                spCreateTime = TimeNow.TheTimeNow.ToShortDateString(),
                spLastLogin = TimeNow.TheTimeNow.ToShortDateString(),
                spLastUpdate = TimeNow.TheTimeNow.ToShortDateString(),
            };

            this.Master._PapaDal.Add("admin", g);
            this.Master._Logger.Log(new AdminException(". " + g.UserID + " Was Successfully Added"),
                            MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(15, "White", g.UserID));

            this.removeUpdateAdminsSelector.Items.Add(new ListItem(g.UserID, "s" + g.LoginID));
        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(16, "Red", this.adminsUserID.Text));
        }
    }

    private void UpdateAdminsInit()
    {
        if (!this.ValidateFields(7))
        {
            this.ClearAdmins();
            return;
        }

        AdminUser p = (AdminUser)this.Master._PapaDal.Get("admin", this.adminsHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearAdmins();
            return;
        }

        try
        {
            this.adminsLastUpdateLabel.Text = p.spLastUpdate;
            this.adminsStatusLabel.Text = p.spActive;
            this.adminsLastLoginLabel.Text = p.spLastLogin;
            this.adminsCreationLabel.Text = p.spCreateTime;
            this.adminsUserID.Text = p.UserID;
            this.ShowAdminsUpdateInfo(true);
            this.ShowAdminsEnableDisable(true);
        }
        catch (Exception e)
        {
            this.ClearAdmins();
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
        }
    }

    private void UpdateAdmins()
    {
        if (!this.ValidateFields(7))
        {
            this.ClearAdmins();
            return;
        }

        if (!this.ValidateFields(2))
        {
            this.ClearAdmins();
            return;
        }

        AdminUser g = (AdminUser)this.Master._PapaDal.Get("admin", this.adminsHiddenUp.Value);
        if (g == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". g == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearAdmins();
            return;
        }

        if (this.Master._PapaDal.GetAdminByUserIDExcept(g.UserID, this.adminsUserID.Text) != null)
        {
            this.Master._Logger.Error(new AdminException
             (". (this.Master._PapaDal.GetAdminByUserIDExcept(g.UserID, this.adminsUserID.Text))"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(47, "Red", this.adminsUserID.Text));
            this.ClearAdmins();
            return;
        }

        if (this.Master._PapaDal.GetAdminUserByPasswordExcept(g.Password, this.adminsPassword2.Text) != null)
        {
            this.Master._Logger.Error(new AdminException
             (". (this.Master._PapaDal.GetAdminUserByPasswordExcept(g.Password, this.adminsPassword2.Text) != null)"),
             MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(48, "Red", this.adminsPassword2.Text));
            this.ClearAdmins();
            return;
        }


        try
        {
            g.spLastUpdate = TimeNow.TheTimeNow.ToShortDateString();
            g.LastUpdate = TimeNow.TheTimeNow;
            g.UserID = this.adminsUserID.Text;
            g.Password = this.adminsPassword2.Text;
            g.spActive = g.spActive;
            g.Active = g.Active;
            g.LoginID = g.LoginID;

            this.Master._PapaDal.Update("admin", g, TimeNow.TheTimeNow);
            this.Master._Logger.Log(new AdminException(". " + g.UserID +
                    " Was Successfully Updated"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(17, "White", g.UserID));

            this.removeUpdateAdminsSelector.Items.FindByValue
            ("s" + g.LoginID).Text = g.UserID;
        }
        catch (Exception e)
        {
            this.Master._Logger.Log(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(18, "Red", this.adminsUserID.Text));
        }
    }

    private void RemoveAdmins()
    {
        if (!this.ValidateFields(6))
        {
            this.ClearAdmins();
            return;
        }

        AdminUser g = (AdminUser)this.Master._PapaDal.Get("admin", this.adminsHiddenRe.Value);
        if (g == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". g == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearAdmins();
            return;
        }

        try
        {
            this.Master._PapaDal.Remove("admin", g.LoginID);
            this.Master._Logger.Log(new AdminException(". " + g.UserID + " Was Successfully Removed"),
                MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(12, "White", g.UserID));

            this.removeUpdateAdminsSelector.Items.Remove
            (this.removeUpdateAdminsSelector.Items.FindByValue
            ("s" + g.LoginID));
        }
        catch (Exception e)
        {
            this.Master._Logger.Log(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(13, "Red", g.UserID));
        }
    }

    private void ShowAdminsEnableDisable(bool visible)
    {
        this.disableAdminsButton.Visible = visible;
        this.enableAdminsButton.Visible = visible;
    }

    private void EnableAdmins()
    {
        if (!this.ValidateFields(5))
        {
            this.ClearAdmins();
            return;
        }

        AdminUser p = (AdminUser)this.Master._PapaDal.Get("admin", this.adminsHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearAdmins();
            return;
        }

        if (p.Active == 1)
        {
            this.Master._Logger.Warn(new AdminException
            (". " + p.UserID + " Is Already Disabled"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(9, "Red", p.UserID));
            this.ClearFields(3);
            return;
        }

        try
        {
            this.Master._PapaDal.Enable("admin", p.LoginID);
            this.Master._Logger.Log(new AdminException(". " + p.UserID +
                " Has Been Successfully Enabled"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(6, "White", p.UserID));
        }
        catch (Exception f)
        {
            this.Master._Logger.Log(f, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(10, "Red", p.UserID));
        }
    }

    private void DisableAdmins()
    {
        if (!this.ValidateFields(5))
        {
            this.ClearAdmins();
            return;
        }

        AdminUser p = (AdminUser)this.Master._PapaDal.Get("admin", this.adminsHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearAdmins();
            return;
        }

        if (p.Active == 2)
        {
            this.Master._Logger.Warn(new AdminException
            (". " + p.UserID + " Is Already Disabled"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(8, "Red", p.UserID));
            this.ClearFields(3);
            return;
        }

        try
        {
            this.Master._PapaDal.Enable("admin", p.LoginID);
            this.Master._Logger.Log(new AdminException(". " + p.UserID +
                " Has Been Successfully Enabled"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(7, "White", p.UserID));
        }
        catch (Exception f)
        {
            this.Master._Logger.Log(f, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(11, "Red", p.UserID));
        }
    }

    protected void okBut_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(5))
        {
            return;
        }

        this.AfterOk(this.adminsHidden.Value);
    }

    protected void cancelBut_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    public void ShowAdminsUpdateInfo(bool visible)
    {
        this.adminsUpdateInfo.Visible = visible;
        if (visible)
        {
            this.adminsUpdateInfo.Attributes["class"] = "mailYes";
        }
        else
        {
            this.adminsUpdateInfo.Attributes["class"] = "mailNo";
        }
    }

    private void ClearAdmins()
    {
        this.ClearFields(2);
        this.ClearFields(3);
        this.ClearFields(4);
    }

    private void Start()
    {
        this.ClearFields(1);
        this.ClearFields(2);
        this.ClearFields(3);
        this.ClearFields(4);

        this.ShowAdminsEnableDisable(false);

        this.DivSwitcher(1);
    }

    private void AfterOk(string selector)
    {
        if (selector == "")
        {
            this.Master._Logger.Error(new AdminException
                 (". selector == \"\""), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            return;
        }

        switch (selector)
        {
            case "0":
                this.Master.Exit();
                break;
            case "1":
                //select an action
                break;
            case "3":
            //no file was sent
            case "4":
                //no pics to remove or update
                break;
            case "5":
                this.RemoveAdmins();
                break;
            case "17":
            case "18":
                //update text
                break;
            case "7":
            case "8":
            case "11":
                //disable pic
                break;
            case "6":
            case "9":
            case "10":
                //enable pic
                break;
            case "12":
            case "13":
                //remove pic
                break;
            case "15":
            case "16":
                //add pic
                break;
            case "19":
                //enter pic place
                break;
            case "20":
                //invalid pic place
                break;
            case "21":
                //pic place not available
                break;
            default:
                break;
        }

        if (selector != "5")
        {
            this.Start();
            this.adminsHidden.Value = "";
        }
    }

    private void Notify(string[] message)
    {
        this.cancelBut.Visible = false;
        try
        {
            if (message == null || message.Count() != 3)
            {
                throw new AdminException(". message == null || message.Count() != 3");
            }

            if (message[0] == "" || message[0] == null ||
                message[1] == "" || message[1] == null ||
                message[2] == "" || message[2] == null)
            {
                throw new AdminException(@". message[0] == """" || message[0] == null || message[1] == """" ||
                                            message[1] == null || message[2] == """" || message[2] == null");
            }

            if (message[1] == "Red")
            {
                this.notifyLabel.ForeColor = Color.Red;
            }
            if (message[1] == "White")
            {
                this.notifyLabel.ForeColor = Color.White;
            }

            this.notifyLabel.Text = message[0];
            this.adminsHidden.Value = message[2];

            switch (message[2])
            {
                case "5":
                    this.cancelBut.Visible = true;
                    break;
                default:
                    break;
            }
        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.notifyLabel.ForeColor = Color.Red;
            this.notifyLabel.Text = "Ooooops! Somthing Wrong Was Happend, Please Try Again Or/And content The Administrator";
        }
        finally
        {
            this.DivSwitcher(4);
        }
    }
}
