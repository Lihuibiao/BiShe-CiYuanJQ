using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class JiXieXianQu : MonoBehaviour
{
    public Animator myAnim;
    private SpriteRenderer SpriteRenderer;
    private PolygonCollider2D PolygonCollider2D;
    private List<Vector2> points = new List<Vector2>();
    public JiXie1 Scene;
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
            if (PlayerDis < 20)
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
            else if (PlayerDis < 30)
            {
                PlayAttack3();
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
        IsAttack3 = false;
        myAnim.SetBool("Attack1" , true);
        lastAttackTime = Time.timeSinceLevelLoad;
    }
    
    
    [InspectorButton]
    public void PlayAttack2()
    {
        IsAttack3 = false;
        myAnim.SetBool("Attack2" , true);
        lastAttackTime = Time.timeSinceLevelLoad;
    }

    // public GameObject Attack3Ani;
    private bool IsAttack3;
    [InspectorButton]
    public void PlayAttack3()
    {
        IsAttack3 = true;
        myAnim.SetBool("Attack3" , true);
        this.Invoke("delayCloseAttack" , 0.2f);
        // this.Invoke("closeAttack3Ani" , 1.2f);
        // Attack3Ani.gameObject.SetActive(true);
    }

    private void closeAttack3Ani()
    {
        // Attack3Ani.gameObject.SetActive(false);
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
        if (PlayerDis < 20)
        {
            PlayerController.Inst.Hp--;
            return;
        }else if (IsAttack3 && PlayerDis < 20)
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
