using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeHandler : MonoBehaviour
{
    public const string LAST_TIME = "LASTTIMEPREFS"; 
    public static DateTime TimeNow => DateTime.Now.ToLocalTime();
    public static DateTime LastTime => DateTime.Parse(Jammer.PlayerPrefs.GetString(LAST_TIME));
    public static bool HAS_KEY_TIME_SAVED => Jammer.PlayerPrefs.HasKey(LAST_TIME);
    public static bool HasAdv()
    {
        if (!HAS_KEY_TIME_SAVED) { return true; }
         
        TimeSpan timeLeft =   LastTime - TimeNow;
        Debug.Log($" {timeLeft.TotalMinutes}  ");
        if (timeLeft.TotalMinutes <= 0) { return true; }
        else { return false; }
    }
   public static void OnSaveTimeAdv()
    {
        int addMinuts = 3;
    
      var timeSaved =  TimeNow.AddMinutes(addMinuts).ToLocalTime();
        //   Debug.Log($"timed SAVED:  {timeSaved.ToString()} ");
        Jammer.PlayerPrefs.SetString(LAST_TIME, timeSaved.ToString());
    }
    public static void DeleteSavedTimes()
    {
        Jammer.PlayerPrefs.DeleteKey(LAST_TIME);
    }
}
