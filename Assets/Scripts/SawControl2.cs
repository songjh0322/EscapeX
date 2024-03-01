using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawControl2 : MonoBehaviour
{
    public float moveDownDistance = 1.5f; // 올라가는 거리
    public float moveSpeed = 3f; // 올라가는 속도
    public float waitTime = 3.5f; // 올라간 후 대기 시간

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position; // 초기 위치 저장
        StartCoroutine(MoveSaw());
    }

    IEnumerator MoveSaw()
    {
        while (true)
        {
            // 위로 올라가기
            yield return MoveToPosition(transform.position - new Vector3(0, moveDownDistance, 0), moveSpeed);
            // 대기
            yield return new WaitForSeconds(waitTime);
            // 원래 위치로 돌아가기
            yield return MoveToPosition(originalPosition, moveSpeed);
            // 대기
            yield return new WaitForSeconds(waitTime);
        }
    }

    IEnumerator MoveToPosition(Vector3 target, float speed)
    {
        while (transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return null;
        }
    }
}