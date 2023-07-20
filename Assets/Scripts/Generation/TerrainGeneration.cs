using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(EdgeCollider2D))]
public class TerrainGeneration : MonoBehaviour {
    [SerializeField] private GameObject player;

    [SerializeField] private int positionCount = 4;
	[SerializeField] private int downLength = 10;
	[SerializeField] private float stepSize = 0.25f;
	[SerializeField] private float slopePerUnit = 1;
	[SerializeField] private float smoothness = 0.1f;

	private List<Vector3> terrainList = new List<Vector3>();
	private Mesh mesh;
	private int globalLastIndex = 0;

	private void OnEnable() {
		mesh = new Mesh {
			name = "Terrain"
		};
		terrainList = generatePoints(positionCount);
		buildMesh();
	}

	private void buildMesh()
	{
        mesh.vertices = convertPointsToVertices(terrainList.ToArray());
        int[] triangles = new int[terrainList.Count * 6 - 6];
        for (int i = 0; i < triangles.Length; i++)
        {
            triangles[i] = i;
        }
        mesh.triangles = triangles;
        GetComponent<MeshFilter>().mesh = mesh;

        updateCollider(terrainList.ToArray());
    }

    public void Update()
    {
		if (player.transform.position.x + 10 > terrainList.Last().x)
		{
			terrainList.RemoveRange(0, 10);
			generatePoints(10);
			buildMesh();
		}
    }

    private void updateCollider(Vector3[] points) {
		EdgeCollider2D collider = GetComponent<EdgeCollider2D>();

		Vector2[] points2d = new Vector2[points.Length];
		for (int i = 0; i < points.Length; i++) {
			points2d[i] = points[i];
		}

		collider.points = points2d;
	}

	private List<Vector3> generatePoints(int amount) {
		List<Vector3> points = terrainList;
		// Generate the points which defin the terrain.
		for (int i = 0; i < amount; i++) {
			points.Add(new Vector3(globalLastIndex * stepSize, 
				Mathf.PerlinNoise1D(globalLastIndex * stepSize * smoothness) - (globalLastIndex * stepSize) * slopePerUnit, 0));
			globalLastIndex++;
		}

		return points;
	}

	private Vector3[] convertPointsToVertices(Vector3[] points) {
		// Two points together always need two triangles.
		// Generating the two triangles from the points.
		List<Vector3> meshPoints = new List<Vector3>();
		for (int i = 0; i < points.Length - 1; i++) {
			meshPoints.AddRange(generateTwoTrianglesFromPoints(points[i], points[i + 1]));
		}
		return meshPoints.ToArray();
	}

	private Vector3[] generateTwoTrianglesFromPoints(Vector3 pointOne, Vector3 pointTwo) {
		Vector3[] vectors = new Vector3[6];

		// Defining triangle one
		vectors[0] = pointOne;
		vectors[1] = pointTwo;
		vectors[2] = pointOne - Vector3.down * downLength;

		// Fill triangle two
		vectors[3] = pointTwo;
		vectors[4] = pointTwo - Vector3.down * downLength;
		vectors[5] = pointOne - Vector3.down * downLength;

		return vectors;
	}

}
