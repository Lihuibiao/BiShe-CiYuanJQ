using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Inst;
    private Animator myAnim;
    private SpriteRenderer SpriteRenderer;
    private PolygonCollider2D PolygonCollider2D;
    private Rigidbody2D myRigidbody;
    private List<Vector2> points = new List<Vector2>();

    public float runSpeed = 1f;
    public int Attack = 1;
    public int Hp = 20;
    public int Score;
    void Awake()
    {
        myAnim = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        PolygonCollider2D = GetComponent<PolygonCollider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
        Inst = this;
        
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        fixedCollider();
    }

    private void FixedUpdate()
    {
        var move = new Vector2(Input.GetAxis("Horizontal"), 0);
        // Vector2 playerVelocity = new Vector2(move.x * runSpeed, myRigidbody.velocity.y);
        // myRigidbody.velocity = playerVelocity;
        if (move.x < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if(move.x > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        if (IsRunAni() || IsJumpAni())
        {
            transform.Translate(Mathf.Abs(Input.GetAxis("Horizontal")) * Time.deltaTime * runSpeed , 0f , 0f);   
        }
        AniCtrl(move);
    }

    void fixedCollider()
    {
        SpriteRenderer.sprite.GetPhysicsShape(0, points);
        PolygonCollider2D.SetPath(0 , points);
    }

    private void AniCtrl(Vector2 move)
    {
        if (move.x != 0 && !IsRunAni())
        {
            myAnim.SetBool("Run" , true);
        }
        else
        {
            myAnim.SetBool("Run", false);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            myAnim.SetBool("Jump" , true);
            // 获取当前动画状态
            AnimatorStateInfo stateInfo = myAnim.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("Jump") && stateInfo.normalizedTime >= 0.5f)
            {
                myAnim.SetBool("Jump2" , true);
            }
        }
        else
        {
            myAnim.SetBool("Jump", false);
            myAnim.SetBool("Jump2", false);
        }
        
        
        if (Input.GetKeyDown(KeyCode.J) && !myAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack_01"))
        {
            myAnim.SetBool("Attack_01" , true);
            this.Invoke("delayCloseAttack" , 0.2f);
        }
        myAnim.SetFloat("MoveX" , Mathf.Abs(move.x));
    }

    private void delayCloseAttack()
    {
        if (myAnim.GetBool("Attack_01"))
        {
            myAnim.SetBool("Attack_01" , false);
            AttackOnce();
        }
    }

    public void SetIdle()
    {
        myAnim.SetBool("Attack_01" , false);
        myAnim.SetBool("Jump" , false);
        myAnim.SetBool("Run" , false);
    }

    public XieZi AttackXieZi;
    private void AttackOnce()
    {
        if (AttackXieZi != null)
        {
            AttackXieZi.OnGetHurt();
        }
    }
    
    private bool IsRunAni()
    {
        return myAnim.GetCurrentAnimatorStateInfo(0).IsName("Run");
    }
    
    private bool IsJumpAni()
    {
        var info = myAnim.GetCurrentAnimatorStateInfo(0);
        return info.IsName("Jump") || info.IsName("Jump2");
    }
    
    public bool IsAttackAni()
    {
        return myAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack_01");
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.LogError(other.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.LogError("Trigger " + other.gameObject);
    }
    
    
}
