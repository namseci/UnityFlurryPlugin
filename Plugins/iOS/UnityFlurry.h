#ifndef Unity_iPhone_Flurry_h
#define Unity_iPhone_Flurry_h


extern "C" {
  void Flurry_startSession(unsigned char* apiKey);
  
  unsigned char* Flurry_getFlurryAgentVersion();
  void Flurry_setShowErrorInLogEnabled(BOOL bEnabled);
  void Flurry_setDebugLogEnabled(BOOL bEnabled);
  void Flurry_setEventLoggingEnabled(BOOL bEnabled);
  void Flurry_setAge(int age);
  void Flurry_setAppVersion(unsigned char* version);
  
  void Flurry_setGender(unsigned char *gender);
  void Flurry_setSessionContinueSeconds(int seconds);
  void Flurry_setSessionReportsOnCloseEnabled(BOOL bEnabled);
  void Flurry_setSessionReportsOnPauseEnabled(BOOL bEnabled);
  void Flurry_setUserID(unsigned char* userId);
  void Flurry_logEvent(unsigned char* eventId, BOOL timed);
  void Flurry_logEventWithParameters(unsigned char* eventId,unsigned char *parameters, BOOL timed);
  void Flurry_endTimedEvent(unsigned char* eventId);
}

#endif
