using UnityEngine;

public class Attractor : MonoBehaviour
{

	public float attraction = 40;

	public void FixedUpdate()
	{
		var items = FindObjectsOfType<Rigidbody2D>();
		foreach (var item in items)
		{
			var currentrb = GetComponent<Rigidbody2D>();

			var heading = currentrb.position - item.position;
			var distance = heading.magnitude;

			if (distance <= 0)
				continue;

			var direction = heading / distance;
			var intensity = (1 / Mathf.Pow(distance, 2)) * attraction;

			item.AddForce(direction * intensity);
		}
	}

}
