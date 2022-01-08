using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;

namespace Shinn
{

    public class CamMotionv3 : MonoBehaviour
    {

        [Space]
        public int MoveToCameraIndex = 0;
        public ViewPoint[] viewpoint;

        [Button]
        public void StartButton()
        {
            if (viewpoint.Length > 0)
                if (viewpoint[MoveToCameraIndex].isLical)
                    MoveFunction(MoveToCameraIndex);
                else
                    MoveFunctionLocal(MoveToCameraIndex);
        }

        public void Go(int index)
        {
            if (viewpoint.Length > 0)
                if (viewpoint[index].isLical)
                    MoveFunction(index);
                else
                    MoveFunctionLocal(index);
        }

        private void MoveFunction(int _index)
        {
            iTween.MoveTo(gameObject, iTween.Hash("position", viewpoint[_index].target.position, "easetype", viewpoint[_index].ease, "time", viewpoint[_index].time, "delay", viewpoint[_index].delaytime));
            iTween.RotateTo(gameObject, iTween.Hash("rotation", viewpoint[_index].target.eulerAngles, "delay", viewpoint[_index].delaytime, "time", viewpoint[_index].rottime));
        }

        private void MoveFunctionLocal(int _index)
        {
            iTween.MoveTo(gameObject, iTween.Hash("position", viewpoint[_index].target.localPosition, "easetype", viewpoint[_index].ease, "time", viewpoint[_index].time, "delay", viewpoint[_index].delaytime));
            iTween.RotateTo(gameObject, iTween.Hash("rotation", viewpoint[_index].target.localEulerAngles, "delay", viewpoint[_index].delaytime, "time", viewpoint[_index].rottime));
        }

        private void OnDisable()
        {
            var itween = GetComponent<iTween>();
            if (itween != null)
                Destroy(itween);
        }
    }

    [System.Serializable]
    public class ViewPoint
    {

        public Transform target;
        public iTween.EaseType ease = iTween.EaseType.easeInOutExpo;

        public bool isLical = false;
        [Range(0, 30)]
        public float delaytime = 0;
        [Range(0, 30)]
        public float time = 5;
        [Range(0, 30)]
        public float rottime = 5;
    }

}
