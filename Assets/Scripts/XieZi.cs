using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XieZi : MonoBehaviour
{
    public Animator myAnim;
    private SpriteRenderer SpriteRenderer;
    private PolygonCollider2D PolygonCollider2D;
    private List<Vector2> points = new List<Vector2>();

    public int Hp = 3;
    private void Awake()
    {
        myAnim = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        PolygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    private float lastAttackTime = 0;

    public float AttackDis = 15;
    public float PlayerDis;
    // Update is called once per frame
    void Update()
    {
        PlayerDis = Vector3.Distance(PlayerController.Inst.transform.position, this.transform.position);
        if (PlayerDis < AttackDis)
        {
            if (Time.timeSinceLevelLoad - lastAttackTime > 3)
            {
                lastAttackTime = Time.timeSinceLevelLoad;
                AttackOnce();   
            }
        }
        
        AnimatorStateInfo stateInfo = myAnim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("蝎子死亡") && stateInfo.normalizedTime >= 0.9f)
        {
            Destroy(this.gameObject);
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

    void AttackOnce()
    {
        this.myAnim.SetBool("Attack" , true);
        this.Invoke("delayCloseAttack" , 0.2f);
    }

    private void delayCloseAttack()
    {
        this.myAnim.SetBool("Attack", false);
        if (!PlayerInTrigger)
        {
            return;
        }
        PlayerController.Inst.Hp--;
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
    private void PlayDeath()
    {
        IsDeath = true;
        this.myAnim.SetBool("Death", true);
    }
    
    private bool IsAttackAni()
    {
        return myAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack_01");
    }

    private bool PlayerInTrigger;
    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.transform.GetComponent<PlayerController>();
        if (player != null)
        {
            PlayerInTrigger = true;
            player.AttackXieZi = this;
            // if (player.IsAttackAni())
            // {
            //     OnGetHurt();
            // }
            // else if (this.IsAttackAni())
            // {
            //     player.Hp--;
            // }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.transform.GetComponent<PlayerController>();
        if (player != null)
        {
            PlayerInTrigger = false;
            player.AttackXieZi = null;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        var player = other.transform.GetComponent<PlayerController>();
        if (player != null)
        {
            PlayerInTrigger = true;
        }
    }

    [InspectorButton]
    public void OnGetHurt()
    {
        Debug.LogError("蝎子受伤一次");
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
