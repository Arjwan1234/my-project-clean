namespace GAMA.CO5.Models
{
    public class DTIViewModel
    {
        public string PageTitle { get; set; } = "مؤشر التحول الرقمي";
        public DTIForm? Form { get; set; }
        public DTIResult? Result { get; set; }
    }

    public class DTIForm
    {
        public int CloudUsage { get; set; }
        public int DigitalStrategy { get; set; }
        public int CyberSecurity { get; set; }
    }

    public class DTIResult
    {
        public int Score { get; set; }
        public string Level { get; set; } = string.Empty;
        public string Recommendation { get; set; } = string.Empty;
    }
}
