using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerControllerNew : MonoBehaviour {

	public float walkSpeed = 2f;
	public float runSpeed = 6f;
	public float speed = 2f;
	bool isRunning = false;
	
	public CharacterController charController;
	public Animator a;
	
	public AudioClip sfxWalk;
	public AudioClip sfxRun;
	public AudioClip[] sfxFemaleAttacks;  
	
	//private Animator anim;
	
	bool isIdle;
	
	
	// Use this for initialization
	void Start () {
		charController = GetComponent<CharacterController>();
		a = GetComponent<Animator>();
		isIdle = true;
	}

	
	// Update is called once per frame
	void Update () {
		
		if ( Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ){
			speed = runSpeed;
			isRunning = true;
		} else{
			speed = walkSpeed;
			isRunning = false;
		}
		
		if ( Input.GetAxis("Vertical") > 0 ){			// forward
			charController.Move(transform.forward * speed * Time.deltaTime);
		} else if ( Input.GetAxis("Vertical") < 0) { 	// back
			charController.Move(transform.forward * -speed*3/4 * Time.deltaTime);
		}
		
		if ( Input.GetAxis("Horizontal") > 0 ){			// right
			charController.Move(transform.right * speed * Time.deltaTime);
		} else if ( Input.GetAxis("Horizontal") < 0) {	// left
			charController.Move(transform.right * -speed * Time.deltaTime);
		}
		
		animate();
		
	}
	
	void animate(){
		
		AnimatorStateInfo ASI = a.GetCurrentAnimatorStateInfo(0); 
		
		if ( Input.GetKeyDown(KeyCode.Alpha1) ){ // if 1 pressed, attack
			
			isIdle = false;
			a.Play("attack", 0);
			
			// play the attack audio
			int attackNumber = Random.Range(0, sfxFemaleAttacks.Length-1);
			
			audio.clip = sfxFemaleAttacks[attackNumber];
			if ( !audio.isPlaying ){
				audio.Play();
			}
			
		} else if ( Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0 && !ASI.IsName("attack")  ){ // if moving & not attacking
			
			isIdle = false;
			
			if ( isRunning && !ASI.IsName("attack") ){
				
				a.Play("run", 0);
				
				// play the running audio
				audio.clip = sfxRun;
				if ( !audio.isPlaying ){a
					audio.Play();
				}
				
			} else {
				
				if ( ( Input.GetAxis("Vertical") > 0 || Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0 )  && !ASI.IsName("attack")  ) {
					a.Play("walk", 0);	
				} else if ( Input.GetAxis("Vertical") < 0 && !ASI.IsName("attack") ) { 
					a.Play("walkback", 0);	
				}
				
			}
			
			// play the walking audio
			if ( !audio.isPlaying ){
				audio.clip = sfxWalk;
				audio.Play();
			}

		} else if ( isIdle && !ASI.IsName("attack") ) {
			
			a.Play("idle", 0);	
			
		}
		
		if ( Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0 ) {
			isIdle = true;
		}

	}
	
	void returnToIdle(){
		isIdle = true;
	}
	
	
}
