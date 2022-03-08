using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacle : MonoBehaviour
{

    public enum ObstaclePasses
    {
        Default,
        Fire,
        Water
    }
    public ObstaclePasses obstaclePasses;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (this.obstaclePasses)
            {
                case ObstaclePasses.Default:
                    if (!SoundStore.playedDeathSFX)
                    {
                        SoundStore.splat.Play();
                        SoundStore.playedDeathSFX = true;
                    }
                    Player.GameOver();
                    break;
                case ObstaclePasses.Fire:
                    if (!other.GetComponent<Player>().playerFireState)
                    {
                        if (!SoundStore.playedDeathSFX)
                        {
                            SoundStore.sizzle.Play();
                            SoundStore.playedDeathSFX = true;
                        }
                        Player.GameOver(); 
                    }
                    break;
                case ObstaclePasses.Water:
                    if (other.GetComponent<Player>().playerFireState)
                    {
                        if (!SoundStore.playedDeathSFX)
                        {
                            SoundStore.sizzle.Play();
                            SoundStore.playedDeathSFX = true;
                        }
                        Player.GameOver(); 
                    }
                    break;
            }

        }
    }

   

}
