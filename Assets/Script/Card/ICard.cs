public interface ICard
{
    int Cost { get; } // The energy cost for this card
    bool CanPlay(int currentEnergy); // Method to check if the card can be played
    void Play(); // Method to execute when the card is played

}
