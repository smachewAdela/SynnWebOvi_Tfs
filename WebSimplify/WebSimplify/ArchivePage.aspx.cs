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

        public string SearchName { get; private set; }
        public string SearchDescription { get; private set; }

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
            var sp = new DocumentSearchParameters { };
            if (!SearchName.IsEmpty() || !SearchDescription.IsEmpty())
            {
                sp.SearchName = SearchName;
                sp.SearchDescription = SearchDescription;
            }
            List<ArchiveDocument> docs =  DBController.DbDocument.GetDocuments(sp);
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
                        Name = GetTextBoxValue(row, "txName"),
                        Description = GetTextBoxValue(row, "txIdesc"),
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
                SetLabelValue(e.Row, "lblName", docA.Name);
                SetLabelValue(e.Row, "lblDescription", docA.Description);
                SetLabelValue(e.Row, "lblCreation", docA.CreationDate.ToShortDateString());
                SetImageButtonArgument(e.Row, "btnDownload", docA.Id);
            }
        }

        protected void btnDownload_Command(object sender, CommandEventArgs e)
        {
            var docId = e.CommandArgument.ToString().ToInteger();
            ArchiveDocument doc = DBController.DbDocument.GetDocumentArchiveFull(docId);

            DownloadFile(doc.LastKnownPath, doc.Data);
        }

        protected void btnSearch_Command(object sender, CommandEventArgs e)
        {
            var row = (sender as ImageButton).NamingContainer as GridViewRow;
            
            this.SearchName = GetTextBoxValue(row,"txName");
            this.SearchDescription = GetTextBoxValue(row, "txIdesc"); 

            RefreshGrids();
        }

        protected void gvDocs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Gv_PageIndexChanging(sender, e);
        }
    }
}