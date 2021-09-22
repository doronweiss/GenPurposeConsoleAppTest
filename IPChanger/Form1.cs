using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IPChanger {
  public partial class Form1 : Form {
    public Form1() {
      InitializeComponent();
    }

    private void doBTN_Click(object sender, EventArgs e) {
      bool res = IPChangeUtils.SetNetworkConfig(adapterTB.Text, isDHCPCHB.Checked, ipTB.Text, gatewayTB.Text, dns1TB.Text, dns2TB.Text);
      MessageBox.Show($"Returned: {res}");
    }
  }
}
