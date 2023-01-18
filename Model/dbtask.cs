
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace taskproject.Model
{
    [Table("Task")]
    public class dbtask
    {
        public string TaskCategory { get; set; }
        public string SelectModule { get; set; }
        public string SelectName { get; set; }
        [Key]
        public int TaskID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime DueTime { get; set; }
        public DateTime ReminderDate { get; set; }
        public DateTime ReminderTime { get; set; }
        public string Notify { get; set; }
        public string AssignTo { get; set; }
    }
}
