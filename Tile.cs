using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class Tile : MonoBehaviour {
	
	public int tileWidth = 6;
	public int tileLength = 6;
	public Vector3 tilePosition;
	public Color tileColor;

	public GameObject stalagmite;
	public GameObject vent_01;
	public GameObject ambient_particles;
	
	GameObject PlaneObject;
	
	public Tile(Vector3 pos, Color color, string name){
		
		tilePosition = pos;
		tileColor = color;
		
		PlaneObject = GameObject.CreatePrimitive(PrimitiveType.Cube);

		PlaneObject.renderer.material.mainTexture = (Texture)Resources.Load("stone_dark_purple");
		
		// NORMAL MAP - not working??
		PlaneObject.renderer.material.SetTexture("_BumpMap", (Texture)Resources.Load("stone_dark_purple_normalmap"));
		
		PlaneObject.name = name;
		PlaneObject.transform.position = tilePosition;
		PlaneObject.transform.localScale = new Vector3 (tileWidth, -0.25f, tileLength);
		PlaneObject.AddComponent<MeshCollider>();
		PlaneObject.renderer.material.color = tileColor;
	
		randomize(pos);
	
		// generate tile particles
		ambient_particles = (GameObject)Resources.Load("Ambient_Particles");
		GameObject ap = (GameObject)Instantiate(ambient_particles, pos, Quaternion.identity);
		ap.transform.parent = this.PlaneObject.transform;
	
	}
	
	public void randomize(Vector3 pos){
		
		int rolls = UnityEngine.Random.Range(0,15);
		
		int r;
		
		// the rolls loop
		for ( int i=0; i<=rolls; i++){
			r = UnityEngine.Random.Range(0,1000);
			
			if ( r >= 750 ){
				// add a stalagmite
				stalagmite = (GameObject)Resources.Load("Stalagmite");
				GameObject mite = (GameObject)Instantiate(stalagmite, randomOffset(pos), Quaternion.identity);
				randomScale(mite);
				mite.transform.parent = this.PlaneObject.transform;
			}
			
			if ( r >= 999 ){
				// add a vent
				vent_01 =  (GameObject)Resources.Load("Vent_01");
				GameObject v01 = (GameObject)Instantiate(vent_01, randomOffset(pos), Quaternion.identity);
				randomScale(v01);
				v01.transform.parent = this.PlaneObject.transform;
				i = rolls+1;
			}
		}
	
	}
	
	public Vector3 randomOffset(Vector3 pos){
		
		int rX = UnityEngine.Random.Range(-5,5);
		int rY = UnityEngine.Random.Range(-1,1);
		int rZ = UnityEngine.Random.Range(-5,5);
		Vector3 tempPos = pos;
		tempPos.x += rX;
		tempPos.y += rY;
		tempPos.z += rZ;
		
		return tempPos;
		
	}
	
	public void randomScale(GameObject obj){
		
		int sX = UnityEngine.Random.Range(0,1);
		int sY = UnityEngine.Random.Range(0,5);
		int sZ = UnityEngine.Random.Range(0,2);

		obj.transform.localScale += new Vector3(sX, sY, sX);
		
	}	
	
	public void dx(){
		GameObject.Destroy(PlaneObject);
		//Destroy(PlaneObject, 0); 
	}
	
	void Start(){
		
	}
	
	void Update(){
		
	}
	
}
