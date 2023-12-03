using GoldInBot.Domain;
using GoldInBot.Domain.Pages;

namespace GoldInBot.UseCase
{
    public class ConnectWithPeopleUseCase
    {
        private readonly Browser _browser;

        private readonly HomeUseCase _homeUseCase;

        public ConnectWithPeopleUseCase(Browser browser)
        {
            _browser = browser;
            _homeUseCase = new HomeUseCase(browser);
        }

        public async Task ConnectWithPeople(string search)
        {
            await _homeUseCase.DoSearch(search);
            var resultPage = new ResultPage(_browser);
            await resultPage.ChangeToPeopleResult();

            while (resultPage.NextButton!.Enabled)
            {
                await resultPage.ConnectEveryBodyOnPageResult();
                await resultPage.NextPageResult();
            }

        }
    }
}
