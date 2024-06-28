using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoDashboard.Api.Models
{
    [Table("cryptopairs")]
    public class CryptoPair
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("pairname")]
        [MaxLength(10)]
        public string PairName { get; set; }

        [Required]
        [Column("date")]
        public DateTime Date { get; set; }

        [Required]
        [Column("price")]
        public decimal Price { get; set; }

        [Required]
        [Column("open")]
        public decimal Open { get; set; }

        [Required]
        [Column("high")]
        public decimal High { get; set; }

        [Required]
        [Column("low")]
        public decimal Low { get; set; }

        [Required]
        [Column("volume")]
        public long Volume { get; set; }

        [Required]
        [Column("changepercentage")]
        public decimal ChangePercentage { get; set; }
    }

    public class PairAverageDifference
    {
        public string PairName { get; set; }
        public double AvgDiff { get; set; }
        public double PercentageDiff { get; set; }
    }


    public class LowAndHigh
    {
        public decimal LowestPrice { get; set; }
        public decimal HighestPrice { get; set; }
    }

    public class LogEntry
    {
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime RequestDate { get; set; }
    }

    public class ErrorLogEntry
    {
        public string Action { get; set; }
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }
        public DateTime Date { get; set; }
    }

}


