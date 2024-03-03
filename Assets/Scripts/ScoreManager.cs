using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private AudioSource audioSource;
    public int score = 0; // 현재 점수
    public Text scoreText; // 점수를 표시할 UI Text

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            // 과일을 먹었을 때 효과음 재생
            audioSource.PlayOneShot(collision.gameObject.GetComponent<Fruit>().eatSound);
            score += 10; // 점수 증가
            scoreText.text = "Score: " + score.ToString(); // 점수 표시 업데이트
            Destroy(collision.gameObject); // 과일 오브젝트 제거
        }
        else if (collision.gameObject.CompareTag("Banana"))
        {
            audioSource.PlayOneShot(collision.gameObject.GetComponent<Fruit>().eatSound);
            score += 20; // 점수 증가
            scoreText.text = "Score: " + score.ToString(); // 점수 표시 업데이트
            Destroy(collision.gameObject); // 과일 오브젝트 제거
        }
        else if (collision.gameObject.CompareTag("Melon"))
        {
            audioSource.PlayOneShot(collision.gameObject.GetComponent<Fruit>().eatSound);
            score += 500; // 점수 증가
            scoreText.text = "Score: " + score.ToString(); // 점수 표시 업데이트
            Destroy(collision.gameObject); // 과일 오브젝트 제거
        }

    }
}
