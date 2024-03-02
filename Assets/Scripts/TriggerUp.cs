using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerUp : MonoBehaviour
{
    public Rigidbody2D objectToRise; // Inspector에서 할당할 Rigidbody2D
    public float moveDistance = 5f; // 오브젝트가 올라갈 거리
    public float moveSpeed = 3f; // 오브젝트가 올라가는 속도
    private Vector2 originalPosition; // 오브젝트의 원래 위치
    private Vector2 targetPosition; // 오브젝트가 이동할 목표 위치

    void Start()
    {
        originalPosition = objectToRise.transform.position; // 게임 시작 시 오브젝트의 원래 위치 저장
        targetPosition = originalPosition + Vector2.up * moveDistance; // 목표 위치 계산
        objectToRise.isKinematic = true; // 오브젝트가 떨어지지 않도록 isKinematic을 true로 설정
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // "Player" 태그를 가진 오브젝트가 트리거에 진입했는지 확인
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(MoveObjectToRise(objectToRise, targetPosition, moveSpeed));
        }
    }

    IEnumerator MoveObjectToRise(Rigidbody2D objectToMove, Vector2 target, float speed)
    {
        // 오브젝트를 목표 위치까지 이동시키는 코루틴
        while ((Vector2)objectToMove.transform.position != target)
        {
            objectToMove.transform.position = Vector2.MoveTowards(objectToMove.transform.position, target, speed * Time.deltaTime);
            yield return null;
        }
    }
}
