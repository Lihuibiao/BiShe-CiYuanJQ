using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JiXie1 : MonoBehaviour
{
    public Vector3 PlayerEnterPos;
    public Image ImgBg;
    public Text MsgTxt;
    public JiXieXianQu Npc;
    private int CurDispIndex = 0;
    private List<TalkItem> FirstTalkList = new List<TalkItem>()
    {
        new TalkItem(){Name = "奥利" , Msg = "这就是机械先驱？" , TalkNPC = TalkNPC.Player1} ,
        new TalkItem(){Name = "托克" , Msg = "是的，让我们联手击败他。" , TalkNPC = TalkNPC.Player2} ,
        new TalkItem(){Name = "机械先驱" , Msg = "（愤怒++++）" , TalkNPC = TalkNPC.NPC} ,
    };
    
    private static bool alreadyTalk;
    // Start is called before the first frame update
    void Start()
    {
        PlayerController.Inst.transform.position = PlayerEnterPos;
        if (!alreadyTalk)
        {
            alreadyTalk = true;
            DispMsg();   
        }
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
            ImgBg.transform.Find("NPC").gameObject.SetActive(false);
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
        // PlayerController.Inst.enabled = true;
    }

    public GameObject BtnBg;
    public void ShowBtnBg()
    {
        BtnBg.gameObject.SetActive(true);
    }

    public void OnClickA()
    {
        BtnBg.gameObject.SetActive(false);
        OpenDoor();
    }
    
    public void OnClickB()
    {
        BtnBg.gameObject.SetActive(false);
        OpenDoor();
    }
    
    public void OnClickC()
    {
        BtnBg.gameObject.SetActive(false);
        OpenDoor();
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
