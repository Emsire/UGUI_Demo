using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterPanel : BasePanel
{
    public Button btnSure;
    public Button btnCancel;

    public InputField inputUN;
    public InputField inputPW;

    public override void Init()
    {
        btnSure.onClick.AddListener(()=>
        {
            if(inputUN.text.Length<=6 || inputPW.text.Length<=6)
            {
                UIManager.Instance.showPanel<TipPanel>().ChangeInfo("账号和密码都必须大于6位");
                return;
            }

            if(LoginMgr.Instance.RegisterUser(inputUN.text, inputPW.text))
            {
                LoginMgr.Instance.ClearLoginData();

                LoginPanel loginPanel = UIManager.Instance.showPanel<LoginPanel>();
                loginPanel.SetInfo(inputUN.text, inputPW.text);

                UIManager.Instance.HidePanel<RegisterPanel>();
            }
            else
            {
                UIManager.Instance.showPanel<TipPanel>().ChangeInfo("账号已存在");
                inputUN.text = "";
                inputPW.text = "";
            }
        });

        btnCancel.onClick.AddListener(()=>
        {
            UIManager.Instance.showPanel<LoginPanel>();
            UIManager.Instance.HidePanel<RegisterPanel>();
        });
    }
}
