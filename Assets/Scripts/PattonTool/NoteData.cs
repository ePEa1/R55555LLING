using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace R55555LLING.ePEa.DataControl
{
    public class NoteData : MonoBehaviour
    {
    }

    [System.Serializable]
    public class NoteDatas
    {
        public DTouchNote[] touchNote;
        public DHoldNote[] holdNote;
        public DCurveNote[] curveNote;
        public DSwipeNote[] swipeNote;
    }

    public class DTouchNote
    {
        public double time;
        public float angle;
    }

    public class DHoldNote
    {
        public double startTime;
        public double endTime;
        public float angle;
    }

    public class DCurveNote
    {
        public double startTime;
        public double endTime;
        public float startAngle;
        public float endAngle;
    }

    public class DSwipeNote
    {
        public double time;
        public float angle;
        public int dir;
    }
}
