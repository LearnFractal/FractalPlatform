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
        private void Dashboard()
        {
            FirstDocOf("Dashboard").OpenForm();
        }

        public override bool OnOpenForm(FormInfo formInfo)
        {
            if (formInfo.AttrPath.GetFirstPath() == "ViewPosts")
            {
                var uid = formInfo.Collection
                                  .GetWhere(formInfo.AttrPath)
                                  .Value("{'ViewPosts':[{'UID':$}]}");

                var who = formInfo.Collection
                                  .GetWhere(formInfo.AttrPath)
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

        public void MyUser()
        {
            ModifyDocsWhere("Users", "{'Name':@UserName}")
                  .ExtendUIDimension("{'ReadOnly':false,'Style':'Save:true;Hide:Avatar,Photo,Value,NewComment;Add:false'}")
                  .OpenForm();
        }

        public void Users()
        {
            ModifyDocsOf("Users")
                  .OpenForm();
        }

        public void NewPost()
        {
            var docID = DocsWhere("Users", "{'Name':@UserName}")
                              .GetFirstID();

            CreateNewDocForArray("NewPost", "Users", "{'Posts':[$]}", docID)
                 .OpenForm(result => Dashboard());
        }

        public void RequestFriend(uint docID)
        {
            if (!DocsWhere("Users", docID)
                      .AndWhere("{'Friends':[{'Name':@UserName}]}")
                      .Any())
            {
                DocsWhere("Users", docID)
                      .Update("{'Friends':[Add,{'Name':@UserName,'Approved':false}]}");

                MessageBox("You have sent friend request.");
            }
            else
            {
                MessageBox("Your have already sent friend request.");
            }
        }

        public void NewComment(AttrPath attrPath, uint docID)
        {
            attrPath.RemoveLastPath()
                    .AddPath("Comments")
                    .AddIndex();

            Log(attrPath);

            CreateNewDocForArray("NewComment", "Users", attrPath, docID)
                  .OpenForm();
        }

        public override bool OnEventDimension(EventInfo eventInfo)
        {
            switch (eventInfo.Action)
            {
                case "Login":
                    Login("Rebeca", "Bob");
                    return true;
                case "MyUser":
                    MyUser();
                    return true;
                case "Users":
                    Users();
                    return true;
                case "NewPost":
                    NewPost();
                    return true;
                case "RequestFriend":
                    RequestFriend(eventInfo.DocID);
                    return true;
                case "NewComment":
                    NewComment(eventInfo.AttrPath, eventInfo.DocID);
                    return true;
                default:
                    return base.OnEventDimension(eventInfo);
            }
        }

        public override object OnComputedDimension(ComputedInfo computedInfo)
        {
            switch (computedInfo.Variable)
            {
                case "Avatar":
                    {
                        var photo = DocsWhere("Users", "{'Name':@UserName}")
                                           .Value("{'Photo':$}");

                        return photo;
                    }
                default:
                    return base.OnComputedDimension(computedInfo);
            }
        }

        private void Friend(AttrPath attrPath, bool approve)
        {
            if (DocsWhere("Users", attrPath)
                     .AndWhere("{'Friends':[{'Approved':@Approve}]}", approve)
                     .Any())
            {
                MessageBox("You have already friend/unfriend user.");

                return;
            }

            //my friend
            DocsWhere("Users", attrPath)
                  .Update("{'Friends':[{'Approved':@Approve}]}", approve);

            //and his friend
            var friend = DocsWhere("Users", attrPath)
                               .Value("{'Friends':[{'Name':$}]}");

            if (approve)
            {
                DocsWhere("Users", "{'Name':@Name}", friend)
                      .Update("{'Friends':[Add,{'Name':@UserName,'Approved':true}]}");
            }
            else
            {
                DocsWhere("Users", "{'Name':@Friend,'Friends':[{'Name':@UserName}]}", friend)
                      .Delete("{'Friends':[{'Name':$,'Approved':$}]}");
            }
        }

        public override bool OnMenuDimension(MenuInfo menuInfo)
        {
            switch (menuInfo.Action)
            {
                case "Friend":
                    Friend(menuInfo.AttrPath, true);
                    break;
                case "Unfriend":
                    Friend(menuInfo.AttrPath, false);
                    break;
                default:
                    throw new NotImplementedException();
            }

            return true;
        }

        public override BaseRenderForm CreateRenderForm(DOMForm form) => new RenderForm(this, form);
    }
}
