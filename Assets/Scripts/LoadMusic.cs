using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R55555LLING.ePEa.DataControl;
using UnityEngine.UI;

public class LoadMusic : MonoBehaviour
{
    AudioSource m_ac;

    [SerializeField]
    InputField m_if;

    void Awake()
    {
        m_ac = GetComponent<AudioSource>();
    }

    public void ChangeMusic()
    {
        AudioClip ac = Resources.Load<AudioClip>("Music/" + SongListData.GetSonglist[int.Parse(m_if.text)].SongFileName);
        m_ac.clip = ac;
    }
}
