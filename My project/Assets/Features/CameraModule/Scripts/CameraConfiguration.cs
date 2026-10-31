using System.Collections.Generic;
using Features.CameraModule.Scripts.Exceptions;
using UnityEngine;

namespace Features.CameraModule.Scripts
{
    [CreateAssetMenu(fileName = nameof(CameraConfiguration), menuName = "Camera/Camera Configuration")]
    public class CameraConfiguration : ScriptableObject
    {
        [SerializeField] private GameObject _mainCameraPrefab;
        [SerializeField] private GameCameraType _defaultCamera = GameCameraType.Turret;
        [SerializeField] private List<CameraEntry> _cameras = new();

        public GameObject MainCameraPrefab => _mainCameraPrefab;
        public GameCameraType DefaultCamera => _defaultCamera;

        public GameCameraBase GetPrefab(GameCameraType type)
        {
            foreach (CameraEntry entry in _cameras)
            {
                if (entry.Type == type)
                    return entry.Prefab;
            }

            throw new CameraNotConfiguredException(type);
        }
    }
}
