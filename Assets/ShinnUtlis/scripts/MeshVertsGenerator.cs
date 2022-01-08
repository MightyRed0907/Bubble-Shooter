using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using EasyButtons;

namespace Shinn
{
    [RequireComponent(typeof(MeshFilter)), ExecuteInEditMode]
    public class MeshVertsGenerator: MonoBehaviour
    {
        public enum Quality
        {
            Low,
            Medium,
            High
        }
        public Quality quality;

        [Header("Gizmos stroke"), Range(0, 1)]
        public float stroke = .01f;

        public Vector3[] GetMeshVertices
        {
            get { return meshverts; }
        }

        private Vector3[] vertices;
        private Vector3[] meshverts;     
        
        private GameObject tempGroup;
        private GameObject[] tempbox;      
        
        private bool draw = false;
        private int count = 0;

        private int QualitySelect(Quality quality)
        {
            switch (quality)
            {
                case Quality.Low:
                    return vertices.Length / 3;
                case Quality.Medium:
                    return vertices.Length / 3 * 2;
                case Quality.High:
                    return vertices.Length;
                default:
                    return vertices.Length;
            }
        }
        
        void Awake()
        {
            MeshFilter mesh = GetComponent<MeshFilter>();
            vertices = mesh.sharedMesh.vertices;
            Debug.Log("Draw in Editor " + vertices.Length);
        }

        [Button]
        public void GenerateMesh()
        {
            count = QualitySelect(quality);

            tempGroup = new GameObject();
            tempGroup.name = "Group(Using the script to delete.)";
            tempGroup.transform.parent = transform;
            
            Instan();
        }

        [Button]
        public void Destory() {
            int childs = transform.childCount;
            for (int i = 0; i < childs; i++)
                DestroyImmediate(transform.GetChild(i).gameObject);

            draw = false;
        }

        [Button]
        public void RemoveCommpont()
        {
            draw = false;
            DestroyImmediate(GetComponent<MeshVertsGenerator>());
        }

        private void Instan()
        {
            meshverts = new Vector3[count];
            tempbox = new GameObject[count];

            for (int i = 0; i < count; i++)
            {
                tempbox[i] = new GameObject();
                tempbox[i].name = "node"+(i+1);
                tempbox[i].transform.localPosition = vertices[i];
                tempbox[i].transform.localEulerAngles = Vector3.zero;
                tempbox[i].transform.localScale = Vector3.one;
                tempbox[i].transform.parent = tempGroup.transform;
                meshverts[i] = vertices[i];
            }

            draw = true;

            tempGroup.transform.localPosition = Vector3.zero;
            tempGroup.transform.localEulerAngles = Vector3.zero;
            tempGroup.transform.localScale = Vector3.one;           
        }

        private void OnDrawGizmos()
        {
            if (draw)
                for (int i = 0; i < tempbox.Length; i++)
                {
                    Gizmos.DrawWireSphere(tempbox[i].transform.position, stroke);
                    Gizmos.color = Color.yellow;
                }
        }

    }

}
