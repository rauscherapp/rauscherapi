namespace Domain.Models
{
  /// <summary>
  /// Segredos dos parâmetros nunca saem da API: na leitura voltam mascarados e, na gravação,
  /// um valor mascarado, vazio ou nulo mantém o que já está salvo.
  /// </summary>
  public static class AppParameterSecrets
  {
    public const string Mask = "********";

    public static string MaskValue(string value) =>
      string.IsNullOrEmpty(value) ? value : Mask;

    public static string Resolve(string incoming, string current) =>
      string.IsNullOrEmpty(incoming) || incoming == Mask ? current : incoming;
  }
}
