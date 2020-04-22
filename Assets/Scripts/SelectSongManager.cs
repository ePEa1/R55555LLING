using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R55555LLING.ePEa.SelectSong.SongDataControl;

namespace R55555LLING.ePEa.SelectSong
{
    public class SelectSongManager : MonoBehaviour
    {
        public enum _Difficult
        {
            EASY,
            NORMAL,
            HARD
        }

        static SelectSongManager selectSongManager;
        public static SelectSongManager GetSelectSongManager { get { return selectSongManager; } }

        static _Difficult g_diffi; //현재 선택한 난이도
        public static _Difficult GetDiffi { get { return g_diffi; } }

        static SongData g_selectSong;
        public static SongData GetSelectSong { get { return g_selectSong; } }

        [SerializeField]
        AudioSource m_ac;

        void Awake()
        {
            if (!GetSelectSongManager)
            {
                selectSongManager = this;
                g_diffi = _Difficult.EASY;
                g_selectSong = SongListData.GetSonglist[0];
            }
            else
            {
                Destroy(this);
                Destroy(gameObject);
            }
        }

        public void ChangeSong(int _songNum)
        {
            if (g_selectSong.Id!=_songNum)
            {
                g_selectSong = SongListData.GetSonglist[_songNum];
                ChangeSongEvent();
            }
        }

        //곡 변경될 때 실행시킬 이벤트
        void ChangeSongEvent()
        {

        }
    }
}