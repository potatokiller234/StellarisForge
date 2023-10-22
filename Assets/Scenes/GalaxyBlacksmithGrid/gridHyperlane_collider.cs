using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridHyperlane_collider : MonoBehaviour
{
    public LineRenderer line;
    public void generateMeshCollider()
    {
        MeshCollider collider = this.gameObject.GetComponent<MeshCollider>();

        Mesh mesh = new Mesh();
        line.BakeMesh(mesh);
        collider.sharedMesh = mesh;
    }
}
