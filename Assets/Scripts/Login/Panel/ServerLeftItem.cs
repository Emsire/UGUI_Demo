using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerLeftItem : MonoBehaviour
{
    public Button btnSelf;
    public Text txtInfo;

    private int beginIndex;
    private int endIndex;

    private void Start()
    {
        btnSelf.onClick.AddListener(()=>
        {
            //通知选服面板 改变右侧的区间内容
            ChooseServerPanel panel = UIManager.Instance.GetPanel<ChooseServerPanel>();
            panel.UpdateInfo(beginIndex, endIndex);
        });
    }

    public void InitInfo(int beginIndex, int endIndex)
    {
        this.beginIndex = beginIndex;
        this.endIndex = endIndex;

        txtInfo.text = beginIndex + " - " + endIndex + " 区";
    }
}
