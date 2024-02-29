using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Move : MonoBehaviour
{
    public float moveSpeed = 5.0f; // 캐릭터의 이동 속도
    private Rigidbody2D rb; // 캐릭터의 Rigidbody2D 컴포넌트
    private Animator animator; // 캐릭터의 Animator 컴포넌트
    private float moveX; // 수평 이동 입력 값
    private bool isMoving; // 캐릭터가 이동 중인지 여부를 나타내는 플래그

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D 컴포넌트를 가져옴
        animator = GetComponent<Animator>(); // Animator 컴포넌트를 가져옴
    }

    private void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal"); // 수평 이동 입력을 받음
        isMoving = moveX != 0; // 캐릭터가 이동 중인지 판단

        // 애니메이션 상태를 업데이트
        animator.SetBool("isMoving", isMoving);

        // 캐릭터의 방향을 업데이트
        if (moveX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // 왼쪽을 바라보게 함
        }
        else if (moveX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // 오른쪽을 바라보게 함
        }
    }

    private void FixedUpdate()
    {
        // Rigidbody2D를 사용해 캐릭터를 이동시킴
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        // 추가적으로, Shift 키를 눌러 이동 속도를 조절할 수 있습니다.
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.velocity = new Vector2(moveX * moveSpeed * 1.5f, rb.velocity.y);
        }
    }
}