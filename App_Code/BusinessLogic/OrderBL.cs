using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public class OrderBL
{
    #region Default constructors zone

    public OrderBL()
    {

    }

    #endregion

    //Area for all the variables declaration zone

    OrderDL orderDL = new OrderDL();

    //End

    //Area for all the user defined functions to display contents

    #region Function to get order type

    public DataSet getOrderType()
    {
        try
        {
            return orderDL.getOrderType();
        }
        catch
        {
            throw;
        }
    }

    #endregion


    #region Function to get order type for display

    public DataSet getOrderTypeDisplay()
    {
        try
        {
            return orderDL.getOrderTypeDisplay();
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to get order categories

    public DataSet getOrderCategories(PetitionOB orderObject)
    {
        try
        {
            return orderDL.getOrderCategories(orderObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function To Display orders with paging

    public DataSet DisplayOrdersWithPaging(PetitionOB orderObject, out int catValue)
    {
        try
        {
            return orderDL.DisplayOrdersWithPaging(orderObject, out catValue);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to get Temp orders record for edit

    public DataSet getOrdersRecordForEdit(PetitionOB orderObject)
    {
        try
        {
            return orderDL.getOrdersRecordForEdit(orderObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

  

    #region Function to get Petition Review numbers for orders

    public DataSet getPetitionReviewNumber()
    {
        try
        {
            return orderDL.getPetitionReviewNumber();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get Petition Appeal numbers

    public DataSet getPetitionAppealNumber()
    {
        try
        {
            return orderDL.getPetitionAppealNumber();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get Order ID from temp table for comparision

    public DataSet OrderIDforComparision(PetitionOB orderObject)
    {
        try
        {
            return orderDL.OrderIDforComparision(orderObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion


    #region function to bind year

    public DataSet Get_YearOrder(PetitionOB objpetOB)
    {
        try
        {
            return orderDL.Get_YearOrder(objpetOB);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to get current Order information

    public DataSet Get_Order(PetitionOB obj_petOB, out int catValue)
    {

        try
        {
            return orderDL.Get_Order(obj_petOB, out catValue);
        }
        catch
        {
            throw;
        }

    }
    #endregion

    #region function to get Previous Order information
    public DataSet Get_OrderPrevious(PetitionOB obj_petOB, out int catValue)
    {

        try
        {
            return orderDL.Get_OrderPrevious(obj_petOB, out catValue);
        }
        catch
        {
            throw;
        }

    }
    #endregion


    #region function to get Orders Search information
    public DataSet Get_SearchOrders(PetitionOB obj_petOB, out int catValue)
    {

        try
        {

            return orderDL.Get_SearchOrders(obj_petOB, out catValue);

        }
        catch
        {
            throw;
        }

    }
    #endregion

    #region function to get Repealed Order information

    public DataSet Get_OrderRepealed(PetitionOB obj_petOB, out int catValue)
    {

        try
        {
            return orderDL.Get_OrderRepealed(obj_petOB, out catValue);
        }
        catch
        {
            throw;
        }

    }
    #endregion


    //Get order pronounced

    #region function to get current Order information

    public DataSet Get_CurrentOrder(PetitionOB obj_petOB, out int catValue)
    {

        try
        {
            return orderDL.Get_CurrentOrder(obj_petOB, out catValue);
        }
        catch
        {
            throw;
        }

    }
    #endregion

    #region function to get Previous Order information
    public DataSet Get_CurrentOrderPrevious(PetitionOB obj_petOB, out int catValue)
    {

        try
        {
            return orderDL.Get_CurrentOrderPrevious(obj_petOB, out catValue);
        }
        catch
        {
            throw;
        }

    }
    #endregion

    #region function to get Orders Search information
    public DataSet Get_CurrentSearchOrders(PetitionOB obj_petOB, out int catValue)
    {

        try
        {

            return orderDL.Get_CurrentSearchOrders(obj_petOB, out catValue);

        }
        catch
        {
            throw;
        }

    }
    #endregion

    #region function to get categoryWise Order information

    public DataSet Get_OrderCategoryWise(PetitionOB obj_petOB, out int catValue)
    {

        try
        {
            return orderDL.Get_OrderCategoryWise(obj_petOB, out catValue);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to get PFD categoryWise

    public DataSet Get_pdf(PetitionOB objPetOB)
    {
        try
        {
            return orderDL.Get_pdf(objPetOB);
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
            return orderDL.getOrderSubcategory(objPetOB);
        }
        catch
        {
            throw;
        }


    }

    #endregion

    #region Function to get Orders numbers for connections

    public DataSet getOrderNumberForConnection()
    {
        try
        {
            return orderDL.getOrderNumberForConnection();
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region function to Get Fileaname from Connected orders

    public DataSet Get_ConnectedFileName(PetitionOB petOB)
    {
        try
        {
            return orderDL.Get_ConnectedFileName(petOB);
        }
        catch
        {
            throw;
        }

    }


    #endregion

    //3 Dec 2012

    #region Function to get connected Orders(For Edit)

    public DataSet get_ConnectedOrders_Edit(PetitionOB petObject)
    {
        try
        {


            return orderDL.get_ConnectedOrders_Edit(petObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion


    #region function to bind year
    public DataSet GetYear()
    {
        try
        {
            return orderDL.GetYear();
        }
        catch
        {
            throw;
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
            return orderDL.insertUpdateOrderTemp(orderObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to update the order status

    public Int32 OrdersUpdateStatus(PetitionOB orderObject)
    {
        try
        {
            return orderDL.OrdersUpdateStatus(orderObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function To insert Orders into web final table

    public Int32 InsertOrdersIntoWeb(PetitionOB orderObject)
    {
        try
        {
            return orderDL.InsertOrdersIntoWeb(orderObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion


    #region Function to insert ConnectedOrders

    public Int32 insertConnectedOrders(PetitionOB petObject)
    {
        try
        {
            return orderDL.insertConnectedOrders(petObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion


    #region Function to update the order status for repealed

    public Int32 OrdersUpdateStatus_repealed(PetitionOB orderObject)
    {
        try
        {

            return orderDL.OrdersUpdateStatus_repealed(orderObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    //End


    #region function to get year of Orders

    public DataSet GetYearOrders()
    {
        try
        {
            return orderDL.GetYearOrders();
        }
        catch
        {
            throw;
        }
    }

    #endregion


    #region function to bind year of order for petition/review/Appeal
    public DataSet GetYearAppeal(PetitionOB orderObject)
    {
        try
        {
            return orderDL.GetYearAppeal(orderObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to get Petition numbers for orders

    public DataSet getPetitionNumberForOrders(PetitionOB objpetOB)
    {
        try
        {
            return orderDL.getPetitionNumberForOrders(objpetOB);
        }
        catch
        {
            throw;
        }

    }

    #endregion


    #region Function to Update petition Review from temp table for comparision

    public Int32 updateReview(PetitionOB orderObject)
    {
        try
        {
            return orderDL.updateReview(orderObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to insert into File_order
    public Int32 insert_intoFileOrder(PetitionOB orderObject)
    {
        try
        {
            return orderDL.insert_intoFileOrder(orderObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion


    #region Function to get FileName for orders

    public DataSet getFileNameForOrders(PetitionOB orderObject)
    {
        try
        {

            return orderDL.getFileNameForOrders(orderObject);
        }
        catch
        {
            throw;
        }


    }

    #endregion

    #region Function to Update status for Files in File_orders

    public Int32 UpdateFileStatusForOrders(PetitionOB orderObject)
    {
        try
        {
            return orderDL.UpdateFileStatusForOrders(orderObject);
        }
        catch
        {
            throw;
        }


    }

    #endregion


    #region function for orderunderAppeal

    public DataSet OrderunderAppeal(PetitionOB obj_petOB, out int catValue)
    {
        try
        {
            return orderDL.OrderunderAppeal(obj_petOB, out  catValue);

        }
        catch
        {
            throw;
        }

    }

    #endregion



    #region Function to get connected orders during deleting record from ConnectedOrder table

    public DataSet get_ConnectedOrderDelete(PetitionOB petObject)
    {
        try
        {

            return orderDL.get_ConnectedOrderDelete(petObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion


    #region Function to update the order status for delete

    public Int32 OrdersUpdateStatusDelete(PetitionOB orderObject)
    {
        try
        {

            return orderDL.OrdersUpdateStatusDelete(orderObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion


    #region Functiont to delete Order either temp or final

    public Int32 Delete_OrderStatus(PetitionOB petObject)
    {
        try
        {

            return orderDL.Delete_OrderStatus(petObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion


    #region Function to update ConnectedOrder_Restore for delete

    public Int32 ConnectedOrder_Restore(PetitionOB rtiObject)
    {
        try
        {

            return orderDL.ConnectedOrder_Restore(rtiObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to get connected order of petition

    public DataSet getConnectedOrders(PetitionOB objpetOB)
    {
        try
        {
            return orderDL.getConnectedOrders(objpetOB);
        }
        catch
        {
            throw;
        }

    }

    #endregion



    #region Function to get category of status of petition

    public DataSet getStatusCategory(PetitionOB objpetOB)
    {
        try
        {
            return orderDL.getStatusCategory(objpetOB);
        }
        catch
        {
            throw;
        }

    }

    #endregion


    #region Function to insert OrderWithPetition

    public Int32 insertOrderwithPetition(PetitionOB petObject)
    {
        try
        {
            return orderDL.insertOrderwithPetition(petObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion



    #region Function to get connected Order for Petition(For Edit)

    public DataSet get_ConnectedOrder_Edit(PetitionOB petObject)
    {
        try
        {
            return orderDL.get_ConnectedOrder_Edit(petObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to get connected Order for Category(For Edit)

    public DataSet get_ConnectedOrderCategory_Edit(PetitionOB petObject)
    {
        try
        {
            return orderDL.get_ConnectedOrderCategory_Edit(petObject);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion

    #region Function to delete Connectedpetition from public notice

    public Int32 deleteConnectedPetitionFromOrder(PetitionOB orderObject)
    {
        try
        {
            return orderDL.deleteConnectedPetitionFromOrder(orderObject);
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to delete Connectedorder categoy

    public Int32 deleteConnectedCategoryFromOrder(PetitionOB orderObject)
    {
        try
        {
            return orderDL.deleteConnectedCategoryFromOrder(orderObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion



    #region Function to get connected Order for Petition(For Add)

    public DataSet get_ConnectedOrder_Add(PetitionOB petObject)
    {
        try
        {
            return orderDL.get_ConnectedOrder_Add(petObject);
        }
        catch
        {
            throw;
        }
 
    }

    #endregion


    #region Function to insert ConnectedOrderCategory

    public Int32 insertConnectedOrderCategory(PetitionOB petObject)
    {
        try
        {
            return orderDL.insertConnectedOrderCategory(petObject);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion

    #region Function to get Year for search

    public DataSet getOrderYearForSearch(PetitionOB petObject)
    {
        try
        {
            return orderDL.getOrderYearForSearch(petObject);
        }
        catch
        {
            throw;
        }
      
    }

    #endregion


    #region Function to get PRONumber for search

    public DataSet getOrderPRONumbersForSearch(PetitionOB petObject)
    {
        try
        {
            return orderDL.getOrderPRONumbersForSearch(petObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion


    #region function to bind year for daily order search

    public DataSet Get_YearOrderDailySearch(PetitionOB objpetOB)
    {
        try
        {
            return orderDL.Get_YearOrderDailySearch(objpetOB);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion 



    #region Function to get DailyOrder PRONumber for search

    public DataSet getDailyOrderPRONumbersForSearch(PetitionOB petObject)
    {
        try
        {
            return orderDL.getDailyOrderPRONumbersForSearch(petObject);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion

    #region function to get Order Details

    public DataSet Get_OrderDetails(PetitionOB obj_petOB)
    {

        try
        {
            return orderDL.Get_OrderDetails(obj_petOB);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion 


    #region Function to get Order year for connections

    public DataSet getOrderYearForConnectionEdit(PetitionOB petObject)
    {
        try
        {
            return orderDL.getOrderYearForConnectionEdit(petObject);
        }
        catch
        {
            throw;
        }
       

    }

    #endregion

    #region Function to get connected Order for Details

    public DataSet get_OrderByIdDetails(PetitionOB petObject)
    {
        try
        {

            return orderDL.get_OrderByIdDetails(petObject);
        }
        catch
        {
            throw;
        }
      
    }

    #endregion


    //BackEnd 28 Aug  2013
    #region Function to get connected Order for Petitions

    public DataSet get_OrderYearForPetition(PetitionOB petObject)
    {
        try
        {
            return orderDL.get_OrderYearForPetition(petObject);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion


    #region Function to get connected OrderId for Petitions

    public DataSet get_OrderIdForPetitionYearWise(PetitionOB petObject)
    {
        try
        {
            return orderDL.get_OrderIdForPetitionYearWise(petObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion


    #region Function to Insert Into ReviewPetition Connected Order

    public Int32 InsertIntoReviewPetitionConnectedOrder(PetitionOB petObject)
    {
        try
        {
            return orderDL.InsertIntoReviewPetitionConnectedOrder(petObject);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion


    #region Function to get Review petition connected Order for Details

    public DataSet GetReviewPetitionConnectedOrder(PetitionOB petObject)
    {
        try
        {
            return orderDL.GetReviewPetitionConnectedOrder(petObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion


    #region Function to delete ConnectedOrderpetitionReview

    public Int32 deleteConnectedOrderReviewPetition(PetitionOB petObject)
    {
        try
        {
            return orderDL.deleteConnectedOrderReviewPetition(petObject);
        }
        catch
        {
            throw;
        }
        
    }

    #endregion


    // 6 Sep 2013 Appeal petition Procedures Connected Order


    #region Function to Insert Into AppealPetition Connected Order

    public Int32 InsertIntoAppealPetitionConnectedOrder(PetitionOB petObject)
    {
        try
        {
            return orderDL.InsertIntoAppealPetitionConnectedOrder(petObject);
        }
        catch
        {
            throw;
        }
      
    }

    #endregion

    #region Function to get Appeal petition connected Order for Details

    public DataSet GeAppealPetitionConnectedOrder(PetitionOB petObject)
    {
        try
        {
            return orderDL.GeAppealPetitionConnectedOrder(petObject);
        }
        catch
        {
            throw;
        }
  
    }

    #endregion


    #region Function to delete ConnectedOrderpetitionAppeal

    public Int32 deleteConnectedOrderAppealPetition(PetitionOB petObject)
    {
        try
        {
            return orderDL.deleteConnectedOrderAppealPetition(petObject);
        
        }
        catch
        {
            throw;
        }
      
    }

    #endregion


    #region function to bind year order under appeal

    public DataSet Get_YearOrderUnderAppeal()
    {
        try
        {

            return orderDL.Get_YearOrderUnderAppeal();
        }
        catch
        {
            throw;
        }
       
    }

    #endregion 

    #region function to get year of Orders during edit

    public DataSet GetYearOrdersEdit(PetitionOB orderObject)
    {
        try
        {
            return orderDL.GetYearOrdersEdit(orderObject);
        }
        catch
        {
            throw;
        }
    }

    #endregion
	
	#region Function to get connected order for Petition(For new editing)

    public DataSet getConnectedOrderEdit(PetitionOB petObject)
    {
        try
        {
            return orderDL.getConnectedOrderEdit(petObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion



    #region function to get email ids to send email for herc.

    public DataSet getEmailIdForSendingOrderMail(PetitionOB petObject)
    {
        try
        {
            return orderDL.getEmailIdForSendingOrderMail(petObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion



    #region function to get Mobile numbers to send Order sms for herc.

    public DataSet getMobileNumberForSendingOrderSms(PetitionOB petObject)
    {
        try
        {
            return orderDL.getMobileNumberForSendingOrderSms(petObject);
        }
        catch
        {
            throw;
        }
       
    }

    #endregion


    //End
}
