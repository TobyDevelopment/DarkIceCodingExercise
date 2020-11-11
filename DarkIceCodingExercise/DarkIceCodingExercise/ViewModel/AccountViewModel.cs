using DarkIceCodingExercise.Model;
using FFImageLoading.Cache;
using FFImageLoading.Forms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace DarkIceCodingExercise.ViewModel
{
    public class AccountViewModel : INotifyPropertyChanged
    {
        private List<AccountGroup> accountlist { get; set; }
        public List<AccountGroup> AccountList
        {
            get { return accountlist; }
            set
            {
                if (value != accountlist)
                {
                    accountlist = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AccountViewModel()
        {
            GetDataAsync();
        }

        private async void GetDataAsync()
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://darkiceinteractive.com/skillstests/xamarinmobiledeveloper/accounts.json");

            if (response.IsSuccessStatusCode)
            {
                // Data to show
                var content = await response.Content.ReadAsStringAsync();
                var accounts = JsonConvert.DeserializeObject<List<Account>>(content);

                
                AccountGroup filteredMaleAccounts = new AccountGroup("Male");
                AccountGroup filteredFemaleAccounts = new AccountGroup("Female");
                for (int i = 0; i < accounts.Count; i++)
                {
                    int year = int.Parse(accounts[i].registered.Split(' ')[3]); // get registered year and convert to int

                    if (accounts[i].isActive && year >= 2016) // filter active accounts, accounts registered after 2016
                    {
                        await CachedImage.InvalidateCache(accounts[i].picture, CacheType.All, true); // clears cached image so it is updated everytime the app loads
                        accounts[i].fullName = accounts[i].name.first + " " + accounts[i].name.last; // concat first + last name ready to display
                        accounts[i].color = accounts[i].eyeColor.Split(' ')[0]; // gets colour for name label

                        if (accounts[i].gender == "male")
                            filteredMaleAccounts.Add(accounts[i]); //add to male list
                        else
                            filteredFemaleAccounts.Add(accounts[i]); //add to female list
                    }
                }

                // Sort by age in descending order, then by account balance in ascending order
                filteredMaleAccounts.Sort((x, y) => {
                    var ret = y.age.CompareTo(x.age);
                    if (ret == 0) ret = x.accountBalance.CompareTo(y.accountBalance);
                    return ret;
                });
                filteredFemaleAccounts.Sort((x, y) => {
                    var ret = y.age.CompareTo(x.age);
                    if (ret == 0) ret = x.accountBalance.CompareTo(y.accountBalance);
                    return ret;
                });

                // Group by gender
                List<AccountGroup> sortedAccounts = new List<AccountGroup>();
                sortedAccounts.Add(filteredMaleAccounts);
                sortedAccounts.Add(filteredFemaleAccounts);

                // Final List
                AccountList = new List<AccountGroup>(sortedAccounts);
            }

        }
    }
}
