using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;

[ExecuteInEditMode]
public class BoxGenerator : MonoBehaviour {

    public GameObject box;
    public Vector2Int RowCol;
    public float scale = 1;
    public float width = 1;
    public float depth = 1;
    float posx;
    float posy;

    [Button]
    public void Generate()
    {
        for (int i=0; i<=RowCol.x; i++)
        {
            for (int j=0; j<=RowCol.y; j++) {
                GameObject go = Instantiate(box);
                go.name =(j+i*10).ToString();
                go.transform.parent = transform;
                go.transform.localScale = Vector3.one * scale;
                go.transform.localPosition = new Vector3(posx, posy, depth);
                posx = j*width;
            }
            posy = i*width;
        }
    }

    [Button]
    public void Clear()
    {
        posx = 0;
        posy = 0;
        int childs = transform.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            GameObject.DestroyImmediate(transform.GetChild(i).gameObject);
        }

    }


}
