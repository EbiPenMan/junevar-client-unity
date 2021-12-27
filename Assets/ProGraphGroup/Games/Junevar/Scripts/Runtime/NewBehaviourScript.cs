using System.Collections.Generic;
using System.Threading.Tasks;
using Nakama;
using UnityEngine;

namespace ProGraphGroup.Games.Junevar
{
    public class NewBehaviourScript : MonoBehaviour
    {
        // Start is called before the first frame update
        async Task Start()
        {
            // using Nakama;
            const string scheme = "http";
            const string host = "127.0.0.1";
            const int port = 7350;
            const string serverKey = "defaultkey";
            var client = new Client(scheme, host, port, serverKey);

            var deviceId = SystemInfo.deviceUniqueIdentifier;
            var session = await client.AuthenticateDeviceAsync(deviceId);
            Debug.Log(session);

            Debug.Log(session.AuthToken); // raw JWT token
            Debug.LogFormat("Session user id: '{0}'", session.UserId);
            Debug.LogFormat("Session user username: '{0}'", session.Username);
            Debug.LogFormat("Session has expired: {0}", session.IsExpired);
            Debug.LogFormat("Session expires at: {0}", session.ExpireTime); // in seconds.


            var saveGame = "{ \"progress\": 50 }";
            var myStats = "{ \"skill\": 24 }";

            List<IApiWriteStorageObject> list = new List<IApiWriteStorageObject>();
            list.Add(new WriteStorageObject()
            {
                Collection = "saves",
                Key = "savegame",
                Value = saveGame
            });
            list.Add(new WriteStorageObject()
            {
                Collection = "stats",
                Key = "skills",
                Value = myStats
            });

            var objectIds = await client.WriteStorageObjectsAsync(session, list.ToArray());
            Debug.LogFormat("Successfully stored objects: [{0}]", string.Join(",\n   ", objectIds));
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}