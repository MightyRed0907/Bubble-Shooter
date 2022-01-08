using UnityEngine;

/// <summary>
/// Compute a skinned mesh's deformation.
///
/// The script must be attached aside a SkinnedMeshRenderer,
/// which is only used to get the bone list and the mesh
/// (it doesn't even need to be enabled).
///
/// Make sure the scripts accessing the results run after this one
/// (otherwise you'll have a 1-frame delay),
/// or use the OnResultsReady delegate.
/// </summary>
public class SkinnedMesh : MonoBehaviour
{
	internal Mesh mesh;
	internal SkinnedMeshRenderer skin;

	internal int vertexCount;
	internal Vector3[] vertices;
	internal Vector3[] normals;
	public System.Action<SkinnedMesh> OnResultsReady;

	public Vector3[] GetVertices{
		get { return vertices; }
	}
	public int GetVerticesLength{
		get { return vertexCount; }
	}

	void Start()
	{
		skin = GetComponent<SkinnedMeshRenderer>();
		mesh = skin.sharedMesh;

		vertexCount = mesh.vertexCount;
		vertices = new Vector3[vertexCount];
		normals = new Vector3[vertexCount];
	}

	void LateUpdate()
	{
		Matrix4x4[] boneMatrices = new Matrix4x4[skin.bones.Length];
		for (int i = 0; i < boneMatrices.Length; i++)
			boneMatrices[i] = skin.bones[i].localToWorldMatrix * mesh.bindposes[i];

		for (int i = 0; i < mesh.vertexCount; i++)
		{
			BoneWeight weight = mesh.boneWeights[i];

			Matrix4x4 bm0 = boneMatrices[weight.boneIndex0];
			Matrix4x4 bm1 = boneMatrices[weight.boneIndex1];
			Matrix4x4 bm2 = boneMatrices[weight.boneIndex2];
			Matrix4x4 bm3 = boneMatrices[weight.boneIndex3];

			Matrix4x4 vertexMatrix = new Matrix4x4();

			for (int n = 0; n < 16; n++)
			{
				vertexMatrix[n] =
					bm0[n] * weight.weight0 +
					bm1[n] * weight.weight1 +
					bm2[n] * weight.weight2 +
					bm3[n] * weight.weight3;
			}

			vertices[i] = vertexMatrix.MultiplyPoint3x4(mesh.vertices[i]);
			normals[i] = vertexMatrix.MultiplyVector(mesh.normals[i]);

		}

		if (OnResultsReady != null)
			OnResultsReady(this);



	}

	void OnDrawGizmos(){
		for (int i = 0; i < vertices.Length; i++) {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere (vertices [i], .01f);
		}
	}


}