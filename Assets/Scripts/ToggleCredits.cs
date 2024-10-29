using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCredits : MonoBehaviour
{
    [SerializeField] CanvasGroup creditsGroup;
    private GameObject fireSFX;
    void Start()
    {
        if (SoundManager.instance)
        {
          fireSFX =  SoundManager.instance.PlaySound(transform.position, SoundManager.instance.FindSoundInfoByName("Fire"));
        } 
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(ToggleScreen(8));
    }

    IEnumerator ToggleScreen(float delay)
    {
        yield return new WaitForSeconds(delay);
        creditsGroup.alpha += Time.deltaTime;
        fireSFX.GetComponent<AudioSource>().volume -= Time.deltaTime;
    }
}
