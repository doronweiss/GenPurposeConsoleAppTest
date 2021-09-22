
namespace IPChanger {
  partial class Form1 {
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.adapterTB = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.isDHCPCHB = new System.Windows.Forms.CheckBox();
      this.label2 = new System.Windows.Forms.Label();
      this.ipTB = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.gatewayTB = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.dns1TB = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.dns2TB = new System.Windows.Forms.TextBox();
      this.doBTN = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // adapterTB
      // 
      this.adapterTB.Location = new System.Drawing.Point(87, 5);
      this.adapterTB.Name = "adapterTB";
      this.adapterTB.Size = new System.Drawing.Size(219, 23);
      this.adapterTB.TabIndex = 0;
      this.adapterTB.Text = "Ethernet";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(11, 8);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(49, 15);
      this.label1.TabIndex = 1;
      this.label1.Text = "Adapter";
      // 
      // isDHCPCHB
      // 
      this.isDHCPCHB.AutoSize = true;
      this.isDHCPCHB.Location = new System.Drawing.Point(11, 35);
      this.isDHCPCHB.Name = "isDHCPCHB";
      this.isDHCPCHB.Size = new System.Drawing.Size(69, 19);
      this.isDHCPCHB.TabIndex = 2;
      this.isDHCPCHB.Text = "Is DHCP";
      this.isDHCPCHB.UseVisualStyleBackColor = true;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(11, 67);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(17, 15);
      this.label2.TabIndex = 4;
      this.label2.Text = "IP";
      // 
      // ipTB
      // 
      this.ipTB.Location = new System.Drawing.Point(87, 64);
      this.ipTB.Name = "ipTB";
      this.ipTB.Size = new System.Drawing.Size(219, 23);
      this.ipTB.TabIndex = 3;
      this.ipTB.Text = "192.168.12.78";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(11, 96);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(51, 15);
      this.label3.TabIndex = 6;
      this.label3.Text = "gateway";
      // 
      // gatewayTB
      // 
      this.gatewayTB.Location = new System.Drawing.Point(87, 93);
      this.gatewayTB.Name = "gatewayTB";
      this.gatewayTB.Size = new System.Drawing.Size(219, 23);
      this.gatewayTB.TabIndex = 5;
      this.gatewayTB.Text = "255.255.255.0";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(11, 125);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(36, 15);
      this.label4.TabIndex = 8;
      this.label4.Text = "DNS1";
      // 
      // dns1TB
      // 
      this.dns1TB.Location = new System.Drawing.Point(87, 122);
      this.dns1TB.Name = "dns1TB";
      this.dns1TB.Size = new System.Drawing.Size(219, 23);
      this.dns1TB.TabIndex = 7;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(11, 154);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(36, 15);
      this.label5.TabIndex = 10;
      this.label5.Text = "DNS2";
      // 
      // dns2TB
      // 
      this.dns2TB.Location = new System.Drawing.Point(87, 151);
      this.dns2TB.Name = "dns2TB";
      this.dns2TB.Size = new System.Drawing.Size(219, 23);
      this.dns2TB.TabIndex = 9;
      // 
      // doBTN
      // 
      this.doBTN.Location = new System.Drawing.Point(11, 190);
      this.doBTN.Name = "doBTN";
      this.doBTN.Size = new System.Drawing.Size(75, 23);
      this.doBTN.TabIndex = 11;
      this.doBTN.Text = "Change";
      this.doBTN.UseVisualStyleBackColor = true;
      this.doBTN.Click += new System.EventHandler(this.doBTN_Click);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(373, 226);
      this.Controls.Add(this.doBTN);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.dns2TB);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.dns1TB);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.gatewayTB);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.ipTB);
      this.Controls.Add(this.isDHCPCHB);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.adapterTB);
      this.Name = "Form1";
      this.Text = "Form1";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox adapterTB;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.CheckBox isDHCPCHB;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox ipTB;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox gatewayTB;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox dns1TB;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox dns2TB;
    private System.Windows.Forms.Button doBTN;
  }
}

