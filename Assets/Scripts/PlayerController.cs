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
    void Awake()
    {
        myAnim = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        PolygonCollider2D = GetComponent<PolygonCollider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
        Inst = this;
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
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        transform.Translate(Mathf.Abs(Input.GetAxis("Horizontal")) * Time.deltaTime * runSpeed , 0f , 0f);
        AniCtrl(move);
    }

    void fixedCollider()
    {
        SpriteRenderer.sprite.GetPhysicsShape(0, points);
        PolygonCollider2D.SetPath(0 , points);
    }

    private void AniCtrl(Vector2 move)
    {
        if (myAnim.GetBool("Run"))
        {
            myAnim.SetBool("Run" , false);
        }
        
        if (myAnim.GetBool("Jump"))
        {
            myAnim.SetBool("Jump" , false);
        }
        
        
        if (move.x != 0 && !IsRunAni())
        {
            myAnim.SetBool("Run" , true);
        }
        else
        {
            myAnim.SetBool("Run", false);
        }
        myAnim.SetFloat("MoveX" , Mathf.Abs(move.x));
    }

    private bool IsRunAni()
    {
        return myAnim.GetCurrentAnimatorStateInfo(0).IsName("Run");
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        // Debug.LogError(other.gameObject.name);
    }
}
