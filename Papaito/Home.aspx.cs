using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dal;

public partial class _02Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ////the logo pics
        //List<string> HomePics = new List<string>();
        
        //MainContact MC = Master.PapaDal.GetContactUsText();
        //Dal.MainAbout MA = Master.PapaDal.GetHomeAboutText();

        //List<PublishPic> FishEyePics = new List<PublishPic>();
        //List<LastRecordPic> LastRecPics = new List<LastRecordPic>();
        //foreach (string s in Master.PapaDal.GetHeaderPics())
        //{
        //    HomePics.Add(s);
        //}

        //foreach(PublishPic P in Master.PapaDal.GetFishEyePics())
        //{
        //    FishEyePics.Add(P);
        //}

        //foreach (LastRecordPic LRP in Master.PapaDal.GetLastRecPics())
        //{
        //    LastRecPics.Add(LRP);
        //}

        //this.HomePic1.Src = HomePics[0];
        //this.HomePic2.Src = HomePics[1];
        //this.HomePic3.Src = HomePics[2];
        //this.HomePic4.Src = HomePics[3];
        //this.HomePic5.Src = HomePics[4];
        //this.HomePic6.Src = HomePics[5];

        //this.FishImg1.Src = FishEyePics[0].PicPath;
        //this.FishImg2.Src = FishEyePics[1].PicPath;
        //this.FishImg3.Src = FishEyePics[2].PicPath;
        //this.FishImg4.Src = FishEyePics[3].PicPath;
        //this.FishImg5.Src = FishEyePics[4].PicPath;
        //this.FishImg6.Src = FishEyePics[5].PicPath;

        //if (Request["Language"] == "He")
        //{
        //    this.FishSpan1.InnerText = FishEyePics[0].TextHe;
        //    this.FishSpan2.InnerText = FishEyePics[1].TextHe;
        //    this.FishSpan3.InnerText = FishEyePics[2].TextHe;
        //    this.FishSpan4.InnerText = FishEyePics[3].TextHe;
        //    this.FishSpan5.InnerText = FishEyePics[4].TextHe;
        //    this.FishSpan6.InnerText = FishEyePics[5].TextHe;

        //    this.AboutLeft.InnerText = MA.AboutLeftHe;
        //    this.AboutLeft.Style.Add("float", "right");
        //    this.AboutRight.InnerText = MA.AboutRightHe;
        //    this.AboutRight.Style.Add("float", "right");
        //    this.MainContact.InnerText = MC.ContactHe;
        //    this.MainContact.Style.Add("float", "right");
            
        //    this.About.InnerText = "אודות";
        //    this.About.Style.Add("margin-left", "90px");

        //    this.ContactUs.InnerText = "צור קשר";
        //    this.ContactUs.Style.Add("margin-left", "75px");

        //    this.WillComeSoon.InnerText = "עתידים להקליט";
        //    this.WillComeSoon.Style.Add("margin-left", "30px");

        //    this.MoreLink1.InnerText = "עוד";
        //    this.MoreLink2.InnerText = "עוד";
            
        //    this.LatestRecords.InnerText = "האחרונים שהקליטו";
        //    this.LatestRecords.Style.Add("margin-left", "12px");

        //    this.LatestNews.InnerText = "עדכונים";
        //    this.LatestNews.Style.Add("margin-left", "80px");
        //}
        //else
        //{
        //    this.FishSpan1.InnerText = FishEyePics[0].TextEn;
        //    this.FishSpan2.InnerText = FishEyePics[1].TextEn;
        //    this.FishSpan3.InnerText = FishEyePics[2].TextEn;
        //    this.FishSpan4.InnerText = FishEyePics[3].TextEn;
        //    this.FishSpan5.InnerText = FishEyePics[4].TextEn;
        //    this.FishSpan6.InnerText = FishEyePics[5].TextEn;

        //    this.AboutLeft.InnerText = MA.AboutLeftEn;
        //    this.AboutRight.InnerText = MA.AboutRightEn;
        //    this.MainContact.InnerText = MC.ContactEn;
            
        //    this.About.InnerText = "About";
        //    this.About.Style.Add("margin-left", "90px");

        //    this.ContactUs.InnerText = "Contact Us";
        //    this.ContactUs.Style.Add("margin-left","60px");
            
        //    this.WillComeSoon.InnerText = "Will Come Soon";
        //    this.WillComeSoon.Style.Add("margin-left", "30px");

        //    this.MoreLink1.InnerText = "More";
        //    this.MoreLink2.InnerText = "More";
            
        //    this.LatestRecords.InnerText = "Latest Records";
        //    this.LatestRecords.Style.Add("margin-left","40px");

        //    this.LatestNews.InnerText = "Latest News";
        //    this.LatestNews.Style.Add("margin-left","60px");
        //}




        ////small pop up pics
        //this.popSm1C.Src = LastRecPics[0].PicPathColor;
        //this.popSm2C.Src = LastRecPics[1].PicPathColor;
        //this.popSm3C.Src = LastRecPics[2].PicPathColor;
        //this.popSm4C.Src = LastRecPics[3].PicPathColor;
        //this.popSm5C.Src = LastRecPics[4].PicPathColor;
        //this.popSm6C.Src = LastRecPics[5].PicPathColor;
        //this.popSm7C.Src = LastRecPics[6].PicPathColor;
        //this.popSm8C.Src = LastRecPics[7].PicPathColor;
        //this.popSm9C.Src = LastRecPics[8].PicPathColor;
        //this.popSm10C.Src = LastRecPics[9].PicPathColor;
        //this.popSm11C.Src = LastRecPics[10].PicPathColor;
        //this.popSm12C.Src = LastRecPics[11].PicPathColor;

        //this.popSm1B.Src = LastRecPics[0].PicPathBW;
        //this.popSm2B.Src = LastRecPics[1].PicPathBW;
        //this.popSm3B.Src = LastRecPics[2].PicPathBW;
        //this.popSm4B.Src = LastRecPics[3].PicPathBW;
        //this.popSm5B.Src = LastRecPics[4].PicPathBW;
        //this.popSm6B.Src = LastRecPics[5].PicPathBW;
        //this.popSm7B.Src = LastRecPics[6].PicPathBW;
        //this.popSm8B.Src = LastRecPics[7].PicPathBW;
        //this.popSm9B.Src = LastRecPics[8].PicPathBW;
        //this.popSm10B.Src = LastRecPics[9].PicPathBW;
        //this.popSm11B.Src = LastRecPics[10].PicPathBW;
        //this.popSm12B.Src = LastRecPics[11].PicPathBW;

        
        
    }
}
