using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchBehavior1 : MonoBehaviour {
    
    public Vector2[] touchPos;
    public Vector2[] TouchPos { get { return touchPos; } }

    private void Update()
    {
        TouchProcessed();
    }

    private void TouchProcessed()
    {
        int fingerCount = 0;
        touchPos = new Vector2[FingerCount()];
        foreach (Touch touch in Input.touches)
        {            
            if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
            {
                fingerCount++;
                for (int i = 0; i < fingerCount; i++)
                    touchPos[i] = Input.GetTouch(i).position;
            }
        }
    }

    private int FingerCount()
    {
        int fingerCount = 0;
        foreach (Touch touch in Input.touches)
            if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
                fingerCount++;

        if (fingerCount > 0)
            return fingerCount;
        else
            return 0;
    }
}
