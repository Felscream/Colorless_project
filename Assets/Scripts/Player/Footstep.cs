using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep : MonoBehaviour {
    [SerializeField]
    private AudioClip[] concrete;
    [SerializeField]
    private float stepTime;
    private AudioSource audioSource;
    private PlayerMovement mv;
    private Rigidbody rb;
    private bool step = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mv = PlayerMovement.GetInstance();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        
    }
    private IEnumerator WaitForSteps(float wait)
    {
        step = true;
        yield return new WaitForSeconds(wait);
        step = false;
    }

    public bool GetStep()
    {
        return step;
    }
    public void PlayConcrete()
    {
        audioSource.clip = concrete[Random.Range(0, concrete.Length)];
        audioSource.Play();
        StartCoroutine(WaitForSteps(stepTime));
    }
}
