using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dal;

public partial class _Staff : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //StaffDudu SD = this.Master.PapaDal.GetStaffDudu();
        //StaffPerri SP = this.Master.PapaDal.GetStaffPerri();
        //StaffItay SI = this.Master.PapaDal.GetStaffItay();
        //StaffNapo SN = this.Master.PapaDal.GetStaffNapo();

        //if (Request["Language"] == "He")
        //{
        //    this.DuduTitle.InnerText = SD.DuduTitleHe;
        //    this.DuduTitle.Style.Add("text-align", "center");
        //    this.DuduText.InnerText = SD.DuduTextHe;
        //    this.DuduText.Style.Add("float", "right");
            
        //    this.PerryTitle.InnerText = SP.PerriTitleHe;
        //    this.PerryTitle.Style.Add("text-align", "center");
        //    this.PerryText.InnerText = SP.PerriTextHe;
        //    this.PerryText.Style.Add("float", "right");
            
        //    this.ItayTitle.InnerText = SI.ItayTitleHe;
        //    this.ItayText.InnerText = SI.ItayTextHe;
        //    this.ItayTitle.Style.Add("text-align", "center");
        //    this.ItayText.Style.Add("float", "right");

        //    this.NapoTitle.InnerText = SN.NapoTitleHe;
        //    this.NapoText.InnerText = SN.NapoTextHe;
        //    this.NapoTitle.Style.Add("text-align", "center");
        //    this.NapoText.Style.Add("float", "right");
            
        //    closeBut1.InnerText = "[סגור]";
        //    closeBut1.Style.Add("float", "left");
        //    closeBut2.InnerText = "[סגור]";
        //    closeBut2.Style.Add("float", "left");
        //    closeBut3.InnerText = "[סגור]";
        //    closeBut3.Style.Add("float", "left");
        //    closeBut4.InnerText = "[סגור]";
        //    closeBut4.Style.Add("float", "left");
        //}
        //else
        //{
        //    this.DuduTitle.InnerText = SD.DuduTitleEn;
        //    this.DuduText.InnerText = SD.DuduTextEn;
        //    this.DuduTitle.Style.Add("text-align", "center");

        //    this.PerryTitle.InnerText = SP.PerriTitleEn;
        //    this.PerryText.InnerText = SP.PerriTextEn;
        //    this.PerryTitle.Style.Add("text-align", "center");
            
        //    this.ItayTitle.InnerText = SI.ItayTitleEn;
        //    this.ItayText.InnerText = SI.ItayTextEn;
        //    this.ItayTitle.Style.Add("text-align", "center");

        //    this.NapoTitle.InnerText = SN.NapoTitleEn;
        //    this.NapoText.InnerText = SN.NapoTextEn;
        //    this.NapoTitle.Style.Add("text-align", "center");

        //    closeBut1.InnerText = "[Close]";
        //    closeBut2.InnerText = "[Close]";
        //    closeBut3.InnerText = "[Close]";
        //    closeBut4.InnerText = "[Close]";
        //}

    }
}
