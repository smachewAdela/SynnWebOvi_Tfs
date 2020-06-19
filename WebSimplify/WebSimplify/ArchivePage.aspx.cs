using SynnWebOvi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSimplify
{
    public partial class ArchivePage : SynnWebFormBase
    {
        public ArchiveDocument doc { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RefreshGrid(gvAdd);
                RefreshGrid(gvDocs);
            }
        }

        protected override List<ClientPagePermissions> RequiredPermissions
        {
            get
            {
                var l = new List<ClientPagePermissions> { ClientPagePermissions.Archive };
                return l;
            }
        }

        internal override string GetGridSourceMethodName(string gridId)
        {
            if (gridId == gvDocs.ID)
                return "GetData";
            if (gridId == gvAdd.ID)
                return DummyMethodName;
            return base.GetGridSourceMethodName(gridId);
        }

        public IEnumerable GetData()
        {
            List<ArchiveDocument> docs =  DBController.DbDocument.GetDocuments(new DocumentSearchParameters { });
            return docs;
        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var dummyObj = e.Row.DataItem;
            }
        }

        protected void btnAdd_Command(object sender, CommandEventArgs e)
        {
            UploadButton_Click(sender, e);
            if (doc == null)
            {
                AlertMessage("לא נבחר קובץ");
                return;
            }

            DBController.DbDocument.Upsert(doc);
            RefreshGrids();
        }

        private void RefreshGrids()
        {
            RefreshGrid(gvAdd);
            RefreshGrid(gvDocs);
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            var row = (sender as ImageButton).NamingContainer as GridViewRow;

            //var FileUploadControl = ((FileUpload)row.FindControl("FileUploadControl"));

            if (FileUploadControl.HasFile)
            {
                try
                {
                    string filename = Path.GetFileName(FileUploadControl.FileName);
                    //FileUploadControl.SaveAs(Server.MapPath("~/") + filename);
                    
                    doc = new ArchiveDocument
                    {
                        CreationDate = DateTime.Now,
                        Data = FileUploadControl.FileBytes,
                        LastKnownPath = Path.GetFileName(FileUploadControl.FileName),
                        Name = ((TextBox)row.FindControl("txName")).Text,
                        Description = ((TextBox)row.FindControl("txIdesc")).Text,
                    };

                    
                }
                catch (Exception ex)
                {
                    AlertMessage( "Upload status: The file could not be uploaded. The following error occured: " + ex.Message);
                }
            }
        }

        protected void gvDocs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var docA = (ArchiveDocument)e.Row.DataItem;
                ((Label)e.Row.FindControl("lblName")).Text = docA.Name.ToString();
                ((Label)e.Row.FindControl("lblDescription")).Text = docA.Description.ToString();
                ((Label)e.Row.FindControl("lblCreation")).Text = docA.CreationDate.ToShortDateString();
                ((ImageButton)e.Row.FindControl("btnDownload")).CommandArgument = docA.Id.ToString();
            }
        }

        protected void btnDownload_Command(object sender, CommandEventArgs e)
        {
            var docId = e.CommandArgument.ToString().ToInteger();
            ArchiveDocument doc = DBController.DbDocument.GetDocumentArchiveFull(docId);

            DownloadFile(doc.LastKnownPath, doc.Data);
        }
    }
}