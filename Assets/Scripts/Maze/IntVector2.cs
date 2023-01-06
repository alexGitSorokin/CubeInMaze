[System.Serializable]
public struct IntVector2 {

    #region Fields
    public int x, z;
    #endregion

    #region Constructor
    public IntVector2 (int x, int z) {
		this.x = x;
		this.z = z;
	}
    #endregion

    #region Methods
    public static IntVector2 operator + (IntVector2 a, IntVector2 b) {
		a.x += b.x;
		a.z += b.z;
		return a;
	}
    #endregion
}