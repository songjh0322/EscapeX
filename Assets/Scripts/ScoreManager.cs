using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int score = 0; // 현재 점수
    public Text scoreText; // 점수를 표시할 UI Text

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Cherry"))
        {
            score += 10; // 점수 증가
            scoreText.text = "Score: " + score.ToString(); // 점수 표시 업데이트
            Destroy(other.gameObject); // 과일 오브젝트 제거
        }
        else if (other.gameObject.CompareTag("Banana"))
        {
            score += 20; // 점수 증가
            scoreText.text = "Score: " + score.ToString(); // 점수 표시 업데이트
            Destroy(other.gameObject); // 과일 오브젝트 제거
        }
        else if (other.gameObject.CompareTag("Melon"))
        {
            score += 50; // 점수 증가
            scoreText.text = "Score: " + score.ToString(); // 점수 표시 업데이트
            Destroy(other.gameObject); // 과일 오브젝트 제거
        }

    }
}
