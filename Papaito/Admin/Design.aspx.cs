using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Drawing;
using Dal;
using System.Text;
using System.IO;

public partial class Admin_Design : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.designSelector.Items.Add(new ListItem("--Select Action--", "1"));
            this.designSelector.Items.Add(new ListItem("Add New Image", "2"));
            this.designSelector.Items.Add(new ListItem("Remove/Update Image", "3"));
            this.designSelector.Items.Add(new ListItem("Update Text", "4"));

            foreach (DesignPic p in (IEnumerable<DesignPic>)this.Master._PapaDal.GetAll("designPic"))
            {
                this.removeUpdatePicSelector.Items.Add(new ListItem(p.PicName, "s" + p.PicID));
            }

            this.DivSwitcher(1);
        }
    }

    protected void designSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!this.ValidateFields(1))
        {
            return;
        }

        this.DivSwitcher(int.Parse(this.designSelector.SelectedValue));
    }

    protected void removeDesignPicButton_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(3))
        {
            return;
        }

        this.Notify(this.Master._Notifier.Notify(5, "Red", this.removeUpdatePicSelector.SelectedItem.Text));
        this.designHiddenRe.Value = this.removeUpdatePicSelector.SelectedValue.Remove(0, 1);
    }

    protected void updateDesignPicButton_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(3))
        {
            return;
        }

        this.designHiddenUp.Value = this.removeUpdatePicSelector.SelectedValue.Remove(0, 1);
        this.UpdatePicInit();
        this.DivSwitcher(2);
    }

    protected void cancelDesignPicButton_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    protected void disableDesignPicButton_Click(object sender, EventArgs e)
    {
        this.DisablePic();
    }

    protected void enableDesignPicButton_Click(object sender, EventArgs e)
    {
        this.EnablePic();
    }

    protected void cancelDesignPic_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    protected void updateDesignAboutButton_Click(object sender, EventArgs e)
    {
        this.UpdateText();
    }

    protected void cancelDesignAboutButton_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    protected void addDesignPicButton_Click(object sender, EventArgs e)
    {
        if (this.designHiddenUp.Value == "")
        {
            this.AddDesignPic();
        }
        else
        {
            this.UpdatePic();
            this.designHiddenUp.Value = "";
        }
    }

    protected void cancelAddDesignPicButton_Click(object sender, EventArgs e)
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
                if (this.designSelector.SelectedIndex == 0)
                {
                    this.Master._Logger.Error(new AdminException(". this.designSelector.SelectedIndex == 0"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(1, "Red", ""));
                    return false;
                }
                break;
            case 2:
                if (this.designPicUpload.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.designPicUpload.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(37, "Red", "Picture"));
                    return false;
                }
                break;
            case 3:
                if (this.removeUpdatePicSelector.SelectedValue == null)
                {
                    this.Master._Logger.Error(new AdminException
                            (". this.removeUpdatePicSelector.SelectedValue == null"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(4, "Red", this.designPicUpload.Value));
                    return false;
                }
                break;
            case 4:
                if (this.designHiddenRe.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.designHiddenRe.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
                    return false;
                }
                break;
            case 5:
                if (this.designHiddenUp.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.designHiddenUp.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
                    return false;
                }
                break;
            case 6:
                if (this.designHidden.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.designHidden.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
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
                foreach (ListItem l in this.designSelector.Items)
                {
                    l.Selected = false;
                }
                break;
            case 2:
                foreach (ListItem l in this.removeUpdatePicSelector.Items)
                {
                    l.Selected = false;
                }
                this.designPicLastUpdateLabel.Text = "";
                this.designPicStatusLabel.Text = "";
                this.designPic.Src = "";
                break;
            case 3:
                this.designHiddenRe.Value = "";
                this.designHiddenUp.Value = "";
                break;
            case 4:
                this.aboutDesignLastUpdateLabel.Text = "";
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

        this.ShowAboutDesignUpdateInfo(false);
        this.ShowDesignPicUpdateInfo(false);

        this.mainDesign.Visible = false;
        this.addDesignPic.Visible = false;
        this.removeUpdatePic.Visible = false;
        this.aboutDesign.Visible = false;
        this.designNotify.Visible = false;

        this.mainDesign.Attributes["class"] = "mailNo";
        this.addDesignPic.Attributes["class"] = "mailNo";
        this.removeUpdatePic.Attributes["class"] = "mailNo";
        this.aboutDesign.Attributes["class"] = "mailNo";
        this.designNotify.Attributes["class"] = "mailNo";

        switch (action)
        {
            case 1:
                this.mainDesign.Visible = true;
                this.mainDesign.Attributes["class"] = "mailYes";
                break;
            case 2:
                this.addDesignPic.Visible = true;
                this.addDesignPic.Attributes["class"] = "mailYes";
                if (this.designHiddenUp.Value == "")
                {
                    this.ShowDesignPicEnableDisable(false);
                    this.ShowDesignPicFile(true);
                    this.ShowDesignPicPicture(false);
                    this.ShowDesignPicUpdateInfo(false);
                }
                else
                {
                    this.ShowDesignPicEnableDisable(true);
                    this.ShowDesignPicFile(true);
                    this.ShowDesignPicPicture(true);
                    this.ShowDesignPicUpdateInfo(true);
                }
                break;
            case 3:
                this.removeUpdatePic.Visible = true;
                this.removeUpdatePic.Attributes["class"] = "mailYes";
                break;
            case 4:
                this.aboutDesign.Visible = true;
                this.aboutDesign.Attributes["class"] = "mailYes";
                this.ShowAboutDesignUpdateInfo(true);

                if (this.Master._PapaDal.GetCount("designText") > 0)
                {
                    DesignText g = (DesignText)this.Master._PapaDal.Get("designText", "0");
                    this.aboutDesignTextHe.Text = g.TextHe;
                    this.aboutDesignTextEn.Text = g.TextEn;
                    this.aboutDesignLastUpdateLabel.Text = g.spLastUpdate;
                }
                break;
            case 5:
                this.designNotify.Visible = true;
                this.designNotify.Attributes["class"] = "mailYes";
                break;
            default:
                break;
        }
    }

    private void AddDesignPic()
    {
        if (!this.ValidateFields(2))
        {
            this.ClearDesignPic();
            return;
        }

        string ID = this.Master._PapaDal.GetNextAvailableID("designPic");
        string fileName = this.designPicUpload.PostedFile.FileName;
        string fileNameToSave = "designPic_id-" + ID + "_" + fileName;
        string fullPath = this.Master._GlobalFunctions.GetFullPath() + fileNameToSave;
        string relativePath = this.Master._GlobalFunctions.GetRelativePath() + fileNameToSave;


        if (!this.Master._GlobalFunctions.ValidatePicEnd(this.designPicUpload.Value))
        {
            this.Master._Logger.Error(new AdminException
            (". !this.Master._GlobalFunctions.ValidatePicEnd(this.designPicUpload.Value)"),
            MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(14, "Red", this.designPicUpload.Value));
            this.ClearDesignPic();
            return;
        }

        try
        {
            this.designPicUpload.PostedFile.SaveAs(fullPath);
        }
        catch (Exception y)
        {
            this.Master._Logger.Error(y, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(36, "Red", this.designPicUpload.Value));
            this.ClearDesignPic();
            return;
        }

        try
        {
            DesignPic p = new DesignPic
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

            this.Master._PapaDal.Add("designPic", p);
            this.Master._Logger.Log(new AdminException(". " + p.PicID + " Was Successfully Added"),
            MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(15, "White", p.PicName));

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
            this.ClearDesignPic();
            return;
        }

        DesignPic p = (DesignPic)this.Master._PapaDal.Get("designPic", this.designHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearDesignPic();
            return;
        }

        try
        {
            this.designPicStatusLabel.Text = p.spActive;
            this.designPicLastUpdateLabel.Text = p.spUploadTime;
            this.designPic.Src = p.PicRelativePath;
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
            this.ClearDesignPic();
            return;
        }

        DesignPic g = (DesignPic)this.Master._PapaDal.Get("designPic", this.designHiddenUp.Value);
        if (g == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". g == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearDesignPic();
            return;
        }

        if (this.designPicUpload.Value != "")
        {
            if (!this.Master._GlobalFunctions.ValidatePicEnd(this.designPicUpload.Value))
            {
                this.Master._Logger.Error(new AdminException
                (". !this.Master._GlobalFunctions.ValidatePicEnd(this.designPicUpload.Value)"),
                MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(14, "Red", this.designPicUpload.Value));
                this.ClearDesignPic();
                return;
            }
            try
            {
                string fileName = this.designPicUpload.PostedFile.FileName;
                string fileNameToSave = "designPic_id-" + g.PicID + "_" + fileName;
                string fullPath = this.Master._GlobalFunctions.GetFullPath() + fileNameToSave;
                string relativePath = this.Master._GlobalFunctions.GetRelativePath() + fileNameToSave;

                if (File.Exists(g.PicFullPath))
                {
                    File.Delete(g.PicFullPath);
                }
                g.PicRelativePath = relativePath;
                this.designPicUpload.PostedFile.SaveAs(fullPath);

                g.PicName = fileName;
                g.PicFullPath = fullPath;
                g.PicRelativePath = relativePath;
            }
            catch (Exception r)
            {
                this.Master._Logger.Error(r, MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(36, "Red", this.designPicUpload.Value));
                this.ClearDesignPic();
                return;
            }
        }

        try
        {
            g.LastUpdate = TimeNow.TheTimeNow;
            g.spLastUpdate = TimeNow.TheTimeNow.ToShortDateString();

            this.Master._PapaDal.Update("designPic", g, TimeNow.TheTimeNow);
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
            this.ClearDesignPic();
            return;
        }

        DesignPic p = (DesignPic)this.Master._PapaDal.Get("designPic", this.designHiddenRe.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearDesignPic();
            return;
        }

        try
        {
            if (File.Exists(p.PicFullPath))
            {
                File.Delete(p.PicFullPath);
            }

            this.Master._PapaDal.Remove("designPic", p.PicID);
            this.Master._Logger.Log(new AdminException(". " + p.PicID + " Was Successfully Removed"),
                MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(12, "White", p.PicName));

            this.removeUpdatePicSelector.Items.Remove
            (this.removeUpdatePicSelector.Items.FindByValue("s" + p.PicID));
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
            this.ClearDesignPic();
            return;
        }

        DesignPic p = (DesignPic)this.Master._PapaDal.Get("designPic", this.designHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearDesignPic();
            return;
        }

        if (p.Active == 1)
        {
            this.Master._Logger.Warn(new AdminException
            (". " + this.designHiddenUp.Value + " Is Already Disabled"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(9, "Red", p.PicName));
            this.ClearDesignPic();
            return;
        }

        try
        {
            this.Master._PapaDal.Enable("designPic", this.designHiddenUp.Value);
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
            this.ClearDesignPic();
            return;
        }

        DesignPic p = (DesignPic)this.Master._PapaDal.Get("designPic", this.designHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearDesignPic();
            return;
        }

        if (p.Active == 2)
        {
            this.Master._Logger.Warn(new AdminException
            (". " + this.designHiddenUp.Value + " Is Already Disabled"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(8, "Red", p.PicName));
            this.ClearDesignPic();
            return;
        }

        try
        {
            this.Master._PapaDal.Disable("designPic", this.designHiddenUp.Value);
            this.Master._Logger.Log(new AdminException(". " + p.PicID +
                " Has Been Successfully Disabeld"), MethodBase.GetCurrentMethod().Name);
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

        string hebrewTextDesign = this.Master._GlobalFunctions.ConvertToUtf8(this.aboutDesignTextHe.Text);

        DesignText v = (DesignText)this.Master._PapaDal.Get("designText", "0");
        if (v == null)
        {
            try
            {
                DesignText e = new DesignText
                {
                    DesignTextID = this.Master._PapaDal.GetNextAvailableID("designText"),
                    TextHe = hebrewTextDesign,
                    TextEn = this.aboutDesignTextEn.Text,
                    LastUpdate = TimeNow.TheTimeNow,
                    spLastUpdate = TimeNow.TheTimeNow.ToString()
                };

                this.Master._PapaDal.Add("designText", e);
                this.Master._Logger.Log(new AdminException(". Design Text Was Successfully Added"),
                    MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(15, "White", "Design Text"));
            }
            catch (Exception e)
            {
                this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(16, "Red", "Design Text"));
            }
        }
        else
        {
            try
            {
                v.TextHe = hebrewTextDesign;
                v.TextEn = this.aboutDesignTextEn.Text;
                v.LastUpdate = TimeNow.TheTimeNow;
                v.spLastUpdate = TimeNow.TheTimeNow.ToString();

                this.Master._PapaDal.Update("designText", v, TimeNow.TheTimeNow);
                this.Master._Logger.Log(new AdminException(". Design Text Was Successfully Updated"),
                    MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(17, "White", "Design Text"));
            }
            catch (Exception e)
            {
                this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(18, "Red", "Design Text"));
            }
        }
    }

    protected void okBut_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(6))
        {
            return;
        }

        this.AfterOk(this.designHidden.Value);
    }

    protected void cancelBut_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    private void ShowDesignPicEnableDisable(bool visible)
    {
        this.disableDesignPicButton.Visible = visible;
        this.enableDesignPicButton.Visible = visible;
        if (visible)
        {
            this.disableDesignPicButton.Attributes["class"] = "mailYes";
            this.enableDesignPicButton.Attributes["class"] = "mailYes";
        }
        else
        {
            this.disableDesignPicButton.Attributes["class"] = "mailNo";
            this.enableDesignPicButton.Attributes["class"] = "mailNo";
        }
    }

    private void ShowDesignPicPicture(bool visible)
    {
        this.designPic.Visible = visible;
        if (visible)
        {
            this.designPic.Attributes["class"] = "mailYes";
        }
        else
        {
            this.designPic.Attributes["class"] = "mailNo";
        }
    }

    private void ShowDesignPicFile(bool visible)
    {
        this.designPicUpload.Visible = visible;
        if (visible)
        {
            this.designPicUpload.Attributes["class"] = "mailYes";
        }
        else
        {
            this.designPicUpload.Attributes["class"] = "mailNo";
        }
    }

    private void ClearDesignPic()
    {
        this.ClearFields(2);
        this.ClearFields(3);
        this.ClearFields(4);
    }

    public void ShowDesignPicUpdateInfo(bool visible)
    {
        this.designPicUpdateInfo.Visible = visible;
        if (visible)
        {
            this.designPicUpdateInfo.Attributes["class"] = "mailYes";
        }
        else
        {
            this.designPicUpdateInfo.Attributes["class"] = "mailNo";
        }
    }

    public void ShowAboutDesignUpdateInfo(bool visible)
    {
        this.aboutDesignUpdateInfo.Visible = visible;
        if (visible)
        {
            this.aboutDesignUpdateInfo.Attributes["class"] = "mailYes";
        }
        else
        {
            this.aboutDesignUpdateInfo.Attributes["class"] = "mailNo";
        }
    }

    private void Start()
    {
        this.ShowDesignPicEnableDisable(false);
        this.ShowDesignPicFile(false);
        this.ShowDesignPicPicture(false);

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
            this.designHidden.Value = "";
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
            this.designHidden.Value = message[2];

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
            this.DivSwitcher(5);
        }
    }
}
