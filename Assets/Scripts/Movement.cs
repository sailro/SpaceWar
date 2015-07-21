using UnityEngine;

public class Movement : MonoBehaviour
{
	public float rotationSpeed = 8;
	public float moveSpeed = 1024;

	public string horizontalAxis = "Horizontal";
	public string verticalAxis = "Vertical";

	private float hAxis;
	private float vAxis;
	private static bool gameOver;

	public void Start()
	{
		/*var currentrb = GetComponent<Rigidbody2D>();
        var heading = transform.rotation * Vector3.right;
        currentrb.AddForce(heading * moveSpeed * 10);*/

		gameOver = false;
	}

	private void Update()
	{
		hAxis = Input.GetAxis(horizontalAxis);
		vAxis = Input.GetAxis(verticalAxis);

		if (vAxis < 0)
			vAxis = 0;

		var thrustersAudio = GetComponentInChildren<AudioSource>();
		if (thrustersAudio == null)
			return;

		if (vAxis <= 0)
			thrustersAudio.Stop();
		else if (vAxis > 0 && !thrustersAudio.isPlaying)
			thrustersAudio.Play();
	}

	public void FixedUpdate()
	{
		transform.Rotate(0, 0, hAxis*-rotationSpeed);
		var currentrb = GetComponent<Rigidbody2D>();

		var heading = transform.rotation*Vector3.right;
		currentrb.AddForce(heading*moveSpeed*vAxis);

		var thrusters = GetComponentInChildren<ParticleSystem>();
		thrusters.emissionRate = 1000*vAxis;
	}

	public void OnDestroy()
	{
		if (!gameOver)
		{
			gameOver = true;
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
