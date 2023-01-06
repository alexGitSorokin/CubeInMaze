using UnityEngine;

public class Player : MonoBehaviour
{
	#region Fields
	[SerializeField] private float _movementSpeed = 4f;
	[SerializeField] private float _rotationSpeed = 180f;
	#endregion

	#region Methods
	private void OnEnable()
	{
		var input = FindObjectOfType<PlayerInput>();
		if (input != null)
		{
			input.MoveButtonPressed += Move;
			input.TurnLeftButtonPressed += TurnLeft;
			input.TurnRightButtonPressed += TurnRight;
		}
	}

	public void SetLocation(MazeCell cell)
	{
		var currentPosition = cell.transform.localPosition;
		transform.localPosition = currentPosition + new Vector3(0, 5, 0);
	}

	private void Move(Vector3 direction)
	{
		transform.Translate(direction * _movementSpeed * Time.deltaTime);
	}

	private void TurnLeft()
	{
		transform.rotation *= Quaternion.Euler(0f, _rotationSpeed * Time.deltaTime, 0f);
	}

	private void TurnRight()
	{
		transform.rotation *= Quaternion.Euler(0f, -_rotationSpeed * Time.deltaTime, 0f);
	}

	private void OnDisable()
	{
		var input = FindObjectOfType<PlayerInput>();
		if (input != null)
		{
			input.MoveButtonPressed -= Move;
			input.TurnLeftButtonPressed -= TurnLeft;
			input.TurnRightButtonPressed -= TurnRight;
		}
	}
	#endregion
}