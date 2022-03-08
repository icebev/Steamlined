using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXLoader : MonoBehaviour
{
    public AudioSource floush;
    public AudioSource sizzle;
    public AudioSource splat;
    // Start is called before the first frame update
    void Start()
    {
        SoundStore.fireFloush = this.floush;
        SoundStore.sizzle = this.sizzle;
        SoundStore.splat = this.splat;

    }

}
