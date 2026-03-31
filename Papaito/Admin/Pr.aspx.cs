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

public partial class Admin_Pr : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.prSelector.Items.Add(new ListItem("--Select Action--", "1"));
            this.prSelector.Items.Add(new ListItem("Add New Image", "2"));
            this.prSelector.Items.Add(new ListItem("Remove/Update Image", "3"));
            this.prSelector.Items.Add(new ListItem("Update Text", "4"));

            foreach (PrPic p in (IEnumerable<PrPic>)this.Master._PapaDal.GetAll("prPic"))
            {
                this.removeUpdatePicSelector.Items.Add(new ListItem(p.PicName, "s" + p.PicID));
            }

            this.DivSwitcher(1);
        }
    }

    protected void prSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!this.ValidateFields(1))
        {
            return;
        }

        this.DivSwitcher(int.Parse(this.prSelector.SelectedValue));
    }

    protected void removePrPicButton_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(7))
        {
            return;
        }

        this.Notify(this.Master._Notifier.Notify(5, "Red", this.removeUpdatePicSelector.SelectedItem.Text));
        this.prHiddenRe.Value = this.removeUpdatePicSelector.SelectedValue.Remove(0, 1);
    }

    protected void updatePrPicButton_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(7))
        {
            return;
        }

        this.prHiddenUp.Value = this.removeUpdatePicSelector.SelectedValue.Remove(0, 1);
        this.UpdatePicInit();
        this.DivSwitcher(2);
    }

    protected void cancelPrPicButton_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    protected void disablePrPicButton_Click(object sender, EventArgs e)
    {
        this.DisablePic();
    }

    protected void enablePrPicButton_Click(object sender, EventArgs e)
    {
        this.EnablePic();
    }

    protected void cancelPrPic_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    protected void updatePrAboutButton_Click(object sender, EventArgs e)
    {
        this.UpdateText();
    }

    protected void cancelPrAboutButton_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    protected void addPrPicButton_Click(object sender, EventArgs e)
    {
        if (this.prHiddenUp.Value == "")
        {
            this.AddPrPic();
        }
        else
        {
            this.UpdatePic();
            this.prHiddenUp.Value = "";
        }
    }

    protected void cancelAddPrPicButton_Click(object sender, EventArgs e)
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
                if (this.prSelector.SelectedIndex == 0)
                {
                    this.Master._Logger.Error(new AdminException(". this.prSelector.SelectedIndex == 0"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(1, "Red", ""));
                    return false;
                }
                break;
            case 2:
                if (this.prPicUpload.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.prPicUpload.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(37, "Red", "Pr"));
                    return false;
                }
                break;
            case 3:
                if (this.removeUpdatePicSelector.SelectedValue == null)
                {
                    this.Master._Logger.Error(new AdminException
                            (". this.removeUpdatePicSelector.SelectedValue == null"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(4, "Red", this.prPicUpload.Value));
                    return false;
                }
                break;
            case 4:
                if (this.prHiddenRe.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.prHiddenRe.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
                    return false;
                }
                break;
            case 5:
                if (this.prHiddenUp.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.prHiddenUp.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
                    return false;
                }
                break;
            case 6:
                if (this.prHidden.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.prHidden.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
                    return false;
                }
                break;
            case 7:
                if (this.removeUpdatePicSelector.SelectedValue == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.removeUpdatePicSelector.SelectedValue == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(57, "Red", "Pr"));
                    return false;
                }
                break;
            case 8:
                if (this.aboutPrTextHe.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.aboutPrTextHe.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(40, "Red", "Pr"));
                    return false;
                }

                if (this.aboutPrTextEn.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.aboutPrTextEn.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(41, "Red", "Pr"));
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
            this.Master._Logger.Error(new AdminException(". action <= 0"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            return;
        }

        switch (action)
        {
            case 1:
                foreach (ListItem l in this.prSelector.Items)
                {
                    l.Selected = false;
                }
                this.prPicStatusLabel.Text = "";
                this.prPicLastUpdateLabel.Text = "";
                this.prPic.Src = "";
                break;
            case 2:
                foreach (ListItem l in this.removeUpdatePicSelector.Items)
                {
                    l.Selected = false;
                }
                break;
            case 3:
                this.prHiddenRe.Value = "";
                this.prHiddenUp.Value = "";
                break;
            case 4:
                this.aboutPrLastUpdateLabel.Text = "";
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

        this.ShowAboutPrUpdateInfo(false);
        this.ShowPrPicUpdateInfo(false);

        this.mainPr.Visible = false;
        this.addPrPic.Visible = false;
        this.removeUpdatePic.Visible = false;
        this.aboutPr.Visible = false;
        this.prNotify.Visible = false;

        this.mainPr.Attributes["class"] = "mailNo";
        this.addPrPic.Attributes["class"] = "mailNo";
        this.removeUpdatePic.Attributes["class"] = "mailNo";
        this.aboutPr.Attributes["class"] = "mailNo";
        this.prNotify.Attributes["class"] = "mailNo";

        switch (action)
        {
            case 1:
                this.mainPr.Visible = true;
                this.mainPr.Attributes["class"] = "mailYes";
                break;
            case 2:
                this.addPrPic.Visible = true;
                this.addPrPic.Attributes["class"] = "mailYes";

                if (this.prHiddenUp.Value == "")
                {
                    this.ShowPrEnableDisable(false);
                    this.ShowPrFile(true);
                    this.ShowPrPicture(false);
                    this.ShowPrPicUpdateInfo(false);
                }
                else
                {
                    this.ShowPrEnableDisable(true);
                    this.ShowPrFile(true);
                    this.ShowPrPicture(true);
                    this.ShowPrPicUpdateInfo(true);
                }
                break;
            case 3:
                this.removeUpdatePic.Visible = true;
                this.removeUpdatePic.Attributes["class"] = "mailYes";
                break;
            case 4:
                this.aboutPr.Visible = true;
                this.aboutPr.Attributes["class"] = "mailYes";
                this.ShowAboutPrUpdateInfo(true);

                if (this.Master._PapaDal.GetCount("prText") > 0)
                {
                    PrText g = (PrText)this.Master._PapaDal.Get("prText", "0");
                    this.aboutPrTextHe.Text = g.TextHe;
                    this.aboutPrTextEn.Text = g.TextEn;
                    this.aboutPrLastUpdateLabel.Text = g.spLastUpdate;
                }
                break;
            case 5:
                this.prNotify.Visible = true;
                this.prNotify.Attributes["class"] = "mailYes";
                break;
            default:
                break;
        }
    }

    private void AddPrPic()
    {
        if (!this.ValidateFields(2))
        {
            this.ClearPrPic();
            return;
        }

        string ID = this.Master._PapaDal.GetNextAvailableID("prPic");
        string fileName = this.prPicUpload.PostedFile.FileName;
        string fileNameToSave = "prPic_id-" + ID + "_" + fileName;
        string fullPath = this.Master._GlobalFunctions.GetFullPath() + fileNameToSave;
        string relativePath = this.Master._GlobalFunctions.GetRelativePath() + fileNameToSave;

        if (!this.Master._GlobalFunctions.ValidatePicEnd(this.prPicUpload.Value))
        {
            this.Master._Logger.Error(new AdminException
            (". !this.Master._GlobalFunctions.ValidatePicEnd(this.prPicUpload.Value)"),
            MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(14, "Red", this.prPicUpload.Value));
            this.ClearPrPic();
            return;
        }

        try
        {
            this.prPicUpload.PostedFile.SaveAs(fullPath);
        }
        catch (Exception t)
        {
            this.Master._Logger.Log(t, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(13, "Red", this.prPicUpload.Value));
            this.ClearPrPic();
            return;
        }


        try
        {
            PrPic p = new PrPic
            {
                Active = 2,
                PicID = ID,
                PicFullPath = fullPath,
                PicRelativePath = relativePath,
                UploadTime = TimeNow.TheTimeNow,
                spUploadTime = TimeNow.TheTimeNow.ToString(),
                spActive = "Disable",
                LastUpdate = TimeNow.TheTimeNow,
                spLastUpdate = TimeNow.TheTimeNow.ToShortDateString(),
                PicName = fileName
            };

            this.Master._PapaDal.Add("prPic", p);
            this.Master._Logger.Log(new AdminException(". " + p.PicID +
                " Was Successfully Added"),
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
            this.ClearPrPic();
            return;
        }

        PrPic p = (PrPic)this.Master._PapaDal.Get("prPic", this.prHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearPrPic();
            return;
        }

        try
        {
            this.prPicStatusLabel.Text = p.spActive;
            this.prPicLastUpdateLabel.Text = p.spUploadTime;
            this.prPic.Src = p.PicRelativePath;
            this.ShowPrPicUpdateInfo(true);
            this.ShowPrEnableDisable(true);
            this.ShowPrFile(true);
            this.ShowPrPicture(true);
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
            this.ClearPrPic();
            return;
        }

        PrPic g = (PrPic)this.Master._PapaDal.Get("prPic", this.prHiddenUp.Value);
        if (g == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". g == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearPrPic();
            return;
        }

        if (this.prPicUpload.Value != "")
        {
            if (!this.Master._GlobalFunctions.ValidatePicEnd(this.prPicUpload.Value))
            {
                this.Master._Logger.Error(new AdminException
                (". !this.Master._GlobalFunctions.ValidatePicEnd(this.prPicUpload.Value)"),
                MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(14, "Red", this.prPicUpload.Value));
                this.ClearPrPic();
                return;
            }

            string fileName = this.prPicUpload.PostedFile.FileName;
            string fileNameToSave = "prPic_id-" + ID + "_" + fileName;
            string fullPath = this.Master._GlobalFunctions.GetFullPath() + fileNameToSave;
            string relativePath = this.Master._GlobalFunctions.GetRelativePath() + fileNameToSave;

            try
            {

                if (File.Exists(g.PicFullPath))
                {
                    File.Delete(g.PicFullPath);
                }
                this.prPicUpload.PostedFile.SaveAs(fullPath);

                g.PicName = fileName;
                g.PicFullPath = fullPath;
                g.PicRelativePath = relativePath;
            }
            catch (Exception r)
            {
                this.Master._Logger.Error(r, MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(36, "Red", g.PicName));
                this.ClearPrPic();
                return;
            }
        }

        try
        {
            g.LastUpdate = TimeNow.TheTimeNow;
            g.spLastUpdate = TimeNow.TheTimeNow.ToShortDateString();

            this.Master._PapaDal.Update("prPic", g, TimeNow.TheTimeNow);
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
            this.ClearPrPic();
            return;
        }

        PrPic p = (PrPic)this.Master._PapaDal.Get("prPic", this.prHiddenRe.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearPrPic();
            return;
        }

        try
        {
            if (File.Exists(p.PicFullPath))
            {
                File.Delete(p.PicFullPath);
            }

            this.Master._PapaDal.Remove("prPic", p.PicID);
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
            this.ClearPrPic();
            return;
        }

        PrPic p = (PrPic)this.Master._PapaDal.Get("prPic", this.prHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearPrPic();
            return;
        }

        if (p.Active == 1)
        {
            this.Master._Logger.Warn(new AdminException
            (". " + this.prHiddenUp.Value + " Is Already Disabled"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(9, "Red", p.PicName));
            this.ClearPrPic();
            return;
        }

        try
        {
            this.Master._PapaDal.Enable("prPic", p.PicID);
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
            this.ClearPrPic();
            return;
        }

        PrPic p = (PrPic)this.Master._PapaDal.Get("prPic", this.prHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearPrPic();
            return;
        }

        if (p.Active == 2)
        {
            this.Master._Logger.Warn(new AdminException
            (". " + p.PicID + " Is Already Disabled"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(8, "Red", p.PicName));
            this.ClearPrPic();
            return;
        }

        try
        {
            this.Master._PapaDal.Disable("prPic", p.PicID);
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
        if (!this.ValidateFields(8))
        {
            this.ClearPrPic();
            return;
        }

        string  hebrewTextPr = this.Master._GlobalFunctions.ConvertToUtf8(this.aboutPrTextHe.Text);

        PrText v = (PrText)this.Master._PapaDal.Get("prText", "0");
        if (v == null)
        {
            try
            {
                PrText e = new PrText
                {
                    PrTextID = this.Master._PapaDal.GetNextAvailableID("prText"),
                    TextHe = hebrewTextPr,
                    TextEn = this.aboutPrTextEn.Text,
                    LastUpdate = TimeNow.TheTimeNow,
                    spLastUpdate = TimeNow.TheTimeNow.ToString()
                };

                this.Master._PapaDal.Add("prText", e);
                this.Master._Logger.Log(new AdminException(".Pr Text Was Successfully Updated"),
                    MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(15, "White", "Pr Text"));
            }
            catch (Exception e)
            {
                this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(16, "Red", "Pr Text"));
            }
        }
        else
        {
            try
            {
                v.TextHe = hebrewTextPr;
                v.TextEn = this.aboutPrTextEn.Text;
                v.LastUpdate = TimeNow.TheTimeNow;
                v.spLastUpdate = TimeNow.TheTimeNow.ToString();

                this.Master._PapaDal.Update("prText", v, TimeNow.TheTimeNow);
                this.Master._Logger.Log(new AdminException(".Pr Text Was Successfully Updated"),
                    MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(17, "White", "Pr Text"));
            }
            catch (Exception e)
            {
                this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(18, "Red", "Pr Text"));
            }
        }
    }

    protected void okBut_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(6))
        {
            return;
        }

        this.AfterOk(this.prHidden.Value);
    }

    protected void cancelBut_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    private void ShowPrEnableDisable(bool visible)
    {
        this.disablePrPicButton.Visible = visible;
        this.enablePrPicButton.Visible = visible;
        if (visible)
        {
            this.disablePrPicButton.Attributes["class"] = "mailYes";
            this.enablePrPicButton.Attributes["class"] = "mailYes";
        }
        else
        {
            this.disablePrPicButton.Attributes["class"] = "mailNo";
            this.enablePrPicButton.Attributes["class"] = "mailNo";
        }
    }

    private void ShowPrPicture(bool visible)
    {
        this.prPic.Visible = visible;
        if (visible)
        {
            this.prPic.Attributes["class"] = "mailYes";
        }
        else
        {
            this.prPic.Attributes["class"] = "mailNo";
        }
    }

    private void ShowPrFile(bool visible)
    {
        this.prPicUpload.Visible = visible;
        if (visible)
        {
            this.prPicUpload.Attributes["class"] = "mailYes";
        }
        else
        {
            this.prPicUpload.Attributes["class"] = "mailNo";
        }
    }

    private void ClearPrPic()
    {
        this.ClearFields(2);
        this.ClearFields(3);
        this.ClearFields(4);
    }

    public void ShowPrPicUpdateInfo(bool visible)
    {
        this.prPicUpdateInfo.Visible = visible;
        if (visible)
        {
            this.prPicUpdateInfo.Attributes["class"] = "mailYes";
        }
        else
        {
            this.prPicUpdateInfo.Attributes["class"] = "mailNo";
        }
    }

    public void ShowAboutPrUpdateInfo(bool visible)
    {
        this.aboutPrUpdateInfo.Visible = visible;
        if (visible)
        {
            this.aboutPrUpdateInfo.Attributes["class"] = "mailYes";
        }
        else
        {
            this.aboutPrUpdateInfo.Attributes["class"] = "mailNo";
        }
    }

    private void Start()
    {
        this.ShowPrEnableDisable(false);
        this.ShowPrFile(false);
        this.ShowPrPicture(false);

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
            this.prHidden.Value = "";
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
            this.prHidden.Value = message[2];

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
