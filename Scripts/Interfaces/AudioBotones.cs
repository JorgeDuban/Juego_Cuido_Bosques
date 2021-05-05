using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioBotones : MonoBehaviour
{
    public AudioSource source { get { return GetComponent<AudioSource>(); } }
    public Button clic { get { return GetComponent<Button>(); } }
    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
       gameObject.AddComponent<AudioSource>();
        clic.onClick.AddListener(PalySound);
    }

    // Update is called once per frame
    void PalySound()
    {
        source.PlayOneShot(clip);
    }
}
