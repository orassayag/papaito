using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Drawing;
using System.IO;
using Dal;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for GlobalFunctions
/// </summary>
public class GlobalFunctions
{
    private PapaDal papaDal = new PapaDal();

    public GlobalFunctions() { }

    /// <summary>
    /// This method check if the uplodad image is with the resolution
    /// that equals or less than given width and height
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public bool ValidatePicResolution(string filePath, int width, int height)
    {
        Bitmap m = new Bitmap(filePath);
        return width <= m.Width && height <= m.Height;
    }

    public bool ValidateMail(string mail)
    {
        Regex r = new Regex(@"/^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/");
        return r.IsMatch(mail);
    }

    public string ConvertToUtf8(string value)
    {
        if (value == "" || value == null)
        {
            return "";
        }

        UTF8Encoding utf8 = new UTF8Encoding();
        byte[] text = utf8.GetBytes(value);
        return utf8.GetString(text);
    }

    /// <summary>
    /// This method check if the uplodad file is with the ending of
    /// jpg,jpeg,png,gif
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public bool ValidatePicEnd(string path)
    {
        if (path != "")
        {
            Regex r = new Regex(@"(.*?)\.(jpg|jpeg|png|gif|JPG|JPEG|PNG|GIF)$");
            return r.IsMatch(path);
        }
        return false;
    }

