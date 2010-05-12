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

public partial class Admin_Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.homeSelector.Items.Add(new ListItem("--Select Action--", "1"));
            this.homeSelector.Items.Add(new ListItem("Add New Header Picture", "2"));
            this.homeSelector.Items.Add(new ListItem("Remove/Update Header Picture", "3"));
            this.homeSelector.Items.Add(new ListItem("Add New Last Records Picture", "4"));
            this.homeSelector.Items.Add(new ListItem("Remove/Update Last Records Picture", "5"));
            this.homeSelector.Items.Add(new ListItem("Add New Will Come Soon Picture", "6"));
            this.homeSelector.Items.Add(new ListItem("Remove/Update Will Come Soon Picture", "7"));
            this.homeSelector.Items.Add(new ListItem("Add New Last News", "8"));
            this.homeSelector.Items.Add(new ListItem("Remove/Update Last Records News", "9"));
            this.homeSelector.Items.Add(new ListItem("Update Contact And About Text", "10"));

            foreach (HeaderPic p in (IEnumerable<HeaderPic>)this.Master._PapaDal.GetAll("headerPic"))
            {
                this.removeUpdateHeaderPicSelector.Items.Add(new ListItem(p.PicName, "s" + p.PicID));
            }

            foreach (LastRecordPic p in (IEnumerable<LastRecordPic>)this.Master._PapaDal.GetAll("lastRecordsPic"))
            {
                this.removeUpdatelastRecordsSelector.Items.Add(new ListItem(p.PicName, "s" + p.PicID));
            }

            foreach (WillComePic p in (IEnumerable<WillComePic>)this.Master._PapaDal.GetAll("willComePic"))
            {
                this.removeUpdateWillComePicSelector.Items.Add(new ListItem(p.PicName, "s" + p.PicID));
            }

            foreach (LastNew p in (IEnumerable<LastNew>)this.Master._PapaDal.GetAll("lastNews"))
            {
                this.removeUpdateLastNewsSelector.Items.Add(new ListItem(p.NewsID, "s" + p.NewsID));
            }

            for (int i = 0; i < 10; i++)
            {
                this.lastRecordsPicPlaceSelector.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

            for (int i = 0; i < 4; i++)
            {
                this.willComePicPlaceSelector.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

            this.DivSwitcher(1);
        }
    }

    protected void homeSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!this.ValidateFields(1))
        {
            return;
        }

        this.DivSwitcher(int.Parse(this.homeSelector.SelectedValue));
    }

    protected void addHeaderPicButton_Click(object sender, EventArgs e)
    {
        if (this.headerPicHiddenUp.Value == "")
        {
            this.AddHeaderPic();
        }
        else
        {
            this.UpdateHeaderPic();
            this.headerPicHiddenUp.Value = "";
        }
    }

    protected void diableHeaderPicButton_Click(object sender, EventArgs e)
    {
        this.DisableHeaderPic();
    }

    protected void enableHeaderPicButton_Click(object sender, EventArgs e)
    {
        this.EnableHeaderPic();
    }

    protected void cancelAddHeaderPicButton_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    protected void removeHeaderPicButton_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(16))
        {
            this.ClearHeaderPic();
            return;
        }

        HeaderPic p = (HeaderPic)this.Master._PapaDal.Get("headerPic",
        this.removeUpdateHeaderPicSelector.SelectedValue.Remove(0, 1));
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearHeaderPic();
            return;
        }

        this.headerPicHiddenRe.Value = p.PicID;
        this.Notify(this.Master._Notifier.Notify(5, "Red", p.PicName));
    }

    protected void updateHeaderPicButton_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(16))
        {
            this.ClearHeaderPic();
            return;
        }

        HeaderPic p = (HeaderPic)this.Master._PapaDal.Get("headerPic",
        this.removeUpdateHeaderPicSelector.SelectedValue.Remove(0, 1));
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearHeaderPic();
            return;
        }

        this.headerPicHiddenUp.Value = p.PicID;
        this.UpdateHeaderPicInit();
        this.DivSwitcher(2);
    }

    protected void cancelHeaderPicButton_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    protected void addLastRecordsButton_Click(object sender, EventArgs e)
    {
        if (this.lastRecordsHiddenUp.Value == "")
        {
            this.AddLastRecordsPic();
        }
        else
        {
            this.UpdateLastRecordsPic();
            this.lastRecordsHiddenUp.Value = "";
        }
    }

    protected void disableLastRecordsButton_Click(object sender, EventArgs e)
    {
        this.DisableLastRecordsPic();
    }

    protected void enableLastRecordsButton_Click(object sender, EventArgs e)
    {
        this.EnableLastRecordsPic();
    }

    protected void cancelAddLastRecordsButton_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    protected void updateLastRecordsButton_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(17))
        {
            this.ClearLastRecordsPic();
            return;
        }

        LastRecordPic p = (LastRecordPic)this.Master._PapaDal.Get("lastRecordsPic",
        this.removeUpdatelastRecordsSelector.SelectedValue.Remove(0, 1));
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearLastRecordsPic();
            return;
        }

        this.lastRecordsHiddenUp.Value = p.PicID;
        this.UpdateLastRecordsPicInit();
        this.DivSwitcher(4);
    }

    protected void removeLastRecordsButton_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(17))
        {
            this.ClearLastRecordsPic();
            return;
        }

        LastRecordPic p = (LastRecordPic)this.Master._PapaDal.Get("lastRecordsPic",
        this.removeUpdatelastRecordsSelector.SelectedValue.Remove(0, 1));
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearLastRecordsPic();
            return;
        }

        this.lastRecordsHiddenRe.Value = p.PicID;
        this.Notify(this.Master._Notifier.Notify(5, "Red", p.PicName));
    }

    protected void cancelLastRecordsButton_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    protected void addWillComePicButton_Click(object sender, EventArgs e)
    {
        if (this.willComePicHiddenUp.Value == "")
        {
            this.AddWillComePic();
        }
        else
        {
            this.UpdateWillComePic();
            this.willComePicHiddenUp.Value = "";
        }
    }

    protected void disableWillComePicButton_Click(object sender, EventArgs e)
    {
        this.DisableWillComePic();
    }

    protected void enableWillComePicButton_Click(object sender, EventArgs e)
    {
        this.EnableWillComePic();
    }

    protected void cancelAddWillComePicButton_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    protected void updateWillComePicButton_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(18))
        {
            this.ClearWillComePic();
            return;
        }

        WillComePic p = (WillComePic)this.Master._PapaDal.Get("willComePic",
        this.removeUpdateWillComePicSelector.SelectedValue.Remove(0, 1));
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearWillComePic();
            return;
        }

        this.willComePicHiddenUp.Value = p.PicID;
        this.UpdateWillComePicInit();
        this.DivSwitcher(6);
    }

    protected void removeWillComePicButton_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(18))
        {
            this.ClearWillComePic();
            return;
        }

        WillComePic p = (WillComePic)this.Master._PapaDal.Get("willComePic",
        this.removeUpdateWillComePicSelector.SelectedValue.Remove(0, 1));
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearWillComePic();
            return;
        }

        this.willComePicHiddenRe.Value = p.PicID;
        this.Notify(this.Master._Notifier.Notify(5, "Red", p.PicName));
    }

    protected void cancelWillComePicButton_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    protected void addLastNewsButton_Click(object sender, EventArgs e)
    {
        if (this.lastNewsHiddenUp.Value == "")
        {
            this.AddLastNews();
        }
        else
        {
            this.UpdateLastNews();
            this.lastNewsHiddenUp.Value = "";
        }
    }

    protected void cancelAddLastNewsButton_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    protected void updateLastNewsButton_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(19))
        {
            this.ClearLastNews();
            return;
        }

        LastNew p = (LastNew)this.Master._PapaDal.Get("lastNews",
        this.removeUpdateLastNewsSelector.SelectedValue.Remove(0, 1));
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearLastNews();
            return;
        }

        this.lastNewsHiddenUp.Value = p.NewsID;
        this.UpdateLastNewsInit();
        this.DivSwitcher(8);
    }

    protected void removeLastNewsButton_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(19))
        {
            this.ClearLastNews();
            return;
        }

        LastNew p = (LastNew)this.Master._PapaDal.Get("lastNews",
        this.removeUpdateLastNewsSelector.SelectedValue.Remove(0, 1));
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearLastNews();
            return;
        }

        this.lastNewsHiddenRe.Value = p.NewsID;
        this.Notify(this.Master._Notifier.Notify(5, "Red", p.NewsID));
    }

    protected void cancelLastNewsButton_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    protected void addContactAndAboutButton_Click(object sender, EventArgs e)
    {
        this.UpdateContactAndAbout();
    }

    protected void cancelContactAndAboutButton_Click(object sender, EventArgs e)
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
                if (this.homeSelector.SelectedIndex == 0)
                {
                    this.Master._Logger.Error(new AdminException(". this.homeSelector.SelectedIndex == 0"),
                    MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(1, "Red", ""));
                    return false;
                }
                break;
            case 2:
                if (this.homeHidden.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.homeHidden.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
                    return false;
                }
                break;
            case 3:
                if (this.headerPicPlace.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.headerPicPlace.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(19, "Red", ""));
                    return false;
                }

                int h = -1;
                if (!int.TryParse(this.headerPicPlace.Text, out h))
                {
                    this.Master._Logger.Error(new AdminException
                        (". (!int.TryParse(this.headerPicPlace.Text, out h)"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(20, "Red", ""));
                    return false;
                }

                if (h < 0)
                {
                    this.Master._Logger.Error(new AdminException
                    (". h < 0"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(20, "Red", ""));
                    return false;
                }
                break;
            case 4:
                if (this.lastRecordsColorPicUpload.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.lastRecordsColorPicUpload.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(37, "Red", "Color Last Record"));
                    return false;
                }

                if (this.lastRecordsBWPicUpload.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.lastRecordsBWPicUpload.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(37, "Red", "Black And White Last Record"));
                    return false;
                }
                break;
            case 5:
                if (this.willComeColorPicUpload.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.willComeColorPicUpload.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(37, "Red", "Color Last Record"));
                    return false;
                }

                if (this.willComeBWPicUpload.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.willComeBWPicUpload.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(37, "Red", "Black And White Last Record"));
                    return false;
                }
                break;
            case 6:
                if (this.lastNewsPlace.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.lastNewsPlace.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(58, "Red", ""));
                    return false;
                }

                int i = -1;
                if (!int.TryParse(this.lastNewsPlace.Text, out i))
                {
                    this.Master._Logger.Error(new AdminException
                        (". (!int.TryParse(this.lastNewsPlace.Text, out i))"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(20, "Red", ""));
                    return false;
                }
                if (this.lastNewsTextHe.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.lastNewsTextHe.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(40, "Red", "Last News"));
                    return false;
                }

                if (this.lastNewsTextEn.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.lastNewsTextEn.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(41, "Red", "Last News"));
                    return false;
                }
                break;
            case 7:
                if (this.homeContactTextHe.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.homeContactTextHe.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
                    return false;
                }

                if (this.homeContactTextEn.Text == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.homeContactTextEn.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
                    return false;
                }
                break;
            case 8:
                if (this.headerPicHiddenRe.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                         (". this.headerPicHiddenRe.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
                    return false;
                }
                break;
            case 9:
                if (this.headerPicHiddenUp.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                         (". this.headerPicHiddenUp.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
                    return false;
                }
                break;
            case 10:
                if (this.lastRecordsHiddenRe.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.lastRecordsHiddenRe.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
                    return false;
                }
                break;
            case 11:
                if (this.lastRecordsHiddenUp.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                       (". this.lastRecordsHiddenUp.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
                    return false;
                }
                break;
            case 12:
                if (this.willComePicHiddenRe.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                       (". this.willComePicHiddenRe.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
                    return false;
                }
                break;
            case 13:
                if (this.willComePicHiddenUp.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                       (". this.willComePicHiddenUp.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
                    return false;
                }
                break;
            case 14:
                if (this.lastNewsHiddenRe.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                        (". this.lastNewsHiddenRe.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
                    return false;
                }
                break;
            case 15:
                if (this.lastNewsHiddenUp.Value == "")
                {
                    this.Master._Logger.Error(new AdminException
                         (". this.lastNewsHiddenUp.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
                    return false;
                }
                break;
            case 16:
                if (this.removeUpdateHeaderPicSelector.SelectedValue == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.removeUpdateHeaderPicSelector.SelectedValue == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(57, "Red", "Picture"));
                    return false;
                }
                break;
            case 17:
                if (this.removeUpdatelastRecordsSelector.SelectedValue == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.removeUpdatelastRecordsSelector.SelectedValue == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(57, "Red", "Picture"));
                    return false;
                }
                break;
            case 18:
                if (this.removeUpdateWillComePicSelector.SelectedValue == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.removeUpdateWillComePicSelector.SelectedValue == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(57, "Red", "Picture"));
                    return false;
                }
                break;
            case 19:
                if (this.removeUpdateLastNewsSelector.SelectedValue == "")
                {
                    this.Master._Logger.Error(new AdminException
                    (". this.removeUpdateLastNewsSelector.SelectedValue == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(57, "Red", "News"));
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
                foreach (ListItem l in this.homeSelector.Items)
                {
                    l.Selected = false;
                }
                break;
            case 2:
                this.headerPicLastUpdateLastUpdateLabel.Text = "";
                this.headerPicStatusLabel.Text = "";
                this.headerPicPlace.Text = "";
                this.headerPicUploadPic.Src = "";
                break;
            case 3:
                foreach (ListItem l in this.removeUpdateHeaderPicSelector.Items)
                {
                    l.Selected = false;
                }
                break;
            case 4:
                this.headerPicHiddenRe.Value = "";
                this.headerPicHiddenUp.Value = "";
                this.lastRecordsHiddenRe.Value = "";
                this.lastRecordsHiddenUp.Value = "";
                this.willComePicHiddenRe.Value = "";
                this.willComePicHiddenUp.Value = "";
                this.lastNewsHiddenRe.Value = "";
                this.lastNewsHiddenUp.Value = "";
                break;
            case 5:
                foreach (ListItem l in this.lastRecordsPicPlaceSelector.Items)
                {
                    l.Selected = false;
                }
                this.lastRecordsPicLastUpdateLabel.Text = "";
                this.lastRecordsPicStatusLabel.Text = "";
                this.lastRecordsColorPicUploadPic.Src = "";
                this.lastRecordsBWPicUploadPic.Src = "";
                break;
            case 6:
                foreach (ListItem l in this.removeUpdatelastRecordsSelector.Items)
                {
                    l.Selected = false;
                }
                break;
            case 7:
                foreach (ListItem l in this.willComePicPlaceSelector.Items)
                {
                    l.Selected = false;
                }
                this.willComeLastUpdateLabel.Text = "";
                this.willComeStatusLabel.Text = "";
                this.willComeColorPicUploadPic.Src = "";
                this.willComeBWPicUploadPic.Src = "";
                break;
            case 8:
                foreach (ListItem l in this.removeUpdateWillComePicSelector.Items)
                {
                    l.Selected = false;
                }
                break;
            case 9:
                this.lastNewsLastUpdateLabel.Text = "";
                this.lastNewsPlace.Text = "";
                this.lastNewsTextHe.Text = "";
                this.lastNewsTextEn.Text = "";
                break;
            case 10:
                foreach (ListItem l in this.removeUpdateLastNewsSelector.Items)
                {
                    l.Selected = false;
                }
                break;
            case 11:
                this.homeContactAndAboutLastUpdateLabel.Text = "";
                this.homeContactTextHe.Text = "";
                this.homeContactTextEn.Text = "";
                this.homeAboutTextHe.Text = "";
                this.homeAboutTextEn.Text = "";
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

        this.ShowContactAndAboutUpdateInfo(false);
        this.ShowHeaderPicUpdateInfo(false);
        this.ShowLastNewsUpdateInfo(false);
        this.ShowLastRecordsPicUpdateInfo(false);
        this.ShowWillComePicUpdateInfo(false);

        this.mainHome.Visible = false;
        this.addHeaderPic.Visible = false;
        this.removeUpdateHeaderPic.Visible = false;
        this.addLastRecordsPic.Visible = false;
        this.removeUpdateLastRecordsPic.Visible = false;
        this.addWillComePic.Visible = false;
        this.removeUpdateWillComePic.Visible = false;
        this.addLastNews.Visible = false;
        this.removeUpdateLastNews.Visible = false;
        this.addContactAndAbout.Visible = false;
        this.homeNotify.Visible = false;

        this.mainHome.Attributes["class"] = "mailNo";
        this.addHeaderPic.Attributes["class"] = "mailNo";
        this.removeUpdateHeaderPic.Attributes["class"] = "mailNo";
        this.addLastRecordsPic.Attributes["class"] = "mailNo";
        this.removeUpdateLastRecordsPic.Attributes["class"] = "mailNo";
        this.addWillComePic.Attributes["class"] = "mailNo";
        this.removeUpdateWillComePic.Attributes["class"] = "mailNo";
        this.addLastNews.Attributes["class"] = "mailNo";
        this.removeUpdateLastNews.Attributes["class"] = "mailNo";
        this.addContactAndAbout.Attributes["class"] = "mailNo";
        this.homeNotify.Attributes["class"] = "mailNo";

        switch (action)
        {
            case 1:
                this.mainHome.Visible = true;
                this.mainHome.Attributes["class"] = "mailYes";
                break;
            case 2:
                this.addHeaderPic.Visible = true;
                this.addHeaderPic.Attributes["class"] = "mailYes";

                if (this.headerPicHiddenUp.Value == "")
                {
                    this.ShowHeaderPicFile(true);
                    this.ShowHeaderPicPicture(false);
                    this.ShowHeaderPicEnableDisable(false);
                    this.ShowHeaderPicUpdateInfo(false);
                }
                else
                {
                    this.ShowHeaderPicFile(true);
                    this.ShowHeaderPicPicture(true);
                    this.ShowHeaderPicEnableDisable(true);
                    this.ShowHeaderPicUpdateInfo(true);
                }
                break;
            case 3:
                this.removeUpdateHeaderPic.Visible = true;
                this.removeUpdateHeaderPic.Attributes["class"] = "mailYes";
                break;
            case 4:
                this.addLastRecordsPic.Visible = true;
                this.addLastRecordsPic.Attributes["class"] = "mailYes";

                if (this.lastRecordsHiddenUp.Value == "")
                {
                    this.ShowLastRecordsPicFile(true);
                    this.ShowLastRecordsPicPicture(false);
                    this.ShowLastRecordsEnableDisable(false);
                    this.ShowLastRecordsPicUpdateInfo(false);
                }
                else
                {
                    this.ShowLastRecordsPicFile(true);
                    this.ShowLastRecordsPicPicture(true);
                    this.ShowLastRecordsEnableDisable(true);
                    this.ShowLastRecordsPicUpdateInfo(true);
                }
                break;
            case 5:
                this.removeUpdateLastRecordsPic.Visible = true;
                this.removeUpdateLastRecordsPic.Attributes["class"] = "mailYes";
                break;
            case 6:
                this.addWillComePic.Visible = true;
                this.addWillComePic.Attributes["class"] = "mailYes";

                if (this.willComePicHiddenUp.Value == "")
                {
                    this.ShowWillComePicFile(true);
                    this.ShowWillComePicPicture(false);
                    this.ShowWillComePicEnableDisable(false);
                    this.ShowWillComePicUpdateInfo(false);
                }
                else
                {
                    this.ShowWillComePicFile(true);
                    this.ShowWillComePicPicture(true);
                    this.ShowWillComePicEnableDisable(true);
                    this.ShowWillComePicUpdateInfo(true);
                }
                break;
            case 7:
                this.removeUpdateWillComePic.Visible = true;
                this.removeUpdateWillComePic.Attributes["class"] = "mailYes";
                break;
            case 8:
                this.addLastNews.Visible = true;
                this.addLastNews.Attributes["class"] = "mailYes";

                if (this.lastNewsHiddenUp.Value == "")
                {
                    this.ShowLastNewsUpdateInfo(false);
                }
                else
                {
                    this.ShowLastNewsUpdateInfo(true);
                }
                break;
            case 9:
                this.removeUpdateLastNews.Visible = true;
                this.removeUpdateLastNews.Attributes["class"] = "mailYes";
                break;
            case 10:
                this.addContactAndAbout.Visible = true;
                this.addContactAndAbout.Attributes["class"] = "mailYes";
                this.ShowContactAndAboutUpdateInfo(true);

                if (this.Master._PapaDal.GetCount("homeContactAbout") > 0)
                {
                    HomeContactAbout m = (HomeContactAbout)this.Master._PapaDal.Get("homeContactAbout", "0");
                    this.homeContactTextHe.Text = m.ContactHe;
                    this.homeContactTextEn.Text = m.ContactEn;
                    this.homeAboutTextHe.Text = m.AboutHe;
                    this.homeAboutTextEn.Text = m.AboutEn;
                    this.homeContactAndAboutLastUpdateLabel.Text = m.spLastUpdate;
                }
                break;
            case 11:
                this.homeNotify.Visible = true;
                this.homeNotify.Attributes["class"] = "mailYes";
                break;
            default:
                break;
        }
    }

    private void AddHeaderPic()
    {
        if (!this.ValidateFields(3))
        {
            this.ClearHeaderPic();
            return;
        }

        if (this.headerPicUpload.Value == "")
        {
            this.Master._Logger.Error(new AdminException
                (". this.headerPicUpload.Value == \"\""), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(37, "Red", "Header"));
            this.ClearHeaderPic();
            return;
        }

        if (!this.Master._GlobalFunctions.ValidatePicEnd(this.headerPicUpload.Value))
        {
            this.Master._Logger.Error(new AdminException
            (". !this.Master._GlobalFunctions.ValidatePicEnd(this.headerPicUpload.Value)"),
            MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(14, "Red", this.headerPicUpload.Value));
            this.ClearHeaderPic();
            return;
        }

        if (!this.Master._PapaDal.CheckAvailablePlace("headerPic",
             int.Parse(this.headerPicPlace.Text)))
        {
            this.Master._Logger.Error(new AdminException
                (@". (!this.Master._PapaDal.CheckAvailablePlace(""headerPic"", 
             int.Parse(this.headerPicPlace.Text)))"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(21, "Red", this.headerPicPlace.Text));
            this.ClearHeaderPic();
            return;
        }

        string ID = this.Master._PapaDal.GetNextAvailableID("headerPic");
        string fileName = this.headerPicUpload.PostedFile.FileName;
        string fileNameToSave = "headerPic_id-" + ID + "_" + fileName;
        string fullPath = this.Master._GlobalFunctions.GetFullPath() + fileNameToSave;
        string relativePath = this.Master._GlobalFunctions.GetRelativePath() + fileNameToSave;

        try
        {
            this.headerPicUpload.PostedFile.SaveAs(fullPath);
        }
        catch (Exception r)
        {
            this.Master._Logger.Error(r, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(36, "Red", this.headerPicUpload.Value));
            this.ClearHeaderPic();
            return;
        }

        try
        {
            HeaderPic g = new HeaderPic
            {
                PicID = ID,
                Active = 2,
                PicName = fileName,
                PicFullPath = fullPath,
                PicRelativePath = relativePath,
                PicPlace = int.Parse(this.headerPicPlace.Text),
                spActive = "Disable",
                spUploadTime = TimeNow.TheTimeNow.ToShortDateString(),
                UploadTime = TimeNow.TheTimeNow,
                LastUpdate = TimeNow.TheTimeNow,
                spLastUpdate = TimeNow.TheTimeNow.ToShortDateString()
            };

            this.Master._PapaDal.Add("headerPic", g);
            this.Master._Logger.Log(new AdminException(". " + g.PicID + " Was Successfully Added"),
                            MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(15, "White", fileName));

            this.removeUpdateHeaderPicSelector.Items.Add(new ListItem(fileName, "s" + g.PicID));
        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(16, "Red", fileName));
        }
    }

    private void UpdateHeaderPicInit()
    {
        if (!this.ValidateFields(9))
        {
            this.ClearHeaderPic();
            return;
        }

        HeaderPic p = (HeaderPic)this.Master._PapaDal.Get("headerPic", this.headerPicHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearHeaderPic();
            return;
        }

        try
        {
            this.headerPicStatusLabel.Text = p.spActive;
            this.headerPicLastUpdateLastUpdateLabel.Text = p.spLastUpdate;
            this.headerPicPlace.Text = p.PicPlace.ToString();
            this.headerPicUploadPic.Src = p.PicRelativePath;
        }
        catch (Exception e)
        {
            this.ClearHeaderPic();
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
        }
    }

    private void UpdateHeaderPic()
    {
        if (!this.ValidateFields(3))
        {
            this.ClearHeaderPic();
            return;
        }

        HeaderPic g = (HeaderPic)this.Master._PapaDal.Get("headerPic", this.headerPicHiddenUp.Value);
        if (g == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". g == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearHeaderPic();
            return;
        }

        if (!this.Master._PapaDal.CheckAvailablePlaceExcept
            ("headerPic", g.PicPlace, int.Parse(this.headerPicPlace.Text)))
        {
            this.Master._Logger.Error(new AdminException
                (@". (!this.Master._PapaDal.CheckAvailablePlaceExcept
            (""headerPic"", g.PicPlace, int.Parse(this.headerPicPlace.Text)))"),
            MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(21, "Red", this.headerPicPlace.Text));
            this.ClearHeaderPic();
            return;
        }

        if (this.headerPicUpload.Value != "")
        {
            if (!this.Master._GlobalFunctions.ValidatePicEnd(this.headerPicUpload.Value))
            {
                this.Master._Logger.Error(new AdminException
                (". !this.Master._GlobalFunctions.ValidatePicEnd(this.headerPicUpload.Value)"),
                    MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(14, "Red", this.headerPicUpload.Value));
                this.ClearHeaderPic();
                return;
            }

            string fileName = this.headerPicUpload.PostedFile.FileName;
            string fileNameToSave = "headerPic_id-" + g.PicID + "_" + fileName;
            string fullPath = this.Master._GlobalFunctions.GetFullPath() + fileNameToSave;
            string relativePath = this.Master._GlobalFunctions.GetRelativePath() + fileNameToSave;

            try
            {
                if (File.Exists(g.PicFullPath))
                {
                    File.Delete(g.PicFullPath);
                }

                this.headerPicUpload.PostedFile.SaveAs(fullPath);

                g.PicName = fileName;
                g.PicFullPath = fullPath;
                g.PicRelativePath = relativePath;
            }
            catch (Exception r)
            {
                this.Master._Logger.Error(r, MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(36, "Red", this.headerPicUpload.Value));
                this.ClearHeaderPic();
                return;
            }
        }

        try
        {
            g.PicPlace = int.Parse(this.headerPicPlace.Text);
            g.LastUpdate = TimeNow.TheTimeNow;
            g.spLastUpdate = TimeNow.TheTimeNow.ToShortDateString();

            this.Master._PapaDal.Update("headerPic", g, TimeNow.TheTimeNow);
            this.Master._Logger.Log(new AdminException(". " + g.PicID +
                    " Was Successfully Updated"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(17, "White", g.PicName));

            this.removeUpdateHeaderPicSelector.Items.FindByValue("s" + g.PicID).Text = g.PicName;
        }
        catch (Exception e)
        {
            this.Master._Logger.Log(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(18, "Red", g.PicName));
        }
    }

    private void RemoveHeaderPic()
    {
        if (!this.ValidateFields(8))
        {
            this.ClearHeaderPic();
            return;
        }

        HeaderPic g = (HeaderPic)this.Master._PapaDal.Get("headerPic", this.headerPicHiddenRe.Value);
        if (g == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". g == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearHeaderPic();
            return;
        }

        try
        {
            if (File.Exists(g.PicFullPath))
            {
                File.Delete(g.PicFullPath);
            }

            this.Master._PapaDal.Remove("headerPic", this.headerPicHiddenRe.Value);
            this.Master._Logger.Log(new AdminException(". " + g.PicID + " Was Successfully Removed"),
                MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(12, "White", g.PicName));

            this.removeUpdateHeaderPicSelector.Items.Remove
            (this.removeUpdateHeaderPicSelector.Items.FindByValue
            ("s" + g.PicID));
        }
        catch (Exception e)
        {
            this.Master._Logger.Log(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(13, "Red", g.PicName));
        }
    }

    private void EnableHeaderPic()
    {
        if (!this.ValidateFields(9))
        {
            this.ClearHeaderPic();
            return;
        }

        HeaderPic p = (HeaderPic)this.Master._PapaDal.Get("headerPic", this.headerPicHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearHeaderPic();
            return;
        }

        if (p.Active == 1)
        {
            this.Master._Logger.Warn(new AdminException
            (". " + p.PicID + " Is Already Disabled"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(9, "Red", p.PicName));
            this.ClearHeaderPic();
            return;
        }

        try
        {
            this.Master._PapaDal.Enable("headerPic", p.PicID);
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

    private void DisableHeaderPic()
    {
        if (!this.ValidateFields(9))
        {
            this.ClearHeaderPic();
            return;
        }

        HeaderPic p = (HeaderPic)this.Master._PapaDal.Get("headerPic", this.headerPicHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearHeaderPic();
            return;
        }

        if (p.Active == 2)
        {
            this.Master._Logger.Warn(new AdminException
            (". " + p.PicID + " Is Already Disabled"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(8, "Red", p.PicName));
            this.ClearHeaderPic();
            return;
        }

        try
        {
            this.Master._PapaDal.Disable("headerPic", p.PicID);
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

    private void AddLastRecordsPic()
    {
        if (!this.ValidateFields(4))
        {
            this.ClearLastRecordsPic();
            return;
        }

        if (!this.Master._PapaDal.CheckAvailablePlace("lastRecordsPic",
            int.Parse(this.lastRecordsPicPlaceSelector.SelectedValue)))
        {
            this.Master._Logger.Error(new AdminException
                (@". (!this.Master._PapaDal.CheckAvailablePlace(""lastRecordsPic"",
            int.Parse(this.lastRecordsPicPlaceSelector.SelectedValue)))"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(21, "Red", this.lastRecordsPicPlaceSelector.SelectedValue));
            this.ClearLastRecordsPic();
            return;
        }

        if (!this.Master._GlobalFunctions.ValidatePicEnd
            (this.lastRecordsColorPicUpload.Value))
        {
            this.Master._Logger.Error(new AdminException
            (@". (!this.Master._GlobalFunctions.ValidatePicEnd
            (this.lastRecordsColorPicUpload.Value))"),
            MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(14, "Red", this.lastRecordsColorPicUpload.Value));
            this.ClearLastRecordsPic();
            return;
        }

        if (!this.Master._GlobalFunctions.ValidatePicEnd(this.lastRecordsBWPicUpload.Value))
        {
            this.Master._Logger.Error(new AdminException
            (@". !this.Master._GlobalFunctions.ValidatePicEnd
            (this.lastRecordsBWPicUpload.Value)"),
            MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(14, "Red", this.lastRecordsBWPicUpload.Value));
            this.ClearLastRecordsPic();
            return;
        }

        string ID = this.Master._PapaDal.GetNextAvailableID("lastRecordsPic");
        string fileColorName = this.lastRecordsColorPicUpload.PostedFile.FileName;
        string fileColorNameToSave = "lastRecordsColorPic_id-" + ID + "_" + fileColorName;
        string fullColorPath = this.Master._GlobalFunctions.GetFullPath() + fileColorNameToSave;
        string relativeColorPath = this.Master._GlobalFunctions.GetRelativePath() + fileColorNameToSave;
        string fileBWName = this.lastRecordsBWPicUpload.PostedFile.FileName;
        string fileBWNameToSave = "lastRecordsBWPic_id-" + ID + "_" + fileBWName;
        string fullBWPath = this.Master._GlobalFunctions.GetFullPath() + fileBWNameToSave;
        string relativeBWPath = this.Master._GlobalFunctions.GetRelativePath() + fileBWNameToSave;

        try
        {
            this.lastRecordsColorPicUpload.PostedFile.SaveAs(fullColorPath);
        }
        catch (Exception r)
        {
            this.Master._Logger.Error(r, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(36, "Red", this.lastRecordsColorPicUpload.Value));
            this.ClearLastRecordsPic();
            return;
        }

        try
        {
            this.lastRecordsBWPicUpload.PostedFile.SaveAs(fullBWPath);
        }
        catch (Exception r)
        {
            this.Master._Logger.Error(r, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(36, "Red", this.lastRecordsBWPicUpload.Value));
            this.ClearLastRecordsPic();
            return;
        }

        try
        {
            LastRecordPic g = new LastRecordPic
            {
                PicID = ID,
                Active = 2,
                PicName = fileColorName,
                PicBWFullPath = fullBWPath,
                PicBWRelativePath = relativeBWPath,
                PicColorFullPath = fullColorPath,
                PicColorRelativePath = relativeColorPath,
                PicPlace = int.Parse(this.lastRecordsPicPlaceSelector.SelectedValue),
                spActive = "Disable",
                spUploadTime = TimeNow.TheTimeNow.ToShortDateString(),
                UploadTime = TimeNow.TheTimeNow,
                LastUpdate = TimeNow.TheTimeNow,
                spLastUpdate = TimeNow.TheTimeNow.ToShortDateString()
            };

            this.Master._PapaDal.Add("lastRecordsPic", g);
            this.Master._Logger.Log(new AdminException(". " + g.PicID + " Was Successfully Added"),
                            MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(15, "White", g.PicName));

            this.removeUpdatelastRecordsSelector.Items.Add(new ListItem(g.PicName, "s" + g.PicID));
        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(16, "Red", fileColorName + " And " + fileBWName));
        }
    }

    private void UpdateLastRecordsPicInit()
    {
        if (!this.ValidateFields(11))
        {
            this.ClearLastRecordsPic();
            return;
        }

        LastRecordPic p = (LastRecordPic)this.Master._PapaDal.Get("lastRecordsPic", this.lastRecordsHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearLastRecordsPic();
            return;
        }

        try
        {
            this.lastRecordsPicLastUpdateLabel.Text = p.spLastUpdate;
            this.lastRecordsPicStatusLabel.Text = p.spActive;
            this.lastRecordsPicPlaceSelector.SelectedValue = p.PicPlace.ToString();
            this.lastRecordsColorPicUploadPic.Src = p.PicColorRelativePath;
            this.lastRecordsBWPicUploadPic.Src = p.PicBWRelativePath;
        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
        }
    }

    private void UpdateLastRecordsPic()
    {
        if (!this.ValidateFields(11))
        {
            this.ClearLastRecordsPic();
            return;
        }

        LastRecordPic g = (LastRecordPic)this.Master._PapaDal.Get("lastRecordsPic", this.lastRecordsHiddenUp.Value);
        if (g == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". g == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearLastRecordsPic();
            return;
        }

        if (!this.Master._PapaDal.CheckAvailablePlaceExcept("lastRecordsPic",
            g.PicPlace, int.Parse(this.lastRecordsPicPlaceSelector.SelectedValue)))
        {
            this.Master._Logger.Error(new AdminException
                (@". (!this.Master._PapaDal.CheckAvailablePlaceExcept(""lastRecordsPic"",
            g.PicPlace, int.Parse(this.lastRecordsPicPlaceSelector.SelectedValue)))"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(21, "Red", this.lastRecordsPicPlaceSelector.SelectedValue));
            this.ClearLastRecordsPic();
            return;
        }

        if (this.lastRecordsColorPicUpload.Value != "")
        {
            if (!this.Master._GlobalFunctions.ValidatePicEnd(this.lastRecordsColorPicUpload.Value))
            {
                this.Master._Logger.Error(new AdminException
                (@". !this.Master._GlobalFunctions.ValidatePicEnd
                (this.lastRecordsColorPicUpload.Value)"), MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(14, "Red", g.PicName));
                this.ClearLastRecordsPic();
                return;
            }

            string fileColorName = this.lastRecordsColorPicUpload.PostedFile.FileName;
            string fileColorNameToSave = "lastRecordsColorPic_id-" + g.PicID + "_" + fileColorName;
            string fullColorPath = this.Master._GlobalFunctions.GetFullPath() + fileColorNameToSave;
            string relativeColorPath = this.Master._GlobalFunctions.GetRelativePath() + fileColorNameToSave;

            try
            {
                if (File.Exists(g.PicColorFullPath))
                {
                    File.Delete(g.PicColorFullPath);
                }

                this.lastRecordsColorPicUpload.PostedFile.SaveAs(fullColorPath);

                g.PicName = fileColorName;
                g.PicColorFullPath = fullColorPath;
                g.PicColorRelativePath = relativeColorPath;
            }
            catch (Exception r)
            {
                this.Master._Logger.Error(r, MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(36, "Red", this.lastRecordsColorPicUpload.Value));
                this.ClearLastRecordsPic();
                return;
            }
        }

        if (this.lastRecordsBWPicUpload.Value != "")
        {
            if (!this.Master._GlobalFunctions.ValidatePicEnd(this.lastRecordsBWPicUpload.Value))
            {
                this.Master._Logger.Error(new AdminException
                (@". !this.Master._GlobalFunctions.ValidatePicEnd
                        (this.lastRecordsBWPicUpload.Value)"),
                MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(14, "Red", this.lastRecordsBWPicUpload.Value));
                this.ClearLastRecordsPic();
                return;
            }

            string fileBWName = this.lastRecordsBWPicUpload.PostedFile.FileName;
            string fileBWNameToSave = "lastRecordsBWPic_id-" + ID + "_" + fileBWName;
            string fullBWPath = this.Master._GlobalFunctions.GetFullPath() + fileBWNameToSave;
            string relativeBWPath = this.Master._GlobalFunctions.GetRelativePath() + fileBWNameToSave;

            try
            {

                if (File.Exists(g.PicBWFullPath))
                {
                    File.Delete(g.PicBWFullPath);
                }

                this.lastRecordsBWPicUpload.PostedFile.SaveAs(fullBWPath);

                g.PicBWFullPath = fullBWPath;
                g.PicBWRelativePath = relativeBWPath;
            }
            catch (Exception r)
            {
                this.Master._Logger.Error(r, MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(36, "Red", this.lastRecordsBWPicUpload.Value));
                this.ClearLastRecordsPic();
                return;
            }
        }

        try
        {
            g.PicPlace = int.Parse(this.lastRecordsPicPlaceSelector.SelectedValue);
            g.LastUpdate = TimeNow.TheTimeNow;
            g.spLastUpdate = TimeNow.TheTimeNow.ToShortDateString();

            this.Master._PapaDal.Update("lastRecordsPic", g, TimeNow.TheTimeNow);
            this.Master._Logger.Log(new AdminException(". " + g.PicID +
                    " Was Successfully Updated"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(17, "White", g.PicName));

            this.removeUpdatelastRecordsSelector.Items.FindByValue("s" + g.PicID).Text = g.PicName;
        }
        catch (Exception e)
        {
            this.Master._Logger.Log(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(18, "Red", g.PicName));
        }
    }

    private void RemoveLastRecordsPic()
    {
        if (!this.ValidateFields(10))
        {
            this.ClearLastRecordsPic();
            return;
        }

        LastRecordPic g = (LastRecordPic)this.Master._PapaDal.Get("lastRecordsPic", this.lastRecordsHiddenRe.Value);
        if (g == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". g == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearFields(3);
            this.ClearFields(4);
            return;
        }

        try
        {

            if (File.Exists(g.PicColorFullPath))
            {
                File.Delete(g.PicColorFullPath);
            }

            if (File.Exists(g.PicBWFullPath))
            {
                File.Delete(g.PicBWFullPath);
            }

            this.Master._PapaDal.Remove("lastRecordsPic", g.PicID);
            this.Master._Logger.Log(new AdminException(". " + g.PicID + " Was Successfully Removed"),
                MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(12, "White", g.PicName));

            this.removeUpdatelastRecordsSelector.Items.Remove
            (this.removeUpdatelastRecordsSelector.Items.FindByValue
            ("s" + g.PicID));
        }
        catch (Exception e)
        {
            this.Master._Logger.Log(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(13, "Red", g.PicName));
        }
    }


    private void EnableLastRecordsPic()
    {
        if (!this.ValidateFields(11))
        {
            this.ClearLastRecordsPic();
            return;
        }

        LastRecordPic p = (LastRecordPic)this.Master._PapaDal.Get("lastRecordsPic", this.lastRecordsHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearLastRecordsPic();
            return;
        }

        if (p.Active == 1)
        {
            this.Master._Logger.Warn(new AdminException
            (". " + p.PicID + " Is Already Disabled"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(9, "Red", p.PicName));
            this.ClearLastRecordsPic();
            return;
        }

        if (!this.Master._PapaDal.CheckPlacesStatus("lastRecordsPic"))
        {
            this.Master._Logger.Error(new AdminException
            (@". (!this.Master._PapaDal.CheckPlacesStatus(""lastRecordsPic""))"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(53, "Red", ""));
            this.ClearLastRecordsPic();
            return;
        }

        try
        {
            this.Master._PapaDal.Enable("lastRecordsPic", p.PicID);
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

    private void DisableLastRecordsPic()
    {
        if (!this.ValidateFields(11))
        {
            this.ClearLastRecordsPic();
            return;
        }

        LastRecordPic p = (LastRecordPic)this.Master._PapaDal.Get("lastRecordsPic", this.lastRecordsHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearLastRecordsPic();
            return;
        }

        if (p.Active == 2)
        {
            this.Master._Logger.Warn(new AdminException
            (". " + p.PicID + " Is Already Disabled"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(8, "Red", p.PicName));
            this.ClearLastRecordsPic();
            return;
        }

        try
        {
            this.Master._PapaDal.Disable("lastRecordsPic", p.PicID);
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

    private void AddWillComePic()
    {
        if (!this.ValidateFields(5))
        {
            this.ClearWillComePic();
            return;
        }

        if (!this.Master._PapaDal.CheckAvailablePlace("willComePic",
                int.Parse(this.willComePicPlaceSelector.SelectedValue)))
        {
            this.Master._Logger.Error(new AdminException
                (@". (!this.Master._PapaDal.CheckAvailablePlace(""willComePic"",
                int.Parse(this.willComePicPlaceSelector.SelectedValue)))"),
            MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(21, "Red", this.willComePicPlaceSelector.SelectedValue));
            this.ClearWillComePic();
            return;
        }

        if (!this.Master._GlobalFunctions.ValidatePicEnd(this.willComeColorPicUpload.Value))
        {
            this.Master._Logger.Error(new AdminException
            (@". !this.Master._GlobalFunctions.ValidatePicEnd
            (this.willComeColorPicUpload.Value)"),
            MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(14, "Red", this.willComeColorPicUpload.Value));
            this.ClearWillComePic();
            return;
        }

        if (!this.Master._GlobalFunctions.ValidatePicEnd(this.willComeBWPicUpload.Value))
        {
            this.Master._Logger.Error(new AdminException
            (@". !this.Master._GlobalFunctions.ValidatePicEnd
        `   (this.willComeBWPicUpload.Value)"),
            MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(14, "Red", this.willComeBWPicUpload.Value));
            this.ClearWillComePic();
            return;
        }

        string ID = this.Master._PapaDal.GetNextAvailableID("willComePic");
        string fileColorName = this.willComeColorPicUpload.PostedFile.FileName;
        string fileColorNameToSave = "willComePicColorPic_id-" + ID + "_" + fileColorName;
        string fullColorPath = this.Master._GlobalFunctions.GetFullPath() + fileColorNameToSave;
        string relativeColorPath = this.Master._GlobalFunctions.GetRelativePath() + fileColorNameToSave;
        string fileBWName = this.willComeBWPicUpload.PostedFile.FileName;
        string fileBWNameToSave = "willComePicBWPic_id-" + ID + "_" + fileBWName;
        string fullBWPath = this.Master._GlobalFunctions.GetFullPath() + fileBWNameToSave;
        string relativeBWPath = this.Master._GlobalFunctions.GetRelativePath() + fileBWNameToSave;

        try
        {
            this.willComeColorPicUpload.PostedFile.SaveAs(fullColorPath);
        }
        catch (Exception r)
        {
            this.Master._Logger.Error(r, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(36, "Red", this.willComeColorPicUpload.Value));
            this.ClearWillComePic();
            return;
        }

        try
        {
            this.willComeBWPicUpload.PostedFile.SaveAs(fullBWPath);
        }
        catch (Exception r)
        {
            this.Master._Logger.Error(r, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(36, "Red", this.willComeBWPicUpload.Value));
            this.ClearWillComePic();
            return;
        }

        try
        {
            WillComePic g = new WillComePic
            {
                PicID = ID,
                Active = 2,
                PicName = fileColorName,
                PicBWFullPath = fullBWPath,
                PicBWRelativePath = relativeBWPath,
                PicColorFullPath = fullColorPath,
                PicColorRelativePath = relativeColorPath,
                PicPlace = int.Parse(this.willComePicPlaceSelector.SelectedValue),
                spActive = "Disable",
                spUploadTime = TimeNow.TheTimeNow.ToShortDateString(),
                UploadTime = TimeNow.TheTimeNow,
                LastUpdate = TimeNow.TheTimeNow,
                spLastUpdate = TimeNow.TheTimeNow.ToShortDateString()
            };

            this.Master._PapaDal.Add("willComePic", g);
            this.Master._Logger.Log(new AdminException(". " + g.PicID + " Was Successfully Added"),
                            MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(15, "White", g.PicName));

            this.removeUpdateWillComePicSelector.Items.Add(new ListItem(g.PicName, "s" + g.PicID));
        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(16, "Red", fileColorName + " And " + fileBWName));
        }
    }

    private void UpdateWillComePicInit()
    {
        if (!this.ValidateFields(13))
        {
            this.ClearWillComePic();
            return;
        }

        WillComePic p = (WillComePic)this.Master._PapaDal.Get("willComePic", this.willComePicHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearWillComePic();
            return;
        }

        try
        {
            this.willComeLastUpdateLabel.Text = p.spLastUpdate;
            this.willComeStatusLabel.Text = p.spActive;
            this.willComePicPlaceSelector.SelectedValue = p.PicPlace.ToString();
            this.willComeColorPicUploadPic.Src = p.PicColorRelativePath;
            this.willComeBWPicUploadPic.Src = p.PicBWRelativePath;
            this.ShowWillComePicPicture(true);
        }
        catch (Exception e)
        {
            this.ClearWillComePic();
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
        }
    }

    private void UpdateWillComePic()
    {
        if (!this.ValidateFields(13))
        {
            this.ClearWillComePic();
            return;
        }

        WillComePic g = (WillComePic)this.Master._PapaDal.Get("willComePic", this.willComePicHiddenUp.Value);
        if (g == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". g == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearWillComePic();
            return;
        }

        if (!this.Master._PapaDal.CheckAvailablePlaceExcept("willComePic",
            g.PicPlace, int.Parse(this.willComePicPlaceSelector.SelectedValue)))
        {
            this.Master._Logger.Error(new AdminException
                (@". (!this.Master._PapaDal.CheckAvailablePlaceExcept(""willComePic"",
            g.PicPlace, int.Parse(this.removeUpdateWillComePicSelector.SelectedValue)))"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(21, "Red", this.willComePicPlaceSelector.SelectedValue));
            this.ClearWillComePic();
            return;
        }


        if (this.willComeColorPicUpload.Value != "")
        {
            if (!this.Master._GlobalFunctions.ValidatePicEnd(this.willComeColorPicUpload.Value))
            {
                this.Master._Logger.Error(new AdminException
                (@". !this.Master._GlobalFunctions.ValidatePicEnd
                (this.willComeColorPicUpload.Value)"),
                MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(14, "Red", this.willComeColorPicUpload.Value));
                this.ClearWillComePic();
                return;
            }

            string fileColorName = this.willComeColorPicUpload.PostedFile.FileName;
            string fileColorNameToSave = "willComePicColorPic_id-" + ID + "_" + fileColorName;
            string fullColorPath = this.Master._GlobalFunctions.GetFullPath() + fileColorNameToSave;
            string relativeColorPath = this.Master._GlobalFunctions.GetRelativePath() + fileColorNameToSave;

            try
            {
                if (File.Exists(g.PicColorFullPath))
                {
                    File.Delete(g.PicColorFullPath);
                }
                this.willComeColorPicUpload.PostedFile.SaveAs(fullColorPath);

                g.PicName = fileColorName;
                g.PicColorFullPath = fullColorPath;
                g.PicColorRelativePath = relativeColorPath;
            }
            catch (Exception r)
            {
                this.Master._Logger.Error(r, MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(36, "Red", this.willComeColorPicUpload.Value));
                this.ClearWillComePic();
                return;
            }
        }

        if (this.willComeBWPicUpload.Value != "")
        {
            if (!this.Master._GlobalFunctions.ValidatePicEnd(this.willComeBWPicUpload.Value))
            {
                this.Master._Logger.Error(new AdminException
                (@". !this.Master._GlobalFunctions.ValidatePicEnd
                (this.willComeBWPicUpload.Value)"),
                MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(14, "Red", this.willComeBWPicUpload.Value));
                this.ClearWillComePic();
                return;
            }

            string fileBWName = this.willComeBWPicUpload.PostedFile.FileName;
            string fileBWNameToSave = "willComePicBWPic_id-" + ID + "_" + fileBWName;
            string fullBWPath = this.Master._GlobalFunctions.GetFullPath() + fileBWNameToSave;
            string relativeBWPath = this.Master._GlobalFunctions.GetRelativePath() + fileBWNameToSave;

            try
            {
                if (File.Exists(g.PicBWFullPath))
                {
                    File.Delete(g.PicBWFullPath);
                }
                this.willComeBWPicUpload.PostedFile.SaveAs(fullBWPath);

                g.PicBWFullPath = fullBWPath;
                g.PicBWRelativePath = relativeBWPath;
            }
            catch (Exception r)
            {
                this.Master._Logger.Error(r, MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(36, "Red", this.willComeBWPicUpload.Value));
                this.ClearWillComePic();
                return;
            }
        }

        try
        {
            g.PicPlace = int.Parse(this.willComePicPlaceSelector.SelectedValue);
            g.LastUpdate = TimeNow.TheTimeNow;
            g.spLastUpdate = TimeNow.TheTimeNow.ToShortDateString();

            this.Master._PapaDal.Update("willComePic", g, TimeNow.TheTimeNow);
            this.Master._Logger.Log(new AdminException(". " + g.PicID +
                    " Was Successfully Updated"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(17, "White", g.PicName));

            this.removeUpdateWillComePicSelector.Items.FindByValue("s" + g.PicID).Text = g.PicName;
        }
        catch (Exception e)
        {
            this.Master._Logger.Log(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(18, "Red", g.PicName));
        }
    }

    private void RemoveWillComePic()
    {
        if (!this.ValidateFields(12))
        {
            this.ClearWillComePic();
            return;
        }

        WillComePic g = (WillComePic)this.Master._PapaDal.Get("willComePic", this.willComePicHiddenRe.Value);
        if (g == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". g == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearWillComePic();
            return;
        }

        try
        {
            if (File.Exists(g.PicBWFullPath))
            {
                File.Delete(g.PicBWFullPath);
            }

            if (File.Exists(g.PicColorFullPath))
            {
                File.Delete(g.PicColorFullPath);
            }

            this.Master._PapaDal.Remove("willComePic", this.willComePicHiddenRe.Value);
            this.Master._Logger.Log(new AdminException(". " + g.PicID + " Was Successfully Removed"),
                MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(12, "White", g.PicName));

            this.removeUpdateWillComePicSelector.Items.Remove
            (this.removeUpdateWillComePicSelector.Items.FindByValue
            ("s" + g.PicID));
        }
        catch (Exception e)
        {
            this.Master._Logger.Log(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(13, "Red", g.PicName));
        }
    }


    private void EnableWillComePic()
    {
        if (!this.ValidateFields(13))
        {
            this.ClearWillComePic();
            return;
        }

        WillComePic p = (WillComePic)this.Master._PapaDal.Get("willComePic", this.willComePicHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearWillComePic();
            return;
        }

        if (p.Active == 1)
        {
            this.Master._Logger.Warn(new AdminException
            (". " + p.PicID + " Is Already Disabled"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(9, "Red", p.PicName));
            this.ClearWillComePic();
            return;
        }

        if (!this.Master._PapaDal.CheckPlacesStatus("willComePic"))
        {
            this.Master._Logger.Error(new AdminException
            (@". (!this.Master._PapaDal.CheckPlacesStatus(""willComePic""))"),
                    MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(51, "Red", ""));
            this.ClearWillComePic();
            return;
        }

        try
        {
            this.Master._PapaDal.Enable("willComePic", p.PicID);
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

    private void DisableWillComePic()
    {
        if (!this.ValidateFields(13))
        {
            this.ClearWillComePic();
            return;
        }

        WillComePic p = (WillComePic)this.Master._PapaDal.Get("willComePic", this.willComePicHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearWillComePic();
            return;
        }

        if (p.Active == 2)
        {
            this.Master._Logger.Warn(new AdminException
            (". " + p.PicID + " Is Already Disabled"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(8, "Red", p.PicName));
            this.ClearWillComePic();
            return;
        }

        try
        {
            this.Master._PapaDal.Disable("willComePic", p.PicID);
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

    private void AddLastNews()
    {
        if (!this.ValidateFields(6))
        {
            this.ClearLastNews();
            return;
        }

        if (!this.Master._PapaDal.CheckAvailablePlace("lastNews",
             int.Parse(this.lastNewsPlace.Text)))
        {
            this.Master._Logger.Error(new AdminException
                (@". (!this.Master._PapaDal.CheckAvailablePlace(""lastNews"",
            int.Parse(this.lastNewsPlace.Text)))"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(21, "Red", this.lastNewsPlace.Text));
            this.ClearLastNews();
            return;
        }

        string ID = this.Master._PapaDal.GetNextAvailableID("lastNews");

        string textHe = "";
        string textHeConverted = "";
        string textEnConverted = "";

        try
        {
            textHeConverted = this.Master._GlobalFunctions.ConfigLastNews("He", this.lastNewsTextHe.Text);
            textEnConverted = this.Master._GlobalFunctions.ConfigLastNews("En", this.lastNewsTextEn.Text); ;
            textHe = this.Master._GlobalFunctions.ConvertToUtf8(this.lastNewsTextHe.Text); ;

            LastNew g = new LastNew
            {
                NewsID = ID,
                NewsPlace = int.Parse(this.lastNewsPlace.Text),
                NewsHe = textHe,
                NewsEn = this.lastNewsTextEn.Text,
                NewsEnConverted = textEnConverted,
                NewsHeConverted = textHeConverted,
                spUploadTime = TimeNow.TheTimeNow.ToShortDateString(),
                UploadTime = TimeNow.TheTimeNow,
                LastUpdate = TimeNow.TheTimeNow,
                spLastUpdate = TimeNow.TheTimeNow.ToShortDateString()
            };

            this.Master._PapaDal.Add("lastNews", g);
            this.Master._Logger.Log(new AdminException(". " + g.NewsID + " Was Successfully Added"),
                            MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(15, "White", g.NewsHe));

            this.removeUpdateLastNewsSelector.Items.Add(new ListItem(g.NewsID, "s" + g.NewsID));
        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(16, "Red", textHe));
        }
    }

    private void UpdateLastNewsInit()
    {
        if (!this.ValidateFields(15))
        {
            this.ClearLastNews();
            return;
        }

        LastNew p = (LastNew)this.Master._PapaDal.Get("lastNews", this.lastNewsHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearLastNews();
            return;
        }

        try
        {
            this.lastNewsLastUpdateLabel.Text = p.spLastUpdate;
            this.lastNewsPlace.Text = p.NewsPlace.ToString();
            this.lastNewsTextHe.Text = p.NewsHe;
            this.lastNewsTextEn.Text = p.NewsEn;
        }
        catch (Exception e)
        {
            this.ClearLastNews();
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
        }
    }

    private void UpdateLastNews()
    {
        if (!this.ValidateFields(6))
        {
            this.ClearLastNews();
            return;
        }

        if (!this.ValidateFields(15))
        {
            this.ClearLastNews();
            return;
        }

        LastNew g = (LastNew)this.Master._PapaDal.Get("lastNews", this.lastNewsHiddenUp.Value);
        if (g == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". g == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearLastNews();
            return;
        }

        if (!this.Master._PapaDal.CheckAvailablePlaceExcept("lastNews",
            g.NewsPlace, int.Parse(this.lastNewsPlace.Text)))
        {
            this.Master._Logger.Error(new AdminException
                (@". (!this.Master._PapaDal.CheckAvailablePlaceExcept(""lastNews"",
            g.NewsPlace, int.Parse(this.lastNewsPlace.Text)))"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(21, "Red", this.lastNewsPlace.Text));
            this.ClearLastNews();
            return;
        }

        string ID = g.NewsID;
        string textHe = "";
        string textHeConverted = "";
        string textEnConverted = "";

        try
        {
            textHeConverted = this.Master._GlobalFunctions.ConfigLastNews("He", this.lastNewsTextHe.Text);
            textEnConverted = this.Master._GlobalFunctions.ConfigLastNews("En", this.lastNewsTextEn.Text); ;
            textHe = this.Master._GlobalFunctions.ConvertToUtf8(this.lastNewsTextHe.Text); ;

            g.NewsPlace = int.Parse(this.lastNewsPlace.Text);
            g.LastUpdate = TimeNow.TheTimeNow;
            g.spLastUpdate = TimeNow.TheTimeNow.ToShortDateString();
            g.NewsHe = textHe;
            g.NewsEn = this.lastNewsTextEn.Text;

            this.Master._PapaDal.Update("lastNews", g, TimeNow.TheTimeNow);
            this.Master._Logger.Log(new AdminException(". " + g.NewsID +
                    " Was Successfully Updated"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(17, "White", g.NewsHe));

            this.removeUpdateLastNewsSelector.Items.FindByValue("s" + g.NewsID).Text = g.NewsID;
        }
        catch (Exception e)
        {
            this.Master._Logger.Log(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(18, "Red", g.NewsHe));
        }
    }

    private void RemoveLastNews()
    {
        if (!this.ValidateFields(14))
        {
            this.ClearLastNews();
            return;
        }

        LastNew g = (LastNew)this.Master._PapaDal.Get("lastNews", this.lastNewsHiddenRe.Value);
        if (g == null)
        {
            this.Master._Logger.Error(new AdminException
                        (". g == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
            this.ClearLastNews();
            return;
        }

        try
        {
            this.Master._PapaDal.Remove("lastNews", this.lastNewsHiddenRe.Value);
            this.Master._Logger.Log(new AdminException(". " + g.NewsID + " Was Successfully Removed"),
                MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(12, "White", g.NewsHe));

            this.removeUpdateLastNewsSelector.Items.Remove
            (this.removeUpdateLastNewsSelector.Items.FindByValue
            ("s" + g.NewsID));
        }
        catch (Exception e)
        {
            this.Master._Logger.Log(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(13, "Red", g.NewsHe));
        }
    }

    private void UpdateContactAndAbout()
    {
        string aboutTextHe = "";
        string contactTextHe = "";

        HomeContactAbout m = (HomeContactAbout)this.Master._PapaDal.Get("homeContactAbout", "0");
        if (m == null)
        {
            try
            {
                aboutTextHe = this.Master._GlobalFunctions.ConvertToUtf8(this.homeAboutTextHe.Text);
                contactTextHe = this.Master._GlobalFunctions.ConvertToUtf8(this.homeContactTextHe.Text);

                HomeContactAbout t = new HomeContactAbout()
                {
                    HomeContactAboutID = this.Master._PapaDal.GetNextAvailableID("homeContactAndAbout"),
                    LastUpdate = TimeNow.TheTimeNow,
                    spLastUpdate = TimeNow.TheTimeNow.ToShortDateString(),
                    ContactHe = contactTextHe,
                    ContactEn = this.homeContactTextEn.Text,
                    AboutHe = aboutTextHe,
                    AboutEn = this.homeAboutTextEn.Text,
                };

                this.Master._PapaDal.Add("homeContactAbout", t);
                this.Master._Logger.Log(new AdminException(". Home Contact And About Was Successfully Updated")
                                        , MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(15, "White", "Home Contact And About"));
            }
            catch (Exception e)
            {
                this.Master._Logger.Log(e, MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(16, "Red", "Home Contact And About"));
            }
        }
        else
        {
            try
            {
                aboutTextHe = this.Master._GlobalFunctions.ConvertToUtf8(this.homeAboutTextHe.Text);
                contactTextHe = this.Master._GlobalFunctions.ConvertToUtf8(this.homeContactTextHe.Text);

                m.ContactHe = contactTextHe;
                m.ContactEn = this.homeContactTextEn.Text;
                m.AboutHe = aboutTextHe;
                m.AboutEn = this.homeAboutTextEn.Text;
                m.LastUpdate = TimeNow.TheTimeNow;
                m.spLastUpdate = TimeNow.TheTimeNow.ToShortDateString();

                this.Master._PapaDal.Update("homeContactAbout", m, TimeNow.TheTimeNow);
                this.Master._Logger.Log(new AdminException(". Home Contact And About Was Successfully Updated")
                                        , MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(17, "White", "Home Contact And About"));
            }
            catch (Exception e)
            {
                this.Master._Logger.Log(e, MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(13, "Red", "Home Contact And About"));
            }
        }
    }

    private void ShowHeaderPicEnableDisable(bool visible)
    {
        this.diableHeaderPicButton.Visible = visible;
        this.enableHeaderPicButton.Visible = visible;
    }

    private void ShowLastRecordsEnableDisable(bool visible)
    {
        this.disableLastRecordsButton.Visible = visible;
        this.enableLastRecordsButton.Visible = visible;
    }

    private void ShowWillComePicEnableDisable(bool visible)
    {
        this.disableWillComePicButton.Visible = visible;
        this.enableWillComePicButton.Visible = visible;
    }

    private void ShowHeaderPicFile(bool visible)
    {
        this.headerPicUpload.Visible = visible;
        if (visible)
        {
            this.headerPicUpload.Attributes["class"] = "mailYes";
        }
        else
        {
            this.headerPicUpload.Attributes["class"] = "mailNo";
        }
    }

    private void ShowHeaderPicPicture(bool visible)
    {
        this.headerPicUploadPic.Visible = visible;
        if (visible)
        {
            this.headerPicUploadPic.Attributes["class"] = "mailYes";
        }
        else
        {
            this.headerPicUploadPic.Attributes["class"] = "mailNo";
        }
    }

    private void ShowLastRecordsPicFile(bool visible)
    {
        this.lastRecordsColorPicUpload.Visible = visible;
        this.lastRecordsBWPicUpload.Visible = visible;
        if (visible)
        {
            this.lastRecordsBWPicUpload.Attributes["class"] = "mailYes";
            this.lastRecordsColorPicUpload.Attributes["class"] = "mailYes";
        }
        else
        {
            this.lastRecordsBWPicUpload.Attributes["class"] = "mailNo";
            this.lastRecordsColorPicUpload.Attributes["class"] = "mailNo";
        }
    }

    private void ShowLastRecordsPicPicture(bool visible)
    {
        this.lastRecordsColorPicUploadPic.Visible = visible;
        this.lastRecordsBWPicUploadPic.Visible = visible;
        if (visible)
        {
            this.lastRecordsBWPicUploadPic.Attributes["class"] = "mailYes";
            this.lastRecordsColorPicUploadPic.Attributes["class"] = "mailYes";
        }
        else
        {
            this.lastRecordsBWPicUploadPic.Attributes["class"] = "mailNo";
            this.lastRecordsColorPicUploadPic.Attributes["class"] = "mailNo";
        }
    }

    private void ShowWillComePicFile(bool visible)
    {
        this.willComeColorPicUpload.Visible = visible;
        this.willComeBWPicUpload.Visible = visible;
        if (visible)
        {
            this.willComeColorPicUpload.Attributes["class"] = "mailYes";
            this.willComeBWPicUpload.Attributes["class"] = "mailYes";
        }
        else
        {
            this.willComeColorPicUpload.Attributes["class"] = "mailNo";
            this.willComeBWPicUpload.Attributes["class"] = "mailNo";
        }
    }

    private void ShowWillComePicPicture(bool visible)
    {
        this.willComeColorPicUploadPic.Visible = visible;
        this.willComeBWPicUploadPic.Visible = visible;
        if (visible)
        {
            this.willComeColorPicUploadPic.Attributes["class"] = "mailYes";
            this.willComeBWPicUploadPic.Attributes["class"] = "mailYes";
        }
        else
        {
            this.willComeColorPicUploadPic.Attributes["class"] = "mailNo";
            this.willComeBWPicUploadPic.Attributes["class"] = "mailNo";
        }
    }

    protected void okBut_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(2))
        {
            return;
        }

        this.AfterOk(this.homeHidden.Value);
    }

    protected void cancelBut_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    private void ClearHeaderPic()
    {
        this.ClearFields(2);
        this.ClearFields(3);
        this.ClearFields(4);
    }

    private void ClearLastRecordsPic()
    {
        this.ClearFields(4);
        this.ClearFields(5);
        this.ClearFields(6);
    }

    private void ClearWillComePic()
    {
        this.ClearFields(4);
        this.ClearFields(7);
        this.ClearFields(8);
    }

    private void ClearLastNews()
    {
        this.ClearFields(4);
        this.ClearFields(9);
        this.ClearFields(10);
    }

    public void ShowHeaderPicUpdateInfo(bool visible)
    {
        this.headerPicUpdateInfo.Visible = visible;
        if (visible)
        {
            this.headerPicUpdateInfo.Attributes["class"] = "mailYes";
        }
        else
        {
            this.headerPicUpdateInfo.Attributes["class"] = "mailNo";
        }
    }

    public void ShowLastRecordsPicUpdateInfo(bool visible)
    {
        this.lastRecordsPicUpdateInfo.Visible = visible;
        if (visible)
        {
            this.lastRecordsPicUpdateInfo.Attributes["class"] = "mailYes";
        }
        else
        {
            this.lastRecordsPicUpdateInfo.Attributes["class"] = "mailNo";
        }
    }

    public void ShowWillComePicUpdateInfo(bool visible)
    {
        this.willComeUpdateInfo.Visible = visible;
        if (visible)
        {
            this.willComeUpdateInfo.Attributes["class"] = "mailYes";
        }
        else
        {
            this.willComeUpdateInfo.Attributes["class"] = "mailNo";
        }
    }

    public void ShowLastNewsUpdateInfo(bool visible)
    {
        this.lastNewsUpdateInfo.Visible = visible;
        if (visible)
        {
            this.lastNewsUpdateInfo.Attributes["class"] = "mailYes";
        }
        else
        {
            this.lastNewsUpdateInfo.Attributes["class"] = "mailNo";
        }
    }

    public void ShowContactAndAboutUpdateInfo(bool visible)
    {
        this.contactAndAboutUpdateInfo.Visible = visible;
        if (visible)
        {
            this.contactAndAboutUpdateInfo.Attributes["class"] = "mailYes";
        }
        else
        {
            this.contactAndAboutUpdateInfo.Attributes["class"] = "mailNo";
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
        this.ClearFields(7);
        this.ClearFields(8);
        this.ClearFields(9);
        this.ClearFields(10);
        this.ClearFields(11);

        this.ShowHeaderPicEnableDisable(false);
        this.ShowLastRecordsEnableDisable(false);
        this.ShowWillComePicEnableDisable(false);
        this.ShowHeaderPicFile(false);
        this.ShowHeaderPicPicture(false);
        this.ShowLastRecordsPicFile(false);
        this.ShowLastRecordsPicPicture(false);
        this.ShowWillComePicFile(false);
        this.ShowWillComePicPicture(false);

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
                if (this.headerPicHiddenRe.Value != "")
                {
                    this.RemoveHeaderPic();
                    break;
                }
                if (this.lastRecordsHiddenRe.Value != "")
                {
                    this.RemoveLastRecordsPic();
                    break;
                }
                if (this.willComePicHiddenRe.Value != "")
                {
                    this.RemoveWillComePic();
                    break;
                }
                if (this.lastNewsHiddenRe.Value != "")
                {
                    this.RemoveLastNews();
                    break;
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
            this.homeHidden.Value = "";
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
            this.homeHidden.Value = message[2];

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
            this.DivSwitcher(11);
        }
    }
}
