using LTADotNetTrainingBatch3.Database.AppDbContextModels;
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
    public partial class frmProduct : Form
    {
        public frmProduct()
        {
            InitializeComponent();
            dgvData.AutoGenerateColumns = false;
        }
        private readonly AppDbContext db = new AppDbContext();
        private int editId = 0;
        private void frmProduct_Load(object sender, EventArgs e)
        {
            bindData();


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (editId == 0) {
                Save();
            }
            else
            {
                Edit();
            }

            ClearFields();
            bindData();

        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex < -1)
                return;
            
            if(e.ColumnIndex == 0) //edit button
            {
                string value = dgvData.Rows[e.RowIndex].Cells["colProductID"].Value.ToString();
                int id = Convert.ToInt32(value);
                var item = db.TblProducts
                            .Where(x=> x.DeleteFlag == false)
                            .FirstOrDefault(x=> x.ProductId == id);
                if(item is null)
                {
                    MessageBox.Show("Product not found.");
                    bindData();
                    return;
                }

                txtProductName.Text = item.ProductName;
                txtQty.Text = item.Quantity.ToString(); 
                txtPrice.Text = item.Price.ToString();
                editId = item.ProductId;
            }
            else if (e.ColumnIndex == 1)
            {
                var result = MessageBox.Show("Are you sure to delete this record?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(result == DialogResult.No)
                {
                    return;
                }

               
                int id = Convert.ToInt32(dgvData.Rows[e.RowIndex].Cells["colProductID"].Value);
                var item = db.TblProducts
                            .Where(x => x.DeleteFlag == false)
                            .FirstOrDefault(x => x.ProductId == id);
                if (item is null)
                {
                    MessageBox.Show("Product not found.");
                    bindData();
                    return;
                }
                item.DeleteFlag = true;

                int value= db.SaveChanges();

                string message = value > 0 ? "Product updated successfully." : "Failed to update product.";
                MessageBox.Show(message);

                bindData();
                editId = item.ProductId;

            }
        }



        //helper function
        void ClearFields()
        {
            txtPrice.Clear();
            txtProductName.Clear();
            txtQty.Clear();
            txtProductName.Focus();
        }

        void bindData()
        {
            dgvData.DataSource = db.TblProducts
                .Where(e => e.DeleteFlag == false)
                .OrderByDescending(e => e.ProductId)
                .ToList();
        }

        private void Save()
        {
            string productName = txtProductName.Text.Trim();
            int qty = Convert.ToInt32(txtQty.Text.Trim());
            decimal price = Convert.ToDecimal(txtPrice.Text.Trim());

            TblProduct newProduct = new TblProduct
            {
                ProductName = productName,
                Quantity = qty,
                Price = price,
                CreatedDateTime = DateTime.Now,
                DeleteFlag = false
            };

            db.TblProducts.Add(newProduct);
            int result = db.SaveChanges();
            string message = result > 0 ? "Product saved successfully." : "Failed to save product.";
            MessageBox.Show(message);
        }

        private void Edit()
        {
            string productName = txtProductName.Text.Trim();
            int qty = Convert.ToInt32(txtQty.Text.Trim());
            decimal price = Convert.ToDecimal(txtPrice.Text.Trim());

            var item = db.TblProducts.Where(x => x.DeleteFlag == false)
                        .FirstOrDefault(x => x.ProductId == editId);

            item.ProductName = productName;
            item.Quantity = qty;
            item.Price = price;
            item.ModifiedDateTime = DateTime.Now;


            int result = db.SaveChanges();
            string message = result > 0 ? "Product updated successfully." : "Failed to update product.";
            MessageBox.Show(message);

            editId = 0;
        }

    }
}
