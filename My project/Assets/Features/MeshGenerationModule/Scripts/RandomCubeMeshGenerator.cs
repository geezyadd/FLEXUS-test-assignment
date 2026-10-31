using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Features.MeshGenerationModule.Scripts
{
    public class RandomCubeMeshGenerator : MonoBehaviour
    {
        private static readonly int[][] Faces =
        {
            new[] { 0, 2, 6, 4 },
            new[] { 1, 3, 7, 5 },
            new[] { 0, 1, 5, 4 },
            new[] { 2, 3, 7, 6 },
            new[] { 0, 1, 3, 2 },
            new[] { 4, 5, 7, 6 }
        };

        [SerializeField] private MeshFilter _meshFilter;

        private MeshGenerationConfiguration _configuration;
        private Mesh _mesh;
        private Vector3 _halfExtents;

        public Mesh Mesh
        {
            get
            {
                EnsureGenerated();
                return _mesh;
            }
        }

        public Vector3 HalfExtents
        {
            get
            {
                EnsureGenerated();
                return _halfExtents;
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
            Vector3[] corners = CreateCorners();
            List<Vector3> vertices = new();

            foreach (int[] face in Faces)
            {
                Vector3 a = corners[face[0]];
                Vector3 b = corners[face[1]];
                Vector3 c = corners[face[2]];
                Vector3 d = corners[face[3]];

                AddTriangle(vertices, a, b, c);
                AddTriangle(vertices, a, c, d);
            }

            return BuildMesh(vertices);
        }

        private Vector3[] CreateCorners()
        {
            float halfSize = _configuration.Size * 0.5f;
            Vector3[] corners = new Vector3[8];

            for (int i = 0; i < corners.Length; i++)
            {
                Vector3 direction = new((i & 1) == 0 ? -1f : 1f, (i & 2) == 0 ? -1f : 1f, (i & 4) == 0 ? -1f : 1f);
                Vector3 offset = Random.insideUnitSphere * (halfSize * _configuration.Noise);
                corners[i] = direction * halfSize + offset;
                _halfExtents = Vector3.Max(_halfExtents, Abs(corners[i]));
            }

            return corners;
        }

        private Vector3 Abs(Vector3 vector) =>
            new(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));

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

            Mesh mesh = new() { name = "RandomCubeMesh" };
            mesh.SetVertices(vertices);
            mesh.SetTriangles(triangles, 0);
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            return mesh;
        }
    }
}
