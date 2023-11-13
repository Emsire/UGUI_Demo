using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginMgr
{
    private static LoginMgr instance = new LoginMgr();
    public static LoginMgr Instance => instance;

    private LoginData loginData;
    public LoginData LoginData => loginData;

    private RegisterData registerData;
    public RegisterData RegisterData => registerData;

    private List<ServerInfo> serverData;
    public List<ServerInfo> ServerData => serverData;

    private LoginMgr()
    {
        loginData = JsonMgr.Instance.LoadData<LoginData>("LoginData");

        registerData = JsonMgr.Instance.LoadData<RegisterData>("RegisterData");

        serverData = JsonMgr.Instance.LoadData<List<ServerInfo>>("ServerInfo");
    }

    public void SaveLoginData()
    {
        JsonMgr.Instance.SaveData(loginData, "LoginData");
    }

    public void ClearLoginData()
    {
        loginData.frontServerID = 0;
        loginData.rememberPw = false;
        loginData.autoLogin = false;
    }

    public void SaveRegisterData()
    {
        JsonMgr.Instance.SaveData(registerData, "RegisterData");
    }

    
    //×¢²á·½·¨
    public bool RegisterUser(string userName, string passWord)
    {
        if (registerData.registerInfo.ContainsKey(userName)) return false;

        registerData.registerInfo.Add(userName, passWord);
        SaveRegisterData();

        return true;
    }

    public bool CheckInfo(string userName, string passWord)
    {
        if(registerData.registerInfo.ContainsKey(userName))
        {
            if(registerData.registerInfo[userName]==passWord)
            {
                return true;
            }
        }

        return false;
    }
}
