using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Drawing;
using System.Text;
using Dal;
using System.IO;

public partial class Admin_ContactAndAbout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.contactAndAboutSelector.Items.Add(new ListItem("--Select Action--", "1"));
            this.contactAndAboutSelector.Items.Add(new ListItem("Add New Image", "2"));
            this.contactAndAboutSelector.Items.Add(new ListItem("Remove/Update Image", "3"));
            this.contactAndAboutSelector.Items.Add(new ListItem("Update Text", "4"));

            foreach (MainContactAboutPic p in (IEnumerable<MainContactAboutPic>)this.Master._PapaDal.GetAll("mainContactAboutPic"))
            {
                this.removeUpdatePicSelector.Items.Add(new ListItem(p.PicName, "s" + p.PicID));
            }

            this.DivSwitcher(1);
        }
    }

    protected void contactAndAboutSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!this.ValidateFields(1))
        {
            return;
        }

        this.DivSwitcher(int.Parse(this.contactAndAboutSelector.SelectedValue));
    }

    protected void removeContactAndAboutPicButton_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(3))
        {
            return;
        }

        this.Notify(this.Master._Notifier.Notify(5, "Red", this.removeUpdatePicSelector.SelectedItem.Text));
        this.contactAndAboutHiddenRe.Value = this.removeUpdatePicSelector.SelectedValue.Remove(0, 1);
    }

    protected void updateContactAndAboutPicButton_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(3))
        {
            return;
        }

        this.contactAndAboutHiddenUp.Value = this.removeUpdatePicSelector.SelectedValue.Remove(0, 1);
        this.UpdatePicInit();
        this.DivSwitcher(2);
    }

    protected void cancelContactAndAboutPicButton_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    protected void disableContactAndAboutPicButton_Click(object sender, EventArgs e)
    {
        this.DisablePic();
    }

    protected void enableContactAndAboutPicButton_Click(object sender, EventArgs e)
    {
        this.EnablePic();
    }

    protected void cancelContactAndAboutPic_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    protected void updateContactAndAboutAboutButton_Click(object sender, EventArgs e)
    {
        this.UpdateText();
    }

    protected void cancelContactAndAboutAboutButton_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    protected void addContactAndAboutPicButton_Click(object sender, EventArgs e)
    {
        if (this.contactAndAboutHiddenUp.Value == "")
        {
            this.AddContactAndAboutPic();
        }
        else
        {
            this.UpdatePic();
            this.contactAndAboutHiddenUp.Value = "";
        }
    }

    protected void cancelAddContactAndAboutPicButton_Click(object sender, EventArgs e)
    {
        this.Start();
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
                if (this.contactAndAboutSelector.SelectedIndex == 0)
                {
                    this.Master._Logger.Error(new AdminException(". this.contactAndAboutSelector.SelectedIndex == 0"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(1, "Red", ""));
                    return false;
                }
                break;
            case 2:
                if (this.contactAndAboutPicUpload.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.contactAndAboutPicUpload.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(37, "Red", "Contact And About"));
                    return false;
                }
                break;
            case 3:
                if (this.removeUpdatePicSelector.SelectedValue == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.removeUpdatePicSelector.SelectedValue == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(57, "Red", "Contact And About"));
                    return false;
                }
                break;
            case 4:
                if (this.contactAndAboutHiddenRe.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.contactAndAboutHiddenRe.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
                    return false;
                }
                break;
            case 5:
                if (this.contactAndAboutHiddenUp.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.contactAndAboutHiddenUp.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
                    return false;
                }
                break;
            case 6:
                if (this.contactAndAboutHidden.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.contactAndAboutHidden.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
                    return false;
                }
                break;
            case 7:
                if (this.aboutContactAndAboutTextHe.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.aboutContactAndAboutTextHe.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(40, "Red", "About"));
                    return false;
                }

                if (this.contactContactAndAboutTextHe.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.contactContactAndAboutTextHe.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(40, "Red", "Contact"));
                    return false;
                }

                if (this.aboutContactAndAboutTextEn.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.aboutContactAndAboutTex.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(41, "Red", "About"));
                    return false;
                }

                if (this.contactContactAndAboutTextEn.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.contactContactAndAboutTextEn.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(41, "Red", "Contact"));
                    return false;
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
                foreach (ListItem l in this.contactAndAboutSelector.Items)
                {
                    l.Selected = false;
                }
                this.contactAndAboutPicLastUpdateLabel.Text = "";
                this.contactAndAboutPicStatusLabel.Text = "";
                this.contactAndAboutPic.Src = "";
                break;
            case 2:
                foreach (ListItem l in this.removeUpdatePicSelector.Items)
                {
                    l.Selected = false;
                }
                break;
            case 3:
                this.contactAndAboutHiddenRe.Value = "";
                this.contactAndAboutHiddenUp.Value = "";
                break;
            case 4:
                this.aboutContactAndAboutLabel.Text = "";
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

        this.ShowAboutContactAndAboutUpdateInfo(false);
        this.ShowContactAndAboutPicUpdateInfo(false);

        this.mainContactAndAbout.Visible = false;
        this.addContactAndAboutPic.Visible = false;
        this.removeUpdatePic.Visible = false;
        this.aboutContactAndAbout.Visible = false;
        this.contactAndAboutNotify.Visible = false;

        this.mainContactAndAbout.Attributes["class"] = "mailNo";
        this.addContactAndAboutPic.Attributes["class"] = "mailNo";
        this.removeUpdatePic.Attributes["class"] = "mailNo";
        this.aboutContactAndAbout.Attributes["class"] = "mailNo";
        this.contactAndAboutNotify.Attributes["class"] = "mailNo";

        switch (action)
        {
            case 1:
                this.mainContactAndAbout.Visible = true;
                this.mainContactAndAbout.Attributes["class"] = "mailYes";
                break;
            case 2:
                this.addContactAndAboutPic.Visible = true;
                this.addContactAndAboutPic.Attributes["class"] = "mailYes";
                if (this.contactAndAboutHiddenUp.Value == "")
                {
                    this.ShowContactAndAboutEnableDisable(false);
                    this.ShowContactAndAboutFile(true);
                    this.ShowContactAndAboutPicture(false);
                    this.ShowContactAndAboutPicUpdateInfo(false);
                }
                else
                {
                    this.ShowContactAndAboutEnableDisable(true);
                    this.ShowContactAndAboutFile(true);
                    this.ShowContactAndAboutPicture(true);
                    this.ShowContactAndAboutPicUpdateInfo(true);
                }
                break;
            case 3:
                this.removeUpdatePic.Visible = true;
                this.removeUpdatePic.Attributes["class"] = "mailYes";
                break;
            case 4:
                this.aboutContactAndAbout.Visible = true;
                this.aboutContactAndAbout.Attributes["class"] = "mailYes";
                this.ShowAboutContactAndAboutUpdateInfo(true);

                if (this.Master._PapaDal.GetCount("mainContactAbout") > 0)
                {
                    MainContactAbout g = (MainContactAbout)this.Master._PapaDal.Get("mainContactAbout", "0");
                    this.aboutContactAndAboutTextHe.Text = g.AboutHe;
                    this.aboutContactAndAboutTextEn.Text = g.AboutEn;
                    this.contactContactAndAboutTextHe.Text = g.ContactHe;
                    this.contactContactAndAboutTextEn.Text = g.ContactEn;
                    this.aboutContactAndAboutLabel.Text = g.spLastUpdate;
                }
                break;
            case 5:
                this.contactAndAboutNotify.Visible = true;
                this.contactAndAboutNotify.Attributes["class"] = "mailYes";
                break;
            default:
                break;
        }
    }

    private void AddContactAndAboutPic()
    {
        if (!this.ValidateFields(2))
        {
            this.ClearContactAndAboutPic();
            return;
        }

        string ID = this.Master._PapaDal.GetNextAvailableID("mainContactAboutPic");
        string fileName = this.contactAndAboutPicUpload.PostedFile.FileName;
        string fileNameToSave = "contactAndAboutPic_id-" + ID + "_" + fileName;
        string fullPath = this.Master._GlobalFunctions.GetFullPath() + fileNameToSave;
        string relativePath = this.Master._GlobalFunctions.GetRelativePath() + fileNameToSave;

        if (!this.Master._GlobalFunctions.ValidatePicEnd(this.contactAndAboutPicUpload.Value))
        {
            this.Master._Logger.Error(new AdminException
            (". !this.Master._GlobalFunctions.ValidatePicEnd(this.contactAndAboutPicUpload.Value)"),
            MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(14, "Red", this.contactAndAboutPicUpload.Value));
            this.ClearContactAndAboutPic();
            return;
        }

        try
        {
            this.contactAndAboutPicUpload.PostedFile.SaveAs(fullPath);
        }
        catch (Exception t)
        {
            this.Master._Logger.Error(t, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(36, "Red", this.contactAndAboutPicUpload.Value));
            this.ClearContactAndAboutPic();
            return;
        }

        try
        {
            MainContactAboutPic p = new MainContactAboutPic
            {
                Active = 2,
                PicName = fileName,
                PicID = ID,
                PicFullPath = fullPath,
                PicRelativePath = relativePath,
                UploadTime = TimeNow.TheTimeNow,
                spUploadTime = TimeNow.TheTimeNow.ToString(),
                spActive = "Disable",
                LastUpdate = TimeNow.TheTimeNow,
                spLastUpdate = TimeNow.TheTimeNow.ToShortDateString()
            };

            this.Master._PapaDal.Add("mainContactAboutPic", p);
            this.Master._Logger.Log(new AdminException(". " + p.PicID + " Was Successfully Added"),
            MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(15, "White", fileName));

            this.removeUpdatePicSelector.Items.Add(new ListItem(p.PicName, "s" + p.PicID));
        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(16, "Red", fileName));
        }
    }

    private void UpdatePicInit()
    {
        if (!this.ValidateFields(5))
        {
            this.ClearContactAndAboutPic();
            return;
        }

        MainContactAboutPic p = (MainContactAboutPic)this.Master._PapaDal.Get("mainContactAboutPic", this.contactAndAboutHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
        }

        try
        {

            this.contactAndAboutPicStatusLabel.Text = p.spActive;
            this.contactAndAboutPicLastUpdateLabel.Text = p.spUploadTime;
            this.contactAndAboutPic.Src = p.PicRelativePath;
            this.ShowContactAndAboutPicUpdateInfo(true);
            this.ShowContactAndAboutFile(true);
            this.ShowContactAndAboutPicture(true);
            this.ShowContactAndAboutEnableDisable(true);
        }
        catch (Exception t)
        {
            this.Master._Logger.Error(t, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
        }
    }

    private void UpdatePic()
    {
        if (!this.ValidateFields(5))
        {
            this.ClearContactAndAboutPic();
            return;
        }

        MainContactAboutPic g = (MainContactAboutPic)this.Master._PapaDal.Get("mainContactAboutPic", this.contactAndAboutHiddenUp.Value);
        if (g == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". g == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearContactAndAboutPic();
            return;
        }

        if (this.contactAndAboutPicUpload.Value != "")
        {
            if (!this.Master._GlobalFunctions.ValidatePicEnd(this.contactAndAboutPicUpload.Value))
            {
                this.Master._Logger.Error(new AdminException
                (". !this.Master._GlobalFunctions.ValidatePicEnd(this.contactAndAboutPicUpload.Value)"),
                MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(14, "Red", this.contactAndAboutPicUpload.Value));
                return;
            }

            string fileName = this.contactAndAboutPicUpload.PostedFile.FileName;
            string fileNameToSave = "contactAndAboutPic_id-" + g.PicID + "_" + fileName;
            string fullPath = this.Master._GlobalFunctions.GetFullPath() + fileNameToSave;
            string relativePath = this.Master._GlobalFunctions.GetRelativePath() + fileNameToSave;

            try
            {
                if (File.Exists(g.PicFullPath))
                {
                    File.Delete(g.PicFullPath);
                }
                this.contactAndAboutPicUpload.PostedFile.SaveAs(fullPath);

                g.PicName = fileName;
                g.PicFullPath = fullPath;
                g.PicRelativePath = relativePath;
            }
            catch (Exception r)
            {
                this.Master._Logger.Error(r, MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(36, "Red", this.contactAndAboutPicUpload.Value));
                this.ClearContactAndAboutPic();
                return;
            }
        }

        try
        {
            g.LastUpdate = TimeNow.TheTimeNow;
            g.spLastUpdate = TimeNow.TheTimeNow.ToShortDateString();

            this.Master._PapaDal.Update("mainContactAboutPic", g, TimeNow.TheTimeNow);
            this.Master._Logger.Log(new AdminException(". " + g.PicID +
                    " Was Successfully Updated"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(17, "White", g.PicName));

            this.removeUpdatePicSelector.Items.FindByValue("s" + g.PicID).Text = g.PicName;
        }
        catch (Exception e)
        {
            this.Master._Logger.Log(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(18, "Red", g.PicName));
        }
    }

    private void RemovePic()
    {
        if (!this.ValidateFields(4))
        {
            return;
        }

        MainContactAboutPic p = (MainContactAboutPic)this.Master._PapaDal.Get("mainContactAboutPic", this.contactAndAboutHiddenRe.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearContactAndAboutPic();
            return;
        }

        try
        {
            File.Delete(p.PicFullPath);

            this.Master._PapaDal.Remove("mainContactAboutPic", p.PicID);
            this.Master._Logger.Log(new AdminException(". " + p.PicID + " Was Successfully Removed"),
                MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(12, "White", p.PicName));
        }
        catch (Exception e)
        {
            this.Master._Logger.Log(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(13, "Red", p.PicName));
        }
    }

    private void EnablePic()
    {
        if (!this.ValidateFields(5))
        {
            this.ClearFields(3);
            return;
        }

        MainContactAboutPic p = (MainContactAboutPic)this.Master._PapaDal.Get("mainContactAboutPic", this.contactAndAboutHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearContactAndAboutPic();
            return;
        }

        if (p.Active == 1)
        {
            this.Master._Logger.Warn(new AdminException
            (". " + this.contactAndAboutHiddenUp.Value + " Is Already Disabled"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(9, "Red", p.PicName));
            this.ClearContactAndAboutPic();
            return;
        }

        try
        {
            this.Master._PapaDal.Enable("mainContactAboutPic", p.PicID);
            this.Master._Logger.Log(new AdminException(". " + p.PicID +
                " Has Been Successfully Enabled"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(6, "White", p.PicName));
        }
        catch (Exception f)
        {
            this.Master._Logger.Log(f, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(10, "Red", p.PicName));
        }
    }

    private void DisablePic()
    {
        if (!this.ValidateFields(5))
        {
            this.ClearContactAndAboutPic();
            return;
        }

        MainContactAboutPic p = (MainContactAboutPic)this.Master._PapaDal.Get("mainContactAboutPic", this.contactAndAboutHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearContactAndAboutPic();
            return;
        }

        if (p.Active == 2)
        {
            this.Master._Logger.Warn(new AdminException
            (". " + this.contactAndAboutHiddenUp.Value + " Is Already Disabled"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(8, "Red", p.PicName));
            this.ClearContactAndAboutPic();
            return;
        }

        try
        {
            this.Master._PapaDal.Disable("mainContactAboutPic", p.PicID);
            this.Master._Logger.Log(new AdminException(". " + p.PicID +
                " Has Been Successfully Disabled"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(7, "White", p.PicName));
        }
        catch (Exception f)
        {
            this.Master._Logger.Log(f, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(11, "Red", p.PicName));
        }
    }

    public void UpdateText()
    {
        if (!this.ValidateFields(7))
        {
            return;
        }

        string aboutHebrewTextContactAndAbout = this.Master._GlobalFunctions.ConvertToUtf8(this.aboutContactAndAboutTextHe.Text);
        string contactHebrewTextContactAndAbout = this.Master._GlobalFunctions.ConvertToUtf8(this.contactContactAndAboutTextHe.Text);

        MainContactAbout v = (MainContactAbout)this.Master._PapaDal.Get("mainContactAbout", "0");
        if (v == null)
        {
            try
            {
                MainContactAbout e = new MainContactAbout
                {
                    MainContactAboutID = this.Master._PapaDal.GetNextAvailableID("mainContactAbout"),
                    AboutEn = this.aboutContactAndAboutTextEn.Text,
                    AboutHe = aboutHebrewTextContactAndAbout,
                    ContactEn = this.contactContactAndAboutTextEn.Text,
                    ContactHe = contactHebrewTextContactAndAbout,
                    LastUpdate = TimeNow.TheTimeNow,
                    spLastUpdate = TimeNow.TheTimeNow.ToString()
                };

                this.Master._PapaDal.Add("mainContactAbout", e);
                this.Master._Logger.Log(new AdminException(". Main Contact And About Text Was Successfully Updated"),
                    MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(15, "White", "Main Contact And About Text"));
            }
            catch (Exception e)
            {
                this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(16, "Red", "Main Contact And About Text"));
            }
        }
        else
        {
            try
            {
                v.AboutEn = this.aboutContactAndAboutTextEn.Text;
                v.AboutHe = aboutHebrewTextContactAndAbout;
                v.ContactEn = this.contactContactAndAboutTextEn.Text;
                v.ContactHe = contactHebrewTextContactAndAbout;
                v.LastUpdate = TimeNow.TheTimeNow;
                v.spLastUpdate = TimeNow.TheTimeNow.ToString();

                this.Master._PapaDal.Update("mainContactAbout", v, TimeNow.TheTimeNow);
                this.Master._Logger.Log(new AdminException(". Main Contact And About Text Was Successfully Updated"),
                    MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(17, "White", "Main Contact And About Text"));
            }
            catch (Exception e)
            {
                this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(18, "Red", "Main Contact And About Text"));
            }
        }
    }

    protected void okBut_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(6))
        {
            return;
        }

        this.AfterOk(this.contactAndAboutHidden.Value);
    }

    protected void cancelBut_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    private void ShowContactAndAboutEnableDisable(bool visible)
    {
        this.disableContactAndAboutPicButton.Visible = visible;
        this.enableContactAndAboutPicButton.Visible = visible;
        if (visible)
        {
            this.disableContactAndAboutPicButton.Attributes["class"] = "mailYes";
            this.enableContactAndAboutPicButton.Attributes["class"] = "mailYes";
        }
        else
        {
            this.disableContactAndAboutPicButton.Attributes["class"] = "mailNo";
            this.enableContactAndAboutPicButton.Attributes["class"] = "mailNo";
        }
    }

    private void ShowContactAndAboutPicture(bool visible)
    {
        this.contactAndAboutPic.Visible = visible;
        if (visible)
        {
            this.contactAndAboutPic.Attributes["class"] = "mailYes";
        }
        else
        {
            this.contactAndAboutPic.Attributes["class"] = "mailNo";
        }
    }

    private void ShowContactAndAboutFile(bool visible)
    {
        this.contactAndAboutPicUpload.Visible = visible;
        if (visible)
        {
            this.contactAndAboutPicUpload.Attributes["class"] = "mailYes";
        }
        else
        {
            this.contactAndAboutPicUpload.Attributes["class"] = "mailNo";
        }
    }

    private void ClearContactAndAboutPic()
    {
        this.ClearFields(2);
        this.ClearFields(3);
    }

    public void ShowContactAndAboutPicUpdateInfo(bool visible)
    {
        this.contactAndAboutPicUpdateInfo.Visible = visible;
        if (visible)
        {
            this.contactAndAboutPicUpdateInfo.Attributes["class"] = "mailYes";
        }
        else
        {
            this.contactAndAboutPicUpdateInfo.Attributes["class"] = "mailNo";
        }
    }

    public void ShowAboutContactAndAboutUpdateInfo(bool visible)
    {
        this.aboutContactAndAboutUpdateInfo.Visible = visible;
        if (visible)
        {
            this.aboutContactAndAboutUpdateInfo.Attributes["class"] = "mailYes";
        }
        else
        {
            this.aboutContactAndAboutUpdateInfo.Attributes["class"] = "mailNo";
        }
    }

    private void Start()
    {
        this.ShowContactAndAboutEnableDisable(false);
        this.ShowContactAndAboutFile(false);
        this.ShowContactAndAboutPicture(false);
        this.ShowContactAndAboutPicUpdateInfo(false);
        this.ShowAboutContactAndAboutUpdateInfo(false);

        this.ClearFields(1);
        this.ClearFields(2);
        this.ClearFields(3);
        this.ClearFields(4);

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
                this.RemovePic();
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
            default:
                break;
        }

        if (selector != "5")
        {
            this.Start();
            this.contactAndAboutHidden.Value = "";
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
            this.contactAndAboutHidden.Value = message[2];

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
            this.notifyLabel.Text = "Oops! Something Wrong Has Happened, Please Try Again Or/And contact The Administrator";
        }
        finally
        {
            this.DivSwitcher(5);
        }
    }
}
