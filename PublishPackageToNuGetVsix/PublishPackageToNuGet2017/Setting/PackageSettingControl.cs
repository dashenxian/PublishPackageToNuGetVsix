﻿using PublishPackageToNuGet2017.Service;
using System;
using System.Linq;
using System.Windows.Forms;

namespace PublishPackageToNuGet2017.Setting
{
    public partial class PackageSettingControl : UserControl
    {
        internal OptionPageGrid OptionPage;

        public PackageSettingControl()
        {
            InitializeComponent();
        }

        public void Initialize()
        {
            try
            {
                var sources = NuGetPkgPublishService.GetAllPackageSources();
                OptionPage.AllPackageSource = sources.Select(n => n.Value).ToList();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }

            txtAuthour.Text = OptionPage.Authour;
            txtPublishKey.Text = OptionPage.PublishKey;

            if (OptionPage.AllPackageSource != null && OptionPage.AllPackageSource.Any())
            {
                cbPackageSource.Items.Clear();
                foreach (var sc in OptionPage.AllPackageSource)
                {
                    cbPackageSource.Items.Add(sc);
                }

                if (string.IsNullOrWhiteSpace(OptionPage.DefaultPackageSource) || !OptionPage.AllPackageSource.Contains(OptionPage.DefaultPackageSource))
                {
                    cbPackageSource.SelectedIndex = 0;
                }
                else
                {
                    cbPackageSource.SelectedItem = OptionPage.DefaultPackageSource;
                }
            }
        }

        public void SavePackageSource(object sender, EventArgs e)
        {
            OptionPage.DefaultPackageSource = cbPackageSource.SelectedItem.ToString();
        }

        public void SaveAuthour(object sender, EventArgs e)
        {
            OptionPage.Authour = txtAuthour.Text;
        }

        public void SavePublishKey(object sender, EventArgs e)
        {
            OptionPage.PublishKey = txtPublishKey.Text;
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            Initialize();
        }
    }
}
