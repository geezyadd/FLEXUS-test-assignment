using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Features.MeshGenerationModule.Scripts
{
    public class RandomSphereMeshGenerator : MonoBehaviour
    {
        [SerializeField] private MeshFilter _meshFilter;

        private MeshGenerationConfiguration _configuration;
        private Mesh _mesh;
        private float _boundingRadius;

        public Mesh Mesh
        {
            get
            {
                EnsureGenerated();
                return _mesh;
            }
        }

        public float BoundingRadius
        {
            get
            {
                EnsureGenerated();
                return _boundingRadius;
            }
        }

        [Inject]
        private void InjectDependencies(MeshGenerationConfiguration configuration) => _configuration = configuration;

        private void Awake() =>
            EnsureGenerated();

        private void OnDestroy()
        {
            if (_mesh != null)
                Destroy(_mesh);
        }

        private void EnsureGenerated()
        {
            if (_mesh != null)
                return;

            _mesh = Generate();
            _meshFilter.sharedMesh = _mesh;
        }

        private Mesh Generate()
        {
            int latitude = _configuration.LatitudeSegments;
            int longitude = _configuration.LongitudeSegments;

            Vector3 northPole = RandomPoint(Vector3.up);
            Vector3 southPole = RandomPoint(Vector3.down);
            Vector3[,] rings = CreateRings(latitude, longitude);

            List<Vector3> vertices = new();

            for (int lon = 0; lon < longitude; lon++)
            {
                int next = (lon + 1) % longitude;

                AddTriangle(vertices, northPole, rings[0, lon], rings[0, next]);
                AddTriangle(vertices, southPole, rings[latitude - 2, next], rings[latitude - 2, lon]);

                for (int lat = 0; lat < latitude - 2; lat++)
                {
                    AddTriangle(vertices, rings[lat, lon], rings[lat + 1, lon], rings[lat + 1, next]);
                    AddTriangle(vertices, rings[lat, lon], rings[lat + 1, next], rings[lat, next]);
                }
            }

            return BuildMesh(vertices);
        }

        private Vector3[,] CreateRings(int latitude, int longitude)
        {
            Vector3[,] rings = new Vector3[latitude - 1, longitude];

            for (int lat = 1; lat < latitude; lat++)
            {
                float theta = Mathf.PI * lat / latitude;

                for (int lon = 0; lon < longitude; lon++)
                {
                    float phi = 2f * Mathf.PI * lon / longitude;
                    Vector3 direction = new(Mathf.Sin(theta) * Mathf.Cos(phi), Mathf.Cos(theta), Mathf.Sin(theta) * Mathf.Sin(phi));
                    rings[lat - 1, lon] = RandomPoint(direction);
                }
            }

            return rings;
        }

        private Vector3 RandomPoint(Vector3 direction)
        {
            float noise = _configuration.Noise;
            float distance = _configuration.Radius * Random.Range(1f - noise, 1f + noise);
            _boundingRadius = Mathf.Max(_boundingRadius, distance);
            return direction * distance;
        }

        private void AddTriangle(List<Vector3> vertices, Vector3 a, Vector3 b, Vector3 c)
        {
            Vector3 normal = Vector3.Cross(b - a, c - a);
            Vector3 center = (a + b + c) / 3f;

            vertices.Add(a);

            if (Vector3.Dot(normal, center) >= 0f)
            {
                vertices.Add(b);
                vertices.Add(c);
            }
            else
            {
                vertices.Add(c);
                vertices.Add(b);
            }
        }

        private Mesh BuildMesh(List<Vector3> vertices)
        {
            int[] triangles = new int[vertices.Count];

            for (int i = 0; i < triangles.Length; i++)
                triangles[i] = i;

            Mesh mesh = new() { name = "RandomSphereMesh" };
            mesh.SetVertices(vertices);
            mesh.SetTriangles(triangles, 0);
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            return mesh;
        }
    }
}
