using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
        }

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProduct frmProduct = new frmProduct();
            frmProduct.ShowDialog();
        }


        private void productCategoryToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmProductCategory frmProductCategory = new frmProductCategory();
            frmProductCategory.ShowDialog();
        }
    }
}
