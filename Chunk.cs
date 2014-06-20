using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// one tile
public class Tile {
	
	public int tileWidth = 2;
	public int tileLength = 2;
	public Vector3 tilePosition;
	public Color tileColor;
	
	GameObject PlaneObject;
	
	public Tile(Vector3 pos, Color color, string name){
		
		tilePosition = pos;
		tileColor = color;
		
		PlaneObject = GameObject.CreatePrimitive(PrimitiveType.Cube);

		PlaneObject.name = name;
		PlaneObject.transform.position = tilePosition;
		PlaneObject.transform.localScale = new Vector3 (tileWidth, -0.25f, tileLength);
		PlaneObject.AddComponent<MeshCollider>();
		PlaneObject.renderer.material.color = tileColor;
		
	}
	
	public void dx(){
		GameObject.Destroy(PlaneObject);
		//Destroy(PlaneObject, 0); 
	}
	
}


public class Chunk : MonoBehaviour { 

	Vector3 chunkCenter;

	public int chunkWidth = 5;
	public int chunkLength = 5;
	
	int chunkMinX = 0;
	int chunkMaxX = 0;
	int chunkMinZ = 0;
	int chunkMaxZ = 0;

	public Tile a;

	public List<List<Tile>> tileList; 
	
	int count = 0;

	// Use this for initialization
	void Start () {
	
		chunkMaxX = chunkWidth - 1;
		chunkMaxZ = chunkLength - 1;
		Debug.Log("chunk MAX:("+chunkMaxX+",  MIN:"+chunkMinX+")");
	
	
		// draw the player green
		this.renderer.material.color = new Color(0.2f,1,0.2f);
	
		chunkCenter = new Vector3(0,0,0);
		
		Color color = new Color(1,1,1); // white
		a = new Tile(new Vector3(15,2,15), new Color(1,0,1), "alpha");
		
		tileList = new List<List<Tile>>();
		
		List<Tile> tempRow;
	
		for (int x=0; x < chunkWidth; x++){
			
			count = 0+x;
			tempRow = new List<Tile>();
				
			for (int y=0; y < chunkLength; y++){
			
				if ( count % 2 == 0 ) {
					color = new Color(1,1,1);
				} else {
					color = new Color(0,0,0);
				}
				
				tempRow.Add( new Tile(
					new Vector3(
						a.tileWidth*x - (float)(a.tileWidth*chunkWidth)/2 + (float)(a.tileWidth)/2,
						-0.25f,
						a.tileLength*y - (float)(a.tileLength*chunkLength)/2 + (float)(a.tileLength)/2
					),
					color,
					"Tile:("+x+","+y+")")
				);
				
				count++;
			
			}
			
			tileList.Add(tempRow);
		}
		
	}


	public void CreateRow(int y){
		
		Color color = new Color(1,1,1); // white
		
		for (int x=0; x < chunkWidth; x++){
			
			count = 0+x;
			
			if ( count % 2 == 0 ) {
				color = new Color(1,1,1);
			} else {
				color = new Color(0,0,0);
			}
			
			tileList[x].Insert(y, new Tile(
				new Vector3(
					a.tileWidth*x - (float)(a.tileWidth*chunkWidth)/2 + (float)(a.tileWidth)/2,
					-0.25f,
					a.tileLength*y - (float)(a.tileLength*chunkLength)/2 + (float)(a.tileLength)/2
				),
				color,
				"Tile:("+x+","+y+")")
			);
				
			count++;

		}
		
	}
	
	public List<Tile> CreateColum(int x){
	
		List<Tile> tempRow;
		
		Color color = new Color(1,1,1); // white
		count = 0 + x;
		tempRow = new List<Tile>();
			
		for (int y=0; y < chunkLength; y++){
	
			if ( count % 2 == 0 ) {
				color = new Color(1,0.8f,0.8f);
			} else {
				color = new Color(0.6f,0,0);
			}
			
			tempRow.Add( new Tile(
				new Vector3(
					a.tileWidth * x - (float)(a.tileWidth*chunkWidth)/2 + (float)(a.tileWidth)/2,
					-0.25f,
					a.tileLength * y - (float)(a.tileLength*chunkLength)/2 + (float)(a.tileLength)/2
				),
				color,
				"Tile:("+x+","+y+")")
			);
			
			count++;
		
		}
		
		return tempRow;
		
	}
	
		
	
	
	void removeRow(int N){
		
		foreach ( List<Tile> l in tileList ){
			l[N].dx();
			l.RemoveAt(N);
		}
		
	}
	
	void removeColum(int N){ 
		
		// destroy the objects in the row
		foreach ( Tile t in tileList[N] ){
			t.dx();
		}
		
		// remove the row from the list
		tileList.RemoveAt(N);
		
	}
	
	
	
	/*
	 *		 |	UP +Z
	 *		 |
	 *	_____|_____
	 *		 |
	 *		 |		RIGHT +X
	 */		 
	void MoveXPlus(){
		
		// destroy all tiles from left column
		// remove left col from list
		removeColum(0);
		
		// generate and add right col
		tileList.Add(CreateColum(chunkMaxX+1));
		
		chunkMaxX++;
		chunkMinX++;
		
		// reset the center
		chunkCenter.x += a.tileWidth;
		
		Debug.Log("chunk X MAX:("+chunkMaxX+",  MIN:"+chunkMinX+")");
		
	}
	
	void MoveXMinus(){

		Debug.Log("move x minus - center:("+chunkCenter.x+","+chunkCenter.y+")");
	
		// destroy all tiles from left column
		// remove left col from list
		removeColum(tileList.Count-1);
	
		// generate and add left colum
		tileList.Insert(0, CreateColum(chunkMinX-1));
	
		chunkMaxX--;
		chunkMinX--;
		
		// reset the center
		chunkCenter.x -= a.tileWidth;
	
		Debug.Log("chunk X MAX:("+chunkMaxX+",  MIN:"+chunkMinX+")");
	
	}

	void MoveZPlus(){
		
		// destroy all tiles from left column
		// remove left col from list
		removeRow(0);
		
		// generate and add top row
		CreateRow(chunkMaxZ+1);
		
		chunkMaxZ++;
		chunkMinZ++;
		
		// reset the center
		chunkCenter.z += a.tileLength;
		
		Debug.Log("chunk Z MAX:("+chunkMaxZ+",  MIN:"+chunkMinZ+")");
		
	}

	void MoveZMinus(){
		
		// destroy all tiles from left column
		// remove left col from list
		removeRow(tileList[0].Count-1);
		
		// generate and bottom row
		CreateRow(0);
		
		chunkMaxZ--;
		chunkMinZ--;
		
		// reset the center
		chunkCenter.z -= a.tileLength;
		
		Debug.Log("chunk Z MAX:("+chunkMaxZ+",  MIN:"+chunkMinZ+")");
		
	}


	
	  
	// Update is called once per frame
	void Update () {
		
		// x motion
		if ( this.transform.position.x > (chunkCenter.x + a.tileWidth/2) ){
			MoveXPlus();
		}
		if ( this.transform.position.x < (chunkCenter.x - a.tileWidth/2) ){
			MoveXMinus();
		}
		
		// z motion
		if ( this.transform.position.z > (chunkCenter.z + a.tileWidth/2) ){
			MoveZPlus();
		}
		if ( this.transform.position.z < (chunkCenter.z - a.tileWidth/2) ){
			MoveZMinus();
		}		
		
	}
	
	
    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(chunkCenter, 0.25f);
    }
	
	
	
}
