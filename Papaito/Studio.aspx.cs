using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dal;
using System.Threading;

public partial class Studio : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        

        //List<string> Pics = new List<string>();
        //List<WhoPlaysHereStudio> StudioPlayers = new List<WhoPlaysHereStudio>();
        //MainContact MC = Master.PapaDal.GetContactUsText();
        //StudioAbout SA = Master.PapaDal.GetStudioAboutText();
        //PreLoadStudio.Src="Studio/PreLoadStudio.jpg";
        //foreach (WhoPlaysHereStudio LRP in Master.PapaDal.WhoPlaysStudio())
        //{
        //    StudioPlayers.Add(LRP);
        //}
        //Pics = Master.PapaDal.GetStudioRecRoomPics();
        //RecordImg1.HRef = Pics[0];
        //RecordImg2.HRef = Pics[1];
        //RecordImg3.HRef = Pics[2];
        //Pics = Master.PapaDal.GetStudioControlPics();
        //ControlImg1.HRef = Pics[0];
        //ControlImg2.HRef = Pics[1];
        //ControlImg3.HRef = Pics[2];
        //Pics = Master.PapaDal.GetStudioLookAPics();
        //LookAroundImg1.HRef = Pics[0];
        //LookAroundImg2.HRef = Pics[1];
        //LookAroundImg3.HRef = Pics[2];
        ////small pop up pics
        //this.popSm1C.Src = StudioPlayers[0].PicPathColor;
        //this.popSm2C.Src = StudioPlayers[1].PicPathColor;
        //this.popSm3C.Src = StudioPlayers[2].PicPathColor;
        //this.popSm4C.Src = StudioPlayers[3].PicPathColor;
        //this.popSm5C.Src = StudioPlayers[4].PicPathColor;
        //this.popSm6C.Src = StudioPlayers[5].PicPathColor;
        //this.popSm7C.Src = StudioPlayers[6].PicPathColor;
        //this.popSm8C.Src = StudioPlayers[7].PicPathColor;
        //this.popSm9C.Src = StudioPlayers[8].PicPathColor;
        //this.popSm10C.Src = StudioPlayers[9].PicPathColor;
        //this.popSm11C.Src = StudioPlayers[10].PicPathColor;
        //this.popSm12C.Src = StudioPlayers[11].PicPathColor;

        //this.popSm1B.Src = StudioPlayers[0].PicPathBW;
        //this.popSm2B.Src = StudioPlayers[1].PicPathBW;
        //this.popSm3B.Src = StudioPlayers[2].PicPathBW;
        //this.popSm4B.Src = StudioPlayers[3].PicPathBW;
        //this.popSm5B.Src = StudioPlayers[4].PicPathBW;
        //this.popSm6B.Src = StudioPlayers[5].PicPathBW;
        //this.popSm7B.Src = StudioPlayers[6].PicPathBW;
        //this.popSm8B.Src = StudioPlayers[7].PicPathBW;
        //this.popSm9B.Src = StudioPlayers[8].PicPathBW;
        //this.popSm10B.Src = StudioPlayers[9].PicPathBW;
        //this.popSm11B.Src = StudioPlayers[10].PicPathBW;
        //this.popSm12B.Src = StudioPlayers[11].PicPathBW;

        //if (Request["Language"] == "He")
        //{
        //    this.AboutTheStudio.InnerText = "על הסטודיו";
        //    this.AboutTheStudio.Style.Add("margin-left", "65px");

        //    this.WhoPlaysHere.InnerText = "מי מנגן אצלנו";
        //    this.WhoPlaysHere.Style.Add("margin-left", "45px");

        //    this.About.InnerText = "אודות";
        //    this.About.Style.Add("margin-left", "90px");

        //    this.ContactUs.InnerText = "צור קשר";
        //    this.ContactUs.Style.Add("margin-left", "75px");

        //    this.MoreLink1.InnerText = "עוד";
            
        //    this.AboutLeft.InnerText = SA.AboutLeftHe;
        //    this.AboutLeft.Style.Add("float","right");
        //    this.AboutRight.InnerText = SA.AboutRightHe;
        //    this.AboutRight.Style.Add("float", "right");
        //    this.MainContact.InnerText = MC.ContactHe;
        //    this.MainContact.Style.Add("float", "right");
        //}
        //else
        //{
        //    this.AboutTheStudio.InnerText = "About The Studio";
        //    this.AboutTheStudio.Style.Add("margin-left", "35px");

        //    this.WhoPlaysHere.InnerText = "Who Plays Here";
        //    this.WhoPlaysHere.Style.Add("margin-left", "30px");

        //    this.About.InnerText = "About";
        //    this.About.Style.Add("margin-left", "90px");

        //    this.ContactUs.InnerText = "Contact Us";
        //    this.ContactUs.Style.Add("margin-left", "75px");
        //    this.MoreLink1.InnerText = "More";

        //    this.AboutLeft.InnerText = SA.AboutLeftEn;
        //    this.AboutRight.InnerText = SA.AboutRightEn;
        //    this.MainContact.InnerText = MC.ContactEn;
        //}
        
    }
}
