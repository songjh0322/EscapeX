using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadGameSceneAfterDelay : MonoBehaviour
{
    public string gameSceneName = "Stage1"; // 원래 게임 씬의 이름

    void Start()
    {
        StartCoroutine(LoadGameSceneAfterDelayCoroutine());
    }

    IEnumerator LoadGameSceneAfterDelayCoroutine()
    {
        yield return new WaitForSeconds(3); // "CurrentLives" 씬에서 3초 대기
        SceneManager.LoadScene(gameSceneName); // 게임 씬 로드
    }
}