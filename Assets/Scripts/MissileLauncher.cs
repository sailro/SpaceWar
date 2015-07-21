using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
	public string fireButton = "Fire1";
	public GameObject missile;
	public float shootingRate = 0.25f;

	private bool fire;
	private float shootCooldown;

	public void Start()
	{
		shootCooldown = 0f;
	}

	private void Update()
	{
		fire = Input.GetButtonDown(fireButton);

		if (shootCooldown > 0)
			shootCooldown -= Time.deltaTime;
	}

	public void FixedUpdate()
	{
		var heading = transform.rotation * Vector3.right;

		if (fire && shootCooldown <= 0)
		{
			var currentrb = GetComponent<Rigidbody2D>();
			var magnitude = currentrb.velocity.magnitude;
			if (Mathf.Approximately(magnitude,0))
				return;

			shootCooldown = shootingRate;

			var newMissile = (GameObject)Instantiate(missile, transform.position, transform.rotation);
			var missilerb = newMissile.GetComponent<Rigidbody2D>();

			missilerb.AddForce(400000 * heading * Mathf.Sqrt(magnitude));

			newMissile.tag = tag;

			Destroy(newMissile, 5);
		}
	}
}
