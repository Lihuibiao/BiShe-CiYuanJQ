using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Scene4Talk : MonoBehaviour
{
    public Image ShopBg;
    public Text ShopMsg;
    void Update()
    {
    }
    
    
    private List<TalkItem> FirstTalkList = new List<TalkItem>()
    {
        new TalkItem(){Name = "黑市商人" , Msg = "瞧一瞧看一看，上等的芯片。" , TalkNPC = TalkNPC.NPC} ,
        new TalkItem(){Name = "奥利" , Msg = "我现在似乎到达了一个黑市" , TalkNPC = TalkNPC.Player1} , 
        new TalkItem(){Name = "托克" , Msg = "我位于一条狭长的通道，是前往机械先驱所在的机械宫殿的必经之路。" , TalkNPC = TalkNPC.Player2} , 
        new TalkItem(){Name = "奥利" , Msg = "听你的语气似乎你与机械先驱是敌对关系？" , TalkNPC = TalkNPC.Player1} , 
        new TalkItem(){Name = "托克" , Msg = "机械先驱将我的家园毁灭，在家园的废墟上建造了他的乐园，我要击败他为我的族人们报仇雪恨。" , TalkNPC = TalkNPC.Player2} ,
        new TalkItem(){Name = "奥利:" , Msg = "这么说来我们的目的是相同的？" , TalkNPC = TalkNPC.Player1} ,
        new TalkItem(){Name = "托克" , Msg = "我可以这么理解。" , TalkNPC = TalkNPC.Player2}
    };
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.transform.GetComponent<PlayerController>();
        if (player != null)
        {
            PlayerController.Inst.enabled = false;
            ShopBg.gameObject.SetActive(true);
            ShowAllShopBtn();
            ShopMsg.text = "我在寻找一种机械零件，不小心迷路了，如果你能能帮我找到，我会给你丰厚的奖励的";
        }
    }
    

    private static bool AlreadyTalk;
    private void HideMsg()
    {
        AlreadyTalk = true;
        if (FirstTalkList.Count > 0)
        {
            FirstTalkList.Clear();
        }
        ShopBg.gameObject.SetActive(false);
        PlayerController.Inst.enabled = true;
    }

    public List<GameObject> AllShopBtnList;
    
    public void OnClickA()
    {
        CloseAllShopBtn();
        ShopMsg.text = "我需要一个精致机械爪，似乎机械蝇身上有";
        this.Invoke("delayCloseShop" , 1);
    }

    public void OnClickB()
    {
        CloseAllShopBtn();
        ShopMsg.text = "你似乎还没找到零件";
        this.Invoke("delayCloseShop" , 1);
    }
    
    public void OnClickC()
    {
        CloseAllShopBtn();
        ShopMsg.text = "触发对话(你终将受到报应！)";
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
