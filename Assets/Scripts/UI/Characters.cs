using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Characters : SaiMonoBehaviour
{
    [SerializeField] protected List<GameObject> skins = new List<GameObject>();
    public List<GameObject> Skins => skins;

    [SerializeField] protected int selectedCharacter;
    public int SelectedCharacter => selectedCharacter;

    protected override void Awake()
    {
        base.Awake();
        this.SetCharacter();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSkin();
    }

    protected virtual void LoadSkin()
    {
        if (this.skins.Count > 0) return;

        Transform characters = transform;

        foreach (Transform character in characters)
        {
            GameObject player = character.gameObject; // Get the GameObject from the Transform
            this.skins.Add(player);
        }

        Debug.LogWarning(transform.name + ": LoadPlayerPrefabs", gameObject);
    }

    public virtual void SetCharacter()
    {
        this.selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 0);

        foreach(GameObject player in this.skins)
        {
            player.SetActive(false);
        }

        this.skins[selectedCharacter].SetActive(true);
    }

    public virtual void ChangeNext()
    {
        this.skins[selectedCharacter].SetActive(false);
        selectedCharacter++;
        if(selectedCharacter == this.skins.Count) 
            selectedCharacter = 0;
        this.skins[selectedCharacter].SetActive(true) ;
        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
    }

    public virtual void ChangePrevious()
    {
        this.skins[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if (selectedCharacter == -1)
            selectedCharacter = this.skins.Count -1;
        this.skins[selectedCharacter].SetActive(true);
        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
    }

} 
