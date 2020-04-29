using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMusic : MonoBehaviour
{
    AudioSource m_ac;

    void Awake()
    {
        m_ac = GetComponent<AudioSource>();
    }

    public void ChangeMusic(int id)
    {

    }
}
