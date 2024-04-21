using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Dengta : MonoBehaviour
{
    public Vector3 PlayerEnterPos;
    public Image ImgBg;
    public Text MsgTxt;
    private int CurDispIndex = 0;
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

    private List<TalkItem> FirstTalkList = new List<TalkItem>()
    {
        new TalkItem(){Name = "托克" , Msg = "在地刺上似乎有个宝箱。" , TalkNPC = TalkNPC.Player2} , 
        new TalkItem(){Name = "奥利" , Msg = "这里是哪儿？前面似乎有个迷路的人" , TalkNPC = TalkNPC.Player1} ,
        new TalkItem(){Name = "托克" , Msg = "系统提示:奖励充满诱惑，同时也充满危险" , TalkNPC = TalkNPC.Player2} ,
    };
    
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

        if (playerInTrig && Input.GetKeyDown(KeyCode.F) && !DianTiAni.enabled)
        {
            DianTiAni.enabled = true;
            this.Invoke("delay2CloseDianTiAni" , 1.4f);
        }
    }

    public Animator DianTiAni;

    private void delay2CloseDianTiAni()
    {
        DianTiAni.enabled = false;
        SceneManager.LoadScene("1(废墟)");
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

    private bool playerInTrig;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            playerInTrig = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            playerInTrig = false;
        }
    }
}
