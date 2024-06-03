using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockCircuits : MonoBehaviour
{
    public Button BlockNascar;
    public Button BlockSenach;
    public Button BlockBreifford;
    public GameObject PanelSenach;
    public GameObject PanelBreifford;
    

    // Start is called before the first frame update
    void Awake()
    {
        BlockBreifford.interactable = false;
        BlockSenach.interactable = false;

        if (MenuManager.Singleton.SceneComplete >= 4 )
        {
            BlockBreifford.interactable = true;
            PanelBreifford.SetActive( false );
            
        }
        if (MenuManager.Singleton.SceneComplete >= 8)
        {
            BlockSenach.interactable = true;
            PanelSenach.SetActive( false );
            
        }
    }

}
