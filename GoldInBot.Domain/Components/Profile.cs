using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldInBot.Domain.Components
{
    public class Profile
    {
        public string Name { get; set; }
        public string SubTitle { get; set; }
        public string Location { get; set; }
        public string About { get; set; }
        public IList<ProfileExperience> ProfileExperiences { get; set; }
        public IList<ProfileEducation> ProfileEducations { get; set; }
        public ProfileContactInfo ContactInfo { get; set; }
        public ProfileAbout ProfileAbout { get; set; }

        public static void Print(Profile profile)
        {
            Console.WriteLine(@$"
                Name: {profile.Name}
                SubTitle: {profile.SubTitle}
                Location: {profile.Location}
                About: {profile.About}
            ");

            Console.WriteLine("                Experiences: ");
            profile.ProfileExperiences.ToList().ForEach(x => Console.WriteLine($@"
                JobTitle: {x.Title}
                SubTitle: {x.SubTitle}
                Period: {x.Period}
                Skills: {String.Join(", ", x.Skills)}
                LinkedinHelpedGetJob: {(x.LinkedinHelpedGetJob ? "yes":"no")}
                Description: {x.Description}
            "));

            Console.WriteLine();
            Console.WriteLine("--------------------------------------");
            Console.WriteLine();
        }
    }
}
