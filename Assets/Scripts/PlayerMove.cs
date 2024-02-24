using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    public float knockPower;
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
        // Speed Modifier - Shift 키를 누르면 속도 2.5배
        float speedModifier = Input.GetKey(KeyCode.LeftShift) ? 2.5f : 1.0f;

        rigid.AddForce(Vector2.right * h * speedModifier, ForceMode2D.Impulse);

        // 속도 조절을 위해 현재 속도가 최대 속도를 넘지 않도록 함
        // 속도가 maxSpeed * speedModifier(Shift 키가 눌린 경우 2배)를 초과하지 않도록 함
        float maxModifiedSpeed = maxSpeed * speedModifier;
        if (rigid.velocity.x > maxModifiedSpeed) // Right Max Speed
        {
            rigid.velocity = new Vector2(maxModifiedSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < -maxModifiedSpeed) // Left Max Speed
        {
            rigid.velocity = new Vector2(-maxModifiedSpeed, rigid.velocity.y);
        }


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Tilemap"))
        {
            isJump = false;
        }

        // "Trap" 오브젝트에 닿았을 때 넉백 실행
        else if (collision.gameObject.name.Equals("Trap"))
        {
            // 현재 이동 방향을 기반으로 넉백 힘과 방향을 설정
            Vector2 knockbackForce = Vector2.zero;
            float knockbackPower = 5f; // 넉백의 힘을 원하는 대로 설정하세요.

            // 캐릭터가 오른쪽으로 이동 중이었다면 왼쪽으로, 왼쪽으로 이동 중이었다면 오른쪽으로 넉백
            if (rigid.velocity.x > 0) // 오른쪽으로 이동 중
            {
                knockbackForce = new Vector2(-knockbackPower, knockPower);
            }
            else if (rigid.velocity.x < 0) // 왼쪽으로 이동 중
            {
                knockbackForce = new Vector2(knockbackPower, knockPower);
            }
            else // 이동하지 않는 경우, 기본적으로 오른쪽으로 넉백
            {
                knockbackForce = new Vector2(-knockbackPower, knockPower);
            }

            // 넉백 힘 적용
            rigid.velocity = Vector2.zero; // 현재 속도 초기화
            rigid.AddForce(knockbackForce, ForceMode2D.Impulse);
        }
    }
}


