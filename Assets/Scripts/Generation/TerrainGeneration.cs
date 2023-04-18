using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(EdgeCollider2D))]
public class TerrainGeneration : MonoBehaviour {
    [SerializeField] private GameObject player;

    [SerializeField] private int positionCount = 4;
	[SerializeField] private int downLength = 10;

	private void OnEnable() {
		var mesh = new Mesh {
			name = "Terrain"
		};
		Vector3[] terrain = generatePoints(positionCount);
		mesh.vertices = convertPointsToVertices(terrain);
		int[] triangles = new int[positionCount * 6 - 6];
		for (int i = 0; i < triangles.Length; i++) {
			triangles[i] = i;
		}
		mesh.triangles = triangles;
		GetComponent<MeshFilter>().mesh = mesh;

		updateCollider(terrain);
	}

	private void updateCollider(Vector3[] points) {
		EdgeCollider2D collider = GetComponent<EdgeCollider2D>();

		Vector2[] points2d = new Vector2[points.Length];
		for (int i = 0; i < points.Length; i++) {
			points2d[i] = points[i];
		}

		collider.points = points2d;
	}

	private Vector3[] generatePoints(int amount) {
		Vector3[] points = new Vector3[amount];
		// Generate the points which defin the terrain.
		for (int i = 0; i < amount; i++) {
			points[i] = new Vector3(i, Random.Range(-0.3f, 0.3f) - i * 0.6f, 0);
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
