using UnityEngine;

public class PlayerMovementController : MonoBehaviour {
	
	[SerializeField] private float _speed;
	
	private bool _isMovementAlreadyStarted;
	private Rigidbody _rigidBody;

	public void Awake () {

		_rigidBody = GetComponent<Rigidbody>();
		if (_rigidBody == null)
		{
			throw new MissingReferenceException("Missing RigidBody component on the GameObject");
		}
		_isMovementAlreadyStarted = false;
	}

	public void FixedUpdate () {

		float x = Input.GetAxis("Horizontal");
		float y = 0f;
		float z = Input.GetAxis("Vertical");
		Vector3 force = new Vector3(x, y, z);
		bool isMoving = _rigidBody.IsSleeping();

		_rigidBody.velocity = force * _speed;
		if (isMoving && !_isMovementAlreadyStarted)
		{
			SendMessage("OnPlayerMovementStart");
		}
		else if (!isMoving && _isMovementAlreadyStarted)
		{
			SendMessage("OnPlayerMovementEnd");
		}
	}
}
