using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Scene3 : MonoBehaviour
{
    public Vector3 PlayerEnterPos;
    void Start()
    {
        PlayerController.Inst.transform.position = PlayerEnterPos;
        DispMsg();
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
        new TalkItem(){Name = "奥利" , Msg = "这里满目疮痍，似乎遭到了很严重的破坏" , TalkNPC = TalkNPC.Player1} ,
        new TalkItem(){Name = "托克" , Msg = "估计是机械先驱的手下机械蝇的杰作，他盘踞于地下室，喜欢破坏在让奴役的人来修复，周而复始。" , TalkNPC = TalkNPC.Player2} , 
        new TalkItem(){Name = "系统提示" , Msg = "玩家可选择多条路线，通过广场中间的传送仓可到达天桥，左连接灯塔顶部，右连接机械宫殿二层，下连接地下一层" , TalkNPC = TalkNPC.Player1} ,
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
