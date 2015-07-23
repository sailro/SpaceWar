using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
	public static Dictionary<string, int> DestroyCountPerTag = new Dictionary<string, int>();
	public Text ScoreText;

	public static void RegisterDeath(GameObject go)
	{
		if (go == null || go.tag == null || go.GetComponent<Movement>() == null)
			return;

		int current;
		if (!DestroyCountPerTag.TryGetValue(go.tag, out current))
			DestroyCountPerTag.Add(go.tag, 0);

		DestroyCountPerTag[go.tag]++;
	}

	void Update()
	{
		int ship1DestroyCount, ship2DestroyCount;

		DestroyCountPerTag.TryGetValue("Ship 1", out ship1DestroyCount);
		DestroyCountPerTag.TryGetValue("Ship 2", out ship2DestroyCount);

		ScoreText.text = string.Concat(ship2DestroyCount, "/", ship1DestroyCount);
	}
}
