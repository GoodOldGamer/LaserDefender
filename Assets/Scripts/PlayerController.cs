using UnityEngine;
using System.Collections;

public class PlayerController : SpaceShipBehaviour 
{
	public float speed;
	public float padding;
	
	float xmin;
	float xmax;
	
	protected override void Start()
	{
		base.Start();
		
		float distance = this.transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint( new Vector3(0,0,distance) );
		Vector3 rightmost = Camera.main.ViewportToWorldPoint( new Vector3(1,0,distance) );
		
		xmin = leftmost.x + padding;
		xmax = rightmost.x - padding;
		
		projectileDirection = 1;
	}

	protected override void Update() 
	{
		base.Update();
		
		float t = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
		this.transform.Translate( t, 0, 0 );
			
		float newX = Mathf.Clamp( transform.position.x, xmin, xmax );	
		this.transform.position = new Vector3( newX, this.transform.position.y, this.transform.position.z );
		
		if ( Input.GetKeyDown(KeyCode.Space) && !IsInvoking("Fire") ) {
			InvokeRepeating( "LaunchProjectiles", 0.000001f, fireRate );
		}
		if ( Input.GetKeyUp(KeyCode.Space) ) {
			CancelInvoke( "LaunchProjectiles" );
		}
	}
	
	protected override void Die( Projectile p )
	{
		base.Die( p );
		GameObject.Find( "LevelManager" ).GetComponent<LevelManager>().LoadLevel( "Lose" );
	}
}
