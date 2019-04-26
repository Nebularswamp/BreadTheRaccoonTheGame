using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (EnemyFov))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        EnemyFov fow = (EnemyFov)target;
        Handles.color = Color.white;
        Handles.DrawWireDisc(fow.transform.position, Vector3.back, fow.viewRadius);

        Handles.color = Color.red;
        foreach(Transform visibleTarget in fow.visiblePlayer)
        {
            Handles.DrawLine(fow.transform.position, visibleTarget.position);
        }
    }

}
