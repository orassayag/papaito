using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Dal;

public partial class _Publish : System.Web.UI.Page
{
    private PapaDal papaDal = new PapaDal();

    protected void Page_Load(object sender, EventArgs e)
    {
        //foreach (PublishGallery g in (IEnumerable<PublishGallery>)this.papaDal.GetAll("publishGallery"))
        //{
        //    LinkButton m = new LinkButton();
        //    m.ID = "ps" + g.PublishGalleryID;
        //    m.CssClass = "set";
        //    m.Text = "Artists " + g.PublishGalleryID;
        //    m.Click += new EventHandler(m_Click);
        //    this.main.Controls.Add(m);
        //}
    }

    private void m_Click(object sender, EventArgs e)
    {
        //foreach (PublishGallery b in (IEnumerable<PublishGallery>)this.papaDal.GetAll("publishGallery"))
        //{
        //    HtmlGenericControl mainli = new HtmlGenericControl();
        //    mainli.ID = "s" + b.PublishGalleryID;
        //    mainli.TagName = "li";

        //    HtmlGenericControl insideUl = new HtmlGenericControl();
        //    insideUl.TagName = "ul";

        //    foreach (PublishPic p in this.papaDal.GetAllPublishPicByGalleryID(b.PublishGalleryID))
        //    {
        //        HtmlGenericControl insideLi = new HtmlGenericControl();

        //        HtmlGenericControl insideA = new HtmlGenericControl();
        //        insideA.TagName = "a";
        //        insideA.Attributes.Add("href", "#");

        //        HtmlImage insideImage = new HtmlImage();
        //        insideImage.ID = "pub" + p.PicID;
        //        insideImage.Src = p.PicPath;
        //        insideImage.Alt = "";

        //        insideLi.TagName = "li";
        //        insideA.Controls.Add(insideImage);
        //        insideLi.Controls.Add(insideA);
        //        insideUl.Controls.Add(insideLi);
        //    }
        //    mainli.Controls.Add(insideUl);
        //    this.topic.Controls.Add(mainli);

        //    foreach (Control ds in this.topic.Controls)
        //    {
        //        HtmlGenericControl g = ds as HtmlGenericControl;
        //        if (g != null)
        //        {
        //            if (g.ID == "s" + b.PublishGalleryID)
        //            {
        //                g.Attributes.Add("class", "active");
        //            }
        //            else
        //            {
        //                g.Attributes["class"] = "";
        //            }
        //        }
        //    }
        //}
    }
}
