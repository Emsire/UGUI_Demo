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
            //֪ͨѡ����� �ı��Ҳ����������
            ChooseServerPanel panel = UIManager.Instance.GetPanel<ChooseServerPanel>();
            panel.UpdateInfo(beginIndex, endIndex);
        });
    }

    public void InitInfo(int beginIndex, int endIndex)
    {
        this.beginIndex = beginIndex;
        this.endIndex = endIndex;

        txtInfo.text = beginIndex + " - " + endIndex + " ��";
    }
}
