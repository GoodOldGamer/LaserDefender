using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour 
{
	static MusicPlayer instance_ = null;
	
	public AudioClip startClip;
	public AudioClip gameClip;
	public AudioClip endClip;
	
	private AudioSource music;
	
	void Awake()
	{
		if ( instance_ != null ) {
			Destroy( gameObject );
			print( "Duplicate MusicPlayer object destroyed" );
		}
		else {
			instance_ = this;
			GameObject.DontDestroyOnLoad( gameObject );
			
			music = GetComponent<AudioSource>();
			music.loop = true;
			music.clip = startClip;
			music.Play();
			
		}
	}
	
	void OnLevelWasLoaded( int level )
	{
		Debug.Log( "Music Player loaded level " + level.ToString() );
		
		music.Stop();
		
		switch ( level ) {
		default: // falltru
		case 0: music.clip = startClip; break;
		case 1: music.clip = gameClip;  break;
		case 2: music.clip = endClip;   break;
		}
		
		music.Play();
	}
}
