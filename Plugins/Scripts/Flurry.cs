using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Generic;
    
public class Flurry : MonoBehaviour {
    
#if UNITY_ANDROID
    private AndroidJavaObject currentActivity = null;
    private AndroidJavaClass flurryClass = null;

    public Flurry() {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		currentActivity = unityPlayer.GetStatic<AndroidJavaObject> ("currentActivity");
		flurryClass = new AndroidJavaClass("com.flurry.android.FlurryAgent");
    }
    
    public void StartSession(string API_KEY)
    {
        flurryClass.CallStatic("setLogLevel", 2);
        flurryClass.CallStatic("setLogEnabled", true);
        flurryClass.CallStatic("setLogEvents", true);
                
        flurryClass.CallStatic("setCaptureUncaughtExceptions", true);
                
		flurryClass.CallStatic("init", currentActivity, API_KEY);
		flurryClass.CallStatic("onStartSession", currentActivity, API_KEY);
    }
    
    public void EndSession()
    {
		flurryClass.CallStatic("onEndSession", currentActivity);
    }
    
    public int GetAgentVersion()
    {
    	return flurryClass.CallStatic<int>("getAgentVersion");
    }
    
    public void LogEvent(string eventId, bool timed = false)
    {
		flurryClass.CallStatic<AndroidJavaObject>("logEvent", eventId, timed);
    }
    
    public void LogEvent (string eventId, Dictionary<string, string> parameters, bool timed = false)
    {
        using(AndroidJavaObject hashMap = new AndroidJavaObject("java.util.HashMap")) 
        {
            System.IntPtr method_Put = AndroidJNIHelper.GetMethodID(obj_HashMap.GetRawClass(), "put",
                "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");
    
            object[] args = new object[2];
    
            foreach(KeyValuePair<string, string> kvp in parameters)
            {
                using(AndroidJavaObject k = new AndroidJavaObject("java.lang.String", kvp.Key))
                {
                    using(AndroidJavaObject v = new AndroidJavaObject("java.lang.String", kvp.Value))
                    {
                        args[0] = k;
                        args[1] = v;
                        AndroidJNI.CallObjectMethod(obj_HashMap.GetRawObject(), method_Put, AndroidJNIHelper.CreateJNIArgArray(args));
                    }
                }
            }
			flurryClass.CallStatic<AndroidJavaObject>("logEvent", eventId, hashMap, timed);
        }
    }
    
    public void EndTimedEvent(string eventId)
    {
         flurryClass.CallStatic("endTimedEvent",eventId);
    }
    
#elif UNITY_IPHONE
    
#region Flurry_Imports
    [DllImport("__Internal", CharSet = CharSet.Ansi)]
    private static extern void Flurry_startSession([In, MarshalAs(UnmanagedType.LPStr)]string apiKey);
    
/*    [DllImport("__Internal")]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string Flurry_getFlurryAgentVersion();
    */
    [DllImport("__Internal")]
    private static extern void Flurry_setShowErrorInLogEnabled(bool bEnabled);
    
    [DllImport("__Internal")]
    private static extern void Flurry_setDebugLogEnabled(bool bEnabled);
    
    [DllImport("__Internal")]
    private static extern void Flurry_setEventLoggingEnabled(bool bEnabled);
    
    [DllImport("__Internal")]
    private static extern void Flurry_setSessionReportsOnCloseEnabled(bool bEnabled);
    
    [DllImport("__Internal")]
    private static extern void Flurry_setSessionReportsOnPauseEnabled(bool bEnabled);
    
    [DllImport("__Internal")]
    private static extern void Flurry_setAge(int age);
    
    [DllImport("__Internal", CharSet = CharSet.Ansi)]
    private static extern void Flurry_setAppVersion([In, MarshalAs(UnmanagedType.LPStr)]string version);
    
    [DllImport("__Internal")]
    private static extern void Flurry_setSessionContinueSeconds(int seconds);
    
    [DllImport("__Internal", CharSet = CharSet.Ansi)]
    private static extern void Flurry_setUserID([In, MarshalAs(UnmanagedType.LPStr)]string userId);
    
    [DllImport("__Internal", CharSet = CharSet.Ansi)]
	private static extern void Flurry_logEvent([In, MarshalAs(UnmanagedType.LPStr)]string evendId, bool timed);
    
    [DllImport("__Internal", CharSet = CharSet.Ansi)]
    private static extern void Flurry_logEventWithParameters(
                [In, MarshalAs(UnmanagedType.LPStr)]string evendId, 
				[In, MarshalAs(UnmanagedType.LPStr)]string parameters, bool timed);
    
    [DllImport("__Internal", CharSet = CharSet.Ansi)]
    private static extern void Flurry_endTimedEvent([In, MarshalAs(UnmanagedType.LPStr)]string evendId);
    
#endregion
    
    public void StartSession(string API_KEY)
    {
        Flurry_setDebugLogEnabled(true);
        Flurry_setShowErrorInLogEnabled(true);
        Flurry_setEventLoggingEnabled(true);
        Flurry_startSession(API_KEY);
    }
    
    public void EndSession()
    {
        return ; // there is no such thing as endSession in iOS API
    }
    
    public int GetAgentVersion()
    {    
        string version = Flurry_getFlurryAgentVersion();
        if(version != null)
            return System.Convert.ToInt32(version);
        return 0;
    }
    
    public void LogEvent(string eventId, bool timed=false)
    {
        Flurry_logEvent(eventId, timed);
    }
    
    public void LogEvent(string eventId, Dictionary<string, string> parameters, bool timed = false)
    {
        string strParams = "";
        foreach(KeyValuePair<string, string> kvp in parameters)
        {
            strParams += kvp.Key +"="+kvp.Value+"\n";
        }
        if(timed == false)
            Flurry_logEventWithParameters(eventId, strParams);
        else
            Flurry_logEventWithParametersTimed(eventId, strParams);
    }
    
    public void EndTimedEvent(string eventId)
    {
        Flurry_endTimedEvent(eventId);
    }
    
    public void SetAge(int age)
    {
        Flurry_setAge(age);
    }
    
#else
    public void StartSession(string API_KEY)
    {
    
    }
    
    public void EndSession()
    {
    
    }
    
    public int GetAgentVersion()
    {
        return 0;
    }
    
    public void LogEvent(string eventId, bool timed=false)
    {
    
    }
    public void LogEvent(string eventId, Dictionary<string, string> parameters, bool timed=false)
    {
        
    }
    public void EndTimedEvent(string eventId)
    {
    
    }
#endif
}