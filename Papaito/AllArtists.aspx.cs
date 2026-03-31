using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class _AllArtists : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LinkButton m = new LinkButton();
        m.ID = "ps3";
        m.CssClass = "set";
        m.Text = "Artists 3";
        m.Click += new EventHandler(m_Click);
        this.main.Controls.Add(m);
    }

    private void m_Click(object sender, EventArgs e)
    {
        HtmlGenericControl mainli = new HtmlGenericControl();
        mainli.ID = "s3";
        mainli.TagName = "li";

        HtmlGenericControl insideUl = new HtmlGenericControl();
        insideUl.TagName = "ul";

        for (int i = 1; i <= 16; i++)
        {
            HtmlGenericControl insideLi = new HtmlGenericControl();

            HtmlGenericControl insideA = new HtmlGenericControl();
            insideA.TagName = "a";
            insideA.Attributes.Add("href", "#");

            HtmlImage insideImage = new HtmlImage();
            insideImage.ID = "pub" + i;
            insideImage.Src = @"OrImages2\76.jpg";
            insideImage.Alt = "";

            insideLi.TagName = "li";
            insideA.Controls.Add(insideImage);
            insideLi.Controls.Add(insideA);
            insideUl.Controls.Add(insideLi);
        }
        mainli.Controls.Add(insideUl);
        this.topic.Controls.Add(mainli);

        foreach (Control ds in this.topic.Controls)
        {
            HtmlGenericControl g = ds as HtmlGenericControl;
            if (g != null)
            {
                if (g.ID == "s3")
                {
                    g.Attributes.Add("class", "active");
                }
                else
                {
                    g.Attributes["class"] = "";
                }
            }
        }
    }
}
