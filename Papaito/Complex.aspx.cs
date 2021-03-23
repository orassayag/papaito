using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Dal;
using System.Threading;


public partial class _03Complex : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
    //    List<string> Pics = new List<string>();
    //    MainContact MC = Master.PapaDal.GetContactUsText();
    //    ComplexAbout CA = Master.PapaDal.GetComplexAboutText();
    //    List<WhoPlaysHereComplex> ComplexPlayers = new List<WhoPlaysHereComplex>();
    //    PreLoadComplex.Src = "Complex/PreLoadComplex.jpg";
    //    foreach (WhoPlaysHereComplex CP in Master.PapaDal.WhoPlaysComplex())
    //    {
    //        ComplexPlayers.Add(CP);
    //    }

    //    Pics = Master.PapaDal.GetComplexRoomAPics();
    //    RoomAImg1.HRef=Pics[0];
    //    RoomAImg2.HRef = Pics[1];
    //    RoomAImg3.HRef = Pics[2];
    //    Pics=Master.PapaDal.GetComplexRoomBPics();
    //    RoomBImg1.HRef = Pics[0];
    //    RoomBImg2.HRef = Pics[1];
    //    RoomBImg3.HRef = Pics[2];
    //    Pics = Master.PapaDal.GetComplexRoomCPics();
    //    RoomCImg1.HRef = Pics[0];
    //    RoomCImg2.HRef = Pics[1];
    //    RoomCImg3.HRef = Pics[2];
    //    Pics = Master.PapaDal.GetComplexLookAroundPics();
    //    LookAroundImg1.HRef = Pics[0];
    //    LookAroundImg2.HRef = Pics[1];


    //    //small pop up pics
    //    this.popSm1C.Src = ComplexPlayers[0].PicPathColor;
    //    this.popSm2C.Src = ComplexPlayers[1].PicPathColor;
    //    this.popSm3C.Src = ComplexPlayers[2].PicPathColor;
    //    this.popSm4C.Src = ComplexPlayers[3].PicPathColor;
    //    this.popSm5C.Src = ComplexPlayers[4].PicPathColor;
    //    this.popSm6C.Src = ComplexPlayers[5].PicPathColor;
    //    this.popSm7C.Src = ComplexPlayers[6].PicPathColor;
    //    this.popSm8C.Src = ComplexPlayers[7].PicPathColor;
    //    this.popSm9C.Src = ComplexPlayers[8].PicPathColor;
    //    this.popSm10C.Src = ComplexPlayers[9].PicPathColor;
    //    this.popSm11C.Src = ComplexPlayers[10].PicPathColor;
    //    this.popSm12C.Src = ComplexPlayers[11].PicPathColor;

    //    this.popSm1B.Src = ComplexPlayers[0].PicPathBW;
    //    this.popSm2B.Src = ComplexPlayers[1].PicPathBW;
    //    this.popSm3B.Src = ComplexPlayers[2].PicPathBW;
    //    this.popSm4B.Src = ComplexPlayers[3].PicPathBW;
    //    this.popSm5B.Src = ComplexPlayers[4].PicPathBW;
    //    this.popSm6B.Src = ComplexPlayers[5].PicPathBW;
    //    this.popSm7B.Src = ComplexPlayers[6].PicPathBW;
    //    this.popSm8B.Src = ComplexPlayers[7].PicPathBW;
    //    this.popSm9B.Src = ComplexPlayers[8].PicPathBW;
    //    this.popSm10B.Src = ComplexPlayers[9].PicPathBW;
    //    this.popSm11B.Src = ComplexPlayers[10].PicPathBW;
    //    this.popSm12B.Src = ComplexPlayers[11].PicPathBW;

    //    if (Request["Language"] == "He")
    //    {
    //        this.AboutTheComplex.InnerText = "על הקומפלקס";
    //        this.AboutTheComplex.Style.Add("margin-left", "40px");

    //        this.WhoPlaysHere.InnerText = "מי מנגן אצלנו";
    //        this.WhoPlaysHere.Style.Add("margin-left", "45px");

    //        this.About.InnerText = "אודות";
    //        this.About.Style.Add("margin-left", "90px");

    //        this.MoreLink1.InnerText = "עוד";

    //        this.ContactUs.InnerText = "צור קשר";
    //        this.ContactUs.Style.Add("margin-left", "75px");

    //        this.AboutLeft.InnerText = CA.AboutLeftHe;
    //        this.AboutLeft.Style.Add("float", "right");
    //        this.AboutRight.InnerText = CA.AboutRightHe;
    //        this.AboutRight.Style.Add("float", "right");
    //        this.MainContact.InnerText = MC.ContactHe;
    //        this.MainContact.Style.Add("float", "right");
    //    }
    //    else
    //    {
    //        this.AboutTheComplex.InnerText = "About The Complex";
    //        this.AboutTheComplex.Style.Add("margin-left", "10px");

    //        this.WhoPlaysHere.InnerText = "Who Plays Here";
    //        this.WhoPlaysHere.Style.Add("margin-left", "30px");

    //        this.About.InnerText = "About";
    //        this.About.Style.Add("margin-left", "90px");

    //        this.MoreLink1.InnerText = "More";

    //        this.ContactUs.InnerText = "Contact Us";
    //        this.ContactUs.Style.Add("margin-left", "75px");

    //        this.AboutLeft.InnerText = CA.AboutLeftEn;
    //        this.AboutRight.InnerText = CA.AboutRightEn;
    //        this.MainContact.InnerText = MC.ContactEn;
    //    }
    }
}
