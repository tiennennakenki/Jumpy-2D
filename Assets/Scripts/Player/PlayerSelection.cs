using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerSelection : SaiMonoBehaviour
{
    [SerializeField] protected List<GameObject> playerPrefabs = new List<GameObject>();
    public List<GameObject> PlayerPrefabs => playerPrefabs;

    [SerializeField] protected int characterIndex;
    public int CharacterIndex => characterIndex;

    [SerializeField] protected CameraController cameraController;
    public CameraController CameraController => cameraController;

    protected override void Awake()
    {
        base.Awake();
        this.SetCharacter();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerPrefabs();
        this.LoadCameraController();
    }

    protected virtual void LoadCameraController()
    {
        if (this.cameraController != null) return;
        this.cameraController = GameObject.Find("CameraController").GetComponent<CameraController>();
        Debug.LogWarning(transform.name + ": LoadCameraController", gameObject);
    }

    protected virtual void SetCharacter()
    {
        this.characterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        GameObject player =  Instantiate(playerPrefabs[characterIndex], transform.position, Quaternion.identity);
        cameraController.player = player.transform;
        player.SetActive(true);
    }

    protected virtual void LoadPlayerPrefabs()
    {
        if (this.playerPrefabs.Count > 0) return;

        Transform collection = transform;

        foreach (Transform col in collection)
        {
            GameObject player = col.gameObject; // Get the GameObject from the Transform
            this.playerPrefabs.Add(player);
        }

        Debug.LogWarning(transform.name + ": LoadPlayerPrefabs", gameObject);
    }
}
