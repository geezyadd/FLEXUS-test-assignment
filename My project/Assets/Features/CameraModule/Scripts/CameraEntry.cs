using System;
using UnityEngine;

namespace Features.CameraModule.Scripts
{
    [Serializable]
    public class CameraEntry
    {
        [SerializeField] private GameCameraType _type;
        [SerializeField] private GameCameraBase _prefab;

        public GameCameraType Type => _type;
        public GameCameraBase Prefab => _prefab;
    }
}
