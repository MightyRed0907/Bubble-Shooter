using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Shinn;

public class Score : MonoBehaviour
{
    [SerializeField] private GameObject success;
    [SerializeField] private GameObject fall;

    private SimpleItween m_myitween;
    private Text m_mytext;
    private bool m_pause = false;
    private bool m_flag = false;

    private void Start()
    {
        m_mytext = GetComponent<Text>();
        m_myitween = GetComponent<SimpleItween>();
    }

    private void Update()
    {
        if (!m_pause)
        {
            float time = Time.time;
            m_mytext.text = time.ToString("0.0");
        }

        if(success.activeSelf || fall.activeSelf)
        {
            m_pause = true;
            if (!m_flag)
            {
                m_flag = true;
                m_myitween.CallStart();
            }
        }
    }
}
