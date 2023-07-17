using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Text;

public partial class Auth_AdminPanel_WhatsNew_View_News : System.Web.UI.Page
{
    Miscelleneous_BL miscellBL = new Miscelleneous_BL();
    Project_Variables p_Var = new Project_Variables();
    UserOB obj_userOB = new UserOB();
    //TenderOB ObjTenderOB = new TenderOB();
    //TenderBL objTenderBL = new TenderBL();
    WhatNewsOB objWhatNewsOB = new WhatNewsOB();
    WhatsNewBL objwn = new WhatsNewBL();
    UserBL obj_UserBL = new UserBL();
	Role_PermissionBL obj_RoleBL = new Role_PermissionBL();
    PetitionOB petObject = new PetitionOB();
    PetitionBL petPetitionBL = new PetitionBL();

    ModuleAuditTrail obj_audit = new ModuleAuditTrail();
    ModuleAuditTrailDL obj_auditDl = new ModuleAuditTrailDL();
    protected void Page_Load(object senter, EventArgs e)
    {
        Page.Title = "HERC:View News";

        if (!IsPostBack)
        {
            lblPageSize.Visible = false;
            ddlPageSize.Visible = false;
            ddlPageSize.Visible = false;
            lblPageSize.Visible = false;
            BtnForReview.Visible = false;
            btnForApprove.Visible = false;
            btnDisApprove.Visible = false;
            btnApprove.Visible = false;
            BtnForReview.Visible = false;
           
            chk_privilages();

            if (Session["WhatsNewStatus"] != null)
            {
                binddropDownlistStatus();
                ddlStatus.SelectedValue = Session["WhatsNewStatus"].ToString();
                getTempNews();
            }
            else
            {
                binddropDownlistStatus();
            }
           // binddropDownlistStatus();

        }
    }



    

    #region Function to bind status in dropDownlist according to permission

