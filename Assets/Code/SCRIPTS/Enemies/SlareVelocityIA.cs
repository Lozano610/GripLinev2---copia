using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlareVelocityIA : MonoBehaviour
{
    public SplineAnimator miplineAnimator;
    public float speed;
    public float aceleracion = 1;
    public RivalType Types = RivalType.Normal;
    //public float StartSpeed;
    public int RivalNumber;
    public FlywightEnemy Flyweight;
    // Start is called before the first frame update
    void Start()
    {
        miplineAnimator = GetComponentInParent<SplineAnimator>();
        speed = Random.Range(0.02f, 0.05f);
        miplineAnimator.speed = speed;
        Flyweight.StartSpeed = speed;
        int RandomMode = Random.Range(0, 3);
        if( RandomMode == 1)
        {
           Types = RivalType.Slow;
        }
        if (RandomMode == 2)
        {
            Types = RivalType.Crazy;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        miplineAnimator.speed = Mathf.Lerp(miplineAnimator.speed, speed, Time.deltaTime * aceleracion);
    }
    public enum RivalType
    {
        Slow,Normal,Crazy
    }
    public void ResetSpeed()
    {
        speed = Flyweight.StartSpeed;
    }
}
