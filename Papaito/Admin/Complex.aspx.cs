using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Drawing;
using Dal;
using System.Configuration;
using System.IO;

public partial class Admin_Complex : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.complexSelector.Items.Add(new ListItem("--Select Action--", "1"));
            this.complexSelector.Items.Add(new ListItem("Add New Image", "2"));
            this.complexSelector.Items.Add(new ListItem("Remove/Update Image", "3"));
            this.complexSelector.Items.Add(new ListItem("Update Text", "4"));

            foreach (ComplexRoomPic p in (IEnumerable<ComplexRoomPic>)this.Master._PapaDal.GetAll("complexPic"))
            {
                this.removeUpdatePicSelector.Items.Add(new ListItem(p.RoomTitleHe, "s" + p.PicID));
            }

            this.complexPicTypeSelector.Items.Add(new ListItem("Room A", "roomA"));
            this.complexPicTypeSelector.Items.Add(new ListItem("Room B", "roomB"));
            this.complexPicTypeSelector.Items.Add(new ListItem("Room C", "roomC"));
            this.complexPicTypeSelector.Items.Add(new ListItem("Look Around", "complexLookAroundRoom"));
            this.complexPicTypeSelector.Items.Add(new ListItem("Who Plays Here", "complexWhoPlaysHere"));

            this.SetPicPlaceSelector("notWho", "none");

            this.DivSwitcher(1);
        }
    }

    protected void complexSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!this.ValidateFields(1))
        {
            return;
        }

        this.DivSwitcher(int.Parse(this.complexSelector.SelectedValue));
    }

    protected void removeComplexPicButton_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(8))
        {
            return;
        }

        this.Notify(this.Master._Notifier.Notify(5, "Red", this.removeUpdatePicSelector.SelectedItem.Text));
        this.complexHiddenRe.Value = this.removeUpdatePicSelector.SelectedValue.Remove(0, 1);
    }

    protected void updateComplexPicButton_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(8))
        {
            return;
        }

        this.complexHiddenUp.Value = this.removeUpdatePicSelector.SelectedValue.Remove(0, 1);

        ComplexRoomPic p = (ComplexRoomPic)this.Master._PapaDal.Get("complexPic", this.complexHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            return;
        }

        if (p.PicBWFullPath != null)
        {
            if (p.PicBWFullPath != "")
            {
                this.complexHiddenType.Value = p.PicBWFullPath;
            }
        }

        this.UpdatePicInit();
        this.DivSwitcher(2);
    }

    protected void cancelComplexPicButton_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    protected void disableComplexPic_Click(object sender, EventArgs e)
    {
        this.DisablePic();
    }

    protected void enableComplexPic_Click(object sender, EventArgs e)
    {
        this.EnablePic();
    }

    protected void updateComplexAboutButton_Click(object sender, EventArgs e)
    {
        this.UpdateText();
    }

    protected void cancelComplexAboutButton_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    protected void addComplexPicButton_Click(object sender, EventArgs e)
    {
        if (this.complexHiddenUp.Value == "")
        {
            this.AddComplexPic();
        }
        else
        {
            this.UpdatePic();
            this.complexHiddenUp.Value = "";
            this.complexHiddenType.Value = "";
        }
    }

    protected void cancelAddComplexPicButton_Click(object sender, EventArgs e)
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
                if (this.complexSelector.SelectedIndex == 0)
                {
                    this.Master._Logger.Error(new AdminException(". this.complexSelector.SelectedIndex == 0"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(1, "Red", ""));
                    return false;
                }
                break;
            case 2:
                if (this.complexTitleRoomHe.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.complexTitleRoomHe.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(40, "Red", "Complex Room Name"));
                    return false;
                }

                if (this.complexTitleRoomEn.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.complexTitleRoomEn.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(41, "Red", "Complex Room Name"));
                    return false;
                }

                if (this.complexAboutRoomHe.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.complexAboutRoomHe.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(40, "Red", "About The Complex Room"));
                    return false;
                }

                if (this.complexAboutRoomEn.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.complexAboutRoomEn.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(41, "Red", "About The Complex Room"));
                    return false;
                }

                break;
            case 3:
                if (this.removeUpdatePicSelector.SelectedValue == "")
                {
                    this.Master._Logger.Error(new AdminException
                            (". this.removeUpdatePicSelector.SelectedValue == null"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(4, "Red", this.complexColorPicUpload.Value));
                    return false;
                }
                break;
            case 4:
                if (this.complexHiddenRe.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.complexHiddenRe.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
                    return false;
                }
                break;
            case 5:
                if (this.complexHiddenUp.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.complexHiddenUp.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
                    return false;
                }
                break;
            case 6:
                if (this.complexHidden.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.complexHidden.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
                    return false;
                }
                break;
            case 7:
                if (this.complexBWPicUpload.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.complexBWPicUpload.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(3, "Red", this.complexBWPicUpload.Value));
                    return false;
                }
                break;
            case 8:
                if (this.removeUpdatePicSelector.SelectedValue == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.removeUpdatePicSelector.SelectedValue == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(57, "Red", "Picture"));
                    return false;
                }
                break;
            case 9:
                if (this.aboutComplexTextHe.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.aboutComplexTextHe.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(40, "Red", "Complex About"));
                    return false;
                }

                if (this.aboutComplexTextEn.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.aboutComplexTextEn.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(41, "Red", "Complex About"));
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
                foreach (ListItem l in this.complexSelector.Items)
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
                this.complexHiddenRe.Value = "";
                this.complexHiddenUp.Value = "";
                this.complexHiddenType.Value = "";
                break;
            case 4:
                foreach (ListItem l in this.complexPicTypeSelector.Items)
                {
                    l.Selected = false;
                }
                foreach (ListItem l in this.complexPicPlaceSelector.Items)
                {
                    l.Selected = false;
                }
                this.complexAboutRoomEn.Text = "";
                this.complexAboutRoomHe.Text = "";
                this.complexTitleRoomEn.Text = "";
                this.complexTitleRoomHe.Text = "";
                this.complexPicLastUpdateLabel.Text = "";
                this.complexPicStatusLabel.Text = "";
                this.complexColorPicUploadPic.Src = "";
                this.complexBWPicUploadPic.Src = "";
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

        this.ShowAboutComplexUpdateInfo(false);
        this.ShowComplexPicUpdateInfo(false);

        this.mainComplex.Visible = false;
        this.addComplexPic.Visible = false;
        this.removeUpdatePic.Visible = false;
        this.aboutComplex.Visible = false;
        this.complexNotify.Visible = false;

        this.mainComplex.Attributes["class"] = "mailNo";
        this.addComplexPic.Attributes["class"] = "mailNo";
        this.removeUpdatePic.Attributes["class"] = "mailNo";
        this.aboutComplex.Attributes["class"] = "mailNo";
        this.complexNotify.Attributes["class"] = "mailNo";

        switch (action)
        {
            case 1:
                this.mainComplex.Visible = true;
                this.mainComplex.Attributes["class"] = "mailYes";
                break;
            case 2:
                this.addComplexPic.Visible = true;
                this.addComplexPic.Attributes["class"] = "mailYes";

                if (this.complexHiddenUp.Value == "")
                {
                    this.ShowColorFile(true);
                    this.ShowColorPic(false);
                    this.ShowEnableDisable(false);
                    this.ShowComplexPicUpdateInfo(false);
                }
                else
                {
                    this.ShowColorFile(true);
                    this.ShowColorPic(true);
                    this.ShowEnableDisable(true);
                    this.ShowComplexPicUpdateInfo(true);
                }
                break;
            case 3:
                this.removeUpdatePic.Visible = true;
                this.removeUpdatePic.Attributes["class"] = "mailYes";
                break;
            case 4:
                this.aboutComplex.Visible = true;
                this.aboutComplex.Attributes["class"] = "mailYes";
                this.ShowAboutComplexUpdateInfo(true);

                if (this.Master._PapaDal.GetCount("complexAbout") > 0)
                {
                    ComplexAbout g = (ComplexAbout)this.Master._PapaDal.Get("complexAbout", "0");
                    this.aboutComplexTextHe.Text = g.AboutHe;
                    this.aboutComplexTextEn.Text = g.AboutEn;
                    this.complexAboutLastUpdate.Text = g.spLastUpdate;
                }
                break;
            case 5:
                this.complexNotify.Visible = true;
                this.complexNotify.Attributes["class"] = "mailYes";
                break;
            default:
                break;
        }
    }

    protected void complexPicTypeSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        string action = "";

        if (this.complexPicTypeSelector.SelectedIndex == 4)
        {
            if (this.complexHiddenUp.Value == "")
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

    private void AddComplexPic()
    {
        if (!this.ValidateFields(2))
        {
            this.ClearComplexPic();
            return;
        }

        if (this.complexColorPicUpload.Value == "")
        {
            this.Master._Logger.Error(new AdminException
            (". this.complexColorPicUpload.Value == \"\""), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(37, "Red", "Complex Color"));
            return;
        }

        string ID = this.Master._PapaDal.GetNextAvailableID("complexPic");
        string fileColorName = this.complexColorPicUpload.PostedFile.FileName;
        string fileColorNameToSave = "complexColorPic_id-" + ID + "_" + fileColorName;
        string fullColorPath = this.Master._GlobalFunctions.GetFullPath() + fileColorNameToSave;
        string relativeColorPath = this.Master._GlobalFunctions.GetRelativePath() + fileColorNameToSave;
        string fileBWName = "";
        string fileBWNameToSave = "";
        string fullBWPath = "";
        string relativeBWPath = "";

        if (!this.Master._PapaDal.CheckAvailablePlace(this.complexPicTypeSelector.SelectedValue,
                    int.Parse(this.complexPicPlaceSelector.SelectedValue)))
        {
            this.Master._Logger.Error(new AdminException
                (@". (!this.Master._PapaDal.CheckAvailablePlace(this.complexPicTypeSelector.SelectedValue,
                    int.Parse(this.complexPicPlaceSelector.SelectedValue)))"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(21, "Red", this.complexPicPlaceSelector.SelectedValue));
            this.ClearComplexPic();
            return;
        }

        if (this.complexPicTypeSelector.SelectedIndex == 4 && this.complexBWPicUpload.Value == "")
        {
            this.Master._Logger.Error(new AdminException
            (". this.complexBWPicUpload.Value == \"\""), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(37, "Red", "Black And White Complex Picture"));
            return;
        }
        else
        {
            if (!this.Master._GlobalFunctions.ValidatePicEnd(this.complexBWPicUpload.Value))
            {
                this.Master._Logger.Error(new AdminException
                (". !this.Master._GlobalFunctions.ValidatePicEnd(this.complexBWPicUpload.Value)"),
                MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(14, "Red", this.complexBWPicUpload.Value));
                this.ClearComplexPic();
                return;
            }

            fileBWName = this.complexBWPicUpload.PostedFile.FileName;
            fileBWNameToSave = "complexBWPic_id-" + ID + "_" + fileBWName;
            fullBWPath = this.Master._GlobalFunctions.GetFullPath() + fileBWNameToSave;
            relativeBWPath = this.Master._GlobalFunctions.GetRelativePath() + fileBWNameToSave;

            if (!this.Master._GlobalFunctions.ValidatePicEnd(this.complexColorPicUpload.Value))
            {
                this.Master._Logger.Error(new AdminException
                (". !this.Master._GlobalFunctions.ValidatePicEnd(this.complexColorPicUpload.Value)"),
                MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(14, "Red", this.complexColorPicUpload.Value));
                this.ClearComplexPic();
                return;
            }
        }

        if (this.complexBWPicUpload.Value != "")
        {
            try
            {
                this.complexBWPicUpload.PostedFile.SaveAs(fullBWPath);
            }
            catch (Exception t)
            {
                this.Master._Logger.Error(t, MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(36, "Red", this.complexBWPicUpload.Value));
                this.ClearComplexPic();
                return;
            }
        }

        try
        {
            this.complexColorPicUpload.PostedFile.SaveAs(fullColorPath);
        }
        catch (Exception o)
        {
            this.Master._Logger.Error(o, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(36, "Red", this.complexColorPicUpload.Value));
            this.ClearComplexPic();
            return;
        }

        string title = "";
        string room = "";

        try
        {
            room = this.Master._GlobalFunctions.ConvertToUtf8(this.complexAboutRoomHe.Text);
            title = this.Master._GlobalFunctions.ConvertToUtf8(this.complexTitleRoomHe.Text);

            ComplexRoomPic p = new ComplexRoomPic
            {
                Active = 2,
                PicPlace = int.Parse(this.complexPicPlaceSelector.SelectedValue),
                RoomName = this.complexPicTypeSelector.SelectedValue,
                RoomTextEn = this.complexAboutRoomEn.Text,
                RoomTextHe = room,
                RoomTitleEn = this.complexTitleRoomEn.Text,
                RoomTitleHe = title,
                PicID = ID,
                PicBWFullPath = fullBWPath,
                PicBWRelativePath = relativeBWPath,
                PicColorFullPath = fullColorPath,
                PicColorRelativePath = relativeColorPath,
                LastUpdate = TimeNow.TheTimeNow,
                spLastUpdate = TimeNow.TheTimeNow.ToShortDateString(),
                UploadTime = TimeNow.TheTimeNow,
                spUploadTime = TimeNow.TheTimeNow.ToString(),
                spActive = "Disable"
            };

            this.Master._PapaDal.Add("complexPic", p);
            this.Master._Logger.Log(new AdminException(". " + p.PicID + " Was Successfully Added"),
                            MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(15, "White", p.RoomTitleHe));

            this.removeUpdatePicSelector.Items.Add(new ListItem(p.RoomTitleHe, "s" + ID));
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
            this.ClearComplexPic();
            return;
        }

        ComplexRoomPic p = (ComplexRoomPic)this.Master._PapaDal.Get("complexPic", this.complexHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearComplexPic();
            return;
        }

        if (p.RoomName == "complexWhoPlaysHere")
        {
            this.SetPicPlaceSelector("who", "update");
        }
        else
        {
            this.SetPicPlaceSelector("notWho", "none");
        }

        foreach (ListItem l in this.complexPicTypeSelector.Items)
        {
            l.Selected = false;
        }

        try
        {

            this.complexPicStatusLabel.Text = p.spActive;
            this.complexPicLastUpdateLabel.Text = p.spLastUpdate;
            this.complexPicTypeSelector.Items.FindByValue(p.RoomName).Selected = true;
            this.complexPicPlaceSelector.SelectedValue = p.PicPlace.ToString();
            this.complexTitleRoomHe.Text = p.RoomTitleHe;
            this.complexTitleRoomEn.Text = p.RoomTitleEn;
            this.complexAboutRoomHe.Text = p.RoomTextHe;
            this.complexAboutRoomEn.Text = p.RoomTextEn;
            this.complexColorPicUploadPic.Src = p.PicColorRelativePath;
            this.ShowColorPic(true);
            this.ShowColorFile(true);

            if (p.PicBWFullPath != null)
            {
                if (p.PicBWFullPath != "")
                {
                    this.complexBWPicUploadPic.Src = p.PicBWRelativePath;
                    this.ShowBWPic(true);
                    this.ShowBWFile(true);
                }
            }
        }

        catch (Exception t)
        {
            this.Master._Logger.Error(t, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearComplexPic();
        }
    }

    private void UpdatePic()
    {
        if (!this.ValidateFields(2))
        {
            this.ClearComplexPic();
            return;
        }

        if (!this.ValidateFields(8))
        {
            this.ClearComplexPic();
            return;
        }

        if (this.complexBWPicUpload.Value == "" && this.complexPicTypeSelector.SelectedIndex == 4)
        {
            this.Master._Logger.Error(new AdminException
            (". this.complexBWPicUpload.Value == \"\""), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(37, "Red", "Black And White Complex Pic"));
            this.ClearComplexPic();
            this.ShowBWArea(false);
            return;
        }

        ComplexRoomPic p = (ComplexRoomPic)this.Master._PapaDal.Get("complexPic", this.complexHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearComplexPic();
            return;
        }

        if (!this.Master._PapaDal.CheckAvailablePlaceExcept(this.complexPicTypeSelector.SelectedValue,
            p.PicPlace, int.Parse(this.complexPicPlaceSelector.SelectedValue)))
        {
            this.Master._Logger.Error(new AdminException
                (@". (!this.Master._PapaDal.CheckAvailablePlaceExcept(this.complexPicTypeSelector.SelectedValue,
            p.PicPlace, int.Parse(this.complexPicPlace.Text)))"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(21, "Red", this.complexPicPlaceSelector.SelectedValue));
            this.ClearComplexPic();
            return;
        }

        if (this.complexColorPicUpload.Value != "")
        {
            if (!this.Master._GlobalFunctions.ValidatePicEnd(this.complexColorPicUpload.Value))
            {
                this.Master._Logger.Error(new AdminException
                (". !this.Master._GlobalFunctions.ValidatePicEnd(this.complexColorPicUpload.Value)"),
                MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(14, "Red", this.complexColorPicUpload.Value));
                this.ClearComplexPic();
                return;
            }

            string fileColorName = this.complexColorPicUpload.PostedFile.FileName;
            string fileColorNameToSave = "complexColorPic_id-" + p.PicID + "_" + fileColorName;
            string fullColorPath = this.Master._GlobalFunctions.GetFullPath() + fileColorNameToSave;
            string relativeColorPath = this.Master._GlobalFunctions.GetRelativePath() + fileColorNameToSave;

            try
            {
                if (File.Exists(p.PicColorFullPath))
                {
                    File.Delete(p.PicColorFullPath);
                }
                this.complexColorPicUpload.PostedFile.SaveAs(fullColorPath);

                p.PicColorFullPath = fullColorPath;
                p.PicColorRelativePath = relativeColorPath;
            }
            catch (Exception f)
            {
                this.Master._Logger.Error(f, MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(36, "Red", this.complexColorPicUpload.Value));
                this.ClearComplexPic();
                return;
            }
        }

        if (this.complexBWPicUpload.Value != "")
        {
            if (!this.Master._GlobalFunctions.ValidatePicEnd(this.complexBWPicUpload.Value))
            {
                this.Master._Logger.Error(new AdminException
                (". !this.Master._GlobalFunctions.ValidatePicEnd(this.complexBWPicUpload.Value)"),
                MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(14, "Red", p.RoomTitleHe));
                this.ClearComplexPic();
                return;
            }

            string fileBWName = this.complexBWPicUpload.PostedFile.FileName;
            string fileBWNameToSave = "complexBWPic_id-" + p.PicID + "_" + fileBWName;
            string fullBWPath = this.Master._GlobalFunctions.GetFullPath() + fileBWNameToSave;
            string relativeBWPath = this.Master._GlobalFunctions.GetRelativePath() + fileBWNameToSave;

            try
            {
                if (File.Exists(p.PicBWFullPath))
                {
                    File.Delete(p.PicBWFullPath);
                }
                this.complexBWPicUpload.PostedFile.SaveAs(fullBWPath);

                p.PicBWFullPath = fullBWPath;
                p.PicBWRelativePath = relativeBWPath;

            }
            catch (Exception f)
            {
                this.Master._Logger.Error(f, MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(36, "Red", this.complexBWPicUpload.Value));
                this.ClearComplexPic();
                return;
            }
        }
        else
        {
            if (File.Exists(p.PicBWFullPath))
            {
                File.Delete(p.PicBWFullPath);
            }

            p.PicBWFullPath = "";
            p.PicBWRelativePath = "";
        }

        try
        {
            p.spLastUpdate = TimeNow.TheTimeNow.ToShortDateString();
            p.LastUpdate = TimeNow.TheTimeNow;
            p.RoomName = this.complexPicTypeSelector.SelectedValue;
            p.PicPlace = int.Parse(this.complexPicPlaceSelector.SelectedValue);
            p.RoomTitleHe = this.complexTitleRoomHe.Text;
            p.RoomTitleEn = this.complexTitleRoomEn.Text;
            p.RoomTextHe = this.complexAboutRoomHe.Text;
            p.RoomTextEn = this.complexTitleRoomEn.Text;

            this.Master._PapaDal.Update("complexPic", p, TimeNow.TheTimeNow);
            this.Master._Logger.Log(new AdminException(". " + p.PicID +
                    " Was Successfully Updated"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(17, "White", p.RoomTitleHe));

            this.removeUpdatePicSelector.Items.FindByValue("s" + p.PicID).Text = p.RoomTitleHe;
        }
        catch (Exception e)
        {
            this.Master._Logger.Log(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(18, "Red", p.RoomTitleHe));
        }
    }

    private void ShowEnableDisable(bool visible)
    {
        this.disableComplexPic.Visible = visible;
        this.enableComplexPic.Visible = visible;
    }

    private void ShowBWArea(bool visible)
    {
        this.morePic.Visible = visible;
    }

    private void ShowBWPic(bool visible)
    {
        if (this.complexBWPicUploadPic.Src == "")
        {
            this.complexBWPicUploadPic.Visible = false;
            return;
        }
        this.complexBWPicUploadPic.Visible = visible;
    }

    private void ShowColorPic(bool visible)
    {
        if (this.complexColorPicUploadPic.Src == "")
        {
            this.complexColorPicUploadPic.Visible = false;
            return;
        }
        this.complexColorPicUploadPic.Visible = visible;
    }

    private void ShowBWFile(bool visible)
    {
        this.complexBWPicUpload.Visible = visible;
    }

    private void ShowColorFile(bool visible)
    {
        this.complexColorPicUpload.Visible = visible;
    }

    private void RemovePic()
    {
        if (!this.ValidateFields(4))
        {
            this.ClearFields(3);
            return;
        }

        ComplexRoomPic p = (ComplexRoomPic)this.Master._PapaDal.Get("complexPic", this.complexHiddenRe.Value);
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

            if (File.Exists(p.PicBWFullPath))
            {
                File.Delete(p.PicBWFullPath);
            }

            if (File.Exists(p.PicColorFullPath))
            {
                File.Delete(p.PicColorFullPath);
            }

            this.Master._PapaDal.Remove("complexPic", this.complexHiddenRe.Value);
            this.Master._Logger.Log(new AdminException(". " + p.RoomTitleHe + " Was Successfully Removed"),
            MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(12, "White", p.RoomTitleHe));

            this.removeUpdatePicSelector.Items.Remove
            (this.removeUpdatePicSelector.Items.FindByValue(
            "s" + this.complexHiddenRe.Value));
        }
        catch (Exception e)
        {
            this.Master._Logger.Log(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(13, "Red", p.RoomTitleHe));
        }
    }

    private void EnablePic()
    {
        if (!this.ValidateFields(5))
        {
            this.ClearFields(3);
            return;
        }

        ComplexRoomPic p = (ComplexRoomPic)this.Master._PapaDal.Get("complexPic", this.complexHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearFields(3);
            return;
        }

        if (p.Active == 1)
        {
            this.Master._Logger.Warn(new AdminException
            (". " + this.complexHiddenUp.Value + " Is Already Disabled"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(9, "Red", p.RoomTitleHe));
            this.ClearFields(3);
            return;
        }

        if (!this.Master._PapaDal.CheckPlacesStatus(p.RoomName))
        {
            this.Master._Logger.Warn(new AdminException
            (". Max Pictures For " + p.RoomName), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(54, "Red", p.RoomName));
            this.ClearFields(3);
            return;
        }

        try
        {
            this.Master._PapaDal.Enable("complexPic", p.RoomTitleHe);
            this.Master._Logger.Log(new AdminException(". " + p.RoomTitleHe + " Has Been Successfully Enabled"),
            MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(6, "White", p.RoomTitleHe));
        }
        catch (Exception f)
        {
            this.Master._Logger.Log(f, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(10, "Red", p.RoomTitleHe));
        }
    }

    private void DisablePic()
    {
        if (!this.ValidateFields(5))
        {
            this.ClearFields(3);
            return;
        }

        ComplexRoomPic p = (ComplexRoomPic)this.Master._PapaDal.Get("complexPic", this.complexHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearFields(3);
            return;
        }

        if (p.Active == 2)
        {
            this.Master._Logger.Warn(new AdminException
            (". " + this.complexHiddenUp.Value + " Is Already Disabled"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(8, "Red", p.RoomTitleHe));
            this.ClearFields(3);
            return;
        }

        try
        {
            this.Master._PapaDal.Disable("complexPic", this.complexHiddenUp.Value);
            this.Master._Logger.Log(new AdminException(". " + p.RoomTitleHe +
                " Has Been Successfully Disabled"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(7, "White", p.RoomTitleHe));
        }
        catch (Exception f)
        {
            this.Master._Logger.Log(f, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(11, "Red", p.RoomTitleHe));
        }
    }

    public void UpdateText()
    {
        if (!this.ValidateFields(9))
        {
            return;
        }

        string hebrewTextComplex = this.Master._GlobalFunctions.ConvertToUtf8(this.aboutComplexTextHe.Text);

        ComplexAbout v = (ComplexAbout)this.Master._PapaDal.Get("complexAbout", "0");
        if (v == null)
        {
            try
            {
                ComplexAbout e = new ComplexAbout
                {
                    ComplexAboutID = this.Master._PapaDal.GetNextAvailableID("complexAbout"),
                    AboutHe = hebrewTextComplex,
                    AboutEn = this.aboutComplexTextEn.Text,
                    LastUpdate = TimeNow.TheTimeNow,
                    spLastUpdate = TimeNow.TheTimeNow.ToString()
                };

                this.Master._PapaDal.Add("complexAbout", e);
                this.Master._Logger.Log(new AdminException(". Complex Text Was Successfully Added"),
                    MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(15, "White", "Complex About Text"));
            }
            catch (Exception e)
            {
                this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(16, "Red", "Complex About Text"));
            }
        }
        else
        {
            try
            {
                v.AboutHe = hebrewTextComplex;
                v.AboutEn = this.aboutComplexTextEn.Text;
                v.LastUpdate = TimeNow.TheTimeNow;
                v.spLastUpdate = TimeNow.TheTimeNow.ToString();

                this.Master._PapaDal.Update("complexText", v, TimeNow.TheTimeNow);
                this.Master._Logger.Log(new AdminException(". Complex Text Was Successfully Updated"),
                    MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(17, "White", "Complex About Text"));
            }
            catch (Exception e)
            {
                this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(18, "Red", "Complex About Text"));
            }
        }
    }

    protected void okBut_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(6))
        {
            return;
        }

        this.AfterOk(this.complexHidden.Value);
    }

    protected void cancelBut_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    private void ClearComplexPic()
    {
        this.ClearFields(2);
        this.ClearFields(3);
        this.ClearFields(4);
    }

    public void ShowComplexPicUpdateInfo(bool visible)
    {
        this.complexPicUpdateInfo.Visible = visible;
        if (visible)
        {
            this.complexPicUpdateInfo.Attributes["class"] = "mailYes";
        }
        else
        {
            this.complexPicUpdateInfo.Attributes["class"] = "mailNo";
        }
    }

    public void ShowAboutComplexUpdateInfo(bool visible)
    {
        this.complexAboutUpdateInfo.Visible = visible;
        if (visible)
        {
            this.complexAboutUpdateInfo.Attributes["class"] = "mailYes";
        }
        else
        {
            this.complexAboutUpdateInfo.Attributes["class"] = "mailNo";
        }
    }

    private void Start()
    {
        this.ClearFields(1);
        this.ClearFields(2);
        this.ClearFields(3);
        this.ClearFields(4);

        this.ShowBWPic(false);
        this.ShowBWFile(false);
        this.ShowColorPic(false);
        this.ShowColorFile(false);
        this.ShowEnableDisable(true);

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
            this.complexHidden.Value = "";
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

        this.complexPicPlaceSelector.Items.Clear();
        for (int i = 0; i < count; i++)
        {
            this.complexPicPlaceSelector.Items.Add(new ListItem(i.ToString(), i.ToString()));
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
            this.complexHidden.Value = message[2];

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



