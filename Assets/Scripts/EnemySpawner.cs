using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour 
{
	public GameObject 	enemyType1Prefab;
	public GameObject 	enemyType2Prefab;
	public GameObject 	enemyType3Prefab;
	public GameObject 	enemyBossPrefab;
	
	public float		width = 10f;
	public float		height = 5f;
	
	public float 		speed = 1f;
	public float 		padding = 0.5f;
	public float		spawnDelay = 0.5f;
	
	private float 		xmin;
	private float 		xmax;
	
	private bool 		movingRight = true;
	
	private int			currentSpawnPoint = 0;
	private int			wave = 1;

	// Use this for initialization
	void Start () 
	{
		float distance = this.transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint( new Vector3(0,0,distance) );
		Vector3 rightmost = Camera.main.ViewportToWorldPoint( new Vector3(1,0,distance) );
		
		xmin = leftmost.x;
		xmax = rightmost.x;
		
		SpawnEnemyShipUntilFull();
	}
	
	public void OnDrawGizmos()
	{
		Gizmos.DrawWireCube( this.transform.position, new Vector3(width,height) );
	}
	
	// Update is called once per frame
	void Update () 
	{
		if ( movingRight ) {
			this.transform.position += Vector3.right * speed * Time.deltaTime;
		}
		else {
			this.transform.position -= Vector3.right * speed * Time.deltaTime;
		}
		
		float formationLeftEdge = this.transform.position.x - (0.5f*width);
		if ( formationLeftEdge <= xmin ) {
			movingRight = true;
		}
		
		float formationRightEdge = this.transform.position.x + (0.5f*width);
		if ( formationRightEdge >= xmax ) {
			movingRight = false;
		}
		
		if ( EnemyWaveIsDead() ) {
			++wave;
			if ( wave > 10 ) {
				Invoke( "AllEnemiesDead", 1.5f );
			}
			else {
				if ( ! IsInvoking("SpawnEnemyShipUntilFull") ) {
					Invoke( "SpawnEnemyShipUntilFull", 0 );
				}
			}
		}
	}
	
	void SpawnEnemyShipUntilFull()
	{	
		SpawnEnemy( this.transform.GetChild(currentSpawnPoint) );
		
		if ( UpdateCurrentSpawnPoint() ) {
			Invoke( "SpawnEnemyShipUntilFull", spawnDelay );
		}
	}
	
	bool EnemyWaveIsDead()
	{
		foreach( Transform child in this.transform ) {
			if ( child.childCount > 0 ) return false;
		}
		return true;
	}
	
	bool UpdateCurrentSpawnPoint()
	{
		++currentSpawnPoint;
		if ( currentSpawnPoint < this.transform.childCount ) {
			return true;
		}
		
		currentSpawnPoint = 0;
		return false;
	}
	
	GameObject GetEnemyPrefab()
	{
		if ( wave == 10 && currentSpawnPoint == 0 ) {
			return enemyBossPrefab;
		}
	
		int which = 1;
		if ( wave >= 3 && wave < 7 ) {
			which = Random.Range( 1, 3 );
		}
		if ( wave >= 7 && wave <= 10 ) {
			which = Random.Range( 2, 4 );
		}
			
		switch ( which ) {
		case 1: return enemyType1Prefab;
		case 2: return enemyType2Prefab;
		case 3: return enemyType3Prefab;
		}
		
		return null;
	}
	
	bool SpawnEnemy( Transform spawnPoint )
	{
		if ( spawnPoint ) {
			GameObject enemy = Instantiate( GetEnemyPrefab(), spawnPoint.transform.position, Quaternion.identity ) as GameObject;
			enemy.transform.parent = spawnPoint;
			return true;
		}
		return false;
	}
	
	void AllEnemiesDead()
	{
		GameObject.Find( "LevelManager" ).GetComponent<LevelManager>().LoadLevel( "Win" );
	}
}
