using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
    public Button btnRegister;
    public Button btnSure;

    public InputField inputUN;
    public InputField inputPW;

    public Toggle togPW;
    public Toggle togAuto;
    public override void Init()
    {
        btnRegister.onClick.AddListener(()=>
        {
            //显示注册面板
            UIManager.Instance.showPanel<RegisterPanel>();

            UIManager.Instance.HidePanel<LoginPanel>();
        });

        btnSure.onClick.AddListener(() =>
        {
            //验证用户名和密码是否正确
            if (inputUN.text.Length <= 6 || inputPW.text.Length <= 6)
            {
                UIManager.Instance.showPanel<TipPanel>().ChangeInfo("账号和密码都必须大于6位");
                return;
            }

            if (LoginMgr.Instance.CheckInfo(inputUN.text, inputPW.text))
            {
                LoginMgr.Instance.LoginData.userName = inputUN.text;
                LoginMgr.Instance.LoginData.password = inputPW.text;
                LoginMgr.Instance.LoginData.rememberPw = togPW.isOn;
                LoginMgr.Instance.LoginData.autoLogin = togAuto.isOn;
                LoginMgr.Instance.SaveLoginData();

                //根据服务器信息进行判断 选择那个面板
                if(LoginMgr.Instance.LoginData.frontServerID<=0)
                {
                    //如果从来没有打开过服务器 直接打开选服面板
                    UIManager.Instance.showPanel<ChooseServerPanel>();
                }
                else
                {
                    UIManager.Instance.showPanel<ServerPanel>();
                }

                UIManager.Instance.HidePanel<LoginPanel>();
            }
            else
            {
                UIManager.Instance.showPanel<TipPanel>().ChangeInfo("账号或密码错误");
            }
        });

        togPW.onValueChanged.AddListener((isOn) =>
        {
            if(!isOn)
            {
                togAuto.isOn = false;
            }
        });

        togAuto.onValueChanged.AddListener((isOn) =>
        {
            if(isOn)
            {
                togPW.isOn = true;
            }
        });
    }

    public override void showMe()
    {
        base.showMe();

        LoginData loginData = LoginMgr.Instance.LoginData;

        togPW.isOn = loginData.rememberPw;
        togAuto.isOn = loginData.autoLogin;

        inputUN.text = loginData.userName;
        if(togPW.isOn)
            inputPW.text = loginData.password;

        if(togAuto.isOn)
        {
            //自动去验证账号密码相关
            if(LoginMgr.Instance.CheckInfo(inputUN.text, inputPW.text))
            {
                if(LoginMgr.Instance.LoginData.frontServerID<=0)
                {
                    UIManager.Instance.showPanel<ChooseServerPanel>();
                }
                else
                {
                    UIManager.Instance.showPanel<ServerPanel>();
                }

                UIManager.Instance.HidePanel<LoginPanel>(false);
            }
            else
            {
                UIManager.Instance.showPanel<TipPanel>().ChangeInfo("账户或密码错误");
            }
        }
    }

    public void SetInfo(string userName, string passWord)
    {
        inputUN.text = userName;
        inputPW.text = passWord;
    }
}
