namespace PlusOffSet.Models
{
    public class job_master
    {

        public int Id { get; set; }
        public string? job_title { get; set; }
        public string? job_item { get; set; }
        public string? job_size { get; set;}
        public string? job_machine { get; set; }
        public int job_qty { get; set;}
        public string? job_details { get; set;}
        public string? job_paper_type { get; set; }
        public string? job_paper_size { get; set; }
        public string? job_GSM { get; set; }
        public string? job_paper_quality { get; set; }
        public string? job_paper_no_of_sheets { get; set; }
        public string? job_paper_by { get; set; }
        public string? job_printing_no_of_sheets { get; set; }
        public string? job_printing_option { get; set; }
        public string? job_printing_no_of_impression { get; set; }
        public int job_printing_no_of_form { get; set; }
        public string? job_printing_GSM { get; set; }
        public string? job_printing_size1 { get; set; }
        public string? job_printing_size2 { get; set; }
        public string? job_finishing_type { get; set; }
        public string? job_finishing_quality { get; set;}
        public string? job_finishing_area { get; set; }
        public string? job_Note { get; set; }
        public string? job_no { get; set; }
        public DateTime? created_date { get; set; }
    }
}
