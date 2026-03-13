using System;
using System.Web;

public partial class DescargableExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            HttpContext context = HttpContext.Current;
            Generales.GetInstance().LimpiaCache(ref context);

            string fileName = Request.QueryString["FileName"].ToString();

            //Read the Excel file in a byte array
            Byte[] fileBytes = (Byte[])Session["File"];

            if (fileBytes != null)
            {
                //Clear the response
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Cookies.Clear();

                //Add the header & other information
                Response.Cache.SetCacheability(HttpCacheability.Private);
                Response.CacheControl = "private";
                Response.Charset = System.Text.UTF8Encoding.UTF8.WebName;
                Response.ContentEncoding = System.Text.UTF8Encoding.UTF8;
                Response.AppendHeader("Content-Length", fileBytes.Length.ToString());
                Response.AppendHeader("Pragma", "cache");
                Response.AppendHeader("Expires", "60");
                Response.AppendHeader("Content-Disposition",
                "attachment; " +
                "filename=\"" + fileName + ".xlsx\"; " +
                "size=" + fileBytes.Length.ToString());
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                //Write it back to the client
                Response.BinaryWrite(fileBytes);
                Response.End();

                Session.Remove("File");
            }
        }
        catch (Exception ex)
        {
        }
    }
}
