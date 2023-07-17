using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using NCM.DAL;

public class OrderDL
{
    #region Default constructors zone

    public OrderDL()
	{

    }

    #endregion

    //Area for all the variables declaration zone

    NCMdbAccess ncmdbObject = new NCMdbAccess();
    Project_Variables p_var = new Project_Variables();

    //End

    //Area for all the user defined functions to display contents

    #region Function to get order type

    public DataSet getOrderType()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("Get_OrderType");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion



    #region Function to get order type for display

    public DataSet getOrderTypeDisplay()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("Get_OrderTypeDisplay");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to get order categories

    public DataSet getOrderCategories(PetitionOB orderObject)
    {
        try
        {
            ncmdbObject.AddParameter("@Order_Type_ID", orderObject.OrderTypeID);
            return ncmdbObject.ExecuteDataSet("Get_OrderTypeCategory");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function To Display orders with paging

    public DataSet DisplayOrdersWithPaging(PetitionOB orderObject, out int catValue)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.AddParameter("@Lang_Id", orderObject.LangId);
            ncmdbObject.AddParameter("@status_id", orderObject.StatusId);
            ncmdbObject.AddParameter("@module_Id", orderObject.ModuleID);
            ncmdbObject.AddParameter("@PageIndex", orderObject.PageIndex);
            ncmdbObject.AddParameter("@PageSize", orderObject.PageSize);
            ncmdbObject.AddParameter("@Year",orderObject.year);
            ncmdbObject.AddParameter("@OrderType",orderObject.OrderTypeID);
            ncmdbObject.AddParameter("@OrderCategory",orderObject.OrderCatID);
            p_var.dSet = ncmdbObject.ExecuteDataSet("ASP_Tmp_Orders_Display");
            catValue = Convert.ToInt16(param[0].Value);

            return p_var.dSet;
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion 

    #region Function to get Temp orders record for edit

    public DataSet getOrdersRecordForEdit(PetitionOB orderObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@TempOrderID", orderObject.TempOrderID));
            ncmdbObject.Parameters.Add(new SqlParameter("@status_id", orderObject.StatusId));
            return ncmdbObject.ExecuteDataSet("ASP_Tmp_Orders_DisplayForEdit");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to get Petition Review numbers for orders

    public DataSet getPetitionReviewNumber()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("ASP_GetApprovedReview_PetitionForOrders");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }

    }

    #endregion

    #region Function to get Petition Appeal numbers for orders

    public DataSet getPetitionAppealNumber()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("ASP_GetApprovedAppeal_PetitionForOrders");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }

    }

    #endregion

    #region Function to get Order ID from temp table for comparision

    public DataSet OrderIDforComparision(PetitionOB orderObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@OrderID", orderObject.OrderID));
            return ncmdbObject.ExecuteDataSet("ASP_Web_Order_OrderID");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region function to bind year 

    public DataSet Get_YearOrder(PetitionOB objpetOB)
    {
        try
        {
            ncmdbObject.AddParameter("@OrderTypeId", objpetOB.OrderTypeID);
            return ncmdbObject.ExecuteDataSet("USP_GetYearOrders");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion 

    //Get daily Orders

    #region function to get current Order information
    public DataSet Get_Order(PetitionOB obj_petOB, out int catValue)
    {

        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.AddParameter("@year", obj_petOB.year);
            ncmdbObject.AddParameter("@PageIndex", obj_petOB.PageIndex);
            ncmdbObject.AddParameter("@PageSize", obj_petOB.PageSize);
            p_var.dSet = ncmdbObject.ExecuteDataSet("USP_GetOrders");
            catValue = Convert.ToInt16(param[0].Value);
            return p_var.dSet;
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }
    #endregion 

    #region function to get Previous Order information
    public DataSet Get_OrderPrevious(PetitionOB obj_petOB, out int catValue)
    {

        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.AddParameter("@year", obj_petOB.year);
            ncmdbObject.AddParameter("@PageIndex", obj_petOB.PageIndex);
            ncmdbObject.AddParameter("@PageSize", obj_petOB.PageSize);
            p_var.dSet = ncmdbObject.ExecuteDataSet("USP_GetOrdersPrevoius");
            catValue = Convert.ToInt16(param[0].Value);
            return p_var.dSet;
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }
    #endregion 

    #region function to get Orders Search information
    public DataSet Get_SearchOrders(PetitionOB obj_petOB, out int catValue)
    {

        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.AddParameter("@PageIndex", obj_petOB.PageIndex);
            ncmdbObject.AddParameter("@PageSize", obj_petOB.PageSize);
            ncmdbObject.AddParameter("@ActionType", obj_petOB.ActionType);
            ncmdbObject.AddParameter("@PetitionerName", obj_petOB.PetitionerName);
            ncmdbObject.AddParameter("@RespondentName", obj_petOB.RespondentName);
            ncmdbObject.AddParameter("@Subject", obj_petOB.subject);
            ncmdbObject.AddParameter("@Year", obj_petOB.year);
            ncmdbObject.AddParameter("@Date", obj_petOB.Date);
            ncmdbObject.AddParameter("@PetitionNumber", obj_petOB.PRONo);
            ncmdbObject.AddParameter("@ConnectionType", obj_petOB.ConnectionType);

            p_var.dSet = ncmdbObject.ExecuteDataSet("USP_Get_SearchOrders");
            catValue = Convert.ToInt16(param[0].Value);
            return p_var.dSet;
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }
    #endregion

    #region function to get repealed Order information
    public DataSet Get_OrderRepealed(PetitionOB obj_petOB, out int catValue)
    {

        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.AddParameter("@year", obj_petOB.year);
            ncmdbObject.AddParameter("@PageIndex", obj_petOB.PageIndex);
            ncmdbObject.AddParameter("@PageSize", obj_petOB.PageSize);
            p_var.dSet = ncmdbObject.ExecuteDataSet("USP_GetRepealedOrders");
            catValue = Convert.ToInt16(param[0].Value);
            return p_var.dSet;
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }
    #endregion 

    //End


    //Get order pronounced

    #region function to get current Order information

    public DataSet Get_CurrentOrder(PetitionOB obj_petOB, out int catValue)
    {

        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.AddParameter("@year", obj_petOB.year);
            ncmdbObject.AddParameter("@PageIndex", obj_petOB.PageIndex);
            ncmdbObject.AddParameter("@PageSize", obj_petOB.PageSize);
            p_var.dSet = ncmdbObject.ExecuteDataSet("USP_GetCurrentOrders");
            catValue = Convert.ToInt16(param[0].Value);
            return p_var.dSet;
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion 

    #region function to get Previous Order information
    public DataSet Get_CurrentOrderPrevious(PetitionOB obj_petOB, out int catValue)
    {

        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.AddParameter("@year", obj_petOB.year);
            ncmdbObject.AddParameter("@PageIndex", obj_petOB.PageIndex);
            ncmdbObject.AddParameter("@PageSize", obj_petOB.PageSize);
            p_var.dSet = ncmdbObject.ExecuteDataSet("USP_GetCurrentOrdersPrevoius");
            catValue = Convert.ToInt16(param[0].Value);
            return p_var.dSet;
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }
    #endregion 

    #region function to get Orders Search information
    public DataSet Get_CurrentSearchOrders(PetitionOB obj_petOB, out int catValue)
    {

        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.AddParameter("@PageIndex", obj_petOB.PageIndex);
            ncmdbObject.AddParameter("@PageSize", obj_petOB.PageSize);
            ncmdbObject.AddParameter("@ActionType", obj_petOB.ActionType);
            ncmdbObject.AddParameter("@ConnectionType", obj_petOB.ConnectionType);
            ncmdbObject.AddParameter("@PetitionerName", obj_petOB.PetitionerName);
            ncmdbObject.AddParameter("@RespondentName", obj_petOB.RespondentName);
            ncmdbObject.AddParameter("@Subject", obj_petOB.subject);
            ncmdbObject.AddParameter("@Year", obj_petOB.year);
            ncmdbObject.AddParameter("@Date", obj_petOB.Date);
            ncmdbObject.AddParameter("@PetitionNumber", obj_petOB.PRONo);
            p_var.dSet= ncmdbObject.ExecuteDataSet("USP_Get_CurrentSearchOrders");
            catValue = Convert.ToInt16(param[0].Value);
            return p_var.dSet;
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }
    #endregion

    #region function to get categoryWise Order information

    public DataSet Get_OrderCategoryWise(PetitionOB obj_petOB, out int catValue)
    {

        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.AddParameter("@CategoryId", obj_petOB.OrderCatID);
            ncmdbObject.AddParameter("@PageIndex", obj_petOB.PageIndex);
            ncmdbObject.AddParameter("@PageSize", obj_petOB.PageSize);
            p_var.dSet = ncmdbObject.ExecuteDataSet("USP_GetOrdersCategoryWise");
            catValue = Convert.ToInt16(param[0].Value);
            return p_var.dSet;
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion 

    #region function to get PFD categoryWise

    public DataSet Get_pdf(PetitionOB objPetOB)
    {
        try
        {
            ncmdbObject.AddParameter("@Orderid",objPetOB.OrderID);
            return ncmdbObject.ExecuteDataSet("USP_GetPdfcategoryWise");
        }
        catch
        {

            throw;
        }
    }
    #endregion 

    //End

    #region Function to get OrderSubcategory for orders

    public DataSet getOrderSubcategory(PetitionOB objPetOB)
    {
        try
        {
            ncmdbObject.AddParameter("@orderTypeId",objPetOB.OrderTypeID);

            return ncmdbObject.ExecuteDataSet("ASP_GetOrderSubcategory");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }

    }

    #endregion

    #region Function to get Orders numbers for connections

    public DataSet getOrderNumberForConnection()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("ASP_GetApprovedOrdersNumberConnected");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }

    }

    #endregion

    #region function to Get Fileaname from Connected orders 

    public DataSet Get_ConnectedFileName(PetitionOB petOB)
    {
        try
        {
            ncmdbObject.AddParameter("@SubCategoryID", petOB.OrderSubcategory);
            return ncmdbObject.ExecuteDataSet("ASP_GetFile");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }
        
        
    #endregion 

    //3 Dec 2012

    #region Function to get connected Orders(For Edit)

    public DataSet get_ConnectedOrders_Edit(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@OrderID", petObject.OrderID));

            return ncmdbObject.ExecuteDataSet("ASP_GetConnectedOrders");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region function to bind year
    public DataSet GetYear()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("ASP_GetYear");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion 

    //End

    //Area for all the user defined function to insert and update

    #region Function to insert-update temp order table records

    public Int32 insertUpdateOrderTemp(PetitionOB orderObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.AddParameter("@ActionType",orderObject.ActionType);
            ncmdbObject.AddParameter("@TempOrderID", orderObject.TempOrderID);
            ncmdbObject.AddParameter("@OrderID", orderObject.OrderID);
            ncmdbObject.AddParameter("@Petition_ID", orderObject.PetitionId);
            ncmdbObject.AddParameter("@ReviewPetition_ID", orderObject.RPId);
            ncmdbObject.AddParameter("@AppealPetition_ID", orderObject.AppealId);

            ncmdbObject.AddParameter("@Status_Id", orderObject.StatusId);
            ncmdbObject.AddParameter("@OrderTitle", orderObject.OrderTitle);
            ncmdbObject.AddParameter("@OrderDescription", orderObject.OrderDescription);
            ncmdbObject.AddParameter("@OrderDate", orderObject.OrderDate);
            ncmdbObject.AddParameter("@Petition_ReviewPetitionYear", orderObject.petitionYear);
            ncmdbObject.AddParameter("@OrderFile", orderObject.OrderFile);
            ncmdbObject.AddParameter("@OrderCatID", orderObject.OrderCatID);
            ncmdbObject.AddParameter("@OrderTypeID", orderObject.OrderTypeID);

            ncmdbObject.AddParameter("@ModuleId", orderObject.ModuleID);
            ncmdbObject.AddParameter("@InsertedBy", orderObject.InsertedBy);
            ncmdbObject.AddParameter("@UpdatedBy", orderObject.LastUpdatedBy);
            ncmdbObject.AddParameter("@InsertedDate", orderObject.RecInsertDate);
            ncmdbObject.AddParameter("@Lang_Id", orderObject.LangId);

            ncmdbObject.AddParameter("@PlaceholderOne", orderObject.StartDate.ToString());
            ncmdbObject.AddParameter("@PlaceholderTwo", orderObject.EndDate.ToString());
            ncmdbObject.AddParameter("@PlaceholderThree", orderObject.OrderSubcategory);
            ncmdbObject.AddParameter("@PlaceholderFour", orderObject.year);
            ncmdbObject.AddParameter("@PlaceholderFive", orderObject.PlaceHolderFive);
            ncmdbObject.AddParameter("@ConnectionID", orderObject.ConnectionType);

            ncmdbObject.Parameters.Add(new SqlParameter("@Petitioner_Name", orderObject.PetitionerName));
            //ncmdbObject.Parameters.Add(new SqlParameter("@Petitioner_Contact_No", petObject.PetitionerContactNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@Petitioner_Email", orderObject.PetitionerEmail));

            ncmdbObject.Parameters.Add(new SqlParameter("@Petitioner_Mobile_No", orderObject.PetitionerMobileNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@Petitioner_Phone_No", orderObject.PetitionerPhoneNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@Petitioner_Fax_No", orderObject.PetitionerFaxNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@Petitioner_Address", orderObject.PetitionerAddress));
            ncmdbObject.Parameters.Add(new SqlParameter("@Respondent_Name", orderObject.RespondentName));
            ncmdbObject.Parameters.Add(new SqlParameter("@Respondent_Mobile_No", orderObject.RespondentMobileNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@Respondent_Phone_No", orderObject.RespondentPhone_No));
            ncmdbObject.Parameters.Add(new SqlParameter("@Respondent_Fax_No", orderObject.RespondentFaxNo));
            ncmdbObject.Parameters.Add(new SqlParameter("@Respondent_Email", orderObject.Respondentmail));
            ncmdbObject.Parameters.Add(new SqlParameter("@Respondent_Address", orderObject.RespondentAddress));
            ncmdbObject.AddParameter("@MetaKeyWords", orderObject.MetaKeyWords);
            ncmdbObject.AddParameter("@MetaDescription", orderObject.MetaDescription);
            ncmdbObject.AddParameter("@MetaTitle", orderObject.MetaTitle);
            ncmdbObject.AddParameter("@MetaLanguage", orderObject.MetaKeyLanguage);
            ncmdbObject.AddParameter("@IPAddress", orderObject.IpAddress);
            ncmdbObject.ExecuteNonQuery("ASP_Tmp_Order_Insert_Update");
            p_var.Result = Convert.ToInt32(param[0].Value);
            return p_var.Result;

        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to update the order status

    public Int32 OrdersUpdateStatus(PetitionOB orderObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@Status_Id", orderObject.StatusId);
            ncmdbObject.AddParameter("@TempOrderID", orderObject.TempOrderID);
            ncmdbObject.AddParameter("@UserID", orderObject.userID);
            ncmdbObject.AddParameter("@IPAddress", orderObject.IpAddress);
            ncmdbObject.ExecuteNonQuery("ASP_Temp_Order_Change_status");
            p_var.Result = Convert.ToInt32(param[0].Value);
            return p_var.Result;
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion 

    #region Function To insert Orders into web final table

    public Int32 InsertOrdersIntoWeb(PetitionOB orderObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@TempOrderID", orderObject.TempOrderID);
            ncmdbObject.AddParameter("@UserID", orderObject.userID);
            ncmdbObject.AddParameter("@IPAddress", orderObject.IpAddress);
            ncmdbObject.ExecuteNonQuery("ASP_Web_Orders_Insert_Update");
            p_var.Result = Convert.ToInt32(param[0].Value);
            return p_var.Result;
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion 

    #region Function to insert ConnectedOrders

    public Int32 insertConnectedOrders(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@ActionType", Convert.ToInt16(Module_ID_Enum.Project_Action_Type.insert)));
            ncmdbObject.Parameters.Add(new SqlParameter("@FileName", petObject.FileName));
            ncmdbObject.Parameters.Add(new SqlParameter("@SubCategoryID", petObject.OrderSubcategory));
            ncmdbObject.Parameters.Add(new SqlParameter("@Status_Id", petObject.StatusId));
            ncmdbObject.AddParameter("@connectionOrder_ID",petObject.ConnectionID);
            ncmdbObject.AddParameter("@OrderId",petObject.OrderID);
            ncmdbObject.AddParameter("@CategoryID", petObject.OrderCatID);
            ncmdbObject.AddParameter("@Remarks", petObject.Remarks);
            // 29 jan 2013
            ncmdbObject.AddParameter("@OrderType",petObject.OrderType);
            ncmdbObject.AddParameter("@Date", petObject.OrderDate);
            ncmdbObject.AddParameter("@Petition_ID", petObject.PetitionId);
            ncmdbObject.AddParameter("@ReviewPetition_ID", petObject.RPId);
            ncmdbObject.AddParameter("@AppealPetition_ID", petObject.AppealId);
            ncmdbObject.ExecuteNonQuery("ASP_Connected_Orders_Insert_Update");
            p_var.Result = Convert.ToInt32(param[0].Value);
            return p_var.Result;
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to update the order status for repealed

    public Int32 OrdersUpdateStatus_repealed(PetitionOB orderObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@Status_Id", orderObject.StatusId);
            ncmdbObject.AddParameter("@TempOrderID", orderObject.TempOrderID);
            ncmdbObject.ExecuteNonQuery("ASP_Order_Change_statusRepealed");
            p_var.Result = Convert.ToInt32(param[0].Value);
            return p_var.Result;
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion 

    //End

    #region function to get year of Orders

    public DataSet GetYearOrders()
    {
        try
        {
            
            return ncmdbObject.ExecuteDataSet("ASP_GetYearOrders_Admin");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region function to bind year of order for petition/review/Appeal
    public DataSet GetYearAppeal(PetitionOB orderObject)
    {
        try
        {
            ncmdbObject.AddParameter("@ConnectionType",orderObject.TempRPId);
            return ncmdbObject.ExecuteDataSet("ASP_GetYearOrder");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion 

    #region Function to get Petition numbers for orders

    public DataSet getPetitionNumberForOrders(PetitionOB objpetOB)
    {
        try
        {
            ncmdbObject.AddParameter("@ConnectionType", objpetOB.ConnectionType);
            ncmdbObject.AddParameter("@Year", objpetOB.year);
            return ncmdbObject.ExecuteDataSet("ASP_GetApprovedPetitionForOrders");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }

    }

    #endregion

    #region Function to Update petition Review from temp table for comparision

    public Int32 updateReview(PetitionOB orderObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@TempOrderID", orderObject.TempOrderID));
            return ncmdbObject.ExecuteNonQuery("updateStatusByOrder");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to insert into File_order 
    public Int32 insert_intoFileOrder(PetitionOB orderObject)
    {
        try
        {
            ncmdbObject.AddParameter("@OrderType",orderObject.OrderTypeID);
            ncmdbObject.AddParameter("@FileName",orderObject.FileName);
            ncmdbObject.AddParameter("@Date", orderObject.Date);
            ncmdbObject.AddParameter("@OrderId",orderObject.OrderID);
            ncmdbObject.AddParameter("@Status_Id",orderObject.StatusId);
            //ncmdbObject.AddParameter("@Petition_ID", orderObject.PetitionId);
            //ncmdbObject.AddParameter("@ReviewPetition_ID", orderObject.RPId);
            //ncmdbObject.AddParameter("@AppealPetition_ID", orderObject.AppealId);
            return ncmdbObject.ExecuteNonQuery("ASP_Insert_FileOrder");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to get FileName for orders

    public DataSet getFileNameForOrders(PetitionOB orderObject)
    {
        try
        {
            ncmdbObject.AddParameter("@OrderId",orderObject.OrderID);
            //ncmdbObject.AddParameter("@Status",orderObject.Status);
            return ncmdbObject.ExecuteDataSet("ASP_GetFileName");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }

    }

    #endregion

    #region Function to Update status for Files in File_orders

    public Int32 UpdateFileStatusForOrders(PetitionOB orderObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@Id", orderObject.ConnectionID);
            ncmdbObject.ExecuteNonQuery("ASP_Update_FileOrderStatus");
            p_var.Result = Convert.ToInt32(param[0].Value);
            return p_var.Result;
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }

    }

    #endregion

    #region function for orderunderAppeal

    public DataSet OrderunderAppeal(PetitionOB obj_petOB, out int catValue)
    {
        try
        {

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
          
            ncmdbObject.AddParameter("@PageIndex", obj_petOB.PageIndex);
            ncmdbObject.AddParameter("@PageSize", obj_petOB.PageSize);
            ncmdbObject.AddParameter("@year", obj_petOB.year);
            p_var.dSet = ncmdbObject.ExecuteDataSet("USP_Get_OrderUnderAppeal");
            catValue = Convert.ToInt16(param[0].Value);
            return p_var.dSet;

        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to get connected orders during deleting record from ConnectedOrder table

    public DataSet get_ConnectedOrderDelete(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@order_Id", petObject.OrderID));
            return ncmdbObject.ExecuteDataSet("ASP_Get_ConnectedOrderNumber");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to update the order status for delete

    public Int32 OrdersUpdateStatusDelete(PetitionOB orderObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.AddParameter("@Status_Id", orderObject.StatusId);
            ncmdbObject.AddParameter("@TempOrderID", orderObject.TempOrderID);
            ncmdbObject.ExecuteNonQuery("ASP_Order_Change_statusDelete");
            p_var.Result = Convert.ToInt32(param[0].Value);
            return p_var.Result;
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Functiont to delete Order either temp or final

    public Int32 Delete_OrderStatus(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];

            ncmdbObject.AddParameter("@Temp_Order_Id", petObject.RPId);
            ncmdbObject.AddParameter("@Status_ID", petObject.StatusId);
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            ncmdbObject.ExecuteNonQuery("ASP_Tmp_order_Delete");
            p_var.Result = Convert.ToInt32(param[0].Value);
            return p_var.Result;
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to update ConnectedOrder_Restore for delete

    public Int32 ConnectedOrder_Restore(PetitionOB rtiObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.AddParameter(param[0]);
            //ncmdbObject.AddParameter("@Status_Id", rtiObject.StatusId);
            ncmdbObject.AddParameter("@Temp_OrderId", rtiObject.OrderID);
            ncmdbObject.ExecuteNonQuery("ASP_ConnectedOrder_Restore");
            p_var.Result = Convert.ToInt32(param[0].Value);
            return p_var.Result;
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to get connected order of petition

    public DataSet getConnectedOrders(PetitionOB objpetOB)
    {
        try
        {
            ncmdbObject.AddParameter("@OrderId", objpetOB.OrderID);
            return ncmdbObject.ExecuteDataSet("USP_GetOrder_FileName");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion


    #region Function to get category of status of petition

    public DataSet getStatusCategory(PetitionOB objpetOB)
    {
        try
        {
            ncmdbObject.AddParameter("@ReviewId", objpetOB.RPId);
            return ncmdbObject.ExecuteDataSet("ASP_getStatusForReview");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to insert OrderWithPetition

    public Int32 insertOrderwithPetition(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@ActionType", Convert.ToInt16(Module_ID_Enum.Project_Action_Type.insert)));
            ncmdbObject.Parameters.Add(new SqlParameter("@Petition_id", petObject.PetitionId));
            ncmdbObject.Parameters.Add(new SqlParameter("@year", petObject.year));
            ncmdbObject.Parameters.Add(new SqlParameter("@OrderID", petObject.OrderID));
            ncmdbObject.Parameters.Add(new SqlParameter("@Status_Id", petObject.StatusId));
            ncmdbObject.Parameters.Add(new SqlParameter("@ReviewId", petObject.RPId));

            ncmdbObject.Parameters.Add(new SqlParameter("@DepttId", petObject.DepttId));
            ncmdbObject.Parameters.Add(new SqlParameter("@OrderTypeID", petObject.OrderTypeID));
            ncmdbObject.ExecuteNonQuery("ASP_ConnectedPetitionWithOrder_Insert_Update");
            p_var.Result = Convert.ToInt32(param[0].Value);
            return p_var.Result;
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to get connected Order for Petition(For Edit)

    public DataSet get_ConnectedOrder_Edit(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@OrderId", petObject.OrderID));

            return ncmdbObject.ExecuteDataSet("ASP_GetConnectedOrderPetition");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to get connected Order for Category(For Edit)

    public DataSet get_ConnectedOrderCategory_Edit(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@OrderId", petObject.OrderID));

            return ncmdbObject.ExecuteDataSet("ASP_GetConnectedOrderCategory");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to delete Connectedpetition from public notice

    public Int32 deleteConnectedPetitionFromOrder(PetitionOB orderObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@OrderID", orderObject.OrderID));
            ncmdbObject.ExecuteNonQuery("ASP_ConnectedPetitionWithOrderDelete");
            p_var.Result = Convert.ToInt32(param[0].Value);
            return p_var.Result;
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion


    #region Function to delete Connectedorder categoy

    public Int32 deleteConnectedCategoryFromOrder(PetitionOB orderObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@OrderID", orderObject.OrderID));
            ncmdbObject.ExecuteNonQuery("ASP_ConnectedOrderCategoryDelete");
            p_var.Result = Convert.ToInt32(param[0].Value);
            return p_var.Result;
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to get connected Order for Petition(For Add)

    public DataSet get_ConnectedOrder_Add(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@OrderId", petObject.OrderID));
            ncmdbObject.Parameters.Add(new SqlParameter("@PetitionType", petObject.PetitionType));
            return ncmdbObject.ExecuteDataSet("ASP_GetConnectedPetitionWithOrderAdd");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to insert ConnectedOrderCategory

    public Int32 insertConnectedOrderCategory(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@ActionType", Convert.ToInt16(Module_ID_Enum.Project_Action_Type.insert)));
            ncmdbObject.Parameters.Add(new SqlParameter("@OrderTypeID", petObject.OrderTypeID));
            ncmdbObject.Parameters.Add(new SqlParameter("@Orderid", petObject.OrderID));
            ncmdbObject.Parameters.Add(new SqlParameter("@OrderCategoryID", petObject.OrderCatID));
            ncmdbObject.Parameters.Add(new SqlParameter("@Status_Id", petObject.StatusId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Year", petObject.year));
            ncmdbObject.ExecuteNonQuery("ASP_Connected_Ordercategory_Insert_Update");
            p_var.Result = Convert.ToInt32(param[0].Value);
            return p_var.Result;
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to get Year for search

    public DataSet getOrderYearForSearch(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@ConnectionType", petObject.ConnectionType);
            return ncmdbObject.ExecuteDataSet("USP_GetYearOrdersSearch");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion



    #region Function to get PRONumber for search

    public DataSet getOrderPRONumbersForSearch(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Year", petObject.year));
            ncmdbObject.Parameters.Add(new SqlParameter("@ConnectionType", petObject.ConnectionType));
            return ncmdbObject.ExecuteDataSet("USP_GetPRONumberOrdersSearch");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion




    #region function to bind year for daily order search

    public DataSet Get_YearOrderDailySearch(PetitionOB objpetOB)
    {
        try
        {
            ncmdbObject.AddParameter("@OrderTypeId", objpetOB.OrderTypeID);
            ncmdbObject.AddParameter("@ConnectionType", objpetOB.ConnectionType);
            return ncmdbObject.ExecuteDataSet("USP_GetYearOrdersDailySearch");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion


    #region Function to get DailyOrder PRONumber for search

    public DataSet getDailyOrderPRONumbersForSearch(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Year", petObject.year));
            ncmdbObject.Parameters.Add(new SqlParameter("@ConnectionType", petObject.ConnectionType));
            return ncmdbObject.ExecuteDataSet("USP_GetPRONumberDailyOrdersSearch");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion


    #region function to get Order Details

    public DataSet Get_OrderDetails(PetitionOB obj_petOB)
    {

        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.AddParameter("@OrderId", obj_petOB.OrderID);
            return p_var.dSet = ncmdbObject.ExecuteDataSet("USP_GetOrdersDetails");


        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion


    #region Function to get Order year for connections

    public DataSet getOrderYearForConnectionEdit(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@OrderID", petObject.TempOrderID);

            return ncmdbObject.ExecuteDataSet("ASP_GetApprovedorderYearEdit");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }

    }

    #endregion



    #region Function to get connected Order for Details

    public DataSet get_OrderByIdDetails(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@OrderId", petObject.OrderID));

            return ncmdbObject.ExecuteDataSet("USP_GetOrdersById");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion


    //BackEnd 28 Aug  2013
    #region Function to get connected Order for Petitions

    public DataSet get_OrderYearForPetition(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@OrderTypeId", petObject.OrderTypeID));

            return ncmdbObject.ExecuteDataSet("ASP_GetConnectedYearOrderPetition");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to get connected OrderId for Petitions

    public DataSet get_OrderIdForPetitionYearWise(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@OrderTypeId", petObject.OrderTypeID));
            ncmdbObject.Parameters.Add(new SqlParameter("@Year", petObject.year));
            return ncmdbObject.ExecuteDataSet("ASP_GetConnectedOrderIdForPetition");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion



    #region Function to Insert Into ReviewPetition Connected Order

    public Int32 InsertIntoReviewPetitionConnectedOrder(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@OrderTypeId", petObject.OrderTypeID));
            ncmdbObject.Parameters.Add(new SqlParameter("@Year", petObject.year));
            ncmdbObject.Parameters.Add(new SqlParameter("@OrderId", petObject.OrderID));
            ncmdbObject.Parameters.Add(new SqlParameter("@ReviewPetitionId", petObject.ReView));
            return ncmdbObject.ExecuteNonQuery("ASP_InsertIntoReviewPetitionConnectedOrder");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to get Review petition connected Order for Details

    public DataSet GetReviewPetitionConnectedOrder(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@ReviewPetitionId", petObject.ReView));

            return ncmdbObject.ExecuteDataSet("ASP_GetReviewPetitionConnectedOrder");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion



    #region Function to delete ConnectedOrderpetitionReview

    public Int32 deleteConnectedOrderReviewPetition(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@RP_Id", petObject.RPId));
            ncmdbObject.ExecuteNonQuery("ASP_ConnectedOrder_ReviewPetitionDelete");
            p_var.Result = Convert.ToInt32(param[0].Value);
            return p_var.Result;
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion


    // 6 Sep 2013 Appeal petition Procedures Connected Order


    #region Function to Insert Into AppealPetition Connected Order

    public Int32 InsertIntoAppealPetitionConnectedOrder(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@OrderTypeId", petObject.OrderTypeID));
            ncmdbObject.Parameters.Add(new SqlParameter("@Year", petObject.year));
            ncmdbObject.Parameters.Add(new SqlParameter("@OrderId", petObject.OrderID));
            ncmdbObject.Parameters.Add(new SqlParameter("@AppealPetitionId", petObject.AppealId));
            return ncmdbObject.ExecuteNonQuery("ASP_InsertIntoAppealPetitionConnectedOrder");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion



    #region Function to get Appeal petition connected Order for Details

    public DataSet GeAppealPetitionConnectedOrder(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@AppealPetitionId", petObject.AppealId));

            return ncmdbObject.ExecuteDataSet("ASP_GetRAppealPetitionConnectedOrder");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

    #region Function to delete ConnectedOrderpetitionAppeal

    public Int32 deleteConnectedOrderAppealPetition(PetitionOB petObject)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@recordCount", SqlDbType.Int);
            param[0].Direction = ParameterDirection.Output;
            ncmdbObject.Parameters.Add(param[0]);
            ncmdbObject.Parameters.Add(new SqlParameter("@PA_Id", petObject.AppealId));
            ncmdbObject.ExecuteNonQuery("ASP_ConnectedOrder_AppealPetitionDelete");
            p_var.Result = Convert.ToInt32(param[0].Value);
            return p_var.Result;
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion



    #region function to bind year order under appeal

    public DataSet Get_YearOrderUnderAppeal()
    {
        try
        {

            return ncmdbObject.ExecuteDataSet("USP_GetYearOrdersUnderAppeal");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion 

    #region function to get year of Orders during edit

    public DataSet GetYearOrdersEdit(PetitionOB orderObject)
    {
        try
        {
            ncmdbObject.AddParameter("@connectionid", orderObject.ConnectionID);
            return ncmdbObject.ExecuteDataSet("ASP_GetYearOrders_AdminEdit");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion

	#region Function to get connected orders for Petition(For new editing)

    public DataSet getConnectedOrderEdit(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@OrderID", petObject.OrderID));
            return ncmdbObject.ExecuteDataSet("ASP_GetConnectedPetitionWithOrder");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion



    #region function to get email ids to send email for herc.

    public DataSet getEmailIdForSendingOrderMail(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@OrderId", petObject.OrderID);
            return ncmdbObject.ExecuteDataSet("asp_GetOrderEmailsForHerc");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion



    #region function to get Mobile numbers to send Order sms for herc.

    public DataSet getMobileNumberForSendingOrderSms(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.AddParameter("@OrderID", petObject.OrderID);
            return ncmdbObject.ExecuteDataSet("asp_GetOrderMobileForHerc");
        }
        catch
        {
            throw;
        }
        finally
        {
            ncmdbObject.Dispose();
        }
    }

    #endregion



    //End
}
