using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foothold : MonoBehaviour
{
    public float moveUpDistance = 1.5f; // 올라가는 높이
    public float moveSpeed = 3f; // 이동 속도
    public float waitTime = 1f; // 목표 위치에 도달한 후 대기 시간

    private Vector3 originalPosition; // 초기 위치

    void Start()
    {
        originalPosition = transform.position; // 스크립트가 시작될 때 초기 위치 저장
        StartCoroutine(MovePlatform());
    }

    IEnumerator MovePlatform()
    {
        while (true) // 무한 반복
        {
            // 위로 올라가기
            Vector3 upPosition = originalPosition + new Vector3(0, moveUpDistance, 0); // 목표 위치 계산
            yield return StartCoroutine(MoveToPosition(upPosition)); // 목표 위치로 이동

            // 원래 위치로 즉시 돌아가기 (순간이동 효과)
            transform.position = originalPosition;

            // 대기
            yield return new WaitForSeconds(waitTime);
        }
    }

    IEnumerator MoveToPosition(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.01f) // 목표 위치에 충분히 가까워질 때까지
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}