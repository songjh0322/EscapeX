using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFall : MonoBehaviour
{
    public Rigidbody2D objectToFall; // Inspector에서 할당

    void Start()
    {
        // 게임 시작 시 오브젝트가 떨어지지 않도록 isKinematic을 true로 설정
        objectToFall.isKinematic = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // "Player" 태그를 가진 오브젝트가 트리거에 진입했는지 확인
        if (collision.gameObject.CompareTag("Player"))
        {
            // isKinematic을 false로 설정하여 오브젝트가 중력의 영향을 받도록 함
            objectToFall.isKinematic = false;
        }
    }
}