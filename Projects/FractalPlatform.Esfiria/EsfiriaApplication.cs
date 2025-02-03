using System.Collections.Generic;
using System;
using System.Linq;
using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Client.UI.DOM;
using FractalPlatform.Common.Enums;
using FractalPlatform.Database.Engine;
using FractalPlatform.Database.Engine.Info;

namespace FractalPlatform.Esfiria
{
    public class EsfiriaApplication : BaseApplication
    {
        private class Period
        {
            public DateTime FromDate { get; set; }

            public DateTime ToDate { get; set; }
        }

        private class Tour
        {
            public string Code { get; set; }

            public string Title { get; set; }

            public string Type { get; set; }

            public string About { get; set; }

            public string Duration { get; set; }

            public string FromCity { get; set; }

            public List<Period> Periods { get; set; }
        }

        public override bool OnEventDimension(EventInfo info)
        {
            switch (info.Action)
            {
                case "Tours":
                    ModifyDocsOf("Tours").OpenForm();
                    return true;
                case "About":
                    NotImplementedMessageBox();
                    return true;
                case "Contacts":
                    NotImplementedMessageBox();
                    return true;
                case "Feedback":
                    NotImplementedMessageBox();
                    return true;
                case "Requests":
                    DocsOf("Requests").OpenForm();
                    return true;
                case "Booked":
                    DocsOf("Booked").OpenForm();
                    return true;
                case "BookTour":
                    {
                        var tours = DocsOf("Tours").Values("{'Title':$}");

                        new
                        {
                            Title = tours.First(),
                        }
                        .ToCollection("Choose a tour")
                        .SetDimension(DimensionType.Enum, DQL("{'Title':{'Items':[@Items]}}", tours))
                        .SetUIDimension("{'Style':'Save:Choose'}")
                        .OpenForm(result =>
                        {
                            if (result.Result)
                            {
                                var title = result.FindFirstValue("Title");

                                var tour = DocsWhere("Tours", "{'Title':@Title}", title)
                                                .SelectOne<Tour>();

                                var periods = tour.Periods.Select(x => $"[{x.FromDate}] - [{x.ToDate}]");

                                new
                                {
                                    Title = title,
                                    Period = periods.First(),
                                }
                                .ToCollection("Choose a period")
                                .SetDimension(DimensionType.Enum, DQL("{'Period':{'Items':[@Items]}}", periods))
                                .SetUIDimension("{'Style':'Save:Book'}")
                                .OpenForm(result =>
                                {
                                    if (result.Result)
                                    {
                                        var period = result.FindFirstValue("Period");

                                        var name = info.FindFirstValue("Name");
                                        var phone = info.FindFirstValue("Phone");
                                        var email = info.FindFirstValue("Email");
                                        var description = info.FindFirstValue("Description");

                                        var book = new
                                        {
                                            OnDate = DateTime.Now,
                                            Name = name,
                                            Phone = phone,
                                            Email = email,
                                            Description = description,
                                            Tour = new
                                            {
                                                Code = tour.Code,
                                                Title = title,
                                                Period = period,
                                            }
                                        };

                                        AddDoc("Booked", book);

                                        ModifyDocsWhere("Requests", info.AttrPath)
                                            .Update("{'IsBooked':true}");

                                        CloseIfOpenedForm("Requests");
                                    }
                                });
                            }
                        });

                        return true;
                    }
                case "ScheduleByMonth":
                    {
                        var tours = DocsOf("Tours").Select<Tour>();

                        new
                        {
                            Months =
                                Enumerable.Range(1, 12)
                                .Select(month => new
                                {
                                    Month = new DateTime(2025, month, 1).ToString("MMMM"),
                                    Tours = tours.Where(x => x.Periods.Any(y => y.FromDate > DateTime.Now &&
                                                                                y.FromDate.Month == month))
                                                 .ToList()
                                })
                        }
                        .ToCollection("Months")
                        .OpenForm();

                        return true;
                    }
                case "OneDayTours":
                    DocsWhere("Tours", "{'Duration':'OneDay'}")
                        .OpenForm();
                    return true;
                case "ToursWithNights":
                    DocsWhere("Tours", "{'Duration':'WithNights'}")
                        .OpenForm();
                    return true;
                case "AllTours":
                    DocsOf("Tours")
                        .OpenForm();
                    return true;
                case "UserAdmin":
                    ModifyDocsOf("Users")
                        .OpenForm();
                    return true;
                case "Send":
                    CreateNewDocFor("NewRequest", "Requests")
                        .OpenForm(result =>
                        {
                            if (result.Result)
                            {
                                var receiver = DocsWhere("Users", "{'Name':'Admin'}")
                                                .Value("{'Telegram':$}");

                                var description = result.FindFirstValue("Description");

                                DocsWhere("Users", "{'Name':'Admin'}")
                                .Update(@"{'TextMessages':[Add,
                                            {'Provider':'Telegram',
                                             'Receiver':@Receiver,
                                             'Message':@Message,
                                             'IsSent':false}]}",
                                        receiver,
                                        $"You have new request of trip: {description}.");

                                MessageBox("Спасибо, Ваш запрос принят в обработку");
                            }
                        });
                    return true;
                default:
                    return base.OnEventDimension(info);
            }
        }

        private void Dashboard()
        {
            FirstDocOf("Dashboard").OpenForm();
        }

        private void AdminDashboard()
        {
            FirstDocOf("AdminDashboard").OpenForm();
        }

        public override void OnStart()
        {
            if (Context.UrlTag == "admin")
            {
                InputBox("Password", "Enter password", result =>
                {
                    if (result.Result)
                    {
                        var currPassword = result.FindFirstValue("Password");

                        if (currPassword == "123")
                        {
                            AdminDashboard();
                        }
                        else
                        {
                            MessageBox("Wrong credentials.");
                        }
                    }
                });
            }
            else
            {
                Dashboard();
            }
        }

        public override BaseRenderForm CreateRenderForm(DOMForm form) => new ExtendedRenderForm(this, form);
    }
}
