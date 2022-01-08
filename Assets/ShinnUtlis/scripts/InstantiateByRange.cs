using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;

namespace Shinn
{
    public class InstantiateByRange : MonoBehaviour
    {
        public bool autostart = true;
        public GameObject[] prefabs;
        public Vector2 InstianteCount = new Vector2(5, 10);

        [Header("Instantiate Range")]
        [MinMax(-100, 100, ShowEditRange = true), Space]
        public Vector2 posxRange;
        [MinMax(-100, 100, ShowEditRange = true), Space]
        public Vector2 posyRange;
        [MinMax(-100, 100, ShowEditRange = true), Space]
        public Vector2 poszRange;

        [Header("Gizmos"), Range(0, 10)]
        public float size = 10;
        public GizmosState mytype;

        private bool MyType()
        {
            switch (mytype)
            {
                case GizmosState.Cube:
                    return true;
                case GizmosState.WireCube:
                    return false;
                default:
                    return false;
            }
        }
        public enum GizmosState
        {
            Cube,
            WireCube
        }

        private void Start()
        {
            if(autostart)
                Generate();
        }
        
        public void Generate()
        {
            if (prefabs.Length != 0)
            {
                int count = Random.Range((int)InstianteCount.x, (int)InstianteCount.y);
                for (int i = 0; i < count; i++)
                {
                    GameObject go = Instantiate(prefabs[Random.Range(0, prefabs.Length)]);
                    float posx = transform.position.x + Random.Range(posxRange.x, posxRange.y);
                    float posy = transform.position.y + Random.Range(posyRange.x, posyRange.y);
                    float posz = transform.position.z + Random.Range(poszRange.x, poszRange.y);
                    go.transform.localPosition = new Vector3(posx, posy, posz);
                    go.transform.parent = transform;
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (MyType())
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawCube(new Vector3(transform.position.x + posxRange.x, transform.position.y + posyRange.x, transform.position.z + poszRange.x), Vector3.one * size);
                Gizmos.DrawCube(new Vector3(transform.position.x + posxRange.y, transform.position.y + posyRange.x, transform.position.z + poszRange.x), Vector3.one * size);
                Gizmos.DrawCube(new Vector3(transform.position.x + posxRange.x, transform.position.y + posyRange.y, transform.position.z + poszRange.x), Vector3.one * size);
                Gizmos.DrawCube(new Vector3(transform.position.x + posxRange.y, transform.position.y + posyRange.y, transform.position.z + poszRange.x), Vector3.one * size);

                Gizmos.DrawCube(new Vector3(transform.position.x + posxRange.x, transform.position.y + posyRange.x, transform.position.z + poszRange.y), Vector3.one * size);
                Gizmos.DrawCube(new Vector3(transform.position.x + posxRange.y, transform.position.y + posyRange.x, transform.position.z + poszRange.y), Vector3.one * size);
                Gizmos.DrawCube(new Vector3(transform.position.x + posxRange.x, transform.position.y + posyRange.y, transform.position.z + poszRange.y), Vector3.one * size);
                Gizmos.DrawCube(new Vector3(transform.position.x + posxRange.y, transform.position.y + posyRange.y, transform.position.z + poszRange.y), Vector3.one * size);
            }

            else
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireCube(new Vector3(transform.position.x + posxRange.x, transform.position.y + posyRange.x, transform.position.z + poszRange.x), Vector3.one * size);
                Gizmos.DrawWireCube(new Vector3(transform.position.x + posxRange.y, transform.position.y + posyRange.x, transform.position.z + poszRange.x), Vector3.one * size);
                Gizmos.DrawWireCube(new Vector3(transform.position.x + posxRange.x, transform.position.y + posyRange.y, transform.position.z + poszRange.x), Vector3.one * size);
                Gizmos.DrawWireCube(new Vector3(transform.position.x + posxRange.y, transform.position.y + posyRange.y, transform.position.z + poszRange.x), Vector3.one * size);

                Gizmos.DrawWireCube(new Vector3(transform.position.x + posxRange.x, transform.position.y + posyRange.x, transform.position.z + poszRange.y), Vector3.one * size);
                Gizmos.DrawWireCube(new Vector3(transform.position.x + posxRange.y, transform.position.y + posyRange.x, transform.position.z + poszRange.y), Vector3.one * size);
                Gizmos.DrawWireCube(new Vector3(transform.position.x + posxRange.x, transform.position.y + posyRange.y, transform.position.z + poszRange.y), Vector3.one * size);
                Gizmos.DrawWireCube(new Vector3(transform.position.x + posxRange.y, transform.position.y + posyRange.y, transform.position.z + poszRange.y), Vector3.one * size);
            }
        }

        #region Gizmos Edit
        [ContextMenu("ResetAllPosition")]
        private void ResetAllPosition()
        {
            posxRange = Vector2.zero;
            posyRange = Vector2.zero;
            poszRange = Vector2.zero;
        }
        [ContextMenu("ResetXPosition")]
        private void ResetXPosition()
        {
            posxRange = Vector2.zero;
        }
        [ContextMenu("ResetYPosition")]
        private void ResetYPosition()
        {
            posyRange = Vector2.zero;
        }
        [ContextMenu("ResetZPosition")]
        private void ResetZPosition()
        {
            poszRange = Vector2.zero;
        }
        #endregion
    }
}
