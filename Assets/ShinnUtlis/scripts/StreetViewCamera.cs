using UnityEngine;

namespace Shinn
{
    public class StreetViewCamera : MonoBehaviour
    {
        [Range(.1f, 3)]
        public float speed = 3.5f;

        private float X;
        private float Y;

        private void FixedUpdate()
        {
            if (Input.GetMouseButton(0))
            {
                transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * speed, -Input.GetAxis("Mouse X") * speed, 0));
                X = transform.rotation.eulerAngles.x;
                Y = transform.rotation.eulerAngles.y;
                transform.rotation = Quaternion.Euler(X, Y, 0);
            }
        }
    }
}