using UnityEngine;
using System.Collections;

public class AsteroidSpawner : MonoBehaviour 
{
	public GameObject 	asteroidPrefab;

	public float		width = 10f;
	public float		height = 5f;
		
	// Update is called once per frame
	void Update() 
	{
		if ( !IsInvoking("SpawnNewAsteroid") ) {
			float spawnTime = Random.Range( 1f, 15f );
			Debug.Log( "Invoke spawning asteroid: " + spawnTime.ToString() + " seconds." );
			Invoke( "SpawnNewAsteroid", spawnTime );
		}
	}
	
	public void OnDrawGizmos()
	{
		Gizmos.DrawWireCube( this.transform.position, new Vector3(width,height) );
	}
	
	void SpawnNewAsteroid()
	{
		int currentSpawnPoint = Random.Range( 0, this.transform.childCount );
		SpawnAsteroid( this.transform.GetChild(currentSpawnPoint) );
		
		Debug.Log( "Spawn point: " + currentSpawnPoint.ToString() );
		Debug.Log( "Child count: " + this.transform.childCount.ToString() );
	}
	
	bool SpawnAsteroid( Transform spawnPoint )
	{
		Debug.Log( "Spawn asteroid." );
		
		if ( spawnPoint ) {
			float asteroidSpeed = Random.Range( 1f, 5f );
		
			GameObject asteroid = Instantiate( asteroidPrefab, spawnPoint.transform.position, Quaternion.identity ) as GameObject;
			asteroid.GetComponent<Rigidbody2D>().velocity = new Vector3( 0, -1 * asteroidSpeed, 0 );
			return true;
		}
		return false;
	}
}
