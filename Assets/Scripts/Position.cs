using UnityEngine;
using System.Collections;

public class Position : MonoBehaviour 
{
	public float sphereRadius = 1f;

	void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere( this.transform.position, sphereRadius );
	}
}
