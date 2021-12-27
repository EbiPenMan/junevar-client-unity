using System;
using System.Collections;
using ProGraphGroup.General.Models;
using UnityEngine;
using UnityEngine.Networking;

namespace ProGraphGroup.General.Utility
{
    public sealed class GeneralUtils
    {
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);

            if (unixTimeStamp.ToString().Length < 12)
                unixTimeStamp = Double.Parse(unixTimeStamp + "000");
            dtDateTime = dtDateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public static IEnumerator LoadImageAsTexture(string picturePathAndName, bool isExternal,
            Action<Texture2D> onDone, Action<ErrorModel> onError)
        {
            if (!isExternal)
            {
                try
                {
                    Debug.Log("Loading From Inside");
                    Texture2D texture2D = Resources.Load(picturePathAndName, typeof(Texture2D)) as Texture2D;
                    onDone(texture2D);
                }
                catch (Exception e)
                {
                    onError(new ErrorModel(e.HResult, e.Message));
                    yield break;
                }

                yield return true;
            }
            else
            {
                Debug.Log("Loading From Outside: " + picturePathAndName);
                using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(picturePathAndName))
                {
                    yield return www.SendWebRequest();
                    try
                    {
                        Texture2D texture2D = DownloadHandlerTexture.GetContent(www);
                        onDone(texture2D);
                    }
                    catch (Exception e)
                    {
                        onError(new ErrorModel(e.HResult, e.Message));
                        yield break;
                    }
                }

                yield return true;
            }
        }
    }
}