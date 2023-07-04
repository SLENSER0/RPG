using UnityEngine;

[RequireComponent(typeof(Outline))]
public class OutlineSelection : MonoBehaviour
{

    Ray ray;
    RaycastHit hit;
	
    void Update()
    {
	    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	    RaycastHit hit;

	    if (Physics.Raycast(ray, out hit))
	    {
		    GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Enemy");
		    foreach (GameObject obj in gameObjects)
		    {
			    obj.GetComponent<Outline>().enabled = false;
			    hit.collider.GetComponent<Outline>().OutlineColor = Color.red;
		    }

		    if (hit.collider.CompareTag("Enemy"))
		    {
			    hit.collider.GetComponent<Outline>().OutlineColor = Color.red;
			    hit.collider.GetComponent<Outline>().enabled = true;
		    }
	    }
    }
}
