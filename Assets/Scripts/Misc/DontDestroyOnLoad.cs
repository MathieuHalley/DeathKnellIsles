using UnityEngine;

namespace Assets.Scripts
{
	public class DontDestroyOnLoad : MonoBehaviour
	{
		void Awake ()
		{
			DontDestroyOnLoad(gameObject);
		}
	}
}
