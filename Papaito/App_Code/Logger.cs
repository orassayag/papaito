﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dal;
using System.Reflection;

/// <summary>
/// Summary description for Logger
/// </summary>
public class Logger
{
    private PapaDal coachingDal = new PapaDal();
    private GlobalFunctions globalFunctions = new GlobalFunctions();

    public Logger() { }

    public void Log(Exception e, string methodName)
    {
        this.WriteLog("Log: " + methodName + ": " + e.Message, "Log");
    }

    public void Warn(Exception e, string methodName)
    {
        this.WriteLog("Warn: " + methodName + ": " + e.Message, "Warn");
    }

    public void Error(Exception e, string methodName)
    {
        this.WriteLog("Error: " + methodName + ": " + e.Message, "Error");
    }

    private void WriteLog(string message, string type)
    {
        try
        {
            if (message == "" || message == null || type == "" || type == null)
            {
                return;
            }

            long id = -1;
            long.TryParse(this.coachingDal.GetNextAvailableID("log"), out id);
            if (id == -1)
            {
                return;
            }

            Log log = new Log()
            {
                LogID = id.ToString(),
                LogMessage = message,
                LogType = type,
                LogDate = TimeNow.TheTimeNow,
                spLogDate = TimeNow.TheTimeNow.ToShortDateString(),
            };

            this.coachingDal.Add("log", log);
        }
        catch (Exception)
        {
            this.globalFunctions.SendMailToAdministrator("Faild To Write Log",
                    string.Format("Faild To Write Log With Parameters: type = {0} message = {1}",
                    type, message));
        }
    }
}
