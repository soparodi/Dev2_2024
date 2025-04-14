namespace _04_webapp_sqlite.Prodotti;
public class AggiungiProdottoModel : PageModel
{
    private readonly ILogger<AggiungiProdottoModel> _logger;

    [BindProperty]
    public Prodotto Prodotto { get; set; } 



    [BindProperty]
    public List<SelectListItem>? CategorieTendina { get; set; } = new List<SelectListItem>();
    public List<SelectListItem> FornitoriSelectList { get; set; } = new List<SelectListItem>();
    public AggiungiProdottoModel()
    {
        Prodotto = new Prodotto();
        //CaricaCategorie();
         try
        {
            CategorieTendina = UtilityDB.ExecuteReader("SELECT * FROM Categorie", reader => new SelectListItem
            {
                Value = reader.GetInt32(0).ToString(),
                Text = reader.GetString(1)
            });

            FornitoriSelectList = UtilityDB.ExecuteReader("SELECT * FROM Fornitori", reader => new SelectListItem
            {
                Value = reader.GetInt32(0).ToString(),
                Text = reader.GetString(1)
            });
        }
        catch (Exception ex)
        {
            SimpleLogger.Log(ex);
        }
    }

    public void OnGet()
    {
        try
        {
            CategorieTendina = UtilityDB.ExecuteReader("SELECT * FROM Categorie", reader => new SelectListItem
            {
                Value = reader.GetInt32(0).ToString(),
                Text = reader.GetString(1)
            });
            FornitoriSelectList = UtilityDB.ExecuteReader("SELECT * FROM Fornitori", reader => new SelectListItem
                {
                    Value = reader.GetInt32(0).ToString(),
                    Text = reader.GetString(1)
                });
        }
        catch (Exception ex)
        {
            SimpleLogger.Log(ex);
        }
    }

    public IActionResult OnPost()
    {
        //Console.WriteLine($"Valore ricevuto: {prezzoconvert}");

        

        if (!ModelState.IsValid)
        {
            try
            {
                CategorieTendina = UtilityDB.ExecuteReader("SELECT * FROM Categorie", reader => new SelectListItem
                {
                    Value = reader.GetInt32(0).ToString(),
                    Text = reader.GetString(1)
                });
                FornitoriSelectList = UtilityDB.ExecuteReader("SELECT * FROM Fornitori", reader => new SelectListItem
                {
                    Value = reader.GetInt32(0).ToString(),
                    Text = reader.GetString(1)
                });
            }
            catch (Exception ex)
            {
                SimpleLogger.Log(ex);
            }

            return Page();
        }

        var sql = @"INSERT INTO Prodotti (Nome, Prezzo, CategoriaId, FornitoreId) VALUES (@nome, @prezzo, @categoria, @fornitore)";

        try
        {
            UtilityDB.ExecuteNonQuery(sql, command =>
            {
                command.Parameters.AddWithValue("@nome", Prodotto.Nome);
                command.Parameters.AddWithValue("@prezzo", Prodotto.Prezzo);
                command.Parameters.AddWithValue("@categoria", Prodotto.CategoriaId);
                command.Parameters.AddWithValue("@fornitore", Prodotto.FornitoreId);
            });
        }
        catch (Exception ex)
        {
            SimpleLogger.Log(ex);
        }
        return RedirectToPage("Index");
    }
}