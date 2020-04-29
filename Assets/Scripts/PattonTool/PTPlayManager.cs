using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PTPlayManager : MonoBehaviour
{
    public static double g_moveSpeed = 1.0;
    public static double g_time = 0.0;

    public static float g_corVec = Mathf.Asin(1) * 4.0f / 360.0f; //360도 각도에서 방향벡터로 바꿀때 곱하는 상수

    public static float g_outLine = 5.0f; //바깥쪽 원 반지름
    public static float g_inLine = 1.0f; //안쪽 원 반지름
    public static float g_depth = 8.0f; //깊이
    public static float g_lineAngle = 10.0f;

    [SerializeField]
    double m_moveSpeed;
    [SerializeField]
    Text m_timeText;
    [SerializeField]
    AudioSource m_ac;

    bool m_start = false;

    float m_startTime = 0.0f;
    
    // Start is called before the first frame update
    void Awake()
    {
        g_moveSpeed = m_moveSpeed;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (m_start)
            {
                m_start = false;
                m_ac.Stop();
                g_time = m_startTime;
            }
            else
            {
                m_start = true;
                m_ac.time = m_startTime;
                m_ac.Play();
            }
        }

        if (!m_start)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                m_startTime += Time.deltaTime;
                g_time = m_startTime;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                m_startTime = Mathf.Max(0, m_startTime - Time.deltaTime);
                g_time = m_startTime;
            }
        }

        m_timeText.text = g_time.ToString();
    }

    private void FixedUpdate()
    {
        if (m_start)
            g_time += Time.fixedDeltaTime;
    }
}
