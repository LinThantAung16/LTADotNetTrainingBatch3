namespace WinFormsApp
{
    partial class frmProductCategory
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            txtCategoryCode = new TextBox();
            txtCategoryName = new TextBox();
            btnSave = new Button();
            btnCancel = new Button();
            dgvData = new DataGridView();
            colEdit = new DataGridViewButtonColumn();
            colDelete = new DataGridViewButtonColumn();
            colCategoryID = new DataGridViewTextBoxColumn();
            colCategoryCode = new DataGridViewTextBoxColumn();
            colCategoryName = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvData).BeginInit();
            SuspendLayout();

            // label1 - Category Code
            label1.AutoSize = true;
            label1.Location = new Point(120, 30);
            label1.Name = "label1";
            label1.Size = new Size(135, 25);
            label1.Text = "Category Code";

            // label2 - Category Name
            label2.AutoSize = true;
            label2.Location = new Point(120, 70);
            label2.Name = "label2";
            label2.Size = new Size(145, 25);
            label2.Text = "Category Name";

            // txtCategoryCode
            txtCategoryCode.Location = new Point(270, 27);
            txtCategoryCode.Name = "txtCategoryCode";
            txtCategoryCode.Size = new Size(300, 31);

            // txtCategoryName
            txtCategoryName.Location = new Point(270, 67);
            txtCategoryName.Name = "txtCategoryName";
            txtCategoryName.Size = new Size(300, 31);

            // btnSave
            btnSave.BackColor = Color.Green;
            btnSave.Location = new Point(458, 118);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(112, 51);
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;

            // btnCancel
            btnCancel.BackColor = SystemColors.ActiveBorder;
            btnCancel.Location = new Point(273, 118);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(112, 51);
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;

            // dgvData
            dgvData.AllowUserToAddRows = false;
            dgvData.AllowUserToDeleteRows = false;
            dgvData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvData.Columns.AddRange(new DataGridViewColumn[] {
                colEdit,
                colDelete,
                colCategoryID,
                colCategoryCode,
                colCategoryName
            });
            dgvData.Dock = DockStyle.Bottom;
            dgvData.Location = new Point(0, 205);
            dgvData.Name = "dgvData";
            dgvData.ReadOnly = true;
            dgvData.RowHeadersWidth = 62;
            dgvData.Size = new Size(800, 300);
            dgvData.CellContentClick += dgvData_CellContentClick;

            // colEdit
            colEdit.HeaderText = "Edit";
            colEdit.MinimumWidth = 8;
            colEdit.Name = "colEdit";
            colEdit.ReadOnly = true;
            colEdit.Text = "Edit";
            colEdit.UseColumnTextForButtonValue = true;
            colEdit.Width = 150;

            // colDelete
            colDelete.HeaderText = "Delete";
            colDelete.MinimumWidth = 8;
            colDelete.Name = "colDelete";
            colDelete.ReadOnly = true;
            colDelete.Text = "Delete";
            colDelete.UseColumnTextForButtonValue = true;
            colDelete.Width = 150;

            // colCategoryID
            colCategoryID.DataPropertyName = "ProductCategoryId";
            colCategoryID.HeaderText = "Category ID";
            colCategoryID.MinimumWidth = 8;
            colCategoryID.Name = "colCategoryID";
            colCategoryID.ReadOnly = true;
            colCategoryID.Width = 150;

            // colCategoryCode
            colCategoryCode.DataPropertyName = "ProductCategoryCode";
            colCategoryCode.HeaderText = "Category Code";
            colCategoryCode.MinimumWidth = 8;
            colCategoryCode.Name = "colCategoryCode";
            colCategoryCode.ReadOnly = true;
            colCategoryCode.Width = 200;

            // colCategoryName
            colCategoryName.DataPropertyName = "ProductCategoryName";
            colCategoryName.HeaderText = "Category Name";
            colCategoryName.MinimumWidth = 8;
            colCategoryName.Name = "colCategoryName";
            colCategoryName.ReadOnly = true;
            colCategoryName.Width = 250;

            // frmProductCategory
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 505);
            Controls.Add(dgvData);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(txtCategoryName);
            Controls.Add(txtCategoryCode);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "frmProductCategory";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Product Category";

            Load += frmProductCategory_Load;

            ((System.ComponentModel.ISupportInitialize)dgvData).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Label label2;
        private TextBox txtCategoryCode;
        private TextBox txtCategoryName;
        private Button btnSave;
        private Button btnCancel;
        private DataGridView dgvData;
        private DataGridViewButtonColumn colEdit;
        private DataGridViewButtonColumn colDelete;
        private DataGridViewTextBoxColumn colCategoryID;
        private DataGridViewTextBoxColumn colCategoryCode;
        private DataGridViewTextBoxColumn colCategoryName;
    }
}