using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R55555LLING.ePEa.SelectSong;
using R55555LLING.ePEa.DataControl;

public class SongControl : MonoBehaviour
{
    [SerializeField]
    KeyCode m_turnClock;
    [SerializeField]
    KeyCode m_turnReverse;

    [SerializeField]
    KeyCode[] m_upleft;
    [SerializeField]
    KeyCode[] m_upright;
    [SerializeField]
    KeyCode[] m_downleft;
    [SerializeField]
    KeyCode[] m_downright;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(m_turnClock))
        {
            SelectSongManager.g_selectSongNumber = (SelectSongManager.g_selectSongNumber + 1) % (SongListData.GetSonglist.Count + 1);
            if (SelectSongManager.g_selectSongNumber == 0)
                SelectSongManager.g_selectSongNumber++;
            SelectSongManager.GetSelectSongManager.ChangeSong(SongListData.GetSonglist[SelectSongManager.g_selectSongNumber + 100].Id);
        }

        if (Input.GetKeyDown(m_turnReverse))
        {
            SelectSongManager.g_selectSongNumber--;
            if (SelectSongManager.g_selectSongNumber < 1)
            {
                SelectSongManager.g_selectSongNumber = SongListData.GetSonglist.Count;
            }

            SelectSongManager.GetSelectSongManager.ChangeSong(SongListData.GetSonglist[SelectSongManager.g_selectSongNumber + 100].Id);
        }
    }
}
