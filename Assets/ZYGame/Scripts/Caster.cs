using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caster : MonoBehaviour 
{
    // Directly reference for quick testing
    public Receiver         receiver;
    public Light            light_source;

    private GameObject      _caster_shadow;
    private MeshCollider    _caster_shadow_collider;

    private Mesh            _caster_shadow_mesh;

	// Use this for initialization
	void Start () 
    {
        _caster_shadow = new GameObject();
        _caster_shadow_collider = _caster_shadow.AddComponent<MeshCollider>();

        var caster_mesh = GetComponent<MeshFilter>();
        var vertices = new Vector3[24];
        for (int i = 0; i < vertices.Length; ++i)
            vertices[i] = caster_mesh.mesh.vertices[i];
        _caster_shadow_mesh                 = new Mesh();
        _caster_shadow_mesh.vertices        = vertices;
        _caster_shadow_mesh.triangles       = GetComponent<MeshFilter>().mesh.triangles;
        _caster_shadow_collider.sharedMesh  = _caster_shadow_mesh;

    }
	
	// Update is called once per frame
	void Update () 
    {
        var light_transform = light_source.transform;
        var light_position  = light_transform.position;
        var caster_position = transform.position;
        var cast_direction  = (caster_position - light_position).normalized;
        var receiver_origin = receiver.transform.position;
        var receiver_normal = receiver.transform.up;

        // Set spawned caster shadow's origin
        _caster_shadow.transform.position = PlaneRayIntersection(receiver_origin, receiver_normal, caster_position, cast_direction);

        // Caster's mesh
        var caster_mesh = GetComponent<MeshFilter>().mesh;
        // Spawned caster shadow's vertices
        var new_vertices = new Vector3[24];
        for (int i = 0; i < caster_mesh.vertexCount; ++i)
        {
            // Vertex position
            var vertex_position         = transform.TransformPoint(caster_mesh.vertices[i]);
            // Project to receiver plane
            var vertex_proj             = PlaneRayIntersection(receiver_origin, receiver_normal, vertex_position, (vertex_position - light_position).normalized);
            // Compute local position of caster shadow
            var caster_shadow_vertex    = vertex_proj - _caster_shadow.transform.position;
            new_vertices[i]             = caster_shadow_vertex;
        }
        // Update mesh collider
        _caster_shadow_mesh.vertices = new_vertices;
        _caster_shadow_collider.sharedMesh = null;
        _caster_shadow_collider.sharedMesh = _caster_shadow_mesh;
    }

    private static Vector3 PlaneRayIntersection(Vector3 plane_origin, Vector3 plane_normal, Vector3 ray_origin, Vector3 ray_dir)
    {
        return ray_origin + Vector3.Dot(plane_origin - ray_origin, plane_normal) / Vector3.Dot(ray_dir, plane_normal) * ray_dir;
    }
}
