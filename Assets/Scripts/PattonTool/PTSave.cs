using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R55555LLING.ePEa.DataControl;
using System.IO;

public class PTSave : MonoBehaviour
{
    [SerializeField]
    GameObject[] m_notes;

    string m_path = "Assets/DataFiles/Pattons";

    public void PattonSave()
    {

    }

    void ExportToData()
    {
        DCurveNote[] curveNote = new DCurveNote[m_notes[1].transform.childCount];
        for (int i = 0; i < m_notes[0].transform.childCount; i++)
        {
            curveNote[i].startTime = m_notes[0].transform.GetChild(i).GetComponent<PTCurveNote>().m_startTime;
            curveNote[i].endTime = m_notes[0].transform.GetChild(i).GetComponent<PTCurveNote>().m_endTime;
            curveNote[i].startAngle = m_notes[0].transform.GetChild(i).GetComponent<PTCurveNote>().m_startDir;
            curveNote[i].endAngle = m_notes[0].transform.GetChild(i).GetComponent<PTCurveNote>().m_endDir;
        }

        DTouchNote[] touchNote = new DTouchNote[m_notes[0].transform.childCount];
        for(int i=0;i<m_notes[0].transform.childCount;i++)
        {
            touchNote[i].time = m_notes[1].transform.GetChild(i).GetComponent<PTTouchNote>().m_clearTime;
        }
    }
}