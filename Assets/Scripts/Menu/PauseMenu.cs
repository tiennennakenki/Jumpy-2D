using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : SaiMonoBehaviour
{
    [SerializeField] protected GameObject pauseMenu;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPauseMenu();
    }

    protected virtual void LoadPauseMenu()
    {
        if (this.pauseMenu != null) return;
        this.pauseMenu = GameObject.Find("PauseMenu");
        Debug.LogWarning(transform.name + ": LoadPauseMenu", gameObject);
    }

    public void Home()
    {
        SceneManager.LoadScene("Start Screen");
        Time.timeScale = 1;
    }

    public void Pause()
    {
        this.pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        this.pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
