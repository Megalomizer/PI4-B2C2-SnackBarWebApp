using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackbarB2C2PI4_LeviFunk_ClassLibrary
{
    public class OrderProduct
    {
        #region Properties

        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }

        #endregion
    }
}
