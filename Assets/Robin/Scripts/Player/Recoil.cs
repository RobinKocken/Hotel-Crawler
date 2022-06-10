using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
	[Header("Recoil Points")]
	public Transform recoilPosition;
	public Transform rotationPoint;


	[Header("Speed Settings")]
	public float positionalRecoilSpeed = 8f;
	public float rotationalRecoilSpeed = 8f;


	public float positionalReturnSpeed = 18f;
	public float rotationalReturnSpeed = 38f;


	[Header("Amount Settings")]
	public Vector3 RecoilRotation = new Vector3(10, 5, 7);
	public Vector3 RecoilKickBack = new Vector3(0.015f, 0f, -0.2f);

	Vector3 rotationalRecoil;
	Vector3 positionalRecoil;
	Vector3 Rot;

	[Header("Particles")]
	public bool shooting;
	public GameObject particleHold;
	public GameObject muzzleFlash;
	public float playParticle;

	private void FixedUpdate()
	{
		rotationalRecoil = Vector3.Lerp(rotationalRecoil, Vector3.zero, rotationalReturnSpeed * Time.deltaTime);
		positionalRecoil = Vector3.Lerp(positionalRecoil, Vector3.zero, positionalReturnSpeed * Time.deltaTime);

		recoilPosition.localPosition = Vector3.Slerp(recoilPosition.localPosition, positionalRecoil, positionalRecoilSpeed * Time.deltaTime);
		Rot = Vector3.Slerp(Rot, rotationalRecoil, rotationalRecoilSpeed * Time.deltaTime);
		rotationPoint.localRotation = Quaternion.Euler(Rot);
	}

	void Update()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			GameObject newPar = Instantiate(muzzleFlash, particleHold.transform.position, Quaternion.identity);
			Destroy(newPar, playParticle);
			Fire();
		}
	}

	public void Fire()
	{
		rotationalRecoil += new Vector3(-RecoilRotation.x, Random.Range(-RecoilRotation.y, RecoilRotation.y), Random.Range(-RecoilRotation.z, RecoilRotation.z));
		rotationalRecoil += new Vector3(Random.Range(-RecoilKickBack.x, RecoilKickBack.x), Random.Range(-RecoilKickBack.y, RecoilKickBack.y), RecoilKickBack.z);
	} 
}
