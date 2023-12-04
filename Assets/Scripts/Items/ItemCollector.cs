using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemCollector : SaiMonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI cherriesText;
    [SerializeField] protected AudioSource collectSoundEffect;
    protected int cherries = 0;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCherriesText();
        this.LoadCollectSoundEffect();
    }

    protected virtual void LoadCollectSoundEffect()
    {
        // Lấy tất cả các AudioSource con của GameObject hiện tại
        AudioSource[] audioSources = GetComponentsInChildren<AudioSource>();

        // Duyệt qua mảng audioSources
        foreach (AudioSource audioSource in audioSources)
        {
            // Kiểm tra nếu có AudioClip
            if (audioSource.clip.name == "collect")
            {
                // Xử lý AudioSource có AudioClip
                this.collectSoundEffect = audioSource;
                Debug.LogWarning(transform.name + ": LoadCollectSoundEffect", gameObject);
            }
        }
    }

    protected virtual void LoadCherriesText()
    {
        if (this.cherriesText != null) return;

        GameObject cherriesTextObject = GameObject.Find("Cherries Text");
        if (cherriesTextObject == null) return;

        this.cherriesText = cherriesTextObject.GetComponent<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + " : LoadCherriesText", gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            this.collectSoundEffect.Play();
            Destroy(collision.gameObject);
            cherries++;
            this.cherriesText.text = "x" + cherries;
        }
    }
}
