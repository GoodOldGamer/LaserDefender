using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SpaceShipBehaviour : MonoBehaviour 
{
	public float health = 150;
	
	public GameObject laserPrefab;
	public float projectileSpeed = 1f;
	public float fireRate = 0.2f;

	public AudioClip laserSound;
	public AudioClip explosionSound;
	public GameObject explosionVisualPrefab;
	
	public GameObject shieldPrefab;
	public float shieldRegenTime = 5f;
	
	public GameObject fireVisualPrefab;
	
	protected float projectileDirection = 1;
	
	private GameObject shield;
	private Transform[] projectilePositions;
	
	// Use this for initialization
	protected virtual void Start() 
	{
		Invoke( "CreateShield", 0 );
		
		// Is there a better way?
		int posCount = 0;
		foreach( Transform child in this.transform ) {
			Position p = child.gameObject.GetComponent<Position>();
			if ( p ) {
				++posCount;
			}	
		}
		projectilePositions = new Transform[ posCount ] ;
		
		int i = 0;
		foreach( Transform child in this.transform ) {
			Position p = child.gameObject.GetComponent<Position>();
			if ( p ) {
				projectilePositions[i++] = child;
			}
		}
	}
	
	protected virtual void Update() 
	{
		if ( ! shield && !IsInvoking("CreateShield") ) {
			Invoke( "CreateShield", shieldRegenTime );
		}
	}
	
	protected virtual void OnTriggerEnter2D( Collider2D trigger )
	{
		// Checks if the GameObject has a Projectile component (see Projectile script)
		Projectile p = trigger.gameObject.GetComponent<Projectile>();
		if ( p ) {
			Hit( p );
		}
	}
	
	protected virtual void Hit( Projectile p )
	{
		CancelInvoke( "CreateShield" );
		
		health -= p.GetDamage();
		p.Hit(); // destroy the projectile
		
		if ( health <= 100 ) {
			ShowFire();
		}	
		
		if ( health <= 0 ) {
			Die( p );
		}	
	}
	
	protected virtual void ShowFire()
	{
		GameObject showFire = Instantiate( fireVisualPrefab, gameObject.transform.position, Quaternion.identity ) as GameObject;
		showFire.GetComponent<ParticleSystem>().startColor = gameObject.GetComponent<SpriteRenderer>().color;
		showFire.transform.parent = this.transform;
	}
	
	protected virtual void Die( Projectile p )
	{
		ShowExplosion();	
		Destroy( this.gameObject );
	}
	
	protected virtual void LaunchProjectiles()
	{
		foreach ( Transform pos in projectilePositions ) {
			if ( pos ) {
				LaunchProjectile( pos.position );
			}
		}
	}
	
	protected virtual void LaunchProjectile( Vector3 startPos )
	{
		GameObject beam = Instantiate( laserPrefab, startPos, Quaternion.identity ) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3( 0, projectileDirection * projectileSpeed, 0 );
		AudioSource.PlayClipAtPoint( laserSound, beam.transform.position );
	}
	
	void ShowExplosion()
	{
		AudioSource.PlayClipAtPoint( explosionSound, transform.position );
		GameObject showExplosion = Instantiate( explosionVisualPrefab, gameObject.transform.position, Quaternion.identity ) as GameObject;
		showExplosion.GetComponent<ParticleSystem>().startColor = gameObject.GetComponent<SpriteRenderer>().color;
	}
	
	void CreateShield()
	{
		//Debug.Log( "Create shield" );
		if ( shieldPrefab && ! shield ) {
			shield = Instantiate( shieldPrefab, this.transform.position, Quaternion.identity ) as GameObject;
			shield.transform.parent = this.transform;
		}
	}
}
