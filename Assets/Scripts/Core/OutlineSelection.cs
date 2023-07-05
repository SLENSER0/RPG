using UnityEngine;

namespace RPG.Core
{
	[RequireComponent(typeof(Outline))]
	public class OutlineSelection : MonoBehaviour
	{
		private RaycastHit _lastHit;
		void Update()
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
	    
	    
			if (Physics.Raycast(ray, out hit))
			{
				if (hit.collider.tag == "Enemy")
				{
					_lastHit = hit;
					hit.collider.GetComponent<Outline>().OutlineColor = Color.red;
					hit.collider.GetComponent<Outline>().enabled = true;
				}
				else
				{
					_lastHit.collider.GetComponent<Outline>().enabled = false;
				}

			}
		}
	}
}

