using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour 
{
	void OnTriggerEnter2D( Collider2D trigger )
	{
		Debug.Log( "Shield Hit" );
		
		// Checks if the GameObject has a Projectile component (see Projectile script)
		Projectile p = trigger.gameObject.GetComponent<Projectile>();
		if ( p ) {
			Hit( p );
		}
	}
	
	protected virtual void Hit( Projectile p )
	{
		p.Hit(); // destroy the projectile
		Destroy( this.gameObject );
	}
}
