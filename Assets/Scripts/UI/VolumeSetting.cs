using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : SaiMonoBehaviour
{
    //[SerializeField] protected AudioSource bgMusic;
    [SerializeField] protected Slider bgMusicSlider;

    protected override void Start()
    {
        base.Start();
        if (!PlayerPrefs.HasKey("bgMusic"))
        {
            PlayerPrefs.SetFloat("bgMusic", 1);
        }

        else
        {
            this.Load();
        }
    }

    public virtual void ChangeVolume()
    {
        AudioListener.volume = bgMusicSlider.value;
        this.Save();
    }

    public virtual void Load()
    {
        bgMusicSlider.value = PlayerPrefs.GetFloat("bgMusic");
    }

    public virtual void Save()
    {
        PlayerPrefs.SetFloat("bgMusic", bgMusicSlider.value);
    }
}
