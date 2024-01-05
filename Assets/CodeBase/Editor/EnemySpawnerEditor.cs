using CodeBase.Logic.EnemySpawners;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(SpawnMarker))]
    public class SpawnMarkerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void DrawCustomGizmo(SpawnMarker spawnPoint, GizmoType gizmoType)
        {
            Vector3 position = spawnPoint.transform.position;

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(position, 0.1f);
            Handles.Label(position + Vector3.up * 0.75f, spawnPoint.EnemyType.ToString());
        }
    }
}