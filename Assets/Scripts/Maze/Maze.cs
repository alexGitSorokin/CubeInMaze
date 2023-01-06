using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze : MonoBehaviour {

    #region Fields
    [SerializeField] private IntVector2 _size;

	[SerializeField] private MazeCell _cellPrefab;

	[SerializeField] private float _generationStepDelay;

	[SerializeField] private MazePassage _passagePrefab;
	[SerializeField] private MazeWall _wallPrefab;

	private MazeCell[,] _cells;
    #endregion

    #region Properties
    public IntVector2 RandomCoordinates {
		get {
			return new IntVector2(Random.Range(0, _size.x), Random.Range(0, _size.z));
		}
	}
    #endregion

    #region Methods
    public bool ContainsCoordinates (IntVector2 coordinate) {
		return coordinate.x >= 0 && coordinate.x < _size.x && coordinate.z >= 0 && coordinate.z < _size.z;
	}

	public MazeCell GetCell (IntVector2 coordinates) {
		return _cells[coordinates.x, coordinates.z];
	}

	public IEnumerator Generate () {
		WaitForSeconds delay = new WaitForSeconds(_generationStepDelay);
		_cells = new MazeCell[_size.x, _size.z];
		List<MazeCell> activeCells = new List<MazeCell>();
		DoFirstGenerationStep(activeCells);
		while (activeCells.Count > 0) {
			yield return delay;
			DoNextGenerationStep(activeCells);
		}
	}

	private void DoFirstGenerationStep (List<MazeCell> activeCells) {
		activeCells.Add(CreateCell(RandomCoordinates));
	}

	private void DoNextGenerationStep (List<MazeCell> activeCells) {
		int currentIndex = activeCells.Count - 1;
		MazeCell currentCell = activeCells[currentIndex];
		if (currentCell.IsFullyInitialized) {
			activeCells.RemoveAt(currentIndex);
			return;
		}
		MazeDirection direction = currentCell.RandomUninitializedDirection;
		IntVector2 coordinates = currentCell.Coordinates + direction.ToIntVector2();
		if (ContainsCoordinates(coordinates)) {
			MazeCell neighbor = GetCell(coordinates);
			if (neighbor == null) {
				neighbor = CreateCell(coordinates);
				CreatePassage(currentCell, neighbor, direction);
				activeCells.Add(neighbor);
			}
			else {
				CreateWall(currentCell, neighbor, direction);
			}
		}
		else {
			CreateWall(currentCell, null, direction);
		}
	}
	private MazeCell CreateCell (IntVector2 coordinates) {
		MazeCell newCell = Instantiate(_cellPrefab) as MazeCell;
		_cells[coordinates.x, coordinates.z] = newCell;
		newCell.Coordinates = coordinates;
		newCell.transform.parent = transform;
		newCell.transform.localPosition = new Vector3(coordinates.x - _size.x * 0.5f + 0.5f, 0f, coordinates.z - _size.z * 0.5f + 0.5f);
		return newCell;
	}

	private void CreatePassage (MazeCell cell, MazeCell otherCell, MazeDirection direction) {
		MazePassage passage = Instantiate(_passagePrefab) as MazePassage;
		passage.Initialize(cell, otherCell, direction);
		passage = Instantiate(_passagePrefab) as MazePassage;
		passage.Initialize(otherCell, cell, direction.GetOpposite());
	}

	private void CreateWall (MazeCell cell, MazeCell otherCell, MazeDirection direction) {
		MazeWall wall = Instantiate(_wallPrefab) as MazeWall;
		wall.Initialize(cell, otherCell, direction);
		if (otherCell != null) {
			wall = Instantiate(_wallPrefab) as MazeWall;
			wall.Initialize(otherCell, cell, direction.GetOpposite());
		}
	}
    #endregion
}