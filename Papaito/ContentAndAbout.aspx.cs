using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dal;

public partial class _ContentAndAbout : System.Web.UI.Page
{
    protected void Page_PreLoad(object sender, EventArgs e)
    {
        //Dal.MainContact MC=this.Master.PapaDal.GetContactUsText();
        //if (Request["Language"] == "He")
        //{
        //    this.About.InnerText = "אודות";
        //    this.MainContact.InnerText = MC.ContactHe;
        //}
        //else
        //{
        //    this.About.InnerText = "About";
        //    this.MainContact.InnerText = MC.ContactEn;
        //}
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        this.textAbout.InnerText = @"ברוכים הבאים לפאפאיתו – אולפני הקלטה למוסיקה. כאן תוכלו לנגן, להקליט, לשיר, ללמוד נגינה, לרכוש ציוד מקצועי, להפיק שיר או קליפ עם הקדשה אישית – והכל תחת קורת גג אחת. 

אולפני הקלטות פאפאיתו כוללים שלושה סניפים פעילים הממוקמים ברמת אביב, צהלה ורמת השרון, ומציעים מגוון אפשרויות: אולפן הקלטות מקצועי, חדרי חזרות מרווחים, פלייבקים, בית ספר לנגינה, חנות כלי נגינה, חדר עריכה, הקלטות לחובבנים (שיר/קליפ במתנה) ועוד. כל הסניפים זמינים לשירותכם, ובכולם חנייה בשפע. ";


    }
}
