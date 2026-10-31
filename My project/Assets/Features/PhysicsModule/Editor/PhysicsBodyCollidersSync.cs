using Features.PhysicsModule.Scripts;
using UnityEditor;
using UnityEngine;

namespace Features.PhysicsModule.Editor
{
    [InitializeOnLoad]
    public static class PhysicsBodyCollidersSync
    {
        static PhysicsBodyCollidersSync() =>
            ObjectChangeEvents.changesPublished += OnChangesPublished;

        private static void OnChangesPublished(ref ObjectChangeEventStream stream)
        {
            for (int i = 0; i < stream.length; i++)
            {
                if (stream.GetEventType(i) != ObjectChangeKind.ChangeGameObjectStructure)
                    continue;

                stream.GetChangeGameObjectStructureEvent(i, out ChangeGameObjectStructureEventArgs args);

                if (EditorUtility.InstanceIDToObject(args.instanceId) is GameObject gameObject &&
                    gameObject.TryGetComponent(out PhysicsBody body))
                    body.FindColliders();
            }
        }
    }
}
