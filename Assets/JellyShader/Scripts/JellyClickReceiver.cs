using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* totally frivolous script for aesthetic purposes only */
/* uses a shader to animate the cube */

public class JellyClickReceiver : MonoBehaviour {

    RaycastHit hit;
    Ray clickRay;

    Renderer modelRenderer;
    float controlTime = 0f;
    float wobbleTimer = 10.0f;

	// Use this for initialization
	void Start () {
        modelRenderer = GetComponent<MeshRenderer>();

        modelRenderer.material.SetVector("_ModelOrigin", transform.position);
        modelRenderer.material.SetVector("_ImpactOrigin", Vector3.up);
    }
	
	// Update is called once per frame
	void Update () {

        controlTime += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            clickRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(clickRay, out hit))
            {
                controlTime = 0;

                modelRenderer.material.SetVector("_ModelOrigin", transform.position);
                modelRenderer.material.SetVector("_ImpactOrigin", hit.point);
            }
        } else {

            if (controlTime > wobbleTimer)
            {
                controlTime = 0;

                modelRenderer.material.SetVector("_ModelOrigin", transform.position);
                modelRenderer.material.SetVector("_ImpactOrigin", Vector3.up);

            }


        }

        modelRenderer.material.SetFloat("_ControlTime", controlTime);
	}
}
