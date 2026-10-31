using Features.PhysicsModule.Scripts.Collisions;
using UnityEditor;
using UnityEngine;

namespace Features.PhysicsModule.Editor
{
    public static class PhysicsColliderGizmoDrawer
    {
        private static readonly Color SelectedColor = new(1f, 0.25f, 0.25f, 1f);
        private static readonly Color NotSelectedColor = new(1f, 0.25f, 0.25f, 0.35f);

        [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
        private static void DrawSphere(SpherePhysicsCollider collider, GizmoType gizmoType)
        {
            Gizmos.color = GetColor(gizmoType);
            Gizmos.DrawWireSphere(collider.transform.position + collider.WorldCenterOffset, collider.WorldRadius);
        }

        [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
        private static void DrawBox(BoxPhysicsCollider collider, GizmoType gizmoType)
        {
            Transform transform = collider.transform;

            Gizmos.color = GetColor(gizmoType);
            Gizmos.matrix = Matrix4x4.TRS(transform.position + collider.WorldCenterOffset, transform.rotation, Vector3.one);
            Gizmos.DrawWireCube(Vector3.zero, collider.WorldHalfExtents * 2f);
            Gizmos.matrix = Matrix4x4.identity;
        }

        private static Color GetColor(GizmoType gizmoType) =>
            (gizmoType & GizmoType.Selected) != 0 ? SelectedColor : NotSelectedColor;
    }
}
