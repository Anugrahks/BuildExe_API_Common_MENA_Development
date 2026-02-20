using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeServices.Models
{
    public class StageActivationDeActivation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public int DivisionId { get; set; }

        public int ProjectId { get; set; }
        public int OrderId { get; set; }
        public string StageName { get; set; }
        public int IsActivated { get; set; }


    }
}
