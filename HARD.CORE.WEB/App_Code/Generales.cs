using HARD.CORE.OBJ;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;

using Telerik.Web.UI;

/// <summary>
/// Summary description for Generales
/// </summary>
public class Generales
{

    #region "Singleton"

    private static Generales instance = null;

    private static object mutex = new object();
    private Generales()
    {
    }

    public static Generales GetInstance()
    {

        if (instance == null)
        {
            lock ((mutex))
            {
                instance = new Generales();
            }
        }

        return instance;

    }

    #endregion    

    public bool LimpiaCache(ref HttpContext context, bool cargaSesion = false)
    {

        try
        {
            context.Response.Cache.SetNoServerCaching();
            context.Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            context.Response.Cache.SetNoStore();
            context.Response.Cache.SetExpires(new DateTime(1900, 1, 1, 0, 0, 0, 0));

            Page page = (Page)context.CurrentHandler;

            if (context.Session["Usuario"] == null)
            {
                context.Response.Redirect("Default.aspx");
                return false;
            }

            // Entra cuando se ingresa a una ventana emergente
            if (page.Master == null)
            {
                ScriptManager.RegisterStartupScript(page, page.GetType(), "NoBack", "window.onload = noback;", true);
                page.Header.Controls.Add(new LiteralControl("<script src='App_Scripts/jquery-3.4.1.min.js'></script>"));
                page.Header.Controls.Add(new LiteralControl("<script src='App_Scripts/Session_TimeOut_Renew.js'></script>"));
            }

            if (cargaSesion)
            {
                page.Header.Controls.Add(new LiteralControl("<script src='App_Scripts/Session_TimeOut.js'></script>"));
                page.Header.Controls.Add(new LiteralControl("<link href='App_Themes/Session_TimeOut.css' rel='stylesheet' />"));
                Usuario usuario = (Usuario)context.Session["Usuario"];
                SessionTimeOut.GetInstance().CargarSession(page.Form, context.Session.Timeout, usuario.ClaveUsuario);
            }

            return true;

        }
        catch (Exception ex)
        {
            return false;
        }

    }

    public void ConfigurarFiltros(RadGrid radGrid)
    {
        try
        {
            GridFilterMenu menu = radGrid.FilterMenu;
            string[] filtrosAplicados = {"NoFilter", "EqualTo", "NotEqualTo", "GreaterThanText", "GreaterThanOrEqualToText", "LessThanText", "LessThanOrEqualToText",
                                        "Sin filtro", "Igual a", "No es igual a", "Mayor que", "Menor que", "Mayor o igual que", "Menor o igual que"};

            foreach (RadMenuItem item in menu.Items)
            {
                if (!filtrosAplicados.Contains(item.Text))
                {
                    item.Visible = false;
                }
            }
        }
        catch
        {
            throw new Exception("Configuración de filtros - Error");
        }
    }

    public void CancelaEdicion(RadGrid radGrid)
    {
        foreach (GridDataItem item in radGrid.EditItems)
        {
            item.Edit = false;
        }
    }

    public void CancelAll(RadGrid radGrid)
    {
        foreach (GridDataItem item in radGrid.Items)
        {
            item.Edit = false;
        }
    }

    public bool CollapseAll(RadGrid radGrid)
    {
        try
        {
            foreach (GridDataItem item in radGrid.Items)
            {
                item.Expanded = false;
            }
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }

    }

    public RadGrid ObtenerRadgrid(Control c)
    {
        if ((c) is Page)
        {
            return null;
        }
        else
        {
            if ((c) is RadGrid)
            {
                return (RadGrid)c;
            }
            else
            {
                PropertyInfo nammingContainer = c.GetType().GetProperty("NammingContainer");
                return ObtenerRadgrid(c.NamingContainer);
            }
        }
    }

    public void ClearFilters(RadGrid radGrid)
    {

        foreach (GridColumn column in radGrid.MasterTableView.Columns)
        {
            column.CurrentFilterFunction = GridKnownFunction.NoFilter;
            column.CurrentFilterValue = string.Empty;
        }

        radGrid.MasterTableView.FilterExpression = string.Empty;

    }

    public RadWizardStep GetWizardStepByTitle(RadWizard radWizard, string title)
    {

        foreach (RadWizardStep step in radWizard.WizardSteps)
        {
            if (step.Title == title)
            {
                return step;
            }
        }
        return null;

    }
    public byte[] ConvertImage(string imagePath)
    {
        byte[] byteImage;

        System.Drawing.Image image = System.Drawing.Image.FromFile(imagePath);
        using (var stream = new System.IO.MemoryStream())
        {
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            byteImage = stream.ToArray();
            stream.Dispose();
            image.Dispose();
        }

        return byteImage;
    }
    public Stream ConvertImageStream(string imagePath)
    {
        Stream imageStream;

        System.Drawing.Image image = System.Drawing.Image.FromFile(imagePath);
        using (var stream = new System.IO.MemoryStream())
        {
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            imageStream = stream;
            stream.Dispose();
        }

        return imageStream;
    }
    public Bitmap ResizeImage(Stream stream)
    {
        System.Drawing.Image originalImage = Bitmap.FromStream(stream);

        int height = 150;
        int width = 150;

        Bitmap scaledImage = new Bitmap(width, height);

        using (Graphics graphics = Graphics.FromImage(scaledImage))
        {
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            graphics.DrawImage(originalImage, 0, 0, width, height);
            return scaledImage;
        }
    }
    public string GuardarImagen(Stream stream, string pathFile)
    {
        try
        {
            Image image = Image.FromStream(stream, true);
            image.Save(pathFile, ImageFormat.Png);

            return "Ok";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public List<Day> GetDaysWeek()
    {
        List<Day> dayOfWeeek = new List<Day>();

        for (int i = 1; i <= 7; i++)
        {
            string dia = "";

            switch (i)
            {
                case 7:
                    dia = "Lunes";
                    break;
                case 1:
                    dia = "Martes";
                    break;
                case 2:
                    dia = "Miercoles";
                    break;
                case 3:
                    dia = "Jueves";
                    break;
                case 4:
                    dia = "Viernes";
                    break;
                case 5:
                    dia = "Sabado";
                    break;
                case 6:
                    dia = "Domingo";
                    break;
            }

            dayOfWeeek.Add(new Day(i, dia));
        }

        return dayOfWeeek;
    }

    public class Day
    {
        public int ClaveDia { get; set; }
        public string Dia { get; set; }
        public Day(int claveDia, string dia)
        {
            ClaveDia = claveDia;
            Dia = dia;
        }
    }

}