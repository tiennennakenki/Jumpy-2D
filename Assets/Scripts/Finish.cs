using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : SaiMonoBehaviour
{
    [SerializeField] protected AudioSource finishSoundEffect;
    protected bool levelCompleted = false;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadFinishSoundEffect();
    }

    protected virtual void LoadFinishSoundEffect()
    {
        // Lấy tất cả các AudioSource con của GameObject hiện tại
        AudioSource[] audioSources = GetComponentsInChildren<AudioSource>();

        // Duyệt qua mảng audioSources
        foreach (AudioSource audioSource in audioSources)
        {
            // Kiểm tra nếu có AudioClip
            if (audioSource.clip.name == "finish")
            {
                // Xử lý AudioSource có AudioClip
                this.finishSoundEffect = audioSource;
                Debug.LogWarning(transform.name + ": LoadFinishSoundEffect", gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player" && !levelCompleted)
        {
            this.finishSoundEffect.Play();
            levelCompleted = true;
            Invoke("CompleteLevel", 2f);
        }
    }

    protected virtual void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
