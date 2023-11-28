using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMenu : SaiMonoBehaviour
{
    public virtual void EndGame()
    {
        Application.Quit();
    }
}
