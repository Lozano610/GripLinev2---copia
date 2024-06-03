using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSelectionRotation : MonoBehaviour
{
    // Start is called before the first frame update
    private void FixedUpdate()
    {
        transform.Rotate(0, 0, 0.5f);
    }
}
