<%@ WebHandler Language="C#" Class="FeedbackUploaderHandler" %>

using System;
using System.Web;
using System.IO;

public class FeedbackUploaderHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Expires = -1;
        try
        {
            HttpPostedFile postedFile = context.Request.Files["Filedata"];

            string savepath = "";
            string tempPath = "";
            //tempPath = context.Request["folder"];
            tempPath = "UploadedFeedback";
                

            savepath = context.Server.MapPath(tempPath);
            string fileName = postedFile.FileName;
            string strUniqueFileName = "";
            if (fileName.Length > 0)
            {
                //strUniqueFileName = clsExtensionUniqueFileName.GetUniqueFileName(fileName);
                strUniqueFileName = fileName;
                
            }

            if (!Directory.Exists(savepath))
                Directory.CreateDirectory(savepath);

            postedFile.SaveAs(savepath + @"\" + strUniqueFileName);
            context.Response.Write(tempPath + "/" + strUniqueFileName);
            context.Response.StatusCode = 200;
        }
        catch (Exception ex)
        {
            context.Response.Write("Error: " + ex.Message);
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}

public static class clsExtensionUniqueFileName
{
    public static string GetUniqueFileName(string fileName)
    {
        string strReturnUnique = string.Concat(
            Path.GetFileNameWithoutExtension(fileName),
            DateTime.Now.ToString("yyyyMMddHHmmssfff"),
            Path.GetExtension(fileName)
            );
        return strReturnUnique;
    }
}