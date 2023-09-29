using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Database.Engine.Info;

namespace FractalPlatform.Examples.Applications.SiteScanner
{
    public class SiteScannerApplication : BaseApplication
    {
        private Dictionary<string, string> _cache = new Dictionary<string, string>();

        private string GetFromCache(string url)
        {
            var client = new WebClient();

            string text;

            if (_cache.ContainsKey(url))
            {
                text = _cache[url];
            }
            else
            {
                text = client.DownloadString(url);

                _cache.Add(url, text);
            }

            return text;
        }

        private void Notificate(UserInfo userInfo,
                                string url,
                                string tag)
        {
            Client.SetDefaultCollection("Users")
                  .GetWhere("{'Name':@Name}", userInfo.Name)
                  .Update(@"{'TextMessages':[Add,{'Provider':'Telegram',
                                                  'Receiver':@Receiver,
                                                  'Message':@Message,
                                                  'IsSent':false}]}",
                             userInfo.TelegramUserID,
                             $"{tag} mentioned on {url}");
        }

        private void ScanPages(UserInfo userInfo)
        {
            var isFirst = !userInfo.Tags.SelectMany(x => x.Sites).Any();

            var text = GetFromCache("https://dou.ua/forums");

            var matches = Regex.Matches(text, "(?<Topic>/topic/[0-9]+)/\"");

            var pages = new List<string>();

            foreach (Match match in matches)
            {
                var url = "https://dou.ua/forums/" + match.Groups["Topic"].Value;

                text = GetFromCache(url);

                foreach (var tag in userInfo.Tags)
                {
                    var count = Regex.Matches(text, tag.Tag, RegexOptions.IgnoreCase).Count;

                    if (count > 0)
                    {
                        var site = tag.Sites.FirstOrDefault(x => x.URL == url);

                        if (site != null)
                        {
                            if (site.Count != count)
                            {
                                site.Count = count;
                                site.LastUpdate = DateTime.Now;

                                if (!isFirst)
                                {
                                    Notificate(userInfo, url, tag.Tag);
                                }
                            }
                        }
                        else
                        {
                            tag.Sites.Add(new SiteInfo
                            {
                                URL = page,
                                Count = count,
                                LastUpdate = DateTime.Now
                            });

                            if (!isFirst)
                            {
                                Notificate(userInfo, url, tag.Tag);
                            }
                        }
                    }
                }
            }
        }

        public override bool OnTimerDimension(TimerInfo timerInfo)
        {
            return false;

            var users = Client.SetDefaultCollection("Users")
                              .GetAll()
                              .Select<UserInfo>();

            foreach (var user in users)
            {
                _cache.Clear();

                ScanPages(user);

                Client.SetDefaultCollection("Users")
                      .GetWhere("{'Name':@Name}", user.Name)
                      .UpdateByObject(user);
            }

            return true;
        }

        public override bool OnEventDimension(EventInfo eventInfo)
        {
            switch (eventInfo.Action)
            {
                case "NewUser":
                    Client.SetDefaultCollection("NewUser")
                          .WantCreateNewDocumentFor("Users")
                          .OpenForm();
                    return true;
                case "Users":
                    Client.SetDefaultCollection("Users")
                          .GetAll()
                          .WantModifyExistingDocuments()
                          .OpenForm();
                    return true;
                default:
                    return base.OnEventDimension(eventInfo);
            }
        }

        public override void OnStart()
        {
            Client.SetDefaultCollection("Dashboard")
                  .GetFirstDoc()
                  .OpenForm();
        }
    }
}