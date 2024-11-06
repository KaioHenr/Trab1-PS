namespace Trab1_PS.Models;

public class Video
{
    private string nome { set; get; }
    private List<string> genero { set; get; }
    private int duracao { set; get; }
    private string sinopse { set; get; }
    private Data lancamento { set; get; }
    private List<string> elenco { set; get; }

    public Video()
    {
        //consulta no banco ou busca na api
    }

    public void avaliacao(int test)
    {
        Console.WriteLine(test);
    }
    
}