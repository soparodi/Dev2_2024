public class Cassa
{
    public int Id { get; set; } 
    public Dipendente Cassiere { get; set; } = null;
    public List<StoricoAcquisti> Acquisti { get; set; }
    public bool ScontrinoProcessato { get; set; } 
    public decimal Fatturato { get; set; } = 0 ;

 
}
