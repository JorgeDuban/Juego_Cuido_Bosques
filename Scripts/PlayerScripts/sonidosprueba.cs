using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sonidosprueba : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource Audio;
    public AudioClip xdd;

    void Start()
    {
        Audio = GetComponent<AudioSource>();  
    }

    // Update is called once per frame
    void Update()
    {
        xdddd();
    }
    void xdddd()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Audio.Play();
        }
    }
}
