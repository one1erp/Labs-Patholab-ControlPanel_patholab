namespace ControlPanel_patholab
{
    partial class ControlPanel_patholab
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlPanel_patholab));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.buttonPrint = new System.Windows.Forms.Button();
            this.bottonCopyToCilipboard = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.EndedQueriesLabel = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.ButtonCreateExcelFile = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonPrint
            // 
            this.buttonPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPrint.Image = ((System.Drawing.Image)(resources.GetObject("buttonPrint.Image")));
            this.buttonPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonPrint.Location = new System.Drawing.Point(444, 9);
            this.buttonPrint.Name = "buttonPrint";
            this.buttonPrint.Size = new System.Drawing.Size(97, 51);
            this.buttonPrint.TabIndex = 4;
            this.buttonPrint.Text = "הדפס";
            this.buttonPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonPrint.UseVisualStyleBackColor = true;
            this.buttonPrint.Click += new System.EventHandler(this.buttonPrint_Click);
            // 
            // bottonCopyToCilipboard
            // 
            this.bottonCopyToCilipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bottonCopyToCilipboard.Image = ((System.Drawing.Image)(resources.GetObject("bottonCopyToCilipboard.Image")));
            this.bottonCopyToCilipboard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bottonCopyToCilipboard.Location = new System.Drawing.Point(581, 9);
            this.bottonCopyToCilipboard.Name = "bottonCopyToCilipboard";
            this.bottonCopyToCilipboard.Size = new System.Drawing.Size(115, 51);
            this.bottonCopyToCilipboard.TabIndex = 3;
            this.bottonCopyToCilipboard.Text = "העתק ללוח";
            this.bottonCopyToCilipboard.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bottonCopyToCilipboard.UseVisualStyleBackColor = true;
            this.bottonCopyToCilipboard.Click += new System.EventHandler(this.bottonCopyToCilipboard_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.EndedQueriesLabel);
            this.panel1.Controls.Add(this.statusLabel);
            this.panel1.Controls.Add(this.ButtonCreateExcelFile);
            this.panel1.Controls.Add(this.bottonCopyToCilipboard);
            this.panel1.Controls.Add(this.buttonPrint);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(911, 69);
            this.panel1.TabIndex = 5;
            // 
            // EndedQueriesLabel
            // 
            this.EndedQueriesLabel.Location = new System.Drawing.Point(12, 42);
            this.EndedQueriesLabel.Name = "EndedQueriesLabel";
            this.EndedQueriesLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.EndedQueriesLabel.Size = new System.Drawing.Size(320, 27);
            this.EndedQueriesLabel.TabIndex = 13;
            this.EndedQueriesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusLabel
            // 
            this.statusLabel.Location = new System.Drawing.Point(12, 9);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusLabel.Size = new System.Drawing.Size(320, 33);
            this.statusLabel.TabIndex = 12;
            this.statusLabel.Text = "מריץ שאילתות במקביל ";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ButtonCreateExcelFile
            // 
            this.ButtonCreateExcelFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonCreateExcelFile.Image = ((System.Drawing.Image)(resources.GetObject("ButtonCreateExcelFile.Image")));
            this.ButtonCreateExcelFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButtonCreateExcelFile.Location = new System.Drawing.Point(736, 9);
            this.ButtonCreateExcelFile.Name = "ButtonCreateExcelFile";
            this.ButtonCreateExcelFile.Size = new System.Drawing.Size(147, 51);
            this.ButtonCreateExcelFile.TabIndex = 14;
            this.ButtonCreateExcelFile.Text = "צור קובץ אקסל";
            this.ButtonCreateExcelFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ButtonCreateExcelFile.UseVisualStyleBackColor = true;
            this.ButtonCreateExcelFile.Click += new System.EventHandler(this.ButtonCreateExcelFile_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView2.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView2.Location = new System.Drawing.Point(0, 69);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(911, 521);
            this.dataGridView2.TabIndex = 7;
            // 
            // ControlPanel_patholab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.panel1);
            this.Name = "ControlPanel_patholab";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(911, 590);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonPrint;
        private System.Windows.Forms.Button bottonCopyToCilipboard;
        private System.Windows.Forms.Panel panel1;
   
        private System.Windows.Forms.Button ButtonCreateExcelFile;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label EndedQueriesLabel;
    }






}


