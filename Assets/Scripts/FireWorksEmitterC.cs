using UnityEngine;
using System.Collections;

public class FireWorksEmitterC : MonoBehaviour {
	public Transform shooter;
	public Transform fireworks;
	public float speed = 10;
	private int burst_counter = 50;


	// Use this for initialization
	void Start () {
		shooter.position = new Vector3(0, shooter.position.y, shooter.position.z);
		fireworks.GetComponent<ParticleEmitter>().emit = false;
	}
	
	// Update is called once per frame
	void Update () {
		burst_counter--;

		if (burst_counter > 0) {
			shooter.Translate(Vector3.forward * speed * Time.deltaTime);
			return;
		}

		if (burst_counter == 0)
		{
			fireworks.GetComponent<ParticleEmitter>().emit = true;
			fireworks.GetComponent<ParticleEmitter>().Emit();
			return;
		}
		
		shooter.position = new Vector3(0, 0, 0);
		burst_counter = 50;
		fireworks.GetComponent<ParticleEmitter>().emit = false;
	}
}
