using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ServerPanel : BasePanel
{
    public Button btnChange;
    public Button btnStart;
    public Button btnBack;

    public Text txtName;
    public override void Init()
    {
        btnBack.onClick.AddListener(() =>
        {
            if (LoginMgr.Instance.LoginData.autoLogin)
                LoginMgr.Instance.LoginData.autoLogin = false;

            UIManager.Instance.showPanel<LoginPanel>();
            UIManager.Instance.HidePanel<ServerPanel>();
        });

        btnStart.onClick.AddListener(()=>
        {
            UIManager.Instance.HidePanel<ServerPanel>();
            UIManager.Instance.HidePanel<LoginBKPanel>();

            LoginMgr.Instance.SaveLoginData();
            SceneManager.LoadScene("GameScene");
        });

        btnChange.onClick.AddListener(() =>
        {
            UIManager.Instance.showPanel<ChooseServerPanel>();
            UIManager.Instance.HidePanel<ServerPanel>();
        });
    }

    public override void showMe()
    {
        base.showMe();

        int id = LoginMgr.Instance.LoginData.frontServerID;
        if(id<=0)
        {
            txtName.text = "ÎÞÑ¡Ôñ";
        }
        else
        {
            ServerInfo info = LoginMgr.Instance.ServerData[id - 1];
            txtName.text = info.id + "Çø " + info.name;
        }
    }
}
