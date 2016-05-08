using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour 
{
	public void LoadLevel( string name )
	{
		Debug.Log( "LoadLevel called for " + name );
		
		Application.LoadLevel( name );
	}
	
	public void LoadNextLevel()
	{
		Application.LoadLevel( Application.loadedLevel + 1 );
	}
	
	public void Quit()
	{
		Debug.Log( "Quit called" );
		
		Application.Quit();
	}
}
