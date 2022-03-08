using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineBurnScript : MonoBehaviour
{
    public GameObject vineMesh;
    public GameObject vineFire;

    private void Start()
    {
        this.vineFire.SetActive(false);    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (other.GetComponent<Player>().playerFireState)
            {
                case true:
                    this.vineMesh.SetActive(false);
                    if (!SoundStore.fireFloush.isPlaying)
                        SoundStore.fireFloush.Play();
                    this.vineFire.SetActive(true);
                    break;
                case false:
                    break;
            }

        }
    }

}
