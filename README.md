# UnityFlurryPlugin
Unity Plugin to work with Flurry SDK v5.4.0

Flurry - https://developer.yahoo.com/flurry/

##Usage:
Flurry.cs script has several function:
- StartSession(string API_KEY)
- EndSession()
- GetAgentVersion()
- LogEvent(string eventId, bool timed = false)
- LogEvent(string eventId, Dictionary<string, string> parameters, bool timed = false)
- EndTimedEvent()

##Sample usage:
add FlurryUI.cs to any object
