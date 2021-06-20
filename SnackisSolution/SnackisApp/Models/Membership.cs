using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnackisApp.Models
{
    public class Membership
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int GroupId { get; set; }

        public bool IsAccepted { get; set; }
    }
}
