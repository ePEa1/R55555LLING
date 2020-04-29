using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R55555LLING.ePEa.DataControl;
using System.IO;
using UnityEngine.UI;

public class PTSave : MonoBehaviour
{
    [SerializeField]
    GameObject[] m_notes;
    [SerializeField]
    Text m_maxNote;

    PTPattonManager m_pm;
    string m_path = "Assets/Resources/Pattons/";

    private void Awake()
    {
        m_pm = GetComponent<PTPattonManager>();
    }

    public void PattonSave()
    {
        //Debug.Log(m_pm.m_nowSong.Id);
        if (m_pm.m_nowSong.Id != 0)
        {
            Debug.Log("Saving...");

            PattonDatas data = NoteToData();
            data.maxNote = int.Parse(m_maxNote.text);

            FileStream f = new FileStream(m_path + m_pm.m_nowSong.Id + ".txt", FileMode.Create, FileAccess.Write);
            string json = JsonHelper.ToJson<PattonDatas>(data);
            StreamWriter writer = new StreamWriter(f, System.Text.Encoding.Unicode);
            writer.WriteLine(json);
            writer.Close();

            Debug.Log("Save Finish");
        }
    }

    PattonDatas NoteToData()
    {
        PattonDatas data = new PattonDatas();

        DCurveNote[] curveNote = new DCurveNote[m_notes[0].transform.childCount];
        for (int i = 0; i < m_notes[0].transform.childCount; i++)
        {
            curveNote[i] = new DCurveNote();
            curveNote[i].startTime = m_notes[0].transform.GetChild(i).GetComponent<PTCurveNote>().m_startTime;
            curveNote[i].endTime = m_notes[0].transform.GetChild(i).GetComponent<PTCurveNote>().m_endTime;
            curveNote[i].startAngle = m_notes[0].transform.GetChild(i).GetComponent<PTCurveNote>().m_startDir;
            curveNote[i].endAngle = m_notes[0].transform.GetChild(i).GetComponent<PTCurveNote>().m_endDir;
        }

        DTouchNote[] touchNote = new DTouchNote[m_notes[1].transform.childCount];
        for(int i=0;i<m_notes[1].transform.childCount;i++)
        {
            touchNote[i] = new DTouchNote();
            touchNote[i].time = m_notes[1].transform.GetChild(i).GetComponent<PTTouchNote>().m_clearTime;
            touchNote[i].angle = m_notes[1].transform.GetChild(i).GetComponent<PTTouchNote>().m_angle;
        }

        DLongNote[] longNote = new DLongNote[m_notes[2].transform.childCount];
        for (int i = 0; i < m_notes[2].transform.childCount; i++)
        {
            longNote[i] = new DLongNote();
            longNote[i].startTime = m_notes[2].transform.GetChild(i).GetComponent<PTLongNote>().m_startTime;
            longNote[i].endTime = m_notes[2].transform.GetChild(i).GetComponent<PTLongNote>().m_endTime;
            longNote[i].angle = m_notes[2].transform.GetChild(i).GetComponent<PTLongNote>().m_angle;
        }

        DSwipeNote[] swipeNote = new DSwipeNote[m_notes[3].transform.childCount];
        for (int i = 0; i < m_notes[3].transform.childCount; i++)
        {
            swipeNote[i] = new DSwipeNote();
            swipeNote[i].time = m_notes[3].transform.GetChild(i).GetComponent<PTSwipeNote>().m_time;
            swipeNote[i].angle = m_notes[3].transform.GetChild(i).GetComponent<PTSwipeNote>().m_angle;
            swipeNote[i].dir = m_notes[3].transform.GetChild(i).GetComponent<PTSwipeNote>().m_dir;
        }

        data.curveNote = curveNote;
        data.touchNote = touchNote;
        data.longNote = longNote;
        data.swipeNote = swipeNote;

        return data;
    }
}