using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
            var client = new HttpClient();

            string text;

            if (_cache.ContainsKey(url))
            {
                text = _cache[url];
            }
            else
            {
                text = client.GetStringAsync(url).Result;

                _cache.Add(url, text);
            }

            return text;
        }

        private void Notificate(UserInfo info,
                                string url,
                                string tag) =>
            DocsWhere("Users", "{'Name':@Name}", info.Name)
                  .Update(@"{'TextMessages':[Add,{'Provider':'Telegram',
                                                  'Receiver':@Receiver,
                                                  'Message':@Message,
                                                  'IsSent':false}]}",
                             info.TelegramUserID,
                             $"{tag} mentioned on {url}");

        private void ScanPages(UserInfo info)
        {
            var isFirst = !info.Tags.SelectMany(x => x.Sites).Any();

            var text = GetFromCache("https://dou.ua/forums");

            var matches = Regex.Matches(text, "(?<Topic>/topic/[0-9]+)/\"");

            var pages = new List<string>();

            foreach (Match match in matches)
            {
                var url = "https://dou.ua/forums" + match.Groups["Topic"].Value;

                text = GetFromCache(url);

                foreach (var tag in info.Tags)
                {
                    var matchTags = Regex.Matches(text,
                                                  "\\b" + tag.Tag,
                                                  RegexOptions.IgnoreCase);

                    if (matchTags.Count > 0)
                    {
                        var phrases = new List<string>();

                        foreach (Match matchTag in matchTags)
                        {
                            var phrase = text.Substring(matchTag.Index - 100, 200);

                            phrase = Regex.Match(phrase,
                                                 "[^><]{0,100}" + tag.Tag + "[^><]{0,100}",
                                                 RegexOptions.IgnoreCase).Value;

                            if (Regex.IsMatch(phrase, "[а-яА-Я]+"))
                            {
                                phrases.Add(phrase);
                            }
                        }

                        var site = tag.Sites.FirstOrDefault(x => x.URL == url);

                        if (site != null)
                        {
                            site.LastUpdate = DateTime.Now;

                            var newPhrases = phrases.Distinct()
                                                    .Except(site.Phrases)
                                                    .ToList();

                            if (newPhrases.Count > 0)
                            {
                                site.Phrases.AddRange(newPhrases);

                                if (!isFirst)
                                {
                                    Notificate(info, url, newPhrases[0]);
                                }
                            }
                        }
                        else
                        {
                            tag.Sites.Add(new SiteInfo
                            {
                                URL = url,
                                LastUpdate = DateTime.Now,
                                Phrases = phrases
                            });

                            if (!isFirst)
                            {
                                Notificate(info, url, phrases.First());
                            }
                        }
                    }
                }
            }
        }

        public override bool OnTimerDimension(TimerInfo info)
        {
            var users = DocsOf("Users").Select<UserInfo>();

            _cache.Clear();

            foreach (var user in users)
            {
                ScanPages(user);

                user.LastScanTime = DateTime.Now;

                DocsWhere("Users", "{'Name':@Name}", user.Name).UpdateByObject(user);
            }

            return true;
        }

        public override void OnStart() =>
            InputBox("Password", "Enter password", result =>
            {
                if (result.Result)
                {
                    if (result.Collection
                              .GetFirstDoc()
                              .IsEquals("{'Password':$}", "sc"))
                    {
                        DocsOf("Users").OpenForm();
                    }
                }
            });
    }
}