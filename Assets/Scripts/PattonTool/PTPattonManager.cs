using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R55555LLING.ePEa.DataControl;
using UnityEngine.UI;

public class PTPattonManager : MonoBehaviour
{
    [SerializeField]
    InputField m_songId; //곡 아이디
    [SerializeField]
    Text m_songName; //곡 이름
    [SerializeField]
    Text m_noteMany; //노트 개수

    [SerializeField]
    GameObject[] m_notes;

    SongData m_nowSong;

    // Start is called before the first frame update
    void Awake()
    {
        m_nowSong = new SongData();
    }

    // Update is called once per frame
    void Update()
    {
        int noteCount = m_notes[0].transform.childCount * 2 + m_notes[1].transform.childCount + m_notes[2].transform.childCount * 2 + m_notes[3].transform.childCount;
        m_noteMany.text = noteCount.ToString();
    }

    public void SetSongId()
    {
        if (SongListData.GetSonglist.ContainsKey(int.Parse(m_songId.text)))
        {
            m_nowSong = SongListData.GetSonglist[int.Parse(m_songId.text)];
            m_songName.text = m_nowSong.SongName;
        }
    }
}
