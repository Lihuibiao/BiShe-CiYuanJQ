using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiXiaGongDian2 : MonoBehaviour
{
    private List<TalkItem> FirstTalkList = new List<TalkItem>()
    {
        new TalkItem(){Name = "奥利" , Msg = "我似乎嗅到了危险的气息" , TalkNPC = TalkNPC.Player1} ,
        new TalkItem(){Name = "托克" , Msg = "似乎机械先驱就在附近，但这个房间似乎没有出口。" , TalkNPC = TalkNPC.Player2} , 
    };

    public Vector3 PlayerEnterPos;
    private void Start()
    {
        PlayerController.Inst.transform.position = PlayerEnterPos;
    }

    public Image ImgBg;
    public Text MsgTxt;
    private int CurDispIndex = 0;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && ImgBg.gameObject.activeSelf)
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
        TalkItem item = null;
        if (FirstTalkList.Count > 0)
        {
            item = FirstTalkList[CurDispIndex++];   
        }

        if (item != null)
        {
            PlayerController.Inst.enabled = false;
            PlayerController.Inst.SetIdle();
            ImgBg.gameObject.SetActive(true);
            ImgBg.transform.Find("Player1").gameObject.SetActive(false);
            ImgBg.transform.Find("Player2").gameObject.SetActive(false);
            ImgBg.transform.Find(item.TalkNPC.ToString()).gameObject.SetActive(true);
            MsgTxt.text = item.Name + ":" + item.Msg;   
        }
    }
    
    private void HideMsg()
    {
        if (FirstTalkList.Count > 0)
        {
            FirstTalkList.Clear();
        }
        CurDispIndex = 0;
        ImgBg.gameObject.SetActive(false);
        PlayerController.Inst.enabled = true;
    }
}
