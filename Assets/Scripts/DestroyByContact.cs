using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
	public bool destroyable;
	public GameObject explosion;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (CompareTag(other.tag))
			return;

		var dbc = other.GetComponent<DestroyByContact>();

		if (dbc == null || dbc.destroyable)
		{
			var instance = Instantiate(explosion, other.transform.position, other.transform.rotation);
			Destroy(other.gameObject);
			Destroy(instance, 5);
		}

		if (destroyable)
			Destroy(gameObject);
	}

}
