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

public partial class Admin_Staff : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.staffSelector.Items.Add(new ListItem("--Select Action--", "1"));
            this.staffSelector.Items.Add(new ListItem("Update Existing Staff", "2"));
            this.staffSelector.Items.Add(new ListItem("Add New Staff", "3"));
            this.staffSelector.Items.Add(new ListItem("Remove/Update New Staff", "4"));

            if (!this.Master._PapaDal.CheckExistingStaff())
            {
                string[] names = new string[] {"", "Itay", "Perri", "Napo", "Dudu" };

                for (int i = 1; i < 5; i++)
                {
                    Staff g = (Staff)this.Master._PapaDal.Get("staffPic", i.ToString());
                    if (g == null)
                    {
                        try
                        {
                            g = new Staff()
                            {
                                spLastUpdate = TimeNow.TheTimeNow.ToShortDateString(),
                                LastUpdate = TimeNow.TheTimeNow,
                                IsExistingStaff = 1,
                                TextEn = this.staffExistingTextEn.Text,
                                TextHe = "",
                                TitleHe = names[i],
                                TitleEn = "",
                                StaffID = this.Master._PapaDal.GetNextAvailableID("staffPic"),
                                PicFullPath = "",
                                PicRelativePath = "",
                                StaffPlace = i,
                                Active = 1,
                                spActive = "Enable",
                            };

                            this.Master._PapaDal.Add("staffPic", g);
                            this.staffExistingSelector.Items.Add(new ListItem(names[i], "s" + g.StaffID));
                        }
                        catch (Exception p)
                        {
                            this.Master._Logger.Error(p, MethodBase.GetCurrentMethod().Name);
                            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
                            this.ClearNewStaff();
                            return;
                        }
                    }
                }
            }
            else
            {
                foreach (Staff p in (IEnumerable<Staff>)this.Master._PapaDal.GetAll("staffPic"))
                {
                    if (p.IsExistingStaff == 1)
                    {
                        this.staffExistingSelector.Items.Add(new ListItem(p.TitleHe, "s" + p.StaffID));
                    }
                }
            }

            foreach (Staff p in (IEnumerable<Staff>)this.Master._PapaDal.GetAll("staffPic"))
            {
                if (p.IsExistingStaff == 2)
                {
                    this.removeUpdateNewStaffSelector.Items.Add(new ListItem(p.TitleHe, "s" + p.StaffID));
                }
            }

            this.DivSwitcher(1);
        }
    }

    protected void staffSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!this.ValidateFields(1))
        {
            return;
        }

        this.DivSwitcher(int.Parse(this.staffSelector.SelectedValue));
    }

    protected void updateExistingCancel_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    protected void addExistingStaffButton_Click(object sender, EventArgs e)
    {
        this.UpdateExistingStaff();
    }

    protected void cancelAddExistingStaffButton_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    protected void staffExistingSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.existingStaffHiddenUp.Value = this.staffExistingSelector.SelectedValue.Remove(0, 1);
        this.UpdateExistingStaffInit();
        this.DivSwitcher(7);
    }

    protected void addNewStaffButton_Click(object sender, EventArgs e)
    {
        if (this.newStaffHiddenUp.Value == "")
        {
            this.AddNewStaff();
        }
        else
        {
            this.UpdateNewStaff();
            this.newStaffHiddenUp.Value = "";
        }
    }

    protected void cancelAddNewStaffButton_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    protected void removeNewStaffButton_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(7))
        {
            return;
        }

        Staff p = (Staff)this.Master._PapaDal.Get("staffPic",
        this.removeUpdateNewStaffSelector.SelectedValue.Remove(0, 1));
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearNewStaff();
            return;
        }

        this.newStaffHiddenRe.Value = p.StaffID;
        this.Notify(this.Master._Notifier.Notify(5, "Red", p.TitleHe));
    }

    protected void updateNewStaffButton_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(7))
        {
            return;
        }

        Staff p = (Staff)this.Master._PapaDal.Get("staffPic",
        this.removeUpdateNewStaffSelector.SelectedValue.Remove(0, 1));
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearNewStaff();
            return;
        }

        this.newStaffHiddenUp.Value = p.StaffID;
        this.UpdateNewStaffInit();
        this.DivSwitcher(3);
    }

    protected void cancelNewStaffButton_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    protected void disableStaffButton_Click(object sender, EventArgs e)
    {
        this.DisableNewStaff();
    }

    protected void enableStaffButton_Click(object sender, EventArgs e)
    {
        this.EnableNewStaff();
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
                if (this.staffSelector.SelectedIndex == 0)
                {
                    this.Master._Logger.Error(new AdminException(". this.publishSelector.SelectedIndex == 0"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(1, "Red", ""));
                    return false;
                }
                break;
            case 2:
                if (this.staffNameHe.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                   (". this.staffPicUpload.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(38, "Red", this.staffExistingSelector.SelectedItem.Text));
                    return false;
                }
                if (this.staffExistingTextHe.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.staffPicUpload.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(40, "Red", this.staffExistingSelector.SelectedItem.Text));
                    return false;
                }
                if (this.staffNameEn.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.staffPicUpload.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(39, "Red", this.staffExistingSelector.SelectedItem.Text));
                    return false;
                }
                if (this.staffExistingTextEn.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.staffPicUpload.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(41, "Red", this.staffExistingSelector.SelectedItem.Text));
                    return false;
                }
                break;
            case 3:
                if (this.newStaffNameHe.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                   (". this.staffPicUpload.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(38, "Red", "Staff"));
                    return false;
                }
                if (this.newStaffNameEn.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.staffPicUpload.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(39, "Red", "Staff"));
                    return false;
                }
                if (this.staffTextHe.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.staffPicUpload.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(40, "Red", "Staff"));
                    return false;
                }
                if (this.staffTextEn.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.staffPicUpload.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(41, "Red", "Staff"));
                    return false;
                }

                if (this.staffPlace.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.songPlace.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(69, "Red", this.staffPlace.Text));
                    return false;
                }

                int h = -1;
                if (!int.TryParse(this.staffPlace.Text, out h))
                {
                    this.Master._Logger.Error(new AdminException
                        (". (!int.TryParse(this.songPlace.Text, out h))"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(68, "Red", ""));
                    return false;
                }

                if (h < 0)
                {
                    this.Master._Logger.Error(new AdminException
                    (". (h < 0)"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(68, "Red", ""));
                    return false;
                }
                break;
            case 4:
                if (this.staffHidden.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.publishGalleryHiddenRe.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
                    return false;
                }
                break;
            case 5:
                if (this.newStaffHiddenRe.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.publishPicUpload.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
                    return false;
                }
                break;
            case 6:
                if (this.newStaffHiddenUp.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.publishPicHiddenRe.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
                    return false;
                }
                break;
            case 7:
                if (this.removeUpdateNewStaffSelector.SelectedValue == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.removeUpdateNewStaffSelector.SelectedValue == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(57, "Red", "Staff"));
                    return false;
                }
                break;
            case 8:
                if (this.existingStaffHiddenUp.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.existingStaffHiddenUp.Value == \"\""), MethodBase.GetCurrentMethod().Name);
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
            this.Master._Logger.Error(new AdminException(". action <= 0"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            return;
        }

        switch (action)
        {
            case 1:
                foreach (ListItem l in this.staffSelector.Items)
                {
                    l.Selected = false;
                }
                break;
            case 2:
                foreach (ListItem l in this.staffExistingSelector.Items)
                {
                    l.Selected = false;
                }

                this.existingStaffStatusLabel.Text = "";
                this.existingStaffLastUpdateLabel.Text = "";
                this.staffNameHe.Text = "";
                this.staffNameEn.Text = "";
                this.staffExistingTextHe.Text = "";
                this.staffExistingTextEn.Text = "";
                break;
            case 3:
                foreach (ListItem l in this.removeUpdateNewStaffSelector.Items)
                {
                    l.Selected = false;
                }

                this.newStaffLastUpdateLabel.Text = "";
                this.newStaffStatusLabel.Text = "";
                this.newStaffNameHe.Text = "";
                this.newStaffNameEn.Text = "";
                this.staffTextHe.Text = "";
                this.staffTextEn.Text = "";
                this.staffPlace.Text = "";
                this.staffPicUploadPic.Src = "";
                break;
            case 4:
                this.newStaffHiddenRe.Value = "";
                this.newStaffHiddenUp.Value = "";
                this.existingStaffHiddenUp.Value = "";
                break;
            case 5:
                foreach (ListItem l in this.removeUpdateNewStaffSelector.Items)
                {
                    l.Selected = false;
                }
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

        this.ShowNewStaffUpdateInfo(false);
        this.ShowExistingStaffUpdateInfo(false);

        this.mainStaff.Visible = false;
        this.updateStaff.Visible = false;
        this.updateExistingStaff.Visible = false;
        this.addNewStaff.Visible = false;
        this.removeUpdateNewStaff.Visible = false;
        this.staffNotify.Visible = false;

        this.mainStaff.Attributes["class"] = "mailNo";
        this.updateStaff.Attributes["class"] = "mailNo";
        this.updateExistingStaff.Attributes["class"] = "mailNo";
        this.addNewStaff.Attributes["class"] = "mailNo";
        this.removeUpdateNewStaff.Attributes["class"] = "mailNo";
        this.staffNotify.Attributes["class"] = "mailNo";

        switch (action)
        {
            case 1:
                this.mainStaff.Visible = true;
                this.mainStaff.Attributes["class"] = "mailYes";
                break;
            case 2:
                this.updateStaff.Visible = true;
                this.updateStaff.Attributes["class"] = "mailYes";

                if (this.existingStaffHiddenUp.Value == "")
                {
                    this.ShowExistingStaffUpdateInfo(false);
                }
                else
                {
                    this.ShowExistingStaffUpdateInfo(true);
                }
                break;
            case 3:
                this.addNewStaff.Visible = true;
                this.addNewStaff.Attributes["class"] = "mailYes";

                if (this.newStaffHiddenUp.Value == "")
                {
                    this.ShowStaffFile(true);
                    this.ShowStaffPicture(false);
                    this.ShowStaffEnableDisable(false);
                    this.ShowNewStaffUpdateInfo(false);
                }
                else
                {
                    this.ShowStaffFile(true);
                    this.ShowStaffPicture(true);
                    this.ShowStaffEnableDisable(true);
                    this.ShowNewStaffUpdateInfo(true);
                }
                break;
            case 4:
                this.removeUpdateNewStaff.Visible = true;
                this.removeUpdateNewStaff.Attributes["class"] = "mailYes";
                break;
            case 5:
                break;
            case 6:
                this.staffNotify.Visible = true;
                this.staffNotify.Attributes["class"] = "mailYes";
                break;
            case 7:
                this.updateExistingStaff.Visible = true;
                this.updateExistingStaff.Attributes["class"] = "mailYes";
                break;
            default:
                break;
        }
    }

    private void UpdateExistingStaffInit()
    {
        if (!this.ValidateFields(8))
        {
            this.ClearExistingStaff();
            return;
        }

        Staff g = (Staff)this.Master._PapaDal.Get("staffPic", this.existingStaffHiddenUp.Value);
        if (g == null)
        {
            this.Master._Logger.Error(new AdminException(". (g == null)"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearExistingStaff();
            return;
        }

        try
        {
            this.existingStaffLastUpdateLabel.Text = g.spLastUpdate;
            this.existingStaffStatusLabel.Text = g.spLastUpdate;
            this.staffNameHe.Text = g.TitleHe;
            this.staffNameEn.Text = g.TitleEn;
            this.staffExistingTextHe.Text = g.TextHe;
            this.staffExistingTextEn.Text = g.TitleEn;
            this.ShowExistingStaffUpdateInfo(true);

        }
        catch (Exception e)
        {
            this.ClearExistingStaff();
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
        }
    }

    private void UpdateExistingStaff()
    {
        if (!this.ValidateFields(2))
        {
            this.ClearExistingStaff();
            return;
        }

        string nameHe = "";
        string textHe = "";

        Staff g = (Staff)this.Master._PapaDal.Get("staffPic", this.existingStaffHiddenUp.Value);

        if (g == null)
        {
            this.Master._Logger.Error(new AdminException
            (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearExistingStaff();
            return;
        }

        try
        {
            nameHe = this.Master._GlobalFunctions.ConvertToUtf8(this.staffNameHe.Text);
            textHe = this.Master._GlobalFunctions.ConvertToUtf8(this.staffExistingTextHe.Text);

            g.spLastUpdate = TimeNow.TheTimeNow.ToShortDateString();
            g.LastUpdate = TimeNow.TheTimeNow;
            g.IsExistingStaff = 1;
            g.TextEn = this.staffExistingTextEn.Text;
            g.TextHe = textHe;
            g.TitleHe = nameHe;
            g.TitleEn = this.staffNameEn.Text;
            g.StaffID = g.StaffID;
            g.PicFullPath = "";
            g.PicRelativePath = "";
            g.StaffPlace = 0;
            g.Active = 1;
            g.spActive = "Enable";

            this.Master._PapaDal.Update("staffPic", g, TimeNow.TheTimeNow);
            this.Master._Logger.Log(new AdminException(". " + g.StaffID + " Was Successfully Updated"),
                            MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(17, "White", g.TitleHe));
        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(18, "Red", g.TitleHe));
        }
    }

    private void AddNewStaff()
    {
        if (!this.ValidateFields(3))
        {
            this.ClearNewStaff();
            return;
        }

        if (this.staffPicUpload.Value == "")
        {
            this.Master._Logger.Error(new AdminException
            (". this.staffPicUpload.Value == \"\""), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(37, "Red", "Staff"));
            return;
        }

        if (!this.Master._PapaDal.CheckAvailablePlace("staffPic", int.Parse(this.staffPlace.Text)))
        {
            this.Master._Logger.Error(new AdminException
                (@". !this.Master._PapaDal.CheckAvailablePlace(""staffPic"",
                    int.Parse(this.staffPlace.Text))"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(21, "Red", this.staffPlace.Text));
            this.ClearNewStaff();
            return;
        }

        if (!this.Master._GlobalFunctions.ValidatePicEnd(this.staffPicUpload.Value))
        {
            this.Master._Logger.Error(new AdminException
            (". !this.Master._GlobalFunctions.ValidatePicEnd(this.staffPicUpload.Value)"),
            MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(14, "Red", this.staffPicUpload.Value));
            this.ClearNewStaff();
            return;
        }

        try
        {
            string ID = this.Master._PapaDal.GetNextAvailableID("staffPic");
            string fileName = this.staffPicUpload.PostedFile.FileName;
            string fileNameToSave = "staffPic_id-" + ID + "_" + fileName;
            string fullPath = this.Master._GlobalFunctions.GetFullPath() + fileNameToSave;
            string relativePath = this.Master._GlobalFunctions.GetRelativePath() + fileNameToSave;

            try
            {
                this.staffPicUpload.PostedFile.SaveAs(fullPath);
            }
            catch (Exception e)
            {
                this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(36, "Red", this.staffPicUpload.Value));
                this.ClearNewStaff();
                return;
            }

            string nameHe = this.Master._GlobalFunctions.ConvertToUtf8(this.newStaffNameHe.Text);
            string textHe = this.Master._GlobalFunctions.ConvertToUtf8(this.staffTextHe.Text);

            Staff p = new Staff
            {
                Active = 2,
                spActive = "Disable",
                IsExistingStaff = 2,
                StaffID = ID,
                PicFullPath = fullPath,
                PicRelativePath = relativePath,
                StaffPlace = int.Parse(this.staffPlace.Text),
                TitleEn = this.newStaffNameEn.Text,
                TitleHe = nameHe,
                TextEn = this.staffTextEn.Text,
                TextHe = textHe,
                LastUpdate = TimeNow.TheTimeNow,
                spLastUpdate = TimeNow.TheTimeNow.ToShortDateString(),
            };

            this.Master._PapaDal.Add("staffPic", p);
            this.Master._Logger.Log(new AdminException(". " + p.StaffID + " Was Successfully Added"),
                            MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(15, "White", nameHe));

            this.removeUpdateNewStaffSelector.Items.Add(new ListItem(nameHe, "s" + ID));
        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(16, "Red", this.newStaffNameHe.Text));
        }
    }

    private void UpdateNewStaffInit()
    {
        if (!this.ValidateFields(6))
        {
            this.ClearNewStaff();
            return;
        }

        Staff p = (Staff)this.Master._PapaDal.Get("staffPic", this.newStaffHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearNewStaff();
            return;
        }

        try
        {
            foreach (ListItem l in this.removeUpdateNewStaffSelector.Items)
            {
                l.Selected = false;
            }

            this.staffPlace.Text = p.StaffPlace.ToString();
            this.newStaffLastUpdateLabel.Text = p.spLastUpdate;
            this.newStaffStatusLabel.Text = p.spActive;
            this.newStaffNameHe.Text = p.TitleHe;
            this.newStaffNameEn.Text = p.TitleEn;
            this.staffTextHe.Text = p.TextEn;
            this.staffTextEn.Text = p.TextEn;
            this.staffPicUploadPic.Src = p.PicRelativePath;
            this.ShowStaffPicture(true);
            this.ShowStaffFile(true);
            this.ShowStaffEnableDisable(true);
            this.ShowNewStaffUpdateInfo(true);
        }
        catch (Exception e)
        {
            this.ClearNewStaff();
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
        }
    }

    private void UpdateNewStaff()
    {
        if (!this.ValidateFields(6))
        {
            this.ClearNewStaff();
            return;
        }

        if (!this.ValidateFields(3))
        {
            this.ClearNewStaff();
            return;
        }

        Staff p = (Staff)this.Master._PapaDal.Get("staffPic", this.newStaffHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearNewStaff();
            return;
        }

        if (!this.Master._PapaDal.CheckAvailablePlaceExcept("staffPic",
            p.StaffPlace, int.Parse(this.staffPlace.Text)))
        {
            this.Master._Logger.Error(new AdminException
                (@". !this.Master._PapaDal.CheckAvailablePlaceExcept(""staffPic"",
                p.StaffPlace, int.Parse(this.staffPlace.Text)))"),
            MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(21, "Red", this.staffPlace.Text));
            this.ClearNewStaff();
            return;
        }

        if (this.staffPicUpload.Value != "")
        {
            if (!this.Master._GlobalFunctions.ValidatePicEnd(this.staffPicUpload.Value))
            {
                this.Master._Logger.Error(new AdminException
                (@". !this.Master._GlobalFunctions.ValidatePicEnd(this.staffPicUpload.Value)"),
                MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(14, "Red", this.staffPicUpload.Value));
                this.ClearNewStaff();
                return;
            }

            string fileName = this.staffPicUpload.PostedFile.FileName;
            string fileNameToSave = "staffPic_id-" + p.StaffID + "_" + fileName;
            string fullPath = this.Master._GlobalFunctions.GetFullPath() + fileNameToSave;
            string relativePath = this.Master._GlobalFunctions.GetRelativePath() + fileNameToSave;

            try
            {
                if (File.Exists(p.PicFullPath))
                {
                    File.Delete(p.PicFullPath);
                }
                this.staffPicUpload.PostedFile.SaveAs(fullPath);

                p.PicFullPath = fullPath;
                p.PicRelativePath = relativePath;
            }
            catch (Exception f)
            {
                this.Master._Logger.Error(f, MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(36, "Red", this.staffPicUpload.Value));
                this.ClearNewStaff();
                return;
            }
        }

        try
        {
            string nameHe = this.Master._GlobalFunctions.ConvertToUtf8(this.newStaffNameHe.Text);
            string textHe = this.Master._GlobalFunctions.ConvertToUtf8(this.staffTextHe.Text);

            p.StaffID = p.StaffID;
            p.spLastUpdate = TimeNow.TheTimeNow.ToShortDateString();
            p.LastUpdate = TimeNow.TheTimeNow;
            p.TextEn = this.staffTextEn.Text;
            p.TextHe = textHe;
            p.TitleEn = this.newStaffNameEn.Text;
            p.TitleHe = nameHe;

            this.Master._PapaDal.Update("staffPic", p, TimeNow.TheTimeNow);
            this.Master._Logger.Log(new AdminException(". " + p.StaffID +
                    " Was Successfully Updated"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(17, "White", p.TitleHe));

            this.removeUpdateNewStaffSelector.Items.FindByValue
            ("s" + p.StaffID).Text = p.TextHe;
        }
        catch (Exception e)
        {
            this.Master._Logger.Log(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(18, "Red", this.newStaffNameHe.Text));
        }
    }

    private void ShowStaffEnableDisable(bool visible)
    {
        this.disableStaffButton.Visible = visible;
        this.enableStaffButton.Visible = visible;
    }

    private void ShowStaffFile(bool visible)
    {
        this.staffPicUpload.Visible = visible;
        if (visible)
        {
            this.staffPicUpload.Attributes["class"] = "mailYes";
        }
        else
        {
            this.staffPicUpload.Attributes["class"] = "mailNo";
        }
    }

    private void ShowStaffPicture(bool visible)
    {
        this.staffPicUploadPic.Visible = visible;
        if (visible)
        {
            this.staffPicUploadPic.Attributes["class"] = "mailYes";
        }
        else
        {
            this.staffPicUploadPic.Attributes["class"] = "mailNo";
        }
    }

    private void RemoveNewStaff()
    {
        if (!this.ValidateFields(5))
        {
            this.ClearNewStaff();
            return;
        }

        Staff p = (Staff)this.Master._PapaDal.Get("staffPic", this.newStaffHiddenRe.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearNewStaff();
            return;
        }

        try
        {
            if (File.Exists(p.PicFullPath))
            {
                File.Delete(p.PicFullPath);
            }

            this.Master._PapaDal.Remove("staffPic", p.StaffID);
            this.Master._Logger.Log(new AdminException(". " + p.StaffID + " Was Successfully Removed"),
                MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(12, "White", p.TitleHe));

            this.removeUpdateNewStaffSelector.Items.Remove
            (this.removeUpdateNewStaffSelector.Items.FindByValue
            ("s" + this.newStaffHiddenRe.Value));
        }
        catch (Exception e)
        {
            this.Master._Logger.Log(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(13, "Red", p.TitleHe));
        }
    }

    private void EnableNewStaff()
    {
        if (!this.ValidateFields(6))
        {
            this.ClearNewStaff();
            return;
        }

        Staff p = (Staff)this.Master._PapaDal.Get("staffPic", this.newStaffHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearNewStaff();
            return;
        }

        if (p.Active == 1)
        {
            this.Master._Logger.Warn(new AdminException
            (". " + p.TextHe + " Is Already Disabled"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(9, "Red", p.TitleHe));
            this.ClearFields(3);
            return;
        }

        try
        {
            this.Master._PapaDal.Enable("staffPic", p.StaffID);
            this.Master._Logger.Log(new AdminException(". " + p.StaffID +
                " Has Been Successfully Enabled"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(6, "White", p.TitleHe));
        }
        catch (Exception f)
        {
            this.Master._Logger.Log(f, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(10, "Red", p.TitleHe));
        }
    }

    private void DisableNewStaff()
    {
        if (!this.ValidateFields(6))
        {
            this.ClearNewStaff();
            return;
        }

        Staff p = (Staff)this.Master._PapaDal.Get("staffPic", this.newStaffHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearNewStaff();
            return;
        }

        if (p.Active == 2)
        {
            this.Master._Logger.Warn(new AdminException
            (". " + p.TextHe + " Is Already Enabled"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(8, "Red", p.TitleHe));
            this.ClearFields(3);
            return;
        }

        try
        {
            this.Master._PapaDal.Disable("staffPic", p.StaffID);
            this.Master._Logger.Log(new AdminException(". " + p.StaffID +
                " Has Been Successfully Disabled"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(7, "White", p.TitleHe));
        }
        catch (Exception f)
        {
            this.Master._Logger.Log(f, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(11, "Red", p.TitleHe));
        }
    }

    protected void okBut_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(4))
        {
            return;
        }

        this.AfterOk(this.staffHidden.Value);
    }

    protected void cancelBut_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    private void ClearExistingStaff()
    {
        this.ClearFields(2);
        this.ClearFields(3);
        this.ClearFields(4);
    }

    private void ClearNewStaff()
    {
        this.ClearFields(4);
        this.ClearFields(5);
        this.ClearFields(6);
    }

    public void ShowExistingStaffUpdateInfo(bool visible)
    {
        this.existingStaffUpdateInfo.Visible = visible;
        if (visible)
        {
            this.existingStaffUpdateInfo.Attributes["class"] = "mailYes";
        }
        else
        {
            this.existingStaffUpdateInfo.Attributes["class"] = "mailNo";
        }
    }

    public void ShowNewStaffUpdateInfo(bool visible)
    {
        this.newStaffUpdateInfo.Visible = visible;
        if (visible)
        {
            this.newStaffUpdateInfo.Attributes["class"] = "mailYes";
        }
        else
        {
            this.newStaffUpdateInfo.Attributes["class"] = "mailNo";
        }
    }

    private void Start()
    {
        this.ClearFields(1);
        this.ClearFields(2);
        this.ClearFields(3);
        this.ClearFields(4);
        this.ClearFields(5);

        this.ShowStaffPicture(false);
        this.ShowStaffFile(false);
        this.ShowStaffEnableDisable(false);

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
                this.RemoveNewStaff();
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
            this.staffHidden.Value = "";
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
            this.staffHidden.Value = message[2];

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
            this.DivSwitcher(6);
        }
    }
}
