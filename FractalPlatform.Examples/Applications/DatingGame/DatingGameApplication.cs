using System;
using System.Linq;
using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Common.Enums;
using FractalPlatform.Database.Engine;

namespace FractalPlatform.Examples.Applications.DatingGame
{
    public class DatingGameApplication : BaseApplication
    {
        private const int MAX_GENDER_PARTICIPANTS = 3;

        private const int MAX_GENDER_QUESTIONS = 3;

        private Participant _myParticipant;

        private uint _gameID;

        private void ShowMatch(Game game)
        {
            DocsWhere("Games", "{'ID':@ID}", _gameID)
                  .Update("{'Status':'Finished'}");

            var participants = _myParticipant.Gender == GenderType.Boy ? game.Girls : game.Boys;

            var choosedParticipant = participants.FirstOrDefault(x => x.Name == _myParticipant.Choose);

            if (choosedParticipant != null && 
                choosedParticipant.Choose == _myParticipant.Name)
            {
                //Match !
                MessageBox("Congratulations, you have MATCH ! Click Ok to show details of your choose",
                           "Match",
                           MessageBoxButtonType.OkCancel,
                           result => 
                           {
                               if (result.Result)
                               {
                                   DocsWhere("Games", "{'ID':@ID,@Gender:[{'Name':@Name}]}",
                                                       _gameID,
                                                       choosedParticipant.GenderGroup,
                                                       choosedParticipant.Name)
                                         .OpenForm(DQL("{@Gender:[!$]}", choosedParticipant.GenderGroup), "Match");
                               }
                               else
                               {
                                   MessageBox("Game is finished.");
                               }
                           });
            }
            else
            {
                MessageBox("Hm, you don't have match in this round. Let's try new game.");
            }
        }

        private void WaitChooses(FormResult formResult = null)
        {
            if (formResult == null || formResult.Result)
            {
                var game = DocsWhere("Games", "{'ID':@ID}", _gameID)
                                 .Select<Game>()[0];

                var countChooses = 0;

                if (_myParticipant.Gender == GenderType.Boy)
                {
                    countChooses = game.Girls.Count(x => !string.IsNullOrEmpty(x.Choose));
                }
                else
                {
                    countChooses = game.Boys.Count(x => !string.IsNullOrEmpty(x.Choose));
                }

                if (countChooses == MAX_GENDER_PARTICIPANTS ||
                    game.ExpiredChooses < DateTime.Now)
                {
                    ShowMatch(game);
                }
                else
                {
                    MessageBox($"We are waiting chooses from all participants (Count chooses:{0}). Game is going to continue in {game.ExpiredChooses.Subtract(DateTime.Now).Seconds} seconds or earlier. Press OK to refresh ...",
                               null,
                               MessageBoxButtonType.Ok,
                               WaitChooses);
                }
            }
        }

        private void ShowAnswers(Game game)
        {
            var participants = _myParticipant.Gender == GenderType.Boy ? game.Girls : game.Boys;

            var chooseParticipants = participants.Select(x => x.Name).ToList();
            chooseParticipants.Insert(0, "NoBody");

            DocsWhere("Games", "{'ID':@ID,'AnswerQuestions':[{'From':@Name}]}", _gameID, _myParticipant.Name)
                  .SetUIDimension("{'Style':'Add:false;Del:false','AnswerQuestion':{'Enabled':false},'Choose':{'ControlType':'ComboBox'}}")
                  .SetDimension(DimensionType.Enum, DQL("{'Choose':{'Items':[@Participants]}}", chooseParticipants))
                  .ExtendDocument("{'Choose':'NoBody'}", _gameID)
                  .OpenForm("{'AnswerQuestions':[{'To':$,'Question':$,'Answer':$}]}",
                            result =>
                            {
                               if (result.Result)
                               {
                                   _myParticipant.Choose = result.Collection
                                                                 .GetDoc(_gameID)
                                                                 .Value("{'Choose':$}");

                                   DocsWhere("Games", "{'ID':@ID,@Gender:[{'Name':@Name}]}", _gameID, _myParticipant.GenderGroup, _myParticipant.Name)
                                         .Update("{@Gender:[{'Choose':@Choose}]}", _myParticipant.GenderGroup, _myParticipant.Choose);

                                   //8. Update choose
                                   WaitChooses();
                               }
                            });
        }

        private void WaitAnswers(FormResult formResult = null)
        {
            if (formResult == null || formResult.Result)
            {
                var game = DocsWhere("Games", "{'ID':@ID}", _gameID)
                                 .Select<Game>()[0];

                if (game.AnswerQuestions.All(x => !string.IsNullOrEmpty(x.Answer)) ||
                    game.ExpiredAnswers < DateTime.Now)
                {
                    ShowAnswers(game);
                }
                else
                {
                    MessageBox($"We are waiting answers (Count answers:{game.Boys.Count}). Game is going to continue in {game.ExpiredAnswers.Subtract(DateTime.Now).Seconds} seconds or earlier. Press OK to refresh ...",
                               null,
                               MessageBoxButtonType.Ok,
                               WaitAnswers);
                }
            }
        }

