using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리를 위해 추가

public class Player2Move : MonoBehaviour
{
    public float moveSpeed = 5.0f; // 캐릭터의 이동 속도
    public float jumpForce = 700f; // 점프 힘
    private Rigidbody2D rb; // 캐릭터의 Rigidbody2D 컴포넌트
    private Animator animator; // 캐릭터의 Animator 컴포넌트
    private bool isGrounded; // 바닥에 있는지 여부
    public Transform groundCheck; // 바닥 체크를 위한 Transform
    public float checkDistance = 0.2f; // 바닥 체크 거리
    public LayerMask whatIsGround; // 바닥으로 간주될 레이어
    private float moveX; // 수평 이동 입력 값
    private bool isMoving; // 캐릭터가 이동 중인지 여부를 나타내는 플래그

    private bool isDead = false; // 캐릭터의 사망 상태


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D 컴포넌트를 가져옴
        animator = GetComponent<Animator>(); // Animator 컴포넌트를 가져옴
    }

    private void Update()
    {
        if (isDead) return; // 사망 상태일 때는 입력을 받지 않음

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
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, checkDistance, whatIsGround);
        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        animator.SetBool("isGrounded", isGrounded);


        // 점프 입력 처리
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            // 사망 처리 로직
            Die();
        }
    }
    void Die()
    {
        isDead = true;
        animator.SetTrigger("Die"); // 사망 애니메이션 실행
        rb.velocity = Vector2.zero; // 캐릭터의 이동 속도를 0으로 설정
        rb.gravityScale = 0; // 사망 시 중력 적용 (필요한 경우 중력 스케일 조정)
        
        // 사망 처리 변경...
        StartCoroutine(ShowCurrentLivesScene()); // "현재 목숨" 씬 표시 코루틴 실행
    }
    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(3); // 3초 대기
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // 현재 씬 재로딩
    }
    IEnumerator ShowCurrentLivesScene()
    {
        yield return new WaitForSeconds(3); // 사망 애니메이션을 위한 대기 시간
        SceneManager.LoadScene("CurrentLives"); // "현재 목숨" 씬으로 전환
        yield return new WaitForSeconds(3); // "현재 목숨" 씬 표시 시간
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // 원래 씬으로 돌아가기
    }



    private void FixedUpdate()
    {
        if (isDead) return; // 사망 상태일 때는 이동 처리를 하지 않음
        // Rigidbody2D를 사용해 캐릭터를 이동시킴
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        // 추가적으로, Shift 키를 눌러 이동 속도를 조절할 수 있다.
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.velocity = new Vector2(moveX * moveSpeed * 1.5f, rb.velocity.y);
        }
    }
    void OnDrawGizmos()
    {
        if (groundCheck == null) return;

        // 바닥 체크 광선 시각화
        Gizmos.color = Color.red;
        Vector2 start = groundCheck.position; // 광선의 시작점
        Vector2 end = start + Vector2.down * checkDistance; // 광선의 끝점 (아래 방향으로 checkDistance만큼)

        Gizmos.DrawLine(start, end);

    }
}