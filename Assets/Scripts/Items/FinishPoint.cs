using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : SaiMonoBehaviour
{
    [SerializeField] protected Animator animator;
    public Animator Animator => animator;
    [SerializeField] protected AudioSource finishSoundEffect;
    protected bool levelCompleted = false;

    [SerializeField] protected ItemsCollector itemsCollector;

    protected override void Start()
    {
        base.Start();
        this.LoadCherryCollector();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAnimator();
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

    protected virtual void LoadAnimator()
    {
        if (this.animator != null) return;
        this.animator = GetComponent<Animator>();
        Debug.LogWarning(transform.name + " :LoadAnimator", gameObject);
    }

    protected virtual void LoadCherryCollector()
    {
        if (this.itemsCollector != null) return;
        this.itemsCollector = FindObjectOfType<ItemsCollector>();
        Debug.LogWarning(transform.name + " :LoadCherryCollector", gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !levelCompleted)
        {
            if (!this.CheckCountCherry()) {
                this.itemsCollector.CheckCountCherriesText.gameObject.SetActive(true);
                Invoke("HiddenText", 3f);
                return;
            };
            if (!this.CheckCountStar())
            {
                this.itemsCollector.CheckCountStarsText.gameObject.SetActive(true);
                Invoke("HiddenText", 3f);
                return;
            };
            this.animator.SetTrigger("havePlayer");
            this.finishSoundEffect.Play();
            levelCompleted = true;
            this.UnlockNewLevel();
            Invoke("CompleteLevel", 2f);
        }
    }

    protected virtual void HiddenText()
    {
        this.itemsCollector.CheckCountCherriesText.gameObject.SetActive(false);
        this.itemsCollector.CheckCountStarsText.gameObject.SetActive(false);
    }

    protected virtual bool CheckCountCherry()
    {
        if (this.itemsCollector.Cherries >= 15) return true;
        else return false;
    }

    protected virtual bool CheckCountStar()
    {
        if (this.itemsCollector.Stars >= 3) return true;
        else return false;
    }

    protected virtual void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    protected virtual void UnlockNewLevel()
    {
        int currentUnlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        // Kiểm tra nếu UnlockedLevel chưa đạt tới giới hạn là 3
        if (currentUnlockedLevel < 3)
        {
            if (SceneManager.GetActiveScene().buildIndex + 1 >= PlayerPrefs.GetInt("ReachedIndex"))
            {
                PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);

                // Tăng giá trị UnlockedLevel chỉ khi nó chưa đạt tới giới hạn là 3
                PlayerPrefs.SetInt("UnlockedLevel", currentUnlockedLevel + 1);

                PlayerPrefs.Save();
            }
        }
        else
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);

            // Tăng giá trị UnlockedLevel chỉ khi nó chưa đạt tới giới hạn là 3
            PlayerPrefs.SetInt("UnlockedLevel", 3);

            PlayerPrefs.Save();
        }


    }


}
