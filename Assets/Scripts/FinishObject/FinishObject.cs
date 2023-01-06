using System;
using UnityEngine;

public class FinishObject : MonoBehaviour
{
    #region Events
    public event Action Hitted;
    #endregion

    #region Methods   

    private void OnCollisionEnter(Collision collision)
    {
        var typeDefinitionComponent = collision.gameObject.GetComponent<Player>();
        if (typeDefinitionComponent != null)
            Hitted?.Invoke();
    }

    public void SetLocation(MazeCell cell) => transform.localPosition = cell.transform.localPosition + new Vector3(0, 5, 0); 
    #endregion
}
