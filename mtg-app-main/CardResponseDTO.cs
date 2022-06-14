using mtg_lib.Library.Models;
public class CardResponseDTO
{
    public List<Card> Cards {get; set; } = new List<Card>();

    public int Pages {get; set; }

    public int CurrentPage { get; set; }
}