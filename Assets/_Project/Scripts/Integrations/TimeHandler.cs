using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeHandler : MonoBehaviour
{
    public const string LAST_TIME = "LASTTIMEPREFS"; 
    public static DateTime TimeNow => DateTime.Now.ToLocalTime();
    public static DateTime LastTime => DateTime.Parse( Jammer.PlayerPrefs.GetString(LAST_TIME)).ToLocalTime();
    public static bool HAS_KEY_TIME_SAVED => Jammer.PlayerPrefs.HasKey(LAST_TIME);
    public static bool HasAdv(int leftHourAdv)
    {
        if (!HAS_KEY_TIME_SAVED) { return true; }

        TimeSpan timeLeft = TimeNow - LastTime;
        if (timeLeft.TotalHours >= leftHourAdv) { return true; }
        else { return false; }
    }
   public static void OnSaveTimeAdv()
    {
        Jammer.PlayerPrefs.SetString(LAST_TIME, TimeNow.ToString());
    }
}
