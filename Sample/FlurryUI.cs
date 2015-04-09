using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlurryUI : MonoBehaviour {
	
	private string FLURRY_API_KEY = "YOUR_API_KEY";	
	private Flurry flurry = null;
	
	void Start () {
		flurry = new Flurry();
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
	}
	
	void OnGUI()
	{ 
		GUILayout.BeginVertical();
		
		if(GUILayout.Button("Start Flurry",GUILayout.Height(60)))
		{
			flurry.StartSession(FLURRY_API_KEY);
		}
		if(GUILayout.Button("Agent Version",GUILayout.Height(60)))
		{
			Debug.Log("Agent version is " + flurry.GetAgentVersion());
		}
		if(GUILayout.Button("Log Event 1", GUILayout.Height(60)))
		{
			flurry.LogEvent("Event 2");
		}
		if(GUILayout.Button("Log Event 1 with params", GUILayout.Height(60)))
		{
			Dictionary<string, string> kvps = new Dictionary<string, string>();
			kvps.Add("GameName", "MyGame");
			kvps.Add("Level", "Level1");
			flurry.LogEvent("Event 3", kvps);
		}
		
		if(GUILayout.Button("Timed Log Event 1 with params", GUILayout.Height(60)))
		{
			Dictionary<string, string> kvps = new Dictionary<string, string>();
			kvps.Add("GameName", "MyGame");
			kvps.Add("Level", "Level1");
			flurry.LogEvent("Timer 2", kvps,true);
		}
		
		if(GUILayout.Button("Timed Event Start", GUILayout.Height(60)))
		{
			Dictionary<string, string> kvps = new Dictionary<string, string>();
			kvps.Add("TGameName", "MyGame");
			kvps.Add("TLevel", "Level1");
			flurry.LogEvent("Timer 2", kvps,true);
		}
		
		if(GUILayout.Button("Timed Event End", GUILayout.Height(60)))
		{
			flurry.EndTimedEvent("Timer 2");
		}
		
		if(GUILayout.Button("End Session", GUILayout.Height(60)))
		{
			flurry.EndSession();
		}
		
		GUILayout.EndVertical();	
	}
	
	void OnApplicationQuit()
	{
		flurry.EndSession();
	}
}
