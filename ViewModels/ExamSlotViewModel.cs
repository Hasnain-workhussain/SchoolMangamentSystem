public class ExamSlotViewModel
{
    public string ExamName { get; set; }
    public DateTime ExamDate { get; set; }
    public string CourseName { get; set; }

    public ExamSlotViewModel(string examName, DateTime examDate, string courseName)
    {
        ExamName = examName;
        ExamDate = examDate;
        CourseName = courseName;
    }
}
