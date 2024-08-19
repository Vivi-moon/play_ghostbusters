using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public AudioClip flareShotSound;
    public AudioClip noAmmoSound;
    public AudioClip reloadSound;


    public void Shoot()
    {
        GetComponent<Animation>().CrossFade("Shoot");
        GetComponent<AudioSource>().PlayOneShot(flareShotSound);
    }
    
}
