using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace R55555LLING.ePEa.DataControl
{
    public class PattonData : MonoBehaviour
    {
    }

    [System.Serializable]
    public class PattonDatas
    {
        public int maxNote;

        public DTouchNote[] touchNote;
        public DLongNote[] longNote;
        public DCurveNote[] curveNote;
        public DSwipeNote[] swipeNote;
    }

    [System.Serializable]
    public class DTouchNote
    {
        public double time;
        public float angle;
    }

    [System.Serializable]
    public class DLongNote
    {
        public double startTime;
        public double endTime;
        public float angle;
    }

    [System.Serializable]
    public class DCurveNote
    {
        public double startTime;
        public double endTime;
        public float startAngle;
        public float endAngle;
    }

    [System.Serializable]
    public class DSwipeNote
    {
        public double time;
        public float angle;
        public int dir;
    }
}
