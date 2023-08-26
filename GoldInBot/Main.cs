using GoldInBot.Domain;
using GoldInBot.UseCase;

string user = Environment.GetEnvironmentVariable("linkedinUser") ?? "naoVeioNada";
string pass = Environment.GetEnvironmentVariable("linkedinPass") ?? "naoVeioNada";

Browser browser = new Browser();
LoginUseCase login = new LoginUseCase(browser);

await login.Login(user, pass);

Console.ReadLine();