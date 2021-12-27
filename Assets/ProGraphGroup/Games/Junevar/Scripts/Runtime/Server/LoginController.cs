using ProGraphGroup.Games.Junevar.Server.LoginPlatform.Base;
using ProGraphGroup.Games.Junevar.Server.Models;
using UnityEngine;

namespace ProGraphGroup.Games.Junevar.Server
{
    public class LoginController : MonoBehaviour
    {
                private LoginType getLastLoginType()
                {
                    return PlayerPrefs.GetInt("lastLoginType") is LoginType
                        ? (LoginType) PlayerPrefs.GetInt("lastLoginType")
                        : LoginType.None;
                }

    }
}