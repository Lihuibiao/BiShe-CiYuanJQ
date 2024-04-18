using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Scene2 : MonoBehaviour
{
    public Vector3 PlayerEnterPos;
    void Start()
    {
        PlayerController.Inst.transform.position = PlayerEnterPos;
    }

    public Image ImgBg;
    public Text MsgTxt;
    private int CurDispIndex = 0;

    public Image ShopBg;
    public Text ShopMsg;
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

        if (AlreadyTalk && CanShowShop)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                ShopBg.gameObject.SetActive(true);
                ShopMsg.text = "我这的芯片都是最顶级的";
                ShowAllShopBtn();
            }
        }
    }
    
    
    private List<TalkItem> FirstTalkList = new List<TalkItem>()
    {
        new TalkItem(){Name = "黑市商人" , Msg = "瞧一瞧看一看，上等的芯片。" , TalkNPC = TalkNPC.NPC} ,
        new TalkItem(){Name = "奥利" , Msg = "我现在似乎到达了一个黑市" , TalkNPC = TalkNPC.Player1} , 
        new TalkItem(){Name = "主角2" , Msg = "我位于一条狭长的通道，是前往机械先驱所在的机械宫殿的必经之路。" , TalkNPC = TalkNPC.Player2} , 
        new TalkItem(){Name = "奥利" , Msg = "听你的语气似乎你与机械先驱是敌对关系？" , TalkNPC = TalkNPC.Player1} , 
        new TalkItem(){Name = "主角2" , Msg = "机械先驱将我的家园毁灭，在家园的废墟上建造了他的乐园，我要击败他为我的族人们报仇雪恨。" , TalkNPC = TalkNPC.Player2} ,
        new TalkItem(){Name = "奥利:" , Msg = "这么说来我们的目的是相同的？" , TalkNPC = TalkNPC.Player1} ,
        new TalkItem(){Name = "主角2" , Msg = "我可以这么理解。" , TalkNPC = TalkNPC.Player2}
    };
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (AlreadyTalk)
        {
            return;
        }
        var player = other.transform.GetComponent<PlayerController>();
        if (player != null)
        {
            DispMsg();
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

    private bool AlreadyTalk;
    private void HideMsg()
    {
        AlreadyTalk = true;
        if (FirstTalkList.Count > 0)
        {
            FirstTalkList.Clear();
        }
        CurDispIndex = 0;
        ImgBg.gameObject.SetActive(false);
        PlayerController.Inst.enabled = true;
    }

    public List<GameObject> AllShopBtnList;
    
    public void OnClickA()
    {
        CloseAllShopBtn();
        ShopMsg.text = "想进入机械宫殿必须有开启大门的钥匙，曾经有一个人寻找钥匙，向着地下室的方向去了。";
        this.Invoke("delayCloseShop" , 1);
    }

    public void OnClickB1()
    {
        CloseAllShopBtn();
        ShopMsg.gameObject.SetActive(false);
        PlayerController.Inst.Score += 10;
        PlayerController.Inst.enabled = true;
    }

    public void OnClickB2()
    {
        CloseAllShopBtn();
        ShopMsg.gameObject.SetActive(false);
        PlayerController.Inst.Score += 10;
        PlayerController.Inst.enabled = true;
    }

    private bool CanShowShop = true;
    public void OnClickC()
    {
        CloseAllShopBtn();
        CanShowShop = false;
        PlayerController.Inst.Score -= 10;
        ShopMsg.text = "你永远别想在黑市买到任何东西！！！";
        this.Invoke("delayCloseShop" , 1);
    }

    private void delayCloseShop()
    {
        ShopBg.gameObject.SetActive(false);
        PlayerController.Inst.enabled = true;
    }

    private void CloseAllShopBtn()
    {
        foreach (var item in AllShopBtnList)
        {
            item.gameObject.SetActive(false);
        }
    }
    
    private void ShowAllShopBtn()
    {
        foreach (var item in AllShopBtnList)
        {
            item.gameObject.SetActive(true);
        }
    }
}
