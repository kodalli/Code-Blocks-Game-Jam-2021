using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    [SerializeField] private AudioSource gunShot;
    [SerializeField] private AudioSource hitMarker;
    [SerializeField] private AudioSource pop;

    private void Awake()
    {
        instance = this;
    }

    public void PlayGunShot()
    {
        gunShot.PlayOneShot(gunShot.clip);
    }

    public void PlayHitMarker()
    {
        hitMarker.PlayOneShot(hitMarker.clip);
    }

    public void PlayPop()
    {
        pop.PlayOneShot(pop.clip, 0.3f);

    }
}
