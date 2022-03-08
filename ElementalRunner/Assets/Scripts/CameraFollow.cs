using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Rigidbody playerBody;
    public int followDistance;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(this.playerBody.transform.position.x - this.followDistance, this.transform.position.y, this.transform.position.z);
    }
}
