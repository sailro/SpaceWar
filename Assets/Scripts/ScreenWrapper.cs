using UnityEngine;
using System.Collections.Generic;

public class ScreenWrapper : MonoBehaviour
{
	private Renderer[] renderers;
	private List<Transform> ghosts = new List<Transform>();

	private float screenWidth;
	private float screenHeight;

	private void Start()
	{
		renderers = GetComponentsInChildren<Renderer>();

		var cam = Camera.main;

		var screenBottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z));
		var screenTopRight = cam.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z));

		screenWidth = screenTopRight.x - screenBottomLeft.x;
		screenHeight = screenTopRight.y - screenBottomLeft.y;

		CreateGhosts();
	}

	private void Update()
	{
		var isVisible = false;
		foreach (var renderer in renderers)
		{
			if (renderer.isVisible)
			{
				isVisible = true;
				break;
			}
		}

		if (!isVisible)
			Swap();
	}

	private void CreateGhosts()
	{
		for (int i = 0; i < 8; i++)
		{
			var ghost = Instantiate(transform, Vector3.zero, Quaternion.identity) as Transform;
			ghosts.Add(ghost);

			DestroyImmediate(ghost.GetComponent<ScreenWrapper>());
			DestroyImmediate(ghost.GetComponent<ParticleSystem>());

			var dbc = ghost.GetComponent<DestroyByContact>();
			dbc.destroyable = false;
		}

		PositionGhosts();
	}

	private void PositionGhosts()
	{
		if (ghosts.Count <= 0)
			return;

		var ghostPosition = transform.position;
		var ghostIndex = 0;

		ghostPosition.x = transform.position.x + screenWidth;
		ghostPosition.y = transform.position.y;
		ghosts[ghostIndex++].position = ghostPosition;

		ghostPosition.x = transform.position.x + screenWidth;
		ghostPosition.y = transform.position.y - screenHeight;
		ghosts[ghostIndex++].position = ghostPosition;

		ghostPosition.x = transform.position.x;
		ghostPosition.y = transform.position.y - screenHeight;
		ghosts[ghostIndex++].position = ghostPosition;

		ghostPosition.x = transform.position.x - screenWidth;
		ghostPosition.y = transform.position.y - screenHeight;
		ghosts[ghostIndex++].position = ghostPosition;

		ghostPosition.x = transform.position.x - screenWidth;
		ghostPosition.y = transform.position.y;
		ghosts[ghostIndex++].position = ghostPosition;

		ghostPosition.x = transform.position.x - screenWidth;
		ghostPosition.y = transform.position.y + screenHeight;
		ghosts[ghostIndex++].position = ghostPosition;

		ghostPosition.x = transform.position.x;
		ghostPosition.y = transform.position.y + screenHeight;
		ghosts[ghostIndex++].position = ghostPosition;

		ghostPosition.x = transform.position.x + screenWidth;
		ghostPosition.y = transform.position.y + screenHeight;
		ghosts[ghostIndex++].position = ghostPosition;

		foreach (var ghost in ghosts)
			ghost.rotation = transform.rotation;
	}

	private void Swap()
	{
		foreach (var ghost in ghosts)
		{
			if (ghost.position.x < screenWidth && ghost.position.x > -screenWidth &&
				ghost.position.y < screenHeight && ghost.position.y > -screenHeight)
			{
				transform.position = ghost.position;
				break;
			}
		}

		PositionGhosts();
	}

	public void OnDestroy()
	{
		/*foreach (var ghost in ghosts)
            Destroy(ghost.gameObject);

        ghosts.Clear();*/
	}
}