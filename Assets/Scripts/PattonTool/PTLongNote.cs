using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PTLongNote : MonoBehaviour
{
    public double m_startTime;
    public double m_endTime;
    public float m_angle;

    double startTime;
    double endTime;

    LineRenderer m_lr;

    float m_outLen = 0.0f;
    float m_inLen = 0.0f;

    // Start is called before the first frame update
    void Awake()
    {
        startTime = m_startTime * (60.0 / PTPattonManager.m_nowSong.BPM);
        endTime = m_endTime * (60.0 / PTPattonManager.m_nowSong.BPM);

        m_lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        startTime = m_startTime * (60.0 / PTPattonManager.m_nowSong.BPM);
        endTime = m_endTime * (60.0 / PTPattonManager.m_nowSong.BPM);

        if (PTPlayManager.g_time >= startTime - PTPlayManager.g_moveSpeed)
        {
            m_lr.enabled = true;
            m_outLen = 1.0f - Mathf.Max(0, (float)(startTime - PTPlayManager.g_time) * (1.0f / (float)PTPlayManager.g_moveSpeed));

            float outPos = m_outLen * (PTPlayManager.g_outLine - PTPlayManager.g_inLine) + PTPlayManager.g_inLine;
            float inPos = m_inLen * (PTPlayManager.g_outLine - PTPlayManager.g_inLine) + PTPlayManager.g_inLine;
            float cos = Mathf.Cos(m_angle * PTPlayManager.g_corVec);
            float sin = Mathf.Sin(m_angle * PTPlayManager.g_corVec);
            m_lr.SetPosition(0, new Vector3(cos, sin, 0) * outPos + Vector3.forward * (1 - m_outLen) * PTPlayManager.g_depth);
            m_lr.SetPosition(1, new Vector3(cos, sin, 0) * inPos + Vector3.forward * (1 - m_inLen) * PTPlayManager.g_depth);
        }
        else
        {
            m_lr.enabled = false;
        }

        //안쪽 라인 계산
        if (PTPlayManager.g_time >= endTime - PTPlayManager.g_moveSpeed)
        {
            m_inLen = Mathf.Min(1, (float)(PTPlayManager.g_time - (endTime - PTPlayManager.g_moveSpeed)) * (1.0f / (float)PTPlayManager.g_moveSpeed));
        }
        else
        {
            m_inLen = 0;
        }

        //다 맞췄으면 점 삭제
        if (PTPlayManager.g_time >= endTime)
        {
            m_lr.enabled = false;
        }

        if (!m_lr.enabled)
        {
            m_inLen = 0;
            m_outLen = 0;
        }
    }
}
