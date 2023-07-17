using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using NCM.DAL;
using System.IO;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Net.Configuration;
using System.Text.RegularExpressions;
using System.Linq;
using System.Globalization;



public class Miscelleneous_DL
{
    #region  Default constructor zone

    public Miscelleneous_DL()
    {

    }

    #endregion

    #region Default data declaration zone

    NCMdbAccess ncmdbObject = new NCMdbAccess();
    Project_Variables p_Val = new Project_Variables();
   
   
    #endregion

    #region Function to get date in mm/dd/yyyy format

    public DateTime getDateFormat(string mydate)
    {
        try
        {
            //Code to convert date format from dd/mm/yyyy to mm/dd/yyyy

            p_Val.date = DateTime.ParseExact(mydate, "dd/mm/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            p_Val.strDate = p_Val.date.ToString("mm/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
           if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
            {
                return (Convert.ToDateTime(p_Val.strDate));
            }
            else
            {
                return Convert.ToDateTime(mydate);
            }
            //End of conversion of date format
        }
        catch
        {
            throw;
        }

    }

    #endregion

	    #region Function to get date in mm/dd/yyyy format

    public DateTime getDateFormatSohCalender(string mydate)
    {
        try
        {
            //Code to convert date format from dd/mm/yyyy to mm/dd/yyyy

            p_Val.date = DateTime.ParseExact(mydate, "dd/mm/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            p_Val.strDate = p_Val.date.ToString("mm/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            
                return (Convert.ToDateTime(p_Val.strDate));
           
            //End of conversion of date format
        }
        catch
        {
            throw;
        }

    }

    #endregion


    #region Function to get date in dd/mm/yyyy format

    public string getDateFormatddMMYYYY(string mydate)
    {
        try
        {
            p_Val.date = DateTime.ParseExact(mydate, "m/d/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            p_Val.strDate = p_Val.date.ToString("d/m/yyyy", System.Globalization.CultureInfo.InvariantCulture);

			 if (Convert.ToInt16(Resources.HercResource.Lang_Id) == Convert.ToInt16(Module_ID_Enum.Language_ID.English))
            {
                 return (p_Val.strDate);
            }
            else
            {
                 return (p_Val.strDate);
            }
           
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region code to Create Directory

    public void MakeDirectoryIfExists(string NewDirectory)
    {
        try
        {
            // Check if directory exists
            if (!Directory.Exists(HttpContext.Current.Request.MapPath(NewDirectory)))
            {
                // Create the directory.
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(NewDirectory));
            }
        }
        catch (IOException _ex)
        {
            System.Web.HttpContext.Current.Response.Write(_ex.Message);
        }
    }

    #endregion

    #region function to check File existance during uploading

    public string getUniqueFileName(string filname, string filepath, string fileNameWithoutExt, string ext)
    {
        int DotPosition = filname.IndexOf(".");
        string NewFileName = filname;
        int Counter = 0;
        // int FullPath = 0;
        try
        {
            if ((System.IO.File.Exists(filepath + "/" + NewFileName)))
            {
                while (((System.IO.File.Exists(filepath + "/" + NewFileName))))
                {
                    Counter = Counter + 1;
                    NewFileName = fileNameWithoutExt + "(" + Counter + ")";
                    NewFileName = NewFileName + ext;
                }
            }
            else
            {
                NewFileName = fileNameWithoutExt + ext;
            }
            return NewFileName;
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get status

    public DataSet getStatusType()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("MST_Get_Status");
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

    #region Function to get Language

    public DataSet getLanguage(UserOB usrObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Lang_Id",usrObject.LangId));
            return ncmdbObject.ExecuteDataSet("MST_Get_Language");
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

    #region Function to find IP Address

    public string IpAddress()
    {
        try
        {
            p_Val.IpAddress = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (p_Val.IpAddress == null || p_Val.IpAddress == "")
            {
                p_Val.IpAddress = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return p_Val.IpAddress;
        }
        catch
        {
            throw;
        }
    }

    #endregion


    #region Function to get Status according to permission

    public DataSet getStatusPermissionwise(UserOB usrObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Status_Id", usrObject.ModulestatusID));
            return ncmdbObject.ExecuteDataSet("MST_Get_Status");
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

    #region Function to get Language  as per given permission

    public DataSet getLanguagePermission(UserOB usrObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Role_Id", usrObject.RoleId));
            ncmdbObject.Parameters.Add(new SqlParameter("@Module_Id", usrObject.ModuleId));
            return ncmdbObject.ExecuteDataSet("CheckPrivilagesALL");
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

    #region Function to limit string length upto 40 characters

    public static string FixCharacters(object Desc, int length)
    {
       
        try
        {
            Project_Variables p_Val_Temp = new Project_Variables();

            p_Val_Temp.sbuilder.Insert(0, Desc.ToString());

            if (p_Val_Temp.sbuilder.Length > length)
                return p_Val_Temp.sbuilder.ToString().Substring(0, length) + "...";
            else return p_Val_Temp.sbuilder.ToString();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to fix on cs page string length upto given characters(For cs page)

    public string FixGivenCharacters(string Desc, int length)
    {
        try
        {
           
            p_Val.sbuilder.Insert(0, Desc.ToString());

            if (p_Val.sbuilder.Length > length)
                return p_Val.sbuilder.ToString().Substring(0, length) + "...";
            else return p_Val.sbuilder.ToString();
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Function to get Categoty For Banner Mgmt
    public  DataSet Get_Category_ForBanner()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("ASP_Category_Master_Category_Type");
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

    #region function to check file extension

    public  bool CheckImageFileExtension(string extension)
    {
        try
        {
            List<string> listExtension = new List<string>() { ".jpg", ".jpeg", ".png", ".gif" };
            return listExtension.Contains(extension.ToLower());
        }
        catch
        {
            throw;
        }
       
    }

    #endregion

    #region Function to get the module Information
    public DataSet ASP_Master_Module_Get_Module()
    {
        try
        {
            return ncmdbObject.ExecuteDataSet("ASP_Master_Module_Get_Module");
        }
        catch
        {
            throw;
        }
    }

    #endregion

    //New Function to check actual file type

    public  bool GetActualFileType(Stream stream)
    {

        try
        {

            BinaryReader sr = new BinaryReader(stream);

            byte[] header = new byte[16];

            sr.Read(header, 0, 16);

            StringBuilder hexString = new StringBuilder(header.Length);

            for (p_Val.i = 0; p_Val.i < header.Length - 1; p_Val.i++)
            {

                hexString.Append(header[p_Val.i].ToString("x2"));

            }

            Miscelleneous_DL com = new Miscelleneous_DL();

            return com.GetFileTypeByCode(hexString.ToString());

        }

        catch (Exception e2)
        {

            return false;

        }

    }


    public bool GetFileTypeByCode(string code)
    {

        p_Val.flag = false;

        // here sis starting bit of wmv,wav

        //if (code.StartsWith("3026b") || code.StartsWith("52494"))

        //{

        // here sis starting bit of jpg or jpeg, png,gif and bmp respectively

        if (code.StartsWith("6a0d") | code.StartsWith("2030") | code.StartsWith("0a31") | code.StartsWith("ffd8") | code.StartsWith("8950") | code.StartsWith("4749") | code.StartsWith("2550") | code.StartsWith("d0cf11e0a") | code.StartsWith("504b0304"))
        {

            //43575307 swf

            //47494638 gif

            //FFD8 jpg

            //424d bmp

            //D0CF11E0A1B11AE10000000000000000 - doc

            //D0CF11E0A1B11AE10000000000000000 - xls

            //504B03041400060008000000210030C9 - docx 

            //504B030414000600080000002100710E - xlsx

            //504B030414000000080060624E3E6267 - zip

            //526172211A0700CF907300000D000000 - rar

            //255044462D312E330D0A25E2E3CFD30D - pdf

            //255044462D312E330D25E2E3CFD30D0A - pdf

            //435753074C130200789CECB875545B6D - swf

            //3026B2758E66CF11A6D900AA0062CE6C - wmv

            //52494646EA40000057415645666D7420 - wav

            //524946463A0B000057415645666D7420 - wav

            ///52494646A44D000057415645666D7420 - wav

            p_Val.flag = true;


        }

        else
        {

            p_Val.flag = false;

        }

        return p_Val.flag;

    }

    public bool SendEmailForLogin(string To, string Cc, string Bcc, string Subject, string From, string Msg)// send mail 
    {
        MailMessage objMail = new MailMessage();
        //objMail.To.Add(new MailAddress(To));

        string[] tos = To.Split(';');

        for (int i = 0; i < tos.Length; i++)
        {

            objMail.To.Add(new MailAddress(tos[i]));

        }

        objMail.Subject = Subject;
        if (From != string.Empty)
        {
            objMail.From = new MailAddress(From);
        }
        else
        {
            objMail.From = new MailAddress("");
        }
        objMail.IsBodyHtml = true;
        objMail.Body = Msg;
        objMail.Priority = MailPriority.High;
        SmtpClient smtp = new SmtpClient();
        smtp.Host = ConfigurationManager.AppSettings["SMTP_Server"].ToString();
        //System.Web.Mail.SmtpMail.SmtpServer = "100.100.7.3";// server name 
        smtp.Send(objMail);
        return true;
    }



    public bool SendEmail(string To, string Cc, string Bcc, string Subject, string From, string Msg)// send mail 
    {
        MailMessage objMail = new MailMessage();
        //objMail.To.Add(new MailAddress(To));

        string[] tos = Bcc.Split(';');

        for (int i = 0; i < tos.Length; i++)
        {
            if (i == 0)
            {
                objMail.Bcc.Add(new MailAddress(tos[i]));
            }
            else
            {
                objMail.To.Add(new MailAddress(tos[i]));
            }

        }

        objMail.Subject = Subject;
        if (From != string.Empty)
        {
            objMail.From = new MailAddress(From);
        }
        else
        {
            objMail.From = new MailAddress("");
        }
        objMail.IsBodyHtml = true;
        objMail.Body = Msg;
        objMail.Priority = MailPriority.High;
        SmtpClient smtp = new SmtpClient();
        smtp.Host = ConfigurationManager.AppSettings["SMTP_Server"].ToString();
        
        smtp.Send(objMail);
        return true;
    }


    #region Function to get Status according to modules

    public DataSet getStatusAccordingtoModule(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Status_Type_Id", petObject.PetitionStatusId));
            return ncmdbObject.ExecuteDataSet("ASP_Status_Type_Status_Display");
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to get Status for petitions

    public DataSet getStatusPetition(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Status_Type_Id", petObject.PetitionStatusId));
            return ncmdbObject.ExecuteDataSet("USP_Status_Type_Status_Display");
        }
        catch
        {
            throw;
        }

    }

    #endregion


    #region Function to get petition Status according to modules

    public DataSet getPetitionStatusAccordingtoModule(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Status_Type_Id", petObject.PetitionStatusId));
            return ncmdbObject.ExecuteDataSet("ASP_Status_Type_Status_PetitionDisplay");
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Function to get petition Appeal Status according to modules

    public DataSet getPetitionAppealStatusAccordingtoModule(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Status_Type_Id", petObject.PetitionStatusId));
            return ncmdbObject.ExecuteDataSet("ASP_Status_Type_Status_AppealDisplay");
        }
        catch
        {
            throw;
        }

    }

    #endregion

    public bool GetActualFileType_pdf(Stream stream)
    {

        try
        {

            BinaryReader sr = new BinaryReader(stream);

            byte[] header = new byte[16];

            sr.Read(header, 0, 16);

            StringBuilder hexString = new StringBuilder(header.Length);

            for (p_Val.i = 0; p_Val.i < header.Length - 1; p_Val.i++)
            {

                hexString.Append(header[p_Val.i].ToString("x2"));

            }

            Miscelleneous_DL com = new Miscelleneous_DL();

            return com.GetFileTypeByCode_pdf(hexString.ToString());

        }

        catch (Exception e2)
        {

            return false;

        }

    }


    public bool GetFileTypeByCode_pdf(string code)
    {

        p_Val.flag = false;

        // here sis starting bit of wmv,wav

        //if (code.StartsWith("3026b") || code.StartsWith("52494"))

        //{

        // here sis starting bit of jpg or jpeg, png,gif and bmp respectively

         if (code.StartsWith("2550")| code.StartsWith("2030") | code.StartsWith("0a31")| code.StartsWith("3137"))
        
        {

            //43575307 swf

            //47494638 gif

            //FFD8 jpg

            //424d bmp

            //D0CF11E0A1B11AE10000000000000000 - doc

            //D0CF11E0A1B11AE10000000000000000 - xls

            //504B03041400060008000000210030C9 - docx 

            //504B030414000600080000002100710E - xlsx

            //504B030414000000080060624E3E6267 - zip

            //526172211A0700CF907300000D000000 - rar

            //255044462D312E330D0A25E2E3CFD30D - pdf

            //255044462D312E330D25E2E3CFD30D0A - pdf

            //435753074C130200789CECB875545B6D - swf

            //3026B2758E66CF11A6D900AA0062CE6C - wmv

            //52494646EA40000057415645666D7420 - wav

            //524946463A0B000057415645666D7420 - wav

            ///52494646A44D000057415645666D7420 - wav

            p_Val.flag = true;


        }

        else
        {

            p_Val.flag = false;

        }

        return p_Val.flag;

    }

    #region Function to remove XXS(Cross-site scripting)

    public string strip_dangerous_tags(string text_with_tags)
    {
        string s = Regex.Replace(text_with_tags, @"<script", "<scrSAFEipt", RegexOptions.IgnoreCase);
        s = Regex.Replace(s, @"</script", "</scrSAFEipt", RegexOptions.IgnoreCase);
        s = Regex.Replace(s, @"<iframe", "</iframe", RegexOptions.IgnoreCase);
        s = Regex.Replace(s, @"<frameset", "</<frameset", RegexOptions.IgnoreCase);
        s = Regex.Replace(s, @"<frame", "</<frame", RegexOptions.IgnoreCase);
        s = Regex.Replace(s, @"<iframe", "</iframe", RegexOptions.IgnoreCase);
        s = Regex.Replace(s, @"<object", "</objSAFEct", RegexOptions.IgnoreCase);
        s = Regex.Replace(s, @"</object", "</obSAFEct", RegexOptions.IgnoreCase);
        // ADDED AFTER THIS QUESTION WAS POSTED
        s = Regex.Replace(s, @"javascript", "javaSAFEscript", RegexOptions.IgnoreCase);

        s = Regex.Replace(s, @"onabort", "onSAFEabort", RegexOptions.IgnoreCase);
        s = Regex.Replace(s, @"onblur", "onSAFEblur", RegexOptions.IgnoreCase);
        s = Regex.Replace(s, @"onchange", "onSAFEchange", RegexOptions.IgnoreCase);
        s = Regex.Replace(s, @"onclick", "onSAFEclick", RegexOptions.IgnoreCase);
        s = Regex.Replace(s, @"ondblclick", "onSAFEdblclick", RegexOptions.IgnoreCase);
        s = Regex.Replace(s, @"onerror", "onSAFEerror", RegexOptions.IgnoreCase);
        s = Regex.Replace(s, @"onfocus", "onSAFEfocus", RegexOptions.IgnoreCase);

        s = Regex.Replace(s, @"onkeydown", "onSAFEkeydown", RegexOptions.IgnoreCase);
        s = Regex.Replace(s, @"onkeypress", "onSAFEkeypress", RegexOptions.IgnoreCase);
        s = Regex.Replace(s, @"onkeyup", "onSAFEkeyup", RegexOptions.IgnoreCase);

        s = Regex.Replace(s, @"onload", "onSAFEload", RegexOptions.IgnoreCase);

        s = Regex.Replace(s, @"onmousedown", "onSAFEmousedown", RegexOptions.IgnoreCase);
        s = Regex.Replace(s, @"onmousemove", "onSAFEmousemove", RegexOptions.IgnoreCase);
        s = Regex.Replace(s, @"onmouseout", "onSAFEmouseout", RegexOptions.IgnoreCase);
        s = Regex.Replace(s, @"onmouseup", "onSAFEmouseup", RegexOptions.IgnoreCase);
        s = Regex.Replace(s, @"onmouseup", "onSAFEmouseup", RegexOptions.IgnoreCase);

        s = Regex.Replace(s, @"onreset", "onSAFEresetK", RegexOptions.IgnoreCase);
        s = Regex.Replace(s, @"onresize", "onSAFEresize", RegexOptions.IgnoreCase);
        s = Regex.Replace(s, @"onselect", "onSAFEselect", RegexOptions.IgnoreCase);
        s = Regex.Replace(s, @"onsubmit", "onSAFEsubmit", RegexOptions.IgnoreCase);
        s = Regex.Replace(s, @"onunload", "onSAFEunload", RegexOptions.IgnoreCase);

        return s;
    }

    #endregion

    public string RemoveFCKEditorTagHtml(string html)
    {
        string s = Regex.Replace(html, @"&lt;", "<", RegexOptions.IgnoreCase);
        s = Regex.Replace(s, @"&gt;", ">", RegexOptions.IgnoreCase);

        return s;
    }



    #region function to make gridview control accessible

    public void MakeAccessible(GridView grid)
    {
        if (grid.Rows.Count > 0)
        {
            //This replaces <td> with <th> and add the scope attribute
            grid.UseAccessibleHeader = true;

            //This will add the <thead> and <tbody> elements
            grid.HeaderRow.TableSection = TableRowSection.TableHeader;


            //This is add the <tfoot> element. Remove if you don't have a footer row
            grid.FooterRow.TableSection = TableRowSection.TableFooter;
        }
    }

    #endregion 

    // Division of characters start from here
    public string Division_characters(string text,int length )
    {
        
        string newString = string.Empty;
        char[] input = text.ToCharArray();
        //int increment = length;

        int start = 0;

        int end = input.Count() / length;
        Enumerable

       .Range(start, end + 1).ToList().ForEach(i => newString += input.Skip(i * length).Take(length).Aggregate("", (a, b) => a + b) + Environment.NewLine);
        text = newString;
        return text;

        
    }

    #region Function to get Status for petition appeal 

    public DataSet getStatusForPetitionAppeal(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Status_Type_Id", petObject.PetitionStatusId));
            return ncmdbObject.ExecuteDataSet("ASP_Status_TypeForPetitionAppeal");
        }
        catch
        {
            throw;
        }

    }

    #endregion


    #region Function to get Status for petition appeal Award

    public DataSet getStatusForPetitionAppealAward(PetitionOB petObject)
    {
        try
        {
            ncmdbObject.Parameters.Add(new SqlParameter("@Status_Type_Id", petObject.PetitionStatusId));
            return ncmdbObject.ExecuteDataSet("ASP_Status_TypeForPetitionAppealAward");
        }
        catch
        {
            throw;
        }

    }

    #endregion

    public bool SendEmailWithAttachments(string To, string Cc, string Bcc, string Subject, string From, string Msg,string attachment)// send mail 
    {
        MailMessage objMail = new MailMessage();
        p_Val.url = ("~/") + ConfigurationManager.AppSettings["WriteReadData"] + "/" + ConfigurationManager.AppSettings["Rti"] + "/";
        //objMail.To.Add(new MailAddress(To));

        string[] tos = Bcc.Split(';');

        for (int i = 0; i < tos.Length; i++)
        {
            if (i == 0)
            {
                objMail.Bcc.Add(new MailAddress(tos[i]));
            }
            else
            {
                objMail.To.Add(new MailAddress(tos[i]));
            }
           // objMail.Bcc.Add(new MailAddress(tos[i]));

        }

        if (attachment != string.Empty)
        {


            string file = p_Val.url + attachment;
            objMail.Attachments.Add(new Attachment (HttpContext.Current.Server.MapPath(p_Val.url + attachment)));
        }
        

        objMail.Subject = Subject;
        if (From != string.Empty)
        {
            objMail.From = new MailAddress(From);
        }
        else
        {
            objMail.From = new MailAddress("");
        }
        objMail.IsBodyHtml = true;
        objMail.Body = Msg;
        objMail.Priority = MailPriority.High;
        SmtpClient smtp = new SmtpClient();
        smtp.Host = ConfigurationManager.AppSettings["SMTP_Server"].ToString();
        //System.Web.Mail.SmtpMail.SmtpServer = "100.100.7.3";// server name 
        smtp.Send(objMail);
        return true;
    }

    //End

    #region Function to get file length

    //public string fileSize(decimal fileLength)
    //{
    //    decimal? FileInMB = null;
    //    string fileSize = string.Empty;
    //    FileInMB = decimal.Round(fileLength / (1024M), 1);
    //    decimal? FileInMBFloorValue = Math.Floor(fileLength / (1024M));


    //    if (Convert.ToDecimal(FileInMB - FileInMBFloorValue) > 0.5M)
    //    {
    //        fileSize = Math.Ceiling(fileLength / (1024M)).ToString() + " " + "KB";
    //        //lblfsize.Visible = true;
    //    }
    //    else
    //    {
    //        fileSize = FileInMB.ToString() + " " + "KB";
    //        //lblfsize.Visible = true;
    //    }
    //    return fileSize;
    //}
    public string fileSize(double sizeInBytes)
    {

        const double Terabyte = 1099511627776;
        const double Gigabyte = 1073741824;
        const double Megabyte = 1048576;
        const double Kilobyte = 1024;

        string result = string.Empty;
        double the_size = 0;
        string units = string.Empty;

        if (sizeInBytes >= Terabyte)
        {
            the_size = Convert.ToInt64(sizeInBytes / Terabyte);
            units = " Tb";
        }
        else
        {
            if (sizeInBytes >= Gigabyte)
            {
                the_size = Convert.ToInt64(sizeInBytes / Gigabyte);
                units = " Gb";
            }
            else
            {
                if (sizeInBytes >= Megabyte)
                {
                    the_size = Convert.ToInt64(sizeInBytes / Megabyte);
                    units = " Mb";
                }
                else
                {
                    if (sizeInBytes >= Kilobyte)
                    {
                        the_size = Convert.ToInt64(sizeInBytes / Kilobyte);
                        units = " Kb";
                    }
                    else
                    {
                        the_size = sizeInBytes;
                        units = " bytes";
                    }
                }
            }
        }

        if (units != "bytes")
        {
            result = the_size.ToString() + " " + units;
        }
        else
        {
            result = the_size.ToString() + " " + units;
        }
        return result;
    }
 


    #endregion


    public string fileSizeForContentPage(double sizeInBytes)
    {

        const long Terabyte = 1099511627776;
        const long Gigabyte = 1073741824;
        const long Megabyte = 1048576;
        const long Kilobyte = 1024;

        string result = string.Empty;
        long the_size = 0;
        string units = string.Empty;

        if (sizeInBytes >= Terabyte)
        {
            //the_size = Math.Round(sizeInBytes / Terabyte, 2);
            the_size =Convert.ToInt64( sizeInBytes / Terabyte);
            units = " Tb";
        }
        else
        {
            if (sizeInBytes >= Gigabyte)
            {
               // the_size = Math.Round(sizeInBytes / Gigabyte, 2);
                the_size = Convert.ToInt64(sizeInBytes / Gigabyte);
                units = " Gb";
            }
            else
            {
                if (sizeInBytes >= Megabyte)
                {
                    //the_size = Math.Round(sizeInBytes / Megabyte, 2);
                    the_size = Convert.ToInt64(sizeInBytes / Megabyte);
                    units = " Mb";
                }
                else
                {
                    if (sizeInBytes >= Kilobyte)
                    {
                        the_size = Convert.ToInt64(sizeInBytes / Kilobyte);
                        units = " Kb";
                    }
                    else
                    {
                        the_size = Convert.ToInt64(sizeInBytes);
                        units = " bytes";
                    }
                }
            }
        }

        if (units != "bytes")
        {
            result = the_size.ToString() + " " + units;
            if (result.Length < 7)
            {
                StringBuilder str = new StringBuilder();
                int count = 7 - result.Length;
                for (int i = 0; i < count; i++)
                {
                    str.Append(" ");
                }
                result = str.ToString() + result;
            }

        }
        else
        {
            result = the_size.ToString() + "" + units;
            if (result.Length < 7)
            {
                StringBuilder str = new StringBuilder();
                int count = 7 - result.Length;
                for (int i = 0; i < count; i++)
                {
                    str.Append(" ");
                }
                result = str.ToString() + result;
            }

        }
        return result;
    }

    public  string RemoveUnnecessaryHtmlTagHtml(string html)
    {
        string html1 = RemoveHtmlEvent(html);
        string method = "onblur|ondatabinding|ondblclick|ondisposed|onfocus|oninit|onkeydown|onkeypress|onkeyup|onload|onmousedown|onmousemove|onmouseout|onmouseover|onmouseup|onprerender|onserverclick|onunload|document.getElementById()|document.getElementsByName()|document.documentElement()|document.createComment()|document.createDocumentFragment()|document.createElement()|document.createTextNode()|document.writeln()|document.write()|alert()";
        string acceptable = "map|strong|b|u|sup|sub|ol|ul|li|br|h2|h3|h4|h5|h6|head|hr|link|p|table|tbody|tr|td|tfoot|th|thead|title|id|style|class|span|div|p|a|img|blockquote|center|col|font|map";
        string stringPattern = "</?(?(?=" + acceptable + ")notag|[a-zA-Z0-9]+)(?:\\s[a-zA-Z0-9\\-]+=?(?:([\",']?).*?\\1?)?)*\\s*/?>|(?=" + method + ")";
        return Regex.Replace(html1, stringPattern, "");

        // return  HttpContext.Current.Server.HtmlDecode(html1);
    }


    public static string RemoveHtmlEvent(string htm)
    {
        string removableEvent = "onblur|ondatabinding|ondblclick|ondisposed|onfocus|oninit|onkeydown|onkeypress|onkeyup|onload|onmousedown|onmousemover|onmouseout|onmouseover|onmouseup|onprerender|onserverclick|onunload|document.getElementById()|document.getElementsByName()|document.documentElement()|document.createComment()|document.createDocumentFragment()|document.createElement()|document.createTextNode()|document.writeln()|document.write()|alert()|script";
        return Regex.Replace(htm, removableEvent, "");
    }

    #region Function to get Meta Language

    public DataSet getMetaLanguage()
    {
        try
        {

            return ncmdbObject.ExecuteDataSet("Asp_GetMetaLanguage");
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



    #region function to get mobile numbers to send sms.

    public DataSet GetMobileNumberForSendingSms(PetitionOB petObject)
    {
        try
        {

            ncmdbObject.AddParameter("@emailid", petObject.ApplicantEmail);
            ncmdbObject.AddParameter("@deptt_id", petObject.DepttId);
            ncmdbObject.AddParameter("@ModuleID", petObject.ModuleID);
            return ncmdbObject.ExecuteDataSet("asp_GetUserMobileNumbers");
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

    public void Sendsms(string Msg,string UserName,string MobileNumber,string text)
    {
        string senderusername = "hrercw.auth";
        string senderpassword = "Lj$*12Qh";

        string senderid = "NICSMS";
        string sURL;


        WebClient client = new WebClient(); client.Headers.Add("user-agent", "Mozilla/4.0(compatible; MSIE 6.0; Windows NT 5.2; .NET CLR1.0.3705;)");

        string textmessage =HttpUtility.HtmlDecode( text + Msg) + Environment.NewLine + " from " + UserName;
        
        textmessage = ConvertStringToHex(textmessage);
        client.QueryString.Add("message", Msg);

        string baseurl = "https://smsgw.sms.gov.in/failsafe/HttpLink?username=" + senderusername + "&pin=" + senderpassword + "&message=" + textmessage + "&mnumber=" + "91" + MobileNumber + "&signature=" + senderid + "&msgType=UC";

        BypassCertificateError();

        Stream data = client.OpenRead(baseurl);

        BypassCertificateError();

        StreamReader reader = new StreamReader(data);

        string s = reader.ReadToEnd();

        data.Close();

        reader.Close();
    }


    public void SendsmsApprove(string Msg, string MobileNumber, string text)
    {
        string senderusername = "hrercw.auth";
        string senderpassword = "Lj$*12Qh";

        string senderid = "NICSMS";
        string sURL;


        WebClient client = new WebClient(); client.Headers.Add("user-agent", "Mozilla/4.0(compatible; MSIE 6.0; Windows NT 5.2; .NET CLR1.0.3705;)");

        string textmessage = HttpUtility.HtmlDecode(text + Msg) + Environment.NewLine;
        textmessage = ConvertStringToHex(textmessage);
        
        client.QueryString.Add("message", Msg);

        string baseurl = "https://smsgw.sms.gov.in/failsafe/HttpLink?username=" + senderusername + "&pin=" + senderpassword + "&message=" + textmessage + "&mnumber=" + "91" + MobileNumber + "&signature=" + senderid + "&msgType=UC";

        BypassCertificateError();

        Stream data = client.OpenRead(baseurl);

        BypassCertificateError();

        StreamReader reader = new StreamReader(data);

        string s = reader.ReadToEnd();

        data.Close();

        reader.Close();
    }


    public static void BypassCertificateError()
    {

        ServicePointManager.ServerCertificateValidationCallback +=



          delegate(object sender,

               System.Security.Cryptography.X509Certificates.X509Certificate certificate,

               System.Security.Cryptography.X509Certificates.X509Chain chain,

                System.Net.Security.SslPolicyErrors sslPolicyErrors)
          {

              return true;

          };

    }


    public static string ConvertStringToHex(string asciiString)
    {
        string hex = "";
        foreach (char c in asciiString)
        {
            int tmp = c;
            hex += String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString())).PadLeft(4,'0');
        }
        return hex;
    }

    public static string getclientIP()
    {
        string result = string.Empty;
        string ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (!string.IsNullOrEmpty(ip))
        {
            string[] ipRange = ip.Split(',');
            int le = ipRange.Length - 1;
            result = ipRange[0];
        }
        else
        {
            result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }

        return result;
    }
}