        private void ShowQuestions()
        {
            ModifyDocsWhere("Games", "{'ID':@ID,'AnswerQuestions':[{'To':@Name}]}", _gameID, _myParticipant.Name)
                  .SetUIDimension("{'Style':'Add:false;Del:false','AnswerQuestions':[{'From':{'Enabled':false},'Question':{'Enabled':false}}]}")
                  .SetDimension(DimensionType.Validation, "{'AnswerQuestions':[{'Answer':{'IsRequired':true}}]}")
                  .OpenForm("{'AnswerQuestions':[{'From':$,'Question':$,'Answer':$}]}",
                            result =>
                            {
                                if (result.Result)
                                {
                                    WaitAnswers();
                                }
                            });
        }

        private void AddQuestion(Participant from, string question, Participant to)
        {
            DocsWhere("Games", "{'ID':@ID}", _gameID)
                .Update("{'AnswerQuestions':[Add,{'From':@From,'Question':@Question,'To':@To,'Answer':''}]}",
                        from.Name, question, to.Name);
        }

        private void StartGame(FormResult formResult)
        {
            if (formResult.Result)
            {
                var game = DocsWhere("Games", "{'ID':@ID}", _gameID)
                                 .SelectOne<Game>();

                if ((game.Boys.Count >= MAX_GENDER_PARTICIPANTS &&
                     game.Girls.Count >= MAX_GENDER_PARTICIPANTS) || //participants are collected
                     (game.ExpiredParticipants < DateTime.Now &&
                      game.Boys.Count >= 1 &&
                      game.Girls.Count >= 1)) //or time is expired
                {
                    //6. Update status
                    if (game.Status == GameStatus.Pending)
                    {
                        //5. Add questions from participants
                        foreach (var from in game.Boys)
                        {
                            foreach (var to in game.Girls)
                            {
                                foreach (string question in from.Questions)
                                {
                                    AddQuestion(from, question, to);
                                }
                            }
                        }

                        foreach (var from in game.Girls)
                        {
                            foreach (var to in game.Boys)
                            {
                                foreach (string question in from.Questions)
                                {
                                    AddQuestion(from, question, to);
                                }
                            }
                        }

                        DocsWhere("Games", "{'ID':@ID}", _gameID)
                          .Update("{'Status':@Status}", GameStatus.Started);
                    }

                    //7. Show questions
                    ShowQuestions();
                }
                else
                {
                    if (game.ExpiredParticipants < DateTime.Now)
                    {
                        MessageBox("Sorry, we don't have enough paricipants for this game. Try to participate later in the new game.");
                    }
                    else
                    {
                        MessageBox($"We are waiting participants (Boys:{game.Boys.Count} / Girls:{game.Girls.Count}). Game is going to start: {game.ExpiredParticipants}. Press OK to refresh ...",
                               null,
                               MessageBoxButtonType.Ok,
                               StartGame);
                    }
                }
            }
        }

        public override void OnStart()
        {
            //1. Register participant
            FirstDocOf("NewParticipant")
                  .OpenForm(result =>
                  {
                      if (result.Result)
                      {
                          //2. Get participant
                          _myParticipant = result.Collection
                                             .GetFirstDoc()
                                             .SelectOne<Participant>();

                          if (_myParticipant.Questions.Count > MAX_GENDER_QUESTIONS)
                          {
                              MessageBox($"You have to ask not more than {MAX_GENDER_QUESTIONS} questions in the game.");

                              return;
                          }

                          //3. Try find Pending game
                          Game[] games;

                          while (true)
                          {
                              games = DocsWhere("Games", "{'Status':@Status}", GameStatus.Pending)
                                            .Select<Game>();

                              foreach (var game in games)
                              {
                                  if (game.ExpiredParticipants < DateTime.Now)
                                  {
                                      DocsWhere("Games", "{'ID':@ID}", _gameID)
                                            .Update("{'Status':'Finished'}");

                                      continue;
                                  }
                                  else if ((_myParticipant.Gender == GenderType.Boy &&
                                            game.Boys.Count < MAX_GENDER_PARTICIPANTS) ||
                                          (_myParticipant.Gender == GenderType.Girl &&
                                            game.Girls.Count < MAX_GENDER_PARTICIPANTS))
                                  {
                                      //game is not full
                                      _gameID = game.ID;

                                      break;
                                  }
                              }

                              if (_gameID > 0)
                                  break;

                              var newDocID = DocsOf("Games")
                                                .Count() + 1;

                              //3. Create new game
                              Client.SetDefaultCollection("Games")
                                    .AddDoc(new Game
                                    {
                                        ID = newDocID,
                                        Status = GameStatus.Pending,
                                        StartDate = DateTime.Now,
                                        ExpiredParticipants = DateTime.Now.AddMinutes(5),
                                        ExpiredAnswers = DateTime.Now.AddMinutes(10),
                                        ExpiredChooses = DateTime.Now.AddMinutes(15),
                                    });
                          }

                          //4. Add participant to game
                          DocsWhere("Games", "{'ID':@ID}", _gameID)
                                .Update("{@Gender:[Add,@Participant]}",
                                            _myParticipant.GenderGroup,
                                            _myParticipant);

                          //5. Check 3+3 participants
                          StartGame(result);
                      }
                  });
        }
    }
}
