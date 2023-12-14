using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemsCollector : SaiMonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI cherriesText;
    [SerializeField] protected TextMeshProUGUI starsText;

    [SerializeField] protected TextMeshProUGUI checkCountCherriesText;
    public TextMeshProUGUI CheckCountCherriesText => checkCountCherriesText;

    [SerializeField] protected TextMeshProUGUI checkCountStarsText;
    public TextMeshProUGUI CheckCountStarsText => checkCountStarsText;

    [SerializeField] protected AudioSource collectSoundEffect;
    [SerializeField] protected int cherries = 0;
    [SerializeField] protected int stars = 0;
    public int Cherries => cherries;
    public int Stars => stars;

    protected override void Awake()
    {
        base.Awake();
        this.checkCountStarsText.gameObject.SetActive(false);
        this.checkCountCherriesText.gameObject.SetActive(false);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCherriesText();
        this.LoadStarsText();
        this.LoadCollectSoundEffect();
        this.LoadCheckCountCherriesText();
        this.LoadCheckCountStarsText();
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

    protected virtual void LoadCheckCountCherriesText()
    {
        if (this.checkCountCherriesText != null) return;

        GameObject checkCountCherriesTextObject = GameObject.Find("CheckCountCherries");
        if (checkCountCherriesTextObject == null) return;

        this.checkCountCherriesText = checkCountCherriesTextObject.GetComponent<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + " : LoadCheckCountCherriesText", gameObject);
    }

    protected virtual void LoadCheckCountStarsText()
    {
        if (this.checkCountStarsText != null) return;

        GameObject checkCountStarsTextObject = GameObject.Find("CheckCountStars");
        if (checkCountStarsTextObject == null) return;

        this.checkCountStarsText = checkCountStarsTextObject.GetComponent<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + " : LoadCheckCountStarsText", gameObject);
    }

    protected virtual void LoadStarsText()
    {
        if (this.starsText != null) return;

        GameObject starsTextObject = GameObject.Find("Stars Text");
        if (starsTextObject == null) return;

        this.starsText = starsTextObject.GetComponent<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + " : LoadStarsText", gameObject);
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

        if (collision.gameObject.CompareTag("Star"))
        {
            this.collectSoundEffect.Play();
            Destroy(collision.gameObject);
            stars++;
            this.starsText.text = "x" + stars;
        }
    }
}
