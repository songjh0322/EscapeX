using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    public bool isJump = false;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;

    private void Awake()  //초기화는 Awake에서 한다. (이유는 하다보면 알게되겠지..)
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    private void Update() //Update : 단발적인 키 입력
    {

        //Jump
        if (Input.GetButtonDown("Jump"))
        {
            if (!isJump)
            {
                isJump = true;
                rigid.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);
                
            }
        }
        //Stop Speed
        if (Input.GetButtonUp("Horizontal")) //뗏을때
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        //Direction Sprite
        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

        }
        //Animation
        if (Mathf.Abs(rigid.velocity.x) < 0.5)
        {
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isWalking", true);


        }
    }

    private void FixedUpdate() //1초에 걍 50번 준다. 즉 누르면 누를수록 엄청 빨라짐(Addforce)->가속을 무한으로 받는다.
    //FixedUpdate : 지속적인 키 입력
    {
        //Move Speed
        float h = Input.GetAxisRaw("Horizontal");

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed) //Right Max Speed
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < maxSpeed * (-1)) //Left Max Speed
        {
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }


    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name.Equals("Tilemap"))
        {
            isJump = false;
        }
    }
}