    /// <summary>
    /// This method take embad link from youtube and
    /// config it to be in orange color and in the right size
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public string ConfigYoutube(string path)
    {
        Regex r = new Regex("(?<YouTube>http://www\\.youtube\\.com\\S*)\"");

        string t = "";
        foreach (Match m in r.Matches(path))
        {
            if (m.Value != "")
            {
                int tar = m.Value.IndexOf('=');
                int how = m.Value.Length - tar;
                string y = m.Value.Remove(tar, how);
                t = y + "en_US&fs=1&color1=0xe1600f&color2=0xfebd01";
                break;
            }
        }
        if (t == "")
        {
            return "";
        }
        return string.Format(@"<object width=""320"" height=""265""><param name=""movie"" 
value=""{0}"">
</param><param name=""allowFullScreen"" value=""true""></param><param name=""allowscriptaccess""
value=""always""></param><embed 
src=""{1}"" 
type=""application/x-shockwave-flash"" allowscriptaccess=""always"" allowfullscreen=""true""
width=""200"" height=""25""></embed></object>", t, t);
    }

    public void SendMailToAdministrator(string title, string message)
    {
        if (title == "" || title == null ||
            message == "" || message == null)
        {
            return;
        }

        //The Recommanded way is to send him the mail by the mail server 
        //Mail Server include: Host Address, User Name and Password

        MailMessage m = new MailMessage("Coaching_Web_Admin@Coaching.com", "orassyag@walla.co.il"); // my walla

        m.Subject = title;
        m.Body = message;

        SmtpClient ss = new SmtpClient("smtp.gmail.com", 587); // put here the address of where you send the mail from 
        //(this is the place for the host mail address)
        ss.EnableSsl = true;
        ss.Timeout = 10000;
        ss.DeliveryMethod = SmtpDeliveryMethod.Network;
        ss.UseDefaultCredentials = false;
        ss.Credentials = new NetworkCredential("username", "pass"); //put here the user name and the password of the mail server host

        ss.Send(m);
    }

    public string FixUrl(string url)
    {
        if (url == "" || url == null)
        {
            return null;
        }

        string a = "http://";

        if (!url.Contains(a))
        {
            url = a + url;
        }
        return url;
    }

    public string ConfigLastNews(string language, string lastNews)
    {
        if (language == "" || language == null || lastNews == "" || lastNews == null)
        {
            return "";
        }

        string converted = "";

        if (language == "He")
        {
            converted = this.ConvertToUtf8(lastNews);
        }

        if (language == "En")
        {
            converted = lastNews;
        }

        Regex textR = new Regex("\\[\"([^\"]*)\" ([^\\]]*)\\]");
        converted = textR.Replace(converted, "<a href=\"http://$2\" title=\"Click to open in a new window or tab\" target=\"&#95;blank\">$1</a>");

        return converted;
    }

    private string ConvertFileName(string fileName)
    {
        if (fileName == "" || fileName == null)
        {
            return null;
        }

        while (fileName.Contains('/'))
        {
            fileName = fileName.Remove(fileName.IndexOf('/'), 1);
        }

        return fileName + ".txt";
    }

    private bool CheckPaths()
    {
        if (ConfigurationSettings.AppSettings["FullPath"] == null)
        {
            return false;
        }

        if (ConfigurationSettings.AppSettings["RelativePath"] == null)
        {
            return false;
        }
        return true;
    }

    public string GetFullPath()
    {
        if (!this.CheckPaths())
        {
            return "";
        }
        return ConfigurationSettings.AppSettings["FullPath"];
    }

    public string GetRelativePath()
    {
        if (!this.CheckPaths())
        {
            return "";
        }
        return ConfigurationSettings.AppSettings["RelativePath"];
    }

    public void WriteLogToFile(DateTime time)
    {
        if (ConfigurationSettings.AppSettings["ErrorLogPath"] == null || time == default(DateTime))
        {
            return;
        }

        string errorPath = ConfigurationSettings.AppSettings["ErrorLogPath"];

        if (time == default(DateTime))
        {
            return;
        }

        string pathFile = "";
        DirectoryInfo directory = null;

        directory = new DirectoryInfo(errorPath);


        if (directory == null)
        {
            return;
        }

        foreach (FileInfo file in directory.GetFiles())
        {
            if (file.Name == this.ConvertFileName(time.ToShortDateString()))
            {
                pathFile = file.FullName;
                break;
            }
        }

        FileStream fileSt = null;
        string date = "";

        if (pathFile == "")
        {
            date = this.ConvertFileName(time.ToShortDateString());

            try
            {
                fileSt = File.Create(directory.FullName + date);
            }
            finally
            {
                if (fileSt != null)
                {
                    pathFile = fileSt.Name;
                    fileSt.Close();
                }
            }
            if (fileSt == null)
            {
                return;
            }
        }

        StreamWriter writer = null;
        try
        {
            writer = new StreamWriter(pathFile, true);
            writer.WriteLine(string.Format("This Is Coaching's Logs From {0}:\n\r", time.ToString()));

            foreach (Log l in (IEnumerable<Log>)this.papaDal.GetAll("log"))
            {
                writer.WriteLine("{0}, {1} ==> {2}\n\r", l.LogDate, l.LogID, l.LogMessage);
            }
        }
        finally
        {
            if (writer != null)
            {
                writer.Close();
            }

            if (fileSt != null)
            {
                fileSt.Close();
            }
        }
    }

    public void SendMailAdmins(string mailAdress)
    {
        if (mailAdress == "" || mailAdress == null)
        {
            return;
        }

        //The Recommanded way is to send him the mail by the mail server 
        //Mail Server include: Host Address, User Name and Password


        StringBuilder bodyBuild = new StringBuilder("This Is Papaito's All Admin's:\n\r");

        foreach (AdminUser c in (IEnumerable<AdminUser>)this.papaDal.GetAll("admin"))
        {
            bodyBuild.Append("User ID: " + c.UserID + "\n\rPassword: " + c.Password + "\n\r\n\r");
        }

        MailMessage m = new MailMessage("Papaito@gmail.com", mailAdress); // itay gmail

        m.Subject = "Admin User ID's And Password Recovery";
        m.Body = bodyBuild.ToString();

        SmtpClient ss = new SmtpClient("smtp.gmail.com", 587); // put here the address of where you send the mail from 
        //(this is the place for the host mail address)
        ss.EnableSsl = true;
        ss.Timeout = 10000;
        ss.DeliveryMethod = SmtpDeliveryMethod.Network;
        ss.UseDefaultCredentials = false;
        ss.Credentials = new NetworkCredential("username", "pass"); //put here the user name and the password of the mail server host
        ss.Send(m);
    }
}
