using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Reflection;
using Dal;
using System.IO;

public partial class Admin_Publish : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.publishSelector.Items.Add(new ListItem("--Select Action--", "1"));
            this.publishSelector.Items.Add(new ListItem("Add New Gallery", "2"));
            this.publishSelector.Items.Add(new ListItem("Remove/Update Gallery", "3"));
            this.publishSelector.Items.Add(new ListItem("Add New Picture", "4"));
            this.publishSelector.Items.Add(new ListItem("Remove/Update Picture", "5"));

            foreach (Gallery p in (IEnumerable<Gallery>)this.Master._PapaDal.GetAll("publishGallery"))
            {
                ListItem l = new ListItem(p.GalleryNameHe, "s" + p.GalleryID);
                this.removeUpdatePublishGallerySelector.Items.Add(l);
                this.addPicGallerySelector.Items.Add(l);
            }

            foreach (PublishPic p in (IEnumerable<PublishPic>)this.Master._PapaDal.GetAll("publishPic"))
            {
                this.removeUpdatePicGallerySelector.Items.Add(new ListItem(p.TextHe, "s" + p.PicID));
            }

            for (int i = 0; i < 7; i++)
            {
                this.topPlaceSelector.Items.Add
                (new ListItem(i.ToString(), i.ToString()));
            }

            this.DivSwitcher(1);
        }
    }

    protected void publishSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!this.ValidateFields(1))
        {
            return;
        }

        this.DivSwitcher(int.Parse(this.publishSelector.SelectedValue));
    }

    protected void addPublishGalleryButton_Click(object sender, EventArgs e)
    {
        if (this.publishGalleryHiddenUp.Value == "")
        {
            this.AddPublishGallery();
        }
        else
        {
            this.UpdateGallery();
            this.publishGalleryHiddenUp.Value = "";
        }
    }

    protected void cancelPublishGalleryButton_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    protected void removePublishGalleryButton_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(12))
        {
            return;
        }

        Gallery p = (Gallery)this.Master._PapaDal.Get("gallery",
        this.removeUpdatePublishGallerySelector.SelectedValue.Remove(0, 1));
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearGallery();
            return;
        }

        this.publishGalleryHiddenRe.Value = p.GalleryID;
        this.Notify(this.Master._Notifier.Notify(5, "Red", p.GalleryNameHe));
    }

    protected void updatePublishGalleryButton_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(12))
        {
            return;
        }

        Gallery p = (Gallery)this.Master._PapaDal.Get("gallery",
        this.removeUpdatePublishGallerySelector.SelectedValue.Remove(0, 1));
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearGallery();
            return;
        }

        this.publishGalleryHiddenUp.Value = p.GalleryID;
        this.UpdateGalleryInit();
        this.DivSwitcher(2);
    }

    protected void addPublishPicGalleryButton_Click(object sender, EventArgs e)
    {
        if (this.publishPicHiddenUp.Value == "")
        {
            this.AddPublishPic();
        }
        else
        {
            this.UpdatePic();
            this.publishPicHiddenUp.Value = "";
        }
    }

    protected void disablePublishPicGalleryButton_Click(object sender, EventArgs e)
    {
        this.DisablePic();
    }

    protected void enablePublishPicGalleryButton_Click(object sender, EventArgs e)
    {
        this.EnablePic();
    }

    protected void cancelAddPicGalleryButton_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    protected void updatePublishPicGalleryButton_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(13))
        {
            return;
        }

        PublishPic p = (PublishPic)this.Master._PapaDal.Get("publishPic",
        this.removeUpdatePicGallerySelector.SelectedValue.Remove(0, 1));
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearPic();
            return;
        }

        this.publishPicHiddenUp.Value = p.PicID;
        this.UpdatePicInit();
        this.DivSwitcher(4);
    }

    protected void removePublishPicGalleryButton_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(13))
        {
            return;
        }

        PublishPic p = (PublishPic)this.Master._PapaDal.Get("publishPic",
        this.removeUpdatePicGallerySelector.SelectedValue.Remove(0, 1));
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearPic();
            return;
        }

        this.publishPicHiddenRe.Value = p.PicID;
        this.Notify(this.Master._Notifier.Notify(5, "Red", p.PicID));
    }

    protected void cancelPublishPicGalleryButton_Click(object sender, EventArgs e)
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
                if (this.publishSelector.SelectedIndex == 0)
                {
                    this.Master._Logger.Error(new AdminException(". this.publishSelector.SelectedIndex == 0"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(1, "Red", ""));
                    return false;
                }
                break;
            case 2:
                if (this.publishPicUpload.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.publishPicUpload.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(37, "Red", "Publish"));
                    return false;
                }
                break;
            case 3:
                if (this.removeUpdatePicGallerySelector.SelectedValue == null)
                {
                    this.Master._Logger.Error(new AdminException
                            (". this.removeUpdatePicGallerySelector.SelectedValue == null"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(4, "Red", this.publishPicUpload.Value));
                    return false;
                }
                break;
            case 4:
                if (this.publishGalleryHiddenRe.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.publishGalleryHiddenRe.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
                    return false;
                }
                break;
            case 5:
                if (this.publishGalleryHiddenUp.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.publishGalleryHiddenUp.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
                    return false;
                }
                break;
            case 6:
                if (this.publishHidden.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.publishHidden.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
                    return false;
                }
                break;
            case 7:
                if (this.publishPicUpload.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.publishPicUpload.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(37, "Red", "Publish"));
                    return false;
                }
                break;
            case 8:
                if (this.publishPicHiddenRe.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.publishPicHiddenRe.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
                    return false;
                }
                break;
            case 9:
                if (this.publishPicHiddenUp.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.publishPicHiddenUp.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
                    return false;
                }
                break;
            case 10:
                if (this.publishGalleryNameHe.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.publishGalleryNameHe.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(24, "Red", ""));
                    return false;
                }

                if (this.publishGalleryNameEn.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.publishGalleryNameEn.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(25, "Red", ""));
                    return false;
                }

                if (this.publishGalleryPlace.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.publishGalleryPlace.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(63, "Red", ""));
                    return false;
                }

                int i = -1;
                if (!int.TryParse(this.publishGalleryPlace.Text, out i))
                {

                    this.Master._Logger.Error(new AdminException
                    (". (!int.TryParse(this.publishGalleryPlace.Text, out i))"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(64, "Red", ""));
                    return false;
                }

                if (i < 0)
                {
                    this.Master._Logger.Error(new AdminException
                    (". (i < 0)"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(64, "Red", ""));
                    return false;
                }
                break;
            case 11:
                if (this.addPicGallerySelector.SelectedValue == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.addPicGallerySelector.SelectedValue == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(65, "Red", ""));
                    return false;
                }

                if (this.publishPicTextHe.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                     (". this.publishPicTextHe.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(24, "Red", ""));
                    return false;
                }

                if (this.publishPicTextEn.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.publishPicTextEn.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(25, "Red", ""));
                    return false;
                }

                if (this.publishPicPlace.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.publishGalleryPlace.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(19, "Red", ""));
                    return false;
                }

                int v = -1;
                if (!int.TryParse(this.publishPicPlace.Text, out v))
                {

                    this.Master._Logger.Error(new AdminException
                    (". (!int.TryParse(this.publishPicPlace.Text, out v))"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(20, "Red", ""));
                    return false;
                }

                if (v < 0)
                {
                    this.Master._Logger.Error(new AdminException
                    (". (v < 0)"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(20, "Red", ""));
                    return false;
                }

                if (v > 16)
                {
                    this.Master._Logger.Error(new AdminException
                    (". (v > 16)"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(66, "Red", v.ToString()));
                    return false;
                }
                break;
            case 12:
                if (this.removeUpdatePublishGallerySelector.SelectedValue == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.removeUpdatePublishGallerySelector.SelectedValue == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(57, "Red", "Gallery"));
                    return false;
                }
                break;
            case 13:
                if (this.removeUpdatePicGallerySelector.SelectedValue == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.removeUpdatePicGallerySelector.SelectedValue == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(57, "Red", "Picture"));
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
                foreach (ListItem l in this.publishSelector.Items)
                {
                    l.Selected = false;
                }
                break;
            case 2:
                this.galleryLastUpdateLabel.Text = "";
                this.publishGalleryNameHe.Text = "";
                this.publishGalleryNameEn.Text = "";
                this.publishGalleryPlace.Text = "";
                break;
            case 3:
                foreach (ListItem l in this.removeUpdatePublishGallerySelector.Items)
                {
                    l.Selected = false;
                }
                break;
            case 4:
                this.publishPicHiddenRe.Value = "";
                this.publishPicHiddenUp.Value = "";
                this.publishGalleryHiddenRe.Value = "";
                this.publishGalleryHiddenUp.Value = "";
                break;
            case 5:
                foreach (ListItem l in this.addPicGallerySelector.Items)
                {
                    l.Selected = false;
                }
                foreach (ListItem l in this.topPlaceSelector.Items)
                {
                    l.Selected = false;
                }
                this.publishPicLastUpdateLabel.Text = "";
                this.publishPicStatusLabel.Text = "";
                this.publishPicTextHe.Text = "";
                this.publishPicTextEn.Text = "";
                this.publishPicPlace.Text = "";
                this.publishPicUploadPic.Src = "";
                break;
            case 6:
                foreach (ListItem l in this.removeUpdatePicGallerySelector.Items)
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

        this.ShowPublishPicUpdateInfo(false);
        this.ShowGalleryUpdateInfo(false);

        this.mainPublish.Visible = false;
        this.addPublishGallery.Visible = false;
        this.removeUpdatePublishGallery.Visible = false;
        this.addPicToGallery.Visible = false;
        this.removeUpdatePicGallery.Visible = false;
        this.publishNotify.Visible = false;

        this.mainPublish.Attributes["class"] = "mailNo";
        this.addPublishGallery.Attributes["class"] = "mailNo";
        this.removeUpdatePublishGallery.Attributes["class"] = "mailNo";
        this.addPicToGallery.Attributes["class"] = "mailNo";
        this.removeUpdatePicGallery.Attributes["class"] = "mailNo";
        this.publishNotify.Attributes["class"] = "mailNo";

        switch (action)
        {
            case 1:
                this.mainPublish.Visible = true;
                this.mainPublish.Attributes["class"] = "mailYes";
                break;
            case 2:
                this.addPublishGallery.Visible = true;
                this.addPublishGallery.Attributes["class"] = "mailYes";

                if (this.publishGalleryHiddenUp.Value == "")
                {
                    this.ShowGalleryUpdateInfo(false);
                }
                else
                {
                    this.ShowGalleryUpdateInfo(true);
                }
                break;
            case 3:
                this.removeUpdatePublishGallery.Visible = true;
                this.removeUpdatePublishGallery.Attributes["class"] = "mailYes";
                break;
            case 4:
                this.addPicToGallery.Visible = true;
                this.addPicToGallery.Attributes["class"] = "mailYes";

                if (this.publishPicHiddenUp.Value == "")
                {
                    this.ShowPublishFile(true);
                    this.ShowPublishPicture(false);
                    this.ShowEnableDisable(false);
                    this.ShowGalleryUpdateInfo(false);
                }
                else
                {
                    this.ShowPublishFile(true);
                    this.ShowPublishPicture(true);
                    this.ShowEnableDisable(true);
                    this.ShowPublishPicUpdateInfo(true);
                }
                break;
            case 5:
                this.removeUpdatePicGallery.Visible = true;
                this.removeUpdatePicGallery.Attributes["class"] = "mailYes";
                break;
            case 6:
                this.publishNotify.Visible = true;
                this.publishNotify.Attributes["class"] = "mailYes";
                break;
            default:
                break;
        }
    }

    private void AddPublishGallery()
    {
        if (!this.ValidateFields(10))
        {
            this.ClearGallery();
            return;
        }

        if (!this.Master._PapaDal.CheckAvailablePlace
            ("publishGallery", int.Parse(this.publishGalleryPlace.Text)))
        {
            this.Master._Logger.Error(new AdminException
                (@". (!this.Master._PapaDal.CheckAvailablePlace
                (""publishGallery"", int.Parse(this.publishGalleryPlace.Text)))"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(21, "Red", this.publishGalleryPlace.Text));
            this.ClearGallery();
            return;
        }

        try
        {
            string textHe = this.Master._GlobalFunctions.ConvertToUtf8(this.publishGalleryNameHe.Text);

            Gallery g = new Gallery
            {
                GalleryID = this.Master._PapaDal.GetNextAvailableID("gallery"),
                GalleryNameEn = this.publishGalleryNameEn.Text,
                GalleryNameHe = textHe,
                GalleryType = "publishGallery",
                LastUpdate = TimeNow.TheTimeNow,
                spLastUpdate = TimeNow.TheTimeNow.ToShortDateString(),
                GalleryPlace = int.Parse(this.publishGalleryPlace.Text),
                spCreationTime = TimeNow.TheTimeNow.ToShortDateString(),
                CreationTime = TimeNow.TheTimeNow,
            };

            this.Master._PapaDal.Add("gallery", g);
            this.Master._Logger.Log(new AdminException(". " + g.GalleryID + " Was Successfully Added"),
                            MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(15, "White", g.GalleryNameHe));

            ListItem l = new ListItem(g.GalleryNameHe, "s" + g.GalleryID);

            this.addPicGallerySelector.Items.Add(l);
            this.removeUpdatePublishGallerySelector.Items.Add(l);
        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(16, "Red", this.publishGalleryNameHe.Text));
        }
    }

    private void UpdateGalleryInit()
    {
        if (!this.ValidateFields(5))
        {
            this.ClearGallery();
            return;
        }

        Gallery p = (Gallery)this.Master._PapaDal.Get("gallery", this.publishGalleryHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearGallery();
            return;
        }

        try
        {
            this.galleryLastUpdateLabel.Text = p.spLastUpdate;
            this.publishGalleryNameHe.Text = p.GalleryNameHe;
            this.publishGalleryNameEn.Text = p.GalleryNameEn;
            this.publishGalleryPlace.Text = p.GalleryPlace.ToString();
            this.ShowGalleryUpdateInfo(true);
        }
        catch (Exception e)
        {
            this.ClearGallery();
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
        }
    }

    private void UpdateGallery()
    {
        if (!this.ValidateFields(5))
        {
            this.ClearGallery();
            return;
        }

        if (!this.ValidateFields(10))
        {
            this.ClearGallery();
            return;
        }

        Gallery g = (Gallery)this.Master._PapaDal.Get("gallery", this.publishGalleryHiddenUp.Value);
        if (g == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". g == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearGallery();
            return;
        }

        if (!this.Master._PapaDal.CheckAvailablePlaceExcept
           ("publishGallery", g.GalleryPlace, int.Parse(this.publishGalleryPlace.Text)))
        {
            this.Master._Logger.Error(new AdminException
                (@". (!this.Master._PapaDal.CheckAvailablePlaceExcept
           (""publishGallery"", g.GalleryPlace, int.Parse(this.publishGalleryPlace.Text)))"),
            MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(21, "Red", this.publishGalleryPlace.Text));
            this.ClearGallery();
            return;
        }

        try
        {
            string textHe = this.Master._GlobalFunctions.ConvertToUtf8(this.publishGalleryNameHe.Text);

            g.spLastUpdate = TimeNow.TheTimeNow.ToShortDateString();
            g.LastUpdate = TimeNow.TheTimeNow;
            g.GalleryNameHe = textHe;
            g.GalleryNameEn = this.publishGalleryNameEn.Text;
            g.GalleryPlace = int.Parse(this.publishGalleryPlace.Text);

            this.Master._PapaDal.Update("gallery", g, TimeNow.TheTimeNow);
            this.Master._Logger.Log(new AdminException(". " + g.GalleryID +
                    " Was Successfully Updated"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(17, "White", g.GalleryNameHe));

            this.removeUpdatePublishGallerySelector.Items.FindByValue
            ("s" + g.GalleryID).Text = g.GalleryNameHe;
            this.addPicGallerySelector.Items.FindByValue
            ("s" + g.GalleryID).Text = g.GalleryNameHe;
        }
        catch (Exception e)
        {
            this.Master._Logger.Log(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(18, "Red", g.GalleryNameHe));
        }
    }

    private void RemoveGallery()
    {
        if (!this.ValidateFields(4))
        {
            this.ClearGallery();
            return;
        }

        Gallery g = (Gallery)this.Master._PapaDal.Get("gallery", this.publishGalleryHiddenRe.Value);
        if (g == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". g == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearGallery();
            return;
        }

        try
        {
            this.DeletePictures(g.GalleryID);

            this.Master._PapaDal.Remove("gallery", g.GalleryID);
            this.Master._Logger.Log(new AdminException(". " + g.GalleryID + " Was Successfully Removed"),
                MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(12, "White", g.GalleryNameHe));

            this.removeUpdatePublishGallerySelector.Items.Remove
            (this.removeUpdatePublishGallerySelector.Items.FindByValue
            ("s" + g.GalleryID));
            this.addPicGallerySelector.Items.Remove
            (this.addPicGallerySelector.Items.FindByValue
            ("s" + g.GalleryID));
        }
        catch (Exception)
        {
            try
            {
                this.Master._PapaDal.Remove("gallery", g.GalleryID);
                this.Master._Logger.Log(new AdminException(". " + g.GalleryID + " Was Successfully Removed"),
                    MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(12, "White", g.GalleryNameHe));

                this.removeUpdatePicGallerySelector.Items.Remove
                (this.removeUpdatePicGallerySelector.Items.FindByValue
                ("s" + g.GalleryID));
                this.addPicGallerySelector.Items.Remove
                (this.addPicGallerySelector.Items.FindByValue
                ("s" + g.GalleryID));
            }
            catch (Exception e)
            {
                this.Master._Logger.Log(e, MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(13, "Red", g.GalleryNameHe));
            }
        }
    }

    private void AddPublishPic()
    {
        if (!this.ValidateFields(2))
        {
            this.ClearPic();
            return;
        }

        if (!this.ValidateFields(11))
        {
            this.ClearPic();
            return;
        }

        string galleryID = this.addPicGallerySelector.SelectedValue.Remove(0, 1);

        if (!this.Master._PapaDal.CheckAvailableGalleriesPicturePlace("publishPic", galleryID, 
            int.Parse(this.publishPicPlace.Text)))
        {
            this.Master._Logger.Error(new AdminException
                (@". (!this.Master._PapaDal.CheckAvailableGalleriesPicturePlace(""publishPic"", ID, 
            int.Parse(this.publishPicPlace.Text)))"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(21, "Red", this.publishPicPlace.Text));
            this.ClearPic();
            return;
        }

        if (!this.Master._PapaDal.CheckAvailablePlace("publishPicTop",
                    int.Parse(this.topPlaceSelector.SelectedValue)))
        {
            this.Master._Logger.Error(new AdminException
                (@". !this.Master._PapaDal.CheckAvailablePlace(""publishPicTop"", 
                    int.Parse(this.topPlaceSelector.SelectedValue))"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(21, "Red", this.topPlaceSelector.SelectedValue));
            this.ClearPic();
            return;
        }

        string ID = this.Master._PapaDal.GetNextAvailableID("publishPic");
        string fileName = this.publishPicUpload.PostedFile.FileName;
        string fileNameToSave = "publishPic_id-" + ID + "_" + fileName;
        string fullPath = this.Master._GlobalFunctions.GetFullPath() + fileNameToSave;
        string relativePath = this.Master._GlobalFunctions.GetRelativePath() + fileNameToSave;


        if (!this.Master._GlobalFunctions.ValidatePicEnd(this.publishPicUpload.Value))
        {
            this.Master._Logger.Error(new AdminException
            (". !this.Master._GlobalFunctions.ValidatePicEnd(this.publishPicUpload.Value)"),
                MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(14, "Red", this.publishPicUpload.Value));
            this.ClearPic();
            return;
        }

        try
        {
            this.publishPicUpload.PostedFile.SaveAs(fullPath);
        }
        catch (Exception u)
        {
            this.Master._Logger.Error(u, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(36, "Red", this.publishPicUpload.Value));
            this.ClearPic();
            return;
        }

        try
        {

            string textHe = this.Master._GlobalFunctions.ConvertToUtf8(this.publishPicTextHe.Text);

            PublishPic p = new PublishPic
            {
                Active = 2,
                GalleryID = galleryID,
                PicFullPath = fullPath,
                PicRelativePath = relativePath,
                PicPlace = int.Parse(this.publishPicPlace.Text),
                TopPagePlace = byte.Parse(this.topPlaceSelector.SelectedValue),
                PicID = ID,
                TextEn = this.publishPicTextEn.Text,
                TextHe = textHe,
                LastUpdate = TimeNow.TheTimeNow,
                spLastUpdate = TimeNow.TheTimeNow.ToShortDateString(),
                UploadTime = TimeNow.TheTimeNow,
                spUploadTime = TimeNow.TheTimeNow.ToString(),
                spActive = "Disable"
            };

            this.Master._PapaDal.Add("publishPic", p);
            this.Master._Logger.Log(new AdminException(". " + p.PicID + " Was Successfully Added"),
                            MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(15, "White", p.TextHe));

            this.removeUpdatePicGallerySelector.Items.Add(new ListItem(p.TextHe, "s" + ID));
        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(16, "Red", this.publishPicTextHe.Text));
        }
    }

    private void UpdatePicInit()
    {
        if (!this.ValidateFields(9))
        {
            this.ClearPic();
            return;
        }

        PublishPic p = (PublishPic)this.Master._PapaDal.Get("publishPic", this.publishPicHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearPic();
            return;
        }

        foreach (ListItem l in this.addPicGallerySelector.Items)
        {
            l.Selected = false;
        }

        foreach (ListItem l in this.topPlaceSelector.Items)
        {
            l.Selected = false;
        }

        try
        {
            this.publishPicStatusLabel.Text = p.spActive;
            this.publishPicLastUpdateLabel.Text = p.spLastUpdate;
            this.addPicGallerySelector.Items.FindByValue("s" + p.GalleryID).Selected = true;
            this.publishPicTextHe.Text = p.TextHe;
            this.publishPicTextEn.Text = p.TextEn;
            this.publishPicPlace.Text = p.PicPlace.ToString();
            this.topPlaceSelector.Items.FindByValue(p.TopPagePlace.ToString()).Selected = true;
            this.publishPicUploadPic.Src = p.PicRelativePath;
            this.ShowPublishPicture(true);
            this.ShowPublishFile(true);
            this.ShowPublishPicUpdateInfo(true);
        }
        catch (Exception e)
        {
            this.ClearPic();
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
        }
    }

    private void UpdatePic()
    {
        if (!this.ValidateFields(9))
        {
            this.ClearPic();
            return;
        }

        if (!this.ValidateFields(11))
        {
            this.ClearPic();
            return;
        }

        PublishPic p = (PublishPic)this.Master._PapaDal.Get("publishPic", this.publishPicHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearPic();
            return;
        }

        string galleryID = this.addPicGallerySelector.SelectedValue.Remove(0, 1);

        if (!this.Master._PapaDal.CheckAvailableGalleriesPicturePlaceExcept("publishPic", galleryID,
            p.PicPlace, int.Parse(this.publishPicPlace.Text)))
        {
            this.Master._Logger.Error(new AdminException
                (@". (!this.Master._PapaDal.CheckAvailableGalleriesPicturePlaceExcept(""publishPic"", p.GalleryID,
                 p.PicPlace, int.Parse(this.publishPicPlace.Text)))"), 
                MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(21, "Red", this.publishPicPlace.Text));
            this.ClearPic();
            return;
        }

        if (!this.Master._PapaDal.CheckAvailablePlaceExcept("publishPicTop",
            (int)p.TopPagePlace, int.Parse(this.topPlaceSelector.SelectedValue)))
        {
            this.Master._Logger.Error(new AdminException
                (@". !this.Master._PapaDal.CheckAvailablePlaceExcept(""publishPicTop"",
                             (int)p.TopPagePlace, int.Parse(this.topPlaceSelector.SelectedValue))"),
            MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(21, "Red", this.topPlaceSelector.SelectedValue));
            this.ClearPic();
            return;
        }

        if (this.publishPicUpload.Value != "")
        {
            if (!this.Master._GlobalFunctions.ValidatePicEnd(this.publishPicUpload.Value))
            {
                this.Master._Logger.Error(new AdminException
                (". !this.Master._GlobalFunctions.ValidatePicEnd(this.publishPicUpload.Value)"),
                     MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(14, "Red", this.publishPicUpload.Value));
                this.ClearPic();
                return;
            }

            string fileName = this.publishPicUpload.PostedFile.FileName;
            string fileNameToSave = "publishPic_id-" + p.PicID + "_" + fileName;
            string fullPath = this.Master._GlobalFunctions.GetFullPath() + fileNameToSave;
            string relativePath = this.Master._GlobalFunctions.GetRelativePath() + fileNameToSave;

            try
            {
                if (File.Exists(p.PicFullPath))
                {
                    File.Delete(p.PicFullPath);
                }
                this.publishPicUpload.PostedFile.SaveAs(fullPath);

                p.PicFullPath = fullPath;
                p.PicRelativePath = relativePath;
            }
            catch (Exception h)
            {
                this.Master._Logger.Error(h, MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(36, "Red", this.publishPicUpload.Value));
                this.ClearPic();
                return;
            }
        }

        string textHe = this.Master._GlobalFunctions.ConvertToUtf8(this.publishPicTextHe.Text);

        if (!this.Master._PapaDal.CheckPublishGalleryStatus(galleryID))
        {
            this.Master._Logger.Error(new AdminException
            (@". (!this.Master._PapaDal.CheckGalleryStatus(galleryID))"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(52, "Red", this.topPlaceSelector.SelectedValue));
            this.ClearPic();
            return;
        }

        try
        {
            p.GalleryID = galleryID;
            p.spLastUpdate = TimeNow.TheTimeNow.ToShortDateString();
            p.LastUpdate = TimeNow.TheTimeNow;
            p.TextHe = textHe;
            p.TextEn = this.publishPicTextEn.Text;
            p.TopPagePlace = byte.Parse(this.topPlaceSelector.SelectedValue);

            this.Master._PapaDal.Update("publishPic", p, TimeNow.TheTimeNow);
            this.Master._Logger.Log(new AdminException(". " + p.PicID +
                    " Was Successfully Updated"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(17, "White", p.TextHe));

            this.removeUpdatePicGallerySelector.Items.FindByValue
            ("s" + p.PicID).Text = p.TextHe;
        }
        catch (Exception e)
        {
            this.Master._Logger.Log(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(18, "Red", textHe));
        }
    }

    private void ShowEnableDisable(bool visible)
    {
        this.disablePublishPicGalleryButton.Visible = visible;
        this.enablePublishPicGalleryButton.Visible = visible;
    }

    private void ShowPublishFile(bool visible)
    {
        this.publishPicUpload.Visible = visible;
        if (visible)
        {
            this.publishPicUpload.Attributes["class"] = "mailYes";
        }
        else
        {
            this.publishPicUpload.Attributes["class"] = "mailNo";
        }
    }

    private void ShowPublishPicture(bool visible)
    {
        this.publishPicUploadPic.Visible = visible;
        if (visible)
        {
            this.publishPicUploadPic.Attributes["class"] = "mailYes";
        }
        else
        {
            this.publishPicUploadPic.Attributes["class"] = "mailNo";
        }
    }

    private void RemovePic()
    {
        if (!this.ValidateFields(8))
        {
            this.ClearPic();
            return;
        }

        PublishPic p = (PublishPic)this.Master._PapaDal.Get("publishPic", this.publishPicHiddenRe.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearPic();
            return;
        }

        try
        {
            if (File.Exists(p.PicFullPath))
            {
                File.Delete(p.PicFullPath);
            }

            this.Master._PapaDal.Remove("publishPic", p.PicID);
            this.Master._Logger.Log(new AdminException(". " + p.PicID + " Was Successfully Removed"),
                MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(12, "White", p.TextHe));

            this.removeUpdatePicGallerySelector.Items.Remove
            (this.removeUpdatePicGallerySelector.Items.FindByValue
            ("s" + p.PicID));
        }
        catch (Exception e)
        {
            this.Master._Logger.Log(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(13, "Red", p.TextHe));
        }
    }

    private void EnablePic()
    {
        if (!this.ValidateFields(9))
        {
            this.ClearPic();
            return;
        }

        PublishPic p = (PublishPic)this.Master._PapaDal.Get("publishPic", this.publishPicHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearPic();
            return;
        }

        if (!this.Master._PapaDal.CheckPublishGalleryStatus(p.GalleryID))
        {
            this.Master._Logger.Error(new AdminException
            (@". (!this.Master._PapaDal.CheckPublishGalleryStatus(p.GalleryID))"),
                MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(52, "Red", ""));
            this.ClearPic();
            return;
        }

        if (this.topPlaceSelector.SelectedValue != "")
        {
            if (this.topPlaceSelector.SelectedValue != "0")
            {
                if (!this.Master._PapaDal.CheckPlacesStatus("publishPicTop"))
                {
                    this.Master._Logger.Error(new AdminException
                    (@". (!this.Master._PapaDal.CheckPlacesStatus(""publishPicTop""))"),
                    MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(67, "Red", "Home Page Publish Pictures"));
                    this.ClearPic();
                    return;
                }
            }
        }

        if (p.Active == 1)
        {
            this.Master._Logger.Warn(new AdminException
            (". " + p.PicID + " Is Already Disabled"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(9, "Red", p.TextHe));
            this.ClearPic();
            return;
        }

        try
        {
            this.Master._PapaDal.Enable("publishPic", p.PicID);
            this.Master._Logger.Log(new AdminException(". " + p.PicID +
                " Has Been Successfully Enabled"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(6, "White", p.TextHe));
        }
        catch (Exception f)
        {
            this.Master._Logger.Log(f, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(10, "Red", p.TextHe));
        }
    }

    private void DisablePic()
    {
        if (!this.ValidateFields(9))
        {
            this.ClearPic();
            return;
        }

        PublishPic p = (PublishPic)this.Master._PapaDal.Get("publishPic", this.publishPicHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearPic();
            return;
        }

        if (p.Active == 2)
        {
            this.Master._Logger.Warn(new AdminException
            (". " + p.PicID + " Is Already Disabled"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(8, "Red", p.TextHe));
            this.ClearPic();
            return;
        }

        try
        {
            this.Master._PapaDal.Disable("publishPic", p.PicID);
            this.Master._Logger.Log(new AdminException(". " + p.PicID +
                " Has Been Successfully Disabeld"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(7, "White", p.TextHe));
        }
        catch (Exception f)
        {
            this.Master._Logger.Log(f, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(11, "Red", p.TextHe));
        }
    }

    protected void okBut_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(6))
        {
            return;
        }

        this.AfterOk(this.publishHidden.Value);
    }

    protected void cancelBut_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    private void ClearGallery()
    {
        this.ClearFields(2);
        this.ClearFields(3);
        this.ClearFields(4);
    }

    private void ClearPic()
    {
        this.ClearFields(4);
        this.ClearFields(5);
        this.ClearFields(6);
    }

    public void ShowGalleryUpdateInfo(bool visible)
    {
        this.galleryUpdateInfo.Visible = visible;
        if (visible)
        {
            this.galleryUpdateInfo.Attributes["class"] = "mailYes";
        }
        else
        {
            this.galleryUpdateInfo.Attributes["class"] = "mailNo";
        }
    }

    public void ShowPublishPicUpdateInfo(bool visible)
    {
        this.publishPicUpdateInfo.Visible = visible;
        if (visible)
        {
            this.publishPicUpdateInfo.Attributes["class"] = "mailYes";
        }
        else
        {
            this.publishPicUpdateInfo.Attributes["class"] = "mailNo";
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

        this.ShowPublishPicture(false);
        this.ShowPublishFile(false);
        this.ShowEnableDisable(false);

        this.DivSwitcher(1);
    }

    private void DeletePictures(string galleryID)
    {
        if (galleryID == "" || galleryID == null)
        {
            this.Master._Logger.Error(new AdminException
            (". galleryID == \"\" || galleryID == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            return;
        }

        Gallery p = (Gallery)this.Master._PapaDal.Get("gallery", galleryID);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
            (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            return;
        }

        foreach (PublishPic d in this.Master._PapaDal.GetAllPublishPictures(p.GalleryID))
        {
            this.removeUpdatePicGallerySelector.Items.Remove
            (this.removeUpdatePicGallerySelector.Items.FindByValue("s" + d.PicID));
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
                if (this.publishGalleryHiddenRe.Value == "")
                {
                    this.RemovePic();
                }
                else
                {
                    this.RemoveGallery();
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
            this.publishHidden.Value = "";
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
            this.publishHidden.Value = message[2];

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
            this.DivSwitcher(6);
        }
    }
}
