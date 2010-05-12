using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Dal;

public partial class _MasterStudio : System.Web.UI.MasterPage
{
    private PapaDal papaDal = new PapaDal();
    
    public PapaDal PapaDal
    {
        get { return this.papaDal; }
        set { this.papaDal = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string PaddingHe = "5px 35.3px 0 35.3px";
        string FontS = "12px";
        string FontHe = "Arial";
        string PaddingEn = "5px 29.5px 0 29px";
        string FontEn = "Adobe Heiti Std R";

        if (Request["Language"] == "He")
        {
            ScrTitle.Text = "ברוכים הבאים לאתר פפאיתו";
            Home.HRef = "Home.aspx?Language=He";
            Home.InnerText = "בית";
            Home.Style.Add(HtmlTextWriterStyle.Padding, PaddingHe);
            Home.Style.Add(HtmlTextWriterStyle.Height, "20px");
            Home.Style.Add("text-align", "center");
            Home.Style.Add("text-decoration", "none");
            Home.Style.Add("font-size", FontS);
            Home.Style.Add("font-weight", "Bold");
            Home.Style.Add("border", "1px solid #808080");
            Home.Style.Add("float", "left");
            Home.Style.Add(HtmlTextWriterStyle.FontFamily, FontHe);

            Studio.HRef = "Studio.aspx?Language=He";
            Studio.InnerText = "סטודיו";
            Studio.Style.Add(HtmlTextWriterStyle.Padding, PaddingHe);
            Studio.Style.Add(HtmlTextWriterStyle.Height, "20px");
            Studio.Style.Add("text-align", "center");
            Studio.Style.Add("text-decoration", "none");
            Studio.Style.Add("font-size", FontS);
            Studio.Style.Add("font-weight", "Bold");
            Studio.Style.Add("border", "1px solid #808080");
            Studio.Style.Add("float", "left");
            Studio.Style.Add(HtmlTextWriterStyle.FontFamily, FontHe);

            Complex.HRef = "Complex.aspx?Language=He";
            Complex.InnerText = "קומפלקס";
            Complex.Style.Add(HtmlTextWriterStyle.Padding, PaddingHe);
            Complex.Style.Add(HtmlTextWriterStyle.Height, "20px");
            Complex.Style.Add("text-align", "center");
            Complex.Style.Add("text-decoration", "none");
            Complex.Style.Add("font-size", FontS);
            Complex.Style.Add("font-weight", "Bold");
            Complex.Style.Add("border", "1px solid #808080");
            Complex.Style.Add("float", "left");
            Complex.Style.Add(HtmlTextWriterStyle.FontFamily, FontHe);

            Staff.HRef = "Staff.aspx?Language=He";
            Staff.InnerText = "הצוות";
            Staff.Style.Add(HtmlTextWriterStyle.Padding, PaddingHe);
            Staff.Style.Add(HtmlTextWriterStyle.Height, "20px");
            Staff.Style.Add("text-align", "center");
            Staff.Style.Add("text-decoration", "none");
            Staff.Style.Add("font-size", FontS);
            Staff.Style.Add("font-weight", "Bold");
            Staff.Style.Add("border", "1px solid #808080");
            Staff.Style.Add("float", "left");
            Staff.Style.Add(HtmlTextWriterStyle.FontFamily, FontHe);

            PR.HRef = "PR.aspx?Language=He";
            PR.InnerText = "יח\"צ";
            PR.Style.Add(HtmlTextWriterStyle.Padding, PaddingHe);
            PR.Style.Add(HtmlTextWriterStyle.Height, "20px");
            PR.Style.Add("text-align", "center");
            PR.Style.Add("text-decoration", "none");
            PR.Style.Add("font-size", FontS);
            PR.Style.Add("font-weight", "Bold");
            PR.Style.Add("border", "1px solid #808080");
            PR.Style.Add("float", "left");
            PR.Style.Add(HtmlTextWriterStyle.FontFamily, FontHe);

            Design.HRef = "Design.aspx?Language=He";
            Design.InnerText = "עיצוב";
            Design.Style.Add(HtmlTextWriterStyle.Padding, PaddingHe);
            Design.Style.Add(HtmlTextWriterStyle.Height, "20px");
            Design.Style.Add("text-align", "center");
            Design.Style.Add("font-weight", "Bold");
            Design.Style.Add("text-decoration", "none");
            Design.Style.Add("font-size", FontS);
            Design.Style.Add("border", "1px solid #808080");
            Design.Style.Add("float", "left");
            Design.Style.Add(HtmlTextWriterStyle.FontFamily, FontHe);

            Prod.HRef = "Production.aspx?Language=He";
            Prod.InnerText = "הפקות";
            Prod.Style.Add(HtmlTextWriterStyle.Padding, PaddingHe);
            Prod.Style.Add(HtmlTextWriterStyle.Height, "20px");
            Prod.Style.Add("text-align", "center");
            Prod.Style.Add("text-decoration", "none");
            Prod.Style.Add("font-size", FontS);
            Prod.Style.Add("font-weight", "Bold");
            Prod.Style.Add("border", "1px solid #808080");
            Prod.Style.Add("float", "left");
            Prod.Style.Add(HtmlTextWriterStyle.FontFamily, FontHe);

            CAB.HRef = "ContentAndAbout.aspx?Language=He";
            CAB.InnerText = "צור קשר";
            CAB.Style.Add(HtmlTextWriterStyle.Padding, PaddingHe);
            CAB.Style.Add(HtmlTextWriterStyle.Height, "20px");
            CAB.Style.Add("text-align", "center");
            CAB.Style.Add("text-decoration", "none");
            CAB.Style.Add("font-size", FontS);
            CAB.Style.Add("font-weight", "Bold");
            CAB.Style.Add("border", "1px solid #808080");
            CAB.Style.Add("float", "left");
            CAB.Style.Add(HtmlTextWriterStyle.FontFamily, FontHe);

        }
        else
        {
            ScrTitle.Text = "Welcome To Papaito Website";
            Home.HRef = "Home.aspx?Language=En";
            Home.InnerText = "Home";
            Home.Style.Add(HtmlTextWriterStyle.Padding, PaddingEn);
            Home.Style.Add(HtmlTextWriterStyle.Height, "20px");
            Home.Style.Add("text-align", "center");
            Home.Style.Add("text-decoration", "none");
            Home.Style.Add("font-size", FontS);
            Home.Style.Add("border", "1px solid #808080");
            Home.Style.Add("float", "left");
            Home.Style.Add(HtmlTextWriterStyle.FontFamily, FontEn);

            Studio.HRef = "Studio.aspx?Language=En";
            Studio.InnerText = "Studio";
            Studio.Style.Add(HtmlTextWriterStyle.Padding, PaddingEn);
            Studio.Style.Add(HtmlTextWriterStyle.Height, "20px");
            Studio.Style.Add("text-align", "center");
            Studio.Style.Add("text-decoration", "none");
            Studio.Style.Add("font-size", FontS);
            Studio.Style.Add("border", "1px solid #808080");
            Studio.Style.Add("float", "left");
            Studio.Style.Add(HtmlTextWriterStyle.FontFamily, FontEn);

            Complex.HRef = "Complex.aspx?Language=En";
            Complex.InnerText = "Complex";
            Complex.Style.Add(HtmlTextWriterStyle.Padding, PaddingEn);
            Complex.Style.Add(HtmlTextWriterStyle.Height, "20px");
            Complex.Style.Add("text-align", "center");
            Complex.Style.Add("text-decoration", "none");
            Complex.Style.Add("font-size", FontS);
            Complex.Style.Add("border", "1px solid #808080");
            Complex.Style.Add("float", "left");
            Complex.Style.Add(HtmlTextWriterStyle.FontFamily, FontEn);

            Staff.HRef = "Staff.aspx?Language=En";
            Staff.InnerText = "Staff";
            Staff.Style.Add(HtmlTextWriterStyle.Padding, PaddingEn);
            Staff.Style.Add(HtmlTextWriterStyle.Height, "20px");
            Staff.Style.Add("text-align", "center");
            Staff.Style.Add("text-decoration", "none");
            Staff.Style.Add("font-size", FontS);
            Staff.Style.Add("border", "1px solid #808080");
            Staff.Style.Add("float", "left");
            Staff.Style.Add(HtmlTextWriterStyle.FontFamily, FontEn);

            PR.HRef = "PR.aspx?Language=En";
            PR.InnerText = "P&R";
            PR.Style.Add(HtmlTextWriterStyle.Padding, PaddingEn);
            PR.Style.Add(HtmlTextWriterStyle.Height, "20px");
            PR.Style.Add("text-align", "center");
            PR.Style.Add("text-decoration", "none");
            PR.Style.Add("font-size", FontS);
            PR.Style.Add("border", "1px solid #808080");
            PR.Style.Add("float", "left");
            PR.Style.Add(HtmlTextWriterStyle.FontFamily, FontEn);

            Design.HRef = "Design.aspx?Language=En";
            Design.InnerText = "Design";
            Design.Style.Add(HtmlTextWriterStyle.Padding, PaddingEn);
            Design.Style.Add(HtmlTextWriterStyle.Height, "20px");
            Design.Style.Add("text-align", "center");
            Design.Style.Add("text-decoration", "none");
            Design.Style.Add("font-size", FontS);
            Design.Style.Add("border", "1px solid #808080");
            Design.Style.Add("float", "left");
            Design.Style.Add(HtmlTextWriterStyle.FontFamily, FontEn);

            Prod.HRef = "Production.aspx?Languuage=En";
            Prod.InnerText = "Production";
            Prod.Style.Add(HtmlTextWriterStyle.Padding, PaddingEn);
            Prod.Style.Add(HtmlTextWriterStyle.Height, "20px");
            Prod.Style.Add("text-align", "center");
            Prod.Style.Add("text-decoration", "none");
            Prod.Style.Add("font-size", FontS);
            Prod.Style.Add("border", "1px solid #808080");
            Prod.Style.Add("float", "left");
            Prod.Style.Add(HtmlTextWriterStyle.FontFamily, FontEn);

            CAB.HRef = "ContentAndAbout.aspx?Language=En";
            CAB.InnerText = "Contact&About";
            CAB.Style.Add(HtmlTextWriterStyle.Padding, PaddingEn);
            CAB.Style.Add(HtmlTextWriterStyle.Height, "20px");
            CAB.Style.Add("text-align", "center");
            CAB.Style.Add("text-decoration", "none");
            CAB.Style.Add("font-size", FontS);
            CAB.Style.Add("border", "1px solid #808080");
            CAB.Style.Add("float", "left");
            CAB.Style.Add(HtmlTextWriterStyle.FontFamily, FontEn);
        }
    }
}
