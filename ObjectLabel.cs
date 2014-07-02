using UnityEngine;
using System.Collections;

[RequireComponent (typeof (GUIText))]
public class ObjectLabel : MonoBehaviour {

	public string npcName;
	public Transform target; // the object the label should follow
	public Vector3 offset = Vector3.up;    // Units in world space to offset; 1 unit above object by default
	public bool useMainCamera = true;   // Use the camera tagged MainCamera
	public Camera cameraToUse;   // Only use this if useMainCamera is false
	Camera cam;
	
	Transform thisTransform;
	
	// Use this for initialization
	void Start () {
		
		guiText.text = npcName;
		
	    thisTransform = transform;
		
		//thisTransform.position = new Vector2(0,0);
		
   	 	if (useMainCamera)
       	 	cam = Camera.main;
    	else
        	cam = cameraToUse;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector2 temp = cam.WorldToViewportPoint( target.position + offset );
		
		
	    //thisTransform.position = cam.WorldToScreenPoint( target.position );
		//guiText.pixelOffset = cam.WorldToScreenPoint( target.position + offset );
		
		// check if camera is facing the target
		float angle = 60;
		
		if ( Vector3.Angle( cam.transform.forward, transform.position - cam.transform.position) < angle ){
			
			transform.position = temp;
			guiText.text = "WORKING";
			guiText.color = Color.green;
			
		} else {
			//transform.position = new Vector2(-1f, -1f);	
			transform.position = temp;
			guiText.text = "WOMP WOMP";
			guiText.color = Color.red;
		}
		
		Debug.Log(temp);
		
	}
	
	
}
