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
        Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle / 2, false);
        Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle / 2, false);

        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);

        Handles.color = Color.red;
        foreach(Transform visibleTarget in fow.visiblePlayer)
        {
            Handles.DrawLine(fow.transform.position, visibleTarget.position);
        }
    }

}
