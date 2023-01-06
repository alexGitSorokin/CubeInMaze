using UnityEngine;

public abstract class MazeCellEdge : MonoBehaviour {

	#region Fields
	private MazeCell _cell, _otherCell;

	private MazeDirection _direction;
    #endregion

    #region Methods
    public void Initialize (MazeCell cell, MazeCell otherCell, MazeDirection direction) {
		_cell = cell;
		_otherCell = otherCell;
		_direction = direction;
		cell.SetEdge(direction, this);
		transform.parent = cell.transform;
		transform.localPosition = Vector3.zero;
		transform.localRotation = direction.ToRotation();
	}
    #endregion
}