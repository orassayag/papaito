using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

/// <summary>
/// Summary description for Notifier
/// </summary>
public class Notifier
{
    public Notifier() { }

    /// <summary>
    /// This method update the status of the result of the submit button
    /// </summary>
    /// <param name="lbl"></param>
    public string[] Notify(int option, string color, string valueName)
    {
        string message = "";

        switch (option)
        {
            case 0:
                message = "Ooooops! Somthing Wrong Was Happend, Please Try Again Or/And content The Administrator";
                break;
            case 1:
                message = "Please Select An Action";
                break;
            case 2:
                message = "No Update Was Found";
                break;
            case 3:
                message = valueName + " Faild To Be Added";
                break;
            case 4:
                message = "No Images To Update/Remove";
                break;
            case 5:
                message = "Are You Sure You Want To Remove " + valueName + "?";
                break;
            case 6:
                message = valueName + " Has Been Successfully Enabled";
                break;
            case 7:
                message = valueName + " Has Been Successfully Disabeld";
                break;
            case 8:
                message = valueName + " Is Already Disabled";
                break;
            case 9:
                message = valueName + " Is Already Enabled";
                break;
            case 10:
                message = "Faild To Enable " + valueName;
                break;
            case 11:
                message = "Faild To Disable " + valueName;
                break;
            case 12:
                message = valueName + " Has Been Successfully Removed";
                break;
            case 13:
                message = "Faild To Remove " + valueName;
                break;
            case 14:
                message = "Faild To Add " + valueName + ". File Must Be With Ending With One Of The Following: jpg, jpeg, png, gif / JPG, JPEG. PNG, GIF";
                break;
            case 15:
                message = valueName + " Has Been Successfully Added";
                break;
            case 16:
                message = "Faild To Add " + valueName;
                break;
            case 17:
                message = valueName + " Has Been Successfully Updated";
                break;
            case 18:
                message = "Faild To Update " + valueName;
                break;
            case 19:
                message = "Please Enter Picture Place";
                break;
            case 20:
                message = "Invalid Picture Place";
                break;
            case 21:
                message = "Place " + valueName + " Is Not Available, Please Select Other Place And Try Again";
                break;
            case 22:
                message = "Please Enter Publish Gallery Name In Hebrew";
                break;
            case 23:
                message = "Please Enter Publish Gallery Name In English";
                break;
            case 24:
                message = "Please Enter All Artists Gallery Name In Hebrew";
                break;
            case 25:
                message = "Please Enter All Artists Gallery Name In English";
                break;
            case 26:
                message = "Please Enter Artists Name In Hebrew";
                break;
            case 27:
                message = "Please Enter Artists Name In English";
                break;
            case 28:
                message = "Please Enter About Artists In Hebrew";
                break;
            case 29:
                message = "Please Enter About Artists In English";
                break;
            case 30:
                message = "Please Enter Song Place";
                break;
            case 31:
                message = "Please Enter Song Name In Hebrew";
                break;
            case 32:
                message = "Please Enter Song Name In English";
                break;
            case 33:
                message = "Invalid YouTube Address";
                break;
            case 34:
                message = "Please Enter YouTube Address";
                break;
            case 35:
                message = "Invalid Song Place";
                break;
            case 36:
                message = "Faild To Save File " + valueName;
                break;
            case 37:
                message = "Please Insert " + valueName + " Picture";
                break;
            case 38:
                message = "Please Enter " + valueName + " Name In Hebrew";
                break;
            case 39:
                message = "Please Enter " + valueName + " Name In English";
                break;
            case 40:
                message = "Please Enter " + valueName + " Text In Hebrew";
                break;
            case 41:
                message = "Please Enter " + valueName + " Text In English";
                break;
            case 44:
                message = "Please Enter User ID";
                break;
            case 45:
                message = "Please Enter Password";
                break;
            case 46:
                message = "Please Re-Enter Password";
                break;
            case 47:
                message = valueName + " Is Already In Use, Please Enter Other User ID";
                break;
            case 48:
                message = valueName + " Is Already In Use, Please Enter Other Password";
                break;
            case 49:
                message = "Passwords Don't Match";
                break;
            case 50:
                message = "No Admins To Remove / Update";
                break;
            case 51:
                message = "Can't Active More Pictures To Will Come Soon Pictures, Max Is 3 Pictures";
                break;
            case 52:
                message = "Can't Active More Pictures To This Gallery, Max Is 16 Pictures Per Gallery";
                break;
            case 53:
                message = "Can't Active More Pictures To Last Records Pictures, Max Is 9 Pictures";
                break;
            case 54:
                message = "Can't Active More Pictures To " + valueName + ", Max Is 3 Pictures";
                break;
            case 57:
                message = "Please Select " + valueName + " To Remove Or Update";
                break;
            case 58:
                message = "Please Enter News Place";
                break;
            case 59:
                message = "Please Enter Production Place";
                break;
            case 60:
                message = "Invalid Production Place";
                break;
            case 62:
                message = "Please Select The Production To Add The Song To";
                break;
            case 63:
                message = "Please Enter Gallery Place";
                break;
            case 64:
                message = "Invalid Gallery Place";
                break;
            case 65:
                message = "Please Select The Gallery To Add The Picture To";
                break;
            case 66:
                message = "Invalid Picture Place " + valueName + ". Please Enter Places From 1 To 16";
                break;
            case 67:
                message = "Can't Active More Pictures To " + valueName + ", Max Is 6 Pictures";
                break;
            case 68:
                message = "Invalid Staff Place";
                break;
            case 69:
                message = "Please Enter Staff Place";
                break;
            case 70:
                message = "You Are Temporarily Blocked, Please Contact Administer";
                break;
            default:
                break;
        }

        //this.productionArtistNameHe.Text = "";
        //this.productionArtistNameEn.Text = "";
        //this.productionArtistNameHe.Text = ""; //clear text boxes text
        //this.productionArtistNameEn.Text = "";

        //this.main.Style["visibility"] = "hidden"; //hide the tabs
        //this.error.Style["visibility"] = "visible"; //show the message to the user
        //this.error.Visible = true;

        if (message == "")
        {
            throw new AdminException("message == \"\" at " + MethodBase.GetCurrentMethod().Name);
        }

        return new string[] { message, color, option.ToString() };
    }
}
