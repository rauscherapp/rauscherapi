namespace Data.BancoCentral.Api.Model
{
  public class Value
  {
    public string cotacaoCompra { get; set; }
    public string cotacaoVenda { get; set; }
    public string dataHoraCotacao { get; set; }
    public string? tipoBoletim { get; set; }
  }
}
