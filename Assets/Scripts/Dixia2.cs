using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Dixia2 : MonoBehaviour
{
    public Vector3 PlayerEnterPos;
    public Image ImgBg;
    public Text MsgTxt;
    public JiXieYing Npc;
    private int CurDispIndex = 0;
    
    private List<TalkItem> FirstTalkList = new List<TalkItem>()
    {
        new TalkItem(){Name = "奥利" , Msg = "这就是机械蝇吗" , TalkNPC = TalkNPC.Player1} ,
        new TalkItem(){Name = "托克" , Msg = "是的，似乎只有击败他才能通过此地,小心机械蝇的利爪" , TalkNPC = TalkNPC.Player2} ,
    };
    
    void Start()
    {
        PlayerController.Inst.transform.position = PlayerEnterPos;
        DispMsg();
    }

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
        Npc.Move2Player();
    }
    
    public GameObject BtnBg;
    public Text BossMsg;
    public GameObject ChuanSongMen;
    public void ShowBtnBg()
    {
        BtnBg.gameObject.SetActive(true);
        if (ChuanSongMen != null)
        {
            ChuanSongMen.gameObject.SetActive(true);   
        }
    }

    public void OnClickA()
    {
        BossMsg.text = "(机械蝇:感谢你帮我脱离控制)";
        OpenDoor();
        this.Invoke("delay2CloseBtnBg" , 2);
    }
    
    public void OnClickB()
    {
        BossMsg.text = "(机械蝇:如此精湛的演技也没能骗过你))";
        OpenDoor();
        this.Invoke("delay2CloseBtnBg" , 2);
    }
    
    public void OnClickC()
    {
        BossMsg.text = "(系统提示:在你犹豫的期间机械蝇逃跑了)";
        OpenDoor();
        this.Invoke("delay2CloseBtnBg" , 2);
    }

    private void delay2CloseBtnBg()
    {
        BtnBg.gameObject.SetActive(false);
    }

    public List<Animator> doors;

    [InspectorButton]
    public void OpenDoor()
    {
        foreach (var door in doors)
        {
            door.enabled = true;
        }
        this.Invoke("delayCloseDoor" , 1.5f);
    }

    private void delayCloseDoor()
    {
        foreach (var door in doors)
        {
            door.gameObject.SetActive(false);
        }
    }
}
