using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Scene4 : MonoBehaviour
{
    public Vector3 PlayerEnterPos;
    private static bool alreadyTalk;
    void Start()
    {
        PlayerController.Inst.transform.position = PlayerEnterPos;
        if (!alreadyTalk)
        {
            alreadyTalk = true;
            DispMsg();   
        }
    }

    public Image ImgBg;
    public Text MsgTxt;
    private int CurDispIndex = 0;
    
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
    
    
    private List<TalkItem> FirstTalkList = new List<TalkItem>()
    {
        new TalkItem(){Name = "奥利" , Msg = "这里是哪儿？前面似乎有个迷路的人" , TalkNPC = TalkNPC.Player1} ,
        new TalkItem(){Name = "托克" , Msg = "已经快接近机械蝇的地盘了，万事小心。" , TalkNPC = TalkNPC.Player2} , 
    };
    
    
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
