using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region Variables
    public SplineAnimator SpeedPlayer;
    //Player gameObject
    public GameObject J1;
    //Player speed
    public float Max_SpeedPlayer = 0.4f;
    //Image of velocity
    public Image aceleracionImage;
    #endregion
    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        //Initial
        SpeedPlayer.speed = 0;
    }
    // Update is called once per frame
    void Update()
    {
        //Player speed accessing the SplineAnimator
        if (SimpleInput.GetAxis("Vertical") >= 0)
        {
            //Movement in vertical axis
            SpeedPlayer.speed = SimpleInput.GetAxis("Vertical") * Max_SpeedPlayer;
            //Fill the image when the player slide the input
            aceleracionImage.fillAmount = SimpleInput.GetAxis("Vertical");
            //The image accesses the animator component and starts it
            aceleracionImage.GetComponent<Animator>().SetFloat("color", SimpleInput.GetAxis("Vertical"));
        }
    }
    #endregion
}
