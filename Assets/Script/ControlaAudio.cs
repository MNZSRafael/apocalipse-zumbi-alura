using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaAudio : MonoBehaviour
{
    private AudioSource meuAudioSorce;
    public static AudioSource instancia;

    void Awake()
    {
        meuAudioSorce = GetComponent<AudioSource>();
        instancia = meuAudioSorce;
    }
}
