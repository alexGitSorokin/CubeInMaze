using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    #region Fields
    [SerializeField] private Maze _mazePrefab;
	[SerializeField] private Player _playerPrefab;
	[SerializeField] private FinishObject _finishObjectPrefub;

	private Maze _mazeInstance;
	private Player _playerInstance;
	private FinishObject _finishObject;
    #endregion

    #region Methods
    private void OnEnable()
    {
		var input = FindObjectOfType<PlayerInput>();
		if (input != null)
		{
			input.ResturtButtonPressed += RestartGame;
			input.FinishGamePressed += FinishGame;
		}
	}

	private void OnFinishGame()
	{
		_finishObject.Hitted -= OnFinishGame;
		RestartGame();
	}

	private void Start()
	{
		StartCoroutine(BeginGame());
	}

	private IEnumerator BeginGame()
	{
		_mazeInstance = Instantiate(_mazePrefab) as Maze;
		yield return StartCoroutine(_mazeInstance.Generate());

		_playerInstance = Instantiate(_playerPrefab) as Player;
		_playerInstance.SetLocation(_mazeInstance.GetCell(_mazeInstance.RandomCoordinates));

		_finishObject = Instantiate(_finishObjectPrefub) as FinishObject;
		_finishObject.SetLocation(_mazeInstance.GetCell(_mazeInstance.RandomCoordinates));
		_finishObject.Hitted += OnFinishGame;
	}

    private void RestartGame()
	{
		StopAllCoroutines();
		Destroy(_mazeInstance.gameObject);
		if (_playerInstance != null)
			Destroy(_playerInstance.gameObject);
		if (_finishObject != null)
			Destroy(_finishObject.gameObject);
		StartCoroutine(BeginGame());
    }

	private void FinishGame() => Application.Quit();

    private void OnDisable()
    {
        var input = FindObjectOfType<PlayerInput>();
        if (input != null)
        {
			input.ResturtButtonPressed -= RestartGame;
			input.FinishGamePressed -= FinishGame;
		}

    }
    #endregion
}