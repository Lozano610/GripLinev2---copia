using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;

public class PauseMenu : MonoBehaviour
{
    #region Variables
    public bool isPaused;
    public GameObject MenuPause;
    public GameObject PauseCanvas;
    public GameObject Hud;
    public GameObject OptionsPause;
    public GameObject CameraCanvas;
    public GameObject SoundCanvas;
    public GameObject GraphicsCanvas;
    public RenderPipelineAsset[] QualityLevels;
    public TMP_Dropdown QualityDropdown;
    Coins CoinsText;
    //public ManagerSavingObjects managerSavingObjects;

    #endregion
    #region UnityMethods
    private void Start()
    {
        QualityDropdown.value = QualitySettings.GetQualityLevel();
    }
    #endregion
    #region OwnMethods
    public void Pause()
    {
        if(isPaused == false)
        {
            Hud.SetActive(false);
            isPaused = true;
            MenuPause.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void ContinueGame()
    {
        if (isPaused == true)
        {
            Hud.SetActive(true);
            isPaused = false;
            MenuPause.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void ResetGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void BackMenú (string PrincipalMenu)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(PrincipalMenu);
        //CoinsText.CoinsNumber.text = string.Empty;
    }
    public void ExitGame()
    {
        ManagerSavingObjects.Singleton.Guardar();
        Application.Quit();
    }
    public void SelectOptions()
    {
        PauseCanvas.SetActive(false);
       OptionsPause.SetActive(true);
    }
    public void CameraOptions()
    {
        OptionsPause.SetActive(false);
        CameraCanvas.SetActive(true);
    }
    public void SoundOptions()
    {
        OptionsPause.SetActive(false);
        SoundCanvas.SetActive(true);
    }
    public void GraphicsOptions()
    {
        OptionsPause.SetActive(false);
        GraphicsCanvas.SetActive(true);
    }
    public void ChangeQuality(int value)
    {
        QualitySettings.SetQualityLevel(value);
        QualitySettings.renderPipeline = QualityLevels[value];
    }
    #endregion
}
