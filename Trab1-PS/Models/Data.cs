namespace Trab1_PS.Models;

public class Data
{
    private int dia { set; get; }
    private int mes { set; get; }
    private int ano { set; get; }

    public Data(int dia, int mes, int ano)
    {
        this.dia = dia;
        this.mes = mes;
        this.ano = ano;
    }
}