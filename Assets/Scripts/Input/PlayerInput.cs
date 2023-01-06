using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    #region Events
    public event Action<Vector3> MoveButtonPressed;
    public event Action TurnLeftButtonPressed;
    public event Action TurnRightButtonPressed;
    public event Action ResturtButtonPressed;
    public event Action FinishGamePressed;
    #endregion

    #region Methods
    private void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            MoveButtonPressed?.Invoke(Vector3.forward);

        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            MoveButtonPressed?.Invoke(Vector3.back);

        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            TurnLeftButtonPressed?.Invoke();

        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            TurnRightButtonPressed?.Invoke();

        else if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.LeftArrow))
            ResturtButtonPressed?.Invoke();

        else if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.LeftArrow))
            FinishGamePressed?.Invoke();
    }
    #endregion
}
