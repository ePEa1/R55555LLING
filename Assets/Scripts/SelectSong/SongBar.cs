using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using R55555LLING.ePEa.DataControl;

public class SongBar : MonoBehaviour
{
    SongData m_songData;

    [SerializeField]
    Text m_songName;
    [SerializeField]
    Text m_composer;

    [SerializeField]
    int m_songOffset;

    // Start is called before the first frame update
    void Awake()
    {
        m_songName.text = "";
        m_composer.text = "";
    }

    private void Start()
    {
        m_songName.text = m_songData.SongName;
        m_composer.text = m_songData.Composer;
    }

    public void ChangeSong()
    {

    }
}
