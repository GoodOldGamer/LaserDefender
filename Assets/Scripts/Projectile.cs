using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
	public float damage = 100f;
	
	public AudioClip hitSound;
	
	public float GetDamage()
	{
		return damage;
	}
	
	public virtual void Hit()
	{
		AudioSource.PlayClipAtPoint( hitSound, transform.position );
		Destroy( this.gameObject );
	}
}
