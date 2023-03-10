using UnityEngine;

#region Enum
public enum MazeDirection {
	North,
	East,
	South,
	West
}
#endregion

public static class MazeDirections {

    #region Fields
    public const int Count = 4;
    #endregion

    #region Properties
    public static MazeDirection RandomValue {
		get {
			return (MazeDirection)Random.Range(0, Count);
		}
	}

    private static MazeDirection[] _opposites = {
		MazeDirection.South,
		MazeDirection.West,
		MazeDirection.North,
		MazeDirection.East
	};
    #endregion

    #region Methods
    public static MazeDirection GetOpposite (this MazeDirection direction) {
		return _opposites[(int)direction];
	}
	
	private static IntVector2[] vectors = {
		new IntVector2(0, 1),
		new IntVector2(1, 0),
		new IntVector2(0, -1),
		new IntVector2(-1, 0)
	};
	
	public static IntVector2 ToIntVector2 (this MazeDirection direction) {
		return vectors[(int)direction];
	}

	private static Quaternion[] rotations = {
		Quaternion.identity,
		Quaternion.Euler(0f, 90f, 0f),
		Quaternion.Euler(0f, 180f, 0f),
		Quaternion.Euler(0f, 270f, 0f)
	};
	
	public static Quaternion ToRotation (this MazeDirection direction) {
		return rotations[(int)direction];
	}

	public static MazeDirection GetNextClockwise(this MazeDirection direction)
	{
		return (MazeDirection)(((int)direction + 1) % Count);
	}

	public static MazeDirection GetNextCounterclockwise(this MazeDirection direction)
	{
		return (MazeDirection)(((int)direction + Count - 1) % Count);
	}
    #endregion
}