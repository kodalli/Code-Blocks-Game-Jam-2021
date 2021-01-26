using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    [SerializeField] private AudioSource gunShot;
    [SerializeField] private AudioSource hitMarker;
    [SerializeField] private AudioSource pop;
    [SerializeField] private AudioSource theme1;
    [SerializeField] private AudioSource systemActivate;
    [SerializeField] private AudioSource purchase;
    [SerializeField] private AudioSource purchaseFail;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        theme1.Play();
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

    public void PlaySystemActivate()
    {
        systemActivate.PlayOneShot(systemActivate.clip, 0.3f);
    }

    public void PlayPurchase()
    {
        purchase.PlayOneShot(purchase.clip, 0.6f);
    }

    public void PlayPurchaseFail()
    {
        purchaseFail.PlayOneShot(purchaseFail.clip, 0.6f);
    }
}
