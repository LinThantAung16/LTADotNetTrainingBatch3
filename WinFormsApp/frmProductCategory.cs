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
    public partial class frmProductCategory : Form
    {
        public frmProductCategory()
        {
            InitializeComponent();
            dgvData.AutoGenerateColumns = false;
        }

        private readonly AppDbContext db = new AppDbContext();
        private int editId = 0;

        private void frmProductCategory_Load(object sender, EventArgs e)
        {
            bindData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (editId == 0)
            {
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
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == 0) // Edit
            {
                int id = Convert.ToInt32(dgvData.Rows[e.RowIndex].Cells["colCategoryID"].Value);
                var item = db.TblProductCategories.FirstOrDefault(x => x.ProductCategoryId == id);

                if (item == null)
                {
                    MessageBox.Show("Category not found.");
                    bindData();
                    return;
                }

                txtCategoryCode.Text = item.ProductCategoryCode;
                txtCategoryName.Text = item.ProductCategoryName;
                editId = item.ProductCategoryId;
            }
            else if (e.ColumnIndex == 1) // Delete
            {
                var result = MessageBox.Show("Are you sure to delete this record?", "",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.No) return;

                int id = Convert.ToInt32(dgvData.Rows[e.RowIndex].Cells["colCategoryID"].Value);
                var item = db.TblProductCategories.FirstOrDefault(x => x.ProductCategoryId == id);

                if (item == null)
                {
                    MessageBox.Show("Category not found.");
                    bindData();
                    return;
                }

                db.TblProductCategories.Remove(item);
                int value = db.SaveChanges();

                MessageBox.Show(value > 0 ? "Category deleted successfully." : "Failed to delete.");
                bindData();
            }
        }

        void ClearFields()
        {
            txtCategoryCode.Clear();
            txtCategoryName.Clear();
            txtCategoryCode.Focus();
            editId = 0;
        }

        void bindData()
        {
            dgvData.DataSource = db.TblProductCategories
                .OrderByDescending(x => x.ProductCategoryId)
                .ToList();
        }

        private void Save()
        {
            var item = new TblProductCategory
            {
                ProductCategoryCode = txtCategoryCode.Text.Trim(),
                ProductCategoryName = txtCategoryName.Text.Trim()
            };

            db.TblProductCategories.Add(item);
            int result = db.SaveChanges();

            MessageBox.Show(result > 0 ? "Category saved successfully." : "Failed to save.");
        }

        private void Edit()
        {
            var item = db.TblProductCategories.FirstOrDefault(x => x.ProductCategoryId == editId);

            if (item == null) return;

            item.ProductCategoryCode = txtCategoryCode.Text.Trim();
            item.ProductCategoryName = txtCategoryName.Text.Trim();

            int result = db.SaveChanges();
            MessageBox.Show(result > 0 ? "Category updated successfully." : "Failed to update.");

            editId = 0;
        }
    }
}
