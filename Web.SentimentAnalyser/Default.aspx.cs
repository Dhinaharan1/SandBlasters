using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using HelperSAUtility;
using System.Threading.Tasks;


public partial class _Default : System.Web.UI.Page
{
    List<Feedbacks> objFeedbacks = new List<Feedbacks>();
    List<Feedbacks> objBlankFeedbacks = new List<Feedbacks>();

    public string strType = "Type";
    public string strProcessUploadedFeedback = "ProcessUploadedFeedback";
    public string strFileNameAlone = "";
    public string strOutputFolder = "Output\\";
    public string strOutputStoredFullPath = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        HandleAsyncRequest();
    }

    private void HandleAsyncRequest()
    {
        try
        {
            if (Request.Form[strType] != null)
            {
                string strPostType = Convert.ToString(Request.Form[strType]);
                if (strPostType == strProcessUploadedFeedback)
                {
                    string strUpdatedFileName = Request.Form["strFileName"] != null ? Convert.ToString(Request.Form["strFileName"]) : string.Empty;
                    if (strUpdatedFileName != string.Empty)
                    {
                        strFileNameAlone = strUpdatedFileName;
                        strUpdatedFileName = Server.MapPath("UploadedFeedback\\" + strUpdatedFileName);
                        ProcessFeedback(strUpdatedFileName);
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    public void ProcessFeedback(string strUploadedFeedbackFile)
    {
        // GET LIST OF FEEDBACK
        objFeedbacks = ReadLoadedFeedback(strUploadedFeedbackFile);
        objBlankFeedbacks = objFeedbacks;

        // ANALYSE SENTIMENTS
        objFeedbacks = AnalyseSentimentPoints(objFeedbacks);
        
        // WRITE OUTPUT FILE AND DISPLAY FILE NAME ON SCREEN
        WriteOutPutFile(objFeedbacks);

        LoadFeedbackToGrid(objFeedbacks);

    }

    private void LoadFeedbackToGrid(List<Feedbacks> objFeedbacks)
    {
        gvFeedbackAnalysis.DataSource = objFeedbacks;
        gvFeedbackAnalysis.DataBind();
    }

    private void WriteOutPutFile(List<Feedbacks> objFeedbacks)
    {
        string strOutputTO = strOutputFolder + clsExtensionUniqueFileName.GetUniqueFileName(strFileNameAlone);
        string strTempFileLocation = strOutputTO;
        strOutputTO = Server.MapPath(strOutputTO);
        File.WriteAllLines(strOutputTO, objFeedbacks.Select(l => l.strResults).ToList());
        strOutputStoredFullPath = strOutputTO;
        divShowOutput.Attributes.Add("style", "display:block");
        lnkShowOutput.HRef = "~/" + strTempFileLocation;

        



    }

    private List<Feedbacks> AnalyseSentimentPoints(List<Feedbacks> objFeedbacks)
    {
        cSentimentAnalyser objSentiment = cSentimentAnalyser.Instance;
        Parallel.ForEach(objFeedbacks, feeds =>
                {
                    var intScores = objSentiment.GetScore(feeds.strFeedbackContent);
                    feeds.strScores = intScores.Sentiment.ToString();
                    if (Convert.ToInt32(feeds.strScores) < 0)
                    {
                        feeds.strResults = "Negative";
                        feeds.strEmoticons = "Images/AngrySmily.png";
                    }
                    if ((Convert.ToInt32(feeds.strScores) >= 0) && (Convert.ToInt32(feeds.strScores) <= 1))
                    {
                        feeds.strResults = "Neutral";
                        feeds.strEmoticons = "Images/NeutralSmily.png";
                    }
                    else if (Convert.ToInt32(feeds.strScores) >= 2)
                    {
                        feeds.strResults = "Positive";
                        feeds.strEmoticons = "Images/PositiveSmily.png";
                    }
                });

        return objFeedbacks;
    }

    private List<Feedbacks> ReadLoadedFeedback(string strUploadedFeedbackFile)
    {
        List<Feedbacks> objTempFeedback = new List<Feedbacks>();
        using (StreamReader reader = File.OpenText(strUploadedFeedbackFile))
        {
            string line = "";
            while ((line = reader.ReadLine()) != null)
            {
                //string[] words = line.Split(',');
                objTempFeedback.Add(new Feedbacks
                {
                    strFeedbackContent = line,
                    strResults = "",
                    strScores = ""
                });
            }
        }
        return objTempFeedback;
    }


    private IEnumerable<string> ReadLogLines(string logPath)
    {
        using (StreamReader reader = File.OpenText(logPath))
        {
            string line = "";
            while ((line = reader.ReadLine()) != null)
            {
                yield return line;
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
}

public class Feedbacks
{
    public string strFeedbackContent { get; set; }
    public string strResults { get; set; }
    public string strScores { get; set; }
    public string strEmoticons { get; set; }

}
