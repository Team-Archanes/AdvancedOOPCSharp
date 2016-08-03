namespace Bejewled.Model.Interfaces
{
    public interface ITileGenerator
    {
        ITile CreateRandomTile(int row, int column);
    }
}