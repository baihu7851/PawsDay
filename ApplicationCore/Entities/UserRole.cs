using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public partial class UserRole : BaseEntity
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserRoleId { get; set; }
        public int UserId { get; set; }
        public int RoleType { get; set; }
        public virtual Member Member { get; set; }
        public virtual Role Role { get; set; }
    }
}
