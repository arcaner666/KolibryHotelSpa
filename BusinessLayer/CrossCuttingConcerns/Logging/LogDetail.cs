namespace BusinessLayer.CrossCuttingConcerns.Logging;

public class LogDetail
{
    public string FullName { get; set; }
    public string MethodName { get; set; }
    public List<LogMethodParameter> MethodParameters { get; set; }
}
