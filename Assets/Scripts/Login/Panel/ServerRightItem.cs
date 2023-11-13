using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ServerRightItem : MonoBehaviour
{
    public Button btnSelf;
    public Image imgNew;
    public Image imgState;
    public Text txtName;
    public ServerInfo nowServerInfo;

    private void Start()
    {
        btnSelf.onClick.AddListener(() =>
        {
            //记录当前选择的服务器
            LoginMgr.Instance.LoginData.frontServerID = nowServerInfo.id;

            UIManager.Instance.HidePanel<ChooseServerPanel>();
            UIManager.Instance.showPanel<ServerPanel>();
        });
    }

    public void InitInfo(ServerInfo info)
    {
        nowServerInfo = info;

        txtName.text = info.id + "区 " + info.name;

        imgNew.gameObject.SetActive(nowServerInfo.isNew);

        imgState.gameObject.SetActive(true);

        SpriteAtlas sa = Resources.Load<SpriteAtlas>("Login");
        switch (nowServerInfo.state)
        {
            case 0:
                imgState.gameObject.SetActive(false);
                break;
            case 1://流畅
                imgState.sprite = sa.GetSprite("ui_DL_liuchang_01");
                break;
            case 2://繁忙
                imgState.sprite = sa.GetSprite("ui_DL_fanhua_01");
                break;
            case 3://火爆
                imgState.sprite = sa.GetSprite("ui_DL_huobao_01");
                break;
            case 4://维护
                imgState.sprite = sa.GetSprite("ui_DL_weihu_01");
                break;
        }
    }
}