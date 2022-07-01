using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Reflections : MonoBehaviour
{
	public GameObject doorToOpen;

	public int reflections;
	public float maxLength;

	private float remainingLength;
	private LineRenderer lineRenderer;
	private Ray ray;
	private RaycastHit hit;

	public bool yeet;

	private void Awake()
	{
		lineRenderer = GetComponent<LineRenderer>();
	}

	private void Update()
	{
		ray = new Ray(transform.position, transform.forward);

		lineRenderer.positionCount = 1;
		lineRenderer.SetPosition(0, transform.position);

		remainingLength = maxLength;

		for (int i = 0; i < reflections; i++)
		{
			if (Physics.Raycast(ray.origin, ray.direction, out hit, remainingLength))
			{
				lineRenderer.positionCount += 1;
				lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
				remainingLength -= Vector3.Distance(ray.origin, hit.point);

				ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));

				if (hit.collider.tag == "WinP1")
				{
					doorToOpen.tag = "Door";
				}
				else
				{
					doorToOpen.tag = "Default";
				}

				if (hit.collider.tag != "Mirror")
					break;				
			}
			else
			{
				yeet = false;
				lineRenderer.positionCount += 1;
				lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + ray.direction * remainingLength);
			}
		}
	}
}
