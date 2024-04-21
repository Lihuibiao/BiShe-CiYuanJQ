using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JiXieGongChang : MonoBehaviour
{
    public Vector3 PlayerEnterPos;
    public Image ImgBg;
    public Text MsgTxt;
    public JiXieShouWei Npc;
    private int CurDispIndex = 0;
    
    private List<TalkItem> FirstTalkList = new List<TalkItem>()
    {
        new TalkItem(){Name = "托克" , Msg = "我看到机械守卫后面有一把钥匙，似乎是机械宫殿一层的钥匙" , TalkNPC = TalkNPC.Player2} ,
        new TalkItem(){Name = "奥利" , Msg = "但想拿到钥匙就必须击败他" , TalkNPC = TalkNPC.Player1} ,
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
        BossMsg.text = "(机械守卫因中枢芯片被取出而死亡)";
        OpenDoor();
        this.Invoke("delay2CloseBtnBg" , 2);
    }
    
    public void OnClickB()
    {
        BossMsg.text = "(机械守卫:任务…失&@“/\u00a3•败)";
        OpenDoor();
        this.Invoke("delay2CloseBtnBg" , 2);
    }
    
    public void OnClickC()
    {
        BossMsg.text = "((系统提示:修改时触发了机械守卫的自动报警装置，机械先驱的警惕性提高。))";
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
