using UnityEngine;

public class Attractor : MonoBehaviour
{

	public float attraction = 40;
	private Rigidbody2D m_cachedRigidbody;

        void Awake()
	{
	       m_cachedRigidbody = GetComponent<Rigidbody2D>();
	}
	
	public void FixedUpdate()
	{
		var items = FindObjectsOfType<Rigidbody2D>();
				
		foreach (var item in items)
		{
			var heading = m_cachedRigidbody.position - item.position;
			var distance = heading.magnitude;

			if (distance <= 0)
				continue;

			var direction = heading / distance;
			var intensity = (1 / Mathf.Pow(distance, 2)) * attraction;

			item.AddForce(direction * intensity);
		}
	}

}
