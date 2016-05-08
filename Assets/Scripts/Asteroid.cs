using UnityEngine;
using System.Collections;

public class Asteroid : Projectile 
{	
	public int scoreValue = 50;
	
	public AudioClip 	explosionSound;
	public GameObject 	explosionVisualPrefab;
	
	private ScoreKeeper scoreKeeper;
	
	void Start()
	{
		scoreKeeper = GameObject.Find( "Score" ).GetComponent<ScoreKeeper>();
	}
	
	public override void Hit()
	{
		Die( null );
	}
	
	void OnTriggerEnter2D( Collider2D trigger )
	{
		// Checks if the GameObject has a Projectile component (see Projectile script)
		Projectile p = trigger.gameObject.GetComponent<Projectile>();
		if ( p ) {
			Hit( p );
		}
	}
	
	void Hit( Projectile p )
	{
		//Debug.Log( "Hit by other projectile" );
		p.Hit(); // destroy the projectile
		Die( p );
	}
	
	void Die( Projectile p )
	{
		ShowExplosion();	
		Destroy( this.gameObject );
		
		if ( p && p.tag == "Player" ) {
			scoreKeeper.Score( scoreValue );
		}
	}
	
	void ShowExplosion()
	{
		AudioSource.PlayClipAtPoint( explosionSound, transform.position );
		GameObject showExplosion = Instantiate( explosionVisualPrefab, gameObject.transform.position, Quaternion.identity ) as GameObject;
		showExplosion.GetComponent<ParticleSystem>().startColor = gameObject.GetComponent<SpriteRenderer>().color;
	}
}
