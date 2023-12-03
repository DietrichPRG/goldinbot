using GoldInBot.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldInBot.Domain.Components;
using GoldInBot.Domain.Helpers;
using GoldInBot.Domain.Pages;
using OpenQA.Selenium.Support.Extensions;

namespace GoldInBot.UseCase
{
    public class GetProfilesDataUseCase
    {
        private readonly Browser _browser;

        private readonly HomeUseCase _homeUseCase;

        public GetProfilesDataUseCase(Browser browser)
        {
            _browser = browser;
            _homeUseCase = new HomeUseCase(browser);
        }

        public async Task<IList<Profile>> GetProfilesBySearch(string search, ConnectionLevelFlag connectionLevel = ConnectionLevelFlag.None)
        {
            List<Profile> lst = new();

            try
            {
                await _homeUseCase.DoSearch(search);
                var resultPage = new ResultPage(_browser);
                await resultPage.ChangeToPeopleResult();
                await resultPage.SetConnectionLevel(connectionLevel);

                while (resultPage.NextButton!.Enabled)
                {
                    var peopleResultListItems = await resultPage.GetPeopleResultListItems();
                    foreach (var i in peopleResultListItems)
                    {
                        this._browser.driver.ExecuteJavaScript($"window.open('{i.ProfileUrl}', '_blank')");
                        this._browser.driver.SwitchTo().Window(this._browser.driver.WindowHandles.Last());
                        await Task.Delay(GlobalConsts.DefaultWaitTimeForLoadPageMs);

                        var profilePage = new ProfilePage(this._browser);

                        var profile = await profilePage.GetProfileData();
                        Profile.Print(profile);

                        lst.Add(profile);

                        this._browser.driver.Close();
                        this._browser.driver.SwitchTo().Window(this._browser.driver.WindowHandles.Last());
                    }

                    await resultPage.NextPageResult();
                }

                return lst;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return lst;
        }
    }
}
