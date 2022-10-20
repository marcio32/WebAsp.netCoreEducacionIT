using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Data.Entities
{
    public class Login
    {
        public string? Mail { get; set; }
        public string? Clave { get; set; }
        public string? Token { get; set; }
        public int Codigo { get; set; }

        [NotMapped]
        public bool Google { get; set; }

    }
}
