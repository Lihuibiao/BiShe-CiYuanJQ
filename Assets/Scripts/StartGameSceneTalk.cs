using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TalkNPC
{
    Player1 , 
    Player2 , 
    NPC,
}
[System.Serializable]
public class TalkItem
{
    public string Name;
    public string Msg;
    public TalkNPC TalkNPC;
}

public class StartGameSceneTalk : MonoBehaviour
{
    public static StartGameSceneTalk Inst;
    public Vector3 PlayerEnterPos;
    private void Awake()
    {
        Inst = this;
    }

    private List<TalkItem> FirstTalkList = new List<TalkItem>()
    {
        new TalkItem(){Name = "奥利" , Msg = "这里是？" , TalkNPC = TalkNPC.Player1} ,
        new TalkItem(){Name = "主角2" , Msg = "你似乎身处于一片废墟中." , TalkNPC = TalkNPC.Player2} , 
        new TalkItem(){Name = "奥利" , Msg = "你是谁？" , TalkNPC = TalkNPC.Player1} , 
        new TalkItem(){Name = "主角2" , Msg = "我通过一个思维侵入装置来进入你的脑海中，我是次元纪年2024年的人。" , TalkNPC = TalkNPC.Player2} , 
        new TalkItem(){Name = "奥利" , Msg = "2024？我这里是次元纪年2124年" , TalkNPC = TalkNPC.Player1} ,
        new TalkItem(){Name = "主角2" , Msg = "我可以通过这个装置来进入你的思维，但是同时你的位置也影响我在我的时间所在的位置，并且你身上似乎也有这个装置。" , TalkNPC = TalkNPC.Player2} ,
        new TalkItem(){Name = "奥利" , Msg = "我前面有一块紫色发光的石头挡住了我的去路" , TalkNPC = TalkNPC.Player1} ,
        new TalkItem(){Name = "主角2" , Msg = "我的前方没有障碍物，如果侵入我的思维也能同时改变你的位置，不妨试着侵入我的思维。" , TalkNPC = TalkNPC.Player2} ,
        new TalkItem(){Name = "系统提示" , Msg = "A和D控制移动,W控制跳跃,按下Q键触发思维侵入装置,思维传送装置会消耗能量(能量通过攻击怪物恢复)当能量不足时会被强行拉回传送思维侵入前的时间。"} ,
    };
    
    // 废墟：
    // 出生  触发对话(奥利与主角2)
    // 奥利:这里是？
    // 主角2:你似乎身处于一片废墟中。
    // 奥利:你是谁？！
    // 主角2:我通过一个思维侵入装置来进入你的脑海中，我是次元纪年2024年的人。
    // 奥利:2024？我这里是次元纪年2124年
    //     主角2:我可以通过这个装置来进入你的思维，但是同时你的位置也影响我在我的时间所在的位置，并且你身上似乎也有这个装置。
    // 奥利:我前面有一块紫色发光的石头挡住了我的去路
    //     主角2:我的前方没有障碍物，如果侵入我的思维也能同时改变你的位置，不妨试着侵入我的思维。

    public Image ImgBg;
    public Text MsgTxt;
    private int CurDispIndex = 0;
    private static bool alreadyTalk;
    private void Start()
    {
        PlayerController.Inst.enabled = false;
        if (!alreadyTalk)
        {
            alreadyTalk = true;
            DispMsg();   
        }
        PlayerController.Inst.transform.position = PlayerEnterPos;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (FirstTalkList.Count > CurDispIndex)
            {
                DispMsg();
            }
            else
            {
                HideMsg();
            }
        }
    }

    private void DispMsg()
    {
        var item = FirstTalkList[CurDispIndex++];
        ImgBg.gameObject.SetActive(true);
        ImgBg.transform.Find("Player1").gameObject.SetActive(false);
        ImgBg.transform.Find("Player2").gameObject.SetActive(false);
        ImgBg.transform.Find(item.TalkNPC.ToString()).gameObject.SetActive(true);
        MsgTxt.text = item.Name + ":" + item.Msg;
    }

    private void HideMsg()
    {
        ImgBg.gameObject.SetActive(false);
        PlayerController.Inst.enabled = true;
    }
}
