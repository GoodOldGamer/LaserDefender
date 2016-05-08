using UnityEngine;
using System.Collections;

public class EnemyBehaviour : SpaceShipBehaviour 
{
	public int scoreValue = 150;
	
	private ScoreKeeper scoreKeeper;
	
	protected override void Start()
	{
		base.Start();
		
		projectileDirection = -1;
		scoreKeeper = GameObject.Find( "Score" ).GetComponent<ScoreKeeper>();
	}
	
	protected override void Update() 
	{
		base.Update();
		
		float prob = Time.deltaTime * fireRate;
		if ( Random.value < prob ) {
			LaunchProjectiles();
		}
	}

	protected override void Die( Projectile p )
	{
		base.Die( p );
		
		if ( p.tag == "Player" ) {
			scoreKeeper.Score( scoreValue );
		}
	}
}
