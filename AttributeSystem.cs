using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class AttributeSystem : MonoBehaviour {
	
	
	public Camera m_Camera;
	public Transform target; // the object the label should follow
	public Vector3 offset = Vector3.up;  // Units in world space to offset; 1 unit above object by default
	
	// make an array of attributes
	public bool hasHP = true;
	public bool hasMP = true;
	
	public List<AttributeBar> attributes;
	
	//public AttributeBar hp;
	//public AttributeBar mp;	
	
	// Use this for initialization
	void Start () {
		
		// assign the camera
		m_Camera = Camera.main;
		
		if ( hasHP ){
			attributes.Add( new AttributeBar(100f, 0f, 100f, "hp", Color.black, Color.green, target, m_Camera, attributes.Count) );
		}
		
		if ( hasMP ){
			attributes.Add( new AttributeBar(11f, 0f, 300f, "mp", Color.black, new Color(0f,0.7f,1.0f), target, m_Camera, attributes.Count) );
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
		//loop the list and update the attributes
		for (int i=0; i <= attributes.Count-1; i++){
			attributes[i].Update();
		}
		
	}
	
	// takes an attribute name
	// return array index
	// or return -1 if not found
	int getAttributeByName(string name){
		
		int aID = -1;
		for (int i=0; i <= attributes.Count-1; i++){
			if ( attributes[i].name == name ){
				aID = i;
			}
		}
		
		return aID;
		
	}
	
	
	
	void OnMouseOver() {
		
		int hpIndex = getAttributeByName("hp");
		int mpIndex = getAttributeByName("mp");
		
		if ( hpIndex != -1 ){
			if( Input.GetMouseButton(0) ) {
				attributes[hpIndex].current -= 1;
			}
		}
		
		if ( mpIndex != -1 ){
			if( Input.GetMouseButton(1) ) {
				attributes[mpIndex].current += 3;
			}
		}		
		
	}
	
	
}
