using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalSwitcher : MonoBehaviour
{
    public GameObject fireBody;
    public GameObject waterBody;
    public float bounceFactor;
    public float bounceFrequency;

    public GameObject playerReference;

    // Start is called before the first frame update
    void Start()
    {
        this.BounceAnimation();

    }

    // Update is called once per frame
    void Update()
    {
        this.BounceAnimation();
    }

    public void BounceAnimation()
    {
        if (this.playerReference.GetComponent<Player>().transform.position.y < -0.35f)
        {
            float fireBounceFactor = (float)(this.bounceFactor + 0.05);

            this.fireBody.transform.localScale = new Vector3((float)(1 - fireBounceFactor + (fireBounceFactor * Math.Sin(Time.fixedTime * this.bounceFrequency))), (float)(1 - this.bounceFactor + (this.bounceFactor * Math.Sin(Time.fixedTime * (this.bounceFrequency + 5)))), 1);
            this.waterBody.transform.localScale = new Vector3(1, (float)(1 - this.bounceFactor + (this.bounceFactor * Math.Sin(Time.fixedTime * this.bounceFrequency))), 1);
        }
        else
        {
            this.fireBody.transform.localScale = new Vector3(0.9f, 1.2f, 0.9f);
            this.waterBody.transform.localScale = new Vector3(0.9f, 1.2f, 0.9f);
        }
    }
}
