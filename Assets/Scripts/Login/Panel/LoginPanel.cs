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
            //��ʾע�����
            UIManager.Instance.showPanel<RegisterPanel>();

            UIManager.Instance.HidePanel<LoginPanel>();
        });

        btnSure.onClick.AddListener(() =>
        {
            //��֤�û����������Ƿ���ȷ
            if (inputUN.text.Length <= 6 || inputPW.text.Length <= 6)
            {
                UIManager.Instance.showPanel<TipPanel>().ChangeInfo("�˺ź����붼�������6λ");
                return;
            }

            if (LoginMgr.Instance.CheckInfo(inputUN.text, inputPW.text))
            {
                LoginMgr.Instance.LoginData.userName = inputUN.text;
                LoginMgr.Instance.LoginData.password = inputPW.text;
                LoginMgr.Instance.LoginData.rememberPw = togPW.isOn;
                LoginMgr.Instance.LoginData.autoLogin = togAuto.isOn;
                LoginMgr.Instance.SaveLoginData();

                //���ݷ�������Ϣ�����ж� ѡ���Ǹ����
                if(LoginMgr.Instance.LoginData.frontServerID<=0)
                {
                    //�������û�д򿪹������� ֱ�Ӵ�ѡ�����
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
                UIManager.Instance.showPanel<TipPanel>().ChangeInfo("�˺Ż��������");
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
            //�Զ�ȥ��֤�˺��������
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
                UIManager.Instance.showPanel<TipPanel>().ChangeInfo("�˻����������");
            }
        }
    }

    public void SetInfo(string userName, string passWord)
    {
        inputUN.text = userName;
        inputPW.text = passWord;
    }
}
