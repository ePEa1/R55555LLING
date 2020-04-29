using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PTSwipeNote : MonoBehaviour
{
    public double m_time;
    public float m_angle;
    public int m_dir;

    MeshRenderer m_mr;

    private void Awake()
    {
        m_mr = GetComponent<MeshRenderer>();
        m_mr.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_time - PTPlayManager.g_moveSpeed <= PTPlayManager.g_time)
        {
            m_mr.enabled = true;
            transform.localEulerAngles = new Vector3(0, 0, m_angle);
            transform.position = new Vector3(Mathf.Cos(m_angle * PTPlayManager.g_corVec), Mathf.Sin(m_angle * PTPlayManager.g_corVec), 0) *
                ((PTPlayManager.g_inLine + (PTPlayManager.g_outLine - PTPlayManager.g_inLine)) * (float)((PTPlayManager.g_time - m_time + PTPlayManager.g_moveSpeed) / PTPlayManager.g_moveSpeed) - PTPlayManager.g_outLine)
                + Vector3.forward * (PTPlayManager.g_depth - (float)((PTPlayManager.g_time - m_time + PTPlayManager.g_moveSpeed) / PTPlayManager.g_moveSpeed) * PTPlayManager.g_depth);

            if (m_time < PTPlayManager.g_time)
            {
                m_mr.enabled = false;
            }
        }
        else
        {
            m_mr.enabled = false;
        }
    }
}
