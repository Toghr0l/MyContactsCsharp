﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyContacts
{
    public partial class Form1 : Form
    {
        IContactsRepository repository;
        public Form1()
        {
            InitializeComponent();
            repository = new ContactsRepository();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BindGrid();
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void BindGrid()
        {
            dgContacts.AutoGenerateColumns = false;
            dgContacts.Columns[0].Visible = false;
            dgContacts.DataSource = repository.SelectAll();
        }

        private void btnNewContact_Click(object sender, EventArgs e)
        {
            frmAddOrEdit frm = new frmAddOrEdit();
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                BindGrid();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgContacts.CurrentRow != null)
            {
                string name = dgContacts.CurrentRow.Cells[1].Value.ToString();
                string family = dgContacts.CurrentRow.Cells[2].Value.ToString();
                string fullname = name + " " + family;
                if (MessageBox.Show($"آیا از حذف {fullname} مطمئن هستید؟", "توجه",MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    int contactID = int.Parse(dgContacts.CurrentRow.Cells[0].Value.ToString());
                    repository.Delete(contactID);
                    BindGrid();

                }
            }
            else
            {
                MessageBox.Show("مخاطبی انتخواب نشده است", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgContacts.CurrentRow != null)
            {
                int contactId = int.Parse(dgContacts.CurrentRow.Cells[0].Value.ToString());
                frmAddOrEdit frm = new frmAddOrEdit();
                frm.contactId = contactId;
                if (frm.ShowDialog()== DialogResult.OK)
                {
                    BindGrid();
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dgContacts.DataSource = repository.Search(txtSearch.Text);
        }
    }
}