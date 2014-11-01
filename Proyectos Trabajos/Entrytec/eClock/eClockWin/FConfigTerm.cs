using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using System.Net;

namespace eClockWin
{
    public partial class FConfigTerm : Form
    {
        private int m_Handle;

        private uint m_DeviceID;
        private uint m_DeviceAddr;

        BSSDK_NS.BSSDK.BESysInfoData m_SysInfo;
        BSSDK_NS.BSSDK.BEConfigData m_Config;
        public int Puerto = 0;

        public FConfigTerm()
        {
            InitializeComponent();
        }
        public void SetDevice(int handle, uint deviceID, uint deviceAddr)
        {
            m_Handle = handle;
            m_DeviceID = deviceID;
            m_DeviceAddr = deviceAddr;

        }



        private void DHCP_CheckedChanged(object sender, EventArgs e)
        {
            if (DHCP.Checked)
            {
                ipAddr.Enabled = false;
                gateway.Enabled = false;
                subnetMask.Enabled = false;
            }
            else
            {
                ipAddr.Enabled = true;
                gateway.Enabled = true;
                subnetMask.Enabled = true;
            }
        }

        private void writeConfig_Click(object sender, EventArgs e)
        {
            m_Config.useDHCP = DHCP.Checked;
            m_Config.useServer = useServer.Checked;
            m_Config.synchTime = synchTime.Checked;
            try
            {
                IPAddress addr = IPAddress.Parse(ipAddr.Text);
                m_Config.ipAddr = (uint)addr.Address;

                addr = IPAddress.Parse(gateway.Text);
                m_Config.gateway = (uint)addr.Address;

                addr = IPAddress.Parse(subnetMask.Text);
                m_Config.subnetMask = (uint)addr.Address;

                addr = IPAddress.Parse(serverIP.Text);
                m_Config.serverIpAddr = (uint)addr.Address;

                m_Config.port = Int32.Parse(port.Text);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("El campo no es numérico o no es IP valida", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            int configSize = Marshal.SizeOf(m_Config);
            IntPtr config = Marshal.AllocHGlobal(configSize);

            Marshal.StructureToPtr(m_Config, config, true);

            Cursor.Current = Cursors.WaitCursor;

            int result = BSSDK_NS.BSSDK.BS_WriteConfigUDP(m_Handle, m_DeviceAddr, m_DeviceID, BSSDK_NS.BSSDK.BEPLUS_CONFIG, configSize, config);

            Marshal.FreeHGlobal(config);

            Cursor.Current = Cursors.Default;

            if (result != BSSDK_NS.BSSDK.BS_SUCCESS)
            {
                MessageBox.Show("No se pudo guardar la configuración", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            ReadSysInfo();
        }

        private void FConfigTerm_Load(object sender, EventArgs e)
        {
            deviceInfo.Text = (m_DeviceAddr & 0xff) + ".";
            deviceInfo.Text += ((m_DeviceAddr >> 8) & 0xff) + ".";
            deviceInfo.Text += ((m_DeviceAddr >> 16) & 0xff) + ".";
            deviceInfo.Text += ((m_DeviceAddr >> 24) & 0xff);
            deviceInfo.Text += " (" + m_DeviceID + ")";

            if (ReadSysInfo())
            {
                ReadConfig();
            }
        }
        public bool ReadConfig()
        {
            int configSize = 0;

            IntPtr config = Marshal.AllocHGlobal(Marshal.SizeOf(m_Config));

            Cursor.Current = Cursors.WaitCursor;

            int result = BSSDK_NS.BSSDK.BS_ReadConfigUDP(m_Handle, m_DeviceAddr, m_DeviceID, BSSDK_NS.BSSDK.BEPLUS_CONFIG, ref configSize, config);

            Cursor.Current = Cursors.Default;

            if (result != BSSDK_NS.BSSDK.BS_SUCCESS)
            {
                MessageBox.Show("No se pudo leer la configuración", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Marshal.FreeHGlobal(config);
                return false;
            }

            m_Config = (BSSDK_NS.BSSDK.BEConfigData)Marshal.PtrToStructure(config, typeof(BSSDK_NS.BSSDK.BEConfigData));

            Marshal.FreeHGlobal(config);

            DHCP.Checked = m_Config.useDHCP;
            useServer.Checked = m_Config.useServer;
            synchTime.Checked = m_Config.synchTime;

            IPAddress addr = new IPAddress(m_Config.ipAddr);
            ipAddr.Text = addr.ToString();

            addr.Address = m_Config.gateway;
            gateway.Text = addr.ToString();

            addr.Address = m_Config.subnetMask;
            subnetMask.Text = addr.ToString();

            addr.Address = m_Config.serverIpAddr;
            serverIP.Text = addr.ToString();

            port.Text = m_Config.port.ToString();
            Puerto = m_Config.port;
            if (DHCP.Checked)
            {
                ipAddr.Enabled = false;
                gateway.Enabled = false;
                subnetMask.Enabled = false;
            }
            else
            {
                ipAddr.Enabled = true;
                gateway.Enabled = true;
                subnetMask.Enabled = true;
            }

            return true;
        }

        private bool ReadSysInfo()
        {
            int configSize = 0;

            IntPtr sysInfo = Marshal.AllocHGlobal(Marshal.SizeOf(m_SysInfo));

            Cursor.Current = Cursors.WaitCursor;

            int result = BSSDK_NS.BSSDK.BS_ReadConfigUDP(m_Handle, m_DeviceAddr, m_DeviceID, BSSDK_NS.BSSDK.BEPLUS_CONFIG_SYS_INFO, ref configSize, sysInfo);

            Cursor.Current = Cursors.Default;

            if (result != BSSDK_NS.BSSDK.BS_SUCCESS)
            {
                MessageBox.Show("No se pudo leer la información del dispositivo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Marshal.FreeHGlobal(sysInfo);
                return false;
            }

            m_SysInfo = (BSSDK_NS.BSSDK.BESysInfoData)Marshal.PtrToStructure(sysInfo, typeof(BSSDK_NS.BSSDK.BESysInfoData));

            Marshal.FreeHGlobal(sysInfo);

            deviceID.Text = String.Format("{0}", m_SysInfo.ID);

            MAC.Text = "";

            int i = 0;
            for (i = 0; i < 5; i++)
            {
                MAC.Text += m_SysInfo.macAddr[i].ToString("X2") + ":";
            }
            MAC.Text += m_SysInfo.macAddr[i].ToString("X2");

            FWVersion.Text = Encoding.ASCII.GetString(m_SysInfo.firmwareVer);

            return true;
        }

        private void refreshConfig_Click(object sender, EventArgs e)
        {
            ReadConfig();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}