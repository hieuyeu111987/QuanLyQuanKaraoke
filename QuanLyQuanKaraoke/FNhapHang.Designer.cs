namespace QuanLyQuanKaraoke
{
    partial class FNhapHang
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbbTenDichVu = new System.Windows.Forms.ComboBox();
            this.txtGiaNhap = new System.Windows.Forms.TextBox();
            this.btNhap = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbbTenKho = new System.Windows.Forms.ComboBox();
            this.txtSoLuong = new System.Windows.Forms.TextBox();
            this.btThoat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ten dich vu";
            // 
            // cbbTenDichVu
            // 
            this.cbbTenDichVu.FormattingEnabled = true;
            this.cbbTenDichVu.Location = new System.Drawing.Point(81, 12);
            this.cbbTenDichVu.Name = "cbbTenDichVu";
            this.cbbTenDichVu.Size = new System.Drawing.Size(186, 21);
            this.cbbTenDichVu.TabIndex = 1;
            // 
            // txtGiaNhap
            // 
            this.txtGiaNhap.Location = new System.Drawing.Point(81, 66);
            this.txtGiaNhap.Name = "txtGiaNhap";
            this.txtGiaNhap.Size = new System.Drawing.Size(186, 20);
            this.txtGiaNhap.TabIndex = 2;
            // 
            // btNhap
            // 
            this.btNhap.Location = new System.Drawing.Point(81, 118);
            this.btNhap.Name = "btNhap";
            this.btNhap.Size = new System.Drawing.Size(90, 30);
            this.btNhap.TabIndex = 3;
            this.btNhap.Text = "Nhap";
            this.btNhap.UseVisualStyleBackColor = true;
            this.btNhap.Click += new System.EventHandler(this.btNhap_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Ten kho";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Gia nhap";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "So luong";
            // 
            // cbbTenKho
            // 
            this.cbbTenKho.FormattingEnabled = true;
            this.cbbTenKho.Location = new System.Drawing.Point(81, 39);
            this.cbbTenKho.Name = "cbbTenKho";
            this.cbbTenKho.Size = new System.Drawing.Size(186, 21);
            this.cbbTenKho.TabIndex = 7;
            // 
            // txtSoLuong
            // 
            this.txtSoLuong.Location = new System.Drawing.Point(81, 92);
            this.txtSoLuong.Name = "txtSoLuong";
            this.txtSoLuong.Size = new System.Drawing.Size(186, 20);
            this.txtSoLuong.TabIndex = 8;
            // 
            // btThoat
            // 
            this.btThoat.Location = new System.Drawing.Point(177, 118);
            this.btThoat.Name = "btThoat";
            this.btThoat.Size = new System.Drawing.Size(90, 30);
            this.btThoat.TabIndex = 9;
            this.btThoat.Text = "Thoat";
            this.btThoat.UseVisualStyleBackColor = true;
            this.btThoat.Click += new System.EventHandler(this.btThoat_Click);
            // 
            // FNhapHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 164);
            this.Controls.Add(this.btThoat);
            this.Controls.Add(this.txtSoLuong);
            this.Controls.Add(this.cbbTenKho);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btNhap);
            this.Controls.Add(this.txtGiaNhap);
            this.Controls.Add(this.cbbTenDichVu);
            this.Controls.Add(this.label1);
            this.Name = "FNhapHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nhap hang";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbbTenDichVu;
        private System.Windows.Forms.TextBox txtGiaNhap;
        private System.Windows.Forms.Button btNhap;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbbTenKho;
        private System.Windows.Forms.TextBox txtSoLuong;
        private System.Windows.Forms.Button btThoat;
    }
}