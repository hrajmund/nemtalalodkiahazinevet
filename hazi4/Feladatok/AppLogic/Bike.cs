using System;

namespace MultiThreadedApp.AppLogic;

/// <summary>
/// Osztály bicikli objektumok reprezentálására.
/// </summary>
class Bike
{
    private volatile int position = 65;
    private volatile bool isWinner;

    /// <summary>
    /// Bicikli rajtszáma, readonly.
    /// </summary>
    public readonly int No;
    public bool IsWinner => isWinner;
    public int Position => position;

    /// <summary>
    /// Konstruktor, paraméter a rajtszám.
    /// </summary>
    public Bike(int no)
    {
        No = no;
    }

    public int Step()
    {
        int step = Random.Shared.Next(3, 9);
        // A += általánosságában nem szálbiztos, több író esetén (ha több szál is "+="-t hív ugyanarra a változóra).
        // De esetünkben egy szál hívja csak a Step függvényt, így a "+="-t is, egy másik szál pedig olvassa a változót,
        // Ha egy másik szál pont a "+=" végrehajtása közben olvassa a változó értékét, akkor az vagy az eredeti,
        // vagy már a növelt értéket adja vissza, de számunkra ez nem okoz gondot.
        position += step;
        return step;
    }

    public void SetAsWinner()
    {
        isWinner = true;
    }
}
