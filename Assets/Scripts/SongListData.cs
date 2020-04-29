using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace R55555LLING.ePEa.DataControl
{
    public class SongListData : MonoBehaviour
    {
        static SongListData songListData;
        public static SongListData GetSongListData { get { return songListData; } }

        //곡 데이터 리스트
        static Dictionary<int, SongData> songList;
        public static Dictionary<int, SongData> GetSonglist { get { return songList; } }

        [SerializeField]
        TextAsset m_songList;

        void Awake()
        {
            if (!GetSongListData)
            {
                songListData = this;
                songList = new Dictionary<int, SongData>();
                LoadSongdata();
            }
            else
            {
                Destroy(this);
                Destroy(this.gameObject);
            }
        }

        //곡 데이터 불러오기
        void LoadSongdata()
        {
            SongData[] songs = JsonHelper.FromJson<SongData>(m_songList.text) as SongData[];
            foreach(SongData song in songs)
            {
                songList.Add(song.Id, song);
                
                Debug.Log("[" + song.Id + "]");
                Debug.Log("Song Name : " + song.SongName);
                Debug.Log("Composer : " + song.Composer);
                Debug.Log("Easy : " + song.Level[0]);
                Debug.Log("Normal : " + song.Level[1]);
                Debug.Log("Hard : " + song.Level[2]);
                
            }
        }
    }

    [System.Serializable]
    public class SongData //곡 정보 데이터
    {
        public int Id; //곡 인덱스 넘버
        public string SongName; //표기용 곡 명
        public string Composer; //작곡가

        public int[] Level; //난이도별 레벨

        public string SongFileName; //곡 파일 이름

        public float[] TrialTime; //미리듣기 시간 범위

        public float Bpm; //곡 bpm
    }
}