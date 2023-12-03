// todo
//-ajustar location do profile experience
//- obter os dados da educação do perfil
//- obter os dados do profile about




using GoldInBot.Domain;
using GoldInBot.Domain.Helpers;
using GoldInBot.UseCase;

string user = Environment.GetEnvironmentVariable("linkedinUser") ?? "naoVeioNada";
string pass = Environment.GetEnvironmentVariable("linkedinPass") ?? "naoVeioNada";

Browser browser = new();
LoginUseCase login = new LoginUseCase(browser);

await login.Login(user, pass);

//ConnectWithPeopleUseCase connect = new ConnectWithPeopleUseCase(browser);
//await connect.ConnectWithPeople("software and c#");

GetProfilesDataUseCase getProfiles = new GetProfilesDataUseCase(browser);
var profiles = await getProfiles.GetProfilesBySearch("software and c#", ConnectionLevelFlag.First);

while (Console.ReadLine() != "0")
{
    Console.WriteLine();
}


