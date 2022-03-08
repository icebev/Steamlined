using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalSlowAnimation : MonoBehaviour
{
    public GameObject fireBody;
    public GameObject waterBody;
    public float bounceFactor;
    public float bounceFrequency;

    public GameObject playerReference;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
            //float fireBounceFactor = (float)(this.bounceFactor + 0.05);

            //this.fireBody.transform.localScale = new Vector3((float)(1 - fireBounceFactor + (fireBounceFactor * Math.Sin(Time.fixedTime * this.bounceFrequency))), (float)(1 - this.bounceFactor + (this.bounceFactor * Math.Sin(Time.fixedTime * (this.bounceFrequency + 5)))), 1);
            this.waterBody.transform.localScale = new Vector3(1, (float)(1 - this.bounceFactor + (this.bounceFactor * Math.Sin(Time.fixedTime * this.bounceFrequency))), 1);

    }
}
