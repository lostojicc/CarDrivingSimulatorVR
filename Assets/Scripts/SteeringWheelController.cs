using UnityEngine;
using UnityEngine.Rendering;

public class SteeringWheelController : MonoBehaviour {
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	private Rigidbody rb;

	private float currentAngle;
	private float lastAngle;

	[SerializeField] private float spring = 5f;
	[SerializeField] private float damper = 50f;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    void Start() {
		lastAngle = GetAngle();
	}

	private float GetAngle() {
		float angle = transform.localEulerAngles.z;
		if (angle > 180f) angle -= 360f;
		return angle;
	}

	// Update is called once per frame
	void Update() {
		float angle = GetAngle();
		float delta = Mathf.DeltaAngle(lastAngle, angle);

		currentAngle += delta;
		currentAngle = Mathf.Clamp(currentAngle, -360f, 360f);
		lastAngle = angle;
	}

	private void FixedUpdate() {
		float springTorque = -currentAngle * spring;
		float damperTorque = -rb.angularVelocity.z * damper;

		rb.AddRelativeTorque(Vector3.forward * (springTorque + damperTorque), ForceMode.Acceleration);
		Debug.Log(currentAngle);
    }
}
