#import <stdio.h>
#include "Flurry.h"

void Flurry_startSession(unsigned char* apiKey)
{
  [Flurry startSession:[NSString stringWithUTF8String:apiKey]];
}

unsigned char* Flurry_getFlurryAgentVersion()
{
  return [Flurry getFlurryAgentVersion];
}

void Flurry_setShowErrorInLogEnabled(BOOL bEnabled)
{
  [Flurry setShowErrorInLogEnabled:bEnabled];
}

void Flurry_setDebugLogEnabled(BOOL bEnabled)
{
  [Flurry setDebugLogEnabled:bEnabled];
}

void Flurry_setEventLoggingEnabled(BOOL bEnabled)
{
  [Flurry setEventLoggingEnabled:bEnabled];
}

void Flurry_setAge(int age)
{
  [Flurry setAge:age];
}

void Flurry_setAppVersion(unsigned char* version)
{
  [Flurry setAppVersion:[NSString stringWithUTF8String:version]];
}

void Flurry_setGender(unsigned char *gender)
{
  [Flurry setGender:[NSString stringWithUTF8String:gender]];
}

void Flurry_setSessionContinueSeconds(int seconds)
{
  [Flurry setSessionContinueSeconds:seconds];
}

void Flurry_setSessionReportsOnCloseEnabled(BOOL bEnabled)
{
  [Flurry setSessionReportsOnCloseEnabled:bEnabled];
}

void Flurry_setSessionReportsOnPauseEnabled(BOOL bEnabled)
{
  [Flurry setSessionReportsOnPauseEnabled:bEnabled];
}

void Flurry_setUserID(unsigned char* userId)
{
  [Flurry setUserID:[NSString stringWithUTF8String:userId]];
}

void Flurry_logEvent(unsigned char* eventId, BOOL timed);
{
  [Flurry logEvent:[NSString stringWithUTF8String:eventId] timed:timed];
}

void Flurry_endTimedEvent(unsigned char* eventId)
{
  [Flurry endTimedEvent:[NSString stringWithUTF8String:eventId] withParameters:nil];
}

void Flurry_logEventWithParameters(unsigned char* eventId, unsigned char *parameters, BOOL timed);
{
  NSString *params = [NSString stringWithUTF8String:parameters];
  NSArray *arr = [params componentsSeparatedByString: @"\n"];
  NSMutableDictionary *pdict = [[[NSMutableDictionary alloc] init] autorelease];
  for(int i=0;i < [arr count]; i++)
  {
    NSString *str1 = [arr objectAtIndex:i];
    NSRange range = [str1 rangeOfString:@"="];
    if (range.location!=NSNotFound) {
      NSString *key = [str1 substringToIndex:range.location];
      NSString *val = [str1 substringFromIndex:range.location+1];
      [pdict setObject:val forKey:key];
    }
  }
  if([pdict count]>0)
    [Flurry logEvent:[NSString stringWithUTF8String:eventId] withParameters:pdict timed:timed];
  else
    Flurry_logEvent(eventId, timed);
}