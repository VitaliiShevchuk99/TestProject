using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Data.Models
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string SendedMessage { get; set; }
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }
        public DateTime MessageTime { get; set; }
    }
}
