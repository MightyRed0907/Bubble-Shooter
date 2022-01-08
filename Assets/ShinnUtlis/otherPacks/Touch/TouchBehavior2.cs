using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchBehavior2 : MonoBehaviour {
    
    [System.Serializable]
    public struct TouchState
    {
        public int index;
        public Vector2 position;
        public int click;
        public string SendData;

        public void TouchProcessed()
        {
            if (Input.touchCount > index)
            {
                if (Input.touches[index].phase == TouchPhase.Began)
                {
                    click = 1;
                    position = Input.touches[index].position;
                    SendData = "/finger " + index + " " + click + " " + position.x + " " + position.y;
                }

                if (Input.touches[index].phase == TouchPhase.Moved)
                {
                    position = Input.touches[index].position;
                    SendData = "/finger " + index + " " + click + " " + position.x + " " + position.y;
                }

                if (Input.touches[index].phase == TouchPhase.Ended || Input.touches[index].phase == TouchPhase.Canceled)
                {
                    click = 0;
                    SendData = "/finger " + index + " " + click + " " + position.x + " " + position.y;
                }
            }

            if (Input.touchCount == 0)
                click = 0;
        }
    }
    public TouchState[] touch;    

    private void Update()
    {
        for (int i = 0; i < touch.Length; i++)
            touch[i].TouchProcessed();
    }


}
