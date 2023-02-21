using System;
using BigDoc.Client.App;
using BigDoc.Client.UI;
using BigDoc.Client.UI.DOM;
using BigDoc.Common.Enums;
using BigDoc.Database.Engine;
using BigDoc.Database.Engine.Info;

namespace FractalPlatform.Examples.Applications.SocialNetwork
{
    public class SocialNetworkApplication : DashboardApplication
    {
        private void Dashboard()
        {
            Client.SetDefaultCollection("Dashboard")
                  .OpenForm();
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

                var query = Client.SetDefaultCollection("Users")
                                  .GetWhere("{'Posts':[{'UID':@UID}]}", uid)
                                  .WantModifyExistingDocuments();

                if (User.Name == who)
                {
                    query.ExtendDimension(DimensionType.UI, "{'Posts':[{'Message':{'Enabled':true}}]}");
                }
                else
                {
                    query.ExtendDimension(DimensionType.UI, "{'Posts':[{'Style':'Save:false;HasLabel:true'}]}");
                }

                query.OpenForm("{'Posts':[$]}");

                return false;
            }

            return true;
        }

        protected override void OnLogin(FormResult result)
        {
            if(result.Result)
            {
                Dashboard();
            }
        }

        public void MyUser()
        {
            Client.SetDefaultCollection("Users")
                  .GetWhere("{'Name':@UserName}")
                  .OpenForm();
        }

        public void Users()
        {
            Client.SetDefaultCollection("Users")
                  .GetAll()
                  .WantModifyExistingDocuments()
                  .OpenForm();
        }

        public void NewPost()
        {
            var docID = Client.SetDefaultCollection("Users")
                              .GetWhere("{'Name':@UserName}")
                              .GetFirstID();

            Client.SetDefaultCollection("NewPost")
                 .WantCreateNewDocumentForArray("Users", "{'Posts':[$]}", docID)
                 .OpenForm(result => Dashboard());
        }

        public void RequestFriend(uint docID)
        {
            if (!Client.SetDefaultCollection("Users")
                      .GetDoc(docID)
                      .AndWhere("{'Friends':[{'Name':@UserName}]}")
                      .Any())
            {
                Client.SetDefaultCollection("Users")
                      .GetDoc(docID)
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

            Client.SetDefaultCollection("NewComment")
                  .WantCreateNewDocumentForArray("Users", attrPath, docID)
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
            switch(computedInfo.Variable)
            {
                case "Avatar":
                    {
                    var photo = Client.SetDefaultCollection("Users")
                                       .GetWhere("{'Name':@UserName}")
                                       .Value("{'Photo':$}");

                    return photo;
                }
                default:
                    return base.OnComputedDimension(computedInfo);
            }
        }

        private void Friend(AttrPath attrPath, bool approve)
        {
            if (Client.SetDefaultCollection("Users")
                     .GetWhere(attrPath)
                     .AndWhere("{'Friends':[{'Approved':@Approve}]}", approve)
                     .Any())
            {
                MessageBox("You have already friend/unfriend user.");

                return;
            }

            //my friend
            Client.SetDefaultCollection("Users")
                  .GetWhere(attrPath)
                  .Update("{'Friends':[{'Approved':@Approve}]}", approve);

            //and his friend
            var friend = Client.SetDefaultCollection("Users")
                               .GetWhere(attrPath)
                               .Value("{'Friends':[{'Name':$}]}");

            if (approve)
            {
                Client.SetDefaultCollection("Users")
                      .GetWhere("{'Name':@Name}", friend)
                      .Update("{'Friends':[Add,{'Name':@UserName,'Approved':true}]}");
            }
            else
            {
                Client.SetDefaultCollection("Users")
                      .GetWhere("{'Name':@Friend,'Friends':[{'Name':@UserName}]}", friend)
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
