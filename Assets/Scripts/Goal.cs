using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public AudioClip victoryMusic; // 승리 배경음악 오디오 클립
    private AudioSource victoryAudioSource; // 승리 배경음악을 재생할 오디오 소스
    private AudioSource backgroundMusicAudioSource; // 기존 배경음악 오디오 소스
    private void Start()
    {
        // 승리 배경음악을 재생할 새 오디오 소스를 추가합니다.
        victoryAudioSource = gameObject.AddComponent<AudioSource>();
        victoryAudioSource.clip = victoryMusic;
        victoryAudioSource.loop = false; // 승리 음악은 반복 재생하지 않습니다.
        victoryAudioSource.playOnAwake = false; // 자동 재생 비활성화
                                                // 기존 배경음악이 재생 중인 오디오 소스를 찾아 참조합니다.
                                                // 플레이어 오브젝트에서 배경음악이 재생 중인 오디오 소스를 찾아 참조합니다.
        backgroundMusicAudioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.gameObject.CompareTag("Player"))
    {
        // 플레이어의 입력을 비활성화합니다.
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.isInputEnabled = false;
            playerController.StopBackgroundMusic(); // 배경음악을 멈춥니다.
        }

        // 승리 배경음악을 재생합니다.
        victoryAudioSource.Play();

        // 다음 스테이지로의 전환을 시작합니다.
        StartCoroutine(TransitionToNextStage());
    }
    }

    IEnumerator TransitionToNextStage()
    {
        // 3초 기다립니다.
        yield return new WaitForSeconds(3.5f);
        // "Stage2" 씬으로 전환합니다.
        SceneManager.LoadScene("Ending");
    }
}
