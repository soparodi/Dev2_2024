using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

public class ProdottoDettaglioModel : PageModel
{
    private readonly ILogger<ProdottoDettaglioModel> _logger;

    public ProdottoDettaglioModel(ILogger<ProdottoDettaglioModel> logger)
    {
        _logger = logger;
    }
    public Prodotto Prodotto { get; set; }
    public void OnGet(int id)
    {
        string filePath = "wwwroot/json/prodotti.json";
        string json = System.IO.File.ReadAllText(filePath);
        var prodotti = JsonConvert.DeserializeObject<IEnumerable<Prodotto>>(json);

        foreach (var prod in prodotti)
        {
            if (prod.Id == id)
            {
                Prodotto = prod;
                _logger.LogInformation($"Prodotto {Prodotto.Nome} caricato correttamente.");
            }
        }
    }
}

// Creiamo una pagina di dettaglio che espone un singolo prodotto
// public Prodotto Prodotto è una proprietà del modello ProdottoDettaglioModel
// che utilizza il modello semplice Prodotto.cs della cartella Models 
// perché "Models" è una directory riconosciuta di DEFAULT dall'archetipo
// dunque è in grado di instanziare un nuovo oggetto nel metodo OnGet. 
// dove OnGet fornisce negli argomenti i dati per popolare gli attributi dell'oggetto Prodotto