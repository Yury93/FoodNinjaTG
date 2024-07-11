using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeHandler : MonoBehaviour
{
    public const string HOUR_KEY = "HOUR_KEY";
    public const string DAYS_KEY = "DAYS_KEY";
    public static DateTime TimeNow => DateTime.Now;
    public static int LastHour { get { return int.Parse( Jammer.PlayerPrefs.GetString(HOUR_KEY)); } }
    public static int LastDays { get { return int.Parse(Jammer.PlayerPrefs.GetString(DAYS_KEY)); } }
    public static bool HasAdv(int leftHourAdv)
    {
        DateTime lastTime = new DateTime();
        lastTime.AddHours(LastHour);
        lastTime.AddHours(LastDays);
        lastTime.AddYears(-lastTime.Year);
        lastTime.AddYears(TimeNow.Year);
        lastTime.AddYears(-lastTime.Month);
        lastTime.AddYears(TimeNow.Month);
        Debug.Log("LaSTtIME: " + lastTime.ToString()+"/ time now:" + TimeNow);
        var timeLeft = TimeNow.AddHours(-leftHourAdv) - lastTime;
        if (timeLeft.TotalSeconds <= 0)
        {
            PlayerPrefs.DeleteKey(HOUR_KEY);
            PlayerPrefs.SetInt(DAYS_KEY);
            return true;
        }
        else return false;
    }
   
}
