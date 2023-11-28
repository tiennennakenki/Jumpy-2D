using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuLevel : SaiMonoBehaviour
{
    [SerializeField] protected List<Button> menuLevels = new List<Button>();
    public List<Button> MenuLevels => menuLevels;

    protected override void Awake()

    {
        base.Awake();
        this.LoadComponents();
        this.UnlockedLevel();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadLevel();
    }

    protected virtual void LoadLevel()
    {
        if (this.menuLevels.Count > 0) return;
        Transform levels = transform.Find("Levels");

        Button btnLevel;
        foreach(Transform level in levels)
        {
            btnLevel = level.GetComponentInChildren<Button>();
            this.menuLevels.Add(btnLevel);
        }
        Debug.LogWarning(transform.name + ": LoadLevel", gameObject);
    }

    protected virtual void UnlockedLevel()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        for (int i = 0; i < menuLevels.Count; i++)
        {
            menuLevels[i].interactable = false;
        }

        for (int i = 0; i < unlockedLevel; i++)
        {
            menuLevels[i].interactable = true;
        }
    }

    public void OpenLevel(int levelId)
    {
        string levelName = "Level " + levelId;
        SceneManager.LoadScene(levelName);
    }
}
