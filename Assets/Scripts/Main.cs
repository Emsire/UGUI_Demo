using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    void Start()
    {
        //TipPanel tipPanel = UIManager.Instance.showPanel<TipPanel>();
        //tipPanel.ChangeInfo("��ʾ���ݸı�");

        UIManager.Instance.showPanel<LoginBKPanel>();
        UIManager.Instance.showPanel<LoginPanel>();
    }

    void Update()
    {
        
    }
}
