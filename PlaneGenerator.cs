using UnityEngine;
using System.Collections;

public class PlaneGenerator : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
		GameObject PlaneObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		PlaneObject.transform.position = new Vector3 (0,0,0);
		PlaneObject.transform.localScale = new Vector3 (50, 0.01f, 50);
		PlaneObject.AddComponent<MeshCollider>();
		PlaneObject.renderer.material.color = new Color(0.2f, 0.4f, 0.2f);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}