using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R55555LLING.ePEa.DataControl;
using System.IO;
using UnityEngine.UI;

public class PTLoad : MonoBehaviour
{
    [SerializeField]
    GameObject[] notes;

    [SerializeField]
    Transform[] noteParent;

    [SerializeField]
    InputField m_id;

    public void LoadPatton()
    {
        for (int i = 0; i < noteParent.Length; i++)
        {
            for (int j = 0; j < noteParent[i].childCount; j++)
            {
                Destroy(noteParent[i].GetChild(0).gameObject);
            }
        }

        PTPlayManager.g_time = 0.0;

        TextAsset t = Resources.Load<TextAsset>("Pattons/" + m_id.text);
        
        if (t != null)
        {
            PattonDatas data = JsonUtility.FromJson<PattonDatas>(t.text);

            for (int i = 0; i < data.curveNote.Length; i++)
            {
                GameObject note = Instantiate(notes[0]);
                note.transform.position = new Vector3(0, 0, 0);
                note.transform.parent = noteParent[0];
                note.GetComponent<PTCurveNote>().m_startDir = data.curveNote[i].startAngle;
                note.GetComponent<PTCurveNote>().m_endDir = data.curveNote[i].endAngle;
                note.GetComponent<PTCurveNote>().m_startTime = data.curveNote[i].startTime;
                note.GetComponent<PTCurveNote>().m_endTime = data.curveNote[i].endTime;
            }

            for (int i = 0; i < data.touchNote.Length; i++)
            {
                GameObject note = Instantiate(notes[1]);
                note.transform.position = new Vector3(0, 0, 0);
                note.transform.parent = noteParent[1];
                note.GetComponent<PTTouchNote>().m_clearTime = data.touchNote[i].time;
                note.GetComponent<PTTouchNote>().m_angle = data.touchNote[i].angle;
            }

            for (int i = 0; i < data.longNote.Length; i++)
            {
                GameObject note = Instantiate(notes[2]);
                note.transform.position = new Vector3(0, 0, 0);
                note.transform.parent = noteParent[2];
                note.GetComponent<PTLongNote>().m_startTime = data.longNote[i].startTime;
                note.GetComponent<PTLongNote>().m_endTime = data.longNote[i].endTime;
                note.GetComponent<PTLongNote>().m_angle = data.longNote[i].angle;
            }

            for (int i = 0; i < data.swipeNote.Length; i++)
            {
                GameObject note = Instantiate(notes[3]);
                note.transform.position = new Vector3(0, 0, 0);
                note.transform.parent = noteParent[3];
                note.GetComponent<PTSwipeNote>().m_time = data.swipeNote[i].time;
                note.GetComponent<PTSwipeNote>().m_angle = data.swipeNote[i].angle;
                note.GetComponent<PTSwipeNote>().m_dir = data.swipeNote[i].dir;
            }
        }
    }
}
