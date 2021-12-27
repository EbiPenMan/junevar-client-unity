using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using ProGraphGroup.General.Models;
using UnityEngine;
using UnityEngine.Networking;
using Object = System.Object;

namespace ProGraphGroup.General.Utility.Network
{
    public enum RestApiMethods
    {
        GET,
        POST,
        PUT,
        DELETE,
        PATCH,
    }

    public class RestApi<TResponse>
    {
        private string url;
        private MonoBehaviour monoBehaviour;
        private RestApiMethods restApiMethod;
        private Dictionary<string, string> headers = new Dictionary<string, string>();
        private Dictionary<string, string> query = new Dictionary<string, string>();
        private Object bodyData = null;

        private Action<TResponse> onDone;
        private Action<ErrorModel> onError;

        public RestApi(MonoBehaviour monoBehaviour, RestApiMethods restApiMethod, string url)
        {
            this.monoBehaviour = monoBehaviour;
            this.url = url;
            this.restApiMethod = restApiMethod;
        }

        public RestApi<TResponse> addHeader(string key, string value)
        {
            headers.Add(key, value);
            return this;
        }

        public RestApi<TResponse> addQuery(string key, string value)
        {
            query.Add(key, value);
            return this;
        }

        public RestApi<TResponse> addBodyData(object data)
        {
            bodyData = data;
            return this;
        }

        public RestApi<TResponse> setOnDone(Action<TResponse> onDone)
        {
            this.onDone = onDone;
            return this;
        }

        public RestApi<TResponse> setOnError(Action<ErrorModel> onError)
        {
            this.onError = onError;
            return this;
        }

        public void Send()
        {
            monoBehaviour.StartCoroutine((string) SendPost());
        }

        private IEnumerable SendPost()
        {
            string bodyDataJsonStr = "null";
            if (bodyData != null)
            {
                //Type t = bodyData.GetType();
                //bool isDict = t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Dictionary<,>);
                //if (isDict)
                //{
                bodyDataJsonStr = JsonConvert.SerializeObject(bodyData);
                //}
                //else
                //{
                //    bodyDataJsonStr = JsonUtility.ToJson(bodyData);
                //}

                Debug.Log("bodyDataJsonStr: " + bodyDataJsonStr);
            }

            addHeader("Content-Type", "application/json");
            addHeader("Accept", "application/json");

            Debug.Log("url: " + url + queryToString());

            using (UnityWebRequest req = UnityWebRequest.Put(url + queryToString(), bodyDataJsonStr))
            {
                foreach (KeyValuePair<string, string> entry in headers)
                {
                    req.SetRequestHeader(entry.Key, entry.Value);
                }

                req.method = UnityWebRequest.kHttpVerbPOST;

                yield return req.Send();
                while (!req.isDone)
                    yield return null;

                if (req.downloadHandler.data == null)
                {
                    if (onError != null)
                        onError.Invoke(new ErrorModel(-1000, "result null"));
                    Debug.LogError(string.Format("Message: {0}, StackTrace: {1}", "result null", ""));
                    yield break;
                }

                try
                {
                    byte[] result = req.downloadHandler.data;
                    string resStr = System.Text.Encoding.UTF8.GetString(result);
                    TResponse resModel =
                        JsonConvert.DeserializeObject<TResponse>(resStr);
                    Debug.Log("RestApiPostRequest res str: " + resStr);
                    onDone(resModel);
                }
                catch (Exception ex)
                {
                    Debug.LogError(string.Format("Message: {0}, StackTrace: {1}", ex.Message, ex.StackTrace));
                    if (onError != null)
                        onError.Invoke(new ErrorModel(-2000,
                            string.Format("Message: {0}, StackTrace: {1}", ex.Message, ex.StackTrace)));
                }
            }
        }

        private IEnumerable SendGet()
        {
            addHeader("Content-Type", "application/json");
            addHeader("Accept", "application/json");


            using (UnityWebRequest req = UnityWebRequest.Get(url + queryToString())
            )
            {
                foreach (KeyValuePair<string, string> entry in headers)
                {
                    req.SetRequestHeader(entry.Key, entry.Value);
                }

                yield return req.Send();
                while (!req.isDone)
                    yield return null;

                if (req.downloadHandler.data == null)
                {
                    if (onError != null)
                        onError.Invoke(new ErrorModel(-1000, "result null"));
                    Debug.LogError(string.Format("Message: {0}, StackTrace: {1}", "result null", ""));
                    yield break;
                }

                try
                {
                    byte[] result = req.downloadHandler.data;
                    string resStr = System.Text.Encoding.UTF8.GetString(result);

                    Debug.Log("RestApiGetRequest res str: " + resStr);


                    TResponse resModel = JsonConvert.DeserializeObject<TResponse>(resStr);

                    // object info = JsonUtility.FromJson<object>(resStr);
                    Debug.Log("RestApiGetRequest res str: " + resStr);
                    onDone(resModel);
                }
                catch (Exception ex)
                {
                    Debug.LogError(string.Format("Message: {0}, StackTrace: {1}", ex.Message, ex.StackTrace));
                    if (onError != null)
                        onError.Invoke(new ErrorModel(-2000,
                            string.Format("Message: {0}, StackTrace: {1}", ex.Message, ex.StackTrace)));
                }
            }
        }

        public string queryToString()
        {
            string queryStr = "";
            if (query.Count > 0)
            {
                foreach (KeyValuePair<string, string> entry in query)
                {
                    if (queryStr == "")
                    {
                        queryStr += "?" + entry.Key + "=" + entry.Value;
                    }
                    else
                    {
                        queryStr += "&" + entry.Key + "=" + entry.Value;
                    }
                }
            }

            return queryStr;
        }
    }
}