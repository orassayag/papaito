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

public partial class Admin_Production : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.productionSelector.Items.Add(new ListItem("--Select Action--", "1"));
            this.productionSelector.Items.Add(new ListItem("Add New Production", "2"));
            this.productionSelector.Items.Add(new ListItem("Remove/Update Production", "3"));
            this.productionSelector.Items.Add(new ListItem("Add New Song", "4"));
            this.productionSelector.Items.Add(new ListItem("Remove/Update Song", "5"));

            foreach (Production p in (IEnumerable<Production>)this.Master._PapaDal.GetAll("production"))
            {
                ListItem l = new ListItem(p.ArtistNameHe, "s" + p.ProID);
                this.removeUpdateProductionSelector.Items.Add(l);
                this.addSongToProductionSelector.Items.Add(l);
            }

            foreach (Song p in (IEnumerable<Song>)this.Master._PapaDal.GetAll("song"))
            {
                this.removeUpdateSongSelector.Items.Add(new ListItem(p.SongNameHe, "s" + p.SongID));
            }

            this.DivSwitcher(1);
        }
    }

    protected void productionSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!this.ValidateFields(1))
        {
            return;
        }

        this.DivSwitcher(int.Parse(this.productionSelector.SelectedValue));
    }

    protected void addProductionButton_Click(object sender, EventArgs e)
    {
        if (this.productionHiddenUp.Value == "")
        {
            this.AddProduction();
        }
        else
        {
            this.UpdateProduction();
            this.productionHiddenUp.Value = "";
        }
    }

    protected void cancelProductionButton_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    protected void disableSongButton_Click(object sender, EventArgs e)
    {
        this.DisableSong();
    }

    protected void enableSongButton_Click(object sender, EventArgs e)
    {
        this.EnableSong();
    }

    protected void removeProductionButton_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(10))
        {
            return;
        }

        Production p = (Production)this.Master._PapaDal.Get("production",
        this.removeUpdateProductionSelector.SelectedValue.Remove(0, 1));
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearProduction();
            return;
        }

        this.productionHiddenRe.Value = p.ProID;
        this.Notify(this.Master._Notifier.Notify(5, "Red", p.ArtistNameHe));
    }

    protected void updateProductionButton_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(10))
        {
            return;
        }

        Production p = (Production)this.Master._PapaDal.Get("production",
        this.removeUpdateProductionSelector.SelectedValue.Remove(0, 1));
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearProduction();
            return;
        }

        this.productionHiddenUp.Value = p.ProID;
        this.UpdateProductionInit();
        this.DivSwitcher(2);
    }

    protected void addSongButton_Click(object sender, EventArgs e)
    {
        if (this.songHiddenUp.Value == "")
        {
            this.AddSong();
        }
        else
        {
            this.UpdateSong();
            this.songHiddenUp.Value = "";
        }
    }

    protected void diableProductionButton_Click(object sender, EventArgs e)
    {
        this.DisableProduction();
    }

    protected void cancelAddProductionButton_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    protected void enableProductionButton_Click(object sender, EventArgs e)
    {
        this.EnableProduction();
    }

    protected void cancelAddSongButton_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    protected void updateSongButton_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(11))
        {
            return;
        }

        Song p = (Song)this.Master._PapaDal.Get("song",
        this.removeUpdateSongSelector.SelectedValue.Remove(0, 1));
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearProduction();
            return;
        }

        this.songHiddenUp.Value = p.SongID;
        this.UpdateSongInit();
        this.DivSwitcher(4);
    }

    protected void removeSongButton_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(11))
        {
            return;
        }

        Song p = (Song)this.Master._PapaDal.Get("song",
        this.removeUpdateSongSelector.SelectedValue.Remove(0, 1));
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearProduction();
            return;
        }

        this.songHiddenRe.Value = p.SongID;
        this.Notify(this.Master._Notifier.Notify(5, "Red", p.SongNameHe));
    }

    protected void cancelSongButton_Click(object sender, EventArgs e)
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
                if (this.productionSelector.SelectedIndex == 0)
                {
                    this.Master._Logger.Error(new AdminException(". this.productionSelector.SelectedIndex == 0"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(1, "Red", ""));
                    return false;
                }
                break;
            case 2:
                if (this.productionHidden.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.productionHidden.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
                    return false;
                }
                break;
            case 3:
                if (this.productionArtistsNameHe.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.productionArtistsNameHe.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(26, "Red", ""));
                    return false;
                }

                if (this.productionArtistsNameEn.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.productionArtistsNameEn.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(27, "Red", ""));
                    return false;
                }
                if (this.productionAboutArtistsHe.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.productionAboutArtistsHe.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(28, "Red", ""));
                    return false;
                }

                if (this.productionAboutArtistsEn.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.productionAboutArtistsEn.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(29, "Red", ""));
                    return false;
                }

                if (this.productionPlace.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.productionPlace.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(59, "Red", ""));
                    return false;
                }

                int i = -1;
                if (!int.TryParse(this.productionPlace.Text, out i))
                {
                    this.Master._Logger.Error(new AdminException
                    (". (!int.TryParse(this.productionPlace.Text,out i))"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(60, "Red", ""));
                    return false;
                }

                if (i < 0)
                {
                    this.Master._Logger.Error(new AdminException
                    (". (i < 0)"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(60, "Red", ""));
                    return false;
                }
                break;
            case 4:
                if (this.songNameHe.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                       (". this.songNameHe.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(31, "Red", ""));
                    return false;
                }

                if (this.songNameHe.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.songNameHe.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(32, "Red", ""));
                    return false;
                }

                if (this.songYoutubeAddress.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.songYoutubeAddress.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(34, "Red", ""));
                    return false;
                }

                string youtube = this.Master._GlobalFunctions.ConfigYoutube(this.songYoutubeAddress.Text);
                if (youtube == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". youtube == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(33, "Red", ""));
                    return false;
                }

                if (this.songPlace.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.songPlace.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(30, "Red", this.productionBWPicUpload.Value));
                    return false;
                }

                int h = -1;
                if (!int.TryParse(this.songPlace.Text, out h))
                {
                    this.Master._Logger.Error(new AdminException
                        (". (!int.TryParse(this.songPlace.Text, out h))"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(35, "Red", ""));
                    return false;
                }

                if (h < 0)
                {
                    this.Master._Logger.Error(new AdminException
                    (". (h < 0)"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(35, "Red", ""));
                    return false;
                }
                break;
            case 5:
                if (this.productionHiddenRe.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.productionHiddenRe.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
                    return false;
                }
                break;
            case 6:
                if (this.productionHiddenUp.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.productionHiddenUp.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
                    return false;
                }
                break;
            case 7:
                if (this.productionArtistsNameHe.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.productionArtistsNameHe.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(26, "Red", ""));
                    return false;
                }

                if (this.productionArtistsNameEn.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.productionArtistsNameEn.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(27, "Red", ""));
                    return false;
                }
                if (this.productionAboutArtistsHe.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.productionAboutArtistsHe.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(28, "Red", ""));
                    return false;
                }

                if (this.productionAboutArtistsEn.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.productionAboutArtistsEn.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(29, "Red", ""));
                    return false;
                }
                break;
            case 8:
                if (this.songHiddenRe.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.songHiddenRe.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
                    return false;
                }
                break;
            case 9:
                if (this.songHiddenUp.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.songHiddenUp.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
                    return false;
                }
                break;
            case 10:
                if (this.removeUpdateProductionSelector.SelectedValue == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.removeUpdatePicSelector.SelectedValue == null"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(57, "Red", "Production"));
                    return false;
                }
                break;
            case 11:
                if (this.removeUpdateSongSelector.SelectedValue == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.removeUpdateSongSelector.SelectedValue == null"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(57, "Red", "Song"));
                    return false;
                }
                break;
            case 12:
                if (this.productionMainPicUpload.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.productionMainPicUpload.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(37, "Red", "Main Picture"));
                    return false;
                }

                if (this.productionColorPicUpload.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.productionColorPicUpload.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(37, "Red", "Color Picture"));
                    return false;
                }

                if (this.productionBWPicUpload.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.productionBWPicUpload.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(37, "Red", "Black And White Picture"));
                    return false;
                }
                break;
            case 13:
                if (this.addSongToProductionSelector.SelectedValue == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.addSongToProductionSelector.SelectedValue == null"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(62, "Red", ""));
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
                foreach (ListItem l in this.productionSelector.Items)
                {
                    l.Selected = false;
                }
                break;
            case 2:
                this.productuionStatusLabel.Text = "";
                this.productionLastUpdateLabel.Text = "";
                this.productionArtistsNameHe.Text = "";
                this.productionArtistsNameEn.Text = "";
                this.productionAboutArtistsHe.Text = "";
                this.productionAboutArtistsEn.Text = "";
                this.productionPlace.Text = "";
                this.productionMainPicUploadPic.Src = "";
                this.productionColorPicUploadPic.Src = "";
                this.productionBWPicUploadPic.Src = "";
                break;
            case 3:
                foreach (ListItem l in this.removeUpdateProductionSelector.Items)
                {
                    l.Selected = false;
                }
                break;
            case 4:
                this.songHiddenRe.Value = "";
                this.songHiddenUp.Value = "";
                this.productionHiddenRe.Value = "";
                this.productionHiddenUp.Value = "";
                break;
            case 5:
                foreach (ListItem l in this.addSongToProductionSelector.Items)
                {
                    l.Selected = false;
                }
                this.songLastUpdateLabel.Text = "";
                this.songStatusLabel.Text = "";
                this.songNameHe.Text = "";
                this.songNameEn.Text = "";
                this.songYoutubeAddress.Text = "";
                this.songPlace.Text = "";
                break;
            case 6:
                foreach (ListItem l in this.removeUpdateSongSelector.Items)
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

        this.ShowSongUpdateInfo(false);
        this.ShowProductionUpdateInfo(false);

        this.mainProduction.Visible = false;
        this.addProduction.Visible = false;
        this.removeUpdateProduction.Visible = false;
        this.addSong.Visible = false;
        this.removeUpdateSong.Visible = false;
        this.productionNotify.Visible = false;

        this.mainProduction.Attributes["class"] = "mailNo";
        this.addProduction.Attributes["class"] = "mailNo";
        this.removeUpdateProduction.Attributes["class"] = "mailNo";
        this.addSong.Attributes["class"] = "mailNo";
        this.removeUpdateSong.Attributes["class"] = "mailNo";
        this.productionNotify.Attributes["class"] = "mailNo";

        switch (action)
        {
            case 1:
                this.mainProduction.Visible = true;
                this.mainProduction.Attributes["class"] = "mailYes";
                break;
            case 2:
                this.addProduction.Visible = true;
                this.addProduction.Attributes["class"] = "mailYes";

                if (this.productionHiddenUp.Value == "")
                {
                    this.ShowProductionFile(true);
                    this.ShowProductionPicture(false);
                    this.ShowProductionEnableDisable(false);
                    this.ShowProductionUpdateInfo(false);
                }
                else
                {
                    this.ShowProductionFile(true);
                    this.ShowProductionPicture(true);
                    this.ShowProductionEnableDisable(true);
                    this.ShowProductionUpdateInfo(true);
                }
                break;
            case 3:
                this.removeUpdateProduction.Visible = true;
                this.removeUpdateProduction.Attributes["class"] = "mailYes";
                break;
            case 4:
                this.addSong.Visible = true;
                this.addSong.Attributes["class"] = "mailYes";

                if (this.songHiddenUp.Value == "")
                {
                    this.ShowSongEnableDisable(false);
                    this.ShowSongUpdateInfo(false);
                }
                else
                {
                    this.ShowSongEnableDisable(true);
                    this.ShowSongUpdateInfo(true);
                }
                break;
            case 5:
                this.removeUpdateSong.Visible = true;
                this.removeUpdateSong.Attributes["class"] = "mailYes";
                break;
            case 6:
                this.productionNotify.Visible = true;
                this.productionNotify.Attributes["class"] = "mailYes";
                break;
            default:
                break;
        }
    }

    private void AddProduction()
    {
        if (!this.ValidateFields(3))
        {
            this.ClearProduction();
            return;
        }

        if (!this.ValidateFields(12))
        {
            this.ClearProduction();
            return;
        }

        if (!this.Master._PapaDal.CheckAvailablePlace("production", int.Parse(this.productionPlace.Text)))
        {
            this.Master._Logger.Error(new AdminException
                (@". !this.Master._PapaDal.CheckAvailablePlace(""production"", 
                    int.Parse(this.productionPlace.Text))"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(21, "Red", this.productionPlace.Text));
            this.ClearProduction();
            return;
        }

        if (!this.Master._GlobalFunctions.ValidatePicEnd(this.productionMainPicUpload.Value))
        {
            this.Master._Logger.Error(new AdminException
            (". !this.Master._GlobalFunctions.ValidatePicEnd(this.productionMainPicUpload.Value)"),
                MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(14, "Red", this.productionMainPicUpload.Value));
            this.ClearProduction();
            return;
        }

        if (!this.Master._GlobalFunctions.ValidatePicEnd(this.productionColorPicUpload.Value))
        {
            this.Master._Logger.Error(new AdminException
            (". !this.Master._GlobalFunctions.ValidatePicEnd(this.productionColorPicUpload.Value)"),
                MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(14, "Red", this.productionColorPicUpload.Value));
            this.ClearProduction();
            return;
        }

        if (!this.Master._GlobalFunctions.ValidatePicEnd(this.productionBWPicUpload.Value))
        {
            this.Master._Logger.Error(new AdminException
            (". !this.Master._GlobalFunctions.ValidatePicEnd(this.productionBWPicUpload.Value)"),
                MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(14, "Red", this.productionBWPicUpload.Value));
            this.ClearProduction();
            return;
        }

        string ID = this.Master._PapaDal.GetNextAvailableID("production");
        string fileMainName = this.productionMainPicUpload.PostedFile.FileName;
        string fileMainNameToSave = "productionMainPic_id-" + ID + "_" + fileMainName;
        string fullMainPath = this.Master._GlobalFunctions.GetFullPath() + fileMainNameToSave;
        string relativeMainPath = this.Master._GlobalFunctions.GetRelativePath() + fileMainNameToSave;
        string fileColorName = this.productionColorPicUpload.PostedFile.FileName;
        string fileColorNameToSave = "productionColorPic_id-" + ID + "_" + fileColorName;
        string fullColorPath = this.Master._GlobalFunctions.GetFullPath() + fileColorNameToSave;
        string relativeColorPath = this.Master._GlobalFunctions.GetRelativePath() + fileColorNameToSave;
        string fileBWName = this.productionBWPicUpload.PostedFile.FileName;
        string fileBWNameToSave = "productionBWPic_id-" + ID + "_" + fileBWName;
        string fullBWPath = this.Master._GlobalFunctions.GetFullPath() + fileBWNameToSave;
        string relativeBWPath = this.Master._GlobalFunctions.GetRelativePath() + fileBWNameToSave;
        string textArtistsHe = "";
        string textAboutHe = "";

        try
        {
            this.productionMainPicUpload.PostedFile.SaveAs(fullMainPath);
        }
        catch (Exception r)
        {
            this.Master._Logger.Error(r, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(36, "Red", this.productionMainPicUpload.Value));
            this.ClearProduction();
            return;
        }

        try
        {
            this.productionColorPicUpload.PostedFile.SaveAs(fullColorPath);
        }
        catch (Exception r)
        {
            this.Master._Logger.Error(r, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(36, "Red", this.productionColorPicUpload.Value));
            this.ClearProduction();
            return;
        }

        try
        {
            this.productionBWPicUpload.PostedFile.SaveAs(fullBWPath);
        }
        catch (Exception r)
        {
            this.Master._Logger.Error(r, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(36, "Red", this.productionBWPicUpload.Value));
            this.ClearProduction();
            return;
        }

        try
        {
            textArtistsHe = this.Master._GlobalFunctions.ConvertToUtf8(this.productionArtistsNameHe.Text);
            textAboutHe = this.Master._GlobalFunctions.ConvertToUtf8(this.productionAboutArtistsHe.Text);

            Production g = new Production
            {
                ProID = ID,
                Active = 2,
                ArtistNameEn = this.productionArtistsNameEn.Text,
                ArtistNameHe = textArtistsHe,
                ArtistTextEn = this.productionAboutArtistsEn.Text,
                ArtistTextHe = textAboutHe,
                PicBWFullPath = fullBWPath,
                PicBWRelativePath = relativeBWPath,
                PicColorFullPath = fullColorPath,
                PicColorRelativePath = relativeColorPath,
                PicMainFullPath = fullMainPath,
                PicMainRelativePath = relativeMainPath,
                ProPlace = int.Parse(this.productionPlace.Text),
                spActive = "Disable",
                spUploadTime = TimeNow.TheTimeNow.ToShortDateString(),
                UploadTime = TimeNow.TheTimeNow,
                LastUpdate = TimeNow.TheTimeNow,
                spLastUpdate = TimeNow.TheTimeNow.ToShortDateString()
            };

            this.Master._PapaDal.Add("production", g);
            this.Master._Logger.Log(new AdminException(". " + g.ProID + " Was Successfully Added"),
                            MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(15, "White", g.ArtistTextHe));

            ListItem l = new ListItem(g.ArtistTextHe, "s" + g.ProID);

            this.removeUpdateProductionSelector.Items.Add(l);
            this.addSongToProductionSelector.Items.Add(l);
        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(16, "Red", textArtistsHe));
        }
    }

    private void UpdateProductionInit()
    {
        if (!this.ValidateFields(6))
        {
            this.ClearProduction();
            return;
        }

        Production p = (Production)this.Master._PapaDal.Get("production", this.productionHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearProduction();
            return;
        }

        try
        {
            this.productionPlace.Text = p.ProPlace.ToString();
            this.productionLastUpdateLabel.Text = p.spLastUpdate;
            this.productuionStatusLabel.Text = p.spActive;
            this.productionArtistsNameHe.Text = p.ArtistNameHe;
            this.productionArtistsNameEn.Text = p.ArtistNameEn;
            this.productionAboutArtistsHe.Text = p.ArtistTextHe;
            this.productionAboutArtistsEn.Text = p.ArtistTextEn;
            this.productionMainPicUploadPic.Src = p.PicMainRelativePath;
            this.productionColorPicUploadPic.Src = p.PicColorRelativePath;
            this.productionBWPicUploadPic.Src = p.PicBWRelativePath;
            this.ShowProductionFile(true);
            this.ShowProductionPicture(true);
            this.ShowSongUpdateInfo(true);
        }
        catch (Exception e)
        {
            this.ClearProduction();
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
        }
    }

    private void UpdateProduction()
    {
        if (!this.ValidateFields(7))
        {
            this.ClearProduction();
            return;
        }

        if (!this.ValidateFields(3))
        {
            this.ClearProduction();
            return;
        }

        Production g = (Production)this.Master._PapaDal.Get("production", this.productionHiddenUp.Value);
        if (g == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". g == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearProduction();
            return;
        }

        if (!this.Master._PapaDal.CheckAvailablePlaceExcept
           ("production", g.ProPlace, int.Parse(this.productionPlace.Text)))
        {
            this.Master._Logger.Error(new AdminException
                (@". (!this.Master._PapaDal.CheckAvailablePlaceExcept
                (""production"", g.ProPlace, int.Parse(this.productionPlace.Text)))"),
                    MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(21, "Red", this.productionPlace.Text));
            this.ClearProduction();
            return;
        }

        if (this.productionMainPicUpload.Value != "")
        {
            if (!this.Master._GlobalFunctions.ValidatePicEnd(this.productionMainPicUpload.Value))
            {
                this.Master._Logger.Error(new AdminException
                (". !this.Master._GlobalFunctions.ValidatePicEnd(this.productionMainPicUpload.Value)"),
                    MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(14, "Red", this.productionMainPicUpload.Value));
                return;
            }

            string fileMainName = this.productionMainPicUpload.PostedFile.FileName;
            string fileMainNameToSave = "productionMainPic_id-" + g.ProID + "_" + fileMainName;
            string fullMainPath = this.Master._GlobalFunctions.GetFullPath() + fileMainNameToSave;
            string relativeMainPath = this.Master._GlobalFunctions.GetRelativePath() + fileMainNameToSave;

            try
            {
                if (File.Exists(g.PicMainFullPath))
                {
                    File.Delete(g.PicMainFullPath);
                }
                this.productionMainPicUpload.PostedFile.SaveAs(fullMainPath);

                g.PicMainFullPath = fullMainPath;
                g.PicMainRelativePath = relativeMainPath;
            }
            catch (Exception r)
            {
                this.Master._Logger.Error(r, MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(36, "Red", this.productionMainPicUpload.Value));
                this.ClearProduction();
                return;
            }
        }

        if (this.productionColorPicUpload.Value != "")
        {
            if (!this.Master._GlobalFunctions.ValidatePicEnd(this.productionColorPicUpload.Value))
            {
                this.Master._Logger.Error(new AdminException
                (". !this.Master._GlobalFunctions.ValidatePicEnd(this.productionColorPicUpload.Value)"),
                    MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(14, "Red", this.productionColorPicUpload.Value));
                this.ClearProduction();
                return;
            }

            string fileColorName = this.productionColorPicUpload.PostedFile.FileName;
            string fileColorNameToSave = "productionColorPic_id-" + g.ProID + "_" + fileColorName;
            string fullColorPath = this.Master._GlobalFunctions.GetFullPath() + fileColorNameToSave;
            string relativeColorPath = this.Master._GlobalFunctions.GetRelativePath() + fileColorNameToSave;

            try
            {
                if (File.Exists(g.PicColorFullPath))
                {
                    File.Delete(g.PicColorFullPath);
                }
                this.productionColorPicUpload.PostedFile.SaveAs(fullColorPath);

                g.PicColorFullPath = fullColorPath;
                g.PicColorRelativePath = relativeColorPath;
            }
            catch (Exception r)
            {
                this.Master._Logger.Error(r, MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(36, "Red", this.productionColorPicUpload.Value));
                this.ClearProduction();
                return;
            }
        }

        if (this.productionBWPicUpload.Value != "")
        {
            if (!this.Master._GlobalFunctions.ValidatePicEnd(this.productionBWPicUpload.Value))
            {
                this.Master._Logger.Error(new AdminException
                (". !this.Master._GlobalFunctions.ValidatePicEnd(this.productionBWPicUpload.Value)"),
                    MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(14, "Red", this.productionBWPicUpload.Value));
                return;
            }

            string fileBWName = this.productionBWPicUpload.PostedFile.FileName;
            string fileBWNameToSave = "productionBWPic_id-" + g.ProID + "_" + fileBWName;
            string fullBWPath = this.Master._GlobalFunctions.GetFullPath() + fileBWNameToSave;
            string relativeBWPath = this.Master._GlobalFunctions.GetRelativePath() + fileBWNameToSave;

            try
            {
                if (File.Exists(g.PicBWFullPath))
                {
                    File.Delete(g.PicBWFullPath);
                }
                this.productionBWPicUpload.PostedFile.SaveAs(fullBWPath);

                g.PicBWFullPath = fullBWPath;
                g.PicBWRelativePath = relativeBWPath;
            }
            catch (Exception r)
            {
                this.Master._Logger.Error(r, MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(36, "Red", this.productionBWPicUpload.Value));
                this.ClearProduction();
                return;
            }
        }

        string textArtistsHe = "";
        string textAboutHe = "";

        try
        {
            textArtistsHe = this.Master._GlobalFunctions.ConvertToUtf8(this.productionArtistsNameHe.Text);
            textAboutHe = this.Master._GlobalFunctions.ConvertToUtf8(this.productionAboutArtistsHe.Text);

            g.ArtistNameEn = this.productionArtistsNameEn.Text;
            g.ArtistNameHe = textArtistsHe;
            g.ArtistTextEn = this.productionAboutArtistsEn.Text;
            g.ArtistTextHe = textAboutHe;
            g.LastUpdate = TimeNow.TheTimeNow;
            g.spLastUpdate = TimeNow.TheTimeNow.ToShortDateString();

            this.Master._PapaDal.Update("production", g, TimeNow.TheTimeNow);
            this.Master._Logger.Log(new AdminException(". " + g.ArtistTextHe +
                    " Was Successfully Updated"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(17, "White", g.ArtistTextHe));

            this.removeUpdateProductionSelector.Items.FindByValue
            ("s" + g.ProID).Text = g.ArtistTextHe;
            this.addSongToProductionSelector.Items.FindByValue
            ("s" + g.ProID).Text = g.ArtistTextHe;
        }
        catch (Exception e)
        {
            this.Master._Logger.Log(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(18, "Red", textArtistsHe));
        }
    }

    private void RemoveProduction()
    {
        if (!this.ValidateFields(5))
        {
            this.ClearProduction();
            return;
        }

        Production g = (Production)this.Master._PapaDal.Get("production", this.productionHiddenRe.Value);
        if (g == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". g == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearProduction();
            return;
        }

        try
        {
            if (File.Exists(g.PicMainFullPath))
            {
                File.Delete(g.PicMainFullPath);
            }

            if (File.Exists(g.PicColorFullPath))
            {
                File.Delete(g.PicColorFullPath);
            }

            if (File.Exists(g.PicBWFullPath))
            {
                File.Delete(g.PicBWFullPath);
            }

            this.DeleteSongs(g.ProID);

            this.Master._PapaDal.Remove("production", g.ProID);
            this.Master._Logger.Log(new AdminException(". " + g.ProID + " Was Successfully Removed"),
                MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(12, "White", g.ArtistNameHe));

            this.removeUpdateProductionSelector.Items.Remove
            (this.removeUpdateProductionSelector.Items.FindByValue
            ("s" + this.productionHiddenRe.Value));
            this.addSongToProductionSelector.Items.Remove
            (this.addSongToProductionSelector.Items.FindByValue
            ("s" + this.productionHiddenRe.Value));

        }
        catch (Exception e)
        {
            this.Master._Logger.Log(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(13, "Red", g.ArtistNameHe));
        }
    }

    private void AddSong()
    {
        if (!this.ValidateFields(4))
        {
            this.ClearSong();
            return;
        }

        if (!this.ValidateFields(13))
        {
            this.ClearSong();
            return;
        }

        if (!this.Master._PapaDal.CheckAvailablePlace("song",
                    int.Parse(this.songPlace.Text)))
        {
            this.Master._Logger.Error(new AdminException
                (@". !this.Master._PapaDal.CheckAvailablePlace(""song"", 
                    int.Parse(this.songPlace.Text))"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(21, "Red", this.songPlace.Text));
            this.ClearSong();
            return;
        }


        try
        {
            string ID = this.Master._PapaDal.GetNextAvailableID("song");

            string textHe = this.Master._GlobalFunctions.ConvertToUtf8(this.songNameHe.Text);

            Song p = new Song
            {
                Active = 2,
                SongID = ID,
                SongNameEn = this.songNameEn.Text,
                SongNameHe = textHe,
                SongPlace = int.Parse(this.songPlace.Text),
                YouTubePath = this.songYoutubeAddress.Text,
                LastUpdate = TimeNow.TheTimeNow,
                spLastUpdate = TimeNow.TheTimeNow.ToShortDateString(),
                UploadTime = TimeNow.TheTimeNow,
                spUploadTime = TimeNow.TheTimeNow.ToString(),
                spActive = "Disable",
                ProID = this.addSongToProductionSelector.SelectedValue.Remove(0, 1)
            };

            this.Master._PapaDal.Add("song", p);
            this.Master._Logger.Log(new AdminException(". " + p.SongID + " Was Successfully Added"),
                            MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(15, "White", textHe));

            this.removeUpdateSongSelector.Items.Add(new ListItem(textHe, "s" + p.SongID));
        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(16, "Red", this.songNameHe.Text));
        }
    }

    private void UpdateSongInit()
    {
        this.ShowSongEnableDisable(true);

        if (!this.ValidateFields(9))
        {
            this.ClearSong();
            return;
        }

        Song p = (Song)this.Master._PapaDal.Get("song", this.songHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearSong();
            return;
        }

        foreach (ListItem l in this.addSongToProductionSelector.Items)
        {
            l.Selected = false;
        }

        try
        {
            this.songLastUpdateLabel.Text = p.spLastUpdate;
            this.songStatusLabel.Text = p.spActive;
            this.songNameHe.Text = p.SongNameHe;
            this.songNameEn.Text = p.SongNameEn;
            this.songYoutubeAddress.Text = p.YouTubePath;
            this.songPlace.Text = p.SongPlace.ToString();
            this.addSongToProductionSelector.Items.FindByValue("s" + p.ProID).Selected = true;
        }
        catch (Exception e)
        {
            this.ClearSong();
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
        }
    }

    private void UpdateSong()
    {
        if (!this.ValidateFields(9))
        {
            this.ClearSong();
            return;
        }

        if (!this.ValidateFields(4))
        {
            this.ClearSong();
            return;
        }

        if (!this.ValidateFields(13))
        {
            this.ClearSong();
            return;
        }

        Song p = (Song)this.Master._PapaDal.Get("song", this.songHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearSong();
            return;
        }

        if (!this.Master._PapaDal.CheckAvailablePlaceExcept("song",
            p.SongPlace, int.Parse(this.songPlace.Text)))
        {
            this.Master._Logger.Error(new AdminException
                (@". !this.Master._PapaDal.CheckAvailablePlaceExcept(""song"",
                             p.SongPlace, int.Parse(this.songPlace.Text)))"),
            MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(21, "Red", this.songPlace.Text));
            this.ClearSong();
            return;
        }

        string textHe = "";
        if (this.songNameHe.Text != "")
        {
            textHe = this.Master._GlobalFunctions.ConvertToUtf8(this.songNameHe.Text);
        }

        try
        {
            p.ProID = this.addSongToProductionSelector.SelectedValue.Remove(0, 1);
            p.spLastUpdate = TimeNow.TheTimeNow.ToShortDateString();
            p.LastUpdate = TimeNow.TheTimeNow;
            p.SongNameHe = textHe;
            p.SongNameEn = this.songNameEn.Text;
            p.SongPlace = byte.Parse(this.songPlace.Text);

            this.Master._PapaDal.Update("song", p, TimeNow.TheTimeNow);
            this.Master._Logger.Log(new AdminException(". " + p.SongID +
                    " Was Successfully Updated"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(17, "White", p.SongNameHe));

            this.removeUpdateSongSelector.Items.FindByValue
            ("s" + p.SongID).Text = p.SongNameHe;
        }
        catch (Exception e)
        {
            this.Master._Logger.Log(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(18, "Red", p.SongNameHe));
        }
    }

    private void ShowProductionEnableDisable(bool visible)
    {
        this.diableProductionButton.Visible = visible;
        this.enableProductionButton.Visible = visible;
    }

    private void ShowSongEnableDisable(bool visible)
    {
        this.disableSongButton.Visible = visible;
        this.enableSongButton.Visible = visible;
    }

    private void ShowProductionFile(bool visible)
    {
        this.productionMainPicUpload.Visible = visible;
        this.productionColorPicUpload.Visible = visible;
        this.productionBWPicUpload.Visible = visible;
        if (visible)
        {
            this.productionMainPicUpload.Attributes["class"] = "mailYes";
            this.productionColorPicUpload.Attributes["class"] = "mailYes";
            this.productionBWPicUpload.Attributes["class"] = "mailYes";
        }
        else
        {
            this.productionMainPicUpload.Attributes["class"] = "mailNo";
            this.productionColorPicUpload.Attributes["class"] = "mailNo";
            this.productionBWPicUpload.Attributes["class"] = "mailNo";
        }
    }

    private void ShowProductionPicture(bool visible)
    {
        this.productionMainPicUploadPic.Visible = visible;
        this.productionColorPicUploadPic.Visible = visible;
        this.productionBWPicUploadPic.Visible = visible;
        if (visible)
        {
            this.productionMainPicUploadPic.Attributes["class"] = "mailYes";
            this.productionColorPicUploadPic.Attributes["class"] = "mailYes";
            this.productionBWPicUploadPic.Attributes["class"] = "mailYes";
        }
        else
        {
            this.productionMainPicUploadPic.Attributes["class"] = "mailNo";
            this.productionColorPicUploadPic.Attributes["class"] = "mailNo";
            this.productionBWPicUploadPic.Attributes["class"] = "mailNo";
        }
    }

    private void RemoveSong()
    {
        if (!this.ValidateFields(8))
        {
            this.ClearFields(4);
            this.ClearFields(6);
            return;
        }

        Song p = (Song)this.Master._PapaDal.Get("song", this.songHiddenRe.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearFields(4);
            this.ClearFields(6);
            return;
        }

        try
        {
            this.Master._PapaDal.Remove("song", p.SongID);
            this.Master._Logger.Log(new AdminException(". " + p.SongID + " Was Successfully Removed"),
                MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(12, "White", p.SongNameHe));

            this.removeUpdateSongSelector.Items.Remove
            (this.removeUpdateSongSelector.Items.FindByValue
            ("s" + p.SongID));
        }
        catch (Exception e)
        {
            this.Master._Logger.Log(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(13, "Red", p.SongNameHe));
        }
    }

    private void EnableSong()
    {
        if (!this.ValidateFields(9))
        {
            this.ClearSong();
            return;
        }

        Song p = (Song)this.Master._PapaDal.Get("song", this.songHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearSong();
            return;
        }

        if (p.Active == 1)
        {
            this.Master._Logger.Warn(new AdminException
            (". " + p.SongNameHe + " Is Already Disabled"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(9, "Red", p.SongNameHe));
            this.ClearSong();
            return;
        }

        try
        {
            this.Master._PapaDal.Enable("song", this.songHiddenUp.Value);
            this.Master._Logger.Log(new AdminException(". " + p.SongID +
                " Has Been Successfully Enabled"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(6, "White", p.SongNameHe));
        }
        catch (Exception f)
        {
            this.Master._Logger.Log(f, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(10, "Red", p.SongNameHe));
        }
    }

    private void DisableSong()
    {
        if (!this.ValidateFields(9))
        {
            this.ClearSong();
            return;
        }

        Song p = (Song)this.Master._PapaDal.Get("song", this.songHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearSong();
            return;
        }

        if (p.Active == 2)
        {
            this.Master._Logger.Warn(new AdminException
            (". " + p.SongNameHe + " Is Already Disabled"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(8, "Red", p.SongNameHe));
            this.ClearSong();
            return;
        }

        try
        {
            this.Master._PapaDal.Disable("song", this.songHiddenUp.Value);
            this.Master._Logger.Log(new AdminException(". " + p.SongID +
                " Has Been Successfully Disabeld"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(7, "White", p.SongNameHe));
        }
        catch (Exception f)
        {
            this.Master._Logger.Log(f, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(11, "Red", p.SongNameHe));
        }
    }

    private void EnableProduction()
    {
        if (!this.ValidateFields(6))
        {
            this.ClearSong();
            return;
        }

        Production p = (Production)this.Master._PapaDal.Get("production", this.productionHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearSong();
            return;
        }

        if (p.Active == 1)
        {
            this.Master._Logger.Warn(new AdminException
            (". " + p.ArtistNameHe + " Is Already Disabled"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(9, "Red", p.ArtistNameHe));
            this.ClearFields(3);
            return;
        }

        try
        {
            this.Master._PapaDal.Enable("production", p.ProID);
            this.Master._Logger.Log(new AdminException(". " + p.ProID +
                " Has Been Successfully Enabled"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(6, "White", p.ArtistNameHe));
        }
        catch (Exception f)
        {
            this.Master._Logger.Log(f, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(10, "Red", p.ArtistNameHe));
        }
    }

    private void DisableProduction()
    {
        if (!this.ValidateFields(6))
        {
            this.ClearSong();
            return;
        }

        Production p = (Production)this.Master._PapaDal.Get("production", this.productionHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearSong();
            return;
        }

        if (p.Active == 2)
        {
            this.Master._Logger.Warn(new AdminException
            (". " + p.ArtistNameHe + " Is Already Disabled"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(8, "Red", p.ArtistNameHe));
            this.ClearSong();
            return;
        }

        try
        {
            this.Master._PapaDal.Disable("production", p.ProID);
            this.Master._Logger.Log(new AdminException(". " + p.ArtistNameHe +
                " Has Been Successfully Disabeld"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(7, "White", p.ArtistNameHe));
        }
        catch (Exception f)
        {
            this.Master._Logger.Log(f, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(11, "Red", p.ArtistNameHe));
        }
    }

    protected void okBut_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(2))
        {
            return;
        }

        this.AfterOk(this.productionHidden.Value);
    }

    protected void cancelBut_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    private void ClearProduction()
    {
        this.ClearFields(2);
        this.ClearFields(3);
        this.ClearFields(4);
    }

    private void ClearSong()
    {
        this.ClearFields(4);
        this.ClearFields(5);
        this.ClearFields(6);
    }

    public void ShowProductionUpdateInfo(bool visible)
    {
        this.productionUpdateInfo.Visible = visible;
        if (visible)
        {
            this.productionUpdateInfo.Attributes["class"] = "mailYes";
        }
        else
        {
            this.productionUpdateInfo.Attributes["class"] = "mailNo";
        }
    }

    public void ShowSongUpdateInfo(bool visible)
    {
        this.songUpdateInfo.Visible = visible;
        if (visible)
        {
            this.songUpdateInfo.Attributes["class"] = "mailYes";
        }
        else
        {
            this.songUpdateInfo.Attributes["class"] = "mailNo";
        }
    }

    private void Start()
    {
        this.ClearFields(1);
        this.ClearFields(2);
        this.ClearFields(3);
        this.ClearFields(4);
        this.ClearFields(5);
        this.ClearFields(6);

        this.ShowProductionEnableDisable(false);
        this.ShowSongEnableDisable(false);
        this.ShowProductionFile(false);
        this.ShowProductionPicture(false);
        this.ShowProductionUpdateInfo(false);
        this.ShowSongUpdateInfo(false);

        this.DivSwitcher(1);
    }

    private void DeleteSongs(string productionID)
    {
        if (productionID == "" || productionID == null)
        {
            this.Master._Logger.Error(new AdminException
            (". productionID == \"\" || productionID == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            return;
        }

        Production p = (Production)this.Master._PapaDal.Get("production", productionID);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
            (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            return;
        }

        foreach (Song d in this.Master._PapaDal.GetAllSongs(p.ProID))
        {
            this.removeUpdateSongSelector.Items.Remove
            (this.removeUpdateSongSelector.Items.FindByValue("s" + d.SongID));
        }
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
                if (this.productionHiddenRe.Value == "")
                {
                    this.RemoveSong();
                }
                else
                {
                    this.RemoveProduction();
                }
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
            this.productionHidden.Value = "";
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
            this.productionHidden.Value = message[2];

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
            this.notifyLabel.Text = "Ooooops! Something Wrong Was Happend, Please Try Again Or/And content The Administrator";
        }
        finally
        {
            this.DivSwitcher(6);
        }
    }
}


