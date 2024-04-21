using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class JiXieShouWei : MonoBehaviour
{
    public Animator myAnim;
    private SpriteRenderer SpriteRenderer;
    private PolygonCollider2D PolygonCollider2D;
    private List<Vector2> points = new List<Vector2>();
    public JiXieGongChang Scene;
    public int Hp = 5;
    private void Awake()
    {
        myAnim = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        PolygonCollider2D = GetComponent<PolygonCollider2D>();
    }
    void Start()
    {
        
    }

    private float lastAttackTime = 0;
    public float PlayerDis;
    // Update is called once per frame
    void Update()
    {
        PlayerDis = Vector3.Distance(PlayerController.Inst.transform.position, this.transform.position);
        if (Move2PlayerIng)
        {
            transform.Translate(-1 * Time.deltaTime * 20 , 0f , 0f);
            if (PlayerDis < 30)
            {
                Move2PlayerIng = false;
                PlayerController.Inst.enabled = true;
                myAnim.SetBool("Move" , false);
                return;
            }
            return;
        }

        if (Time.timeSinceLevelLoad - lastAttackTime > 3)
        {
            if (PlayerDis < 10)
            {
                // 攻击
                if (Random.Range(0, 3) == 0)
                {
                    PlayAttack1();
                }
                else
                {
                    PlayAttack2();
                }
            }
        }
        
    }

    private void LateUpdate()
    {
        if (IsDeath)
        {
            return;
        }
        try
        {
            fixedCollider();
        }
        catch (Exception e)
        {
        }
    }
    
    void fixedCollider()
    {
        SpriteRenderer.sprite.GetPhysicsShape(0, points);
        PolygonCollider2D.SetPath(0 , points);
    }
    
    public bool Move2PlayerIng = false;
    public void Move2Player()
    {
        // 机械先驱移动
        this.Move2PlayerIng = true;
        myAnim.SetBool("Move" , true);
        lastAttackTime = Time.timeSinceLevelLoad;
    }

    [InspectorButton]
    public void PlayAttack1()
    {
        myAnim.SetBool("Attack1" , true);
        this.Invoke("delayCloseAttack" , 0.2f);
        lastAttackTime = Time.timeSinceLevelLoad;
    }
    
    
    [InspectorButton]
    public void PlayAttack2()
    {
        myAnim.SetBool("Attack2" , true);
        this.Invoke("delayCloseAttack" , 0.2f);
        lastAttackTime = Time.timeSinceLevelLoad;
    }
    
    void HurtOnce()
    {
        this.myAnim.SetBool("Hurt" , true);
        this.Invoke("delayCloseHurt" , 0.2f);
    }
    
    private void delayCloseHurt()
    {
        this.myAnim.SetBool("Hurt", false);
    }
    
    private bool IsDeath;
    [InspectorButton]
    private void PlayDeath()
    {
        IsDeath = true;
        this.myAnim.SetBool("Death", true);
        this.myAnim.SetBool("Attack1", false);
        this.myAnim.SetBool("Attack2", false);
        this.myAnim.SetBool("Attack3", false);
        this.Invoke("delayDestroySlef" , 0.5f);
        Scene.ShowBtnBg();
    }

    private void delayDestroySlef()
    {
        Destroy(this.gameObject);
    }
    
    private void delayCloseAttack()
    {
        this.myAnim.SetBool("Attack1", false);
        this.myAnim.SetBool("Attack2", false);
        this.myAnim.SetBool("Attack3", false);
        if (PlayerDis < 10)
        {
            PlayerController.Inst.Hp--;
            return;
        }
    }
    
    [InspectorButton]
    public void OnGetHurt()
    {
        Debug.LogError(" 机械先驱受伤一次");
        this.Hp--;
        if (this.Hp <= 0)
        {
            this.PolygonCollider2D.enabled = false;
            this.PlayDeath();
        }
        else
        {
            HurtOnce();
        }
    }
}
