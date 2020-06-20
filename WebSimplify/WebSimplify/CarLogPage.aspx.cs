using SynnWebOvi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSimplify.Data;

namespace WebSimplify
{
    public partial class CarLogPage : SynnWebFormBase
    {
        public CarArchiveDocument doc { get; set; }
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
                var l = new List<ClientPagePermissions> { ClientPagePermissions.CarLog };
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

        public string SearchName { get; private set; }
        public string SearchDescription { get; private set; }
        public int? SearchCarId { get; set; }

        public IEnumerable GetData()
        {
            var sp = new DocumentSearchParameters { };
            if (!SearchName.IsEmpty() || !SearchDescription.IsEmpty())
            {
                sp.SearchName = SearchName;
                sp.SearchDescription = SearchDescription;
            }
            sp.SearchCarId = SearchCarId;
            List<CarArchiveDocument> docs = DBController.DbDocument.GetCarDocuments(sp);
            return docs;
        }

        protected void gvAdd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var cmbCars =  ((DropDownList)e.Row.FindControl("cmbCars"));

                var car = DBController.DbGenericData.GetGenericData(new GenericDataSearchParameters { FromType = typeof(ICar) });

                foreach (GenericData c in car)
                {
                    ListItem item = new ListItem(c.Description, c.Id.ToString());
                    cmbCars.Items.Add(item);
                }
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

            DBController.DbDocument.UpsertCarDoc(doc);
            RefreshGrids();
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
                    var carId = GetDropNullAbleValue(row, "cmbCars");
                    if (carId.HasValue)
                    {
                        doc = new CarArchiveDocument
                        {
                            CreationDate = DateTime.Now,
                            Data = FileUploadControl.FileBytes,
                            LastKnownPath = Path.GetFileName(FileUploadControl.FileName),
                            Name = GetTextBoxValue(row, "txName"),
                            Description = GetTextBoxValue(row, "txIdesc"),
                            CarId = carId.Value
                        };
                    }
                    else
                    {
                        AlertMessage("לא נבחר רכב לשיוך");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    AlertMessage("Upload status: The file could not be uploaded. The following error occured: " + ex.Message);
                }
            }
        }


        private void RefreshGrids()
        {
            RefreshGrid(gvAdd);
            RefreshGrid(gvDocs);
        }

        protected void btnSearch_Command(object sender, CommandEventArgs e)
        {
            var row = (sender as ImageButton).NamingContainer as GridViewRow;

            this.SearchName = GetTextBoxValue(row, "txName");
            this.SearchDescription = GetTextBoxValue(row, "txIdesc");
            this.SearchCarId = GetDropNullAbleValue(row, "cmbCars");
            RefreshGrids();
        }

        protected void btnDownload_Command(object sender, CommandEventArgs e)
        {
            var docId = e.CommandArgument.ToString().ToInteger();
            CarArchiveDocument doc = DBController.DbDocument.GetCarDocumentArchiveFull(docId);

            DownloadFile(doc.LastKnownPath, doc.Data);
        }

        protected void gvDocs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Gv_PageIndexChanging(sender, e);
        }

        protected void gvDocs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var docA = (CarArchiveDocument)e.Row.DataItem;
                SetLabelValue(e.Row, "lblName", docA.Name);
                SetLabelValue(e.Row, "lblDescription", docA.Description);

                SetImageButtonArgument(e.Row, "btnDownload", docA.Id);

                var icar = (GenericData)DBController.DbGenericData.GetSingleGenericData(new GenericDataSearchParameters
                {
                    Id = docA.CarId,
                    FromType = typeof(ICar)
                });
                SetLabelValue(e.Row, "lblCar", icar.Description);
            }
        }
    }
}