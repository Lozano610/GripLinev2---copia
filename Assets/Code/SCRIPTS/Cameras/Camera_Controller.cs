using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Camera_Controller : MonoBehaviour
{
    #region Variables
    //Position of the target, in this case, the player
    public Transform target;
    public Transform pos1;
    public Transform pos2;
    public Transform cam;
    //Camera velocity
    public float cameraSpeed = 2;
    public bool isBack = false;
    // Update is called once per frame
    #endregion
    #region UnityMethods
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        //The camera takes the position of the player and follows it
        transform.position = Vector3.Lerp(target.position, target.position, cameraSpeed * Time.deltaTime);
        if (isBack == true)
        {
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, pos2.rotation, cameraSpeed * Time.deltaTime);
            cam.transform.position = Vector3.Lerp(cam.transform.position, pos2.position, cameraSpeed * Time.deltaTime);
        }
        else
        {
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, pos1.rotation, cameraSpeed * Time.deltaTime);
            cam.transform.position = Vector3.Lerp(cam.transform.position, pos1.position, cameraSpeed * Time.deltaTime);
        }
    }
    #endregion
    #region OwnMethods
    public void PosTop()
    {
        isBack = false;
        cam.transform.position = pos1.position;
        cam.transform.rotation = pos1.rotation;
    }
    public void PosBack()
    {
        isBack = true;
        cam.transform.position = pos2.position;
        cam.transform.rotation = pos2.rotation;
    }
    public void ChangeCamera()
    {if(isBack == false)
        {
            isBack = true;
            cam.transform.position = pos1.position;
            cam.transform.rotation = pos1.rotation;
        }
        else
        {
            isBack = false;
            cam.transform.position = pos2.position;
            cam.transform.rotation = pos2.rotation;
        }
        
    }
    #endregion
}
