using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Drawing;
using Dal;
using System.IO;

public partial class Admin_Studio : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.studioSelector.Items.Add(new ListItem("--Select Action--", "1"));
            this.studioSelector.Items.Add(new ListItem("Add New Image", "2"));
            this.studioSelector.Items.Add(new ListItem("Remove/Update Image", "3"));
            this.studioSelector.Items.Add(new ListItem("Update Text", "4"));

            foreach (StudioRoomPic p in (IEnumerable<StudioRoomPic>)this.Master._PapaDal.GetAll("studioPic"))
            {
                this.removeUpdatePicSelector.Items.Add(new ListItem(p.PicName, "s" + p.PicID));
            }

            this.studioPicTypeSelector.Items.Add(new ListItem("Recording Room", "recordingRoom"));
            this.studioPicTypeSelector.Items.Add(new ListItem("Control Room", "controlRoom"));
            this.studioPicTypeSelector.Items.Add(new ListItem("Look Around", "studioLookAroundRoom"));
            this.studioPicTypeSelector.Items.Add(new ListItem("Who Plays Here", "studioWhoPlaysHere"));

            this.SetPicPlaceSelector("notWho", "none");

            this.DivSwitcher(1);
        }
    }

    protected void studioSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!this.ValidateFields(1))
        {
            return;
        }

        this.DivSwitcher(int.Parse(this.studioSelector.SelectedValue));
    }

    protected void removeStudioPicButton_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(3))
        {
            return;
        }

        this.Notify(this.Master._Notifier.Notify(5, "Red", this.removeUpdatePicSelector.SelectedItem.Text));
        this.studioHiddenRe.Value = this.removeUpdatePicSelector.SelectedValue.Remove(0, 1);
    }

    protected void updateStudioPicButton_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(3))
        {
            return;
        }

        this.studioHiddenUp.Value = this.removeUpdatePicSelector.SelectedValue.Remove(0, 1);

        StudioRoomPic p = (StudioRoomPic)this.Master._PapaDal.Get("studioPic", this.studioHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            return;
        }

        if (p.PicFullBWPath != "")
        {
            this.studioHiddenType.Value = p.PicFullBWPath;
        }

        this.UpdatePicInit();
        this.DivSwitcher(2);
    }

    protected void cancelStudioPicButton_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    protected void disableStudioPic_Click(object sender, EventArgs e)
    {
        this.DisablePic();
    }

    protected void enableStudioPic_Click(object sender, EventArgs e)
    {
        this.EnablePic();
    }

    protected void updateStudioAboutButton_Click(object sender, EventArgs e)
    {
        this.UpdateText();
    }

    protected void cancelStudioAboutButton_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    protected void addStudioPicButton_Click(object sender, EventArgs e)
    {
        if (this.studioHiddenUp.Value == "")
        {
            this.AddStudioPic();
        }
        else
        {
            this.UpdatePic();
            this.studioHiddenUp.Value = "";
            this.studioHiddenType.Value = "";
        }
    }

    protected void cancelAddStudioPicButton_Click(object sender, EventArgs e)
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
                if (this.studioSelector.SelectedIndex == 0)
                {
                    this.Master._Logger.Error(new AdminException(". this.studioSelector.SelectedIndex == 0"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(1, "Red", ""));
                    return false;
                }
                break;
            case 2:
                break;
            case 3:
                if (this.removeUpdatePicSelector.SelectedValue == "")
                {
                    this.Master._Logger.Error(new AdminException
                            (". this.removeUpdatePicSelector.SelectedValue == null"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(57, "Red", "Picture"));
                    return false;
                }
                break;
            case 4:
                if (this.studioHiddenRe.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.studioHiddenRe.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
                    return false;
                }
                break;
            case 5:
                if (this.studioHiddenUp.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.studioHiddenUp.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
                    return false;
                }
                break;
            case 6:
                if (this.studioHidden.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.studioHidden.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
                    return false;
                }
                break;
            case 7:
                if (this.studioBWPicUpload.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.studioBWPicUpload.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(3, "Red", this.studioBWPicUpload.Value));
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
                foreach (ListItem l in this.studioSelector.Items)
                {
                    l.Selected = false;
                }
                break;
            case 2:
                foreach (ListItem l in this.removeUpdatePicSelector.Items)
                {
                    l.Selected = false;
                }
                break;
            case 3:
                this.studioHiddenRe.Value = "";
                this.studioHiddenUp.Value = "";
                this.studioHiddenType.Value = "";
                break;
            case 4:
                foreach (ListItem l in this.studioPicTypeSelector.Items)
                {
                    l.Selected = false;
                }
                foreach (ListItem l in this.studioPicPlaceSelector.Items)
                {
                    l.Selected = false;
                }
                this.studioColorPicUploadPic.Src = "";
                this.studioBWPicUploadPic.Src = "";
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

        this.ShowAboutStudioUpdateInfo(false);
        this.ShowStudioPicUpdateInfo(false);

        this.mainStudio.Visible = false;
        this.addStudioPic.Visible = false;
        this.removeUpdatePic.Visible = false;
        this.aboutStudio.Visible = false;
        this.studioNotify.Visible = false;

        this.mainStudio.Attributes["class"] = "mailNo";
        this.addStudioPic.Attributes["class"] = "mailNo";
        this.removeUpdatePic.Attributes["class"] = "mailNo";
        this.aboutStudio.Attributes["class"] = "mailNo";
        this.studioNotify.Attributes["class"] = "mailNo";

        switch (action)
        {
            case 1:
                this.mainStudio.Visible = true;
                this.mainStudio.Attributes["class"] = "mailYes";
                break;
            case 2:
                this.addStudioPic.Visible = true;
                this.addStudioPic.Attributes["class"] = "mailYes";

                if (this.studioHiddenUp.Value == "")
                {
                    this.ShowColorFile(true);
                    this.ShowColorPic(false);
                    this.ShowEnableDisable(false);
                    this.ShowStudioPicUpdateInfo(false);
                }
                else
                {
                    this.ShowColorFile(true);
                    this.ShowColorPic(true);
                    this.ShowEnableDisable(true);
                    this.ShowStudioPicUpdateInfo(true);
                }
                break;
            case 3:
                this.removeUpdatePic.Visible = true;
                this.removeUpdatePic.Attributes["class"] = "mailYes";
                break;
            case 4:
                this.aboutStudio.Visible = true;
                this.aboutStudio.Attributes["class"] = "mailYes";
                this.ShowAboutStudioUpdateInfo(true);

                if (this.Master._PapaDal.GetCount("studioAbout") > 0)
                {
                    StudioAbout g = (StudioAbout)this.Master._PapaDal.Get("studioAbout", "0");
                    this.aboutTeStudioTextHe.Text = g.TechnicalHe;
                    this.aboutTeStudioTextEn.Text = g.TechnicalEn;
                    this.aboutStudioTextHe.Text = g.AboutHe;
                    this.aboutStudioTextEn.Text = g.AboutEn;
                    this.studioAboutLastUpdateLabel.Text = g.spLastUpdate;
                }
                break;
            case 5:
                this.studioNotify.Visible = true;
                this.studioNotify.Attributes["class"] = "mailYes";
                break;
            default:
                break;
        }
    }

    protected void studioPicTypeSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        string action = "";

        if (this.studioPicTypeSelector.SelectedIndex == 3)
        {
            if (this.studioHiddenUp.Value == "")
            {
                action = "add";
            }
            else
            {
                action = "update";
            }

            this.SetPicPlaceSelector("who", action);
        }
        else
        {
            this.SetPicPlaceSelector("notWho", "none");
        }
    }

    private void AddStudioPic()
    {
        if (this.studioColorPicUpload.Value == "")
        {
            this.Master._Logger.Error(new AdminException
            (". this.studioColorPicUpload.Value == \"\""), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(37, "Red", "Color Picture"));
            this.ClearStudioPic();
            return;
        }

        string ID = this.Master._PapaDal.GetNextAvailableID("studioPic");
        string fileColorName = this.studioColorPicUpload.PostedFile.FileName;
        string fileColorNameToSave = "studioColorPic_id-" + ID + "_" + fileColorName;
        string fullColorPath = this.Master._GlobalFunctions.GetFullPath() + fileColorNameToSave;
        string relativeColorPath = this.Master._GlobalFunctions.GetRelativePath() + fileColorNameToSave;
        string fileBWName = "";
        string fileBWNameToSave = "";
        string fullBWPath = "";
        string relativeBWPath = "";

        if (!this.Master._PapaDal.CheckAvailablePlace(this.studioPicTypeSelector.SelectedValue,
            int.Parse(this.studioPicPlaceSelector.SelectedValue)))
        {
            this.Master._Logger.Error(new AdminException
                (@". !this.Master._PapaDal.CheckAvailablePlace(""this.studioPicTypeSelector.SelectedValue"", 
                    int.Parse(this.studioPicPlace.Text))"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(21, "Red", this.studioPicPlaceSelector.SelectedValue));
            this.ClearStudioPic();
            return;
        }

        if (!this.Master._GlobalFunctions.ValidatePicEnd(this.studioColorPicUpload.Value))
        {
            this.Master._Logger.Error(new AdminException
            (". !this.Master._GlobalFunctions.ValidatePicEnd(this.studioColorPicUpload.Value)"),
            MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(14, "Red", this.studioColorPicUpload.Value));
            this.ClearStudioPic();
            return;
        }

        if (this.studioPicTypeSelector.SelectedIndex == 3 && this.studioBWPicUpload.Value == "")
        {
            this.Master._Logger.Error(new AdminException
            (". this.studioBWPicUpload.Value == \"\""), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(37, "Red", "Black And White Complex Picture"));
            this.ClearStudioPic();
            return;
        }
        else
        {
            if (!this.Master._GlobalFunctions.ValidatePicEnd(this.studioBWPicUpload.Value))
            {
                this.Master._Logger.Error(new AdminException
                (". !this.Master._GlobalFunctions.ValidatePicEnd(this.studioBWPicUpload.Value)"),
                MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(14, "Red", this.studioBWPicUpload.Value));
                this.ClearFields(3);
                return;
            }

            fileBWName = this.studioBWPicUpload.PostedFile.FileName;
            fileBWNameToSave = "studioBWPic_id-" + ID + "_" + fileBWName;
            fullBWPath = this.Master._GlobalFunctions.GetFullPath() + fileBWNameToSave;
            relativeBWPath = this.Master._GlobalFunctions.GetRelativePath() + fileBWNameToSave;

            try
            {
                this.studioBWPicUpload.PostedFile.SaveAs(fullBWPath);
            }
            catch (Exception b)
            {
                this.Master._Logger.Error(b, MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(36, "Red", this.studioBWPicUpload.Value));
                this.ClearStudioPic();
                return;
            }
        }

        try
        {
            this.studioColorPicUpload.PostedFile.SaveAs(fullColorPath);
        }
        catch (Exception m)
        {
            this.Master._Logger.Error(m, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(36, "Red", this.studioColorPicUpload.Value));
            this.ClearStudioPic();
            return;
        }

        try
        {
            StudioRoomPic p = new StudioRoomPic
            {
                Active = 2,
                PicName = fileColorName,
                PicPlace = int.Parse(this.studioPicPlaceSelector.SelectedValue),
                PicID = ID,
                PicFullBWPath = fullBWPath,
                PicRelativeBWPath = relativeBWPath,
                PicFullColorPath = fullColorPath,
                PicRelativeColorPath = relativeBWPath,
                LastUpdate = TimeNow.TheTimeNow,
                spLastUpdate = TimeNow.TheTimeNow.ToShortDateString(),
                RoomName = this.studioPicTypeSelector.SelectedValue,
                UploadTime = TimeNow.TheTimeNow,
                spUploadTime = TimeNow.TheTimeNow.ToString(),
                spActive = "Disable"
            };

            this.Master._PapaDal.Add("studioPic", p);
            this.Master._Logger.Log(new AdminException(". " + p.PicID + " Was Successfully Added"),
                            MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(15, "White", p.PicName));

            this.removeUpdatePicSelector.Items.Add(new ListItem(p.PicName, "s" + ID));
        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(16, "Red", fileBWName + " And " + fileColorName));
        }
    }

    private void UpdatePicInit()
    {
        if (!this.ValidateFields(5))
        {
            this.ClearStudioPic();
            return;
        }

        StudioRoomPic p = (StudioRoomPic)this.Master._PapaDal.Get("studioPic", this.studioHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearStudioPic();
            return;
        }

        foreach (ListItem l in this.studioPicTypeSelector.Items)
        {
            l.Selected = false;
        }

        if (p.RoomName == "studioWhoPlaysHere")
        {
            this.SetPicPlaceSelector("who", "update");
        }
        else
        {
            this.SetPicPlaceSelector("notWho", "none");
        }

        try
        {
            this.studioPicStatusLabel.Text = p.spActive;
            this.studioPicLastUpdateLabel.Text = p.spLastUpdate;
            this.studioPicTypeSelector.Items.FindByValue(p.RoomName).Selected = true;
            this.studioPicPlaceSelector.SelectedValue = p.PicPlace.ToString();
            this.studioColorPicUploadPic.Src = p.PicRelativeColorPath;
            this.ShowColorPic(true);
            this.ShowColorFile(true);
            this.ShowStudioPicUpdateInfo(true);

            if (p.PicFullBWPath != null)
            {
                if (p.PicFullBWPath != "")
                {
                    this.studioBWPicUploadPic.Src = p.PicRelativeBWPath;
                    this.ShowBWPic(true);
                    this.ShowBWFile(true);
                    this.ShowBWArea(true);
                }
            }
        }
        catch (Exception t)
        {
            this.Master._Logger.Error(t, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearStudioPic();
        }
    }

    private void UpdatePic()
    {
        if (!this.ValidateFields(5))
        {
            this.ClearStudioPic();
            return;
        }

        if (this.studioPicTypeSelector.SelectedIndex == 3 && this.studioBWPicUpload.Value == "")
        {
            this.Master._Logger.Error(new AdminException
            (". this.studioBWPicUpload.Value == \"\""), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(37, "Red", "Black And White Complex Picture"));
            this.ClearStudioPic();
            return;
        }

        StudioRoomPic p = (StudioRoomPic)this.Master._PapaDal.Get("studioPic", this.studioHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearStudioPic();
            return;
        }

        if (!this.Master._PapaDal.CheckAvailablePlaceExcept(this.studioPicTypeSelector.SelectedValue,
            p.PicPlace, int.Parse(this.studioPicPlaceSelector.SelectedValue)))
        {
            this.Master._Logger.Error(new AdminException
                (@". if (!this.Master._PapaDal.CheckAvailablePlaceExcept(""this.studioPicTypeSelector.SelectedValue"",
            p.PicPlace, int.Parse(this.studioPicPlaceSelector.SelectedValue)))"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(21, "Red", this.studioPicPlaceSelector.SelectedValue));
            this.ClearStudioPic();
            return;
        }

        if (this.studioColorPicUpload.Value != "")
        {
            if (!this.Master._GlobalFunctions.ValidatePicEnd(this.studioColorPicUpload.Value))
            {
                this.Master._Logger.Error(new AdminException
                (". !this.Master._GlobalFunctions.ValidatePicEnd(this.studioColorPicUpload.Value)"),
                MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(14, "Red", this.studioColorPicUpload.Value));
                this.ClearStudioPic();
                return;
            }

            string fileColorName = this.studioColorPicUpload.PostedFile.FileName;
            string fileColorNameToSave = "studioColorPic_id-" + ID + "_" + fileColorName;
            string fullColorPath = this.Master._GlobalFunctions.GetFullPath() + fileColorNameToSave;
            string relativeColorPath = this.Master._GlobalFunctions.GetRelativePath() + fileColorNameToSave;

            try
            {
                if (File.Exists(p.PicFullColorPath))
                {
                    File.Delete(p.PicFullColorPath);
                }
                this.studioColorPicUpload.PostedFile.SaveAs(fullColorPath);

                p.PicName = fileColorName;
                p.PicFullColorPath = fullColorPath;
                p.PicRelativeColorPath = relativeColorPath;
            }
            catch (Exception f)
            {
                this.Master._Logger.Error(f, MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(36, "Red", this.studioColorPicUpload.Value));
                this.ClearStudioPic();
                return;
            }
        }

        if (this.studioBWPicUpload.Value != "")
        {
            if (!this.Master._GlobalFunctions.ValidatePicEnd(this.studioBWPicUpload.Value))
            {
                this.Master._Logger.Error(new AdminException
                (". !this.Master._GlobalFunctions.ValidatePicEnd(this.studioBWPicUpload.Value)"),
                MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(14, "Red", this.studioBWPicUpload.Value));
                this.ClearStudioPic();
                return;
            }

            string fileBWName = this.studioBWPicUpload.PostedFile.FileName;
            string fileBWNameToSave = "studioBWPic_id-" + p.PicID + "_" + fileBWName;
            string fullBWPath = this.Master._GlobalFunctions.GetFullPath() + fileBWNameToSave;
            string relativeBWPath = this.Master._GlobalFunctions.GetRelativePath() + fileBWNameToSave;

            try
            {
                if (File.Exists(p.PicFullBWPath))
                {
                    File.Delete(p.PicFullBWPath);
                }
                this.studioBWPicUpload.PostedFile.SaveAs(fullBWPath);

                p.PicFullBWPath = fullBWPath;
                p.PicRelativeBWPath = relativeBWPath;
            }
            catch (Exception f)
            {
                this.Master._Logger.Error(f, MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(36, "Red", this.studioBWPicUpload.Value));
                this.ClearStudioPic();
                return;
            }
        }
        else
        {
            if (File.Exists(p.PicFullBWPath))
            {
                File.Delete(p.PicFullBWPath);
            }

            p.PicFullBWPath = "";
            p.PicRelativeBWPath = "";
        }

        try
        {
            p.spLastUpdate = TimeNow.TheTimeNow.ToShortDateString();
            p.LastUpdate = TimeNow.TheTimeNow;
            p.RoomName = this.studioPicTypeSelector.SelectedValue;
            p.PicPlace = int.Parse(this.studioPicPlaceSelector.SelectedValue);

            this.Master._PapaDal.Update("studioPic", p, TimeNow.TheTimeNow);
            this.Master._Logger.Log(new AdminException(". " + p.PicID +
                    " Was Successfully Updated"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(17, "White", p.PicName));

            this.removeUpdatePicSelector.Items.FindByValue("s" + p.PicID).Text = p.PicName;
        }
        catch (Exception e)
        {
            this.Master._Logger.Log(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(18, "Red", p.PicName));
        }
    }

    private void ShowEnableDisable(bool visible)
    {
        this.disableStudioPic.Visible = visible;
        this.enableStudioPic.Visible = visible;
    }

    private void ShowBWArea(bool visible)
    {
        this.morePic.Visible = visible;
    }

    private void ShowBWPic(bool visible)
    {
        if (this.studioBWPicUploadPic.Src == "")
        {
            this.studioBWPicUploadPic.Visible = false;
            return;
        }

        this.studioBWPicUploadPic.Visible = visible;
    }

    private void ShowColorPic(bool visible)
    {
        if (this.studioColorPicUploadPic.Src == "")
        {
            this.studioColorPicUploadPic.Visible = false;
            return;
        }

        this.studioColorPicUploadPic.Visible = visible;
    }

    private void ShowBWFile(bool visible)
    {
        this.studioBWPicUpload.Visible = visible;
    }

    private void ShowColorFile(bool visible)
    {
        this.studioColorPicUpload.Visible = visible;
    }

    private void RemovePic()
    {
        if (!this.ValidateFields(4))
        {
            this.ClearStudioPic();
            return;
        }

        StudioRoomPic p = (StudioRoomPic)this.Master._PapaDal.Get("studioPic", this.studioHiddenRe.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearFields(3);
            return;
        }

        try
        {
            if (p.PicFullBWPath != "")
            {
                File.Delete(p.PicFullBWPath);
            }

            if (p.PicFullColorPath != "")
            {
                File.Delete(p.PicFullColorPath);
            }

            this.Master._PapaDal.Remove("studioPic", p.PicID);
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
            this.ClearFields(3);
            return;
        }

        StudioRoomPic p = (StudioRoomPic)this.Master._PapaDal.Get("studioPic", this.studioHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearStudioPic();
            return;
        }

        if (p.Active == 1)
        {
            this.Master._Logger.Warn(new AdminException
            (". " + this.studioHiddenUp.Value + " Is Already Disabled"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(9, "Red", p.PicName));
            this.ClearStudioPic();
            return;
        }

        if (!this.Master._PapaDal.CheckPlacesStatus(p.RoomName))
        {
            this.Master._Logger.Warn(new AdminException
            (". Max Pictures For " + p.RoomName), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(54, "Red", p.RoomName));
            this.ClearStudioPic();
            return;
        }

        try
        {
            this.Master._PapaDal.Enable("studioPic", p.PicID);
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
            this.ClearStudioPic();
            return;
        }

        StudioRoomPic p = (StudioRoomPic)this.Master._PapaDal.Get("studioPic", this.studioHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", p.PicName));
            this.ClearStudioPic();
            return;
        }

        if (p.Active == 2)
        {
            this.Master._Logger.Warn(new AdminException
            (". " + this.studioHiddenUp.Value + " Is Already Disabled"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(8, "Red", p.PicName));
            this.ClearStudioPic();
            return;
        }

        try
        {
            this.Master._PapaDal.Disable("studioPic", p.PicID);
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

        string studioTeTextHe = this.Master._GlobalFunctions.ConvertToUtf8(this.aboutTeStudioTextHe.Text);
        string studioAboutTextHe = this.Master._GlobalFunctions.ConvertToUtf8(this.aboutStudioTextHe.Text);

        StudioAbout v = (StudioAbout)this.Master._PapaDal.Get("studioAbout", "0");
        if (v == null)
        {
            try
            {
                StudioAbout e = new StudioAbout
                {
                    StudioAboutID = this.Master._PapaDal.GetNextAvailableID("studioAbout"),
                    AboutHe = studioAboutTextHe,
                    AboutEn = this.aboutStudioTextEn.Text,
                    TechnicalEn = this.aboutTeStudioTextEn.Text,
                    TechnicalHe = studioTeTextHe,
                    LastUpdate = TimeNow.TheTimeNow,
                    spLastUpdate = TimeNow.TheTimeNow.ToString()
                };

                this.Master._PapaDal.Add("studioAbout", e);
                this.Master._Logger.Log(new AdminException(". Studio Text Was Successfully Updated"),
                    MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(15, "White", "Studio Text"));
            }
            catch (Exception e)
            {
                this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(16, "Red", "Studio Text"));
            }
        }
        else
        {
            try
            {
                v.AboutHe = studioAboutTextHe;
                v.AboutEn = this.aboutStudioTextEn.Text;
                v.TechnicalEn = this.aboutTeStudioTextEn.Text;
                v.TechnicalHe = studioTeTextHe;
                v.LastUpdate = TimeNow.TheTimeNow;
                v.spLastUpdate = TimeNow.TheTimeNow.ToString();

                this.Master._PapaDal.Update("studioAbout", v, TimeNow.TheTimeNow);
                this.Master._Logger.Log(new AdminException(". Studio Text Was Successfully Updated"),
                    MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(17, "White", "Studio Text"));
            }
            catch (Exception e)
            {
                this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(18, "Red", "Studio Text"));
            }
        }
    }

    protected void okBut_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(6))
        {
            return;
        }

        this.AfterOk(this.studioHidden.Value);
    }

    protected void cancelBut_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    private void ClearStudioPic()
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

        this.ShowBWArea(false);
        this.ShowBWPic(false);
        this.ShowBWFile(false);
        this.ShowColorPic(false);
        this.ShowColorFile(false);
        this.ShowEnableDisable(false);

        this.DivSwitcher(1);
    }

    public void ShowStudioPicUpdateInfo(bool visible)
    {
        this.studioPicUpdateInfo.Visible = visible;
    }

    public void ShowAboutStudioUpdateInfo(bool visible)
    {
        this.studioAboutUpdateInfo.Visible = visible;
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
            this.studioHidden.Value = "";
        }
    }

    private void SetPicPlaceSelector(string type, string action)
    {
        this.ShowBWArea(true);

        if (type == "" || type == null || action == "" || action == null)
        {
            return;
        }

        int count = 0;

        switch (type)
        {
            case "notWho":
                count = 4;
                break;
            case "who":
                count = 13;
                break;
            default:
                break;
        }

        switch (action)
        {
            case "none":
                this.ShowBWArea(false);
                break;
            case "add":
                this.ShowBWPic(false);
                this.ShowBWFile(true);
                break;
            case "update":
                this.ShowBWPic(true);
                this.ShowBWFile(true);
                break;
            default:
                break;
        }

        this.studioPicPlaceSelector.Items.Clear();
        for (int i = 0; i < count; i++)
        {
            this.studioPicPlaceSelector.Items.Add(new ListItem(i.ToString(), i.ToString()));
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
            this.studioHidden.Value = message[2];

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
