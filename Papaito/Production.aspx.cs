using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using Dal;
using System.Text;

public partial class _Production : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
//        if (this.Master.PapaDal.GetCount("production") == 0)
//        {
//            return;
//        }
        
//        StringBuilder build = new StringBuilder(@"<ul class=""thumbPro"">");
//        StringBuilder musicDiv = new StringBuilder();

//        foreach (Production m in (IEnumerable<Production>)this.Master.PapaDal.GetAll("production"))
//        {
//            if (m.Active == 2)
//            {
//                continue;
//            }

//            StringBuilder songs = new StringBuilder();
//            foreach (Song b in this.Master.PapaDal.GetAllSongs(m.ProID))
//            {
//                if (b.Active == 2)
//                {
//                    continue;
//                }
//                if (Request["Language"] == "He")
//                {
//                    songs.Append(b.SongNameHe + b.YouTubePath);
//                }
//                else
//                {
//                    songs.Append(b.SongNameEn + b.YouTubePath);
//                }
//            }

//            musicDiv.Append(string.Format(@"<div id=""musics{0}"" class=""music""></div>",m.ProID));
//            build.Append("<li>");
//            build.Append(string.Format(@"<div id=""pic{0}"" class=""fadehover"">", m.ProID));
//            build.Append(string.Format(@"<img id=""proB{0}"" src=""{1}"" class=""a"" alt="""" />", m.ProID, m.PicPathBW));
//            build.Append(string.Format(@"<img id=""proC{0}"" src=""{1}"" class=""b"" alt="""" />", m.ProID, m.PicPathColor));
//            if (Request["Language"] == "He")
//            {
//                About.InnerText = "אודות";
//                //About.Style.Add("font-family", "Arial");
//                LatestRecords.InnerText = "האחרונים שהקליטו";
//                //LatestRecords.Style.Add("font-family", "Arial");
//                LetsHearIt.InnerText = "שמע אותנו";
//                //LetsHearIt.Style.Add("font-family", "Arial");
//                //LetsHearIt.Style.Add("margin-left", "45px");
//                //LetsHearIt.Style.Add("margin-top", "10px");
//                HearMore.InnerText = @"רוצה לשמוע עוד ממיטב האמנים שהקליטו
//                    אצלנו? הקלק כאן";
//                HearMore.Style.Add("font-family", "Arial");
//                build.Append(string.Format(@"<input id=""textAbout{0}"" class=""textAboout"" style=""float:right"" type=""hidden"" value=""{1}"" />", m.ProID, m.ArtistTextHe)); //this.Session["lan"] == "he" ? m.ArtistTextHe : m.ArtistTextEn));
//            }
//            else
//            {
//                About.InnerText = "About";
//                LatestRecords.InnerText = "Latest Records";
//                LetsHearIt.InnerText = "Lets Hear It";
//                //LetsHearIt.Style.Add("margin-top", "10px");
//                HearMore.InnerText = "Wanna Hear More Artists? Click Here!";
//                build.Append(string.Format(@"<input id=""textAbout{0}"" class=""textAboout"" style=""float:left"" type=""hidden"" value=""{1}"" />", m.ProID, m.ArtistTextEn)); //this.Session["lan"] == "he" ? m.ArtistTextHe : m.ArtistTextEn));
//            }
//                build.Append(string.Format(@"<div id=""s{0}"" class=""songAboout"">{1}</div>", m.ProID, songs.ToString()));
//            build.Append("</div></li>");
//        }

//        this.music.InnerHtml = musicDiv.ToString();

//        this.proGallery.InnerHtml = build.Append("</ul>").ToString();

    }
}
