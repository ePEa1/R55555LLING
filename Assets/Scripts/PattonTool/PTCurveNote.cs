using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PTCurveNote : MonoBehaviour
{
    public double m_startTime;
    public double m_endTime;
    public float m_startDir;
    public float m_endDir;

    float m_outLen = 1.0f; //바깥쪽 라인(1 -> 0)
    float m_inLen = 0.0f; //안쪽 라인(0 -> 1)

    float m_outAngle; //바깥쪽 현재 각도
    float m_inAngle; //안쪽 현재 각도

    float m_angle; //시작과 끝 각도 차이
    double m_dis; //시작과 끝 시간 차이
    float m_cor; //시간에서 각도로 변환할때 곱해야되는 상수
    float m_getTime; //각도에서 시간으로 변환할때 곱해야되는 상수
    
    Transform m_outPoint; //바깥쪽 위치
    Transform m_inPoint; //안쪽 위치

    LineRenderer m_line;
    bool m_isDraw = false;

    // Start is called before the first frame update
    void Awake()
    {
        m_outPoint = transform.GetChild(2);
        m_inPoint = transform.GetChild(1);
        m_line = transform.GetChild(0).GetComponent<LineRenderer>();

        m_inLen = 0;
        m_angle = m_endDir - m_startDir;
        m_dis = m_endTime - m_startTime;
        m_cor = (float)(m_angle / m_dis);
        m_getTime = (float)(m_dis / m_angle);
    }

    // Update is called once per frame
    void Update()
    {
        if (PTPlayManager.g_time >= m_startTime - PTPlayManager.g_moveSpeed)
        {
            //점 생성
            m_inPoint.gameObject.SetActive(true);
            m_outPoint.gameObject.SetActive(true);
            m_isDraw = true;

            //바깥 라인 계산
            m_outLen = Mathf.Max(0, (float)(m_startTime - PTPlayManager.g_time) * (1.0f / (float)PTPlayManager.g_moveSpeed));

            //바깥쪽 각도, 안쪽 각도 계산
            m_outAngle = m_startDir + (float)(PTPlayManager.g_time - (m_startTime - m_outLen * PTPlayManager.g_moveSpeed)) * m_cor;
            m_inAngle = m_startDir + (float)(PTPlayManager.g_time - (m_startTime - (1 - m_inLen) * PTPlayManager.g_moveSpeed)) * m_cor;

            //회전 방향에 따라서 각도 최대/최소값 설정
            if (m_startDir>m_endDir)
            {
                m_outAngle = Mathf.Max(m_outAngle, m_endDir);
                m_inAngle = Mathf.Max(m_inAngle, m_endDir);
            }
            else if (m_startDir<m_endDir)
            {
                m_outAngle = Mathf.Min(m_outAngle, m_endDir);
                m_inAngle = Mathf.Min(m_inAngle, m_endDir);
            }
            else
            {
                m_outAngle = m_endDir;
                m_inAngle = m_endDir;
            }

            //각도값 / 시간값을 가지고 벡터로 변환해서 점위치 계산
            Vector3 outVec = new Vector3(Mathf.Cos(m_outAngle * PTPlayManager.g_corVec), Mathf.Sin(m_outAngle * PTPlayManager.g_corVec), 0);
            m_outPoint.transform.position = outVec * (PTPlayManager.g_outLine - m_outLen * (PTPlayManager.g_outLine - PTPlayManager.g_inLine)) + Vector3.forward * (m_outLen * PTPlayManager.g_depth);

            Vector3 inVec = new Vector3(Mathf.Cos(m_inAngle * PTPlayManager.g_corVec), Mathf.Sin(m_inAngle * PTPlayManager.g_corVec), 0);
            m_inPoint.transform.position = inVec * (PTPlayManager.g_inLine + m_inLen * (PTPlayManager.g_outLine - PTPlayManager.g_inLine)) + Vector3.forward * (PTPlayManager.g_depth - m_inLen * PTPlayManager.g_depth);
        }
        else
        {
            m_inPoint.gameObject.SetActive(false);
            m_outPoint.gameObject.SetActive(false);
            m_isDraw = false;
        }

        //안쪽 라인 계산
        if (PTPlayManager.g_time >= m_endTime - PTPlayManager.g_moveSpeed)
        {
            m_inLen = Mathf.Min(1, (float)(PTPlayManager.g_time - (m_endTime - PTPlayManager.g_moveSpeed)) * (1.0f / (float)PTPlayManager.g_moveSpeed));
        }
        else
        {
            m_inLen = 0;
        }

        //다 맞췄으면 점 삭제
        if (PTPlayManager.g_time >= m_endTime)
        {
            m_inPoint.gameObject.SetActive(false);
            m_outPoint.gameObject.SetActive(false);
            m_isDraw = false;
        }

        //라인렌더러
        if (m_isDraw)
        {
            int pointCount = (int)Mathf.Ceil(Mathf.Abs(m_inAngle - m_outAngle) / PTPlayManager.g_lineAngle);

            float neg;
            if (m_inAngle - m_outAngle > 0)
                neg = 1;
            else neg = -1;

            m_line.positionCount = pointCount + 1;

            float outPoint = (1 - m_outLen) * (PTPlayManager.g_outLine - PTPlayManager.g_inLine);
            float inPoint = m_inLen * (PTPlayManager.g_outLine - PTPlayManager.g_inLine);

            for (int i = 0; i < pointCount; i++)
            {
                float ang = m_outAngle + i * PTPlayManager.g_lineAngle * neg; //보간하려고 하는 점의 각도
                float t = (float)((ang - m_startDir) * m_getTime + m_startTime); //보간하려고 하는 점의 시간값

                //보간점의 시간위치 (0(in) ~ 1(out))
                float tPower = (float)((PTPlayManager.g_moveSpeed - (float)(t - PTPlayManager.g_time)) / PTPlayManager.g_moveSpeed);
                float timePoint = tPower * (PTPlayManager.g_outLine - PTPlayManager.g_inLine);//(4.0f - inPoint);

                m_line.SetPosition(i, new Vector3(Mathf.Cos(ang * PTPlayManager.g_corVec), Mathf.Sin(ang * PTPlayManager.g_corVec), 0) * (PTPlayManager.g_inLine + timePoint) + Vector3.forward * (PTPlayManager.g_depth - (timePoint) * (PTPlayManager.g_depth / (PTPlayManager.g_outLine - PTPlayManager.g_inLine))));
            }
            m_line.SetPosition(pointCount, m_inPoint.position);
        }
        else
        {
            m_inLen = 0;
            m_outLen = 1;
            m_line.positionCount = 0;
        }
    }
}