    public void binddropDownlistStatus()
    {
        Miscelleneous_BL miscDdlStatus = new Miscelleneous_BL();
        Miscelleneous_BL miscdlStatus = new Miscelleneous_BL();

        obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
        obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
        p_Var.dSet = miscDdlStatus.getLanguagePermission(obj_userOB);
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {
            UserOB usrObject = new UserOB();
            if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][3]) == true)
            {
                p_Var.sbuilder.Append(Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.draft)).Append(",");
                btnAddNew.Visible = true;

                //code written on date 21 sep 2013
                p_Var.sbuilder.Append(Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved)).Append(",");

                //end
            }
            else
            {
                btnAddNew.Visible = false;
            }
            if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][4]) == true)
            {
                p_Var.sbuilder.Append(Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review)).Append(",");

            }
            if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][5]) == true)
            {
                p_Var.sbuilder.Append(Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved)).Append(",");
                p_Var.sbuilder.Append(Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover)).Append(",");
                p_Var.sbuilder.Append(Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete));

            }

            usrObject.ModulestatusID = p_Var.sbuilder.ToString();
            p_Var.sbuilder.Remove(0, p_Var.sbuilder.Length);
            p_Var.dSet = null;
            p_Var.dSet = miscdlStatus.getStatusPermissionwise(usrObject);
            ddlStatus.DataSource = p_Var.dSet;
            ddlStatus.DataTextField = "Status";
            ddlStatus.DataValueField = "Status_Id";
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("Select Status", "0"));
            //  ddlStatus.Items.Add(new ListItem("Expiry Data", "100"));
           // btnForReview.Visible = false;
        }
        p_Var.dSet = null;
    }

    #endregion



    protected void getTempNews()
    {
        objWhatNewsOB.ActionType = 1;
      
        objWhatNewsOB.ApproveStatus = Convert.ToInt32(ddlStatus.SelectedValue);

        objWhatNewsOB.TenderId = 0;
        objWhatNewsOB.PageIndex = 1;
        objWhatNewsOB.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
       
        DataSet ds = objwn.ASP_Get_News(objWhatNewsOB);
        ViewState["DatasSet"] = ds;
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete))
            {
                GvAdd_Details.Columns[7].HeaderText = "Purge";
                GvAdd_Details.Columns[9].Visible = false;
            }
            else
            {
                GvAdd_Details.Columns[7].HeaderText = "Delete";
            }

            lblmsg.Visible = false;
            GvAdd_Details.Visible = true;
            GvAdd_Details.DataSource = ds.Tables[0];
            GvAdd_Details.DataBind();

            

            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved))
            {
                GvAdd_Details.Columns[0].Visible = false;
                GvAdd_Details.Columns[9].Visible = true;
            }
            else
            {
                GvAdd_Details.Columns[0].Visible = true;
                GvAdd_Details.Columns[9].Visible = false;
            }
            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.draft))
            {
                BtnForReview.Visible = true;
                GvAdd_Details.Columns[9].Visible = false;
            }
            else
            {
                BtnForReview.Visible = false;
            }

            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review))
            {
                GvAdd_Details.Columns[9].Visible = false;
                btnForApprove.Visible = true;
                btnDisApprove.Visible = true;
            }
            else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
            {
                GvAdd_Details.Columns[9].Visible = true;
                btnApprove.Visible = true;
                btnDisApprove.Visible = true;
            }
            else
            {
                btnApprove.Visible = false;
                btnForApprove.Visible = false;
                btnDisApprove.Visible = false;
            }

            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
            {
                GvAdd_Details.Columns[9].Visible = true;
                btnApprove.Visible = true;
                btnDisApprove.Visible = true;
                btnForApprove.Visible = false;
            }
            else
            {

                btnApprove.Visible = false;
                //btnDisApprove.Visible = false;
            }



            //This is done by ruchi on date 26 dec 2013
            Miscelleneous_BL miscDdlLanguage = new Miscelleneous_BL();
            Miscelleneous_BL miscdlLanguage = new Miscelleneous_BL();
            obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
            obj_userOB.ModuleId = Convert.ToInt32(Request.QueryString["ModuleID"]);
            p_Var.dSet = miscDdlLanguage.getLanguagePermission(obj_userOB);

            if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][6]) == true) //This is for Edit Permission
            {
                if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete))
                {
                    GvAdd_Details.Columns[9].Visible = false;
                    GvAdd_Details.Columns[6].Visible = false;  //This is for Edit Column

                }
                else
                {
                    GvAdd_Details.Columns[6].Visible = true; 
                }
            }
            else
            {
                GvAdd_Details.Columns[6].Visible = false; //This is for Edit Column
            }
            if (Convert.ToBoolean(p_Var.dSet.Tables[0].Rows[0][7]) == true)  //This is for Delete Permission
            {
                GvAdd_Details.Columns[7].Visible = true;  //This is for delete Column
            }
            else
            {
                GvAdd_Details.Columns[7].Visible = false;  //This is for delete Column

            }
            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete))
            {
                GvAdd_Details.Columns[9].Visible = false;
                GvAdd_Details.Columns[8].Visible = true;  //This is for restore Column
            }
            else
            {
                GvAdd_Details.Columns[8].Visible = false;  //This is for restore Column
            }

        }
        else
        {
            GvAdd_Details.Visible = false;
            btnForApprove.Visible = false;
            BtnForReview.Visible = false;
            btnApprove.Visible = false;
            btnDisApprove.Visible = false;
            lblmsg.Visible = true;
            lblmsg.Text = "Records Not Found";
          
            
        }


    }
  
    protected void btnForApprove_Click(object senter, EventArgs e)
    {
        pnlPopUpEmails.Visible = true;
        pnlGrid.Visible = false;
        bindCheckBoxListWithApproverEmailIDs();
        
       
    }
    protected void btnApprove_Click(object senter, EventArgs e)
    {
        try
        {
           
            foreach (GridViewRow row in GvAdd_Details.Rows)
            {
                CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                if ((selCheck.Checked == true))
                {
                  p_Var.dataKeyID = Convert.ToInt32(GvAdd_Details.DataKeys[row.RowIndex].Value);
                  objWhatNewsOB.TenderId = p_Var.dataKeyID;
                  objWhatNewsOB.ApproveStatus = (int)Module_ID_Enum.Module_Permission_ID.Approved;
                  objWhatNewsOB.Status = Convert.ToInt32(ddlStatus.SelectedValue);
                  objWhatNewsOB.UserId = Convert.ToInt32(Session["User_Id"]);
                  objWhatNewsOB.IpAddress = Miscelleneous_DL.getclientIP();
                  p_Var.Result = objwn.ASP_InsertUpdateDelete_News(objWhatNewsOB);
                   

                }
            }
            if (p_Var.Result > 0)
            {
                Session["msg"] = "What's new record has been published successfully.";
                Session["Redirect"] = "~/Auth/AdminPanel/WhatsNew/View_News.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
               

            }
            else
            {
                Session["msg"] = "What's new record has not been published";
                Session["Redirect"] = "~/Auth/AdminPanel/WhatsNew/View_News.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
            }
        }
        catch
        {
            throw;
        }
    }
    protected void btnDisApprove_Click(object senter, EventArgs e)
    {

        if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
        {
            pnlPopUpEmailsDis.Visible = true;
            pnlGrid.Visible = false;
            bindCheckBoxListWithEmailIDDiaapproves();
            //this.mpeSendEmailDis.Show();
        }
        else
        {
            pnlPopUpEmailsDis.Visible = true;
            pnlGrid.Visible = false;
            bindCheckBoxListWithDataEntryEmailIDs();
            // this.mpeSendEmailDis.Show();
        }
       
    }
    protected void btnAddNew_Click(object senter, EventArgs e)
    {
        Response.Redirect(ResolveUrl("~/auth/adminpanel/WhatsNew/") + "AddNews.aspx?ModuleID=7");
    }
    protected void ddlLanguage_SelectedIndexChanged(object senter, EventArgs e)
    {
        binddropDownlistStatus();
    }
    protected void ddlStatus_SelectedIndexChanged(object senter, EventArgs e)
    {
        Session["WhatsNewStatus"] = ddlStatus.SelectedValue;
        getTempNews();
        
    }
    protected void OnPaging(object senter, GridViewPageEventArgs e)
    {
        GvAdd_Details.PageIndex = e.NewPageIndex;

        GvAdd_Details.DataBind();
        getTempNews();
    }
    protected void ddlPageSize_SelectedIndexChanged(object senter, EventArgs e)
    {

    }

    protected void GvAdd_Details_RowCommand(object senter, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Restore")
        {
          objWhatNewsOB.TenderId = Convert.ToInt32(e.CommandArgument);
          objWhatNewsOB.ApproveStatus = Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved);
          objWhatNewsOB.ModuleID = (int)Module_ID_Enum.Project_Module_ID.Whats_New;
            int i = objwn.RestoreNews(objWhatNewsOB);
            
            if(i>0)
            {

                obj_audit.ActionType = "R";
                obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                obj_audit.UserName = Session["UserName"].ToString();
                obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                obj_audit.IpAddress = miscellBL.IpAddress();
                obj_audit.Title = "NewsR";
                obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                Session["msg"] = "What's new record has been restored successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/WhatsNew/View_News.aspx?ModuleID=7";
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");

                    }
                    else
                    {
                        Session["msg"] = "What's new record has not been restored.";
                        Session["Redirect"] = "~/Auth/AdminPanel/WhatsNew/View_News.aspx?ModuleID=7";
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");

                    }
        }

        if (e.CommandName == "delete")
        {
            Control ctrl1 = e.CommandSource as Control;
            if (ctrl1 != null)
            {
                GridViewRow row = ctrl1.Parent.NamingContainer as GridViewRow;

                Label refNo = (Label)row.FindControl("LblModuleID");
                HiddenField hfRefNumber = (HiddenField)row.FindControl("hfTenderRefNo");
                if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Approved))
                {
                  objWhatNewsOB.ActionType = 2;
                  objWhatNewsOB.TenderId = Convert.ToInt32(e.CommandArgument);
                  objWhatNewsOB.ApproveStatus = Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete);
                  objWhatNewsOB.ModuleID = (int)Module_ID_Enum.Project_Module_ID.Whats_New;
                   
                    p_Var.Result = objwn.News_Delete(objWhatNewsOB);
                       
                    if (p_Var.Result > 0)
                    {
                      objWhatNewsOB.ActionType = 4;
                       
                        p_Var.Result = objwn.News_Delete(objWhatNewsOB);


                        obj_audit.ActionType = "D";
                        obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                        obj_audit.UserName = Session["UserName"].ToString();
                        obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                        obj_audit.IpAddress = miscellBL.IpAddress();
                        obj_audit.Title = "News";
                        obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                        Session["msg"] = "What's new record has been deleted successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/WhatsNew/View_News.aspx?ModuleID=7 ";

                        Response.Redirect(ResolveUrl("~/Auth/AdminPanel/ConfirmationPage.aspx"));

                    }
                    else
                    {
                        Session["msg"] = "What's new record has not been deleted.";
                        Session["Redirect"] = "~/Auth/AdminPanel/WhatsNew/View_News.aspx??ModuleID=7";
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");

                    }
                }
                else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete))
                {
                    objWhatNewsOB.ActionType = Convert.ToInt32(Module_ID_Enum.Project_Action_Type.delete); 
                  objWhatNewsOB.TenderId = Convert.ToInt32(e.CommandArgument);
                  objWhatNewsOB.ApproveStatus = Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete);
                    p_Var.Result = objwn.News_Delete(objWhatNewsOB);
                    if (p_Var.Result > 0)
                    {
                      objWhatNewsOB.ActionType = 1;
                      objWhatNewsOB.ModuleID = (int)Module_ID_Enum.Project_Module_ID.Whats_New;
                      objWhatNewsOB.Status = Convert.ToInt32(ddlStatus.SelectedValue);

                      objWhatNewsOB.ActionType = Convert.ToInt32(Module_ID_Enum.Project_Action_Type.delete);

                      obj_audit.ActionType = "D";
                      obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                      obj_audit.UserName = Session["UserName"].ToString();
                      obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                      obj_audit.IpAddress = miscellBL.IpAddress();
                      obj_audit.Title = "News";
                      obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                      Session["msg"] = "What's new record has been purged (deleted permanently) successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/WhatsNew/View_News.aspx?ModuleID=7";
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");

                    }
                    else
                    {
                        Session["msg"] = "What's new record has not been purged (deleted permanently).";
                        Session["Redirect"] = "~/Auth/AdminPanel/WhatsNew/View_News.aspx?ModuleID=7 ";
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");

                    }
                }
                else 
                {
                  objWhatNewsOB.ActionType = Convert.ToInt32(Module_ID_Enum.Project_Action_Type.insert);
                  objWhatNewsOB.TenderId = Convert.ToInt32(e.CommandArgument);
                  objWhatNewsOB.ApproveStatus = Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete);
                    p_Var.Result = objwn.News_Delete(objWhatNewsOB);
                    if (p_Var.Result > 0)
                    {
                      objWhatNewsOB.ActionType = 1;
                      objWhatNewsOB.ModuleID = (int)Module_ID_Enum.Project_Module_ID.Whats_New;
                      objWhatNewsOB.Status = Convert.ToInt32(ddlStatus.SelectedValue);

                      objWhatNewsOB.ActionType = 3;

                      obj_audit.ActionType = "D";
                      obj_audit.UserId = Convert.ToInt32(Session["User_Id"]);
                      obj_audit.UserName = Session["UserName"].ToString();
                      obj_audit.ModuleID = Convert.ToInt32(Request.QueryString["ModuleID"]);
                      obj_audit.IpAddress = miscellBL.IpAddress();
                      obj_audit.Title = "News";
                      obj_auditDl.InsertModuleAuditTrailDetails(obj_audit);

                      Session["msg"] = "What's new record has been deleted successfully.";
                        Session["Redirect"] = "~/Auth/AdminPanel/WhatsNew/View_News.aspx?ModuleID=7";
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");

                    }
                    else
                    {
                        Session["msg"] = "What's new record has not been deleted.";
                        Session["Redirect"] = "~/Auth/AdminPanel/WhatsNew/View_News.aspx?ModuleID=7 ";
                        Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");

                    }
                }
            }
        }
        else if (e.CommandName == "Audit")
        {

            DataSet dSetAuditTrail = new DataSet();
            petObject.PetitionId = Convert.ToInt32(e.CommandArgument);
            petObject.ModuleID = Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Whats_New);
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            petObject.ModuleType = null;
            dSetAuditTrail = petPetitionBL.AuditTrailRecords(petObject);
            HiddenField lblprono = row.FindControl("hidTitle") as HiddenField;
            if (dSetAuditTrail.Tables[0].Rows.Count > 0)
            {
                ltrlPetitionNo.Text = "<strong>" + lblprono.Value + "</strong>";
                if (dSetAuditTrail.Tables[0].Rows[0]["createdBy"] != DBNull.Value)
                {
                    ltrlCreation.Text = "by : " + dSetAuditTrail.Tables[0].Rows[0]["createdBy"].ToString() + " on : " + dSetAuditTrail.Tables[0].Rows[0]["CreatedDateTime"].ToString() + " from IP : " + dSetAuditTrail.Tables[0].Rows[0]["CreatedIPAddress"].ToString();
                }
                else
                {
                    ltrlCreation.Text = "";
                }
                if (dSetAuditTrail.Tables[0].Rows[0]["editedBy"] != DBNull.Value)
                {
                    ltrlLastEdited.Text = "by : " + dSetAuditTrail.Tables[0].Rows[0]["editedBy"].ToString() + " on : " + dSetAuditTrail.Tables[0].Rows[0]["EditedDateTime"].ToString() + " from IP : " + dSetAuditTrail.Tables[0].Rows[0]["UpdatedIPAddress"].ToString();
                }
                else
                {
                    ltrlLastEdited.Text = "";
                }
                if (dSetAuditTrail.Tables[0].Rows[0]["reviewedBy"] != DBNull.Value)
                {
                    ltrlLastReviewed.Text = "by : " + dSetAuditTrail.Tables[0].Rows[0]["reviewedBy"].ToString() + " on : " + dSetAuditTrail.Tables[0].Rows[0]["ReviewedDate"].ToString() + " from IP : " + dSetAuditTrail.Tables[0].Rows[0]["ReviewedIPAddress"].ToString();
                }
                else
                {
                    ltrlLastReviewed.Text = "";
                }
                if (dSetAuditTrail.Tables[0].Rows[0]["publishedBy"] != DBNull.Value)
                {
                    ltrlLastPublished.Text = "by : " + dSetAuditTrail.Tables[0].Rows[0]["publishedBy"].ToString() + " on : " + dSetAuditTrail.Tables[0].Rows[0]["PublishedDateTime"].ToString() + " from IP : " + dSetAuditTrail.Tables[0].Rows[0]["PublishedIPAddress"].ToString();
                }
                else
                {
                    ltrlLastPublished.Text = "";
                }
            }
            else
            {
                ltrlCreation.Text = "";
                ltrlLastEdited.Text = "";
                ltrlLastPublished.Text = "";
                ltrlLastReviewed.Text = "";
                ltrlPetitionNo.Text = "";
            }
            this.mdpAuditTrail.Show();
        }
    }


    protected void GvAdd_Details_RowDataBound(object senter, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            System.Web.UI.WebControls.Image img = e.Row.FindControl("imgedit") as System.Web.UI.WebControls.Image;
            System.Web.UI.WebControls.Image img1 = e.Row.FindControl("emgnotedit") as System.Web.UI.WebControls.Image;
            ImageButton imgDelete = (ImageButton)e.Row.FindControl("BtnDelete");

            ImageButton BtnRestore = (ImageButton)e.Row.FindControl("BtnRestore");

          objWhatNewsOB.TenderId = Convert.ToInt32( GvAdd_Details.DataKeys[e.Row.RowIndex].Value);

            p_Var.dSet = objwn.News_BL_get_AprovedEdit(objWhatNewsOB);
            if (p_Var.dSet.Tables.Count>0)
            {

                for (int i = 0; i < p_Var.dSet.Tables[0].Rows.Count; i++)
                {
                    if (p_Var.dSet.Tables[0].Rows[i]["News_ID"] != DBNull.Value)
                    {

                        if (Convert.ToInt32(GvAdd_Details.DataKeys[e.Row.RowIndex].Value) == Convert.ToInt32(p_Var.dSet.Tables[0].Rows[i]["News_ID"]))
                        {

                            img.Visible = false;
                            img1.Visible = true;
                            img1.Height = 10;
                            img1.Width = 10;


                        }
                        else
                        {
                            img1.Visible = false;
                            img.Visible = true;

                        }
                    }
                }
            }

            object objTemp = GvAdd_Details.DataKeys[e.Row.RowIndex].Value as object;
            if (objTemp != null)
            {
              objWhatNewsOB.TenderId = Convert.ToInt32(objTemp.ToString());

            }
            Repeater rptFN = (Repeater)e.Row.FindControl("Rpt_FilePdf");

          objWhatNewsOB.ActionType = 1;
          objWhatNewsOB.ModuleID = 14;
          objWhatNewsOB.Status = Convert.ToInt32(ddlStatus.SelectedValue);



          if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.Delete))
          {

              imgDelete.Attributes.Add("onclick", "javascript:return " +
              "confirm('Are you sure you want to purge record no- " + DataBinder.Eval(e.Row.DataItem, "News_ID") + "')");

              BtnRestore.Attributes.Add("onclick", "javascript:return " +
             "confirm('Are you sure you want to restore record no- " + DataBinder.Eval(e.Row.DataItem, "News_ID") + "')");

          }
          else
          {

              imgDelete.Attributes.Add("onclick", "javascript:return " +
              "confirm('Are you sure you want to delete record no- " + DataBinder.Eval(e.Row.DataItem, "News_ID") + "')");

              BtnRestore.Attributes.Add("onclick", "javascript:return " +
             "confirm('Are you sure you want to restore record no- " + DataBinder.Eval(e.Row.DataItem, "News_ID") + "')");
          }
         

        }
                
            
    }
   

    protected void txtSearch_TextChanged(object senter, EventArgs e)
    {

    }


	 public void chk_privilages()
    {
        //DataSet dsprv = new DataSet();

        string url1 = Request.Url.ToString();

        obj_userOB.RoleId = Convert.ToInt32(Session["Role_Id"]);
        p_Var.dSet = obj_RoleBL.ASP_CheckPrivilagesALL_For_Master(obj_userOB);
        int id = (from DataRow dr in p_Var.dSet.Tables[0].Rows
                  where (int)dr["Module_Id"] == Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Whats_New)
                  select (int)dr["Module_Id"]).FirstOrDefault();
        if (p_Var.dSet.Tables[0].Rows.Count > 0)
        {

            if (id != Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Whats_New))
            {
                Session.Abandon();
                Response.Redirect("~/Auth/AdminPanel/Login.aspx");
            }

        }
    }
     protected void BtnForReview_Click(object senter, EventArgs e)
     {

         pnlPopUpEmails.Visible = true;
         bindCheckBoxListWithEmailIDs();
         pnlGrid.Visible = false;
        
     }
     protected void btnSendEmails_Click(object sender, EventArgs e)
     {
         try
         {
             StringBuilder sbuilder = new StringBuilder();
             if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.draft))
             {
                 sbuilder.Remove(0, p_Var.sbuildertmp.Length);
                 foreach (GridViewRow row in GvAdd_Details.Rows)
                 {
                     CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                     Label lblMenuName = (Label)row.FindControl("lblMenuName");
                     if ((selCheck.Checked == true))
                     {
                         sbuilder.Append(lblMenuName.Text + "; ");
                         p_Var.dataKeyID = Convert.ToInt32(GvAdd_Details.DataKeys[row.RowIndex].Value);
                         objWhatNewsOB.TenderId = p_Var.dataKeyID;
                         objWhatNewsOB.ApproveStatus = (int)Module_ID_Enum.Module_Permission_ID.review;
                         objWhatNewsOB.Status = Convert.ToInt32(ddlStatus.SelectedValue);
                         objWhatNewsOB.UserId = Convert.ToInt32(Session["User_Id"]);
                         objWhatNewsOB.IpAddress = Miscelleneous_DL.getclientIP();
                         p_Var.Result = objwn.ASP_ChangeStatus_TempNews(objWhatNewsOB);
                     }
                 }
                 if (p_Var.Result > 0)
                 {

                     p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                     foreach (System.Web.UI.WebControls.ListItem li in chkSendEmails.Items)
                     {

                         if (li.Selected == true)
                         {
                             p_Var.sbuildertmp.Append(li.Value + ";");

                         }

                     }
                     if (p_Var.sbuildertmp.ToString().EndsWith(";"))
                     {
                         p_Var.sbuilder.Append("You have what's new for review : " + sbuilder.ToString().Remove(sbuilder.Length - 1));
                         //p_Var.sbuildertmp.Append(p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1));
                         string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                         p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                         p_Var.sbuildertmp.Append(email);
                         miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "Email For Review", Session["Email"].ToString(), p_Var.sbuilder.ToString());

                     }

                     Session["msg"] = "What's new record has been sent for review successfully.";
                     Session["Redirect"] = "~/Auth/AdminPanel/WhatsNew/View_News.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                     Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                 }
             }

             else
             {
                 sbuilder.Remove(0, p_Var.sbuildertmp.Length);
                 foreach (GridViewRow row in GvAdd_Details.Rows)
                 {
                     CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                     Label lblMenuName = (Label)row.FindControl("lblMenuName");
                     if ((selCheck.Checked == true))
                     {
                         sbuilder.Append(lblMenuName.Text + "; ");
                         p_Var.dataKeyID = Convert.ToInt32(GvAdd_Details.DataKeys[row.RowIndex].Value);
                         objWhatNewsOB.TenderId = p_Var.dataKeyID;
                         objWhatNewsOB.ApproveStatus = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                         objWhatNewsOB.Status = Convert.ToInt32(ddlStatus.SelectedValue);
                         objWhatNewsOB.UserId = Convert.ToInt32(Session["User_Id"]);
                         objWhatNewsOB.IpAddress = Miscelleneous_DL.getclientIP();

                         p_Var.Result = objwn.ASP_ChangeStatus_TempNews(objWhatNewsOB);


                     }
                 }
                 if (p_Var.Result > 0)
                 {
                     p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                     foreach (System.Web.UI.WebControls.ListItem li in chkSendEmails.Items)
                     {

                         if (li.Selected == true)
                         {
                             p_Var.sbuildertmp.Append(li.Value + ";");

                         }

                     }
                     if (p_Var.sbuildertmp.ToString().EndsWith(";"))
                     {
                         p_Var.sbuilder.Append("You have what's new for publish : " + sbuilder.ToString().Remove(sbuilder.Length - 1));
                         //p_Var.sbuildertmp.Append(p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1));
                         string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                         p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                         p_Var.sbuildertmp.Append(email);
                         miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "Email For Publish", Session["Email"].ToString(), p_Var.sbuilder.ToString());
                     }

                     Session["msg"] = "What's new record has been sent for publish successfully.";
                     Session["Redirect"] = "~/Auth/AdminPanel/WhatsNew/View_News.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                     Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");


                 }
                
             }
            
         }
         catch
         {
             throw;
         }
     }
     protected void btnSendEmailsWithoutEmails_Click(object sender, EventArgs e)
     {
         try
         {
             if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.draft))
             {
                 foreach (GridViewRow row in GvAdd_Details.Rows)
                 {
                     CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                     if ((selCheck.Checked == true))
                     {
                         p_Var.dataKeyID = Convert.ToInt32(GvAdd_Details.DataKeys[row.RowIndex].Value);
                         objWhatNewsOB.TenderId = p_Var.dataKeyID;
                         objWhatNewsOB.ApproveStatus = (int)Module_ID_Enum.Module_Permission_ID.review;
                         objWhatNewsOB.Status = Convert.ToInt32(ddlStatus.SelectedValue);
                         objWhatNewsOB.UserId = Convert.ToInt32(Session["User_Id"]);
                         
                         objWhatNewsOB.IpAddress = Miscelleneous_DL.getclientIP();
                         p_Var.Result = objwn.ASP_ChangeStatus_TempNews(objWhatNewsOB);
                     }
                 }
                 if (p_Var.Result > 0)
                 {
                     Session["msg"] = "What's new record has been sent for review successfully.";
                     Session["Redirect"] = "~/Auth/AdminPanel/WhatsNew/View_News.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                     Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");
                 }
             }
             else
             {
                 foreach (GridViewRow row in GvAdd_Details.Rows)
                 {
                     CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                     if ((selCheck.Checked == true))
                     {
                         p_Var.dataKeyID = Convert.ToInt32(GvAdd_Details.DataKeys[row.RowIndex].Value);
                         objWhatNewsOB.TenderId = p_Var.dataKeyID;
                         objWhatNewsOB.ApproveStatus = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                         //objWhatNewsOB.Status = Convert.ToInt32(ddlStatus.SelectedValue);
                         objWhatNewsOB.Status = Convert.ToInt32(ddlStatus.SelectedValue);
                         objWhatNewsOB.UserId = Convert.ToInt32(Session["User_Id"]);
                         objWhatNewsOB.IpAddress = Miscelleneous_DL.getclientIP();

                         p_Var.Result = objwn.ASP_ChangeStatus_TempNews(objWhatNewsOB);


                     }
                 }
                 if (p_Var.Result > 0)
                 {
                     Session["msg"] = "What's new record has been sent for publish successfully.";
                     Session["Redirect"] = "~/Auth/AdminPanel/WhatsNew/View_News.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                     Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");


                 }

             }
            
         }
         catch
         {
             throw;
         }
     }
     protected void btnCancelEmail_Click(object sender, EventArgs e)
     {
         pnlGrid.Visible = true;
         pnlPopUpEmails.Visible = false;
     }
     protected void btnSendEmailsDis_Click(object sender, EventArgs e)
     {
         try
         {
             StringBuilder sbuilder = new StringBuilder();
             foreach (GridViewRow row in GvAdd_Details.Rows)
             {
                 CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                 Label lblMenuName = (Label)row.FindControl("LnkTitle");
                 if ((selCheck.Checked == true))
                 {
                     p_Var.dataKeyID = Convert.ToInt32(GvAdd_Details.DataKeys[row.RowIndex].Value);
                     objWhatNewsOB.TenderId = p_Var.dataKeyID;
                     sbuilder.Append(lblMenuName.Text + "; ");
                     if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review))
                     {
                         objWhatNewsOB.ApproveStatus = (int)Module_ID_Enum.Module_Permission_ID.draft;
                     }
                     else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
                     {
                         objWhatNewsOB.ApproveStatus = (int)Module_ID_Enum.Module_Permission_ID.review;
                     }
                     else
                     {
                         objWhatNewsOB.ApproveStatus = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                     }

                     //objWhatNewsOB.ApproveStatus = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                     objWhatNewsOB.Status = Convert.ToInt32(ddlStatus.SelectedValue);
                     objWhatNewsOB.UserId = Convert.ToInt32(Session["User_Id"]);
                     objWhatNewsOB.IpAddress = miscellBL.IpAddress();

                     p_Var.Result = objwn.ASP_ChangeStatus_TempNews(objWhatNewsOB);


                 }
             }
             if (p_Var.Result > 0)
             {
                 p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                 foreach (System.Web.UI.WebControls.ListItem li in chkSendEmailsDis.Items)
                 {

                     if (li.Selected == true)
                     {
                         p_Var.sbuildertmp.Append(li.Value + ";");

                     }

                 }
                 if (p_Var.sbuildertmp.ToString().EndsWith(";"))
                 {
                     p_Var.sbuilder.Append("What's new are disapproved : " + sbuilder.ToString().Remove(sbuilder.Length - 1));
                     //p_Var.sbuildertmp.Append(p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1));
                     string email = p_Var.sbuildertmp.ToString().Substring(0, p_Var.sbuildertmp.ToString().Length - 1);
                     p_Var.sbuildertmp.Remove(0, p_Var.sbuildertmp.Length);
                     p_Var.sbuildertmp.Append(email);
                     miscellBL.SendEmail("", "", "no-reply.herc@nic.in;" + p_Var.sbuildertmp.ToString().Trim(), "Email For disapprove", Session["Email"].ToString(), p_Var.sbuilder.ToString());

                 }

                 Session["msg"] = "What's new record has been disapproved successfully.";
                 Session["Redirect"] = "~/Auth/AdminPanel/WhatsNew/View_News.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                 Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");


             }
             
         }
         catch
         {
             throw;
         }
     }
     protected void btnSendEmailsWithoutEmailsDis_Click(object sender, EventArgs e)
     {
         try
         {

             foreach (GridViewRow row in GvAdd_Details.Rows)
             {
                 CheckBox selCheck = (CheckBox)row.FindControl("chkSelect");
                 if ((selCheck.Checked == true))
                 {
                     p_Var.dataKeyID = Convert.ToInt32(GvAdd_Details.DataKeys[row.RowIndex].Value);
                     objWhatNewsOB.TenderId = p_Var.dataKeyID;
                     if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.review))
                     {
                         objWhatNewsOB.ApproveStatus = (int)Module_ID_Enum.Module_Permission_ID.draft;
                     }
                     else if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(Module_ID_Enum.Module_Permission_ID.ForApprover))
                     {
                         objWhatNewsOB.ApproveStatus = (int)Module_ID_Enum.Module_Permission_ID.review;
                     }
                     else
                     {
                         objWhatNewsOB.ApproveStatus = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                     }

                     //objWhatNewsOB.ApproveStatus = (int)Module_ID_Enum.Module_Permission_ID.ForApprover;
                     objWhatNewsOB.Status = Convert.ToInt32(ddlStatus.SelectedValue);
                     objWhatNewsOB.UserId = Convert.ToInt32(Session["User_Id"]);
                     objWhatNewsOB.IpAddress = miscellBL.IpAddress();

                     p_Var.Result = objwn.ASP_ChangeStatus_TempNews(objWhatNewsOB);


                 }
             }
             if (p_Var.Result > 0)
             {
                 Session["msg"] = "What's new record has been disapproved successfully.";
                 Session["Redirect"] = "~/Auth/AdminPanel/WhatsNew/View_News.aspx?ModuleID=" + Request.QueryString["ModuleID"];
                 Response.Redirect("~/Auth/AdminPanel/ConfirmationPage.aspx");


             }
            
         }
         catch
         {
             throw;
         }
     }
     protected void btnCancelEmailDis_Click(object sender, EventArgs e)
     {
                 pnlGrid.Visible = true;
        pnlPopUpEmailsDis.Visible = false;
     }

     #region Function to bind emailid of reviewers status in checkboxlist

     public void bindCheckBoxListWithEmailIDs()
     {

         try
         {
             obj_userOB.DepttId = Convert.ToInt32(Module_ID_Enum.hercType.herc);
             obj_userOB.ModuleId = Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Whats_New);
             p_Var.dSetCompare = obj_UserBL.getReviewEmailIds(obj_userOB);
             lblSelectors.Text = "Select the Reviewer(s)";
             lblSelectors.Font.Bold = true;
             chkSendEmails.DataSource = p_Var.dSetCompare;
             chkSendEmails.DataTextField = "newEmail";
             chkSendEmails.DataValueField = "email";
             chkSendEmails.DataBind();

         }
         catch
         {
             throw;
         }
     }

     #endregion

     #region Function to bind emailid of reviewers status in checkboxlist

     public void bindCheckBoxListWithApproverEmailIDs()
     {

         try
         {
             lblSelectors.Text = "Select the Publisher(s)";
             lblSelectors.Font.Bold = true;
             obj_userOB.DepttId = Convert.ToInt32(Module_ID_Enum.hercType.herc);
             obj_userOB.ModuleId = Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Whats_New);
             p_Var.dSetCompare = obj_UserBL.getPublisherEmailIds(obj_userOB);

             chkSendEmails.DataSource = p_Var.dSetCompare;
             chkSendEmails.DataTextField = "newEmail";
             chkSendEmails.DataValueField = "email";
             chkSendEmails.DataBind();

         }
         catch
         {
             throw;
         }
     }

     #endregion

     #region Function to bind emailid of reviewers status in checkboxlist

     public void bindCheckBoxListWithEmailIDDiaapproves()
     {

         try
         {
             obj_userOB.DepttId = Convert.ToInt32(Module_ID_Enum.hercType.herc);
             obj_userOB.ModuleId = Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Whats_New);
             p_Var.dSetCompare = obj_UserBL.getReviewEmailIds(obj_userOB);
             lblSelectorsDis.Text = "Select the Reviewer(s)";
             lblSelectorsDis.Font.Bold = true;
             chkSendEmailsDis.DataSource = p_Var.dSetCompare;
             chkSendEmailsDis.DataTextField = "newEmail";
             chkSendEmailsDis.DataValueField = "email";
             chkSendEmailsDis.DataBind();

         }
         catch
         {
             throw;
         }
     }

     #endregion

     #region Function to bind emailid of Data entry operator status in checkboxlist

     public void bindCheckBoxListWithDataEntryEmailIDs()
     {

         try
         {
             lblSelectorsDis.Text = "Select the Data Entry Personnel";
             lblSelectorsDis.Font.Bold = true;
             obj_userOB.DepttId = Convert.ToInt32(Module_ID_Enum.hercType.herc);
             obj_userOB.ModuleId = Convert.ToInt32(Module_ID_Enum.Project_Module_ID.Whats_New);
             p_Var.dSetCompare = obj_UserBL.getDataEntryEmailIds(obj_userOB);

             chkSendEmailsDis.DataSource = p_Var.dSetCompare;
             chkSendEmailsDis.DataTextField = "newEmail";
             chkSendEmailsDis.DataValueField = "email";
             chkSendEmailsDis.DataBind();

         }
         catch
         {
             throw;
         }
     }

     #endregion
}
