using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FractalPlatform.Client.App;
using FractalPlatform.Client.UI.DOM;
using FractalPlatform.Common.Enums;
using FractalPlatform.Database.Dimensions.Auth;
using FractalPlatform.Database.Dimensions.Sync;
using FractalPlatform.Database.Engine;
using FractalPlatform.Database.Engine.Info;

namespace FractalPlatform.Sandbox
{
    public class WinFormsFactory : IFormFactory
    {
        public bool IsSupportComponents => false;

        public bool IsWinForms => true;

        private bool _isFormOpening = false;

        private List<MainForm> ActiveMainForms { get; } = new List<MainForm>();

        public bool HasForms => ActiveMainForms.Count > 0;

        private MainForm ActiveMainForm => HasForms ? ActiveMainForms[ActiveMainForms.Count - 1] : null;

        private DOMForm ActiveForm => ActiveMainForm?.DomForm;

        public Collection ActiveCollection => ActiveForm?.Collection;

        public AttrPath ActiveFormParentAttrPath
        {
            get
            {
                return HasForms ? AttrPath.Wrap(ActiveForm.ParentKey) : null;
            }
            set
            {
                ActiveForm.ParentKey = value.Key;
            }
        }

        public bool OpenForm(Context context,
                                 string formName,
                                 string groupBoxName,
                                 Collection collection,
                                 KeyMap parentKey,
                                 uint docID,
                                 string filter = null,
                                 Action<FormResult> handleResult = null)
        {
            bool resultEvent;

            if (!_isFormOpening) //avoid recursion in the event. Event can open form
            {
                _isFormOpening = true;

                resultEvent = context.Application
                                     .ExecuteWithSaveContext(context,
                                                             () => context.Application
                                                                          .OnOpenFormWithDebug(new FormInfo(collection, new AttrPath(parentKey), docID)));

                _isFormOpening = false;
            }
            else
            {
                resultEvent = true;
            }

            if (resultEvent)
            {
                //check auth
                if (collection.HasDimension(DimensionType.Auth))
                {
                    var auth = (AuthDimension)collection.GetDimension(DimensionType.Auth);

                    if (auth.IsNeedRelogin(context))
                    {
                        var application = context.Application as DashboardApplication;

                        if (application != null) //valid only for dashboard app
                        {
                            application.LoginOrRegister(result =>
                            {
                                OpenForm(context, formName, groupBoxName, collection, parentKey, docID, filter, handleResult);
                            });
                        }
                    }
                }

                //open form
                var domForm = new DOMForm(ActiveForm,
                                          formName,
                                          groupBoxName,
                                          collection,
                                          parentKey,
                                          docID,
                                          filter);

                var form = new MainForm(domForm);

                var prevActiveForm = ActiveForm;

                ActiveMainForms.Add(form);

                var isOk = form.ShowDialog() == DialogResult.OK;

                var result = new FormResult(isOk, domForm.Collection, domForm.DocID);

                if (handleResult != null)
                {
                    handleResult(result);

                    if (result.Result && prevActiveForm != null)
                    {
                        prevActiveForm.NeedRefreshForm = true;
                    }
                }

                //add async reload collection
                if (context.LocalInstance != null &&
                    context.LocalInstance.HasDatabases &&
                    context.LocalInstance.GetDatabase(context.CollIdentity.DatabaseID)
                                         .HasCollections &&
                    !context.CollIdentity.IsLocalCollection)
                {
                    var serverCollection = context.LocalInstance.GetCollection(context.CollIdentity);

                    var syncDimension = (SyncDimension)serverCollection.GetDimension(DimensionType.Sync);

                    if (syncDimension != null && syncDimension.AsyncReloadForm.IsEnabled)
                    {
                        syncDimension.AddReloadCollection(context, collection);

                        form.DomForm.AsyncReloadForm = syncDimension.AsyncReloadForm;
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public void NeedRefreshForm()
        {
            if (HasForms)
            {
                ActiveMainForm.DomForm.NeedRefreshForm = true;
            }
        }

        public void RefreshForm()
        {
            ActiveForm.Context.TryRaiseError();

            ActiveForm.RefreshForm();

            while (HasForms &&
                   ActiveMainForms.Count > 1 &&
                   !ActiveForm.HasVisibleControls() &&
                   !ActiveForm.HasFilter)
            {
                CloseForm();
            }
        }

        public void CloseAllForms()
        {
            while (HasForms)
            {
                CloseForm();
            }
        }

        public bool CloseForm()
        {
            if (HasForms &&
                ActiveForm.Context
                          .Application
                          .ExecuteWithSaveContext(ActiveForm.Context,
                                                  () => ActiveForm.Context.Application.OnCloseFormWithDebug(new FormInfo(ActiveForm.Collection,
                                                                         new AttrPath(ActiveForm.ParentKey),
                                                                         ActiveForm.DocID))))
            {
                //add async reload collection
                if (ActiveForm.Context.LocalInstance.HasDatabases &&
                    ActiveForm.Context.LocalInstance.GetDatabase(ActiveForm.Context.CollIdentity.DatabaseID)
                                             .HasCollections &&
                    !ActiveForm.Context.CollIdentity.IsLocalCollection)
                {
                    var serverCollection = ActiveForm.Context
                                                     .LocalInstance
                                                     .GetCollection(ActiveForm.Context.CollIdentity);

                    var syncDimension = (SyncDimension)serverCollection.GetDimension(DimensionType.Sync);

                    if (syncDimension != null && syncDimension.AsyncReloadForm.IsEnabled)
                    {
                        syncDimension.DeleteReloadCollection(ActiveForm.Context, ActiveForm.Collection);
                    }
                }

				ActiveMainForm.Close();

                ActiveMainForms.Remove(ActiveMainForm);

				return true;
            }
            else
            {
                return false;
            }
        }

        public bool SaveForm()
        {
            ActiveForm.SaveForm();

            return true;
        }
    }
}