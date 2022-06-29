using CorePush.Apple;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
   public static class CommonStaticStrings
    {
       // public static string ImageUrlServer = "http://mtmohali.dyndns.org:2021/";
        public static string ImageUrlServer = "C:/HostedApplications/Qbubble/Current/wwwroot";
        public static string SingleAnswerDefault = "Multiple Option Answer";
        public static string FCMAppName = "Qbubble";
        //public static string FCMAuthCredentials = "C:\\Users\\Hosting\\Downloads\\Sourabh\\Qbubble\\WebApiApp\\Auth.json";
        public static string FCMAuthCredentials = "C:\\Users\\QbubblePublished\\Auth.json";
        public static double BubbleSaftyLevel = 100;
        public static string BubbleSaftyUrl1 = "https://api.covidactnow.org/v2/county/";
        public static string BubbleSaftyUrl2 = ".json?apiKey=d953777cd4c04369af5465a8c01cb1e7";
        public static string AccountSid = "ACfa01cfc4882d310f6d4a05862a23631f";
        public static string AuthToken = "ba3ef4518a6fb5e30ddbfdc18719a87b";
        public static string ServiceSid = "VA60e836a1198ec1eb1b73c45b7ccd8528";
        public static string Sms = "sms";
        public static string apnBundleId = "com.mechlintech.qbubble";
        public static string apnP8PrivateKey = "MIGTAgEAMBMGByqGSM49AgEGCCqGSM49AwEHBHkwdwIBAQQg3GvVwvsECL0+YIc0nATwQvpW1xnX+GNMMuOBNfnyPnKgCgYIKoZIzj0DAQehRANCAATjfaPG8ToRzFskD3JiIt9plFtNNsv4CwdYwLT25rWw0DfJcEarlsCZAAq8A5YJxrbJXrxIUY2twVGPLpZuEpAF";
        public static string apnP8PrivateKeyId = "JSKKWAA47B";
        public static string apnTeamId = "BE4VCG6W8P";
        public static ApnServerType apnServerType = ApnServerType.Development;
    }
}
