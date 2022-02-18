using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Projectile reflection demonstration in Unity 3D
 * 
 * Demonstrates a projectile reflecting in 3D space a variable number of times.
 * Reflections are calculated using Raycast's and Vector3.Reflect
 * 
 * Developed on World of Zero: https://youtu.be/GttdLYKEJAM
 */
public class ProjectileReflectionEmitterUnityNative : MonoBehaviour
{
    public int maxReflectionCount = 5;
    public float maxStepDistance = 200;

    //[Header("Show raycast point In GameView")]
    //public GameObject prefab;
    //private GameObject[] obpos;

    //private void Start()
    //{
    //    obpos = new GameObject[20];
    //    for (int i=0; i<20; i++)        
    //        obpos[i] = Instantiate(prefab);
        
    //}

    void OnDrawGizmos()
    {
        //Handles.color = Color.red;
        //Handles.ArrowHandleCap(0, this.transform.position + this.transform.forward * 0.25f, this.transform.rotation, 0.5f, EventType.Repaint);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, 0.25f);
        
        DrawPredictedReflectionPattern(this.transform.position + this.transform.forward * 0.75f, this.transform.forward, maxReflectionCount);

        //for (int i = 0; i < 20; i++)
        //    obpos[i].transform.position = this.transform.position + this.transform.forward * 0.75f;
    }

    private void DrawPredictedReflectionPattern(Vector3 position, Vector3 direction, int reflectionsRemaining)
    {
        if (reflectionsRemaining == 0)        
            return;        

        Vector3 startingPosition = position;

        Ray ray = new Ray(position, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxStepDistance))
        {
            direction = Vector3.Reflect(direction, hit.normal);
            position = hit.point;

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(position, 0.25f);
        }
        else        
            position += direction * maxStepDistance;
        

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(startingPosition, position);

        DrawPredictedReflectionPattern(position, direction, reflectionsRemaining - 1);
    }
}