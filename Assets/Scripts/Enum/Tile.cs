public struct Tile
{
    public int zone;
    public bool isAccesible;

    public static implicit operator Tile(int value)
    {
        return new Tile() { zone = value, isAccesible = true };
    }
}
