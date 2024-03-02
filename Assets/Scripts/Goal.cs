using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().isInputEnabled = false;
            StartCoroutine(TransitionToNextStage());
        }
    }

    IEnumerator TransitionToNextStage()
    {
        // 3초 기다립니다.
        yield return new WaitForSeconds(3);
        // "Stage2" 씬으로 전환합니다.
        SceneManager.LoadScene("Ending");
    }
}
