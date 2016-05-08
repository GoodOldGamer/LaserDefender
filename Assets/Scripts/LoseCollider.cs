using UnityEngine;
using System.Collections;

public class LoseCollider : MonoBehaviour 
{
	private LevelManager levelManager_;
	
	// Use this for initialization
	void Start() 
	{
		levelManager_ = GameObject.FindObjectOfType<LevelManager>();
	}
	
	void OnTriggerEnter2D( Collider2D trigger )
	{
		print( "Trigger" );
		if ( levelManager_.name == "Level_01" ) {
			levelManager_.LoadLevel( "Level_02" );
		}
		else {
			levelManager_.LoadLevel( "Lose" );
		}
	}
	
	void OnCollisionEnter2D( Collision2D collision )
	{
		print( "Collision" );
	}
}
