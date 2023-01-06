using UnityEngine;

public class MazeCell : MonoBehaviour
{
	#region Fields
	public IntVector2 _coordinates;

	private MazeCellEdge[] edges = new MazeCellEdge[MazeDirections.Count];

	private int _initializedEdgeCount;
	#endregion

	#region Properties
	public IntVector2 Coordinates
	{
		get { return _coordinates; }
		set { _coordinates = value; }
	}

	public bool IsFullyInitialized
	{
		get
		{
			return _initializedEdgeCount == MazeDirections.Count;
		}
	}

	public MazeDirection RandomUninitializedDirection
	{
		get
		{
			int skips = Random.Range(0, MazeDirections.Count - _initializedEdgeCount);
			for (int i = 0; i < MazeDirections.Count; i++)
			{
				if (edges[i] == null)
				{
					if (skips == 0)
					{
						return (MazeDirection)i;
					}
					skips -= 1;
				}
			}
			throw new System.InvalidOperationException("MazeCell has no uninitialized directions left");
		}
	}
    #endregion

    #region Methods
    public MazeCellEdge GetEdge(MazeDirection direction)
	{
		return edges[(int)direction];
	}

	public void SetEdge(MazeDirection direction, MazeCellEdge edge)
	{
		edges[(int)direction] = edge;
		_initializedEdgeCount += 1;
	}
    #endregion
}