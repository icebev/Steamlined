using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    public float twistStep;
    public float cameraPan;
    public float panFrequency;

    public GameObject playerTarget;
    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, this.playerTarget.transform.rotation, this.twistStep * Time.deltaTime);

        this.transform.position += new Vector3(0, 0, this.cameraPan * Time.deltaTime * (float)Math.Sin(Time.realtimeSinceStartup * this.panFrequency));
    }
}
