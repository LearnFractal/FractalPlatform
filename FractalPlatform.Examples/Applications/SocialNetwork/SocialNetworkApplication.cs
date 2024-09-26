using System;
using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Client.UI.DOM;
using FractalPlatform.Database.Engine;
using FractalPlatform.Database.Engine.Info;

namespace FractalPlatform.Examples.Applications.SocialNetwork
{
    public class SocialNetworkApplication : DashboardApplication
    {
        private void Dashboard() => FirstDocOf("Dashboard").OpenForm();

        public override bool OnOpenForm(FormInfo info)
        {
            if (info.AttrPath.GetFirstPath() == "ViewPosts")
            {
                var uid = info.Collection
                                  .GetWhere(info.AttrPath)
                                  .Value("{'ViewPosts':[{'UID':$}]}");

                var who = info.Collection
                                  .GetWhere(info.AttrPath)
                                  .Value("{'ViewPosts':[{'Who':$}]}");

                var query = DocsWhere("Users", "{'Posts':[{'UID':@UID}]}", uid)
                                  .WantModifyExistingDocuments();

                if (User.Name == who)
                {
                    query.ExtendUIDimension("{'Posts':[{'Message':{'ReadOnly':false},'NewComment':{'ReadOnly':false}}]}");
                }
                else
                {
                    query.ExtendUIDimension("{'Posts':[{'Style':'Save:false;HasLabel:true','NewComment':{'ReadOnly':false}}]}");
                }

                query.OpenForm("{'Posts':[$]}");

                return false;
            }

            return true;
        }

        public override void OnLogin(FormResult result)
        {
            if (result.Result)
            {
                Dashboard();
            }
        }

        public bool MyUser() =>
            ModifyDocsWhere("Users", "{'Name':@UserName}")
                  .ExtendUIDimension("{'ReadOnly':false,'Style':'Save:true;Hide:Avatar,Photo,Value,NewComment;Add:false'}")
                  .OpenForm();

        public bool Users() => ModifyDocsOf("Users").OpenForm();

        public bool NewPost()
        {
            var docID = DocsWhere("Users", "{'Name':@UserName}")
                              .GetFirstID();

            return CreateNewDocForArray("NewPost", "Users", "{'Posts':[$]}", docID)
                    .OpenForm(result => Dashboard());
        }

        public bool RequestFriend(uint docID)
        {
            if (!DocsWhere("Users", docID)
                      .AndWhere("{'Friends':[{'Name':@UserName}]}")
                      .Any())
            {
                DocsWhere("Users", docID)
                      .Update("{'Friends':[Add,{'Name':@UserName,'Approved':false}]}");

                return MessageBox("You have sent friend request.");
            }
            else
            {
                return MessageBox("Your have already sent friend request.");
            }
        }

        public bool NewComment(AttrPath attrPath, uint docID)
        {
            attrPath.RemoveLastPath()
                    .AddPath("Comments")
                    .AddIndex();

            Log(attrPath);

            return CreateNewDocForArray("NewComment", "Users", attrPath, docID).OpenForm();
        }

        public override bool OnEventDimension(EventInfo info) =>
            info.Action switch
            {
                "Login" => Login("Rebeca", "Bob"),
                "MyUser" => MyUser(),
                "Users" => Users(),
                "NewPost" => NewPost(),
                "RequestFriend" => RequestFriend(info.DocID),
                "NewComment" => NewComment(info.AttrPath, info.DocID),
                _ => base.OnEventDimension(info)
            };

        public override object OnComputedDimension(ComputedInfo info) =>
            info.Variable switch
            {
                "Avatar" => DocsWhere("Users", "{'Name':@UserName}").Value("{'Photo':$}"),
                _ => base.OnComputedDimension(info)
            };

        private bool Friend(AttrPath attrPath, bool approve)
        {
            if (DocsWhere("Users", attrPath)
                     .AndWhere("{'Friends':[{'Approved':@Approve}]}", approve)
                     .Any())
            {
                MessageBox("You have already friend/unfriend user.");

                return true;
            }

            //my friend
            DocsWhere("Users", attrPath)
                  .Update("{'Friends':[{'Approved':@Approve}]}", approve);

            //and his friend
            var friend = DocsWhere("Users", attrPath)
                               .Value("{'Friends':[{'Name':$}]}");

            if (approve)
            {
                return DocsWhere("Users", "{'Name':@Name}", friend)
                        .Update("{'Friends':[Add,{'Name':@UserName,'Approved':true}]}");
            }
            else
            {
                return DocsWhere("Users", "{'Name':@Friend,'Friends':[{'Name':@UserName}]}", friend)
                        .Delete("{'Friends':[{'Name':$,'Approved':$}]}");
            }
        }

        public override bool OnMenuDimension(MenuInfo info) =>
                info.Action switch
                {
                    "Friend" => Friend(info.AttrPath, true),
                    "Unfriend" => Friend(info.AttrPath, false),
                    _ => throw new NotImplementedException()
                };

        public override BaseRenderForm CreateRenderForm(DOMForm form) => new RenderForm(this, form);
    }
}