using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ChooseServerPanel : BasePanel
{
    public ScrollRect svLeft;
    public ScrollRect svRight;

    public Text txtName;
    public Image imgState;

    public Text txtRange;

    private List<GameObject> itemList = new List<GameObject>();

    public override void Init()
    {
        //动态创建左侧按钮
        List<ServerInfo> infoList = LoginMgr.Instance.ServerData;
        int num = infoList.Count / 5 + 1;
        for(int i=0;i<num;i++)
        {
            GameObject item = Instantiate(Resources.Load<GameObject>("UI/ServerLeftItem"));
            item.transform.SetParent(svLeft.content, false);

            ServerLeftItem serverLeft = item.GetComponent<ServerLeftItem>();
            int beginIndex = i * 5 + 1;
            int endIndex = (i + 1) * 5;
            if (endIndex > infoList.Count)
                endIndex = infoList.Count;
            serverLeft.InitInfo(beginIndex, endIndex);
        }
    }

    public override void showMe()
    {
        base.showMe();
        //动态创建右侧按钮

        int id = LoginMgr.Instance.LoginData.frontServerID;
        if(id<=0)
        {
            txtName.text = "无";
            imgState.gameObject.SetActive(false);
        }
        else
        {
            ServerInfo info = LoginMgr.Instance.ServerData[id - 1];
            txtName.text = info.id + "区 " + info.name;
            imgState.gameObject.SetActive(true);
            SpriteAtlas sa = Resources.Load<SpriteAtlas>("Login");
            switch (info.state)
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

        UpdateInfo(1, 5 > LoginMgr.Instance.ServerData.Count?LoginMgr.Instance.ServerData.Count:5);
    }

    public void UpdateInfo(int beginIndex, int endIndex)
    {
        txtRange.text = "服务器 " + beginIndex + "-" + endIndex;

        for(int i=0;i<itemList.Count;i++)
        {
            Destroy(itemList[i]);
        }
        itemList.Clear();

        for(int i=beginIndex;i<=endIndex;i++)
        {
            ServerInfo nowInfo = LoginMgr.Instance.ServerData[i - 1];
            GameObject serverInfo = Instantiate(Resources.Load<GameObject>("UI/ServerRightItem"));
            serverInfo.transform.SetParent(svRight.content, false);

            ServerRightItem rightItem = serverInfo.GetComponent<ServerRightItem>();
            rightItem.InitInfo(nowInfo);

            itemList.Add(serverInfo);
        }
    }
}
