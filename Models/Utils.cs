namespace Youtube.Api.Models
{
  public static class Utils
  {
    public static string RandomId()
    {
      string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
      Random random = new Random();
      return new string(Enumerable.Repeat(chars, 10)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }
  }
}